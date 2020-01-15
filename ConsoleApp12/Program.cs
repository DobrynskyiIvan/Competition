using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public class Girls
    {
            public string Name { get; set; }
            public decimal Score { get; set; }
    }
    public class StartVolume
    {
        public int  Judjes { get; set; }
        
        public int CountOfGIrls { get; set; }
    }
   
    class Voutes
    {
        public decimal Result { get; set; }
        public static int GetVolume()
        {
            Random rand = new Random();
            int z = 61;
            int volume = rand.Next(z) ;
            return volume; 
        }

       protected int k = GetVolume();

        public  Voutes(int a)
        {
            decimal[] voute = new decimal[a];
            Random rand = new Random();
            Console.WriteLine($"result of this girl {k/10.0}");

            for (int i = 0; i < a; i++)
            {
                decimal value = Convert.ToDecimal(rand.Next(61-k) / 10.0M);
                //Console.WriteLine(value);
                voute[i] =(k/10.0M)+ value;
                Console.WriteLine(voute[i]);

            }
            // сортировка
            decimal temp;
            for (int i = 0; i < voute.Length - 1; i++)
            {
                for (int j = i + 1; j < voute.Length; j++)
                {
                    if (voute[i] > voute[j])
                    {
                        temp = voute[i];
                        voute[i] = voute[j];
                        voute[j] = temp;
                    }
                }
            }

            decimal opt = (voute.Sum() - voute.Max() - voute.Min()) / (voute.Length - 2) * 1.0M;
            decimal result = Math.Round(opt, 1);
            Result = result;
            Console.WriteLine($"is the final score:{result}");

        }
        

    }
    class Program
    {
        #region Cheking the name!!!
        private static string ParseString()
        {
            while (true)
            {
                Console.WriteLine("Введите имя:");
                string name = Console.ReadLine();
                bool isValid = name.All(char.IsLetter) && name.Length >= 3;
                if (isValid == true)
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("You enter incorect value, use letter(min size is 3) ");
                }     
            }  
        }
        #endregion
        #region Cheking the int value!!!

        private static int ParseInt(string name)
        {
            while (true)
            {
                Console.WriteLine($"Enter number of {name} in competition  :");
               
                bool isValid = int.TryParse(Console.ReadLine(), out int value);
                if (isValid == true&&value>3&&value<10)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"wrong format of  {name}, must be digits from 3 to 10 ");
                }
            }
        }
        #endregion
        static void Main(string[] args)
        {
            var count = ParseInt("girls ");
            var judjes = ParseInt("judjes");

            StartVolume user = new StartVolume { CountOfGIrls = count, Judjes = judjes };


            #region Try to solve thrue dictionary 
            //Dictionary<string, decimal> scores = new Dictionary<string, decimal>(user.Count);
            //for (int i = 0; i < user.Count; i++)
            //{
            //    Console.WriteLine("Введите имя:");
            //    string name = Console.ReadLine();
            //    Voutes voutes = new Voutes(user.Judjes);

            //    scores.Add(name, voutes.Result);

            //}

            //scores = scores.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);



            //foreach (KeyValuePair<string, decimal> keyValue in scores)
            //{

            //    Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            //}
            //Console.WriteLine("First place girl name: {0} with score: {1}", scores.ElementAt(0).Key, scores.ElementAt(0).Value);
            //Console.WriteLine("Second place girl name: {0} with score: {1}", scores.ElementAt(1).Key, scores.ElementAt(1).Value);
            //Console.WriteLine("Third place girl name: {0} with score: {1}", scores.ElementAt(2).Key, scores.ElementAt(2).Value);
            #endregion

            #region  !!!Right option to solve the problem

            List<Girls> resultsOf = new List<Girls>(user.CountOfGIrls);
            for (int i = 0; i < user.CountOfGIrls; i++)
            {
                var name = ParseString();
               
                Voutes voutes = new Voutes(user.Judjes);

                resultsOf.Add(new Girls { Name = name, Score = voutes.Result });

            }
                    
                    var resultGroups = from girls in resultsOf
                                       group girls by girls.Score;
     
                    var sortedUsers = from u in resultGroups
                              orderby u.Key descending
                              select u;

            int place = 1;
                    foreach (IGrouping<decimal, Girls> g in sortedUsers)
                    {
                        if (place < 4)
                        {
                            Console.WriteLine($"The {place} place has result:  {g.Key} scores and that is:");
                            foreach (var t in g)
                                Console.WriteLine($"{t.Name}  with result : {t.Score}");
                            place++;

                        }
                       

                    }
            #endregion
            Console.Read();
        }
    }
        
}
    

