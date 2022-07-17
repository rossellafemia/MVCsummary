using Cloud_BikeStore_Femia.Models;
using Cloud_BikeStore_Femia.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Cloud_BikeStore_Femia.ViewModel.DashBoardSummary;

namespace Cloud_BikeStore_Femia.Controllers
{
    public class DashBoard : Controller
    {

        private readonly Cloud_BikeStoresContext _context;

        public DashBoard(Cloud_BikeStoresContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Administrators")]
        public async Task<IActionResult> Index()
        {
            DashBoardSummary dbs = new DashBoardSummary();
            Fatturato f1 = new Fatturato();
            Fatturato f2 = new Fatturato();
            Fatturato f3 = new Fatturato();

            f1.Anno = 2016;
            f1.FatturatoAnnuo = Fatturato(2016);

            f2.Anno = 2017;
            f2.FatturatoAnnuo = Fatturato(2017);

            f3.Anno = 2018;
            f3.FatturatoAnnuo = Fatturato(2018);

            dbs.ListaFatturato = new List<Fatturato>();
            dbs.ListaFatturato.Add(f1);
            dbs.ListaFatturato.Add(f2);
            dbs.ListaFatturato.Add(f3);


            var s = await  _context.Stocks
                .Include(d => d.Product)
                .Include(d => d.Store)
                .Where(d => d.Quantity < 10 && d.Quantity > 0 && d.ProductId == d.Product.ProductId)
                .Select(d => d.Product)
                .ToListAsync();

            dbs.ProdottiScorta = new List<Product>();
            dbs.ProdottiScorta = s;



            var e = await _context.Stocks
                .Include(c => c.Product)
                .Include(c => c.Store)
                .Where(c => c.Quantity == 0 && c.ProductId == c.Product.ProductId)
                .Select(c => c.Product)
                .ToListAsync();

            dbs.ProdottiEsauriti = new List<Product>();
            dbs.ProdottiEsauriti = e;


            return View(dbs);
        }

        public decimal Fatturato(int anno)
        {
            var a = _context.Orders
                .Where(x => x.OrderDate.Year == anno)
                .Include(y => y.OrderItems)
                .Select(z => z.OrderItems);

            decimal somma = 0;
            foreach (var ord in a)
            {
                foreach (var singord in ord)
                {
                    somma += singord.Quantity * singord.ListPrice * singord.Discount;
                }
            }
            return somma;
        }
    }
}
