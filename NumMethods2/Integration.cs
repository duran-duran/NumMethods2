using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumMethods2
{
    static class Integration
    {
        //Формула средних прямоугольников
        public static double RectangleRule(PostfixNotation func_notation, double left_bound, double right_bound, int subint_count)
        {
            //Вычисляем шаг разбиения
            double norm = (right_bound - left_bound) / subint_count;
            //Получаем, собственно, разбиение (мн-во точек)
            double[] x = GetPartition(left_bound, right_bound, subint_count);

            double sum = 0;

            //Проходим циклом по мн-ву точек
            //Замечание: число точек на единицу больше, чем число отрезков разбиения
            for (int i = 0; i < subint_count; i++)
            {
                //На каждом шагу добавляем полусумму значений ф-ции в текущей и следующей точках
                sum += func_notation.Calculate((x[i] + x[i+1]) /2);
            }

            //Возвращаем полученную сумму, предварительно умножив на шаг разбиения
            return norm * sum;
        }

        //Простейшая формула трапеций
        public static double TrapezoidalRule(PostfixNotation func_notation, double left_bound, double right_bound, int subint_count)
        {
            //Вычисляем шаг разбиения
            double norm = (right_bound - left_bound) / subint_count;
            //Получаем, собственно, разбиение (мн-во точек)
            double[] x = GetPartition(left_bound, right_bound, subint_count);

            double sum = 0;

            //Проходим циклом по мн-ву точек
            //Замечание: число точек на единицу больше, чем число отрезков разбиения
            for (int i = 0; i <= subint_count; i++)
            {
                //На концах интервала
                if (i == 0 || i == subint_count)
                    //берем по половине значения ф-ции
                    sum += func_notation.Calculate(x[i]) / 2;
                //В остальных точках - целиком
                else
                    sum += func_notation.Calculate(x[i]);
            }

            //Возвращаем полученную сумму, предварительно умножив на шаг разбиения
            return norm * sum;
        }

        //Получение разбиения (мн-ва точек) заданного интервала на заданное число отрезков
        public static double[] GetPartition(double left_bound, double right_bound, int subint_count)
        {
            //Точек в разбиение получается на одну больше, чем число отрезков
            double[] result = new double[subint_count + 1];
            //Вычисляем шаг разбиения
            double norm = (right_bound - left_bound) / subint_count;

            for (int i = 0; i <= subint_count; i++)
            {
                //Вычисляем значения точек
                result[i] = left_bound + i * norm;
            }

            return result;
        }
    }
}
