using System;

namespace AddsToTen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Sample Output");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 99; i > 0; i--) //loop decending
            {
                int firstDigit = i / 10;
                int secondDigit = i % 10;


                if ((i > 18) && (i < 92) && (firstDigit + secondDigit == 10))
                {
                     
                    if(i == 82)
                    {
                        Console.Write("Match found for " + i + ": (" + firstDigit + "," + secondDigit + ")");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                     else if (i == 64)
                    {
                        Console.Write("Match found for " + i + ": (" + firstDigit + "," + secondDigit + ")");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    Console.Write("Match found for " + i + ": (" + firstDigit + "," + secondDigit + ")");
                    Console.WriteLine();
                }
               
            }

            Console.WriteLine();
            Console.Write("No match found for:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 99; i > 0; i--) //loop decending
            {

                int firstDigit = i / 10;
                int secondDigit = i % 10;

                if (!((i > 18) && (i < 92) && (firstDigit + secondDigit == 10)))
                {
                    Console.Write(i+",");
                }

            }
            Console.Write("\b \b");


            Console.ReadKey();
        }
    }
}
