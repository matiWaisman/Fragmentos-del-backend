using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Color
    {
        public String NombreColor { get; set; }
        public List<Talle> StockTalle { get; set; }
    }
}
