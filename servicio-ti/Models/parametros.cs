using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace servicio_ti
{
    public class parametros
    {
        List<string> centro_ventas;
        List<string> punto_ventas;
        int count_string;

        public string leer(String ubicacion_archivo)
        {
            centro_ventas = new List<string>();
            punto_ventas = new List<string>();
            int count_string = 0;

            StreamReader leer = new StreamReader(@"C:\texto\mi_texto.txt");

            while (!leer.EndOfStream)
            {
                String linea = leer.ReadLine();
                for (int i = 0; i < linea.Length; i++)
                {
                    count_string++;
                }
                string dif;
                dif = linea.Substring(0, 1);
                if (dif == "C")
                    centro_ventas.Add(linea.Substring(2, (count_string - 2)));
                if (dif == "P")
                    punto_ventas.Add(linea.Substring(2, (count_string - 2)));
            }

            Console.WriteLine("El contenido de punto ventas es:");
            foreach (string punto in punto_ventas)
            {
                Console.WriteLine(punto);
            }

            Console.WriteLine("El contenido de centro ventas es:");
            foreach (string centro in centro_ventas)
            {
                Console.WriteLine(centro);
            }

            Console.ReadLine();

            return "a";

        }

    }
}