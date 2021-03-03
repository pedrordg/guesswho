using AutoMapper;
using GuessWho.Execution.Contracts;
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
    public class PlayerRelationCrud : IPlayerRelationCrud
    {
        private readonly ITable<PlayerRelationEntity> _table;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayerRelationCrud> _logger;
        private readonly IPlayerCrud _playerCrud;

        public PlayerRelationCrud(ITable<PlayerRelationEntity> table, IMapper mapper, IPlayerCrud playerCrud, ILogger<PlayerRelationCrud> logger)
        {
            _table = table;
            _mapper = mapper;
            _logger = logger;
            _playerCrud = playerCrud;
        }

        public async Task AddFriend(AddFriendDto addFriendDto)
        {
            _logger.LogDebug("Creating new relationship");

            var entity = _mapper.Map<PlayerRelationEntity>(addFriendDto);

            if ((await _table.QueryAsync(FilterBuilder.CreateForPartitionKeyAndRowKey(entity.PartitionKey, entity.RowKey), 1)).Any())
            {
                throw new Exception("There already exists a relation");
            }

            await _table.InsertAsync(entity);

            await _playerCrud.AddFriend(addFriendDto);
        }

        public async Task RemoveFriend(RemoveFriendDto removeFriendDto)
        {
            _logger.LogDebug("Deleting existing relationship");

            var entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKeyAndRowKey(removeFriendDto.PlayerId, removeFriendDto.FriendId), 1)).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No relationship exists");
            }

            await _table.DeleteAsync(entity);

            await _playerCrud.RemoveFriend(removeFriendDto);
        }
    }
}
