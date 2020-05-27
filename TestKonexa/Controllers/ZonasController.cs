using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestKonexa.Context;
using TestKonexa.Models;

namespace TestKonexa.Controllers
{
    public class ZonasController : Controller
    {
        private readonly AplicationDbContext _context;

        public ZonasController(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var aplicationDbContext = _context.Zona.Include(z => z.ResponsableZonal);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: ZonaCrears
        public async Task<IActionResult> CrearZona()
        {

            ViewBag.ResponsableZonal = new SelectList(_context.ResponsableZonal, "Id", "Nombre");
            ViewBag.sucursalDisponible = new SelectList((from s in _context.Sucursal
                                                         where !_context.ZonaSucursal.Any(x => x.IdSucursal == s.Id)
                                                         select s), "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZonaCrear zona)
        {
            if (ModelState.IsValid)
            {
                Zona zonaAdd = new Zona
                {
                    Nombre = zona.Nombre,
                    NombreCorto = zona.NombreCorto,
                    Codigo = zona.Codigo,
                    Observacion = zona.Observacion,
                    IdResponsableZonal = zona.ResponsableZonal,
                    IdSucursalCabecera= zona.Cabecera
                   
                };

                _context.Zona.Add(zonaAdd);
                await _context.SaveChangesAsync();

                List<ZonaSucursal> zonaSucursal = new List<ZonaSucursal>();

                foreach (var item in zona.SucursalesSeleccionada)
                {
                   

                    zonaSucursal.Add(new ZonaSucursal { IdSucursal = item, IdZona = zonaAdd.Id });
                }

                _context.ZonaSucursal.AddRange(zonaSucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdResponsableZonal"] = new SelectList(_context.ResponsableZonal, "Id", "Nombre", zona.ResponsableZonal);
            return View(zona);
        }

        // GET: Zonas/Edit/5
        public async Task<IActionResult> EditarZona(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zona.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }

            ZonaCrear zonaEditar = new ZonaCrear
            {
                Id = zona.Id,
                Nombre = zona.Nombre,
                NombreCorto = zona.NombreCorto,
                Codigo = zona.Codigo,
                Observacion = zona.Observacion,
                Cabecera = zona.IdSucursalCabecera,
                DesCabecera = _context.Sucursal.FirstOrDefault(x => x.Id == zona.IdSucursalCabecera).Nombre,
                ResponsableZonal = zona.IdResponsableZonal
            };

            var dataResponsable = new SelectList(_context.ResponsableZonal, "Id", "Nombre");

            ViewBag.ResponsableZonal = dataResponsable;

            ViewBag.sucursalDisponible = new SelectList((from s in _context.Sucursal
                                                         where !_context.ZonaSucursal.Any(x => x.IdSucursal == s.Id)
                                                         select s), "Id", "Nombre");

            ViewBag.sucursalDisponible = new SelectList((from s in _context.Sucursal
                                                         where !_context.ZonaSucursal.Any(x => x.IdSucursal == s.Id)
                                                         select s), "Id", "Nombre");

            var data = (from s in _context.Sucursal
                        join zs in _context.ZonaSucursal on s.Id equals zs.IdSucursal
                       where zs.IdZona == id
                       select s).ToList();

            ViewBag.sucursalesSeleccionada = new SelectList(data, "Id", "Nombre");            

            return View(zonaEditar);
        }

        // POST: Zonas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZonaCrear zona)
        {
            if (id != zona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Zona zonaAdd = new Zona
                    {
                        Id = zona.Id,
                        Nombre = zona.Nombre,
                        NombreCorto = zona.NombreCorto,
                        Codigo = zona.Codigo,
                        Observacion = zona.Observacion,
                        IdResponsableZonal = zona.ResponsableZonal,
                        IdSucursalCabecera = zona.Cabecera
                    };


                    List<ZonaSucursal> zonaSucursal = new List<ZonaSucursal>();

                    foreach (var item in zona.SucursalesSeleccionada)
                    {
                        zonaSucursal.Add(new ZonaSucursal { IdSucursal = item, IdZona = zonaAdd.Id });
                    }
                    var dataBorrar = (_context.ZonaSucursal.Where(x => x.IdZona == id).ToList());
                    _context.ZonaSucursal.RemoveRange(dataBorrar);
                    await _context.SaveChangesAsync();

                    _context.Zona.UpdateRange(zonaAdd);
                    _context.ZonaSucursal.AddRange(zonaSucursal);                   
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonaExists(zona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdResponsableZonal"] = new SelectList(_context.ResponsableZonal, "Id", "Nombre", zona.ResponsableZonal);
            return View(zona);
        }

        private bool ZonaExists(int id)
        {
            return _context.Zona.Any(e => e.Id == id);
        }

        // GET: Zonas1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zona
                .Include(z => z.ResponsableZonal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // POST: Zonas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zona = await _context.Zona.FindAsync(id);
            _context.Zona.Remove(zona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Zonas/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zona.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }

            ZonaCrear zonaEditar = new ZonaCrear
            {
                Id = zona.Id,
                Nombre = zona.Nombre,
                NombreCorto = zona.NombreCorto,
                Codigo = zona.Codigo,
                Observacion = zona.Observacion,
                Cabecera = zona.IdSucursalCabecera,
                DesCabecera = _context.Sucursal.FirstOrDefault(x => x.Id == zona.IdSucursalCabecera).Nombre
            };

            ViewBag.ResponsableZonal = new SelectList(_context.ResponsableZonal.Where(x=> x.Id == zona.IdResponsableZonal), "Id", "Nombre");
            ViewBag.sucursalDisponible = new SelectList((from s in _context.Sucursal
                                                         where !_context.ZonaSucursal.Any(x => x.IdSucursal == s.Id)
                                                         select s), "Id", "Nombre");

            ViewBag.sucursalDisponible = new SelectList((from s in _context.Sucursal
                                                         where !_context.ZonaSucursal.Any(x => x.IdSucursal == s.Id)
                                                         select s), "Id", "Nombre");

            var data = (from s in _context.Sucursal
                        join zs in _context.ZonaSucursal on s.Id equals zs.IdSucursal
                        where zs.IdZona == id
                        select s).ToList();

            ViewBag.sucursalesSeleccionada = new SelectList(data, "Id", "Nombre");

            return View(zonaEditar);
        }

    }
}
