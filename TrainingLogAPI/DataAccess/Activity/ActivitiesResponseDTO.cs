using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingLog.DataAccess.Event;

namespace TrainingLog.DataAccess.Activity {
    public class ActivitiesResponseDTO {
        public List<ActivityCalendarDTO> CalendarDays { get; set; }
    }
}