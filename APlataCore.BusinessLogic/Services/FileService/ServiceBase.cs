using APlataCore.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.FileService
{
    public abstract class ServiceBase
    {
        public int GetMaxId(List<IBaseId> entities)
        {
            if (entities == null || !entities.Any())
                return 0;

            return entities.Max(e => e.Id);
        }

        protected string GetStoragePath(string fileName)
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\{fileName}";
        }
    }
}
