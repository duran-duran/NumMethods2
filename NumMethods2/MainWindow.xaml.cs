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
using System.Windows.Forms.DataVisualization.Charting;

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

        private void IntegrateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Получаем стоку, в которой записана ф-ция
                string func_input = FunctionInput.Text;
                PostfixNotation func_notation = new PostfixNotation(func_input);

                //Получаем границы интервала
                double left_bound = Convert.ToDouble(LeftBoundBox.Text);
                double right_bound = Convert.ToDouble(RightBoundBox.Text);

                //Получаем начальное/конечное значения и шаг для цикла вычислений
                int partition_start = Convert.ToInt32(PartStartBox.Text);
                int partition_finish = Convert.ToInt32(PartFinishBox.Text);
                int step = Convert.ToInt32(PartStepBox.Text);

                //Получаем код требуемого способа интегрирования
                string rule = (RuleBox.SelectedItem as ComboBoxItem).Name;

                //Считаем число итераций, которые требуется сделать
                int iterations_count = (int)Math.Floor((double)((partition_finish - partition_start) / step)) + 1;

                //Инициализируем массивы, в которых будут хранится данные для осей X/Y графика
                int[] partition_row = new int[iterations_count];
                double[] results = new double[iterations_count];

                //Запускаем цикл вычислений
                for (int i = 0; i < iterations_count; i++)
                {
                    //Вычисляем число отрезков, на которые нужно разбить интервал на данной итерации
                    int subint_count = partition_start + i * step;

                    //Вычисляем интеграл соотв. формулой
                    switch (rule)
                    {
                        case "Rect":
                            results[i] = Integration.RectangleRule(func_notation, left_bound, right_bound, subint_count);
                            break;
                        case "Trap":
                            results[i] = Integration.TrapezoidalRule(func_notation, left_bound, right_bound, subint_count);
                            break;
                    }

                    //Записываем мелкость разбиения на данной итерации
                    partition_row[i] = subint_count;
                }

                //Отображаем график в интерфейсе, если он был скрыт
                if (ChartContainer.Visibility == System.Windows.Visibility.Collapsed)
                    ChartContainer.Visibility = System.Windows.Visibility.Visible;

                //Добавляем линию на график
                Series result_series = new Series();
                result_series.ChartType = SeriesChartType.Line;
                result_series.Points.DataBindXY(partition_row, results);
                StatsChart.Series.Add(result_series);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WFHost_Loaded(object sender, RoutedEventArgs e)
        {
            StatsChart.ChartAreas.Add(new ChartArea("Result"));
            StatsChart.ChartAreas["Result"].AxisX.Title = "Subinterval count";
            StatsChart.ChartAreas["Result"].AxisY.Title = "Result";
            StatsChart.ChartAreas["Result"].AxisY.IsStartedFromZero = false;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            StatsChart.Series.Clear();
            ChartContainer.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
