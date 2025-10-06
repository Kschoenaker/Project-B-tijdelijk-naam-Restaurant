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
        }
        if (haslower && hasupper && hassymbole)
        {
            return true;
        }
        return false;
        }
    public static bool Passwordvalidator()
    {

        return true;
         }
    public static bool emailvalidator()
    {
        return true;
        }

    }