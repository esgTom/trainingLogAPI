using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingLog.DataAccess.Micro_Iteration {
    public class Micro_IterationCommandFactory {
        internal SqlCommand GetMicro_Iterations(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            SELECT 
                Create_By, 
                Create_Date, 
                End_Date, 
                Iteration_Id, 
                Micro_Iteration_Id, 
                Micro_Iteration_Type_Code, 
                Mod_By, 
                Mod_Date, 
                Start_Date
            FROM Micro_Iteration
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand GetMicro_Iteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            SELECT 
                Create_By, 
                Create_Date, 
                End_Date, 
                Iteration_Id, 
                Micro_Iteration_Id, 
                Micro_Iteration_Type_Code, 
                Mod_By, 
                Mod_Date, 
                Start_Date
            FROM Micro_Iteration
            WHERE MicroIterationId = @MicroIterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand InsertMicro_Iteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            INSERT INTO Micro_Iteration(
                Create_By, Create_Date, End_Date, Iteration_Id, Micro_Iteration_Id, Micro_Iteration_Type_Code, Mod_By, Mod_Date, Start_Date) 
                VALUES (@CreateBy, @CreateDate, @EndDate, @IterationId, @MicroIterationId, @MicroIterationTypeCode, @ModBy, @ModDate, @StartDate) 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand UpdateMicro_Iteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            UPDATE Micro_Iteration SET 
                Create_By = @CreateBy,
                Create_Date = @CreateDate,
                End_Date = @EndDate,
                Iteration_Id = @IterationId,
                Micro_Iteration_Id = @MicroIterationId,
                Micro_Iteration_Type_Code = @MicroIterationTypeCode,
                Mod_By = @ModBy,
                Mod_Date = @ModDate,
                Start_Date = @StartDate
            WHERE MicroIterationId = @MicroIterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand DeleteMicro_Iteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            DELETE Micro_Iteration
            WHERE MicroIterationId = @MicroIterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
    }
}
