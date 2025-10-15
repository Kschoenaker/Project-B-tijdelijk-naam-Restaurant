public static class RegistrationLogic
{




    public static void MakeAccount(UsersModel user)
    {

        UsersAccess access = new();
        access.Add(user);
        Console.WriteLine("Account is made");
    }


    public static bool DuplicateAccount(UsersModel user)
    {
        UsersAccess access = new();

        if (access.GetByLogIn(user.Name, user.Password) is not null)
        {
            return false;
        }

        return true;

    }

}