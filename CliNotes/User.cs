using System.ComponentModel.DataAnnotations.Schema;

namespace CliNotes
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }
        public DateTime CreationDate { get; set; }
        public List<Note> Notes { get; set; } = new();

        private User() { }

        public User(int id)
        {
            Id = id;
            CreationDate = DateTime.UtcNow;
        }
    }
}
