namespace CliNotes
{
    public class NoteService
    {
        private readonly NotesDbContext _context;

        public NoteService(NotesDbContext context)
        {
            _context = context;
        }

        public void AddNote(int userId, string content)
        {
            var user = _context.Users.Find(userId);
            var note = new Note
            {
                Content = content,
                CreationDate = DateTime.UtcNow,
                User = user
            };
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public List<Note> GetNotes(int userId)
        {

            return _context.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreationDate)
                .ToList();
        }

        public bool DeleteNote(int userId, int noteId)
        {
            var note = _context.Notes.Find(noteId);
            if (note != null && note.UserId == userId)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
