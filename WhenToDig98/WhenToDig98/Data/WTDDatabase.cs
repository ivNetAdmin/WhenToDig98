
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WhenToDig98.Helpers;
using WhenToDig98.Models;

namespace WhenToDig98.Data
{
    public class WTDDatabase
    {
        private SQLiteConnection _connection;

        public WTDDatabase()
        {
            _connection = Xamarin.Forms.DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Task>();
            _connection.CreateTable<Plant>();
            _connection.CreateTable<Variety>();
        }


        public void ResetDb()
        {
            _connection.DeleteAll<Variety>();
            _connection.DeleteAll<Plant>();
            _connection.DeleteAll<Task>();
        }


        #region tasks
        public IEnumerable<Task> GetTasks()
        {
            return (from t in _connection.Table<Task>() select t).ToList();
        }
       
        public Task GetTask(int id) {
            return _connection.Table<Task>().FirstOrDefault(t => t.ID == id);
        }

        public void DeleteTask(int id) {
            _connection.Delete<Task>(id);
        }

        public void AddTask(int id, string description, string notes, int type, DateTime date, string plant ) {
            var newTask = new Task { Description = description, Notes = notes, Type = type, Date = date, Plant = plant, Timestamp = DateTime.Now.Ticks };

            if (id == 0)
            {
                _connection.Insert(newTask);
            }
            else
            {
                newTask.ID = id;
                _connection.Update(newTask);
            }
        }

        public IEnumerable<Task> GetTasksByMonth(DateTime _currentCallendarDate)
        {

            var startDate = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1);
            var endDate = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1).AddMonths(1);

            var tasks = _connection.Query<Task>("SELECT * FROM Task WHERE Date > ? AND Date < ?", 
                new DateTime(startDate.Year, startDate.Month, startDate.Day).Ticks,
                new DateTime(endDate.Year, endDate.Month, endDate.Day).Ticks);

            return tasks;

        }

        public IEnumerable<string> GetYears()
        {
            var yearList = new List<string>();
            var task = _connection.Query<Task>("SELECT * FROM Task ORDER BY Date DESC").FirstOrDefault();

            if (task != null)
            {
                for (var i = task.Date.Year; i <= DateTime.Now.Year; i++)
                {
                    yearList.Add(i.ToString());
                }
            }else
            {
                yearList.Add(DateTime.Now.Year.ToString());
            }
            return yearList;
        }

        public IEnumerable<string> GetMonths()
        {
            var monthList = new List<string>();         
            var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        
            for (var i = 0; i <12; i++)
            {
                monthList.Add(months[i]);
            }
            return monthList;
        }

        public IEnumerable<Task> GetTasks(string season, int month, string taskType, string plant, string task)
        {
           // if (season == "All Years") season = null;
           // if (taskType == "All") taskType = null;
           // if (plant == "All") plant = null;

            int startMonth = month;
            int endMonth = startMonth;
            var sql = new StringBuilder("SELECT * FROM Task");
            var criteriaCount = 0;

            if (month > 0 && string.IsNullOrEmpty(season))
            {
                season = Convert.ToString(DateTime.Now.Year);
            }

            if (!string.IsNullOrEmpty(season) && month < 1)
            {
                startMonth = 1;
                endMonth = 12;
            }
          
            if(!string.IsNullOrEmpty(season))
            {
                var startDate = new DateTime(Convert.ToInt32(season), startMonth, 1);
                var endDate = new DateTime(Convert.ToInt32(season), endMonth, new DateTime(Convert.ToInt32(season), endMonth, 1).AddMonths(1).AddDays(-1).Day);
                sql.Append(GetOperator(criteriaCount));
                sql.Append(string.Format("Date > {0} AND Date < {1}", startDate.Ticks, endDate.Ticks));
                criteriaCount++;
            }

            if (!string.IsNullOrEmpty(taskType))
            {
                var taskTypeId = 1;
                if (taskType == "Sow") taskTypeId = 2;
                if (taskType == "Harvest") taskTypeId = 4;

                sql.Append(GetOperator(criteriaCount));
                sql.Append(string.Format("Type = {0}", taskTypeId));
                criteriaCount++;
            }

            if(!string.IsNullOrEmpty(plant))
            {
                sql.Append(GetOperator(criteriaCount));
                sql.Append(string.Format("Plant = '{0}'", plant));
                criteriaCount++;
            }
            
            if(!string.IsNullOrEmpty(task))
            {
                sql.Append(GetOperator(criteriaCount));
                sql.Append(string.Format("Description LIKE '%{0}%'", task));
                criteriaCount++;
            }

            return _connection.Query<Task>(sql.ToString());
        }

        #endregion

        #region taskTypes
        public string[] GetTaskTypes()
        {
            return new[] { "Cultivate", "Sow", "Harvest" };
        }
        #endregion

        #region plants
        public IEnumerable<Plant> GetPlants()
        {
            return (from p in _connection.Table<Plant>() select p).ToList();
        }   
     
        public Plant GetPlant(int id) {
            return _connection.Table<Plant>().FirstOrDefault(t => t.ID == id);
        }

        public int AddPlant(int id, string name, string plantType, string plantTime, string harvestTime)
        {
            var newPlant = new Plant
            {
                Name = name,
                Type = plantType,
                PlantingTime = plantTime,
                HarvestingTime = harvestTime
            };

            if (id == 0)
            {
                _connection.Insert(newPlant);
            }
            else
            {
                newPlant.ID = id;
                _connection.Update(newPlant);
            }

            return newPlant.ID;
        }
        #endregion

        #region variety
        public Variety GetVariety(int id)
        {
            return _connection.Table<Variety>().FirstOrDefault(t => t.ID == id);
        }
        
        public IEnumerable<Variety> GetVarieties(int id)
        {
            return (from t in _connection.Table<Variety>() select t).ToList();
        }

        public ObservableCollection<Variety> GetPlantVarieties(int plantId)
        {
            var varieties = (from p in _connection.Table<Variety>() select p).ToList();
            return new List<Variety>().ToObservableCollection();
        }

        public void AddVariety(int id, string name, string plantingNotes, string harvestingNotes, int plantId)
        {
            if (name == "New") return;
            var newVariety = new Variety
            {
                Name = name,
                PlantingNotes = plantingNotes,
                HarvestingNotes= harvestingNotes,
                PlantID = plantId
            };

            if (id == 0)
            {
                _connection.Insert(newVariety);
            }
            else
            {
                newVariety.ID = id;
                _connection.Update(newVariety);
            }
        }
        #endregion

        private string GetOperator(int criteriaCount)
        {
            if(criteriaCount==0)
            {
                return " WHERE ";
            }else{
                return " AND ";
            }
        }
    }
}
