public class UsersModel
{
    public Int64 ID { get; set; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public UsersModel(Int64 id, string email, string password, string name)
    {
        ID = id;
        EmailAddress = email;
        Password = password;
        Name = name;
    }

}



