using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using FluentNHibernate.AspNet.Identity.Entities;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Snork.FluentNHibernateTools;

namespace FluentNHibernate.AspNet.Identity.Repositories
{
    public abstract class RepositoryBase
    {
        private static readonly object Mutex = new object();

        private static readonly Dictionary<string, ISessionFactory> _sessionFactories =
            new Dictionary<string, ISessionFactory>();


        private bool _testBuild;

        protected internal RepositoryBase(string sessionFactoryKey)
        {
            SessionFactoryKey = sessionFactoryKey;
        }

        protected RepositoryBase(ProviderTypeEnum providerType, string nameOrConnectionString,
            FluentNHibernatePersistenceBuilderOptions options)
        {
            var k = new {providerType, nameOrConnectionString, options};
            SessionFactoryKey = new JavaScriptSerializer().Serialize(k);
            lock (Mutex)
            {
                if (!_sessionFactories.ContainsKey(SessionFactoryKey))
                {
                    var configurationInfo = FluentNHibernatePersistenceBuilder.Build(providerType,
                        nameOrConnectionString, options);

                    var mappings = GetMappings();
                    var fluentConfiguration = Fluently.Configure()
                        .Mappings(mappings)
                        .Database(configurationInfo.PersistenceConfigurer);
                    fluentConfiguration.ExposeConfiguration(cfg =>
                    {
                        var a = new SchemaUpdate(cfg);
                        using (var stringWriter = new StringWriter())
                        {
                            try
                            {
                                a.Execute(i => stringWriter.WriteLine(i), true);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            var d = stringWriter.ToString();
                        }
                        using (var stringWriter = new StringWriter())
                        {
                            try
                            {
                                a.Execute(i => stringWriter.WriteLine(i), true);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            var c1 = stringWriter.ToString();
                        }
                    });
                    fluentConfiguration.BuildConfiguration();
                    _sessionFactories[SessionFactoryKey] = fluentConfiguration.BuildSessionFactory();
                }
            }
        }

        public string SessionFactoryKey { get; }

        private Action<MappingConfiguration> GetMappings()
        {
            return x => x.FluentMappings.AddFromAssemblyOf<AspNetRole>();
        }

        public IStatelessSession GetStatelessSession()
        {
            DoTestBuild();
            return _sessionFactories[SessionFactoryKey].OpenStatelessSession();
        }

        public ISession GetSession()
        {
            DoTestBuild();
            return _sessionFactories[SessionFactoryKey].OpenSession();
        }

        private void DoTestBuild()
        {
            if (!_testBuild)
            {
                _testBuild = true;
            }
        }
    }
}