using System;
using System.Runtime.InteropServices;
using static HomeworkATM.ATM;
using static HomeworkATM.Card;

namespace HomeworkATM
{
    class Program
    {
        static void Main()
        {
            Card card1 = new Card(
                "1234567812345678", 
                "Artificer", 
                DateTime.Parse("01/01/2030"), 
                "Rain World", 
                0);

            ATM atm1 = new ATM(
                "Rain World",
                "evilslugcats");

            ATM atm2 = new ATM(
                "Spearmaster's Bank",
                "scambank");

            Console.WriteLine($"Card: {card1.ToString()}");
            Console.WriteLine($"ATM1: {atm1.ToString()}");
            Console.WriteLine($"ATM2: {atm2.ToString()}");

        }
    }
}
