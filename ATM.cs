using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static HomeworkATM.Card;

namespace HomeworkATM
{
    public class ATM
    {
        public long Id { get; init; }
        public string Owner_Bank { get; init; }
        private Dictionary<int, int> Accepted_Money { get; set; }
        private List<string> History { get; set; }
        private string Secret_Key { get; init; }
        public int CashAmount => Accepted_Money.Values.Sum();
        private int[] validMoney = { 10, 50, 100, 200, 500, 1000, 2000, 5000 };

        public ATM(string bankName, string secretKey)
        {
            Random rand = new Random();
            Id = (long)(rand.NextDouble() * 1_000_000_000);
            Owner_Bank = bankName;
            Secret_Key = secretKey;
            Accepted_Money = new Dictionary<int, int>();
            History = new List<string>();
        }

        public override string ToString()
        {
            return $"ID банкомата: {Id}\n" +
                $"Банк, которому принадлежит банкомат: {Owner_Bank}\n" +
                $"Кассета с принятыми деньгами: {Accepted_Money}\n" +
                $"История транзакций: {History}\n" +
                $"Секретный ключ для валидации инкассаторов: {Secret_Key}";
        }

        private void Log(string message)
        {
            History.Add($"{DateTime.Now::HH:mm:ss} - {message}");
        }

        private bool IsCardValid(Card card)
        {
            if (card.Expire < DateTime.Now)
            {
                string msg = "Card date is expired";
                Log(msg);
                return false;
            }
            return true;
        }

        public void Deposit(Card card, Dictionary<int, int> Income)
        {
            if (!IsCardValid(card)) return;

            double totalDeposit = 0;

            foreach (var item in Income)
            {
                if (!validMoney.Contains(item.Key))
                {
                    string msg = "Not a valid amount type";
                    Log(msg);
                    return;
                }
                totalDeposit += item.Key * item.Value;
                if (Accepted_Money.ContainsKey(item.Key)) Accepted_Money[item.Key] += item.Value;
                else Accepted_Money[item.Key] = item.Value;
            }

            double fee = (card.Bank_Name != Owner_Bank) ? totalDeposit * 0.05 : 0;
            card.Balance += totalDeposit - fee;

            Log($"+ Deposit: {totalDeposit - fee}, Fee: {fee}. Balance: {card.Balance}");
            if (fee > 0) Console.WriteLine($"Deposit fee: {fee}");
        }

        public void Withdraw(Card card, double amount)
        {
            if (!IsCardValid(card)) return;

            double fee = (card.Bank_Name != Owner_Bank) ? amount * 0.05 : 0;
            double totalToDebit = amount + fee;

            if (card.Balance < totalToDebit)
            {
                string msg = "Insufficient amount in balance";
                Log(msg);
                Console.WriteLine(msg);
                return;
            }

            card.Balance -= totalToDebit;
            Log($"- Withdrawal: {amount}, Fee: {fee}. Balance: {card.Balance}");
            Console.WriteLine($"Withdrew: {amount}");
        }

        public void Pickup(string encryptedKey)
        {
            string decryptedKey = CaesarCipher(encryptedKey, -4);

            if (decryptedKey != Secret_Key)
            {
                Console.WriteLine("Invalid key");
                return;
            }

            Accepted_Money.Clear();

            Console.WriteLine("Encashment is successful. History:");
            foreach (var record in History) Console.WriteLine(record);

            History.Clear();
        }

        private string CaesarCipher(string input, int shift)
        {
            return new string(input.Select(x => (char)(x + shift)).ToArray());
        }
    }
}
