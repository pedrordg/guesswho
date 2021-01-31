using System;

namespace GuessWho.Execution.Dtos
{
    public class UpdateIdolDto
    {
        public string Id { get; set; }

        public string ThemeId { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
