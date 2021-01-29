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
    public interface IMachineStatesLogService
    {
        void Add(MachineStatesLog category);

        void Update(MachineStatesLog category);

        void Delete(int id);

        List<MachineStatesLog> GetAll();

        MachineStatesLog Get(int id);
    }

    public class MachineStatesLogService : ServiceBase, IMachineStatesLogService
    {
        private const string FilePath = @"\bin\Data\MachineStatesLog.txt";

        private List<MachineStatesLog> _categories;

        public MachineStatesLogService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<MachineStatesLog>();
        }

        public void Add(MachineStatesLog category)
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

        public MachineStatesLog Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<MachineStatesLog> GetAll()
        {
            return _categories;
        }

        public void Update(MachineStatesLog category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<MachineStatesLog> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<MachineStatesLog>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
