using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Matrix
    {
        static readonly Random rn = new Random();
        double[] a, b, c, p, q, f;
        int k;
        double[] x;
        double[] _x;
        public void GenerateMas(int kolEquation, double range)
        {
            a = new double[kolEquation];
            b = new double[kolEquation];
            c = new double[kolEquation];
            p = new double[kolEquation];
            q = new double[kolEquation];
            x = new double[kolEquation]; //точное решение
            //определяем расположение столбцов
             k = rn.Next(2, kolEquation - 4);
            //задаем рандомные значения векторам
            for (int i = 0; i < kolEquation; i++)
            {
                a[i] = (double)rn.NextDouble() * 2 * range - range;
                b[i] = ((double)rn.NextDouble() * 2 * range - range);
                c[i] = (double)rn.NextDouble() * 2 * range - range;
                p[i] = (double)rn.NextDouble() * 2 * range - range;
                q[i] = (double)rn.NextDouble() * 2 * range - range;
            }

            a[0] = 0;
            c[kolEquation - 1] = 0;
            p[k - 1] = c[k - 1];
            p[k] = b[k];
            p[k + 1] = a[k + 1];
            q[k] = c[k];
            q[k + 1] = b[k + 1];
                q[k + 2] = a[k + 2];
        }

        //генирируем рандомные решения
        public double[] GenerateX(int kolEquation, double range)
        {
            for (int i = 0; i < kolEquation; i++)
            {
                x[i] = (double)rn.NextDouble() * 2 * range - range;
            }
            return x;
        }

        //создаем единичный вектор
        public double[] GenerateX1(int kolEquation)
        {
            for (int i = 0; i < kolEquation; i++)
            {
                x[i] = 1;
            }
            return x;
        }

        //перемножаем вектор на матрицу ++
        public double[] GenerateF()
        {
            int N = b.Length;
            f = new double[N];
            f[0] = b[0] * x[0] + c[0] * x[1] + p[0]*x[k]+q[0]*x[k+1];

            for (int i = 1; i < k - 1; i++)
            {
                f[i] = a[i] * x[i - 1] + b[i] * x[i] + c[i] * x[i + 1] + p[i] * x[k] + q[i] * x[k + 1];       
            }

            f[k - 1] = a[k - 1] * x[k - 2] + b[k - 1] * x[k-1] + c[k - 1] * x[k] + q[k - 1] * x[k + 1];
            f[k] = a[k] * x[k - 1] + b[k] * x[k] + c[k] * x[k + 1];
            f[k + 1] = a[k + 1] * x[k] + b[k + 1] * x[k + 1] + c[k + 1] * x[k + 2];
            f[k + 2] = p[k + 2] * x[k] + a[k + 2] * x[k + 1] + b[k + 2] * x[k + 2] + c[k + 2] * x[k + 3];

            for (int i = k + 3; i < N-1; i++)
            {
                f[i] = a[i] * x[i - 1] + b[i] * x[i] + c[i] * x[i + 1] + p[i] * x[k] + q[i] * x[k + 1];
            }
            f[N-1] = a[N-1] * x[N - 2] + b[N-1] * x[N-1] +  p[N-1] * x[k] + q[N-1] * x[k + 1];

            return f;
        }

        public void Step_1()
        {
            int N = b.Length;
            double r;
            //первые к строк делаем нули на нижней кодиагонали
            for (int i = 0; i <= k-1; i++) //k+1
            {
                if (b[i] != 0)
                {
                    r = 1 / b[i];
                    b[i] = 1;
                    f[i] = f[i] * r;
                    p[i] = p[i] * r;
                    q[i] = q[i] * r;
                    c[i] = r * c[i];
                    if (a[i + 1] != 0)
                    {
                        r = a[i + 1];
                        a[i + 1] = 0;
                        b[i + 1] = b[i + 1] - r * c[i];
                        p[i + 1] = p[i + 1] - r * p[i];
                        q[i + 1] = q[i + 1] - r * q[i];
                        f[i + 1] = f[i + 1] - r * f[i];
                        if (i == k - 2)
                            c[k - 1] = p[k - 1];
                        if (i == k - 1)
                        {
                            b[k] = p[k];
                            c[k] = q[k];
                        }
                    }

                }
                else throw new Exception("Невозможно найти значения!");
            }

        }
       
        public void Step_2()
        {
            int N = b.Length;
            double r;
            //снизу до к строки делаем нули на верхней кодиагонали
            for (int i = N - 1; i >= k+1; i--)
            {
                if (b[i] != 0)
                {
                    r = 1 / b[i];
                    b[i] = 1;
                    f[i] = f[i] * r;
                    p[i] = p[i] * r;
                    q[i] = q[i] * r;
                    a[i] = a[i] * r;
                    if (c[i - 1] != 0)
                    {
                        r = c[i - 1];
                        c[i - 1] = 0;
                        b[i - 1] = b[i - 1] - r * a[i];
                        p[i - 1] = p[i - 1] - r * p[i];
                        q[i - 1] = q[i - 1] - r * q[i];
                        f[i - 1] = f[i - 1] - r * f[i];
                        if (i == k + 2)
                            a[k + 1] = p[k + 1];
                        if (i == k + 3)
                            a[k + 2] = q[k + 2];
                    }
                } 
                else throw new Exception("Невозможно найти значения!");
            }
        }
       
        public void Step_3()
        {
            if (b[k] != 0)
            {
                _x[k] = f[k] / b[k];
                _x[k + 1] = f[k + 1] - a[k + 1] * _x[k];
                //идем вниз
                for (int i = k + 2; i < b.Length; i++)
                {
                    if (i == k + 2)
                        _x[i] = f[i] - p[i] * _x[k]  - q[i] * _x[k+1];
                    else
                        _x[i] = f[i] - p[i] * _x[k] - q[i] * _x[k + 1] - a[i] * _x[i - 1];
                }
                //поднимаемся наверх
                for (int i = k - 1; i > -1; i--)
                {
                    if (i == k-1)
                        _x[i] = f[i]  - p[i] * _x[k] - q[i] * _x[k+1];
                    else
                        _x[i] = f[i] - c[i] * _x[i + 1] - p[i] * _x[k] - q[i] * _x[k + 1];

                }               
            }
            else throw new Exception("Невозможно найти значения!");
        }
        
        public double[] Solve()
        {

            _x = new double[b.Length]; //решение системы
            Step_1();
            Step_2();
            Step_3();
            return _x; 
        }

        //оценка точности
        double CountMark()
        {
            double mark = 0;
            for (int i = 0; i < b.Length; i++)
            {
                mark = Math.Max(mark, Math.Abs(_x[i] - 1));
            }
            return mark;
        }

        //погрешность
        double CountEps()
        {
            double eps = 0;
            for (int i = 0; i < b.Length; i++)
            {
                eps = Math.Max(Math.Abs(x[i] - _x[i]), eps);
            }
            return eps;
        }

        public void Test(int kolTests, double range, int _kolEquation, out double avgEps, out double avgMark)
        {
            avgMark = 0;
            avgEps = 0;

            //считаем погрешности
            for (int i = 0; i < kolTests; i++)
            {
                //проверяем на размерности 10*10
                //считаем погрешность
                GenerateMas(_kolEquation, range);
                GenerateX(_kolEquation, range);
                GenerateF();
                Solve();
                avgEps += CountEps();
                //счтиаем оценку точности
                GenerateMas(_kolEquation, range);
                GenerateX1(_kolEquation);
                GenerateF();
                Solve();
                avgMark += CountMark();
            }
            avgEps /= _kolEquation;
            avgMark /= _kolEquation;
        }
    }
}
