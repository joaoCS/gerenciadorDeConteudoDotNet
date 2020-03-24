using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_GerenciadorDeConteudo.Startup))]
namespace MVC_GerenciadorDeConteudo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
