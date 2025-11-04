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

    public void Update(ReservationModel theme)
    {
        string sql = $@"
            UPDATE {Table}
            SET ThemeName = @ThemeName
            WHERE ID = @ID";
        _connection.Execute(sql, theme);
    }

    public void Delete(ReservationModel theme)
    {
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { theme.ID });
    }

    public List<ThemeModel> GetByThemeID(int themeId)
    {
        string sql = $"SELECT * FROM {Table} WHERE ID = @ThemeId";
        return _connection.Query<ThemeModel>(sql, new { ThemeId = themeId }).ToList();
    }

    public int GetLastInsertedId()
    {
        string sql = "SELECT last_insert_rowid();";
        return _connection.ExecuteScalar<int>(sql);
    }
}