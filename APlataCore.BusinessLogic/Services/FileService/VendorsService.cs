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
    public interface IVendorsService
    {
        void Add(Vendors category);

        void Update(Vendors category);

        void Delete(int id);

        List<Vendors> GetAll();

        Vendors Get(int id);
    }

    public class VendorsService : ServiceBase, IVendorsService
    {
        private const string FilePath = @"\bin\Data\Vendors.txt";

        private List<Vendors> _categories;

        public VendorsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Vendors>();
        }

        public void Add(Vendors category)
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

        public Vendors Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Vendors> GetAll()
        {
            return _categories;
        }

        public void Update(Vendors category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Vendors> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Vendors>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
