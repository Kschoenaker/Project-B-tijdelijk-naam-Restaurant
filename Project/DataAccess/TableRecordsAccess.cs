using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;

public class TableRecordsAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "TableRecords";

    // Add a new table record
    public void Add(TableRecordsModel tableRecord)
    {
        string sql = $@"
            INSERT INTO {Table} (Tables_ID, Reservation_ID)
            VALUES (@Tables_ID, @Reservation_ID)";
        _connection.Execute(sql, tableRecord);
    }

    // Update an existing table record
    public void Update(TableRecordsModel tableRecord)
    {
        string sql = $@"
            UPDATE {Table}
            SET Tables_ID = @Tables_ID,
                Reservation_ID = @Reservation_ID
            WHERE ID = @ID";
        _connection.Execute(sql, tableRecord);
    }

    // Delete a table record
    public void Delete(TableRecordsModel tableRecord)
    {
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { tableRecord.ID });
    }

    // Get all table records for a reservation
    public List<TableRecordsModel> GetByReservation(int reservationId)
    {
        string sql = $@"
            SELECT * FROM {Table}
            WHERE Reservation_ID = @Reservation_ID";

        var result = _connection.Query<TableRecordsModel>(sql, new { Reservation_ID = reservationId });
        return result.ToList();
    }

    // Get all table records
    public List<TableRecordsModel> GetAll()
    {
        string sql = $@"SELECT * FROM {Table}";
        var result = _connection.Query<TableRecordsModel>(sql);
        return result.ToList();
    }
}
