using Recipes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Management.Managers.Abstract
{
    public interface IRecipeManager : IManager
    {
        IEnumerable<Recipe> GetAll();
        void AddRecipe(Recipe recipe);
        void RemoveRecipe(List<Recipe> recipes);
    }
}
