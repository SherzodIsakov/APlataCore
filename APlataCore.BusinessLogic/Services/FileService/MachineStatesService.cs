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
    public interface IMachineStatesService
    {
        void Add(MachineStates category);

        void Update(MachineStates category);

        void Delete(int id);

        List<MachineStates> GetAll();

        MachineStates Get(int id);
    }

    public class MachineStatesService : ServiceBase, IMachineStatesService
    {
        private const string FilePath = @"\bin\Data\MachineStates.txt";

        private List<MachineStates> _categories;

        public MachineStatesService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<MachineStates>();
        }

        public void Add(MachineStates category)
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

        public MachineStates Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<MachineStates> GetAll()
        {
            return _categories;
        }

        public void Update(MachineStates category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<MachineStates> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<MachineStates>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
