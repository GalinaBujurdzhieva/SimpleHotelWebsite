namespace MyHotelWebsite.Web.ViewModels.Blogs
{
    using AutoMapper;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class SingleBlogViewModel : IMapFrom<Blog>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string BlogImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Blog, SingleBlogViewModel>()
                .ForMember(x => x.BlogImageUrl, opt => opt.MapFrom(b => b.BlogImageUrl.Replace("\r\n", "<br />").Replace("\n", "<br />")));
        }
    }
}
