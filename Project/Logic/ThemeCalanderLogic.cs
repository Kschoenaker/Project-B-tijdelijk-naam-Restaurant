public class ThemeCalanderLogic
{
    public static ThemeCalanderModel? GetCurrentThemeCalander()
    {
        return GetThemeCalanderByDate(DateTime.Now);
    }

    public static ThemeCalanderModel? GetThemeCalanderByDate(DateTime time)
    {
        var access = new ThemeCalanderAccess();
        var times = access.GetAllThemeDate()
                          .OrderBy(t => t.ThemeDate)
                          .ToList();

        ThemeCalanderModel? current = null;

        for (int i = 0; i < times.Count; i++)
        {
            var t = times[i];

            // last theme in list
            if (i == times.Count - 1)
            {
                if (t.ThemeDate <= time)
                    return t;
            }
            else
            {
                var next = times[i + 1];

                if (t.ThemeDate <= time && time < next.ThemeDate)
                    return t;
            }
        }

        return null;
    }

}