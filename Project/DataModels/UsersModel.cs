using System.ComponentModel;

public class UsersModel
{
    public Int64 ID { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }
    public string Name { get; set; }

    public UsersModel() { }

    public UsersModel(Int64 id, string email, string password, string name)
    {
        ID = id;
        Email = email;
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


        if (username.Length < 4 || username.Length > 15)
        {
            return false;
        }


        return true;
    }
    public static bool EmailValidator(string email)

    {
        List<string> emailDomains = new()
{
    "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com", "aol.com",
    "mail.com", "gmx.com", "live.com", "msn.com", "me.com", "fastmail.com",
    "protonmail.com", "proton.me", "tutanota.com", "posteo.de", "runbox.com",
    "yandex.com", "yandex.ru", "qq.com", "163.com", "126.com", "naver.com", "rediffmail.com",
    "seznam.cz", "mail.ru",
    "tempmail.com", "mailinator.com", "guerrillamail.com", "10minutemail.com", "throwawaymail.com",
    "company.com", "business.com", "enterprise.com", "organization.org", "school.edu"
};




        foreach (string domain in emailDomains)
        {
            if (email.EndsWith("@" + domain))
            {
                return true;
            }
        }
        return false;
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