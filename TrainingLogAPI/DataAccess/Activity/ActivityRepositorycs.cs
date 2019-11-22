using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Common;
using TrainingLog.DataAccess.Event;

namespace TrainingLog.DataAccess.Activity {
    internal class ActivityRepository : RepositoryBase {
        public List<ActivityDTO> GetActivity(int activityIdToLookup) {

            var items = new List<ActivityDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ActivityCommandFactory();
                using (var command = cmd.GetActivity(connection)) {

                    command.Parameters["@ActivityId"].Value = activityIdToLookup;

                    using (var reader = command.ExecuteReader()) {
                        try {
                            while (reader.Read()) {
                                var activityCategoryCode = (String)(reader["Activity_Category_Code"]);
                                var activityCode = (String)(reader["Activity_Code"]);
                                var activityDate = (DateTime)(reader["Activity_Date"] == DBNull.Value ? null : reader["Activity_Date"]);
                                var activityDuration = (String)(reader["Activity_Duration"]);
                                var activityId = (int)(reader["Activity_Id"]);
                                var activityIntensityFactor = (int?)(reader["Activity_Intensity_Factor"] == DBNull.Value ? null : reader["Activity_Intensity_Factor"]);
                                var activitySetCount = (int?)(reader["Activity_Set_Count"] == DBNull.Value ? null : reader["Activity_Set_Count"]);
                                var activityTime = (String)(reader["Activity_Time"]);
                                var activityUnitCode = (String)(reader["Activity_Unit_Code"]);
                                var createBy = (String)(reader["Create_By"]);
                                var createDate = (DateTime)(reader["Create_Date"]);
                                var microIterationId = (int)(reader["Micro_Iteration_Id"]);
                                var modBy = (String)(reader["Mod_By"]);
                                var modDate = (DateTime)(reader["Mod_Date"]);
                                var stateRating = (int?)(reader["State_Rating"] == DBNull.Value ? null : reader["State_Rating"]);

                                items.Add(new ActivityDTO(activityCategoryCode, activityCode, activityDate, activityDuration, activityId, activityIntensityFactor, activitySetCount, activityTime, activityUnitCode, 0, createBy, createDate, microIterationId, modBy, modDate, stateRating));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }

        //public ActivitiesResponseDTO GetActivitysByDateRange(int eventId, DateTime seachBeginDate, DateTime searchEndDate) {
        public ActivitiesResponseDTO GetActivitiesCalendar(int eventId, int searchYear, int searchMonth) {

            var activities = new ActivitiesResponseDTO();
            

            // Fill in current values for any missing parameters
            eventId = GetDefaultEventId(eventId);
            var searchParms = GetDefaultActivitySearchParms(searchYear, searchMonth);
            if (searchParms != null && searchParms.Count > 0) {
                searchYear = searchParms[0];
                searchMonth = searchParms[1];
            }

            var calendarDates = GetCalendarDates(searchYear, searchMonth); // Gets first / last visible date on calendar
            if (calendarDates != null && calendarDates.Count > 0) {


                var calendarStartDate = calendarDates[0];
                var calendarEndDate = calendarDates[1];
                activities.CalendarDays = GetCalendarDays(eventId, calendarStartDate, calendarEndDate); // 

                using (var connection = new SqlConnection(ConnectionString)) {
                    connection.Open();

                    var cmd = new ActivityCommandFactory();
                    using (var command = cmd.GetActivitysForDates(connection)) {

                        command.Parameters["@eventId"].Value = eventId;
                        command.Parameters["@searchBeginDate"].Value = calendarStartDate.ToShortDateString();
                        command.Parameters["@searchEndDate"].Value = calendarEndDate.ToShortDateString(); 

                        using (var reader = command.ExecuteReader()) {
                            try {

                                while (reader.Read()) {

                                    var activityDate = (DateTime?)(reader["Activity_Date"] == DBNull.Value ? null : reader["Activity_Date"]);
                                    var calendarDay = activities.CalendarDays.Find(cd => cd.ActivityDate == activityDate);

                                    if (calendarDay != null) { // Found matching date, so load it with activity

                                        calendarDay.EventId = eventId;
                                        calendarDay.EventDescription = (String)(reader["Event_Description"]);
                                        calendarDay.PhaseId = (int)(reader["Phase_Id"]);
                                        calendarDay.PhaseName = (String)(reader["Phase_Name"]);
                                        calendarDay.IterationId = (int)(reader["Iteration_Id"]);
                                        calendarDay.IterationDescription = (String)(reader["Iteration_Description"]);
                                        calendarDay.MicroIterationId = (int)(reader["Micro_Iteration_Id"]);
                                        calendarDay.MicroIterationTypeCode = (String)(reader["Micro_Iteration_Type_Code"]);

                                        var activity = new ActivityDTO();
                                        var activityCategoryCode = (String)(reader["Activity_Category_Code"]);
                                        var activityCode = (String)(reader["Activity_Code"]);
                                        var activityDuration = (String)(reader["Activity_Duration"]);
                                        var activityId = (int)(reader["Activity_Id"]); ;
                                        var activityIntensityFactor = (decimal?)(reader["Activity_Intensity_Factor"] == DBNull.Value ? null : reader["Activity_Intensity_Factor"]);
                                        var activitySetCount = (int?)(reader["Activity_Set_Count"] == DBNull.Value ? null : reader["Activity_Set_Count"]);
                                        var activityTime = reader["Activity_Time"] == DBNull.Value ? null : (String)(reader["Activity_Time"]);
                                        var activityUnitCode = (String)(reader["Activity_Unit_Code"]);
                                        
                                        activity.ActivityCategoryCode = activityCategoryCode;
                                        activity.ActivityCode = activityCategoryCode;
                                        activity.ActivityDate = activityDate.Value;
                                        activity.ActivityDuration = activityDuration;
                                        activity.ActivityId = activityId;
                                        activity.ActivityIntensityFactor = activityIntensityFactor;
                                        activity.ActivitySetCount = activitySetCount;
                                        activity.ActivityTime = activityTime;
                                        activity.ActivityUnitCode = activityUnitCode;

                                        activity.CreateBy = (String)(reader["Create_By"]);
                                        activity.CreateDate = (DateTime)(reader["Create_Date"]);
                                        activity.ModBy = (String)(reader["Mod_By"]);
                                        activity.ModDate = (DateTime)(reader["Mod_Date"]);
                                        activity.StateRating = (int?)(reader["State_Rating"] == DBNull.Value ? null : reader["State_Rating"]);
                                        
                                        var intensityParms = new IntensityParms(
                                            activityCategoryCode, activityCode, activityDuration, 
                                            activityId, activityIntensityFactor, activitySetCount, 
                                            activityTime, activityUnitCode
                                           );
                                        activity.Intensity = Helpers.CalculateIntensity(intensityParms);
                                        calendarDay.Intensity += Helpers.CalculateIntensity(intensityParms);

                                        if (calendarDay.Activities == null) {
                                            calendarDay.Activities = new List<ActivityDTO>();
                                        } 

                                        calendarDay.Activities.Add(activity);
                                    }

                                }
                            } finally {
                                reader.Close();
                            }
                        }
                    }
                }
            }




            return activities;
        }


        internal List<ActivityCalendarDTO>  GetCalendarDays( int eventId, DateTime calendarStartDate, DateTime calendarEndDate) {
            var calendarDays = new List<ActivityCalendarDTO>();

            var daysToAdd = (calendarEndDate - calendarStartDate).TotalDays;
                
            for (int i = 0; i < daysToAdd; i++) { // Fill list with 1 ActivityCalendarDTO per day in date range

                var activityCalendar = new ActivityCalendarDTO() {
                    ActivityDate = calendarStartDate.AddDays(i)
                };
                    
                calendarDays.Add(activityCalendar);
            }

            return calendarDays;
        }

        private List<DateTime> GetCalendarDates(int searchYear, int searchMonth) {

            // Calc 1st/last day of month
            var monthStartDate = DateTime.Parse(string.Format("{0}/01/{1}", searchMonth.ToString(), searchYear.ToString()));
            var monthStartDay = monthStartDate.DayOfWeek;
            var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);
            var monthEndDay = monthEndDate.DayOfWeek;

            // Calc 1st/last visible dates on calendar
            var calendarStartDate = monthStartDate.AddDays(((int)monthStartDay) * -1);
            var calendarEndDate = monthEndDate.AddDays(6-(int)monthEndDay);

            var calendarDates = new List<DateTime>();
            calendarDates.Add(calendarStartDate);
            calendarDates.Add(calendarEndDate);
            return calendarDates;

        }

        private int GetDefaultEventId(int eventId ) {
            if (eventId == 0) {
                var eventRepo = new EventRepository();
                var events = eventRepo.GetEvents(0);
                if (events != null && events.Count > 0) {
                    eventId = events[0].EventId;
                }

            }
            return eventId;
        }

        private List<int> GetDefaultActivitySearchParms(int searchYear, int searchMonth) {
            var searchParms = new List<int>();
            if (searchYear == 0 || searchMonth == 0) {
                var activityYear = DateTime.Now.Year;
                var activityMonth = 7; // DateTime.Now.Month;
                searchParms.Add(activityYear);
                searchParms.Add(activityMonth);
            }
            return searchParms;
        }



        #region Insert, Update, Delete

        public Boolean InsertActivity(ActivityDTO activityDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ActivityCommandFactory();
                using (var command = cmd.InsertActivity(connection)) {

                    command.Parameters["@ActivityCategoryCode"].Value = activityDTO.ActivityCategoryCode;
                    command.Parameters["@ActivityCode"].Value = activityDTO.ActivityCode;
                    command.Parameters["@ActivityDate"].Value = activityDTO.ActivityDate;
                    command.Parameters["@ActivityDuration"].Value = activityDTO.ActivityDuration;
                    command.Parameters["@ActivityId"].Value = activityDTO.ActivityId;
                    command.Parameters["@ActivityIntensityFactor"].Value = activityDTO.ActivityIntensityFactor;
                    command.Parameters["@ActivitySetCount"].Value = activityDTO.ActivitySetCount;
                    command.Parameters["@ActivityTime"].Value = activityDTO.ActivityTime;
                    command.Parameters["@ActivityUnitCode"].Value = activityDTO.ActivityUnitCode;
                    command.Parameters["@CreateBy"].Value = activityDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = activityDTO.CreateDate;
                    command.Parameters["@MicroIterationId"].Value = activityDTO.MicroIterationId;
                    command.Parameters["@ModBy"].Value = activityDTO.ModBy;
                    command.Parameters["@ModDate"].Value = activityDTO.ModDate;
                    command.Parameters["@StateRating"].Value = activityDTO.StateRating;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdateActivity(ActivityDTO activityDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ActivityCommandFactory();
                using (var command = cmd.UpdateActivity(connection)) {

                    command.Parameters["@ActivityCategoryCode"].Value = activityDTO.ActivityCategoryCode;
                    command.Parameters["@ActivityCode"].Value = activityDTO.ActivityCode;
                    command.Parameters["@ActivityDate"].Value = activityDTO.ActivityDate;
                    command.Parameters["@ActivityDuration"].Value = activityDTO.ActivityDuration;
                    command.Parameters["@ActivityId"].Value = activityDTO.ActivityId;
                    command.Parameters["@ActivityIntensityFactor"].Value = activityDTO.ActivityIntensityFactor;
                    command.Parameters["@ActivitySetCount"].Value = activityDTO.ActivitySetCount;
                    command.Parameters["@ActivityTime"].Value = activityDTO.ActivityTime;
                    command.Parameters["@ActivityUnitCode"].Value = activityDTO.ActivityUnitCode;
                    command.Parameters["@CreateBy"].Value = activityDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = activityDTO.CreateDate;
                    command.Parameters["@MicroIterationId"].Value = activityDTO.MicroIterationId;
                    command.Parameters["@ModBy"].Value = activityDTO.ModBy;
                    command.Parameters["@ModDate"].Value = activityDTO.ModDate;
                    command.Parameters["@StateRating"].Value = activityDTO.StateRating;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean DeleteActivity(int activityId) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {

                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var cmd = new ActivityCommandFactory();
                    using (var command = cmd.DeleteActivity(connection)) {
                        command.Parameters["@ActivityId"].Value = activityId;

                        command.ExecuteNonQuery();
                        rowsAffected = (int)command.Parameters["@rowsAffected"].Value;
                    }
                    transaction.Commit();

                } catch (Exception exception) {
                    transaction.Rollback();
                } finally {
                    transaction = null;
                }
            }

            return (rowsAffected > 0);
        }

        #endregion
    }

}
