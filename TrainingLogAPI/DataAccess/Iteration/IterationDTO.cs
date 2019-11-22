using System;
using System.Collections.Generic;
using TrainingLog.DataAccess.Micro_Iteration;

namespace TrainingLog.DataAccess.Iteration {

    public class IterationDTO {
        public String Comment { get; set; }
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public String Description { get; set; }
        public DateTime EndDate { get; set; }
        public int IterationId { get; set; }
        public List<Micro_IterationDTO> MicroIterations { get; set; }
        public String ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public int PhaseId { get; set; }
        public DateTime StartDate { get; set; }
 
        public IterationDTO(String comment, String createBy, DateTime createDate, String description, DateTime endDate, int iterationId, List<Micro_IterationDTO> microIterations, String modBy, DateTime modDate, int phaseId, DateTime startDate) {

            this.Comment = comment;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.Description = description;
            this.EndDate = endDate;
            this.IterationId = iterationId;
            this.ModBy = modBy;
            this.ModDate = modDate;
            this.PhaseId = phaseId;
            this.StartDate = startDate;
            this.MicroIterations = microIterations;
        }

        public IterationDTO() {
        }
    }
}
