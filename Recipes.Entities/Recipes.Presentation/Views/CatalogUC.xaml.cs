using Recipes.Entities;
using Recipes.Management.Managers.Abstract;
using Recipes.Management.Managers.Concrete;
using Recipes.Presentation.Models;
using Recipes.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for CatalogUC.xaml
    /// </summary>
    public partial class CatalogUC : UserControl
    {
        public CatalogUC()
        {
            InitializeComponent();
        }

        public IEnumerable<CatalogModel> currentCatalog;

        static public string connStr = ConfigurationManager.ConnectionStrings["RecipesDBEntities"].ConnectionString;

        static public ICategoryManager categoryManager = new CategoryManager(connStr);
        static public IRecipeManager recipeManager = new RecipeManager(connStr);
        static public CatalogViewModel catalogViewModel = new CatalogViewModel(
            categoryManager, recipeManager);

        private void SetContext(IEnumerable<CatalogModel> catalog)
        {
            dgCatalogUC.DataContext = catalog.OrderBy(c => c.Name);
            currentCatalog = catalog;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> categories =
                    catalogViewModel.Categories.Where(c => c.ParentID == null).OrderBy(c => c.Name).Select(c => c.Name).ToList();

                categories.Insert(0, "Всі рецепти");

                cmbMainCategory.DataContext = categories;

                SetContext(catalogViewModel.Catalog);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            SetContext(currentCatalog.Where(c => c.Name.ToLower().IndexOf(tbxSearch.Text.ToLower()) != -1
                || c.Ingredients.ToLower().IndexOf(tbxSearch.Text.ToLower()) != -1));
        }

        private void cmbMainCategory_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMainCategory.SelectedIndex == 0)
                {
                    SetContext(catalogViewModel.Catalog.OrderBy(c => c.Name));
                    cmbCategory.IsEnabled = false;
                }
                else
                {
                    Category selectedCategory = catalogViewModel.Categories.Where(c => c.Name == cmbMainCategory.SelectedItem.ToString())
                        .First();

                    List<string> subcategories = catalogViewModel.Categories.Where(c => c.ParentID == selectedCategory.ID)
                        .Select(c => c.Name).ToList();

                    List<CatalogModel> list = new List<CatalogModel>();
                    foreach (string x in subcategories)
                    {
                        list.AddRange(catalogViewModel.Catalog.Where(c => c.Category == x));
                    }

                    SetContext(list.OrderBy(c => c.Name));

                    subcategories.Insert(0, "Всі підкатегорії");

                    cmbCategory.IsEnabled = true;
                    cmbCategory.ItemsSource = subcategories;
                    cmbCategory.Items.Refresh();
                    cmbCategory.SelectedItem = cmbCategory.Items[0];
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddRecipe_Click_1(object sender, RoutedEventArgs e)
        {
            ChildWindow chldWin = new ChildWindow();
            chldWin.Content = new AddRecipeUC();
            chldWin.Title = "Додавання рецепту";
            chldWin.ShowDialog();
            dgCatalogUC.DataContext = catalogViewModel.Catalog.OrderBy(c => c.Name);
        }

        private void DeleteRecipe_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgCatalogUC.SelectedItems.Count == 0)
            {
                MessageBox.Show("Не виділено жодного рецепту", "Інформація",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show("Ви дійсно бажаєте видалити видалені рецепти?", "Підтвердження вибору",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    List<Recipe> recipes = new List<Recipe>();

                    foreach (CatalogModel x in dgCatalogUC.SelectedItems)
                    {
                        Recipe res = new Recipe
                        {
                            ID = x.ID,
                            Name = x.Name,
                            CategoryID = catalogViewModel.Categories.Where(c => c.Name == x.Category).Select(c => c.ID).First(),
                            Ingredients = x.Ingredients,
                            CookingText = x.CookingString,
                        };

                        recipes.Add(res);
                    }

                    recipeManager.RemoveRecipe(recipes);
                    dgCatalogUC.DataContext = catalogViewModel.Catalog.OrderBy(c => c.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmbCategory_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedIndex != 0)
            {
                SetContext(catalogViewModel.Catalog.Where(c => c.Category == cmbCategory.SelectedItem.ToString()));
            }
            else
            {
                Category selectedCategory = catalogViewModel.Categories.Where(c => c.Name == cmbMainCategory.SelectedItem.ToString())
                    .First();

                List<string> subcategories = catalogViewModel.Categories.Where(c => c.ParentID == selectedCategory.ID)
                    .Select(c => c.Name).ToList();

                List<CatalogModel> list = new List<CatalogModel>();
                foreach (string x in subcategories)
                {
                    list.AddRange(catalogViewModel.Catalog.Where(c => c.Category == x));
                }
                SetContext(list.OrderBy(c => c.Name));
            }
        }
    }
}
