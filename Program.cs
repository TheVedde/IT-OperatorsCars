using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace IT_OperatorsCars
{
    class Program
    {

        public enum Origin
        {
            Europe,
            USA,
            Japan,
        }

        public List<Car> ReturnCarsModelled()
        {
            //Make reference local to project.
            var Json = File.ReadAllText(@"cars.json");
            //JsonTextReader JsonReader = new JsonTextReader(new TextReader)
            var cars = JsonConvert.DeserializeObject<List<Car>>(Json);
            return cars;

        }
        public void ReturnOldFordCars()
        {
            var cars = ReturnCarsModelled();

            int count = 0;

            Console.WriteLine("Task 3: Find antallet af Ford biler som er fra efter 1 januar 1980.\n");
            var carQuery = from car in cars where (car.Name).Contains("ford") && (car.Year) >= new DateTime(1980, 1, 1) select car;
            foreach (Car car in carQuery)
            {
                Console.Write("{0}\n ", car.Name);
                count++;
            }
            Console.WriteLine("AMount: "  + count);
            /*
            for (int i = 0; i < cars.Count; i++)
            {
                if (Regex.Match(cars[i].Name, @"\w+").ToString() == "ford" && cars[i].Year >= new DateTime(1980, 1, 1)) {
                      count++;
                }
            }*/
        }



        static void Main(string[] args)
        {
            Program p = new Program();

            //Task 1
            Console.WriteLine("Task 1: Lav en model som har alt data bilerne fra filen har og indlæs Json filen ind i en liste af modellen\n");
            var cars = p.ReturnCarsModelled();

            //Task 2
            Console.WriteLine("Task 2: Find ud af hvor mange biler der er af hver bil mærke : Failed\n");
            List<string> carBrand = new List<string>();
            for (int i = 0; i < cars.Count; i++)
            {                
                //Find a way to use the names to sort items
                string name = Regex.Match(cars[i].Name, @"\w+").ToString();
                carBrand.Add(name);
            }
            //Console.WriteLine(carBrand.Count);

            //Task 3
            p.ReturnOldFordCars();


            Console.WriteLine("Task 4: Find gennemsnitlige hestekræfter på biler grupperet efter “origin” : Failed\n");
            var groupByOriginAverageQuery = from car in cars group car by car.Origin into originGroup select new { origin = originGroup.Key, AverageHP = (from car2 in originGroup select car2.Horsepower) }; 
            foreach(var orgingroup in groupByOriginAverageQuery) 
            {
//                Console.WriteLine($"Key: {orgingroup.Key}");
            }


            //Task 5
            Console.WriteLine("Task 5 : Find km/L på alle biler, regn med at der er 1,6 km pr. mile");
            float value = 0;
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Miles_per_Gallon.HasValue)
                    value = p.ConvertToKML((float)cars[i].Miles_per_Gallon);
                    Console.WriteLine(cars[i].Name + " has " + cars[i].Miles_per_Gallon + " Per Gallon = " + value + "KM/L");
            }
            /*
            for (int i = 0; i < cars.Count; i++)
            {
                if(Enum.IsDefined(typeof(Origin), cars[i].Origin))
                {

                }

            }*/
        }


        public float ConvertToKML(float MPG)
        {
            //Uses the value conversion rate.
            return MPG * 0.425144f;
        }
    }
    public class Car
    {
        public string Name { get; set; }
        public float? Miles_per_Gallon { get; set; }
        public int Cylinders { get; set; }
        public float Displacement { get; set; }
        public int? Horsepower { get; set; }
        public int Weight_in_lbs { get; set; }
        public float Acceleration { get; set; }
        public DateTime Year { get; set; }
        public string Origin { get; set; }
    }
}
