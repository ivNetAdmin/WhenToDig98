
using SQLite;

namespace WhenToDig98.Entities
{
    public class Plant
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PlantingTime { get; set; }
        public string HarvestingTime { get; set; }   

        public string PlantDisplayName
        {
            get {
                return string.Format("{0}{1}", this.Name, string.IsNullOrEmpty(this.Type) ? string.Empty : string.Format(" ({0})", this.Type));
            }
        }
        
        public Plant()
        { }
    }
}
