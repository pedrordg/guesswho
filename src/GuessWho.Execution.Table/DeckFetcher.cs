using AutoMapper;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Dtos;
using GuessWho.Infra.Blob.Contracts;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution.Table
{
    public class DeckFetcher : IDeckFetcher
    {
        private readonly ITable<IdolEntity> _idolTable;
        private readonly IMapper _mapper;
        private readonly IBlobReader _blobReader;

        public DeckFetcher(ITable<IdolEntity> idolTable, IMapper mapper, IBlobReader blobReader)
        {
            _idolTable = idolTable;
            _mapper = mapper;
            _blobReader = blobReader;
        }

        public async Task<DeckDto> GetDeckById(string deckId)
        {
            string query = FilterBuilder.CreateForPartitionKey(deckId);

            IEnumerable<IdolEntity> idols = await _idolTable.QueryAsync(query);

            var result = new DeckDto();
            result.Idols = idols
                .Select(idol =>
                {
                    var dto = _mapper.Map<IdolDto>(idol);
                    dto.Pic = _blobReader.DownloadContent(string.Format("{0}/{1}", idol.PartitionKey, idol.RowKey)).Result;
                    return dto;
                })
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 6)
                .Select(x => x.Select(v => v.Value));

            return result;
        }
    }
}
