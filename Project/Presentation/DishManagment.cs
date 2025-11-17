using System.Security.Cryptography.X509Certificates;

public static class DishManagment
{


    public static void CreateNewDish(string dishType)
    {

        // ask input
        string dishName;
        string priceInput;
        bool nameValid = false;
        bool priceValid = false;

        do
        {
            Console.Write("Voer de naam van de dish in: ");
            dishName = Console.ReadLine();

            Console.Write("Voer de prijs in: ");
            priceInput = Console.ReadLine();

            nameValid = DishLogic.ValidateDishName(dishName);
            priceValid = DishLogic.ValidateDishPrice(priceInput);

            if (!nameValid || !priceValid)
            {
                Console.WriteLine("Input is ongeldig. Probeer opnieuw.\n");
            }

        } while (!nameValid || !priceValid);

        // Als validatie geslaagd is:
        Console.Write("Confirm or discard new dish? (confirm/discard): ");
        string confirm = Console.ReadLine().Trim().ToLower();

        if (confirm == "confirm")
        {
            priceInput = priceInput.Replace(".", ",");
            double price = double.Parse(priceInput);

            Console.WriteLine($"New dish has been added. price = {price:F2}");
            // put it in the databsae

        }
        else if (confirm == "discard")
        {
            Console.WriteLine("Dish discarded.");
        }
        else
        {
            Console.WriteLine("Ongeldige keuze. Programma beëindigd.");
        }
    }


    public static bool ChoiceDishType(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("Appitizer");
                CreateNewDish("Appitizer");
                break;
            case 1:
                Console.WriteLine("Main Course");
                CreateNewDish("Main Course");
                break;
            case 2:
                Console.WriteLine("Dessert");
                CreateNewDish("Dessert");
                break;
            case 3:
                return false; // go back
        }
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
        return true;
    }






    public static void PrintAlldishes(List<DishModel> Alldishes){

        


    }


    }


        



