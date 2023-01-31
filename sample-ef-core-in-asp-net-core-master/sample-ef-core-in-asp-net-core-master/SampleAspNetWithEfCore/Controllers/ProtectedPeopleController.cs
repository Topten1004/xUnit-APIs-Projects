using Microsoft.AspNetCore.Authorization;
using SampleAspNetWithEfCore.DataAccess;

namespace SampleAspNetWithEfCore.Controllers
{
    [Authorize]
    public class ProtectedPeopleController : PeopleController
    {
        // Just reuse another controller, and expose the endpoints in a
        // controller requiring Authorization.

        public ProtectedPeopleController(PeopleDbContext db)
            : base(db)
        { }
    }
}
