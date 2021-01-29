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
    public interface IBpcHostsService
    {
        void Add(BpcHosts category);

        void Update(BpcHosts category);

        void Delete(int id);

        List<BpcHosts> GetAll();

        BpcHosts Get(int id);
    }

    public class BpcHostsService : ServiceBase, IBpcHostsService
    {
        private const string FilePath = @"\bin\Data\BpcHosts.txt";

        private List<BpcHosts> _categories;

        public BpcHostsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<BpcHosts>();
        }

        public void Add(BpcHosts category)
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

        public BpcHosts Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<BpcHosts> GetAll()
        {
            return _categories;
        }

        public void Update(BpcHosts category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<BpcHosts> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<BpcHosts>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
