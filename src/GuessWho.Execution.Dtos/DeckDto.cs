using System.Collections.Generic;

namespace GuessWho.Execution.Dtos
{
    public class DeckDto
    {
        public IEnumerable<IEnumerable<IdolDto>> Idols { get; set; }
    }
}
