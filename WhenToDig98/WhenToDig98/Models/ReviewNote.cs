
namespace WhenToDig98.Models
{
    public class ReviewNote
    {
        public string PlantName { get; set; }
        public string PlantType { get; set; }
        public string Variety { get; set; }
        public string NoteA { get; set; }
        public string NoteB { get; set; }
        
        public string PlantDisplayName
        {
            get {
                return string.Format("{0}{1}{2}", 
                this.PlantName, 
                string.IsNullOrEmpty(this.PlantType) ? string.Empty : string.Format(" ({0})", this.PlantType),
                string.IsNullOrEmpty(this.Variety) ? string.Empty : string.Format(" {0}", this.Variety)
                );
            }
        }
    }
}
