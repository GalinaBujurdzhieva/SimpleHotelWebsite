namespace MyHotelWebsite.Web.ViewModels.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Migrations;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class SingleBlogViewModel : IMapFrom<Blog>
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string BlogImageUrl { get; set; }
    }
}
