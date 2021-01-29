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
    public interface ICommandsService
    {
        void Add(Commands category);

        void Update(Commands category);

        void Delete(int id);

        List<Commands> GetAll();

        Commands Get(int id);
    }

    public class CommandsService : ServiceBase, ICommandsService
    {
        private const string FilePath = @"\bin\Data\Commands.txt";

        private List<Commands> _categories;

        public CommandsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Commands>();
        }

        public void Add(Commands category)
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

        public Commands Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Commands> GetAll()
        {
            return _categories;
        }

        public void Update(Commands category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Commands> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Commands>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
