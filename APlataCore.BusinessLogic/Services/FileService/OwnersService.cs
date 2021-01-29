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
    public interface IOwnersService
    {
        void Add(Owners category);

        void Update(Owners category);

        void Delete(int id);

        List<Owners> GetAll();

        Owners Get(int id);
    }

    public class OwnersService : ServiceBase, IOwnersService
    {
        private const string FilePath = @"\bin\Data\Owners.txt";

        private List<Owners> _categories;

        public OwnersService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Owners>();
        }

        public void Add(Owners category)
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

        public Owners Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Owners> GetAll()
        {
            return _categories;
        }

        public void Update(Owners category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Owners> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Owners>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
