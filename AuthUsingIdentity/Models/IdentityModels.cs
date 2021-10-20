using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthUsingIdentity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole():base()
        {

        }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("StudentDBContext", throwIfV1Schema: false)
        {

        }
        public virtual DbSet<StudentInformation> StudentInformations { get; set; }
        public virtual DbSet<TspInfo> TspInfos { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<ModuleDtl> ModuleDtls { get; set; }
        public virtual DbSet<CourseDtl> CourseDtls { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ResultMake> ResultMakes { get; set; }
        public virtual DbSet<Admission> Admissions { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
    }
}