static class AdminMenu
{

    static public void Start()
    {

        while (true)
        {
            Console.WriteLine("[P] To see admin panel");
            Console.WriteLine("[T] To make themes");
            Console.WriteLine("[M] To add themes to a Month");
            Console.WriteLine("[F] To add food to a theme");
            Console.WriteLine("[L] To log out");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "P":
                    // GoAdminPanel();
                    break;

                case "T":
                    GoMakeThemes();
                    break;

                case "M":
                    // GoAddTheme();
                    break;

                case "F":
                    // GoAddFood();
                    break;

                case "L":
                    Console.WriteLine("Logging out...");
                    return; // exits the method, which also ends the loop
            }
        }

    }

    public static void GoMakeThemes()
    {
        Console.WriteLine("Type your new theme:");
        string theme = Console.ReadLine();
        ThemeLogic themelogic = new();
        themelogic.AddTheme(theme);


    }
    


}