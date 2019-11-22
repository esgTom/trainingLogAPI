using System;
using System.Data;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Common;

namespace TrainingLog.DataAccess.Activity {
    public class ActivityCommandFactory {

        #region Get Activities
        internal SqlCommand GetActivitys(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                Activity_Category_Code, 
                Activity_Code, 
                Activity_Date, 
                Activity_Duration, 
                Activity_Id, 
                Activity_Intensity_Factor, 
                Activity_Set_Count, 
                Activity_Time, 
                Activity_Unit_Code, 
                Create_By, 
                Create_Date, 
                Micro_Iteration_Id, 
                Mod_By, 
                Mod_Date, 
                State_Rating
                FROM Activity
                ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand GetActivity(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                Activity_Category_Code, 
                Activity_Code, 
                Activity_Date, 
                Activity_Duration, 
                Activity_Id, 
                Activity_Intensity_Factor, 
                Activity_Set_Count, 
                Activity_Time, 
                Activity_Unit_Code, 
                Create_By, 
                Create_Date, 
                Micro_Iteration_Id, 
                Mod_By, 
                Mod_Date, 
                State_Rating
                FROM Activity
                WHERE ActivityId = @ActivityId 
                ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }

        internal SqlCommand GetActivitysForDates(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT	e.Description AS 'Event_Description',
		                e.Event_Id,
		                p.Phase_Name,
		                p.Phase_Id,
                        i.Comment,
                        i.End_Date,
                        i.Description AS 'Iteration_Description',
		                i.Iteration_Id,
                        i.Start_Date,
                        mi.End_Date,
		                mi.Micro_Iteration_Type_Code,
		                mi.Micro_Iteration_Id,
                        mi.Start_Date,
                        a.Activity_Id, 
		                a.Activity_Date, 
		                a.Activity_Category_Code, 
		                a.Activity_Code, 
		                a.Activity_Unit_Code, 
		                a.Activity_Duration, 
		                a.Activity_Set_Count, 
		                a.Activity_Intensity_Factor, 
		                a.Activity_Time, 
		                a.State_Rating, 
		                a.Create_Date, 
		                a.Create_By, 
		                a.Mod_Date, 
		                a.Mod_By
                FROM	Activity a INNER JOIN Micro_Iteration mi
			                ON a.Micro_Iteration_Id = mi.Micro_Iteration_Id
		                JOIN Iteration i 
			                ON i.Iteration_Id = mi.Iteration_Id
		                JOIN Phase p 
			                ON p.Phase_Id = i.Phase_Id
		                JOIN Event e 
			                ON e.Event_Id = p.Event_Id
                WHERE	e.Event_Id = @eventId 
                AND		a.Activity_Date BETWEEN @searchBeginDate AND @searchEndDate 
				ORDER BY a.Activity_Date
            ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);
            cmd.Parameters.Add("@eventId", SqlDbType.Int);
            cmd.Parameters.Add("@searchBeginDate", SqlDbType.Date);
            cmd.Parameters.Add("@searchEndDate", SqlDbType.Date);

            return cmd;
        }

        #endregion

        #region Insert, Update, Delete

        internal SqlCommand InsertActivity(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                INSERT INTO Activity(
                Activity_Category_Code, Activity_Code, Activity_Date, Activity_Duration, Activity_Id, Activity_Intensity_Factor, Activity_Set_Count, Activity_Time, Activity_Unit_Code, Create_By, Create_Date, Micro_Iteration_Id, Mod_By, Mod_Date, State_Rating) 
                VALUES (@ActivityCategoryCode, @ActivityCode, @ActivityDate, @ActivityDuration, @ActivityId, @ActivityIntensityFactor, @ActivitySetCount, @ActivityTime, @ActivityUnitCode, @CreateBy, @CreateDate, @MicroIterationId, @ModBy, @ModDate, @StateRating) 
                ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand UpdateActivity(SqlConnection connection) {

            // TODO: Return records updated count
            var queryString = @"
                SET NOCOUNT ON 
                UPDATE Activity(
                Activity_Category_Code = @ActivityCategoryCode,
                Activity_Code = @ActivityCode,
                Activity_Date = @ActivityDate,
                Activity_Duration = @ActivityDuration,
                Activity_Id = @ActivityId,
                Activity_Intensity_Factor = @ActivityIntensityFactor,
                Activity_Set_Count = @ActivitySetCount,
                Activity_Time = @ActivityTime,
                Activity_Unit_Code = @ActivityUnitCode,
                Create_By = @CreateBy,
                Create_Date = @CreateDate,
                Micro_Iteration_Id = @MicroIterationId,
                Mod_By = @ModBy,
                Mod_Date = @ModDate,
                State_Rating = @StateRating
                WHERE ActivityId = @ActivityId 
                ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand DeleteActivity(SqlConnection connection) {

            // TODO: Return records updated count

            var queryString = @"
                SET NOCOUNT ON 
                DELETE Activity
                WHERE ActivityId = @ActivityId 
                ";

            var cmd = new SqlCommand(Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }

        #endregion
    }
}
