using System;
using CustomLinq;
using System.Collections.Generic;

namespace CustomLinqExtensionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Demo
            var list = new List<int> {0, 1, 2, 3, 4, 5};
            var evenList = list.SelectExt(x => x * 2);
            foreach (var item in evenList)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();

            var AncientEmpiresAndPolices = new List<string> { "Rome", "Etruscan League", "Arvernii", "Boiotian League", "Athenai", "Seleucid Empire", "Carthage" };
            var selection = AncientEmpiresAndPolices.WhereExt(x => x.Length > 7);
            foreach (var item in selection)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();

            var PopulationByCountry = new List<Country>();
            var China = new Country() { Name = "China", Population = 1400000000, Capital = "Beijing" };
            var India = new Country() { Name = "India", Population = 1380000000, Capital = "Delhi" };
            var Japan = new Country() { Name = "Japan", Population = 12580000, Capital = "Tokyo" };
            PopulationByCountry.Add(China);
            PopulationByCountry.Add(India);
            PopulationByCountry.Add(Japan);
            var countryDict = PopulationByCountry.ToDictionaryExt(x => x.Name);
            foreach (var item in countryDict)
            {
                Console.Write(item.Key + "-" + item.Value + ", ");
            }
            Console.WriteLine();

            var orderedByPopulation = PopulationByCountry.OrderByDescExt(x => x.Population);
            foreach (var item in orderedByPopulation)
            {
                Console.WriteLine(item.Name + " " + item);
            }
            Console.WriteLine();

            PopulationByCountry.Add(China);
            PopulationByCountry.Add(China);
            PopulationByCountry.Add(China);
            PopulationByCountry.Add(Japan);

            var groupByCountry = PopulationByCountry.GroupByExt(x => x.Name);
            foreach (var item in groupByCountry)
            {
                Console.WriteLine(item.Key);
                foreach (var piece in PopulationByCountry)
                {
                    if (piece.Name == item.Key)
                    {
                        Console.Write(piece + " ");
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
            #endregion

        }
        
    }
    class Country : System.Collections.IEnumerable
    {
        public string Name;
        public int Population;
        public string Capital;

        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Capital+" "+Population.ToString();
        }
    }

}
