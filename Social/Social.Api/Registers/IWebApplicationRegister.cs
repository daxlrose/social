namespace Social.Api.Registers
{
    public interface IWebApplicationRegister : IRegister
    {
        void RegisterPipelineComponents(WebApplication app);
    }
}
