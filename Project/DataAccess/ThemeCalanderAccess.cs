using Microsoft.Data.Sqlite;

using Dapper;


public class ThemeCalanderAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "ThemeCalander";

    public void Add( ThemeCalanderModel themecalandermodel)
    {
        string sql = $@"
            INSERT INTO {Table} (ThemeDate,Theme_ID)
            VALUES (@ThemeDate,@Theme_ID)";
        _connection.Execute(sql, themecalandermodel);
    }

    public void Update( ThemeCalanderModel themecalandermodel)
    {
        string sql = $@"
            UPDATE {Table}
            SET ID = @ID,
            Theme_ID = @Theme_ID
            WHERE ThemeDate = @ThemeDate";
        _connection.Execute(sql, themecalandermodel);
    }

    public void Delete( ThemeCalanderModel themecalandermodel)
    {
        // _connection.Open();
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { themecalandermodel.ID });

    }

public int? GetIDByDatetime(DateTime date)
{
    string sql = $"SELECT ID FROM {Table} WHERE ThemeDate = @ThemeDate";

    int? id = _connection.QuerySingleOrDefault<int?>(sql, new { ThemeDate = date });

    return id;
}

    public List<ThemeCalanderModel> GetBythemeID(string themeid)
    {
        string sql = $"SELECT * FROM {Table} WHERE Theme_ID = @ThemeName";
        return _connection.Query<ThemeCalanderModel>(sql, new { ThemeName = themeid }).ToList();
    }

    public List<DateTime> GetAllThemeDate()
    {
    string sql = $"SELECT ThemeDate FROM {Table}";
    return _connection.Query<DateTime>(sql).ToList();
    }



    public int GetLastInsertedId()
    {
        string sql = $"SELECT MAX(Id) FROM {Table}";
        return _connection.ExecuteScalar<int>(sql);
    }


}