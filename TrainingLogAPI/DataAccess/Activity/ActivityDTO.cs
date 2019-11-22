using System;

namespace TrainingLog.DataAccess.Activity {

    public class ActivityDTO {
        public String ActivityCategoryCode { get; set; }
        public String ActivityCode { get; set; }
        public DateTime ActivityDate { get; set; }
        public String ActivityDuration { get; set; }
        public int ActivityId { get; set; }
        public Decimal? ActivityIntensityFactor { get; set; }
        public int? ActivitySetCount { get; set; }
        public String ActivityTime { get; set; }
        public String ActivityUnitCode { get; set; }
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int MicroIterationId { get; set; }
        public String ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public int? StateRating { get; set; }
        public int Intensity { get; set; }

        public ActivityDTO() {}
        public ActivityDTO(String activityCategoryCode, String activityCode, DateTime activityDate, String activityDuration, int activityId, 
            Decimal? activityIntensityFactor, int? activitySetCount, String activityTime, String activityUnitCode, int intensity,
            String createBy, DateTime createDate, int microIterationId, String modBy, DateTime modDate, int? stateRating) {

            this.ActivityCategoryCode = activityCategoryCode;
            this.ActivityCode = activityCode;
            this.ActivityDate = activityDate;
            this.ActivityDuration = activityDuration;
            this.ActivityId = activityId;
            this.ActivityIntensityFactor = activityIntensityFactor;
            this.ActivitySetCount = activitySetCount;
            this.ActivityTime = activityTime;
            this.ActivityUnitCode = activityUnitCode;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.MicroIterationId = microIterationId;
            this.ModBy = modBy;
            this.ModDate = modDate;
            this.StateRating = stateRating;
            this.Intensity = intensity;

        }
    }
}
