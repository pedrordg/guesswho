using AutoMapper;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Dtos;
using GuessWho.Models;
using Matrix.PaymentGateway.Infra.Blob.Contracts;
using Matrix.PaymentGateway.Infra.TableStorage.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution.Table
{
    public class IdolFetcher : IIdolFetcher
    {
        private readonly ITable<IdolEntity> _idolTable;
        private readonly IMapper _mapper;
        private readonly IBlobReader _blobReader;

        public IdolFetcher(ITable<IdolEntity> idolTable, IMapper mapper, IBlobReader blobReader)
        {
            _idolTable = idolTable;
            _mapper = mapper;
            _blobReader = blobReader;
        }

        public async Task<IEnumerable<IdolDto>> GetIdolsByDeck(string deckId)
        {
            string query = FilterBuilder.CreateForPartitionKey(deckId);

            IEnumerable<IdolEntity> idols = await _idolTable.QueryAsync(query);

            return idols.Select(idol => 
            {
                var dto = _mapper.Map<IdolDto>(idol);
                dto.Pic = _blobReader.DownloadContent(string.Format("{0}/{1}",idol.PartitionKey, idol.RowKey)).Result;
                return dto;
            });
        }

        public async Task<IdolDto> GetIdolById(string deckId, string cardId)
        {
            string query = FilterBuilder.CreateForPartitionKeyAndRowKey(deckId, cardId);

            IEnumerable<IdolEntity> idols = (await _idolTable.QueryAsync(query));

            return idols.Select(idol =>
            {
                var dto = _mapper.Map<IdolDto>(idol);
                dto.Pic = _blobReader.DownloadContent(string.Format("{0}/{1}", idol.PartitionKey, idol.RowKey)).Result;
                return dto;
            }).FirstOrDefault();
        }
    }
}
