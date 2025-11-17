using Microsoft.Data.Sqlite;

using Dapper;


public class ThemeAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Theme";

    public void Add(ThemeModel theme)
    {
        string sql = $@"
            INSERT INTO {Table} (ThemeName)
            VALUES (@ThemeName)";
        _connection.Execute(sql, theme);
    }

    public void Update(ThemeModel theme)
    {
        string sql = $@"
            UPDATE {Table}
            SET ThemeName = @ThemeName
            WHERE ID = @ID";
        _connection.Execute(sql, theme);
    }

    public void Delete(ThemeModel theme)
    {
        // _connection.Open();
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { theme.ID });

    }

    public List<ThemeModel> GetByThemesID(int themeId)
    {
        string sql = $"SELECT * FROM {Table} WHERE ID = @ThemeID";
        return _connection.Query<ThemeModel>(sql, new { ThemeId = themeId }).ToList();
    }

    public ThemeModel GetThemeByName(string themeName)
    {
        string sql = $"SELECT * FROM {Table} WHERE ThemeName = @ThemeName";
        return _connection.Query<ThemeModel>(sql, new { ThemeName = themeName }).FirstOrDefault();
    }

    public int? GetThemeIdByNamecanbenull(string themeName)
    {
        string sql = $"SELECT ID FROM {Table} WHERE ThemeName = @ThemeName";
        return _connection.ExecuteScalar<int?>(sql, new { ThemeName = themeName });
    }
    public int GetThemeIdByName(string themeName)
    {
        string sql = $"SELECT ID FROM {Table} WHERE ThemeName = @ThemeName";
        return _connection.ExecuteScalar<int>(sql, new { ThemeName = themeName });
    }

    public List<string> GetAllThemeNames()
    {
    string sql = $"SELECT ThemeName FROM {Table}";
    return _connection.Query<string>(sql).ToList();
    }

    // public List<ThemeModel> GetWithID(int ID){


    // // make list with all the dishes in this theme
    // }


    public int GetLastInsertedId()
    {
        string sql = $"SELECT MAX(Id) FROM {Table}";
        return _connection.ExecuteScalar<int>(sql);
    }

}