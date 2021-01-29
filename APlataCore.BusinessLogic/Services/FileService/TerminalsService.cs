using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.FileService
{
    public interface ITerminalsService
    {
        void Add(Terminals category);

        void Update(Terminals category);

        void Delete(int id);

        List<Terminals> GetAll();

        Terminals Get(int id);
    }

    public class TerminalsService : ServiceBase, ITerminalsService
    {
        private const string FilePath = @"\bin\Data\Terminals.txt";

        private List<Terminals> _categories;

        public TerminalsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Terminals>();
        }

        public void Add(Terminals category)
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

        public Terminals Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Terminals> GetAll()
        {
            return _categories;
        }

        public void Update(Terminals category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Terminals> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Terminals>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
