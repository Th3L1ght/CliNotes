namespace CliNotes
{
    public class UserService
    {
        public User FindOrCreateUser(int userId)
        {
            using (var db = new NotesDbContext())
            {
                var user = db.Users.Find(userId);

                if(user == null)
                {
                    user = new User(userId);
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return user;
            }
        }
    }
}
