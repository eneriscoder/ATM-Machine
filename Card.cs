using System;
using System.Collections.Generic;
using System.Text;

namespace HomeworkATM
{
    public class Card
    {   
        public string Id { get; init; }
        public string Owner_Name { get; init; }
        public DateTime Expire { get; init; }
        public string Bank_Name { get; init; }
        private double _balance;
        public double Balance
        {
            get => _balance;
            set { if (value >= 0) _balance = value; }
        }

        public Card(string id, string owner_name, DateTime expire, string bank_name, double balance)
        {
            this.Id = id;
            this.Owner_Name = owner_name;
            this.Expire = expire;
            this.Bank_Name = bank_name;
            this.Balance = balance;

        }

        public override string ToString()
        {
            return $"Номер карты: {Id}\n" +
                $"Имя владельца: {Owner_Name}\n" +
                $"Месяц и год окончания действия карты: {Expire}\n" +
                $"Банк-эмитент карты: {Bank_Name}\n" +
                $"Сумма денег на счету: {Balance}";
        }
    }
}
