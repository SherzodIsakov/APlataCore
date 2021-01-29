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
    public interface ISessionsService
    {
        void Add(Sessions category);

        void Update(Sessions category);

        void Delete(int id);

        List<Sessions> GetAll();

        Sessions Get(int id);
    }

    public class SessionsService : ServiceBase, ISessionsService
    {
        private const string FilePath = @"\bin\Data\Sessions.txt";

        private List<Sessions> _categories;

        public SessionsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Sessions>();
        }

        public void Add(Sessions category)
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

        public Sessions Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Sessions> GetAll()
        {
            return _categories;
        }

        public void Update(Sessions category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Sessions> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Sessions>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
