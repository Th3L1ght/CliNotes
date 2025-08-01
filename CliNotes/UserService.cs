namespace CliNotes
{
    public class UserService
    {
        private readonly NotesDbContext _context;

        public UserService(NotesDbContext context)
        {
            _context = context;
        }

        public User FindOrCreateUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                user = new User(userId);
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return user;
        }
    }
}
