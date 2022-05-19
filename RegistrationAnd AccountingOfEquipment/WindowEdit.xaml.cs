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
using System.Windows.Shapes;

namespace RegistrationAnd_AccountingOfEquipment
{
    /// <summary>
    /// Логика взаимодействия для WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        private Equipment _currentequipment = new Equipment();

        public WindowEdit(Equipment selectedEquipment)
        {
            InitializeComponent();
            
            if (selectedEquipment != null)
                _currentequipment = selectedEquipment;

            DataContext = _currentequipment;
            


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            
            if (string.IsNullOrWhiteSpace(_currentequipment.Name_Equipment.Name))
                errors.AppendLine("Укажите название оборудывания");
            if (_currentequipment.Quantity < 0)
                errors.AppendLine("Укажите количество оборудывания");
            if (_currentequipment.SerialNumber < 0 )
                errors.AppendLine("Укажите сирийный номер");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if(_currentequipment.id == 0)
                Equipment_accountingEntities.GetContext().Equipment.Add(_currentequipment);

            try
            {
                Equipment_accountingEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                MainWindow equipmentwindow = new MainWindow();
                equipmentwindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow equipmentwindow = new MainWindow();
            equipmentwindow.Show();
            Close();
        }
    }
}
