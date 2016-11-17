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
    public class CategoryManager : AbstractManager, ICategoryManager
    {
        public CategoryManager(string connStr) : base(connStr) { }

        public IEnumerable<Category> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Category>().ToList();
            }
        }

        public void AddCategory(Category category)
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                ctx.Set<Category>().Add(category);
                ctx.SaveChanges();
            }
        }

        public void RemoveCategory(IEnumerable<Category> categories)
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                ctx.Set<Category>().RemoveRange(categories);
                ctx.SaveChanges();
            }
        }
    }
}
