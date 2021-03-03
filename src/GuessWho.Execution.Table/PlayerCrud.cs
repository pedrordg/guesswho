using AutoMapper;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Dtos;
using GuessWho.Execution.Dtos.Player;
using GuessWho.Execution.Table;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution
{
    public class PlayerCrud : IPlayerCrud
    {
        private readonly ITable<PlayerEntity> _table;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayerCrud> _logger;

        public PlayerCrud(ITable<PlayerEntity> table, IMapper mapper, ILogger<PlayerCrud> logger)
        {
            _table = table;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PlayerDto> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            _logger.LogDebug("Creating new Player");

            PlayerEntity entity = _mapper.Map<PlayerEntity>(createPlayerDto);

            if ((await _table.QueryAsync(FilterBuilder.CreateForPartitionKey(entity.PartitionKey), 1)).Any())
            {
                throw new Exception("There already exists a Player with this id");
            }

            entity = await _table.InsertAsync(entity);
            return _mapper.Map<PlayerDto>(entity);
        }

        public async Task<PlayerDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            _logger.LogDebug("Updating existing player");

            PlayerEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKey(updatePlayerDto.Id))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No Player exists with this id");
            }

            entity.Name = updatePlayerDto.Name;

            entity = await _table.UpdateAsync(entity);
            return _mapper.Map<PlayerDto>(entity);
        }

        public async Task DeletePlayer(DeletePlayerDto deletePlayerDto)
        {
            _logger.LogDebug("Deleting existing Player");

            PlayerEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKey(deletePlayerDto.Id))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No Player exists with this id");
            }

            await _table.DeleteAsync(entity);
        }

        public async Task AddFriend(AddFriendDto addFriendDto)
        {
            PlayerEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKey(addFriendDto.PlayerId))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No Player exists with this id");
            }

            var friendsList = entity.Friends.Split(new char[] { ';' }).ToList();
            if (friendsList.Contains(addFriendDto.FriendId))
            {
                _logger.LogDebug("players are already friends");
                return;
            }
            friendsList.Add(addFriendDto.FriendId);

            entity.Friends = string.Join(';', friendsList);
            await _table.UpdateAsync(entity);
        }

        public async Task RemoveFriend(RemoveFriendDto removeFriendDto)
        {
            PlayerEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKey(removeFriendDto.PlayerId))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No Player exists with this id");
            }

            var friendsList = entity.Friends.Split(new char[] { ';' }).ToList();
            if (!friendsList.Contains(removeFriendDto.FriendId))
            {
                _logger.LogDebug("players are not friends");
                return;
            }
            friendsList.Remove(removeFriendDto.FriendId);

            entity.Friends = string.Join(';', friendsList);
            await _table.UpdateAsync(entity);
        }
    }
}
