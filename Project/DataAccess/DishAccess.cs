using Microsoft.Data.Sqlite;

using Dapper;


public class DishAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Dish";

    public void Add(DishModel dish)
    {
        
        string sql = $@"
            INSERT INTO {Table} (Theme_ID, DishName, DishPrice, DishType) 
            VALUES (@Theme_ID, @DishName, @DishPrice, @DishType) ;
            SELECT last_insert_rowid();";

        // ExecuteScalar returns the new ID
        dish.ID = _connection.ExecuteScalar<int>(sql, dish);
        Console.WriteLine(dish.ID);
    }

    public DishModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<DishModel>(sql, new { Email = email });
    }

    public DishModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<DishModel>(sql, new { Id = id });
    }

    public void Update(DishModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, account);
    }

    public void Delete(DishModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = account.ID });
    }

    public DishModel GetByLogIn(string username, string password)
    {
        string sql = $"SELECT * FROM {Table} WHERE name = @Name AND password = @Password";
        return _connection.QueryFirstOrDefault<DishModel>(sql, new { Name = username, Password = password });
    }




}