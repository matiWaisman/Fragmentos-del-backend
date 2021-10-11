using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string LinkProducto { get; set; }
        public float Precio { get; set; }
        public string Imagen { get; set; }
        public int IdTienda { get; set; }
        public List<Color> ListColores { get; set; }
        



        public static Producto ConstructirAPartirDeLink(string link){
            Producto resultado;
            // Busco cual es la tienda
            Tienda.inicializarTiendas();
            int IndiceTienda = Tienda.obtenerIndiceTienda(link);
            if (IndiceTienda != -1)
            {
                Tienda tiendaEncontrada = Tienda.ListaTiendas[IndiceTienda];
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.FULLH4RD)
                {
                    Scraper.ScrapeFull4hrd(link);
                    resultado = Scraper.listarProducto();
                    resultado.IdTienda = tiendaEncontrada.IdTienda;
                    return resultado;
                }
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.DEXTER)
                {
                    Scraper.ScrapeDexter(link);
                    resultado = Scraper.listarProducto();
                    resultado.IdTienda = tiendaEncontrada.IdTienda;
                    return resultado;
                }
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.MARTENS)
                {
                    Scraper.ScrapeMartens(link);
                    resultado = Scraper.listarProducto();
                    return resultado;
                }
                /*
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.ADIDAS)
                {
                    Scraper.ScrapeAdidas(link);
                    resultado = Scraper.listarProducto();
                    return resultado;
                }
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.FRAVEGA)
                {
                    Scraper.ScrapeFravega(link);
                    resultado = Scraper.listarProducto();
                    return resultado;
                }*/
                if (tiendaEncontrada.TipoDeTienda == TipoTienda.ZARA)
                {
                    Scraper.ScrapeZara(link);
                    resultado = Scraper.listarProducto();
                    resultado.IdTienda = tiendaEncontrada.IdTienda;
                    return resultado;
                }
                else
                {
                    Scraper.devolverProductoNull();
                    resultado = Scraper.listarProducto();
                    return resultado;
                }
            }
            else
            {
                Scraper.devolverProductoNull();
                resultado = Scraper.listarProducto();
                return resultado;
            }
        }
    }
}
