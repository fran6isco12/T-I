using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace servicio_ti
{
    public class hoja_ruta
    {
        public hoja_ruta() { }

        public string calcular(parametros a)
        {
            List<int> array;
            int con = 1;            
            int cona = 0;
            int centroac;
            int camion = 1000;
            string ruta = "Camion";
            int dist;
            int xac = 0;
            int yac = 0;
            List<int> array2;
            string path = @"\grafos-ti\hojaruta(" + $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}" + ".txt";
            File.Create(path);
            StreamWriter w = File.AppendText(path);
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.Close();
            while (cona < a.centro_ventas.Count())
            {
                array = new List<int>();
                centroac = Int32.Parse(a.centro_ventas[cona]);
                if (a.centro.Contains(centroac) == true)
                {
                    for (int i = 0; i < a.centro.Count(); i++)
                    {
                        if (a.centro[i] == centroac)
                        {
                            array.Add(i);
                        }

                    }
                    while (array.Count() != 0)
                    {

                        camion = 1000;
                        xac = a.cd_x[cona];
                        yac = a.cd_y[cona];
                        ruta = ruta + con + ": estacionamiento(0,0)=>centro de distribucion " + centroac + "(" + a.cd_x[cona] + "," + a.cd_y[cona] + ")" + "=>";
                        dist = distancia(a, array, xac, yac);
                        xac = a.pv_x[array[dist]];
                        yac = a.pv_y[array[dist]];
                        ruta = ruta + "=>puntodeventa" + a.punto_ventas[array[dist]] + "(" + xac + "," + yac + ")(" + a.carga[array[dist]] + ")";
                        camion = camion - a.carga[dist];
                        array2 = array;
                        array2.Remove(array[dist]);
                        array.Remove(array[dist]);
                        while (camion != 0)
                        {
                            while (array2.Count() > 0)
                            {
                                dist = distancia(a, array2, xac, yac);
                                if (a.carga[array2[dist]] > 0 && camion >= a.carga[array2[dist]])
                                {
                                    xac = a.pv_x[array2[dist]];
                                    yac = a.pv_y[array2[dist]];
                                    ruta = ruta + "=>puntodeventa" + a.punto_ventas[array2[dist]] + "(" + xac + "," + yac + ")(" + a.carga[array2[dist]] + ")";
                                    camion = camion - a.carga[dist];
                                    array2.Remove(array2[dist]);
                                    array.Remove(array2[dist]);
                                }
                                else
                                {
                                    if (array2.Count() == 1)
                                    {
                                        if (a.carga[array2[0]] > 0 && camion >= a.carga[array2[0]])
                                        {
                                            ruta = ruta + "puntodeventa" + a.punto_ventas[array2[0]] + "(" + a.pv_x[array2[0]] + "," + a.pv_y[array2[0]] + ")(" + a.carga[array2[0]] + ")";
                                            camion = 0;
                                            array2.Remove(array2[0]);
                                            array.Remove(array2[0]);
                                        }
                                        else
                                        {
                                            array2.Clear();
                                            camion = 0;
                                        }
                                    }
                                    else
                                    {
                                        array2.Remove(array2[dist]);
                                    }
                                }
                            }
                            logruta(ruta, File.AppendText(path));
                            con += 1;
                            ruta = "camion";

                        }


                    }


                }
                cona += 1;
            }
            return ("Hoja de ruta creada:" + path);
        }
     
        
        public int distancia(parametros ab,List<int> vert, int x,int y)
        {
            int x2;
            int y2;
            int resp=-1;
            double dist=0;
            for(int i=0; i < vert.Count(); i++)
            {
                x2 = ab.pv_x[vert[i]];
                y2 = ab.pv_y[vert[i]];

                if (i == 0)
                {
                    dist = Math.Sqrt((Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2)));
                    resp = i;
                }
                else
                {
                    if(dist> Math.Sqrt((Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2))))
                    {
                        dist= Math.Sqrt((Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2)));
                        resp = i;
                    }
                }
            }
            return resp;
        }
        public void logruta(string tx, TextWriter w)
        {
            w.WriteLine("/n" + tx);
        }
    }

}