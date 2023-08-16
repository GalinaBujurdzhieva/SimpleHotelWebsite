namespace MyHotelWebsite.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using Xunit;

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

        // DO NOT PASS
        [Fact]
        public async Task DeleteBlogAsyncShouldWorkCorrectly()
        {
            var blogsRepo = new Mock<IDeletableEntityRepository<Blog>>();
            var blogsImagesRepo = new Mock<IDeletableEntityRepository<BlogImage>>();

            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test Title",
                    Content = "Test Content",
                    BlogImageUrl = "images/blogs/1.png",
                },
            };
            var mock = blogs.AsQueryable().BuildMock() /*.AsAsyncEnumerable()*/;

            blogsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var blogsService = new BlogsService(blogsRepo.Object, blogsImagesRepo.Object);
            await blogsService.DeleteBlogAsync(1);
            int count = await blogsService.GetCountAsync();
            Assert.Equal(0, count);
        }
    }
}
