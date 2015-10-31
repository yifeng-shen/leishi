using Microsoft.AspNet.Identity.EntityFramework;

namespace leishi.DataModel.Models
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public AppUserRole()
            : base()
        {

        }
    }
}
