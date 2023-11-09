using Sitanshu.Blogs.Application.Common.Interfaces;

namespace Sitanshu.Blogs.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
