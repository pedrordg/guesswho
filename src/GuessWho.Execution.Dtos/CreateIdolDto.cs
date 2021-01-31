namespace GuessWho.Execution.Dtos
{
    public class CreateIdolDto
    {
        public string ThemeId { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
