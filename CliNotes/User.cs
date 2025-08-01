namespace CliNotes
{
    public class User
    {
        public int Id { get; private set; }
        public DateTime CreationDate { get; set; }
        public List<Note> Notes { get; set; } = new();
    }
}
