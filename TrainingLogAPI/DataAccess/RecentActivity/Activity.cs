
using System;

namespace TrainingLog.DataAccess.RecentActivity {
    public class Activity {
        public int ActivityId { get; set; }
        public string ActivityType {get; set;}
        public string ActivityDate { get; set; }
        public string ActivityDescription { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string ModBy { get; set; }
        public string ModDate { get; set; }

        public Activity( int activityId, string activityType, DateTime activityDate, string activityDescription, string createBy, DateTime createDate, string modifiedBy, DateTime modifiedDate) {
            this.ActivityId = activityId;
            this.ActivityType = activityType;
            this.ActivityDate = activityDate.ToShortDateString();
            this.ActivityDescription = activityDescription;
            this.CreateBy = createBy;
            this.CreateDate = createDate.ToShortDateString();
            this.ModBy = modifiedBy;
            this.ModDate = modifiedDate.ToShortDateString();
        }

    }

}