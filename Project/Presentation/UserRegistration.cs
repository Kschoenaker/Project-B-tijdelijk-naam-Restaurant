static class Registration
{


    public static void Start()
    {

        string ussername, password, email;

        Console.WriteLine("Registration:");
        Console.WriteLine("---------------------------------------------------------");


        while (true)
        {
            Console.WriteLine("Please enter your username ");
            Console.WriteLine("Must be 8-15 characters long");
            ussername = Console.ReadLine();
            if (UsersModel.UsernameValidator(ussername))
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
        Console.WriteLine("Please enter your email ( @gmail.com)");
            email = Console.ReadLine();
            if (UsersModel.EmailValidator(email))
            {
                break;
            }
            Console.WriteLine("Invalid email");

        } 

    }
}