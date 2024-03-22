using System;
using SQLite;

namespace productDemo.Data.Todo.implementation.local
{
    public class Todo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}