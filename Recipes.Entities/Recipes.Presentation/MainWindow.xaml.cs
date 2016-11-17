using Recipes.Management.Managers.Abstract;
using Recipes.Management.Managers.Concrete;
using Recipes.Presentation.ViewModels;
using Recipes.Presentation.Views;
using System;
using System.Windows;

namespace Recipes.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartScreenUC startScreen = new StartScreenUC();
        CatalogUC catalogUC = new CatalogUC();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.Title = "Рецепти v. 1.0";
            this.Content = startScreen;
            OpenCatalog();
        }

        private void OpenCatalog()
        {
            this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            this.Content = catalogUC;
        }
    }
}
