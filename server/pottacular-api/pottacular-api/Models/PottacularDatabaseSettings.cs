using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottacular_api.Models
{
    public interface IPottacularDatabaseSettings
    {
        string ConnectionString { get; set; }
        string TestRequestCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
    public class PottacularDatabaseSettings : IPottacularDatabaseSettings
    {
        public string TestRequestCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
