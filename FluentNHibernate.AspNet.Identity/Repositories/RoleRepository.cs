using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.AspNet.Identity.Entities;
using NHibernate.Linq;
using Snork.FluentNHibernateTools;

namespace FluentNHibernate.AspNet.Identity.Repositories
{
    public class RoleRepository<TRole> : RepositoryBase where TRole : IdentityRole
    {
        internal RoleRepository(string sessionFactoryKey) : base(sessionFactoryKey)
        {
        }

        public RoleRepository(ProviderTypeEnum providerType, string nameOrConnectionString,
            FluentNHibernatePersistenceBuilderOptions options) : base(providerType, nameOrConnectionString, options)
        {
        }

        public IQueryable<TRole> GetRoles()
        {
            var roles = new List<TRole>();
            using (var session = GetStatelessSession())
            {
                foreach (var role1 in session.Query<AspNetRole>())
                {
                    var role = (TRole) Activator.CreateInstance(typeof(TRole));
                    role.Id = role1.Id;
                    role.Name = role1.Name;
                    roles.Add(role);
                }
            }

            return roles.AsQueryable();
        }

        public void Insert(IdentityRole role)
        {
            using (var session = GetStatelessSession())
            {
                session.Insert(new AspNetRole {Id = role.Id, Name = role.Name});
            }
        }

        public void Delete(string roleId)
        {
            using (var session = GetStatelessSession())
            {
                var qry = string.Format("delete from {0} where {1} = :id", nameof(AspNetRole), nameof(AspNetUser.Id));
                session.CreateQuery(qry).SetParameter("id", roleId).ExecuteUpdate();
            }
        }

        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        private string GetRoleName(string roleId)
        {
            using (var session = GetStatelessSession())
            {
                return session.Query<AspNetRole>().Where(i => i.Id == roleId).Select(i => i.Name).FirstOrDefault();
            }
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        private string GetRoleId(string roleName)
        {
            using (var session = GetStatelessSession())
            {
                return session.Query<AspNetRole>().Where(i => i.Name == roleName).Select(i => i.Id).FirstOrDefault();
            }
        }

        public void Update(IdentityRole role)
        {
            using (var session = GetStatelessSession())
            {
                var qry = string.Format("update {0} set {1}=:name where {2}=:id", nameof(AspNetRole),
                    nameof(AspNetRole.Name), nameof(AspNetRole.Id));
                session.CreateQuery(qry).SetParameter("name", role.Name).SetParameter("id", role.Id).ExecuteUpdate();
            }
        }
    }
}