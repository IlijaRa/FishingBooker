using FishingBooker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FishingBooker.Startup))]
namespace FishingBooker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //// In this method we will create default User roles and Admin user for login    
        //private void CreateRoles()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    // In Startup iam creating first Admin Role and creating a default Admin User     
        //    if (!roleManager.RoleExists("HeadAdmin"))
        //    {

        //        // first we create Admin rool    
        //        var role = new IdentityRole();
        //        role.Name = "HeadAdmin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                   

        //        var user = new ApplicationUser();
        //        user.UserName = "rakocevic7";
        //        user.Email = "rakocevic@gmail.com";

        //        string userPWD = "A@Z200711";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role HeadAdmin    
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "HeadAdmin");

        //        }

        //        // creating FishingInstructor role     
        //        if (!roleManager.RoleExists("FishingInstructor"))
        //        {
        //            var role2 = new IdentityRole();
        //            role2.Name = "FishingInstructor";
        //            roleManager.Create(role2);

        //        }
        //    }
        //}
    }
}
