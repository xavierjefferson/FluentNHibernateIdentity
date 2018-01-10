using System.Collections.Generic;
using System.Data;
using System.Linq;
using FluentNHibernate.AspNet.Identity.Entities;
using NHibernate.Linq;

namespace FluentNHibernate.AspNet.Identity.Repositories
{
    public class UserRoleRepository<TUser> : RepositoryBase where TUser : IdentityUser
    {
        private readonly string _connectionString;

        public UserRoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(TUser user, string roleName)
        {
            using (var session = GetStatelessSession())
            {
                var role = session.Query<AspNetRole>().FirstOrDefault(i => i.Name == roleName);
                if (role != null)
                {
                    var userRole = new AspNetUserRole {User = new AspNetUser {Id = user.Id}, Role = role};
                    session.Insert(userRole);
                }
            }
        }

        public void Delete(TUser user, string roleName)
        {
            var qry = string.Format("delete from {0} where {1}.{2}=:id and {3}.{4}=:rolename",
                nameof(AspNetUserRole), nameof(AspNetUserRole.User), nameof(AspNetUser.Id), nameof(AspNetUserRole.Role),
                nameof(AspNetRole.Name));
            using (var session = GetStatelessSession())
            {
                session.CreateQuery(qry).SetParameter("id", user.Id).SetParameter("rolename", roleName).ExecuteUpdate();
            }
        }

        public List<string> PopulateRoles(string userId)
        {
            using (var session = GetStatelessSession())
            {
                return session.Query<AspNetUserRole>()
                    .Where(i => i.User.Id == userId)
                    .Select(i => i.Role.Name)
                    .ToList();
            }
        }
    }
}