

//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }
    private AccountsAccess _access = new();

    public AccountsLogic()
    {
        // Could do something here

    }

    public static bool HandleCreateAccount(UsersModel account)
    {
        bool valid = true;

        // Call validators
        // ....

        if (valid)
        {
            UsersAccess.Add(account);
            return true;
        }
        else
        {
            return false;
        }
    }
}




