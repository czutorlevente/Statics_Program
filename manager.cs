using System;

public class Manager
{
    public static void menu()
    {
        bool exit = false;
        string unit_1 = "#";
        string unit_2 = "feet";
        while (exit == false)
        {
            Console.WriteLine("Choose from the following menu options:\n\n1 - Support reactions\n2 - \n3 - Change default units of measurement\n4 - Exit\n");
            Console.Write("Your choice: ");
            int response = int.Parse(Console.ReadLine());

            if (response == 1)
            {
                Calculator.SupportReactions(unit_1,unit_2); 
            }

            else if (response == 3)
            {
                Console.Write("Unit of measurement for force: ");
                unit_1= Console.ReadLine();
                Console.Write("Unit of measurement for distance: ");
                unit_2= Console.ReadLine();
            }

            else if (response == 4)
            {
                exit = true;
            }

        }
    }
}