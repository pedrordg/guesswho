using System.Collections.Generic;

namespace GuessWho.Execution.Dtos
{
    public class DeckDto
    {
        public DeckDto()
        {
            Idols = new List<IdolDto>();
        }

        public IEnumerable<IdolDto> Idols { get; set; }
    }
}
