namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using Xunit;

    using static System.Net.WebRequestMethods;

    public class BlogsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Blog>> blogsRepo;
        private readonly Mock<IDeletableEntityRepository<BlogImage>> blogsImagesRepo;

        public BlogsServiceTests()
        {
            this.blogsRepo = new Mock<IDeletableEntityRepository<Blog>>();
            this.blogsImagesRepo = new Mock<IDeletableEntityRepository<BlogImage>>();
        }

        [Fact]
        public async Task BlogDetailsByIdAsyncShouldWorkCorrectly()
        {
            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title 1",
                    Content = "Test Content 1",
                    BlogImageUrl = "images/blogs/test-1.png",
                },
                new Blog()
                {
                    Id = 2,
                    Title = "Test Title 2",
                    Content = "Test Content 2",
                    BlogImageUrl = "images/blogs/test-2.png",
                },
            };
            var mock = blogs.AsQueryable().BuildMock();

            this.blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var blogsService = new BlogsService(this.blogsRepo.Object, this.blogsImagesRepo.Object);
            var currentBlog = await blogsService.BlogDetailsByIdAsync<SingleBlogViewModel>(1);
            Assert.NotNull(currentBlog);
            Assert.Equal(1, currentBlog.Id);
            Assert.Equal("Test Title 1", currentBlog.Title);
            Assert.Equal("Test Content 1", currentBlog.Content);
        }

        [Fact]
        public async Task DoesBlogExistsAsyncShouldWorkCorrectly()
        {
            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title 1",
                    Content = "Test Content 1",
                    BlogImageUrl = "images/blogs/test-1.png",
                },
                new Blog()
                {
                    Id = 2,
                    Title = "Test Title 2",
                    Content = "Test Content 2",
                    BlogImageUrl = "images/blogs/test-2.png",
                },
            };
            var mock = blogs.AsQueryable().BuildMock();

            this.blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var blogsService = new BlogsService(this.blogsRepo.Object, this.blogsImagesRepo.Object);
            Assert.True(await blogsService.DoesBlogExistsAsync(2));
            Assert.False(await blogsService.DoesBlogExistsAsync(5));
        }

        [Fact]
        public async Task GetAllBlogsAsyncShouldReturnAllBlogs()
        {
            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title 1",
                    Content = "Test Content 1",
                    BlogImageUrl = "images/blogs/test-1.png",
                },
                new Blog()
                {
                    Id = 2,
                    Title = "Test Title 2",
                    Content = "Test Content 2",
                    BlogImageUrl = "images/blogs/test-2.png",
                },

                new Blog()
                {
                    Id = 3,
                    Title = "Test Title 3",
                    Content = "Test Content 3",
                    BlogImageUrl = "images/blogs/test-3.png",
                },

                new Blog()
                {
                    Id = 4,
                    Title = "Test Title 4",
                    Content = "Test Content 4",
                    BlogImageUrl = "images/blogs/test-4.png",
                },
            };
            var mock = blogs.AsQueryable().BuildMock();

            this.blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var blogsService = new BlogsService(this.blogsRepo.Object, this.blogsImagesRepo.Object);
            IEnumerable<SingleBlogViewModel> allBlogsFirstPage = await blogsService.GetAllBlogsAsync<SingleBlogViewModel>(1, 2);
            IEnumerable<SingleBlogViewModel> allBlogsSecondPage = await blogsService.GetAllBlogsAsync<SingleBlogViewModel>(2, 2);
            Assert.NotNull(allBlogsFirstPage);
            Assert.NotNull(allBlogsSecondPage);
            Assert.Equal(2, allBlogsFirstPage.Count());
            Assert.Equal(3, allBlogsSecondPage.First().Id);
        }

        [Fact]
        public async Task GetCountAsyncShouldReturnCorrectNumber()
        {
            var blogs = new List<Blog>
            {
                new Blog(),
                new Blog(),
                new Blog(),
            };
            var mock = blogs.AsQueryable().BuildMock();

            this.blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var blogsService = new BlogsService(this.blogsRepo.Object, this.blogsImagesRepo.Object);
            Assert.Equal(3, await blogsService.GetCountAsync());
        }

        [Fact]
        public async Task GetLastBlogsAsyncShouldReturnCorrectBlogs()
        {
            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title 1",
                    Content = "Test Content 1",
                    BlogImageUrl = "images/blogs/test-1.png",
                },
                new Blog()
                {
                    Id = 2,
                    Title = "Test Title 2",
                    Content = "Test Content 2",
                    BlogImageUrl = "images/blogs/test-2.png",
                },

                new Blog()
                {
                    Id = 3,
                    Title = "Test Title 3",
                    Content = "Test Content 3",
                    BlogImageUrl = "images/blogs/test-3.png",
                },

                new Blog()
                {
                    Id = 4,
                    Title = "Test Title 4",
                    Content = "Test Content 4",
                    BlogImageUrl = "images/blogs/test-4.png",
                },
            };
            var mock = blogs.AsQueryable().BuildMock();

            this.blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var blogsService = new BlogsService(this.blogsRepo.Object, this.blogsImagesRepo.Object);
            IEnumerable<SingleBlogViewModel> lastBlogs = await blogsService.GetLastBlogsAsync<SingleBlogViewModel>(2);
            Assert.NotNull(lastBlogs);
            Assert.Equal(2, lastBlogs.Count());
            Assert.Equal(1, lastBlogs.First().Id);
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task DeleteBlogAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestBlogsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Blog> blogsRepo = new EfDeletableEntityRepository<Blog>(dbContext);
            EfDeletableEntityRepository<BlogImage> blogImagesRepo = new EfDeletableEntityRepository<BlogImage>(dbContext);
            await blogsRepo.AddAsync(
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title",
                    Content = "Test Content",
                    BlogImageUrl = "images/blogs/1.png",
                });
            await blogsRepo.SaveChangesAsync();
            BlogsService blogsService = new BlogsService(blogsRepo, blogImagesRepo);
            await blogsService.DeleteBlogAsync(1);
            int count = await blogsService.GetCountAsync();
            Assert.Equal(0, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task AddBlogAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestBlogsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Blog> blogsRepo = new EfDeletableEntityRepository<Blog>(dbContext);
            EfDeletableEntityRepository<BlogImage> blogImagesRepo = new EfDeletableEntityRepository<BlogImage>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            BlogsService blogsService = new BlogsService(blogsRepo, blogImagesRepo);
            var bytes1 = Encoding.UTF8.GetBytes("This is the first dummy file");
            var bytes2 = Encoding.UTF8.GetBytes("This is the second dummy file");
            IFormFile file1 = new FormFile(new MemoryStream(bytes1), 0, bytes1.Length, "Data", "dummy.txt");
            IFormFile file2 = new FormFile(new MemoryStream(bytes2), 0, bytes2.Length, "Data", "dummy.txt");
            CreateBlogViewModel model1 = new CreateBlogViewModel()
            {
                Title = "Title 1",
                Content = "Content 1",
                BlogImageUrl = "images/blogs/test-1.png",
                Image = file1,
            };
            CreateBlogViewModel model2 = new CreateBlogViewModel()
            {
                Title = "Title 2",
                Content = "Content 2",
                BlogImageUrl = "images/blogs/test-2.png",
                Image = file2,
            };
            await blogsService.AddBlogAsync(model1, "55eddfb6-4d2d-492e-99c8-1c75ca59d673", "dummyImagePath1");
            await blogsService.AddBlogAsync(model2, "b65d8626-ae18-4116-ab72-bd99cb99247c", "dummyImagePath2");
            int count = await blogsService.GetCountAsync();
            Assert.Equal(2, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task EditBlogAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestBlogsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Blog> blogsRepo = new EfDeletableEntityRepository<Blog>(dbContext);
            EfDeletableEntityRepository<BlogImage> blogImagesRepo = new EfDeletableEntityRepository<BlogImage>(dbContext);
            await blogsRepo.AddAsync(
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title 1",
                    Content = "Test Content 1",
                    BlogImageUrl = "images/blogs/1.png",
                    BlogImage = new BlogImage()
                    {
                        BlogId = 1,
                        Extension = "png",
                    },
                });
            await blogsRepo.AddAsync(
                new Blog()
                {
                    Id = 2,
                    Title = "Test Title 2",
                    Content = "Test Content 2",
                    BlogImageUrl = "images/blogs/2.png",
                    BlogImage = new BlogImage()
                    {
                        BlogId = 2,
                        Extension = "png",
                    },
                });
            await blogsRepo.SaveChangesAsync();
            await blogImagesRepo.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            BlogsService blogsService = new BlogsService(blogsRepo, blogImagesRepo);
            var bytes2 = Encoding.UTF8.GetBytes("This is the second dummy file");
            IFormFile file2 = new FormFile(new MemoryStream(bytes2), 0, bytes2.Length, "Data", "dummy.txt");
            EditBlogViewModel model2 = new EditBlogViewModel()
            {
                Title = "Test Title 2 - EDITED",
                Content = "Test Content 2 - EDITED",
                BlogImageUrl = "images/blogs/test-2.png",
            };
            await blogsService.EditBlogAsync(model2, 2, "b65d8626-ae18-4116-ab72-bd99cb99247c", "dummyImagePath2", null);
            var secondBlog = await blogsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal("Test Title 2 - EDITED", secondBlog.Title);
            Assert.Equal("Test Content 2 - EDITED", secondBlog.Content);
            Assert.Equal("b65d8626-ae18-4116-ab72-bd99cb99247c", secondBlog.ApplicationUserId);
            dbContext.Dispose();
        }
    }
}
