using Recipes.Entities;
using Recipes.Management.Managers.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Management.Managers.Concrete
{
    public class RecipeManager : AbstractManager, IRecipeManager
    {
        public RecipeManager(string connStr) : base(connStr) { }

        public IEnumerable<Recipe> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Recipe>().ToList();
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                ctx.Set<Recipe>().Add(recipe);
                ctx.SaveChanges();
            }
        }

        public void RemoveRecipe(List<Recipe> recipes)
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                foreach (Recipe r in recipes)
                {
                    Recipe x = ctx.Set<Recipe>().Find(r.ID);
                    ctx.Set<Recipe>().Remove(x);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
