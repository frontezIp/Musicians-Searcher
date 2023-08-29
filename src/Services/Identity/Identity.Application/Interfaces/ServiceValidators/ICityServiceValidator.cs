using Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.ServiceValidators
{
    public interface ICityServiceValidator
    {
        Task<City> CheckIfCityExistsAndGetAsync(Guid cityId, bool trackChanges);
    }
}
