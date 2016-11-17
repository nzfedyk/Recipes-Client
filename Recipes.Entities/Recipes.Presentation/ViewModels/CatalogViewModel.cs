using Recipes.Entities;
using Recipes.Management.Managers.Abstract;
using Recipes.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Presentation.ViewModels
{
    public class CatalogViewModel
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IRecipeManager _recipeManager;

        List<CatalogModel> _recipes = null;

        static public List<CatalogModel> CreateCatalogModel(
            IEnumerable<Category> categories,
            IEnumerable<Recipe> recipes
            )
        {
            List<CatalogModel> listCatalogModels = new List<CatalogModel>();

            var _res = 
                from r in recipes
                join c in categories 
                    on r.CategoryID equals c.ID
                select new
                {
                    id = r.ID,
                    name = r.Name,
                    category = c.Name,
                    ingredients = r.Ingredients,
                    cookingString = r.CookingText
                };

            foreach (var r in _res)
            {
                listCatalogModels.Add(new CatalogModel(
                    r.id,
                    r.name,
                    r.category,
                    r.ingredients,
                    r.cookingString
                    ));
            }

            return listCatalogModels;
        }

        public IEnumerable<CatalogModel> Catalog
        {
            get
            {
                _recipes = CreateCatalogModel(
                        _categoryManager.GetAll(),
                        _recipeManager.GetAll()
                    );
                
                return _recipes;
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return _categoryManager.GetAll();
            }
        }

        #region Ctor
        public CatalogViewModel(
            ICategoryManager categoryManager,
            IRecipeManager recipeManager)
        {
            _categoryManager = categoryManager;
            _recipeManager = recipeManager;
        }
        #endregion Ctor
    }
}
