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
    public interface IMachineSalesService
    {
        void Add(MachineSales category);

        void Update(MachineSales category);

        void Delete(int id);

        List<MachineSales> GetAll();

        MachineSales Get(int id);
    }

    public class MachineSalesService : ServiceBase, IMachineSalesService
    {
        private const string FilePath = @"\bin\Data\MachineSales.txt";

        private List<MachineSales> _categories;

        public MachineSalesService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<MachineSales>();
        }

        public void Add(MachineSales category)
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

        public MachineSales Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<MachineSales> GetAll()
        {
            return _categories;
        }

        public void Update(MachineSales category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<MachineSales> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<MachineSales>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
