using System;
using System.Collections.Generic;
using TrainingLog.DataAccess.Activity;

namespace TrainingLog.DataAccess.Micro_Iteration {

    public class Micro_IterationDTO {
        public List<ActivityDTO> activities {get; set;}
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IterationId { get; set; }
        public int MicroIterationId { get; set; }
        public String MicroIterationTypeCode { get; set; }
        public String ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public DateTime? StartDate { get; set; }
        public Micro_IterationDTO(List<ActivityDTO> activities, String createBy, DateTime createDate, DateTime? endDate, int iterationId, int microIterationId, String microIterationTypeCode, String modBy, DateTime modDate, DateTime? startDate) {
            this.activities = activities;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.EndDate = endDate;
            this.IterationId = iterationId;
            this.MicroIterationId = microIterationId;
            this.MicroIterationTypeCode = microIterationTypeCode;
            this.ModBy = modBy;
            this.ModDate = modDate;
            this.StartDate = startDate;

        }
    }
}
