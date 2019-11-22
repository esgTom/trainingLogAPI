using System.Web.Http;
using TrainingLog.DataAccess.Event;
using System.Web.Http.Cors;


namespace TrainingLog.Controllers {

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EventController : ApiController {

        [HttpGet]
        public IHttpActionResult GetEvent(int eventId) {

            var repo = new EventRepository();
            var response = repo.GetEvent(eventId);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpGet]
        public IHttpActionResult GetEvents(int foreignKeyId) {

            var repo = new EventRepository();
            var response = repo.GetEvents(foreignKeyId);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpGet]
        public IHttpActionResult GetEventGraph(int eventGraphId) {

            var repo = new EventRepository();
            var response = repo.GetEventGraph(eventGraphId);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateEvent(EventDTO eventDTO) {

            var repo = new EventRepository();
            var response = repo.InsertEvent(eventDTO);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPut]
        public IHttpActionResult InsertEvent(EventDTO eventDTO) {

            var repo = new EventRepository();
            var response = repo.UpdateEvent(eventDTO);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int eventId) {

            var repo = new EventRepository();
            var response = repo.DeleteEvent(eventId);
            if (response) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
    }
}
