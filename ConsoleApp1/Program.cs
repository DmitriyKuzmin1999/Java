using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m = new Matrix();

            /*  m.GenerateMas(7, 7);
              double[] test = m.GenerateX(7,5);
              double[] check =  new double[7];
              test.CopyTo(check, 0);
              m.GenerateF();
              double[] res = m.Solve();
           /*  for (int i = 0; i<7; i++)
                 Console.WriteLine(Math.Abs(res[i]-check[i]));*/
            /*  foreach (double item in res)
                  Console.WriteLine(item);*/
            double avgEps, avgMark;
            m.Test(10, 100, 100, out avgEps, out avgMark);
            string strEps = String.Format("0.00", avgEps);
            string strMark = String.Format("0.00", avgMark);
            Console.WriteLine(avgEps+"-------------------------------------"+avgMark);
          //  foreach (double item in check)
            //    Console.WriteLine(item);

        }
    }
}
