public class ThemeLogic
{

    public bool MakeTheme(string theme)
    {




        ThemeAccess themeaccess = new ThemeAccess();

        int? id = themeaccess.GetThemeIdByNamecanbenull(theme);

        if (id is null)
        {
            ThemeModel thememodel = new(0, theme);
            themeaccess.Add(thememodel);
            return true;
        }

        else
        {
            ThemeModel thememodel = new(id.Value, theme);
            themeaccess.Update(thememodel);
            return true;


        }

    }
    public int? CheckIDbyname(string theme)
    {
        ThemeAccess access = new();

        return access.GetThemeIdByNamecanbenull(theme);
    }
    public int ThemeCheck()
    {


        ThemeAccess themeaccess = new ThemeAccess();
        int number = themeaccess.GetLastInsertedId();
        return number;

    }


    public void checkTheme()
    {
        

    }
    public bool AddTheme()
    {

{
        ThemeLogic themelogic = new();

        if (themelogic.ThemeCheck() == 0)
        {
            Console.WriteLine("No themes added !");
            return false;
        }


        List<string> optionNames = new List<string>() { "Year", "Month", "Theme" };
        List<int> optionValues = new List<int>() { 2025, 1, 0 };
        ThemeAccess themeaccess = new();
        List<string> themes = themeaccess.GetAllThemeNames();



        int selectedOption = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select Year, Month & Theme\n");
            Console.WriteLine("Use ← / → to change option, ↑ / ↓ to change value, Enter to confirm:\n");

            for (int i = 0; i < optionNames.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (i == 2) // Theme
                    Console.Write($" {optionNames[i]}: {themes[optionValues[i]]} ");
                else
                    Console.Write($" {optionNames[i]}: {optionValues[i]} ");

                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    selectedOption--;
                    if (selectedOption < 0) selectedOption = optionNames.Count - 1;
                    break;
                case ConsoleKey.RightArrow:
                    selectedOption++;
                    if (selectedOption >= optionNames.Count) selectedOption = 0;
                    break;
                case ConsoleKey.UpArrow:
                    if (selectedOption == 0)
                        optionValues[selectedOption]++;
                    else if (selectedOption == 1)
                    {
                        optionValues[selectedOption]++;
                        if (optionValues[selectedOption] > 12) optionValues[selectedOption] = 1;
                    }
                    else if (selectedOption == 2)
                    {
                        optionValues[selectedOption]++;
                        if (optionValues[selectedOption] >= themes.Count) optionValues[selectedOption] = 0;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOption == 0)
                        optionValues[selectedOption]--;
                    else if (selectedOption == 1)
                    {
                        optionValues[selectedOption]--;
                        if (optionValues[selectedOption] < 1) optionValues[selectedOption] = 12;
                    }
                    else if (selectedOption == 2)
                    {
                        optionValues[selectedOption]--;
                        if (optionValues[selectedOption] < 0) optionValues[selectedOption] = themes.Count - 1;
                    }
                    break;
            }

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        Console.WriteLine("Theme has been added:");
        for (int i = 0; i < optionNames.Count; i++)
        {
            if (i == 2)
                Console.WriteLine(optionNames[i] + ": " + themes[optionValues[i]]);
            else
                Console.WriteLine(optionNames[i] + ": " + optionValues[i]);
        }


        int year = optionValues[0];
        int month = optionValues[1];
        int theme_id = themeaccess.GetThemeIdByName(themes[optionValues[2]]);

        DateTime themeDate = new DateTime(year, month, 1);
        



        ThemeCalanderAccess themecalanderaccess = new();


        int? id = themecalanderaccess.GetIDByDatetime(themeDate);
        if (id is null)
        {
            ThemeCalanderModel themecalandermodel = new(0, themeDate, theme_id);
                themecalanderaccess.Add(themecalandermodel);
                return true;
        }

        else
        {
            ThemeCalanderModel themecalandermodel = new(id.Value, themeDate, theme_id);
                themecalanderaccess.Update(themecalandermodel);
                return true;


        }

        // Console.WriteLine("Select");
        // string theme = Console.ReadLine();
        // ThemeLogic themelogic = new();
        // themelogic.AddTheme(theme);


    }


    }


}