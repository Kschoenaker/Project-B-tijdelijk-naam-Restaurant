using System.ComponentModel.Design;

static class LoginForm
{
    public static void Start()
    {
        Console.Clear();
        Menu.PrintHeader();
        Console.WriteLine("username");



        bool NotLoggedIn = true;
        while (NotLoggedIn)
        {
            Console.WriteLine("username");
            string? UserName = Console.ReadLine();

            Console.WriteLine("password");
            string? PassWord = Console.ReadLine();
            Console.WriteLine("");


            if (UserLogic.UsernameVal(UserName) && UserLogic.PasswordVal(PassWord))
            {
                // find if the account is in the build
                if (UserLogic.HandleLogin(UserName, PassWord))
                {

                    Console.WriteLine("Could not find account");
                    NotLoggedIn = false;
                }
            }
            else
            {
                Console.WriteLine("Username");
                Console.WriteLine("Needs to be at least 8 charachters");
                Console.WriteLine("");
                Console.WriteLine("Password");
                Console.WriteLine("Needs to be at least 8 charachters");
                Console.WriteLine("Should contain a capatal letter");
                Console.WriteLine("Should contain a number");
                
            }

        }
    }
}


