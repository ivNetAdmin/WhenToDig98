﻿
using SQLite;

namespace WhenToDig98.Models
{
    public class Plant
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Variety { get; set; }
        public string Type { get; set; }
        public string PlantingNotes { get; set; }
        public string HarvestingNotes { get; set; }

        public Plant()
        { }
    }
}
