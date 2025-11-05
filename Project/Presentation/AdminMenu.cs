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
            Console.WriteLine("[F] To add Dish to a theme");
            Console.WriteLine("[L] To log out");

            string choice = Console.ReadLine().ToUpper();
            ThemeLogic themelogic = new();
            DishLogic dishlogic = new();

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


                    if (themelogic.ThemeCheck() == 0)
                    {
                        Console.WriteLine("No themes added !");

                    }
                    else
                    {
                        Console.WriteLine("Write a theme to add a dish to:");
                        string themeuser = Console.ReadLine();
                        //themelogic.checkTheme(themeuser);

                        Console.WriteLine("Write your dishname to add to theme:");
                        string dishname = Console.ReadLine();

                        Console.WriteLine("Write your price of the dish:");
                        double dishprice = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Write your type of the dish:");
                        string dishtype = Console.ReadLine();

                        dishlogic.AddDish(themeuser, dishname, dishprice, dishtype);
                    }
                    break;
                case "L":
                    Console.WriteLine("Logging out...");
                    return; // exits the method, which also ends the loop
            }
        }

    }
}


