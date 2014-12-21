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

namespace NumMethods2
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

        private void ConvertBtn_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.Text;
            PostfixNotation notaion = new PostfixNotation(input);
            try
            {
                double res = notaion.Calculate(3);
                MessageBox.Show(res.ToString());
            }
            catch
            {
                MessageBox.Show("Запись функции имеет неверный формат");
            }


        }
    }
}
