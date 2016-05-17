
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void ResetTasks()
        {
            _connection.DeleteAll<Task>();
        }

        internal IEnumerable<Task> GetTasksByMonth(DateTime _currentCallendarDate)
        {

            var startDate = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1);
            var endDate = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1).AddMonths(1);

            var tasks = _connection.Query<Task>("SELECT * FROM Task WHERE Date > ? AND Date < ?", 
                new DateTime(startDate.Year, startDate.Month, startDate.Day).Ticks,
                new DateTime(endDate.Year, endDate.Month, endDate.Day).Ticks);

            return tasks;

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

        internal void AddPlant(int id, string name, string plantType, string plantTime, string harvestTime)
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
        }
        #endregion

    }
}
