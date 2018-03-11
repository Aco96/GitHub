using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak2
{
    class Program
    {
        static void Main(string[] args)
        {
            Naocare n1 = new Naocare("rey-ban", 10000);
            Naocare n2 = new Naocare("bvlgari", 20000);
            Sat s1 = new Sat("armany", 100000);
            Sat s2 = new Sat("g-shock", 30000);

            

            Klijent k = new Klijent("Pera","Peric");

            k.kreirajPorudzbinu();
            k.m_Porudzbina[0].dodajStavku(new Stavka(s1, 3));
            k.m_Porudzbina[0].dodajStavku(new Stavka(n1, 4));

            foreach(Stavka s in k.m_Porudzbina[0].m_Stavka)
            {
                Console.WriteLine("Klijent " + k.Ime + " " + k.Prezime + " je porucio " + s.m_Proizvod.ToString() + "\nkolicina:" + s.Kolicina);
            }
            
        }
    }
}
