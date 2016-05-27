using SQLite;
using System;

namespace WhenToDig98.Entities
{
    public class Frost
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }      
        public DateTime Date { get; set; }
    }
}
