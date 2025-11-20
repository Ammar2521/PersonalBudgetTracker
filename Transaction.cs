using System;

namespace PersonalBudgetTracker
{
    // Klass som representerar en transaktion (inkomst eller utgift)
    public class Transaction
    {
        // Egenskaper (public så vi kan läsa dem från andra klasser)
        public string Description { get; set; }    // t.ex. "Lön", "Mat"
        public decimal Amount { get; set; }       // positivt = inkomst, negativt = utgift
        public string Category { get; set; }      // t.ex. "Mat", "Transport"
        public string Date { get; set; }          // enkel textrepr. t.ex. "2025-10-10"

        // Konstruktor för att enkelt skapa objekt
        public Transaction(string description, decimal amount, string category, string date)
        {
            Description = description;
            Amount = amount;
            Category = category;
            Date = date;
        }

        // Metod som skriver ut transaktionens information
        public void ShowInfo(int index = -1)
        {
            // index används för att visa nummer i listan (valfritt)
            if (index >= 0)
            {
                Console.Write($"[{index}] ");
            }

            // Skriv ut belopp i grönt för inkomst och rött för utgift (bonus)
            if (Amount >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Amount:F2} ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Amount:F2} ");
            }
            Console.ResetColor();

            // Resten av informationen
            Console.WriteLine($"| {Description} | Kategori: {Category} | Datum: {Date}");
        }
    }
}

