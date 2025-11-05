using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;

public class TableAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Tables";

    // Add a new table record
    public void Add(TablesModel table)
    {
        string sql = $@"INSERT INTO {Table} (TableSeats, TablesName) VALUES (@TableSeats, @TablesName)";
        _connection.Execute(sql, table);
    }

    // Update an existing table record
    public void Update(TablesModel table)
    {
        string sql = $@"UPDATE {Table} SET TableSeats = @TableSeats, TablesName = @TablesName WHERE ID = @ID";
        _connection.Execute(sql, table);
    }

    // Delete a table record
    public void Delete(TablesModel table)
    {
        string sql = $@"DELETE FROM {Table} WHERE ID = @ID";
        _connection.Execute(sql, table);
    }

    // Get all table records
    public List<TablesModel> GetAll()
    {
        string sql = $@"SELECT * FROM {Table}";

        _connection.Open();
        var result = _connection.Query<TablesModel>(sql);
        _connection.Close();
        return result.ToList();
    }

    // Get all tables that do NOT have an ACTIVE reservation on a specific day
    public List<TablesModel> GetUnreservedTablesByDate(DateTime date)
    {
        string sql = @"
            SELECT t.ID, t.TableSeats, t.TablesName
            FROM Tables t
            WHERE NOT EXISTS (
                SELECT 1
                FROM TableRecords tr
                JOIN Reservation r ON tr.Reservation_ID = r.ID
                WHERE tr.Tables_ID = t.ID
                  AND DATE(r.Time) = DATE(@Date)
                  AND r.Status = 'Active'
            );
        ";

        var result = _connection.Query<TablesModel>(sql, new { Date = date.Date });
        return result.ToList();
    }

    // Check if the Table table is empty
    public bool IsTableEmpty()
    {
        string sql = $@"SELECT COUNT(1) FROM {Table}";

        _connection.Open();
        int count = _connection.ExecuteScalar<int>(sql);
        _connection.Close();
        return count == 0; // true if no tables exist
    }
}
