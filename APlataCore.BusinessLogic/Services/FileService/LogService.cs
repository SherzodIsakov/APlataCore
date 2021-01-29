using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.FileService
{
    public interface ILogService
    {
        void Add(Log category);

        void Update(Log category);

        void Delete(int id);

        List<Log> GetAll();

        Log Get(int id);
    }

    public class LogService : ServiceBase, ILogService
    {
        private const string FilePath = @"\bin\Data\Log.txt";

        private List<Log> _categories;

        public LogService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Log>();
        }

        public void Add(Log category)
        {
            int id = GetMaxId(_categories.OfType<IBaseId>().ToList());

            category.SessionID = id + 1;

            _categories.Add(category);

            SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Get(id);

            if (category != null)
            {
                _categories.Remove(category);
            }

            SaveChanges();
        }

        public Log Get(int id)
        {
            return _categories.FirstOrDefault(x => x.SessionID == id);
        }

        public List<Log> GetAll()
        {
            return _categories;
        }

        public void Update(Log category)
        {
            var oldCategory = Get(category.SessionID);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Log> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Log>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
