using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Management.Managers.Abstract
{
    public class AbstractManager
    {
        private readonly string _connectionString;

        public AbstractManager(string connStr)
        {
            this._connectionString = connStr;
        }

        protected DbContext CreateDbContext()
        {
            return new DbContext(_connectionString);
        }
    }
}
