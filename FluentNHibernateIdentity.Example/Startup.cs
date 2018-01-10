using FluentNHibernateIdentity.Example;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace FluentNHibernateIdentity.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
