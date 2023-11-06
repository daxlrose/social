using Microsoft.EntityFrameworkCore;
using Social.Dal;

namespace Social.Api.Registers
{
    public class DbRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(cs);
            });
        }
    }
}
