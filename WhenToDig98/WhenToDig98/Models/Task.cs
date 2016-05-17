
using SQLite;
using System;

namespace WhenToDig98.Models
{
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string Plant { get; set; }
        public long Timestamp { get; set; }
        public string Day {
            get
            {
                return Convert.ToString(Date.Day.ToString("D2"));
            }
        }
        public string TaskTypeImage
        {
            get
            {
                switch(Type)
                {
                    case 2:
                        return "sow.png";
                   
                    case 4:
                        return "harvest.png";
                      
                    default:
                        return "cultivate.png";
                }             
            }
        }

        public Task()
        { }
    }
}
