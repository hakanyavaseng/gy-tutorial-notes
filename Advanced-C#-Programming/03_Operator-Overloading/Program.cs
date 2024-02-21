Console.WriteLine();

Server s = new();
bool uploadResult  = s >> 3;
bool downloadResult = s << 2;

#region Operator Overloading
Student student = new()
{
    Name = "Hakan"
};

Book book = new()
{
    Name = "Asil Kan",
    Author = "Kazım Yurdakul"
};

Student? result = student + book;

Console.WriteLine(result.Books.First().Name); // Asil Kan


class Student
{
    public Student()
    {
        this.Books = new List<Book>();
    }
    public string? Name { get; set; }
    public List<Book> Books { get; set; }

    // + operator overloading
    public static Student operator +(Student s, Book b)
    {
        s.Books.Add(b);
        return s;
    }

    // + operator overloading with symmetric.
    public static Student operator +(Book b,Student s)
    {
        s.Books.Add(b);
        return s;
    }

    //++ ve -- overloading
    public static Student operator++(Student s)
    {
        //Islemler
        return s;
    }
    public static Student operator --(Student s)
    {
        //Islemler
        return s;
    }
}

class Book
{
    public string Name { get; set; }
    public string Author { get; set; }
}

#endregion

#region Ornek
class Server
{
    static void Download(int fileCount)
    {
        for (int i = 0; i < fileCount; i++)
        {
            Console.WriteLine($"{i + 1}. file downloaded.");
        }
    }

    static void Upload(int fileCount)
    {
        for (int i = 0; i < fileCount; i++)
        {
            Console.WriteLine($"{i + 1}. file uploaded.");
        }
    }

    public static bool operator>>(Server server, int fileCount)
    {
        Upload(fileCount);
        return true;
    }

    public static bool operator <<(Server server, int fileCount)
    {
        Download(fileCount);
        return true;
    }
}

#endregion
