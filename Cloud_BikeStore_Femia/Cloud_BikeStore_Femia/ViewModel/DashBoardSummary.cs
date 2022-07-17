using Cloud_BikeStore_Femia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_BikeStore_Femia.ViewModel
{
    public class DashBoardSummary
    {
        public List<Fatturato> ListaFatturato { get; set; }
        public List<Product> ProdottiScorta { get; set; }
        public List<Product> ProdottiEsauriti { get; set; }

        public class Fatturato
        {
            public int Anno { get; set; }
            public decimal FatturatoAnnuo { get; set; }

        }
    }
}
