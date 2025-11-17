using Microsoft.Data.Sqlite;

using Dapper;


public class DishThemeRecordsAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "DishThemeRecords";

    public void Add(DishThemeRecordModel item)
    {
        string sql = $@"
            INSERT INTO {Table} (Dish_ID, Theme_ID)
            VALUES (@Dish_ID, @Theme_ID)";
        _connection.Execute(sql, item);
    }

    // public void Update(DishThemeRecordModel item)
    // {
    //     string sql = $@"
    //         UPDATE {Table}
    //         SET Time = @Time,
    //             NumPeople = @NumPeople,
    //             Remark = @Remark,
    //             Status = @Status,
    //             Users_ID = @Users_ID
    //         WHERE ID = @ID";
    //     _connection.Execute(sql, item);
    // }

    public void Delete(DishThemeRecordModel item)
    {
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { item.ID });
    }

    public List<DishThemeRecordModel> GetByThemeID(int themeId)
    {
        string sql = $"SELECT * FROM {Table} WHERE ID = @ThemeID";
        return _connection.Query<DishThemeRecordModel>(sql, new { Theme_ID = themeId }).ToList();
    }
}