using AutoMapper;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Dtos;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution.Table
{
    public class PlayerFetcher : IPlayerFetcher
    {
        private readonly ITable<PlayerEntity> _playerTable;
        private readonly IMapper _mapper;
        private readonly IPlayerRelationFetcher _playerRelationFetcher;

        public PlayerFetcher(ITable<PlayerEntity> playerTable, IMapper mapper, IPlayerRelationFetcher playerRelationFetcher)
        {
            _playerTable = playerTable;
            _mapper = mapper;
            _playerRelationFetcher = playerRelationFetcher;
        }

        public async Task<PlayerDto> GetById(string playerId)
        {
            IEnumerable<PlayerEntity> players = await _playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey(playerId));

            return players.Select(idol =>
            {
                var player = _mapper.Map<PlayerDto>(idol);
                player.Friends = GetPlayerFriends(playerId).Result;
                return player;
            }).FirstOrDefault();
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayerFriends(string playerId)
        {
            var friendIds = await _playerRelationFetcher.GetPlayerFriendIds(playerId);

            IEnumerable<PlayerEntity> friends = await _playerTable.QueryAsync(FilterBuilder.CreateForPartitionKeys(friendIds));

            return friends.Select(f => _mapper.Map<PlayerDto>(f));
        }
    }
}
