public static class ThemePresentation
{
    
public static bool ChoiceDishType(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("Edit Theme");
                // gaat naar een list van themes formatted
                ShowAllThemes();
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

public static void CreateNewTheme()
    {
        Console.WriteLine("Enter a Theme Name");


        string themeName = Console.ReadLine();

        if (ThemeLogic.IsThemeNameValid(themeName))
        {
            ThemeModel themeAdded = ThemeLogic.Add(themeName);            
        }
    }

public static void ShowAllThemes()
    {
        Console.WriteLine("");

        ThemeLogic connect = new();
        List<ThemeModel> AllThemes = connect.AllThemes();

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