using Recipes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Management.Managers.Abstract
{
    public interface ICategoryManager : IManager
    {
        IEnumerable<Category> GetAll();
        void AddCategory(Category category);
        void RemoveCategory(IEnumerable<Category> categories);
    }
}
