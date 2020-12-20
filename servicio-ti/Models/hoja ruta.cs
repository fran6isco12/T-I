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
            List<int> array2;
            int con = 1;            
            int cona = 0;
            int centroac;
            int camion = 1000;
            string ruta = "Camion";
            int dist;
            int xac = 0;
            int yac = 0;
            StreamWriter w2;
            string path = @"\grafos-ti\hojaruta(" + DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss") +  ").txt";           
            File.Create(path).Close();
            
            w2 = File.AppendText(path);
            w2.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w2.Close();
            Log("archivo creado en" + path, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
            while (cona < a.centro_ventas.Count())
            {
                array = new List<int>();
                centroac = Int32.Parse(a.centro_ventas[cona]);
                if (a.centro.Contains(centroac) == true)
                {
                    Log("creando las rutas del cento" + centroac, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                    for (int i = 0; i < a.centro.Count(); i++)
                    {
                        if (a.centro[i] == centroac)
                        {
                            array.Add(i);
                        }

                    }
                    while (array.Count() != 0)
                    {
                        array2 = new List<int>();
                        camion = 1000;
                        xac = a.cd_x[cona];
                        yac = a.cd_y[cona];
                        ruta = ruta + con + ": estacionamiento(0,0)=>centro de distribucion " + centroac + "(" + a.cd_x[cona] + "," + a.cd_y[cona] + ")";
                        if (array.Count > 1)
                        {
                            dist = distancia(a, array, xac, yac);
                            xac = a.pv_x[array[dist]];
                            yac = a.pv_y[array[dist]];
                            Log("agregado el punto" + a.punto_ventas[array[dist]] + " al camion" + con, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                            ruta = ruta + "=>puntodeventa" + a.punto_ventas[array[dist]] + "(" + xac + "," + yac + ")(" + a.carga[array[dist]] + ")";
                            camion = camion - a.carga[array[dist]];
                            array.Remove(array[dist]);
                            array2.AddRange(array.ToArray());
                            while (camion != 0)
                            {

                                if (array2.Count() > 1)
                                {

                                    if (a.carga[array2[distancia(a, array2, xac, yac)]] > 0)
                                    {
                                        if (camion >= a.carga[array[dist]])
                                        {

                                            dist = distancia(a, array2, xac, yac);
                                            xac = a.pv_x[array2[dist]];
                                            yac = a.pv_y[array2[dist]];
                                            Log("agregado el punto" + a.punto_ventas[array2[dist]]+" al camion"+con, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                                            ruta = ruta + "=>puntodeventa" + a.punto_ventas[array2[dist]] + "(" + xac + "," + yac + ")(" + a.carga[array2[dist]] + ")";
                                            camion = camion - a.carga[array2[dist]];
                                            array.Remove(array[dist]);
                                            array2.Remove(array2[dist]);
                                        }
                                        else
                                        {
                                            array2.Remove(array2[dist]);
                                        }
                                    }

                                }
                                else
                                {
                                    if (array2.Count() == 1)
                                    {
                                        if (a.carga[array2[0]] > 0)
                                        {
                                            if (camion >= a.carga[array2[0]])
                                            {
                                                Log("agregado el punto" + a.punto_ventas[array2[0]] + " al camion" + con, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                                                ruta = ruta + "=>puntodeventa" + a.punto_ventas[array2[0]] + "(" + a.pv_x[array2[0]] + "," + a.pv_y[array2[0]] + ")(" + a.carga[array2[0]] + ")";
                                                camion = 0;
                                                array.Remove(array[0]);
                                                array2.Remove(array2[0]);
                                            }
                                            else
                                            {

                                                camion = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (camion >= a.carga[array[0]])
                            {
                                Log("agregado el punto" + a.punto_ventas[array[0]] + " al camion" + con, "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                                ruta = ruta + "=>puntodeventa" + a.punto_ventas[array[0]] + "(" + a.pv_x[array[0]] + "," + a.pv_y[array[0]] + ")(" + a.carga[array[0]] + ")";
                                camion = 0;
                                array.Remove(array[0]);                                
                            }
                        }


                        Log("agregando linea al archivo" , "calcular", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
                        logruta(ruta, File.AppendText(path));
                        con += 1;
                        ruta = "Camion";
                    }


                }
                cona += 1;
            }
            return ("Hoja de ruta creada:" + path);
        }
     
        
        public int distancia(parametros ab,List<int> vert, int x,int y)
        {
            Log("calculando distancia mas corta", "distancia", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
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
            Log("retornando indice de distancia mas corta "+resp, "distancia", File.AppendText(@"/grafos-ti/tmp/log(hojaruta).txt"));
            return resp;
        }
        public void logruta(string tx, TextWriter w)
        {
            w.WriteLine("\n" + tx);
            w.Close();
        }
        public static void Log(string logMessage,string function, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("funcion  : "+function);
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
            w.Close();
        }
    }

}