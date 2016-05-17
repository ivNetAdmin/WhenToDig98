
using SQLite;

namespace WhenToDig98.Models
{
    public class Variety
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PlantingNotes { get; set; }
        public string HarvestingNotes { get; set; }
    }
}
