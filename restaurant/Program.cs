﻿using System;
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
            Console.WriteLine("Свободные места - PF");
            Console.WriteLine("Свободные столики - TF");
            Console.WriteLine("Добавить столики - A");
            Console.WriteLine("Всего столиков в зале - T");
            Console.WriteLine("Информация по все столикам - I");
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
            else if (choice == "PF")
            {
                if (isOpen == true) Console.WriteLine($"Свободных мест осталось: {FreePlacesCountAll(room)}");
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else if (choice == "A")
            {
                if (isOpen == true) AddTables();
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else if (choice == "T")
            {
                if (isOpen == true) Console.WriteLine("Всего столиков в зале: " + TablesCountAll(room));
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else if (choice == "TF")
            {
                if (isOpen == true) TablesWithFreePlaces(room);
                else Console.WriteLine(">>>Сначала нужно открыть ресторан!");
            }
            else if (choice == "I")
            {
                if (isOpen == true) AllTablesInfo(room);
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
            AddTables();
            Console.WriteLine("Ресторан открыт! Добро пожаловать!");
            Console.WriteLine($"Свободных официантов - {room.waiters.Count}\nСвободных столиков - {room.tables.Count}");
        }
        public void VisitorRegistration()
        {
            if (FreeTablesCount(room).Count < 1) Console.WriteLine("Нет свободных столиков!");
            else
            {
                Console.WriteLine("Введите имя клиента");
                visitors.Add(new Visitor(Console.ReadLine()));
                Console.WriteLine($"Укажите номер столика за который посадить {visitors[visitors.Count - 1].name}?");
                TablesWithFreePlaces(room);
                Console.WriteLine("Выберите столик:");
            again: int choise = int.Parse(Console.ReadLine());
                for (int i = 0; i < FreeTablesCount(room).Count; i++)
                {
                    if (FreeTablesCount(room)[i].id == choise)
                    { FreeTablesCount(room)[i].visitorsAtTable.Add(visitors[visitors.Count - 1]); break; }
                    else if (i == FreeTablesCount(room).Count - 1) { Console.WriteLine("Столика с таким номером нет, либо за ним нет свободных мест! Повторите ввод:"); goto again; }
                }
                Console.WriteLine($"Посититель {visitors[visitors.Count - 1].name} - посажен за столик №{choise}!");
            }
        }
        public void AddTables()
        {
            Console.WriteLine("Сколько столиков добавить?");
            int amountTables = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Сколько мест за каждым столиком?");
            int amountPlaces = Int32.Parse(Console.ReadLine());
            for (int i = 1; i <= amountTables; i++)
            {
                if (room.tables.Count == 0) room.tables.Add(new Table(i, amountPlaces));
                else room.tables.Add(new Table(room.tables[room.tables.Count - 1].id + 1, amountPlaces));
            }
        }
        public int FreePlacesCountAll(Room room)
        {
            int amount = 0;
            for (int i = 0; i < room.tables.Count; i++)
            {
                amount += room.tables[i].places - room.tables[i].visitorsAtTable.Count;
            }
            return amount;
        }
        public int FreePlacesCount(Table table)
        {
            return table.places - table.visitorsAtTable.Count;
        }
        public bool CheckTableIsBusy(Table table)
        {
            if (FreePlacesCount(table) > 0) return false;
            else return true;
        }
        public int TablesCountAll(Room room)
        {
            int amount = 0;
            for (int i = 0; i < room.tables.Count; i++)
            {
                amount++;
            }
            return amount;
        }
        public List<Table> FreeTablesCount(Room room)
        {
            List<Table> tables = new List<Table>();
            for (int i = 0; i < room.tables.Count; i++)
            {
                if (CheckTableIsBusy(room.tables[i]) == false) tables.Add(room.tables[i]);
            }
            return tables;
        }
        public void TablesWithFreePlaces(Room room)
        {
            Console.WriteLine($"Номера столиков с незанятыми местами:->");
            for (int i = 0; i < FreeTablesCount(room).Count; i++)
            {
                Console.WriteLine($"|№{FreeTablesCount(room)[i].id}(свободных мест:{FreePlacesCount(FreeTablesCount(room)[i])}) | ");
            }
        }
        public void AllTablesInfo(Room room)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
            for (int i = 0; i < room.tables.Count; i++)
            {
                int n = 1;
                Console.WriteLine($"|Столик №{room.tables[i].id} | ");
                foreach (Visitor item in room.tables[i].visitorsAtTable)
                {
                    if (item.name == null) Console.WriteLine($"место №{n++} Пусто");
                    else Console.WriteLine($"место №{i + 1} {item.name}");
                }
            }
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
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
        public List<Visitor> visitorsAtTable;
        public int id { get; set; }
        public int places { get; }
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
