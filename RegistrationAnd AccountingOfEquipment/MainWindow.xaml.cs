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

namespace RegistrationAnd_AccountingOfEquipment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            
            WindowEdit equipmentwindow = new WindowEdit(null);
            equipmentwindow.Show();
            Close();
        }

        private void Button_Dell_Click(object sender, RoutedEventArgs e)
        {
            var equipmentForRemoving = DataGrid.SelectedItems.Cast<Equipment>().ToList();

            if(MessageBox.Show($"Вы точно хотте удалить следующие{equipmentForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Equipment_accountingEntities.GetContext().Equipment.RemoveRange(equipmentForRemoving);
                    Equipment_accountingEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалины!");

                    DataGrid.ItemsSource = Equipment_accountingEntities.GetContext().Equipment.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {

            WindowEdit equipmentwindow = new WindowEdit((sender as Button).DataContext as Equipment);
            equipmentwindow.Show();
            Close();
        }


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Equipment_accountingEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGrid.ItemsSource = Equipment_accountingEntities.GetContext().Equipment.ToList();
            }
        }
        
    }
}
