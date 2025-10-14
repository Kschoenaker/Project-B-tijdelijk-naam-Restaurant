static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        // als login succesful -->

        if (UserLogic.CurrentAccount is null)
        {
            LoginForm.Start();

        }
        
        if (UserLogic.CurrentAccount is not null)
        {
            int selectedOption = 0;
            ConsoleKey key;
            List<string> options = new List<string>(["Make reservation", "See reservation", "Log out"]);
            do
            {
                Console.Clear();
                PrintHeader();

                Console.WriteLine("Use ↑/↓ to navigate and Enter to select option");

                for (int i = 0; i < options.Count; i++)
                {
                    // Colors
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);
                }

                Console.ResetColor();

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption < 0)
                        selectedOption = options.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption >= options.Count)
                        selectedOption = 0;
                }
                else if (key == ConsoleKey.Enter)
                {
                    //Switch for the methodes
                    switch (selectedOption)
                    {
                        case 0:
                            ReservationLogic.HandleReservationForm();
                            break;
                        case 1:
                            ReservationLogic.HandleSeeReservation(AccountsLogic.CurrentAccount);
                            break;
                        case 2:
                            UserLogic.LogOut();
                            break;
                    }
                }
            } while (UserLogic.CurrentAccount is not null);
        }
        else
        {
            LoginForm.Start();
            

        }
    }

    public static void PrintHeader()
    {
        Console.WriteLine("Welcome the system!");
        Console.WriteLine();
    }

    //public static void Login()
    // {
    //     var UserLogin = new UserLogin();


    //     Console.WriteLine("And welcome to the login page");
    //     Console.WriteLine("Please enter your username");

    //     string? InpUsername = Console.ReadLine();
    //     UserLogin.login();

    //     // put in the logic to check the validation

    //     Console.WriteLine("Please enter your password");
    //     string InpPassword = Console.ReadLine();

    //     // check the validation in the logic


    //     // if account not fount --> not null 
    //     Console.WriteLine("Could not find account");


    //     // if account is found --> start the new code

    //     // set accounf dound --> account current acount --> naar found account --> dan begint het automatisch


    //     // vataladion password
    //     Console.WriteLine("You did not enter a valid Password");
    //     Console.WriteLine("You did not enter a valid Username");


    // }


}