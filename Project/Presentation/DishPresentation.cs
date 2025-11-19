public static class DishPresentation
{
    

    public static bool DishMenu(int selectedOption)
    {
        switch (selectedOption)
        {
            //PrintToScreen["AdminMenu"] = new List<string> { "See Reservations", "Theme Management", "Theme Planner", "Log out" };
            case 0:
                Console.WriteLine("Dish Overview");
                // gaat naar een list van themes formatted
                AdminPresentation.TestTheme();
                break;
            case 1:
                Console.WriteLine("New Dish");
                break;
            case 2:
                return false; // go back
        }
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
        return true;
    }

}