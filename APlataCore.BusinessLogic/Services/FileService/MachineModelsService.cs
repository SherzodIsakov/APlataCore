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
    public interface IMachineModelsService
    {
        void Add(MachineModels category);

        void Update(MachineModels category);

        void Delete(int id);

        List<MachineModels> GetAll();

        MachineModels Get(int id);
    }

    public class MachineModelsService : ServiceBase, IMachineModelsService
    {
        private const string FilePath = @"\bin\Data\MachineModels.txt";

        private List<MachineModels> _categories;

        public MachineModelsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<MachineModels>();
        }

        public void Add(MachineModels category)
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

        public MachineModels Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<MachineModels> GetAll()
        {
            return _categories;
        }

        public void Update(MachineModels category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<MachineModels> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<MachineModels>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
