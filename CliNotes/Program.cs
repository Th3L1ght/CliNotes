namespace CliNotes
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NotesDbContext())
            {
                UserService userService = new UserService(context);
                NoteService noteService = new NoteService(context);

                Greeting();
                int userId = UserAuth(userService);

                while (true)
                {
                    Console.WriteLine("Choose the operation: ");
                    Console.WriteLine("1. Create new note.");
                    Console.WriteLine("2. Delete my note.");
                    Console.WriteLine("3. Check all my notes.");
                    Console.WriteLine("Enter \"exit\" to quit the program.");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            HandleAddNote(noteService, userId);
                            break;
                        case "2":
                            HandleDeleteNote(noteService, userId);
                            break;
                        case "3":
                            HandleGetNotes(noteService, userId);
                            break;
                        case "exit":
                            return;
                        default:
                            Console.Clear();
                            ErrorWrite("Wrong operation!");
                            break;
                    }
                }
            }
        }

        // Greeting Message
        public static void Greeting()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to CLI Notes!");
            Console.ResetColor();
        }

        // Method for user authentication
        public static int UserAuth(UserService userService)
        {
            int userId;
            while (true)
            {
                Console.WriteLine("Enter user ID: ");
                if (int.TryParse(Console.ReadLine(), out userId))
                    break;
                else
                {
                    ErrorWrite("Error. Enter a number!");
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Processing...");
            Console.ResetColor();

            userService.FindOrCreateUser(userId);
            Console.Clear();
            SuccessWrite("Welcome to your notes!");
            return userId;
        }

        // Methods for handling notes operations
        public static void HandleAddNote(NoteService noteService, int userId)
        {
            Console.Clear();
            Console.Write("Enter the content for your note: ");
            string content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(content))
            {
                ErrorWrite("Note content cannot be empty! Please try again.");
                return;
            }
            noteService.AddNote(userId, content);
            SuccessWrite("Note created successfully!");
        }

        public static void HandleDeleteNote(NoteService noteService, int userId)
        {
            Console.Clear();
            Console.Write("Enter the id of the note you want to delete: ");
            if (int.TryParse(Console.ReadLine(), out int noteId))
            {
                if (noteService.DeleteNote(userId, noteId))
                {
                    SuccessWrite("Note deleted successfully.");
                }
                else
                {
                    ErrorWrite("Note not found! Try again!");
                }
            }
            else
            {
                ErrorWrite("Error! Write a number!");
                return;
            }
        }

        public static void HandleGetNotes(NoteService noteService, int userId)
        {
            Console.Clear();
            List<Note> notes = noteService.GetNotes(userId);
            foreach (var note in notes)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {note.Id}\nCreated at: {note.CreationDate}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Content: {note.Content}");
            }
            if (notes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("You have no notes yet.");
            }
            Console.ResetColor();
        }

        // Methods for colored console output for success and error messages
        public static void ErrorWrite(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void SuccessWrite(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
