using LinkLambdaStudies.Entities;
using LinkLambdaStudies.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkLambda
{
    class Program
    {

        delegate double BinaryNumericOperation(double n1, double n2);
        delegate void BinaryNumericOperationDel(double n1, double n2);

        static void Main(string[] args)
        {
            #region Linq Delagate 
            List<Product> list = new List<Product>();

            list.Add(new Product("TV", 900.00));
            list.Add(new Product("Mouse", 50.00));
            list.Add(new Product("Notebook", 1200.00));
            list.Add(new Product("Tablet", 450.00));
            list.Add(new Product("HD Case", 80.90));

            list.Sort(CompareProducts);

            Comparison<Product> comp = CompareProducts;
            list.Sort(comp);

            Comparison<Product> comp2 = (p1, p2) => p1.Name.ToUpper().CompareTo(p2.Name.ToUpper());
            list.Sort(comp2);

            list.Sort((p1, p2) => p1.Name.ToUpper().CompareTo(p2.Name.ToUpper()));

            foreach (Product p in list)
            {
                Console.WriteLine(p);
            }
            #endregion

            Console.WriteLine("===============================================================================");

            #region Delegate CalculationService
            double a = 10, b = 12;
            Console.WriteLine(CalculationService.Square(a));
            Console.WriteLine(CalculationService.Sum(a, b));

            BinaryNumericOperation opSum = CalculationService.Sum;
            Console.WriteLine(opSum(a, b));

            BinaryNumericOperation opMax = CalculationService.Max;
            Console.WriteLine(opMax(a, b));

            //others
            BinaryNumericOperation opMaxOther = new BinaryNumericOperation(CalculationService.Max);
            BinaryNumericOperation opMaxOther2 = new BinaryNumericOperation(CalculationService.Max);
            double result = opMaxOther2.Invoke(a, b);
            #endregion

            Console.WriteLine("===============================================================================");

            #region MulticastDelegates
            BinaryNumericOperationDel opMultDel = CalculationService.ShowSum;
            opMultDel += CalculationService.ShowMax;
            opMultDel.Invoke(a, b);
            //other
            opMultDel(a, b);
            #endregion

            Console.WriteLine("===============================================================================");

            #region Delegate Predicate
            list.RemoveAll(x => x.Price >= 100);
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }

            list.Add(new Product("TV", 900.00));
            list.Add(new Product("Notebook", 1200.00));
            list.Add(new Product("Tablet", 450.00));

            list.RemoveAll(ProductTest);
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }

            list.Add(new Product("TV", 900.00));
            list.Add(new Product("Notebook", 1200.00));
            list.Add(new Product("Tablet", 450.00));
            #endregion

            Console.WriteLine("===============================================================================");

            #region Delegate Action with Foreach
            list.ForEach(UpdatePrice);
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            Action<Product> act = UpdatePrice;
            list.ForEach(act);
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            Action<Product> act1 = p => { p.Price += p.Price * 0.1; };
            list.ForEach(act1);
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            list.ForEach(p => { p.Price += p.Price * 0.1; });
            foreach (Product item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            #endregion

            Console.WriteLine("===============================================================================");

            #region Delegate Func with Select
            List<string> selectString = list.Select(NameUpper).ToList();
            foreach (string item in selectString)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            Func<Product, string> funcStringToUpper = NameUpper;
            List<string> select2 = list.Select(funcStringToUpper).ToList();
            foreach (string item in select2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            Func<Product, string> funcStringToUpper2 = x => x.Name.ToUpper();
            List<string> select3 = list.Select(funcStringToUpper2).ToList();
            foreach (string item in select3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            List<string> select4 = list.Select(x => x.Name.ToUpper()).ToList();
            foreach (string item in select3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ");

            #endregion

            Console.WriteLine("===============================================================================");

            #region Linq com Lambda
            int[] numbers = new int[] { 1, 2, 3, 4, 5 };

            //Define query expression - all even elements multiplied by 10
            IEnumerable<int> evenNumbers = numbers.Where(x => x % 2 == 0).Select(y => y * 10);
            Console.WriteLine(" ");

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Products> products = new List<Products>()
            {
                new Products() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Products() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Products() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Products() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Products() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Products() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Products() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Products() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Products() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Products() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Products() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            #region Linq 1

            IEnumerable<Products> res1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("TIER 1 AND PRICE < 900:", res1);

            IEnumerable<string> namesTools = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("NAMES OF PRODUCTS FROM TOOLS", namesTools);

            var objectsNamesStartedC = products.Where(p => p.Name[0] == 'C').Select(p => (p.Name, p.Price, CategoryName: p.Category.Name));
            //IEnumerable<(string Name, double Price, string CategoryName)> objectsNamesStartedC = products.Where(p => p.Name[0] == 'C').Select(p => (p.Name, p.Price, CategoryName: p.Category.Name));
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", objectsNamesStartedC);

            IOrderedEnumerable<Products> tier1OrderNamePrice = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", tier1OrderNamePrice);

            IEnumerable<Products> skipTake = tier1OrderNamePrice.Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", skipTake);

            Products firstElement = products.FirstOrDefault();
            Console.WriteLine("First or default test1: " + firstElement);

            Products firstPriceNoneElement = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2: " + firstPriceNoneElement);
            Console.WriteLine();

            Products singleElement = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or default test1: " + singleElement);
            Console.WriteLine();

            Products singleNoneElement = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("Single or default test2: " + singleNoneElement);
            Console.WriteLine();

            double biggestPrice = products.Max(p => p.Price);
            Console.WriteLine("Max price: " + biggestPrice);

            double lowestPrice = products.Min(p => p.Price);
            Console.WriteLine("Min price: " + lowestPrice);

            double sumPricesCat1 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Category 1 Sum prices: " + sumPricesCat1);

            double averagePriceCat1 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Category 1 Average prices: " + averagePriceCat1);

            double averagePriceCat5 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Category 5 Average prices: " + averagePriceCat5);
            
            double aggregateSum = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("Category 1 aggregate sum: " + aggregateSum);
            Console.WriteLine();

            IEnumerable<IGrouping<Category, Products>> categoryGroup1 = products.GroupBy(p => p.Category);
            foreach (IGrouping<Category, Products> group in categoryGroup1)
            {
                Console.WriteLine("Category " + group.Key.Name + ":");
                foreach (Products p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }

            #endregion
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            #region Linq 2

            res1 = from p in products
                   where p.Category.Tier == 1 && p.Price < 900.0
                   select p;
            Print("TIER 1 AND PRICE < 900:", res1);

            namesTools = from p in products
                         where p.Category.Name == "Tools"
                         select p.Name;
            Print("NAMES OF PRODUCTS FROM TOOLS", namesTools);

            var objectsNamesStartedC2 = from p in products
                                        where p.Name[0] == 'C'
                                        select new
                                        {
                                            p.Name,
                                            p.Price,
                                            CategoryName = p.Category.Name
                                        };
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", objectsNamesStartedC2);

            tier1OrderNamePrice = from p in products
                                  where p.Category.Tier == 1
                                  orderby p.Name
                                  orderby p.Price
                                  select p;
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", tier1OrderNamePrice);

            skipTake = (from p in tier1OrderNamePrice
                        select p)
                        .Skip(2)
                        .Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", skipTake);

            firstElement = (from p in products select p).FirstOrDefault();
            Console.WriteLine("First or default test1: " + firstElement);

            firstPriceNoneElement = (from p in products
                                     where p.Price > 3000.0
                                     select p).FirstOrDefault();
            Console.WriteLine("First or default test2: " + firstPriceNoneElement);
            Console.WriteLine();

            singleElement = (from p in products
                             where p.Id == 3
                             select p).SingleOrDefault();
            Console.WriteLine("Single or default test1: " + singleElement);
            Console.WriteLine();

            singleNoneElement = (from p in products
                                 where p.Id == 30
                                 select p).SingleOrDefault();
            Console.WriteLine("Single or default test2: " + singleNoneElement);
            Console.WriteLine();

            biggestPrice = (from p in products
                            select p.Price).Max();
            Console.WriteLine("Max price: " + biggestPrice);

            lowestPrice = (from p in products
                           select p).Min(p => p.Price);
            Console.WriteLine("Min price: " + lowestPrice);

            sumPricesCat1 = (from p in products
                             where p.Category.Id == 1
                             select p.Price).Sum();
            Console.WriteLine("Category 1 Sum prices: " + sumPricesCat1);

            averagePriceCat1 = (from p in products
                                where p.Category.Id == 1
                                select p).Average(p => p.Price);
            Console.WriteLine("Category 1 Average prices: " + averagePriceCat1);

            averagePriceCat5 = (from p in products
                                where p.Category.Id == 5
                                select p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Category 5 Average prices: " + averagePriceCat5);

            aggregateSum = (from p in products
                            where p.Category.Id == 1
                            select p.Price).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("Category 1 aggregate sum: " + aggregateSum);
            Console.WriteLine();

            IEnumerable<IGrouping<Category, Products>> categoryGroup = from p in products
                            group p by p.Category; //IEnumerable<IGrouping<Category, Products>> 
            foreach (IGrouping<Category, Products> group in categoryGroup)
            {
                Console.WriteLine("Category " + group.Key.Name + ":");
                foreach (Products p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }

            #endregion
            #endregion

            Console.WriteLine("===============================================================================");
        }

        static int CompareProducts(Product p1, Product p2) => p1.Name.ToUpper().CompareTo(p2.Name.ToUpper());

        public static bool ProductTest(Product p) => p.Price >= 100;

        static void UpdatePrice(Product p) => p.Price += p.Price * 0.1;

        static string NameUpper(Product p) => p.Name.ToUpper();

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
    }
}