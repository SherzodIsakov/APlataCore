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
    public interface IPacketLogService
    {
        void Add(PacketLog category);

        void Update(PacketLog category);

        void Delete(int id);

        List<PacketLog> GetAll();

        PacketLog Get(int id);
    }

    public class PacketLogService : ServiceBase, IPacketLogService
    {
        private const string FilePath = @"\bin\Data\PacketLog.txt";

        private List<PacketLog> _categories;

        public PacketLogService()
        {
            var savedData = ReadData();

            _categories = savedData != null ? savedData : new List<PacketLog>();
        }

        public void Add(PacketLog category)
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

        public PacketLog Get(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public List<PacketLog> GetAll()
        {
            return _categories;
        }

        public void Update(PacketLog category)
        {
            var oldCategory = Get(category.Id);

            //oldCategory.na = category.Name;

            SaveChanges();
        }

        private List<PacketLog> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<PacketLog>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}
