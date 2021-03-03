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

        public PlayerFetcher(ITable<PlayerEntity> playerTable, IMapper mapper)
        {
            _playerTable = playerTable;
            _mapper = mapper;
        }

        public async Task<PlayerDto> GetById(string playerId)
        {
            IEnumerable<PlayerEntity> players = await _playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey(playerId));

            return players.Select(idol => _mapper.Map<PlayerDto>(idol)).FirstOrDefault();
        }
    }
}
