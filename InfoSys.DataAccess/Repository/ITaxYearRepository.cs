using InfoSys.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public interface ITaxYearRepository
    {
        IEnumerable<SelectListItem> GetAll();

        TaxYear GetTaxYearById(int id);
    }
}
