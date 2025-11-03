using System.ComponentModel.Design;

static class LoginForm
{
    public static void Start()
    {
        Console.Clear();
        Header.PrintHeader();

        bool NotLoggedIn = true;
        while (NotLoggedIn)
        {
            Console.WriteLine("Enter username:");
            string? UserName = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Enter password:");
            string? PassWord = Console.ReadLine();
            Console.WriteLine("");

            if (UserLogic.UsernameVal(UserName) && UserLogic.PasswordVal(PassWord))
            {
                // find if the account is in the build
                if (UserLogic.HandleLogin(UserName, PassWord))
                {
                    // Log in valid
                    NotLoggedIn = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Could not find account. Try again.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Username");
                Console.WriteLine("Needs to be at least 4 characters");
                Console.WriteLine("");
                Console.WriteLine("Password");
                Console.WriteLine("Needs to be at least 8 characters");
                Console.WriteLine("Should contain a capital letter");
                Console.WriteLine("Should contain a number");
                
            }

        }
    }
}


