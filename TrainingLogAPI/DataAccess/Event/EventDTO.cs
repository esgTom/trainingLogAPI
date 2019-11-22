using System;
using System.Collections.Generic;
using TrainingLog.DataAccess.Phase;

namespace TrainingLog.DataAccess.Event {

    public class EventDTO {
        public String Comments { get; set; }
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public String Description { get; set; }
        public DateTime EventDate { get; set; }
        public int EventId { get; set; }
        public List<PhaseDTO> eventPhases { get; set; }
        public String Goals { get; set; }
        public String ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public List<PhaseDTO> phases { get; set; }

        public EventDTO() { }
        public EventDTO(String comments, String createBy, DateTime createDate, String description, DateTime eventDate, List<PhaseDTO> eventPhases, int eventId, String goals, String modBy, DateTime modDate) {
            
            this.Comments = comments;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.Description = description;
            this.EventDate = eventDate;
            this.EventId = eventId;
            this.eventPhases = eventPhases;
            this.Goals = goals;
            this.ModBy = modBy;
            this.ModDate = modDate;

        }
    }
}
