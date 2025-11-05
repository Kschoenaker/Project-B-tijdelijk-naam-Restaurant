using Microsoft.VisualBasic;

static class AdminMenu
{

    static public void Start()
    {

        while (true)
        {
            Console.WriteLine("[P] To see admin panel");
            Console.WriteLine("[M] To make themes");
            Console.WriteLine("[A] To add themes to a Month");
            Console.WriteLine("[F] To add food to a theme");
            Console.WriteLine("[L] To log out");

            string choice = Console.ReadLine().ToUpper();
            ThemeLogic themelogic = new();
            switch (choice)
            {
                case "P":
                    // GoAdminPanel();
                    break;

                case "M":
                    Console.WriteLine("Type your new theme:");
                    string theme = Console.ReadLine();
                    themelogic.MakeTheme(theme);
                    break;

                case "A":
                    themelogic.AddTheme();
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
}


