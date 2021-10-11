using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using HtmlAgilityPack;

namespace WebApplication1.Models
{
	public class Scraper
	{
		public static Producto ProductoScrapeado
        {  
            get
            {   
                return _ProductoScrapeado;
            }
            private set
            {
                _ProductoScrapeado = value;
            }
        }
		
		public static Producto listarProducto()
        {
            return _ProductoScrapeado;
        }
		
		public static float cortarPrecio(string precioScrapeado)
        {
            string precioFinal = "";
            string precioAux = "";
            float precioF;
            string [] palabras = precioScrapeado.Split(' ', '$');
            int i = 0;
            bool encontreAlPrecio = false;
            int contadorComas = 0;
            while(i < palabras.Length && encontreAlPrecio == false)
            {
                bool containsInt = palabras[i].Any(char.IsDigit);
                if (containsInt == true)
                {
                    precioAux = palabras[i];
                    encontreAlPrecio = true;
                }
                i++;
            }
            bool tieneSimbolo = precioAux.Contains('$');
            if (tieneSimbolo == true)
            {
                string[] precioSinSimbolo = precioAux.Split('$');
                try
                {
                    precioFinal = precioSinSimbolo[1];
                }
                catch (IndexOutOfRangeException)
                {
                    precioFinal = precioSinSimbolo[0];
                }
            }
            string[] precio = precioAux.Split(',');
            precioFinal = precio[0].Replace(",", ".");
            precioF = float.Parse(precioFinal);
            return precioF;
        }
		
		public static String obtenerImagen(HtmlDocument doc, string posicionDeLaImagen, string link)
        {
            String linkImagen = "";
            var imgContainer = doc.DocumentNode.SelectSingleNode(posicionDeLaImagen);
            var img = imgContainer.Attributes["src"].Value;
            linkImagen += link + img;
            return linkImagen;
        }
		
		public static void ScrapeDexter(string link)
        {
            //declaro variables
            string linkImagenes = "";
            string precioAuxiliar;
            Producto producto = new Producto();
            var web = new HtmlWeb();
            var doc = web.Load(link);
            HtmlNode nodeInformacion = doc.DocumentNode.SelectSingleNode("//div[@class = 'col-12 col-sm-6 product-basic-information']");
            string productoSize;
            string sizeAux;

            //consigo nombre y precio
            producto.Nombre = HttpUtility.HtmlDecode(nodeInformacion.SelectSingleNode("//h1[@class = 'product-name']").InnerText); 
            precioAuxiliar = HttpUtility.HtmlDecode(nodeInformacion.SelectSingleNode("//span[@class = 'value']").InnerText);
            producto.Precio = cortarPrecio(precioAuxiliar);

            //consigo imagen
            producto.Imagen = obtenerImagen(doc, "//img[@id = 'img-0']", "https://www.dexter.com.ar");

            //consigo array stocks
            HtmlNodeCollection sizesDisabled = doc.DocumentNode.SelectNodes("//li[@class = 'variation-attribute-size disabled ']");
            HtmlNodeCollection sizesEnabled = doc.DocumentNode.SelectNodes("//li[@class = 'variation-attribute-size  ']");
            producto.ListColores = new List<Color>();
            List<Talle> disabled = obtenerArrayStock(doc, sizesDisabled, false);
            List<Talle> enabled = obtenerArrayStock(doc, sizesEnabled, true);
            var allProducts = new List<Talle>(disabled.Count + enabled.Count);
            allProducts.AddRange(disabled);
            allProducts.AddRange(enabled);
            Color colorTemporal = new Color();
            colorTemporal.NombreColor = producto.Nombre;
            colorTemporal.StockTalle = allProducts;
            producto.ListColores.Add(colorTemporal);
            _ProductoScrapeado = producto;
        }
	}
}
