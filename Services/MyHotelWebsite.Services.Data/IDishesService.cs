using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Services.Data
{
    public interface IDishesService
    {
        Task<int> GetCountAsync();
    }
}
