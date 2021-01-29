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
    public interface IResponseDescriptionsService
    {
        void Add(ResponseDescriptions category);

        void Update(ResponseDescriptions category);

        void Delete(int id);

        List<ResponseDescriptions> GetAll();

        ResponseDescriptions Get(int id);
    }

    public class ResponseDescriptionsService : ServiceBase, IResponseDescriptionsService
    {
        private const string FilePath = @"\bin\Data\ResponseDescriptions.txt";

        private List<ResponseDescriptions> _categories;

        public ResponseDescriptionsService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<ResponseDescriptions>();
        }

        public void Add(ResponseDescriptions category)
        {
            int id = GetMaxId(_categories.OfType<IBaseId>().ToList());

            category.ResponseCode = id + 1;

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

        public ResponseDescriptions Get(int id)
        {
            return _categories.FirstOrDefault(x => x.ResponseCode == id);
        }

        public List<ResponseDescriptions> GetAll()
        {
            return _categories;
        }

        public void Update(ResponseDescriptions category)
        {
            var oldCategory = Get(category.ResponseCode);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<ResponseDescriptions> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<ResponseDescriptions>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
