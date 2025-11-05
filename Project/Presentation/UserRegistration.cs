static class UserRegistration
{


    public static void Start()
    {
        Console.Clear();

        string username, password, email;

        Console.WriteLine("Registration:");
        Console.WriteLine("---------------------------------------------------------");


        while (true)
        {
            Console.WriteLine("Please enter your username ");
            Console.WriteLine("Must be 4-15 characters long");
            username = Console.ReadLine();
            if (UsersModel.UsernameValidator(username))
            {
                break;
            }
            Console.WriteLine("Invalid ussername");
        }


        while (true)
        {
            Console.WriteLine("Please enter your password ");
            Console.WriteLine("Must have: 1 capital letter ( ABCDEF..) | 1 small letter ( abcdef..)| 1  symbole (!#$%^&..) NOT @  | 1 number (12345..)  ");
            Console.WriteLine("Must be: 8-15 characters long");
            password = Console.ReadLine();
            if (UsersModel.PasswordValidator(password))
            {
                break;
            }
            Console.WriteLine("Invalid password");

        }


        while (true)
        {
            Console.WriteLine("Please enter your email ( @gmail.com | @outlook.com | @hotmail.com | ...)");
            email = Console.ReadLine();
            if (UsersModel.EmailValidator(email))
            {
                break;
            }
            Console.WriteLine("Invalid email");


        }
        UsersModel user = new(0, email, password, username);
        RegistrationLogic.MakeAccount(user);
        UserLogic.HandleLogin(user);
    }
    
}