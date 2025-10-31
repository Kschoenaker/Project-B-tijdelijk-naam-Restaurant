using Microsoft.Data.Sqlite;

using Dapper;


public class ReservationAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Reservation";

    public void Add(ReservationModel reservation)
    {
        string sql = $@"
            INSERT INTO {Table} (Time, NumPeople, Remark, Status, Users_ID)
            VALUES (@Time, @NumPeople, @Remark, @Status, @Users_ID)";
        _connection.Execute(sql, reservation);
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $@"
            UPDATE {Table}
            SET Time = @Time,
                NumPeople = @NumPeople,
                Remark = @Remark,
                Status = @Status,
                Users_ID = @Users_ID
            WHERE ID = @ID";
        _connection.Execute(sql, reservation);
    }

    public void Delete(ReservationModel reservation)
    {
        string sql = $@"
            DELETE FROM {Table}
            WHERE ID = @ID";
        _connection.Execute(sql, new { reservation.ID });
    }

    public List<ReservationModel> GetByUserID(int userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE Users_ID = @UserId";
        return _connection.Query<ReservationModel>(sql, new { UserId = userId }).ToList();
    }

    public int GetLastInsertedId()
    {
        string sql = "SELECT last_insert_rowid();";
        return _connection.ExecuteScalar<int>(sql);
    }
}