using InfoSys.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public interface ITaxRepository
    {
        decimal TaxAmount(decimal totalAmount);

    }
}
