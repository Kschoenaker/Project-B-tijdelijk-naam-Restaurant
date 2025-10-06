public class ThemeCalanderModel
{
    public int ID { get; set; }
    public DateTime ThemeDate { get; set; }
    public int Theme_ID { get; set; }

    public ThemeCalanderModel(int id, DateTime themeDate, int theme_ID)
    {
        ID = id;
        ThemeDate = themeDate;
        Theme_ID = theme_ID;
    }
}