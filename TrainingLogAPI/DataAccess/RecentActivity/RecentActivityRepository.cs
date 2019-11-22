using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrainingLog.DataAccess.Common;
using TrainingLog.DataAccess.RecentActivity.DataAccess;

namespace TrainingLog.DataAccess.RecentActivity
{
    internal class RecentActivityRepository : RepositoryBase {

        public List<Activity>  GetRecentActivity() {

            var activities = new List<Activity>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                
                var activtyCmd = new RecentActivityCommandFactory();
                using (var command = activtyCmd.GetRecentActivity(connection)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {

                                var activityId = (int)(reader["Activity_Id"]);
                                var activityType = (String)(reader["Activity_Type"]);
                                var activityDate = (DateTime)(reader["Activity_Date"]);
                                var activityDescription = (String)(reader["Activity_Description"]);
                                var createBy = (String)(reader["Create_By"] == DBNull.Value ? "" : reader["Create_By"]);
                                var createDate = (DateTime)(reader["Create_Date"]);
                                var modifiedBy = (String)(reader["Mod_By"] == DBNull.Value ? "" : reader["Mod_By"]);
                                var modifiedDate = (DateTime)(reader["Mod_Date"]);

                                activities.Add(new Activity(activityId, activityType, activityDate, activityDescription, createBy, createDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }        
            }
            return activities;
        }
    }
}