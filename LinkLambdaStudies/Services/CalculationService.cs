using System;

namespace LinkLambdaStudies.Services
{
    class CalculationService
    {
        public static double Max(double x, double y) =>  (x > y) ? x : y;

        public static double Sum(double x, double y) => x + y;
        
        public static double Square(double x) => x * x;

        public static void ShowMax(double x, double y) => Console.WriteLine((x > y ? x : y));
        
        public static void ShowSum(double x, double y) => Console.WriteLine( x + y);

    }
}
