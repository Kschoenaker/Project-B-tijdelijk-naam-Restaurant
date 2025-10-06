using Microsoft.Data.Sqlite;

using Dapper;


public class UsersAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Users";

    public void Add(UsersModel account)
    {
        string sql = $"INSERT INTO {Table} (Name, Email, Password) VALUES (@Name, @Email, @Password)";
        _connection.Execute(sql, account);
    }

    public UsersModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<UsersModel>(sql, new { Email = email });
    }

    public void Update(UsersModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, account);
    }

    public void Delete(UsersModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = account.ID });
    }



}