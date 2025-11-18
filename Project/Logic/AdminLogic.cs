public static class AdminLogic
{
    // als een admin heb ik een ader statrtchemm

    // the going to menu's logic 
    public static bool IsAdmin(string username, string password)
    {
        if (username == "admin123" && password == "passAdmin123!")// admin username found in database is ok  
        { return true; }
        if (username == "admin1!" && password == "Password1!")// admin username found in database is ok  
        { return true; }
        if (username == "admin1!" && password == "admin1!")// admin username found in database is ok  
        { return true; }
        return false;

    }

    public static void NewDish(string dishName, double dishPrice, string dishType)
    {

    }

    public static void NewDish()
    {

    }
    public static void AddnewTheme()
    {


    }
}