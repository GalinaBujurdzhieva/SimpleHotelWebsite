using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Services.Data
{
    internal class StorageService : IStorageService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public StorageService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string AbsolutePath()
        {
            return this.hostingEnvironment.WebRootPath;
        }
    }
}
