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
    public interface IEventsService
    {
        void Add(Events category);

        void Update(Events category);

        void Delete(int id);

        List<Events> GetAll();

        Events Get(int id);
    }

    public class EventsService : ServiceBase, IEventsService
    {
        private const string FilePath = @"\bin\Data\Events.txt";

        private List<Events> _categories;

        public EventsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Events>();
        }

        public void Add(Events category)
        {
            int id = GetMaxId(_categories.OfType<IBaseId>().ToList());

            category.Id = id + 1;

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

        public Events Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Events> GetAll()
        {
            return _categories;
        }

        public void Update(Events category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Events> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Events>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
