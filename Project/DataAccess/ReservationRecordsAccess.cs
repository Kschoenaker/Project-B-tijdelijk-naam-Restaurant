using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;

public class ReservationRecordsAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "ReservationRecords";

    // Add a new table record
    public void Add(ReservationRecordsModel table)
    {
        string sql = $@"INSERT INTO {Table} (Dish_ID, Reservation_ID) VALUES (@Dish_ID, @Reservation_ID)";
        _connection.Execute(sql, table);
    }

    // Update an existing table record
    public void Update(ReservationRecordsModel table)
    {
        string sql = $@"UPDATE {Table} SET Dish_ID = @Dish_ID, Reservation_ID = @Reservation_ID WHERE ID = @ID";
        _connection.Execute(sql, table);
    }

    // Delete a table record
    public void Delete(ReservationRecordsModel table)
    {
        string sql = $@"DELETE FROM {Table} WHERE ID = @ID";
        _connection.Execute(sql, table);
    }

    // Get all table records
    public List<ReservationRecordsModel> GetAll()
    {
        string sql = $@"SELECT * FROM {Table}";

        _connection.Open();
        var result = _connection.Query<ReservationRecordsModel>(sql);
        _connection.Close();
        return result.ToList();
    }
}
