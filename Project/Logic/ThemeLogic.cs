public class ThemeLogic
{

    public bool AddTheme(string theme)
    {


        ThemeModel thememodel = new(0, theme);
        
        
        ThemeAccess themeaccess = new ThemeAccess();
        themeaccess.Add(thememodel);
        return true;

    }
}