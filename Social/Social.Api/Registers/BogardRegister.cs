using Social.Application.UserProfiles.Queries;
using System.Reflection;

namespace Social.Api.Registers
{
    public class BogardRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfiles));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllUserProfiles).GetTypeInfo().Assembly);
            });
        }
    }
}
