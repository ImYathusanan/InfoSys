using InfoSys.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly ApplicationDbContext _context;

        private decimal _taxRate;

        private decimal _taxAmount;

        public TaxRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount <= 1042)
            {
                _taxRate = .0m;
                _taxAmount = totalAmount * _taxRate;
            }
            else if(totalAmount > 1042 && totalAmount <= 3125)
            {
                _taxRate = 0.20m;
                _taxAmount = (1042 * .0m) + ((totalAmount - 1042) * _taxRate);
            }
            else if(totalAmount > 3125 && totalAmount <= 12500)
            {
                _taxRate = .40m;
                _taxAmount = (1042 * .0m) 
                             + ((3125 - 1042) * .20m) 
                             + ((totalAmount - 3125) * _taxRate);
            }
            else if(totalAmount > 12500)
            {
                _taxRate = .45m;
                _taxAmount = (1042 * .0m) + ((3125 - 1042) * .20m) 
                             + ((12500 - 3125) * .40m) 
                             + ((totalAmount - 12500) * _taxRate);
            }

            return _taxAmount;
        }
    }
}
