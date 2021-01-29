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
    public interface IEventTypesService
    {
        void Add(EventTypes category);

        void Update(EventTypes category);

        void Delete(int id);

        List<EventTypes> GetAll();

        EventTypes Get(int id);
    }

    public class EventTypesService : ServiceBase, IEventTypesService
    {
        private const string FilePath = @"\bin\Data\EventTypes.txt";

        private List<EventTypes> _categories;

        public EventTypesService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<EventTypes>();
        }

        public void Add(EventTypes category)
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

        public EventTypes Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<EventTypes> GetAll()
        {
            return _categories;
        }

        public void Update(EventTypes category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<EventTypes> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<EventTypes>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
