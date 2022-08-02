using System;
using System.Collections.Generic;

namespace restaurant
{
    class Program
    {
        bool isOpen = false;
        Room room = new Room();
        List<Visitor> visitors = new List<Visitor>();
        List<int> freeTables = new List<int>();
        static void Main()
        {
            new Program().Control();
        }
        public void Control()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("Открыть ресторан - O");
            Console.WriteLine("Рарегистрировать посетителя - N");
            Console.WriteLine("Свободные столики - F");
            string choice = Console.ReadLine();
            if (choice == "O")
            {
                if (isOpen == false)
                {
                    isOpen = true;
                    StartWorking();
                }
                else Console.WriteLine(">>>Ресторан уже открыт!");
            }
            //else if (choice == "N")
            //{
            //    if (isOpen == true) VisitorRegistration();
            //    else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            //}
            else if (choice == "F")
            {
                if (isOpen == true) FreeTables();
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else Console.WriteLine("!!!Неопознанный ввод");
            Control();
        }
        public void StartWorking()
        {
            Console.WriteLine("Сколько официантов на смене?");
            int amountWaiters = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < amountWaiters; i++)
            {
                room.waiters.Add(new Waiter());
            }
            Console.WriteLine("Сколько столиков в зале?");
            int amountTables = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < amountTables; i++)
            {
                room.tables.Add(new Table(i + 1, 2));
                freeTables[i] = room.tables[i].id;
            }
            Console.WriteLine("Ресторан открыт! Добро пожаловать!");
            Console.WriteLine($"Свободных официантов - {room.waiters.Count}\n Свободных столиков - {room.tables.Count}");
        }
        //public void VisitorRegistration()
        //{
        //    if (FreeTables().Count < 1) Console.WriteLine("Нет свободных столиков!");
        //    else
        //    {
        //        Console.WriteLine("Введите имя клиента");
        //        visitors.Add(new Visitor(Console.ReadLine()));
        //        for (int i = 0; i < room.tables.Count; i++)
        //        {
        //            if (room.tables[i].CheckTableIsBusy() == false)
        //            {
        //                Console.WriteLine($"Столик номер {room.tables[i].id} свободен");
        //                //room.tables[i].visitor = visitors[visitors.Count - 1];
        //                //Console.WriteLine($"Стол {room.tables[i].id} забронирован на имя {room.tables[i].visitor.name}");
        //                break;
        //            }
        //            else Console.WriteLine("Нет свободных столиков!");
        //        }
        //    }
        //}
        public List<int> FreeTables()
        {  
            freeTables.Clear();
            for (int i = 0; i < room.tables.Count; i++)
            {
                if (room.tables[i].CheckTableIsBusy() == false)
                {
                    freeTables.Add(room.tables[i].id);
                }
            }
            return freeTables;
        }
    }





    public class Human
    {
        public string name;
    }
    public class Waiter : Human
    {
        //public Visitor visitor;
    }
    public class Visitor : Human
    {
        public Visitor(string name) { base.name = name; }
    }
    public class Table
    {
        public Table(int id, int places)
        {
            this.id = id;
            this.places = places;
            visitorsAtTable = new List<Visitor>(places);
        }
        public int FreePlaces()
        {
            int amount = 0;
            for (int i = 0; i < places; i++)
            {
                if (visitorsAtTable[i] == null) amount++;
            }
            return amount;
        }
        public bool CheckTableIsBusy()
        {
            if (FreePlaces() <= FreePlaces()) return true;
            else return false;
        }
        public List<Visitor> visitorsAtTable;
        public int id { get; }
        public int places { get; }//
    }
    public class Room
    {
        public List<Table> tables = new List<Table>();
        public List<Waiter> waiters = new List<Waiter>();
    }
    public class Order
    {
        public Table table;
    }
}
