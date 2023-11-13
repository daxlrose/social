using Social.Application.Services;

namespace Social.Api.Registers
{
    public class ApplicationLayerRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
