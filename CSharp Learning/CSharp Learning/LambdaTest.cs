using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Learning
{
    class LambdaTest
    {
        public static void Run()
        {
            List<int> datas = new List<int>();
            datas.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            foreach (int j in datas.FindAll(i => i % 2 == 0))
            {
                Console.Write("{0}\t", j);
            }
        }
    }
}
