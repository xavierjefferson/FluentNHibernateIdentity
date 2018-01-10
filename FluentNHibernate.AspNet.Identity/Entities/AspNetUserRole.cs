namespace FluentNHibernate.AspNet.Identity.Entities
{
     class AspNetUserRole
    {
        public virtual AspNetUser User { get; set; }
        public virtual AspNetRole Role { get; set; }
    }
}