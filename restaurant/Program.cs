using System;
using System.Collections.Generic;

namespace restaurant
{
    class Program
    {
        bool isOpen = false;
        Room room = new Room();
        List<Visitor> visitors = new List<Visitor>();
        static void Main()
        {
            new Program().Control();
        }
        public void Control()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("Открыть ресторан - O");
            Console.WriteLine("Рарегистрировать посетителя - N");
            Console.WriteLine("Свободные места - F");
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
            else if (choice == "N")
            {
                if (isOpen == true) VisitorRegistration();
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else if (choice == "F")
            {
                if (isOpen == true) Console.WriteLine($"Свободных мест осталось: {FreePlacesCountAll()}");
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
            }
            Console.WriteLine("Ресторан открыт! Добро пожаловать!");
            Console.WriteLine($"Свободных официантов - {room.waiters.Count}\nСвободных столиков - {room.tables.Count}");
        }
        public void VisitorRegistration()
        {
            if (FreeTablesCountAll(room) < 1) Console.WriteLine("Нет свободных столиков!");
            else
            {
                Console.WriteLine("Введите имя клиента");
                visitors.Add(new Visitor(Console.ReadLine()));
                Console.WriteLine($"Укажите номер столика за который посадить {visitors[visitors.Count-1].name}?");
                Console.WriteLine($"Свободные столики:->" );
                for (int i = 0; i < FreeTables(room).Count; i++)//??????????????????????????????
                {
                    Console.Write($"|{i+1} - {FreeTables(room)[i].id}| ");
                }
                int choise = Int32.Parse(Console.ReadLine());
                room.tables[choise-1].visitorsAtTable.Add(visitors[visitors.Count-1]);
                Console.WriteLine($"Посититель {visitors[visitors.Count-1].name} - посажен за столик {FreeTables(room)[choise-1].id}!");
            }
        }
        public int FreePlacesCountAll()
        {
            int amount = 0;
            for (int i = 0; i < room.tables.Count; i++)
            {
                for (int j = 1; j < room.tables[i].places; j++)
                {
                    amount += room.tables[i].places - room.tables[i].visitorsAtTable.Count;
                }
            }
            return amount;
        }
        public int FreePlacesAtTable(Table table)
        {
            int amount = 0;
            for (int i = 0; i < table.places; i++)
            {
                if (table.visitorsAtTable.Equals(null)) amount++;
            }
            return amount;
        }
        public bool CheckTableIsBusy(Table table)
        {
            if (FreePlacesAtTable(table) < table.places) return false;
            else return true;
        }
        public int FreeTablesCountAll(Room room)
        {
            int amount = 0;
            for (int i = 0; i < room.tables.Count; i++)
            {
                if (CheckTableIsBusy(room.tables[i]) == false) amount++;
            }
            return amount;
        }
        public List<Table> FreeTables(Room room)
        {
            List<Table> tables = new List<Table>();
            for (int i = 0; i < room.tables.Count; i++)
            {
                if (CheckTableIsBusy(room.tables[i]) == false) tables.Add(room.tables[i]);
            }
            return tables;
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
        public Table() { }
        public Table(int id, int places)
        {
            this.id = id;
            this.places = places;
            visitorsAtTable = new List<Visitor>(places);
        }
        public List<Visitor> visitorsAtTable;
        public int id { get; }
        public int places { get;}
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
