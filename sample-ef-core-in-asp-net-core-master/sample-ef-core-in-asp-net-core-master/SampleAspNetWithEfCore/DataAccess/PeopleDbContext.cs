using Microsoft.EntityFrameworkCore;
using SampleAspNetWithEfCore.Entities;
using System.Linq;
using System.Security.Claims;

namespace SampleAspNetWithEfCore.DataAccess
{
    public class PeopleDbContext : DbContext
    {
        private readonly int[] filterOnOnlyTheseTeamIds = new int[0];
        private readonly bool isAuthenticated;

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options, MetaDbContext teamDb, ClaimsPrincipal user)
            : base (options)
        {
            var userNameClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userNameClaim != null)
            {
                filterOnOnlyTheseTeamIds = teamDb.TeamUsers
                    .Where(tu => tu.UserName == userNameClaim.Value)
                    .Select(tu => tu.TeamId)
                    .ToArray();
            }

            isAuthenticated = user?.Identity?.IsAuthenticated ?? false;
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>();
            modelBuilder.Entity<Team>().HasIndex(t => t.Code).IsUnique();

            modelBuilder.Entity<Person>().OwnsOne(p => p.Pet);
            modelBuilder.Entity<Person>().HasQueryFilter(p => !p.IsArchived);

            // Bit weird to show unauthenticated users everything, but for easier testing...
            modelBuilder.Entity<Person>().HasQueryFilter(p => !isAuthenticated || filterOnOnlyTheseTeamIds.Contains(p.Team.Id));
        }

        public static void Seed(PeopleDbContext db)
        {
            var defaultTeam = db.Teams.SingleOrDefaultAsync(t => t.Code == Team.DefaultTeamCode).Result;

            if (defaultTeam == null)
            {
                db.Teams.Add(new Team { Code = Team.DefaultTeamCode, Name = Team.DefaultTeamCode });
            }
            
            db.SaveChanges();
        }
    }
}
