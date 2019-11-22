using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingLog.DataAccess.Activity {
    public class IntensityParms {
        public string ActivityCategoryCode { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityDuration { get; set; }
        public int ActivityId { get; set; }
        public decimal? ActivityIntensityFactor { get; set; }
        public int? ActivitySetCount { get; set; }
        public string ActivityTime { get; set; }
        public string ActivityUnitCode { get; set; }

        public IntensityParms() {}

        public IntensityParms(string activityCategoryCode, string activityCode, string      activityDuration, int activityId, decimal? activityIntensityFactor,
            int? activitySetCount, string activityTime, string activityUnitCode) {

            this.ActivityCategoryCode = activityCategoryCode;
            this.ActivityCode = activityCode;
            this.ActivityDuration = activityDuration;
            this.ActivityId = activityId;
            this.ActivityIntensityFactor = activityIntensityFactor;
            this.ActivitySetCount = activitySetCount;
            this.ActivityTime = activityTime;
            this.ActivityUnitCode = activityUnitCode;
        }

    }
    
}