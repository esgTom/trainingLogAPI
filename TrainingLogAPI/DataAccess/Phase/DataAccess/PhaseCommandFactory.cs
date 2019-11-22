using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingLog.DataAccess.Phase {
    public class PhaseCommandFactory {
        internal SqlCommand GetPhases(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Create_By, 
                    Create_Date, 
                    End_Date, 
                    Event_Id, 
                    Mod_By, 
                    Mod_Date, 
                    Phase_Id, 
                    Phase_Name, 
                    Start_Date
                FROM Phase
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand GetPhase(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Create_By, 
                    Create_Date, 
                    End_Date, 
                    Event_Id, 
                    Mod_By, 
                    Mod_Date, 
                    Phase_Id, 
                    Phase_Name, 
                    Start_Date
                FROM Phase
                WHERE PhaseId = @PhaseId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand InsertPhase(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                INSERT INTO Phase(
                    Create_By, Create_Date, End_Date, Event_Id, Mod_By, Mod_Date, Phase_Id, Phase_Name, Start_Date) 
                    VALUES (@CreateBy, @CreateDate, @EndDate, @EventId, @ModBy, @ModDate, @PhaseId, @PhaseName, @StartDate) 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand UpdatePhase(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                UPDATE Phase SET
                    Create_By = @CreateBy,
                    Create_Date = @CreateDate,
                    End_Date = @EndDate,
                    Event_Id = @EventId,
                    Mod_By = @ModBy,
                    Mod_Date = @ModDate,
                    Phase_Id = @PhaseId,
                    Phase_Name = @PhaseName,
                    Start_Date = @StartDate
                WHERE PhaseId = @PhaseId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand DeletePhase(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                DELETE Phase
                WHERE PhaseId = @PhaseId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
    }
}
