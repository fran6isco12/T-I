using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web.Http.Cors;

namespace servicio_ti.Controllers
{
    [EnableCors(origins: "http://localhost:3030", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            rppar rappar = new rppar();
            parametros npar = new parametros();
            npar.Id = id;
            string mensaje = npar.leer();
            if (mensaje == "parametros agregados")
            {
                rappar.eliminar();
                rappar.Agregar(npar);
                Log(mensaje, "getparametros", File.AppendText(@"/grafos-ti/tmp/log.txt"));
                return Ok(mensaje);
            }
            else
            {
                Log(mensaje,"getparametros", File.AppendText(@"/grafos-ti/tmp/loge.txt"));
                return BadRequest();
            }
        }
        [Route("api/values/centros/{idc}")]
        [HttpGet]
        public IHttpActionResult centrosg(int idc)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(idc);
            if (parametr != null)
            {
                
                return Ok(parametr.cdtar());
                
            }
            else
            {
                
                return BadRequest();
            }

        }
        [Route("api/values/puntos/{idc}")]
        [HttpGet]
        public IHttpActionResult puntosgt(int idc)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(idc);
            if (parametr != null)
            {   
                
                return Ok(parametr.pvtar());
            }
            else
            {                
                return BadRequest();
            }

        }


        [Route("api/values/agregar/{id}")]
        [HttpPost]

        public IHttpActionResult agregar(datos guia, int id)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(id);
            if (parametr != null)
            {
                int pvi = parametr.punto_ventas.IndexOf(guia.pv.ToString());
                Log("agregado" + parametr.carga[pvi] + "-" + parametr.centro[pvi], "post agregar despacho", File.AppendText(@"/grafos-ti/tmp/log.txt"));
                parametr.carga[pvi] = guia.pvp;
                parametr.centro[pvi] = guia.cdd;
                return Ok("despacho agregado");
            }
            else
            {
                Log("parametros no encontrado", "post agregar despacho", File.AppendText(@"/grafos-ti/tmp/loge.txt"));
                return BadRequest();
            }
        }
        [Route("api/values/hojaruta/{id}")]
        [HttpGet]
        public IHttpActionResult hojaruta(int id)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(id);
            if (parametr.centro_ventas.Count() != 0)
            {
                if (parametr.punto_ventas.Count() != 0)
                {
                    if (parametr.cargas() != 0)
                    {
                        Log("hoja de ruta generada", "gethoja ruta", File.AppendText(@"/grafos-ti/tmp/log.txt"));
                        return Ok(new hoja_ruta().calcular(parametr));
                    }
                    else
                    {
                        Log("hoja de no ruta generada no existen despachos", "gethoja ruta", File.AppendText(@"/grafos-ti/tmp/loge.txt"));
                        return BadRequest();
                    }
                }
                else
                {
                    Log("hoja de no ruta generada no existen puntos de venta", "gethoja ruta", File.AppendText(@"/grafos-ti/tmp/loge.txt"));
                    return BadRequest();
                }
            }
            else
            {
                Log("hoja de no ruta generada no existen centros de distribucion", "gethoja ruta", File.AppendText(@"/grafos-ti/tmp/loge.txt"));
                return BadRequest();
            }
        }

        public static void Log(string logMessage, string function, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("funcion: " + function);
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
            w.Close();
        }
    }
}
