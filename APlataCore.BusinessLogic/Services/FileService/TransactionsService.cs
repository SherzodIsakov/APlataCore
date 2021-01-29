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
    public interface ITransactionsService
    {
        void Add(Transactions category);

        void Update(Transactions category);

        void Delete(int id);

        List<Transactions> GetAll();

        Transactions Get(int id);
    }

    public class TransactionsService : ServiceBase, ITransactionsService
    {
        private const string FilePath = @"\bin\Data\Transactions.txt";

        private List<Transactions> _categories;

        public TransactionsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Transactions>();
        }

        public void Add(Transactions category)
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

        public Transactions Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Transactions> GetAll()
        {
            return _categories;
        }

        public void Update(Transactions category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Transactions> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Transactions>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
