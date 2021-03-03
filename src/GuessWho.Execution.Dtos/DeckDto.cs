using System.Collections.Generic;

namespace GuessWho.Execution.Dtos
{
    public class DeckDto
    {
        public IEnumerable<IdolDto> Idols { get; set; }
    }
}
