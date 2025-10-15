static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        // Add default tables when program starts
        TableLogic.AddDefaultTables();

        int selectedOption = 0;
        ConsoleKey key;
        List<string> options = new List<string>(["Make reservation", "See reservation", "Log out", "Exit"]);

        bool runCode = true;
        while (runCode)
        {
            if (UserLogic.CurrentAccount is not null)
            {
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
                                ReservationLogic.HandleSeeReservation(UserLogic.CurrentAccount);
                                break;
                            case 2:
                                UserLogic.LogOut();
                                break;
                            case 3:
                                runCode = false;
                                break;
                        }
                    }
                } while (UserLogic.CurrentAccount is not null);
            }
            else
            {
                PreStart();
            }
        }
    }

    public static void PrintHeader()
    {
        Console.WriteLine("Welcome the system!");
        Console.WriteLine();
    }

    public static void PreStart()
    {
        int selectOption = 0;
        ConsoleKey key;
        List<string> preStartOptions = new List<string>(["Log in", "Register"]);

        do
        {
            Console.Clear();
            PrintHeader();

            Console.WriteLine("Use ↑/↓ to navigate and Enter to select option");


            for (int i = 0; i < preStartOptions.Count; i++)
            {
                // Colors
                if (i == selectOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(preStartOptions[i]);
            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectOption--;
                if (selectOption < 0)
                    selectOption = preStartOptions.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectOption++;
                if (selectOption >= preStartOptions.Count)
                    selectOption = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                //Switch for the methodes
                switch (selectOption)
                {
                    case 0:
                        Console.WriteLine("Go to log in");
                        LoginForm.Start();
                        break;
                    case 1:
                        // See reservations
                        Console.WriteLine("Go to register");
                        UserRegistration.Start();
                        break;
                }
            }
        } while (UserLogic.CurrentAccount is null);




    }


}