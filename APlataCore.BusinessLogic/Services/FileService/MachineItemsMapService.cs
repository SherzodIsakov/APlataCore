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
    public interface IMachineItemsMapService
    {
        void Add(MachineItemsMap category);

        void Update(MachineItemsMap category);

        void Delete(int id);

        List<MachineItemsMap> GetAll();

        MachineItemsMap Get(int id);
    }

    public class MachineItemsMapService : ServiceBase, IMachineItemsMapService
    {
        private const string FilePath = @"\bin\Data\MachineItemsMap.txt";

        private List<MachineItemsMap> _categories;

        public MachineItemsMapService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<MachineItemsMap>();
        }

        public void Add(MachineItemsMap category)
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

        public MachineItemsMap Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<MachineItemsMap> GetAll()
        {
            return _categories;
        }

        public void Update(MachineItemsMap category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<MachineItemsMap> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<MachineItemsMap>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
