public class ThemeLogic
{

    public bool AddTheme(string theme)
    {


        ThemeModel thememodel = new(0, theme);


        ThemeAccess themeaccess = new ThemeAccess();
        themeaccess.Add(thememodel);
        return true;

    }
    public int ThemeCheck()
    {


        ThemeAccess themeaccess = new ThemeAccess();
        int number = themeaccess.GetLastInsertedId();
        return number;

    }
}