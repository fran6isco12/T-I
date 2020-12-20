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
                Log("archivo encontrado", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
                leer = new StreamReader(@"\grafos-ti\parametros.txt"); 
            }
            else
            {
                Log("archivo no encontrado", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
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
                                    Log(linea + " solo se aceptan hasta 2 ;", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
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
                                    Log(linea + " mas de una coma en el archivo", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                                    return ("formato de los datos no valido");
                                }
                            }
                        }
                    }
                    if (linea.Substring(0, 1) == "C")
                    {
                        if (number(linea.Substring(pc1 + 1, pc2 - (pc1 + 1))) == true)
                        {
                            centro_ventas.Add(linea.Substring(pc1 + 1, pc2 - (pc1 + 1)));
                        }
                        else
                        {
                            Log(linea + " mas de una coma en el archivo", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                            return ("formato de los datos no valido");
                        }
                        if(number(linea.Substring(pc2 + 1, com - (pc2 + 1))) == true)
                        {
                            cd_x.Add(Int32.Parse((linea.Substring(pc2 + 1, com - (pc2 + 1)))));
                        }
                        else
                        {
                            Log(linea + " mas de una coma en el archivo", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                            return ("formato de los datos no valido");
                        }
                        if (number(linea.Substring(com + 1, linea.Count() - (com + 1))) == true)
                        {
                            cd_y.Add(Int32.Parse(linea.Substring(com + 1, linea.Count() - (com + 1))));
                        }
                        else
                        {
                            Log(linea + " mas de una coma en el archivo", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                            return ("formato de los datos no valido");
                        }
                    }
                    else
                    {
                        if (linea.Substring(0, 1) == "P")
                        {   if (number(linea.Substring(pc1 + 1, pc2 - (pc1 + 1))) == true)
                            {
                                punto_ventas.Add(linea.Substring(pc1 + 1, pc2 - (pc1 + 1)));
                            }
                            else
                            {
                                Log(linea + " mas de una coma en el archivo", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                                return ("formato de los datos no valido");
                            }
                            if (number(linea.Substring(pc2 + 1, com - (pc2 + 1))) == true)
                            {
                                pv_x.Add(Int32.Parse(linea.Substring(pc2 + 1, com - (pc2 + 1))));
                            }
                            else
                            {
                                Log(linea + "formato de los datos no valido ", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                                return ("formato de los datos no valido");
                            }
                            if (number(linea.Substring(com + 1, linea.Count() - (com + 1))) == true)
                            {
                                pv_y.Add(Int32.Parse(linea.Substring(com + 1, linea.Count() - (com + 1))));
                            }
                            else
                            {
                                Log(linea + " mas de una coma en el archivo formato de los datos no valido", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                                return ("formato de los datos no valido");
                            }
                        }
                        else
                        {
                            Log(linea + " error de identificador P o  C formato de los datos no valido", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                            return ("formato de los datos no valido");
                        }
                    }

                }
            }
            
            leer.Close();
            carga = new int[punto_ventas.Count()];
            centro = new int[punto_ventas.Count()];
            if (centro_ventas.Count() != 0)
            {   if (punto_ventas.Count() != 0)
                {
                    Log("operacion completada parametros agregados", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
                    return "parametros agregados";
                    
                }
                else
                {
                    Log("operacion completada archivo no contiene puntos de ventas", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                    return ("archivo no contiene puntos de ventas");
                }
            }
            else
            {
                Log("operacion completada archivo no contiene centros de distribucion", "leyendo archivo", File.AppendText(@"/grafos-ti/tmp/loge(parametros).txt"));
                return ("archivo no contiene centros de distribucion");

            }

        }
        private bool number(string a)
        { int num=new int();
          return Int32.TryParse(a, out num);
        }
        public string[] pvtar() {
            Log("puntos:" + punto_ventas.ToArray().ToString(), "retornando puntos", File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
            return punto_ventas.ToArray();
        }

        public string[] cdtar()
        {
            Log("centros:" + centro_ventas.ToArray().ToString(), "retornando centros", File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
            return centro_ventas.ToArray();
        }


        public int cargas()
        {
            int con = 0;
            for(int i = 0; i <centro.Count(); i++)
            {
                if (centro[i] != 0)
                {
                    con += 1;
                }
            }
            Log("cargas:"+carga.ToArray().ToString(), "retornando cargas",File.AppendText(@"/grafos-ti/tmp/log(parametros).txt"));
            return con;
            
        }
        public static void Log(string logMessage,string func, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("funcion  :"+func);
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
            w.Close();
        }

    }
}