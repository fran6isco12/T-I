using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace servicio_ti.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            rppar rappar = new rppar();
            parametros npar = new parametros();
            npar.Id = id;
            string mensaje=npar.leer();
            if(mensaje== "parametros agregados"){
                rappar.Agregar(npar);
                return Ok(mensaje);
            }
            else
            {
                return BadRequest(mensaje);
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
                return BadRequest("El archivo de parametros no esta cargado");
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
                return BadRequest("El archivo de parametros no esta cargado");
            }

        }
        [Route("api/values/carga/{idc}")]
        [HttpGet]
        public IHttpActionResult cargas(int idc)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(idc);
            if (parametr != null)
            {
                return Ok(parametr.carga);
            }
            else
            {
                return BadRequest("El archivo de parametros no esta cargado");
            }

        }
        [Route("api/values/agregar/{id}")]
        [HttpPost]

        public IHttpActionResult agregar(datos guia, int id)
        {
            rppar rappar = new rppar();
            var parametr = rappar.obtenerpar(id);
            if (parametr != null)
            {   int pvi= parametr.punto_ventas.IndexOf(guia.pv.ToString());
                Log("agregado"+parametr.carga[pvi]+"-" + parametr.centro[pvi], File.AppendText(@"/grafos-ti/loge.txt"));
                parametr.carga[pvi]=guia.pvp;
                parametr.centro[pvi] = guia.cdd;
                Log("agregado2" + parametr.carga[pvi] + "-" + parametr.centro[pvi], File.AppendText(@"/grafos-ti/loge.txt"));
                return Ok("despacho agregado");
            }
            else
            {
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
                        return Ok(new hoja_ruta().calcular(parametr));
                    }
                    else
                    {
                        return BadRequest("no existen despachos");
                    }
                }
                else
                {
                    return BadRequest("no existen puntos de venta");
                }
            }
            else
            {
                return BadRequest("no existen centros de distribucion");
            }
        }
        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
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
