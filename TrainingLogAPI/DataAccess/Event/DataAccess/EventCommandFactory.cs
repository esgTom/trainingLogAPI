using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingLog.DataAccess.Event {
    public class EventCommandFactory {
        internal SqlCommand GetEvents(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                Comments, 
                Create_By, 
                Create_Date, 
                Description, 
                Event_Date, 
                Event_Id, 
                Goals, 
                Mod_By, 
                Mod_Date
                FROM Event
                ORDER BY Event_Date DESC
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand GetEvent(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Comments, 
                    Create_By, 
                    Create_Date, 
                    Description, 
                    Event_Date, 
                    Event_Id, 
                    Goals, 
                    Mod_By, 
                    Mod_Date
                FROM Event
                WHERE EventId = @eventId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            cmd.Parameters.Add("@eventId", SqlDbType.Int);
            return cmd;
        }

        internal SqlCommand GetEventGraph(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 

                SELECT e.* FROM Event e WHERE e.Event_Id = @EventId

                SELECT p.* FROM Event e INNER JOIN Phase p ON p.Event_Id = e.Event_Id WHERE e.Event_Id = @EventId

                SELECT  i.*
                FROM    Event e INNER JOIN Phase p
		                    ON p.Event_Id = e.Event_Id
	                    INNER JOIN Iteration i
		                    ON p.Phase_Id = i.Phase_Id
                WHERE e.Event_Id = @EventId

                SELECT mi.*
                FROM    Event e INNER JOIN Phase p
		                    ON p.Event_Id = e.Event_Id
	                    INNER JOIN Iteration i
		                    ON p.Phase_Id = i.Phase_Id
	                    INNER JOIN Micro_Iteration mi
		                    ON i.Iteration_Id = mi.Iteration_Id
                WHERE e.Event_Id = @EventId
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            cmd.Parameters.Add("@eventId", SqlDbType.Int);
            return cmd;
        }
        internal SqlCommand InsertEvent(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                INSERT INTO Event(
                    Comments, Create_By, Create_Date, Description, Event_Date, Event_Id, Goals, Mod_By, Mod_Date) 
                    VALUES (@Comments, @CreateBy, @CreateDate, @Description, @EventDate, @EventId, @Goals, @ModBy, @ModDate) 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand UpdateEvent(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                UPDATE Event SET
                    Comments = @Comments,
                    Create_By = @CreateBy,
                    Create_Date = @CreateDate,
                    Description = @Description,
                    Event_Date = @EventDate,
                    Event_Id = @EventId,
                    Goals = @Goals,
                    Mod_By = @ModBy,
                    Mod_Date = @ModDate
                WHERE EventId = @EventId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand DeleteEvent(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                    DELETE Event
                WHERE EventId = @eventId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            cmd.Parameters.Add("@eventId", SqlDbType.Int);
            return cmd;
        }
    }
}
