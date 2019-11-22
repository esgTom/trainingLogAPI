using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrainingLog.DataAccess.RecentActivity.DataAccess
{
    public class RecentActivityCommandFactory
    {
            internal SqlCommand GetRecentActivity(SqlConnection connection) {

            var queryString = "SELECT * FROM Workout";
            var cmd = new SqlCommand(queryString, connection);
            return cmd;
        }        
    }
}