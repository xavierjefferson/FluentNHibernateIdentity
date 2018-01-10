using System;
using NHibernate;

namespace FluentNHibernate.AspNet.Identity.Repositories
{
    public class RepositoryBase
    {
        protected ISession GetSession()
        {
            throw new NotImplementedException();
        }

        protected IStatelessSession GetStatelessSession()
        {
            throw new NotImplementedException();
        }
    }
}