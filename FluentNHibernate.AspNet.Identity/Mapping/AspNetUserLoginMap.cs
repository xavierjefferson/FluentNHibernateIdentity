using FluentNHibernate.AspNet.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNet.Identity.Mapping
{
    internal class AspNetUserLoginMap : ClassMap<AspNetUserLogin>
    {
        public AspNetUserLoginMap()
        {
            Id(i => i.Id).Column("`Id`").GeneratedBy.Assigned();
            References(i => i.User).Column("`UserId`").Not.Nullable();
            Map(i => i.LoginProvider).Column("`LoginProvider`").Length(128).Not.Nullable();
            Map(i => i.ProviderKey).Column("`ProviderKey`").Not.Nullable().Length(128);
        }
    }
}