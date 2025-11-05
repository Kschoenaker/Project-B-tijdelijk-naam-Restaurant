using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;

public class ThemeAccess
{
    private readonly SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private readonly string Table = "Theme";

   
    public void Add(ThemeModel theme)
    {
        _connection.Open();

        string sql = $@"
            INSERT INTO {Table} (ThemeName)
            VALUES (@ThemeName);
            SELECT last_insert_rowid();";

        theme.ID = _connection.ExecuteScalar<int>(sql, theme);

        _connection.Close();

    }

    //zsoek thema via ID
    public ThemeModel GetById(int id)
    {
        _connection.Open();

        string sql = $"SELECT * FROM {Table} WHERE ID = @Id";
        ThemeModel theme = _connection.QueryFirstOrDefault<ThemeModel>(sql, new { Id = id });

        _connection.Close();
        return theme;
    }

    // via naam
    public ThemeModel GetByName(string name)
    {
        _connection.Open();

        string sql = $"SELECT * FROM {Table} WHERE ThemeName = @ThemeName";
        ThemeModel theme = _connection.QueryFirstOrDefault<ThemeModel>(sql, new { ThemeName = name });

        _connection.Close();
        return theme;
    }

    public List<ThemeModel> GetAll()
    {
        _connection.Open();

        string sql = $"SELECT * FROM {Table}";
        List<ThemeModel> themes = _connection.Query<ThemeModel>(sql).AsList();

        _connection.Close();
        return themes;
    }

    // Update thema
    public void Update(ThemeModel theme)
    {
        _connection.Open();

        string sql = $"UPDATE {Table} SET ThemeName = @ThemeName WHERE ID = @ID";
        _connection.Execute(sql, theme);

        _connection.Close();
    }

    // Verwijder thema
    public void Delete(int id)
    {
        _connection.Open();

        string sql = $"DELETE FROM {Table} WHERE ID = @Id";
        _connection.Execute(sql, new { Id = id });

        _connection.Close();

    }
}
