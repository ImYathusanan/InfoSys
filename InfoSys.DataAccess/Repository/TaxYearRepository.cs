using InfoSys.DataAccess.Data;
using InfoSys.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public class TaxYearRepository : ITaxYearRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxYearRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            return _context.TaxYears.Select(y => new SelectListItem() 
            {
                Text = y.YearOfTax,
                Value = y.Id.ToString()
            });
        }

        public TaxYear GetTaxYearById(int id)
            => _context.TaxYears.Where(y => y.Id == id).FirstOrDefault();
    }
}
