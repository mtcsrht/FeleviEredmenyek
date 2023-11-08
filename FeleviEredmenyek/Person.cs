using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace FeleviEredmenyek
{
    class Person
    {
        public string Nev { get; set; }
        public string OktAzon { get; set; }
        public List<int> Jegyek { get; set; }

        public double Atlag => Jegyek.Average();

        public double LegjobbJegy => Jegyek.Max();

        public Person(string sor)
        {
            Jegyek = new List<int>();
            var tmp = sor.Split("\t");
            Nev = tmp[0];
            OktAzon = tmp[1];
            for (int i = 2; i < tmp.Length; i++)
            {
                Jegyek.Add(int.Parse(tmp[i]));
            }

        }
        
        public override string ToString()
        {
            string returns = $"Név: {Nev}, Okt az: {OktAzon}, Jegyek:";
            for (int i = 0; i < Jegyek.Count; i++)
            {
                returns += $"{Program.tantNevek[i]}: {Jegyek[i]}"; 
            }
            return returns;
        }
    }
}
