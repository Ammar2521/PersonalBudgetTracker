using System;
using System.Globalization;
using System.Transactions;

namespace PersonalBudgetTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            BudgetManager manager = new BudgetManager();

            // En enkel meny-loop
            bool running = true;
            while (running)
            {
                ShowMenu();
                Console.Write("Välj ett alternativ (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransactionFlow(manager);
                        break;
                    case "2":
                        manager.ShowAll();
                        break;
                    case "3":
                        decimal balance = manager.CalculateBalance();
                        Console.Write("Aktuell balans: ");
                        if (balance >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{balance:F2}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{balance:F2}");
                        }
                        Console.ResetColor();
                        break;
                    case "4":
                        DeleteTransactionFlow(manager);
                        break;
                    case "5":
                        // Bonus: Visa per kategori
                        Console.Write("Ange kategori att filtrera (eller tryck Enter för att hoppa över): ");
                        string cat = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(cat))
                        {
                            manager.ShowByCategory(cat.Trim());
                        }
                        else
                        {
                            Console.WriteLine("Ingen kategori angavs.");
                        }
                        break;
                    case "6":
                        // Statistik (bonus)
                        manager.ShowStatistics();
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Avslutar programmet. Hej då!");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Välj 1-7.");
                        break;
                }

                Console.WriteLine(); // tom rad för läsbarhet
            }
        }

        // Visar menyval
        static void ShowMenu()
        {
            Console.WriteLine("=== Personal Budget Tracker ===");
            Console.WriteLine("1)  Lägg till transaktion");
            Console.WriteLine("2)  Visa alla transaktioner");
            Console.WriteLine("3)  Visa total balans");
            Console.WriteLine("4)  Ta bort transaktion");
            Console.WriteLine("5)  Visa transaktioner per kategori (bonus)");
            Console.WriteLine("6)  Statistik (bonus)");
            Console.WriteLine("7)  Avsluta programmet");
        }

        // Flöde för att lägga till transaktion — med validering
        static void AddTransactionFlow(BudgetManager manager)
        {
            Console.Write("Beskrivning: ");
            string desc = Console.ReadLine();

            // Validera belopp (decimal)
            decimal amount;
            while (true)
            {
                Console.Write("Belopp (positivt för inkomst, negativt för utgift): ");
                string amtInput = Console.ReadLine();
                if (decimal.TryParse(amtInput, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
                {
                    break;
                }
                Console.WriteLine("Fel: Ange ett giltigt tal. Exempel: 1000 eller -250.50");
            }

            Console.Write("Kategori (t.ex. Mat, Transport, Hyra, Inkomst): ");
            string category = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(category)) category = "Övrigt";

            // Datum som text — vi accepterar enklare format men kan kontrollera
            string date;
            while (true)
            {
                Console.Write("Datum (YYYY-MM-DD), tryck Enter för dagens datum: ");
                string dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    date = DateTime.Today.ToString("yyyy-MM-dd");
                    break;
                }
                // Enkel validering av format
                if (DateTime.TryParse(dateInput, out DateTime parsed))
                {
                    date = parsed.ToString("yyyy-MM-dd");
                    break;
                }
                Console.WriteLine("Fel: Ange ett giltigt datum i formatet YYYY-MM-DD.");
            }

            Transaction t = new Transaction(desc, amount, category, date);
            manager.AddTransaction(t);

            // Lärarkommentar: Bekräfta tydligt vad som sparats
            Console.WriteLine("Läggning genomförd: ");
            t.ShowInfo();
        }

        // Flöde för att ta bort transaktion
        static void DeleteTransactionFlow(BudgetManager manager)
        {
            // Visa alla så användaren kan välja index
            manager.ShowAll();
            Console.Write("Ange index (nummer) på transaktionen att ta bort: ");
            string idxInput = Console.ReadLine();
            if (int.TryParse(idxInput, out int index))
            {
                bool success = manager.DeleteTransaction(index);
                if (success)
                {
                    Console.WriteLine("Transaktionen togs bort.");
                }
                else
                {
                    Console.WriteLine("Misslyckades: ogiltigt index.");
                }
            }
            else
            {
                Console.WriteLine("Fel: Ange ett giltigt heltal som index.");
            }
        }
    }
}

