using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleAspNetWithEfCore.DataAccess;
using SampleAspNetWithEfCore.Entities;

namespace SampleAspNetWithEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly MetaDbContext metaDb;
        private readonly PeopleDbContext db;

        public TeamsController(MetaDbContext metaDb, PeopleDbContext db)
        {
            this.metaDb = metaDb;
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Team>> Get()
        {
            var Teams = db.Set<Team>().OrderBy(p => p.Id).Take(10).ToList();
            return Ok(Teams);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var Team = db.Set<Team>().Find(id);

            if (Team == null)
            {
                return NotFound();
            }

            return Ok(Team);
        }

        [HttpPost]
        public IActionResult Post(Team Team)
        {
            if (Team.Id != default(int))
            {
                return BadRequest("Can't supply the Id with POST");
            }

            db.Set<Team>().Add(Team);
            db.SaveChanges();

            return Created(new Uri($"/{Team.Id}", UriKind.Relative), Team);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Team Team)
        {
            if (Team.Id != default(int) && Team.Id != id)
            {
                return BadRequest("Id on Team had unexpected value");
            }

            Team.Id = id;
            db.Set<Team>().Update(Team);
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Team = db.Set<Team>().Find(id);

            if (Team == null)
            {
                return NotFound();
            }

            db.Set<Team>().Remove(Team);
            db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{teamId}/user/{userName}")]
        public IActionResult PutTeamUser(int teamId, string userName)
        {
            var teamUser = metaDb.TeamUsers.FirstOrDefault(tu => tu.TeamId == teamId && tu.UserName == userName);

            if (teamUser != null)
            {
                return Ok();
            }

            teamUser = new TeamUser { TeamId = teamId, UserName = userName };

            metaDb.TeamUsers.Add(teamUser);
            metaDb.SaveChanges();

            return Created($"{teamId}/user/{userName}", teamUser);
        }

        [HttpGet("{teamId}/user")]
        public ActionResult<IEnumerable<TeamUser>> GetTeamUsers(int teamId)
        {
            return Ok(
                metaDb.TeamUsers.OrderBy(tu => tu.TeamId).ThenBy(tu => tu.UserName).ToList()
            );
        }

        [HttpPost("{teamId}/add-person/{personId}")]
        public IActionResult AddPerson(int teamId, int personId)
        {
            var team = db.Teams.Find(teamId);

            if (team == null) return NotFound();

            var person = db.People.Find(personId);

            if (person == null) return NotFound();

            person.Team = team;
            db.SaveChanges();

            return Ok();
        }
    }
}
