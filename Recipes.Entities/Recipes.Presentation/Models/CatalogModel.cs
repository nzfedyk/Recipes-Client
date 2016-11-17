using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Recipes.Presentation.Models
{
    public class CatalogModel
    {
        #region Ctor
        public CatalogModel(
            int ID, string Name, string Category,
            string CookList, string CookingString)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.Ingredients = CookList;
            this.CookingString = CookingString;
        }

        #endregion

        #region Data Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string CookingString { get; set; }
        #endregion
    }
}
