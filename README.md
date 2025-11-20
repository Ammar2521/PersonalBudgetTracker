# Personal Budget Tracker

## Beskrivning
Ett enkelt konsolprogram för att hålla koll på inkomster och utgifter. Bygger på objektorientering (Transaction, BudgetManager) och använder en lista (`List<Transaction>`) för att spara poster i minnet.

## Funktioner
- Lägg till inkomster och utgifter.
- Visa alla transaktioner.
- Visa total balans.
- Ta bort transaktioner.
- (Bonus) Visa per kategori och grundläggande statistik.
- Färg i konsolen: grönt för inkomst, rött för utgift.



 # Klassdiagram 
 
┌────────────────────────────────────────┐
│              Transaction               │
├────────────────────────────────────────┤
│ - Description : string                 │
│ - Amount      : decimal                │
│ - Category    : string                 │
│ - Date        : string                 │
├────────────────────────────────────────┤
│ + ShowInfo() : void                    │
└────────────────────────────────────────┘


                 ( * )
      ┌───────────────────────────┐
      │   BudgetManager           │
      └───────────────────────────┘
                 ▲
                 │  "har många"
                 │
                 │ 1
┌────────────────────────────────────────┐
│              BudgetManager             │
├────────────────────────────────────────┤
│ - transactions : List<Transaction>     │
├────────────────────────────────────────┤
│ + AddTransaction(t : Transaction) : void│
│ + ShowAll() : void                      │
│ + CalculateBalance() : decimal          │
│ + DeleteTransaction(index : int) : bool │
│ + ShowByCategory(cat : string) : void   │
└────────────────────────────────────────┘




# Flödesschema
                ┌──────────┐
                │   Start  │
                └─────┬────┘
                      ↓
            ┌──────────────────┐
            │   Visa meny      │
            └───────┬──────────┘
                    ↓
            ┌──────────────────┐
            │ Läs användarens  │
            │       val        │
            └───────┬──────────┘
                    ↓
        ┌─────────────────────────────┐
        │     Är valet 1–5 (eller 6)? │
        └──────────┬──────────────────┘
                   ↓

    ┌───────────────────────────────────────────┐
    │ 1 – Lägg till transaktion                 │
    │   → Fråga användaren om:                  │
    │        • beskrivning                      │
    │        • belopp                           │
    │        • kategori                         │
    │        • datum                            │
    │   → Skapa transaktion & lägg till listan  │
    └───────────────────────────────────────────┘
                   ↓
                 (Tillbaka till meny)


    ┌───────────────────────────────────────────┐
    │ 2 – Visa alla transaktioner               │
    │   → Loopar igenom listan                  │
    │   → Anropar ShowInfo() på varje objekt    │
    └───────────────────────────────────────────┘
                   ↓
                 (Tillbaka till meny)


    ┌───────────────────────────────────────────┐
    │ 3 – Visa total balans                     │
    │   → Räkna samman alla belopp              │
    │   → Skriv ut resultatet                   │
    └───────────────────────────────────────────┘
                   ↓
                 (Tillbaka till meny)


    ┌───────────────────────────────────────────┐
    │ 4 – Ta bort transaktion                   │
    │   → Visa alla transaktioner med index     │
    │   → Användaren väljer vilket index        │
    │   → Ta bort den posten                    │
    └───────────────────────────────────────────┘
                   ↓
                 (Tillbaka till meny)


    ┌───────────────────────────────────────────┐
    │ 5 – Visa per kategori                     │
    │   → Fråga efter kategori                  │
    │   → Filtrera listan                       │
    │   → Visa endast matchande poster          │
    └───────────────────────────────────────────┘
                   ↓
                (Tillbaka till meny)


    ┌───────────────────────────────────────────┐
    │ 6 – Avsluta programmet                    │
    │   → Stäng ner                             │
    │   → Skriv “Hej då!”                       │
    └───────────────────────────────────────────┘
                   ↓
                ┌──────────┐
                │   Slut   │
                └──────────┘

