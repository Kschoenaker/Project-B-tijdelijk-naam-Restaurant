public static class ThemePresentation
{


public static bool DisThemeManagementhMenu(int selectedOption)
    {
        switch (selectedOption)
        {
            //PrintToScreen["ThemeManagement"] = new List<string> { "Theme", "Dish", "Go back" };
            case 0:
                Console.WriteLine("====Theme====");
                NavigationLogic.NavigateChoices("Theme" , ChoiceThemeType);
                // Gaat naar theme managment
                break;
            case 1:
                Console.WriteLine("====Dish====");
                NavigationLogic.NavigateChoices("Dish", ChoiceThemeType);
                break;
            case 2:
                return false; // go back
        }
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
        return true;
    }    

    
public static bool ChoiceThemeType(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("Edit theme");
                // gaat naar een list van themes formatted
                AdminPresentation.TestTheme();
                break;
            case 1:
                Console.WriteLine("Add new theme");
                // vraagt om een nieuwe nieuwe thwmw
                CreateNewTheme();
                break;
            case 2:
                return false; // go back
        }
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
        return true;
    }


public static bool DishMenu(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("Dish Overview");
                // gaat naar een list van themes formatted
                AdminPresentation.TestTheme();
                break;
            case 1:
                Console.WriteLine("New Dish");
                CreateNewTheme();
                break;
            case 2:
                return false; // go back
        }
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
        return true;
    }




public static void CreateNewTheme()
    {
        Console.Clear();
        Console.WriteLine("Enter a Theme Name");


        string themeName = Console.ReadLine();
        string confirmTheme = "";

        while (confirmTheme != "Y" && confirmTheme != "N")
        {
            Console.WriteLine($"Do you want to create {themeName} as the new themed [Y/N]");
            confirmTheme = Console.ReadLine().ToUpper();

            if (confirmTheme == "Y")
            {
                
                ThemeModel themeAdded = ThemeLogic.Add(themeName);
                Console.WriteLine("Theme created!");
            }
            else if (confirmTheme == "N")
            {
                NavigationLogic.NavigateChoices("Theme", ChoiceThemeType);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input, please enter Y or N.\n");
            }
        }
    }
            //Console.WriteLine("You have already ");

public static void ShowAllThemes()
    {
        Console.Clear();
        Console.WriteLine("");

        ThemeLogic connect = new();
        List<ThemeModel> AllThemes = ThemeLogic.AllThemes();

        //TableUI<ThemeModel> MakeThemeTable = new();

        foreach(ThemeModel theme in AllThemes)
        {
            Console.WriteLine($"{theme.ThemeName}");
        }





        // get a list of all themes
        // show themes in table
        // make possible to see menu when clicking on the theme


    }

}