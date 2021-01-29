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
    public interface IUsersService
    {
        void Add(Users category);

        void Update(Users category);

        void Delete(int id);

        List<Users> GetAll();

        Users Get(int id);
    }

    public class UsersService : ServiceBase, IUsersService
    {
        private const string FilePath = @"\bin\Data\Users.txt";

        private List<Users> _categories;

        public UsersService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<Users>();
        }

        public void Add(Users category)
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

        public Users Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Users> GetAll()
        {
            return _categories;
        }

        public void Update(Users category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<Users> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Users>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
