using AutoMapper;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Dtos;
using GuessWho.Execution.Table;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution
{
    public class IdolCrud : IIdolCrud
    {
        private readonly ITable<IdolEntity> _table;
        private readonly IMapper _mapper;
        private readonly ILogger<IdolCrud> _logger;

        public IdolCrud(ITable<IdolEntity> table, IMapper mapper, ILogger<IdolCrud> logger)
        {
            _table = table;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdolDto> CreateIdol(CreateIdolDto createIdolDto)
        {
            _logger.LogDebug("Creating new idol");

            IdolEntity entity = _mapper.Map<IdolEntity>(createIdolDto);

            if ((await _table.QueryAsync(FilterBuilder.CreateForPartitionKeyAndRowKey(entity.PartitionKey, entity.RowKey), 1)).Any())
            {
                throw new Exception("There already exists an idol with this id");
            }

            await _table.InsertAsync(entity);
            return _mapper.Map<IdolDto>(entity);
        }

        public async Task<IdolDto> UpdateIdol(UpdateIdolDto updateIdolDto)
        {
            _logger.LogDebug("Updating existing idol");

            IdolEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKeyAndRowKey(updateIdolDto.ThemeId, updateIdolDto.Id))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No idol exists with this id");
            }

            entity.Name = updateIdolDto.Name;

            await _table.UpdateAsync(entity);
            return _mapper.Map<IdolDto>(entity);
        }

        public async Task DeleteIdol(DeleteIdolDto deleteIdolDto)
        {
            _logger.LogDebug("Deleting existing idol");

            IdolEntity entity = (await _table.QueryAsync(FilterBuilder.CreateForPartitionKeyAndRowKey(deleteIdolDto.ThemeId, deleteIdolDto.Id))).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("No idol exists with this id");
            }

            await _table.DeleteAsync(entity);
        }
    }
}
