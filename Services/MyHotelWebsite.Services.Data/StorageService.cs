namespace MyHotelWebsite.Services.Data
{
    using Microsoft.AspNetCore.Hosting;

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
