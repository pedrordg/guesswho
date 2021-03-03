using System.Collections.Generic;

namespace GuessWho.Execution.Dtos
{
    public class PlayerDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
         
        public IEnumerable<PlayerDto> Friends { get; set; }
    }
}
