using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Web.Http.Cors;
using System.Diagnostics;


namespace WebApplication1.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "GET")]
  
  public class ProductosController : ApiController
  { 
    Scraper scraper;
    public IHttpActionResult ObtenerPorLink(string link)
        {
            Producto resultado;
            bool esURLValida = Tienda.ValidarURL(link);
            if (esURLValida)
            {
                resultado = Producto.ConstructirAPartirDeLink(link);
                if(resultado.Nombre == null)
                {
                    return Content(HttpStatusCode.BadRequest, resultado);
                }
                else
                {
                    return Ok(resultado);
                }
            }
            else
            {
                Scraper.devolverProductoNull();
                resultado = Scraper.listarProducto();
                return Content(HttpStatusCode.BadRequest, resultado);
            }
        }

  }
}
