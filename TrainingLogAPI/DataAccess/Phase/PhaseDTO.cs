using System;
using System.Collections.Generic;
using TrainingLog.DataAccess.Iteration;

namespace TrainingLog.DataAccess.Phase {

    public class PhaseDTO {
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EventId { get; set; }
        public List<IterationDTO> Iterations { get; set; }
        public String ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public int PhaseId { get; set; }
        public String PhaseName { get; set; }
        public DateTime? StartDate { get; set; }
        public PhaseDTO(String createBy, DateTime createDate, DateTime? endDate, int eventId, String modBy, DateTime modDate, int phaseId, List<IterationDTO> iterations, String phaseName, DateTime? startDate) {

            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.EndDate = endDate;
            this.EventId = eventId;
            this.Iterations = iterations;
            this.ModBy = modBy;
            this.ModDate = modDate;
            this.PhaseId = phaseId;
            this.PhaseName = phaseName;
            this.StartDate = startDate;
        }

        public PhaseDTO() {

        }
    }

}