using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace servicio_ti
{
    public class rppar
    {
        public static List<parametros> _listparm = new List<parametros>();

        public parametros obtenerpar(int id)
        {
            var parr= _listparm.Where(parametros => parametros.Id == id);
            return parr.FirstOrDefault();
        }
        public void Agregar(parametros newpar)
        {
            if (newpar != null)
            {
                _listparm.Add(newpar);
            }
        }
        public void eliminar()
        {
            _listparm.Clear();
        }
    }
}