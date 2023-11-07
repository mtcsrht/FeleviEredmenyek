using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FeleviEredmenyek
{
    class Program
    {
        private static List<Person> tanulok = new List<Person>();

        static void Main(string[] args)
        {
            using var sr = new StreamReader(@"..\..\..\src\eredmenyek.txt");

            List<string> tantNevek = sr.ReadLine().Split("\t").Skip(2).ToList();
            _ = sr.ReadLine();
            while (!sr.EndOfStream)
            { 
                tanulok.Add(new Person(sr.ReadLine()));
                _ = sr.ReadLine();
            }

            Console.WriteLine($"Osztaly atlag {OsztalyAtlag():0.00}");

            foreach (var item in TantargyankentiAtlag(tantNevek))
            {
                Console.WriteLine($"{item.Key}: {item.Value:0.00}");
            }

            Console.WriteLine("Megbukottak");
            foreach (var item in ProgMegbukottak())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Elso angol 3-as");
            Console.WriteLine(ElsoHarmasAngol().ToString());


            Console.Write("Kinek a legjobb jegyet szeretned?: ");
            string resp = Console.ReadLine();
            var kivalasztott = tanulok.FirstOrDefault(t => t.Nev == resp);
            Console.WriteLine($"{kivalasztott.LegjobbJegy}");

            using var sw = new StreamWriter(path: @"..\..\..\src\kiirando.txt", append: false);
            sw.WriteLine($"{kivalasztott.Nev} {kivalasztott.OktAzon}");


        }


        public static Person ElsoHarmasAngol()
        {
            return tanulok.FirstOrDefault(t=> t.Jegyek[4] == 3);
        }
        public static List<Person> ProgMegbukottak()
        {
            return tanulok.Where(t => t.Jegyek[3] == 1).ToList();
        }
        public static double OsztalyAtlag()
        {
            return tanulok.Average(t => t.Atlag);
        }

        public static Dictionary<string, double> TantargyankentiAtlag(List<string> tantNevek)
        {
            int[,] tMatrix = new int[tanulok.Count, 8];

            for (int r = 0; r < tMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < tMatrix.GetLength(1); c++)
                {
                    tMatrix[r, c] = tanulok[r].Jegyek[c];
                }
            }
            List<double> ertekek = new List<double>();
            int helper;
            for (int c = 0; c < tMatrix.GetLength(1); c++)
            {
                helper = 0;
                for (int r = 0; r < tMatrix.GetLength(0); r++)
                {
                    helper += tMatrix[r, c];
                }
                ertekek.Add(helper);
            }

            for (int i = 0; i < ertekek.Count; i++)
            {
                ertekek[i] = ertekek[i] / tanulok.First().Jegyek.Count;
            }

            Dictionary<string, double> tantargyakatlag = new Dictionary<string, double>();
            for (int i = 0; i < ertekek.Count; i++)
            {
                tantargyakatlag.Add(tantNevek[i], ertekek[i]);
            }

            return tantargyakatlag;
        }


    }
}
