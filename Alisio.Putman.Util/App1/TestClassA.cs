using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class TestClassA
    {
        private string TekstA = "tekstA";
        public string TekstB = "tekstB";
    
        private static string TekstC = "tekstC";
        public static string TekstD = "tekstD";

        private int GetSum(int value1, int value2)
        {
            return value1 + value2;
        }

        public int GetMultiply(int value1, int value2)
        {
            return value1 * value2;
        }

        private static int GetDivide(int value1, int value2)
        {
            return value1 / value2;
        }

        public static int GetMin(int value1, int value2)
        {
            return value1 - value2;
        }
    }
}
