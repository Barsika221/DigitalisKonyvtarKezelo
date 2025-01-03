using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DigitalisKonyvtarKezelo
{
    internal class FileManager
    {
        private const string UsersFilePath = "users.txt";

        public static List<User> LoadUsers()
        {
            EnsureFileExists();

            List<User> users = new List<User>();
            string[] lines = File.ReadAllLines(UsersFilePath);

            // Ha a fájl üres vagy csak a fejlécet tartalmazza, visszatérünk az üres listával
            if (lines.Length <= 1)
            {
                return users;
            }

            // Az első sort (fejlécet) kihagyjuk
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length == 4)
                {
                    users.Add(new User(parts[0], parts[1], parts[2], bool.Parse(parts[3])));
                }
            }

            return users;
        }

        private static void EnsureFileExists()
        {
            if (!File.Exists(UsersFilePath))
            {
                using (StreamWriter sw = File.CreateText(UsersFilePath))
                {
                    sw.WriteLine("username,password,email,is_admin");
                }
            }
        }

        public static void SaveUsers(List<User> users)
        {
            EnsureFileExists();

            using (StreamWriter sw = new StreamWriter(UsersFilePath))
            {
                sw.WriteLine("username,password,email,is_admin");
                foreach (var user in users)
                {
                    sw.WriteLine($"{user.Username},{user.Password},{user.Email},{user.IsAdmin}");
                }
            }
        }
        public static void SaveBooks(List<Book> books)
        {
            string path = "books.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                foreach (Book book in books)
                {
                    file.WriteLine(book.ToString());
                }
            }
        }
        public static List<Book> LoadBooks()
        {
            string path = "books.txt";
            List<Book> books = new List<Book>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length >= 4)
                    {
                        string title = parts[0];
                        string author = parts[1];
                        int publicationYear = int.Parse(parts[2]);
                        string category = parts[3];
                        Book book = new Book(title, author, publicationYear, category);
                        books.Add(book);
                    }
                }
            }
            return books;
        }
    }
}
