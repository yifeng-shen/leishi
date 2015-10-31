using Microsoft.AspNet.Identity.EntityFramework;

namespace leishi.DataModel.Models
{
    public class AppRole : IdentityRole<string, AppUserRole>
    {
        public AppRole()
            : base()
        {

        }

        public AppRole(string name)
            : base()
        {
            Name = name;
        }
    }
}
