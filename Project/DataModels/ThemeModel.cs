public class ThemeModel
{
    public int ID { get; set; }
    public string ThemeName { get; set; }

    public ThemeModel(int id, string themeName)
    {
        ID = id;
        ThemeName = themeName;
    }
}