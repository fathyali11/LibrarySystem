using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace SystemLibrary
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public static Users AddUser()
        {
            Console.Write("Enter Name : ");
            string ?name = Console.ReadLine();
            if (name == null)
            {
                throw new InvalidDataException("Please Enter User Name ");
            }
            Console.Write("Enter ID : ");

            int id;
            if(int.TryParse(Console.ReadLine(),out int i))
            {
                id = i;
            }
            else
            {
                throw new InvalidOperationException("Invalid Data");
            }
            return new Users { Name = name, Id = id };
        }
        public override string ToString()
        {
            return $"Name : {this.Name}\t\tID : {this.Id}";
        }
        public override bool Equals(object? obj)
        {
            var user=obj as Users;
            if(user is null)
                return false;
            return this.Name==user.Name && this.Id==user.Id;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = (hash * 13) + Name.GetHashCode();
                hash = (hash * 13) + Id.GetHashCode();
                return hash;
            }
        }

    }
    public class Books
    {
        public string? Name { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }
        public static Books AddBook()
        {
            Console.Write("Enter Book Name : ");
            string ?name = Console.ReadLine();
            if (name == null)
            {
                throw new InvalidDataException("Please Enter User Name ");
            }
            Console.Write("Enter Book ID : ");
            int id;
            if (int.TryParse(Console.ReadLine(), out int i))
            {
                id = i;
            }
            else
            {
                throw new InvalidOperationException("Invalid Data");
            }
            Console.Write("Enter Book Quantity : ");

            int quantity;
            if (int.TryParse(Console.ReadLine(), out int q))
            {
                quantity = q;
            }
            else
            {
                throw new InvalidOperationException("Invalid Data");
            }
            return new Books { Name = name, ID = id, Quantity = quantity };
        }
        public override string ToString()
        {
            return $"{this.Name}\t\t{this.ID}\t\t{this.Quantity}";
        }
        public override bool Equals(object? obj)
        {
            var book = obj as Books;
            if (book is null)
                return false;
            return this.Name==book.Name&& this.ID==book.ID&& this.Quantity==book.Quantity;  
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = (hash * 13) + Name.GetHashCode();
                hash = (hash * 13) + ID.GetHashCode();
                hash = (hash * 13) + Quantity.GetHashCode();
                return hash;
            }
        }
    }
    public class system
    {
        List<Books> LBooks=new List<Books>();
        List<Users> LUsers=new List<Users>();
        List<Users> LUsersBorrowBook = new List<Users>();

        private int Menu()
        {
            int Choice = -1;
            while (Choice == -1)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Library Menu");
                Console.WriteLine("1  : Add Book");
                Console.WriteLine("2  : Search Book By Prefix");
                Console.WriteLine("3  : Print Who Borrowed Book By Name");
                Console.WriteLine("4  : Print Library By Name");
                Console.WriteLine("5  : Print Library By Id");
                Console.WriteLine("6  : Add User");
                Console.WriteLine("7  : User Borrow Book");
                Console.WriteLine("8  : User Return Book");
                Console.WriteLine("9  : Print Users");
                Console.WriteLine("10 : Print Data Of Book By Name");
                Console.WriteLine("11 : Exit");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Enter Your Choice From 1 : 11");
                Console.Write("Your Choice : ");
                if(int.TryParse(Console.ReadLine(),out int choice))
                {
                    Choice = choice;
                }
                else
                {
                    throw new InvalidCastException("This Is Not A Number");
                }
                if (Choice > 11 || Choice < 1)
                {
                    Console.WriteLine("Invalid Choice!!!");
                    Choice = -1;

                }

            }
            return Choice;
        }
        private void PrintLUsers()
        {
            foreach (var lu in LUsers)
                Console.WriteLine(lu);
        }
        private void PrintWhoBorrowedBookByName()
        {
            Console.Write("________________________");
            foreach (var lu in LUsersBorrowBook)
                Console.WriteLine(lu);
            Console.Write("________________________");
        }
        private void PrintLBooksByName()
        {
            List<Books> li = LBooks.OrderBy(book => book.Name).ToList();
            Console.Write("{ ");
            foreach (var item in li)
            {
                Console.Write($"{item.Name} ,");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        private void PrintLBooksByID()
        {
            List<Books> li = LBooks.OrderBy(book => book.ID).ToList();
            Console.Write("{ ");
            foreach (var item in li)
            {
                Console.Write($"{item.Name} ,");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        private void SearchBookByPrefix()
        {
            Console.Write("Enter Prefix : ");
            string pref = Console.ReadLine();
            List<string> prefix =LBooks .Where(x => x.Name.StartsWith(pref)).Select(x => x.Name).ToList();
            Console.WriteLine(string.Join(",", prefix));
        }
        private bool BorrowBookBy()
        {
            Console.Write("Enter Book Name : ");
            string name = Console.ReadLine();
            foreach (var book in LBooks)
            {
                if (book.Name == name)
                {
                    if (book.Quantity < 1)
                    {
                        Console.WriteLine("Sorry! there is no books of this");
                        return false;
                    }
                    else
                    {
                        book.Quantity -= 1;
                        return true;
                    }
                }
            }
            return false;
        }
        private void BorrowBookByName()
        {
            Console.Write("Enter User Name Who Want to Borrow Book : ");
            string name = Console.ReadLine();
            Console.Write("Enter User ID Who Want to Borrow Book : ");
            int id = int.Parse(Console.ReadLine());
            bool IsFound = false;
            foreach (var u in LUsers)
            {
                if (u.Name == name && u.Id == id)
                {
                    IsFound = true;
                    break;
                }
            }
            if (!IsFound)
            {
                Console.WriteLine("InValid Data Of User");
                return;
            }
            if (BorrowBookBy())
            {
                LUsersBorrowBook.Add(new Users { Name = name, Id = id });
            }
        }
        private void DataOfBookBy()
        {
            Console.Write("Enter Book Name Which You Want To Know Information : ");
            string name = Console.ReadLine();
            Books book = LBooks.FirstOrDefault(b => b.Name == name);
            if (book == null)
            {
                Console.WriteLine("there is no book of this name");
            }
            else
            {
                Console.WriteLine($"\n{{\n    Name : {book.Name}\tId : {book.ID}\tQuantity : {book.Quantity}\n}}");
            }
        }
        private void ReturnBook()
        {
            Console.Write("Enter Book Name : ");
            string name = Console.ReadLine();
            Console.Write("Enter Uer ID : ");
            int id = int.Parse(Console.ReadLine());

            foreach (var book in LBooks)
            {
                if (book.Name == name)
                {
                    book.Quantity += 1;
                    LUsers.RemoveAll(x => x.Id == id);
                    return;
                }
            }
            Console.WriteLine("This Book Not Found Before");
        }
        public void Management()
        {
            int Choice = -1;
            while (true)
            {
                try
                {

                    Choice = Menu();
                    if (Choice == 1)
                        LBooks.Add(Books.AddBook());
                    else if (Choice == 2)
                        this.SearchBookByPrefix();
                    else if (Choice == 3)
                        this.PrintWhoBorrowedBookByName();
                    else if (Choice == 4)
                        this.PrintLBooksByName();
                    else if (Choice == 5)
                        this.PrintLBooksByID();
                    else if (Choice == 6)
                        LUsers.Add(Users.AddUser());
                    else if (Choice == 7)
                        this.BorrowBookByName();
                    else if (Choice == 8)
                        this.ReturnBook();
                    else if (Choice == 9)
                        this.PrintLUsers();
                    else if (Choice == 10)
                        this.DataOfBookBy();
                    else if (Choice == 11)
                        break;

                    else
                    {
                        Choice = -1;
                    }
                }
                catch (InvalidCastException inEx)
                {
                    Console.WriteLine(inEx.Message);
                }
                catch(InvalidOperationException opex)
                {
                    Console.WriteLine(opex.Message);
                }
                catch(InvalidDataException opda)
                {
                    Console.WriteLine(opda.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}