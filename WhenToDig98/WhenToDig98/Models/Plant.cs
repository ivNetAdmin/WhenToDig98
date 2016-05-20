
using SQLite;

namespace WhenToDig98.Models
{
    public class Plant
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PlantingTime { get; set; }
        public string HarvestingTime { get; set; }   

        public Plant()
        { }
        
        public string GetPlantDisplayName()
        {
           return string.Format ("{0}{1}",this.Name,this.Type==null?string.Empty:string.Format(" ({0})",this.Type));
        }
    }
}
