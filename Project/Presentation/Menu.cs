static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        if (AccountsLogic.CurrentAccount is null)
        {
            int selectedOption = 0;
            ConsoleKey key;
            List<string> options = new List<string>(["Make reservation", "See reservation", "Log out"]);
            do
            {
                Console.Clear();
                PrintHeader();

                Console.WriteLine("Use ⬆️/⬇️ to navigate and Enter to select option");

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
                            // See reservations
                            break;
                        case 2:
                            AccountsLogic.LogOut();
                            break;
                    }
                }
            } while (AccountsLogic.CurrentAccount is null);
        }
        else
        {
            // Log in form
        }
    }

    public static void PrintHeader()
    {
        Console.WriteLine("Welcome the system!");
        Console.WriteLine();
    }
}