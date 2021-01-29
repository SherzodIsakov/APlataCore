using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.FileService
{
    public interface IMachinesService
    {
        void Add(Machines category);

        void Update(Machines category);

        void Delete(int id);

        List<Machines> GetAll();

        Machines Get(int id);
    }

    public class MachinesService : ServiceBase, IMachinesService
    {
        private const string FilePath = @"\bin\Data\Machines.txt";

        private List<Machines> _categories;

        public MachinesService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Machines>();
        }

        public void Add(Machines category)
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

        public Machines Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Machines> GetAll()
        {
            return _categories;
        }

        public void Update(Machines category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Machines> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Machines>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
