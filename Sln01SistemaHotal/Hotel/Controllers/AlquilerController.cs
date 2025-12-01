using Hotel.Data;
using Hotel.Filtro;
using Hotel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Controllers
{
    [AutenticacionFilter]
    public class AlquilerController : Controller
    {
        private readonly HotelContext _context;

        public AlquilerController(HotelContext context)
        {
            _context = context;
        }

        // GET: Alquiler
        public async Task<IActionResult> Index(DateTime? fecha1, DateTime? fecha2)
        {
            var fechaActual = DateTime.Now.Date;
            
            if (!fecha2.HasValue)
            {
                fecha2 = fechaActual;
            }
            if (!fecha1.HasValue)
            {
                fecha1 = fecha2;
            }

            if (fecha1 > fecha2 || fecha1 > fechaActual)
            {
                ModelState.AddModelError(string.Empty, "La fecha inicial no puede ser mayor que la fecha final ni mayor que la fecha actual.");
                return View(new List<ListaAlquilerFrontEnd>());
            }
            var alquiler =  from a in _context.Alquiler
                                  join h in _context.Habitacion on a.idHab equals h.Id
                                  join c in _context.Cliente on a.idCli equals c.id
                                  join t in _context.TipoHab on h.TipoId equals t.id
                                  join td in _context.TipoDoc on c.tipoId equals td.id
                                  where a.fechaActual >= fecha1 && a.fechaActual <= fecha2
                                  select new ListaAlquilerFrontEnd
                                  {
                                      id = a.id,
                                      numHab = h.Numero,
                                      nombre = c.nombre + " " + c.apellido,
                                      documento=td.tipo+" "+ c.numeroDoc,
                                      dias = a.dias,
                                      precio = t.precio,
                                      fechaActual = a.fechaActual,
                                      fechaEntrada = a.fechaEntrada,
                                      fechaSalida = a.fechaSalida,
                                      total = a.total,
                                  };
            ViewBag.Fecha1 = fecha1?.ToString("yyyy-MM-dd");
            ViewBag.Fecha2 = fecha2?.ToString("yyyy-MM-dd");
            return View(await alquiler.ToListAsync());
        }
        public async Task<IActionResult> ExportarPDF(DateTime? fecha1, DateTime? fecha2)
        {

            var alquiler = from a in _context.Alquiler
                           join h in _context.Habitacion on a.idHab equals h.Id
                           join c in _context.Cliente on a.idCli equals c.id
                           join t in _context.TipoHab on h.TipoId equals t.id
                           join td in _context.TipoDoc on c.tipoId equals td.id
                           where a.fechaActual >= fecha1 && a.fechaActual <= fecha2
                           select new ListaAlquilerFrontEnd
                           {
                               id = a.id,
                               numHab = h.Numero,
                               nombre = c.nombre + " " + c.apellido,
                               documento = td.tipo + " " + c.numeroDoc,
                               dias = a.dias,
                               precio = t.precio,
                               fechaActual = a.fechaActual,
                               fechaEntrada = a.fechaEntrada,
                               fechaSalida = a.fechaSalida,
                               total = a.total,
                           };

            return new ViewAsPdf("ExportarPDF", await alquiler.ToListAsync())
            {
                FileName = "Reporte_Alquileres.pdf",
            };
        }



    }
}
