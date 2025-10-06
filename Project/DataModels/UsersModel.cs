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
    public static bool PasswordValidator(string password)
    {
        bool haslower = false;
        bool hasupper = false;
        bool hassymbole = false;
        bool hasnumber = false;
        if (password.Length < 8 || password.Length > 15)
        {
            return false;
        }
        for (int i = 0; i < password.Length; i++)
        {

            if ("abcdefghijklmnoqrstuvwxyz".Contains(password[i]))
            {

                haslower = true;
            }
            if ("ABCDEFGHIJKLMNOPQRSTUVWYZ".Contains(password[i]))
            {

                hasupper = true;
            }
            if ("!@#$%^&*()_+-={}[]:;\"'<>,.?/\\|~`\"".Contains(password[i]))
            {
                hassymbole = true;
            }
            if (password[i] == '@')
            {
                return false;
            }
            if ("124567890".Contains(password[i]))
            {
                hasnumber = true;
            }
            
        }
        if (haslower && hasupper && hassymbole && hasnumber)
        {
            return true;
        }
        return false;
        }
    public static bool UsernameValidator(string username)
    {

   
        if (username.Length < 8 || username.Length > 15)
        {
            return false;
        }


        return true;
         }
    public static bool EmailValidator(string email)
    {
        if (email.Contains("@gmail.com"))
        {
            return true;
        }
        return false;
        }

    }