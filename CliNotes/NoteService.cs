namespace CliNotes
{
    public class NoteService
    {
        public void AddNote(int userId, string content)
        {
            using (var db = new NotesDbContext())
            {
                var user = db.Users.Find(userId);
                var note = new Note
                {
                    Content = content,
                    CreationDate = DateTime.UtcNow,
                    User = user
                };
                db.Notes.Add(note);
                db.SaveChanges();
            }
        }

        public List<Note> GetNotes(int userId)
        {
            using (var db = new NotesDbContext())
            {
                return db.Notes
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreationDate)
                    .ToList();
            }
        }

        public bool DeleteNote(int userId, int noteId)
        {
            using (var db = new NotesDbContext())
            {
                var note = db.Notes.Find(noteId);
                if(note != null && note.UserId == userId)
                {
                    db.Notes.Remove(note);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
    }
}
