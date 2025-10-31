//This class is not static so later on we can use inheritance and interfaces
public class UserLogic // alle logic layer moet static zijn --> hoeft er geen instanties
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UsersModel? CurrentAccount { get; private set; }
    private static UsersAccess _access = new();

    public UserLogic()
    {
        // Could do something here

    }

    public UsersAccess Access
    {
        get { return _access; }
        set { _access = value; }
    }

    public static UsersModel GetUserID(int id)
    {
        UsersAccess usersAccess = _access;
        return usersAccess.GetById(id);
    }
    
    public static bool HandleLogin(UsersModel user)
    {
        return HandleLogin(user.Name, user.Password);
    }

    public static bool HandleLogin(string username, string password)
    {
        // als login mogelijk is moet
        // kijken of de login in de database staat
        {
            var user = _access.GetByLogIn(username, password);

            if (user != null)
            {
                CurrentAccount = user;
                return true;
            }
            else
            {
                return false;
            }

        }

    }


    public static bool UsernameVal(string? username) => !string.IsNullOrEmpty(username) && username.Length >= 8;
    
    public static bool PasswordVal(string password)
    {
        if (password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit))
        {
            return true;
        }
        return false;
    }

    public static bool HandleCreateAccount(UsersModel account)
    {
        bool valid = true;

        // Call validators
        // ....

        if (valid)
        {
            UsersAccess usersaccess = new UsersAccess();
            usersaccess.Add(account);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void LogOut()
    {
        CurrentAccount = null;
    }
}




