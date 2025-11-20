using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace PersonalBudgetTracker
{
    // Ansvar: hålla reda på alla transaktioner och erbjuda funktionalitet
    public class BudgetManager
    {
        // Lista som håller alla Transaction-objekt i minnet
        private List<Transaction> transactions = new List<Transaction>();

        // Lägger till en transaktion
        public void AddTransaction(Transaction t)
        {
            transactions.Add(t);
            // Lärarkommentar: Bra att ha en bekräftelse när posten sparats
            Console.WriteLine("Transaktionen lades till.");
        }

        // Visar alla transaktioner
        public void ShowAll()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }

            for (int i = 0; i < transactions.Count; i++)
            {
                transactions[i].ShowInfo(i);
            }
        }

        // Räknar ut total balans (summa av alla belopp)
        public decimal CalculateBalance()
        {
            decimal balance = 0m;
            foreach (var t in transactions)
            {
                balance += t.Amount;
            }
            return balance;
        }

        // Tar bort transaktion med angivet index. Returnerar true om lyckades.
        public bool DeleteTransaction(int index)
        {
            if (index < 0 || index >= transactions.Count)
            {
                return false;
            }
            transactions.RemoveAt(index);
            return true;
        }

        // Bonus: visa transaktioner per kategori
        public void ShowByCategory(string category)
        {
            var filtered = transactions.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            if (filtered.Count == 0)
            {
                Console.WriteLine($"Inga transaktioner i kategorin '{category}'.");
                return;
            }
            foreach (var t in filtered)
            {
                t.ShowInfo();
            }
        }

        // Bonus: statistik (antal, total inkomst, total utgift)
        public void ShowStatistics()
        {
            int count = transactions.Count;
            decimal totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            decimal totalExpense = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
            Console.WriteLine($"Antal transaktioner: {count}");
            Console.WriteLine($"Total inkomst: {totalIncome:F2}");
            Console.WriteLine($"Total utgift: {totalExpense:F2}");
        }
    }
}





