using Microsoft.AspNetCore.Authorization;
using SampleAspNetWithEfCore.DataAccess;

namespace SampleAspNetWithEfCore.Controllers
{
    [Authorize]
    public class ProtectedTeamsController : TeamsController
    {
        // Just reuse another controller, and expose the endpoints in a
        // controller requiring Authorization.

        public ProtectedTeamsController(MetaDbContext teamUserDb, PeopleDbContext db)
            : base(teamUserDb, db)
        { }
    }
}
