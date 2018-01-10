using FluentNHibernate.AspNet.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNet.Identity.Mapping
{
    internal class AspNetUserMap : ClassMap<AspNetUser>
    {
        public AspNetUserMap()
        {
            Id(i => i.Id).Column("`Id`").Length(128).Not.Nullable();
            Map(i => i.Email).Column("`Email`").Length(256).Not.Nullable();
            Map(i => i.EmailConfirmed).Column("`EmailConfirmed`").Not.Nullable();
            Map(i => i.PasswordHash).Length(4001).Column("`PasswordHash`").Nullable();
            Map(i => i.SecurityStamp).Length(4001).Column("`SecurityStamp`").Nullable();
            Map(i => i.PhoneNumber).Length(4001).Column("`PhoneNumber`").Nullable();
            Map(i => i.PhoneNumberConfirmed).Length(4001).Column("`PhoneNumberConfirmed`").Not.Nullable();
            Map(i => i.TwoFactorEnabled).Length(4001).Column("`TwoFactorEnabled`").Not.Nullable();
            Map(i => i.LockoutEndDateUtc).Length(4001).Column("`LockoutEndDateUtc`").Nullable();
            Map(i => i.LockoutEnabled).Length(4001).Column("`LockoutEnabled`").Not.Nullable();
            Map(i => i.AccessFailedCount).Length(4001).Column("`AccessFailedCount`").Not.Nullable();
            Map(i => i.UserName).Length(256).Column("`UserName`").Not.Nullable();
        }
    }
}