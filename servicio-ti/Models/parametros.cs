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
        public int Id;
        public List<string> centro_ventas;
        public List<string> punto_ventas;
        public List<int> cd_x;
        public List<int> cd_y;
        public List<int> pv_x;
        public List<int> pv_y;
        public int[] carga;
        public int[] centro;

        public parametros() { }
        public string leer()
        { 
            int pc1 = 0;
            int pc2 = 0;
            int com = 0;
            cd_x = new List<int>();
            cd_y = new List<int>();
            pv_x = new List<int>();
            pv_y = new List<int>();
            String linea = "";
            centro_ventas = new List<string>();
            punto_ventas = new List<string>();            
            StreamReader leer;
            if (File.Exists(@"\grafos-ti\parametros.txt")==true){

                leer = new StreamReader(@"\grafos-ti\parametros.txt"); 
            }
            else
            {
                return ("archivo no encontrado");
            }
            while (!leer.EndOfStream)
            {   
                pc1 = 0;
                pc2 = 0;
                com = 0;
                linea= leer.ReadLine();
                if (linea != null)
                {
                    for (int i = 0; i < linea.Count(); i++)
                    {
                        if (linea.Substring(i, 1) == ";")
                        {
                            if (pc1 == 0)
                            {
                                pc1 = i;
                            }
                            else
                            {
                                if (pc2 == 0)
                                {
                                    pc2 = i;
                                }
                                else
                                {
                                    Log(linea + " solo se aceptan hasta 2 ;", File.AppendText(@"/grafos-ti/loge.txt"));
                                    return ("formato de los datos no valido");
                                }
                            }
                        }
                        else
                        {   if (linea.Substring(i, 1) == ",")
                            {
                                if (com == 0)
                                {
                                    com = i;
                                }
                                else
                                {
                                    Log(linea + " mas de una coma en el archivo", File.AppendText(@"/grafos-ti/loge.txt"));
                                    return ("formato de los datos no valido");
                                }
                            }
                        }
                    }
                    if (linea.Substring(0, 1) == "C")
                    {
                        centro_ventas.Add(linea.Substring(pc1+1, pc2 - (pc1 + 1)));
                        cd_x.Add(Int32.Parse((linea.Substring(pc2+1, com - (pc2 + 1)))));
                        cd_y.Add(Int32.Parse(linea.Substring(com+1, linea.Count() - (com + 1))));
                    }
                    else
                    {
                        if (linea.Substring(0, 1) == "P")
                        {
                            punto_ventas.Add(linea.Substring(pc1+1, pc2 - (pc1 + 1)));
                            pv_x.Add(Int32.Parse(linea.Substring(pc2+1, com-(pc2 + 1))));
                            pv_y.Add(Int32.Parse(linea.Substring(com+1, linea.Count()-(com + 1))));
                        }
                        else
                        {
                            Log(linea + " error de identificador P o  C", File.AppendText(@"/grafos-ti/loge.txt"));
                            return ("formato de los datos no valido");
                        }
                    }

                }
            }
            Log("operacion completada", File.AppendText(@"/grafos-ti/log.txt"));
            leer.Close();
            carga = new int[punto_ventas.Count()];
            centro = new int[punto_ventas.Count()];
            if (centro_ventas.Count() != 0)
            {   if (punto_ventas.Count() != 0)
                { 
                    return "parametros agregados"; 
                }
                else
                {
                    return ("archivo no contiene puntos de ventas");
                }
            }
            else
            {
                return ("archivo no contiene centros de distribucion");

            }

        }
        public string[] pvtar() {
            return punto_ventas.ToArray();
        }

        public string[] cdtar()
        {
            return centro_ventas.ToArray();
        }


        public int cargas()
        {
            Log(centro.Count().ToString()+"-"+centro[0], File.AppendText(@"/grafos-ti/loge.txt"));
            int con = 0;
            for(int i = 0; i <centro.Count(); i++)
            {
                if (centro[i] != 0)
                {
                    con += 1;
                    Log(con+ "->" + centro[i], File.AppendText(@"/grafos-ti/loge.txt"));
                }
            }
            Log(con+""+centro[0], File.AppendText(@"/grafos-ti/log.txt"));
            return con;
            
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
            w.Close();
        }

    }
}