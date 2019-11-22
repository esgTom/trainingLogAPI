using System;
using System.Web.Http;
using System.Web.Http.Cors;
using TrainingLog.DataAccess.Activity;

    
namespace TrainingLog.Controllers {

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ActivityController : ApiController {


        [HttpGet]
        public IHttpActionResult GetActivitiesCalendar(int id, int searchYear, int searchMonth) {

            var repo = new ActivityRepository();
            var response = repo.GetActivitiesCalendar(id, searchYear, searchMonth);
            if (response != null) {
                return Ok(response);
            } else {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult GetActivity(int activityId) {

            var repo = new ActivityRepository();
            var response = repo.GetActivity(activityId);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateActivity(ActivityDTO activityDTO) {

            var repo = new ActivityRepository();
            var response = repo.InsertActivity(activityDTO);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPut]
        public IHttpActionResult InsertActivity(ActivityDTO activityDTO) {

            var repo = new ActivityRepository();
            var response = repo.UpdateActivity(activityDTO);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteActivity(int activityId) {

            var repo = new ActivityRepository();
            var response = repo.DeleteActivity(activityId);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
    }
}
