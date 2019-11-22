using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingLog.DataAccess.Activity {
    public class ActivityCalendarDTO {

        public int EventId { get; set; }
        public String EventDescription { get; set; }
        public int PhaseId { get; set; }
        public String PhaseName { get; set; }
        public int IterationId { get; set; }
        public String IterationDescription { get; set; }
        public int MicroIterationId { get; set; }
        public String MicroIterationTypeCode { get; set; }
        public DateTime ActivityDate{ get; set; }
        public List<ActivityDTO> Activities { get; set; }

        public int Intensity { get; set; }

    }
}