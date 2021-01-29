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
    public interface IItemsService
    {
        void Add(Items category);

        void Update(Items category);

        void Delete(int id);

        List<Items> GetAll();

        Items Get(int id);
    }

    public class ItemsService : ServiceBase, IItemsService
    {
        private const string FilePath = @"\bin\Data\Items.txt";

        private List<Items> _categories;

        public ItemsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Items>();
        }

        public void Add(Items category)
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

        public Items Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Items> GetAll()
        {
            return _categories;
        }

        public void Update(Items category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Items> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Items>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
