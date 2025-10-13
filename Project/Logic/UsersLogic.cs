

//This class is not static so later on we can use inheritance and interfaces
public class UserLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UsersModel? CurrentAccount { get; private set; }
    private UsersAccess _access = new();

    public UserLogic()
    {
        // Could do something here

    }

    public static void HandleLogin()
    {
        // als login mogelijk is moet 
    }

    public static bool UserNameValidation(string username)
    {
        // if (username.Length() < 8)
        // {
        //     return false;
        // }
        return true;
    }
    public static bool PasswordValidation(string password)
    {

        return true;
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




