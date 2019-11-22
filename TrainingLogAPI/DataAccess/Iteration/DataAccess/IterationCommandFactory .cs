using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingLog.DataAccess.Iteration {
    public class IterationCommandFactory {
        internal SqlCommand GetIterations(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Comment, 
                    Create_By, 
                    Create_Date, 
                    Description, 
                    End_Date, 
                    Iteration_Id, 
                    Mod_By, 
                    Mod_Date, 
                    Phase_Id, 
                    Start_Date
                FROM Iteration
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand GetIteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            SELECT 
                Comment, 
                Create_By, 
                Create_Date, 
                Description, 
                End_Date, 
                Iteration_Id, 
                Mod_By, 
                Mod_Date, 
                Phase_Id, 
                Start_Date
            FROM Iteration
            WHERE IterationId = @IterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand InsertIteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            INSERT INTO Iteration(
                Comment, Create_By, Create_Date, Description, End_Date, Iteration_Id, Mod_By, Mod_Date, Phase_Id, Start_Date) 
                VALUES (@Comment, @CreateBy, @CreateDate, @Description, @EndDate, @IterationId, @ModBy, @ModDate, @PhaseId, @StartDate) 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand UpdateIteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            UPDATE Iteration SET 
                Comment = @Comment,
                Create_By = @CreateBy,
                Create_Date = @CreateDate,
                Description = @Description,
                End_Date = @EndDate,
                Iteration_Id = @IterationId,
                Mod_By = @ModBy,
                Mod_Date = @ModDate,
                Phase_Id = @PhaseId,
                Start_Date = @StartDate
            WHERE IterationId = @IterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
        internal SqlCommand DeleteIteration(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            DELETE Iteration
            WHERE IterationId = @IterationId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }
    }
}
