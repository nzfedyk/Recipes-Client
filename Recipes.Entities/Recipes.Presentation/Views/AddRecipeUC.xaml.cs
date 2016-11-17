using Recipes.Entities;
using Recipes.Management.Managers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Recipes.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddRecipeUC.xaml
    /// </summary>
    public partial class AddRecipeUC : UserControl
    {
        public AddRecipeUC()
        {
            InitializeComponent();   
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<string> categories =
                    CatalogUC.catalogViewModel.Categories.Where(c => c.ParentID == null).OrderBy(c => c.Name)
                    .Select(c => c.Name).ToList();
            cmbMainCategory.DataContext = categories;
        }

        private void cmbMainCategory_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            cmbCategory.IsEnabled = true;

            Category selectedCategory = CatalogUC.catalogViewModel.Categories
                .Where(c => c.Name == cmbMainCategory.SelectedItem.ToString())
                .First();

            List<string> subcategories = CatalogUC.catalogViewModel.Categories.Where(c => c.ParentID == selectedCategory.ID)
                .OrderBy(c => c.Name).Select(c => c.Name).ToList();

            cmbCategory.DataContext = subcategories;
        }

        private void AddMainCategoryBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void AddCategoryBtn_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text != null && Ingredients.Text != null && CookingText.Text != null
                && cmbMainCategory.SelectedItem != null && cmbCategory.SelectedItem != null)
            {
                try
                {
                    Category category = CatalogUC.catalogViewModel.Categories
                        .Where(c => c.Name == cmbCategory.SelectedItem.ToString())
                        .First();

                    Recipe res = new Recipe
                    {
                        CategoryID = category.ID,
                        Name = nameTxt.Text,
                        Ingredients = Ingredients.Text,
                        CookingText = CookingText.Text
                    };

                    CatalogUC.recipeManager.AddRecipe(res);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Window parentWindow = (Window)this.Parent;
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show("Не всі поля заповнені");
            }
        }

        private void btnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }
    }
}
