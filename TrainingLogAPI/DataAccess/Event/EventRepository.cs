using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Activity;
using TrainingLog.DataAccess.Common;
using TrainingLog.DataAccess.Iteration;
using TrainingLog.DataAccess.Micro_Iteration;
using TrainingLog.DataAccess.Phase;

namespace TrainingLog.DataAccess.Event {
    internal class EventRepository : RepositoryBase {
        public List<EventDTO> GetEvent(int eventIdToLookup) {

            var items = new List<EventDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new EventCommandFactory();
                using (var command = cmd.GetEvent(connection)) {

                    command.Parameters["@EventId"].Value = eventIdToLookup;

                    using (var reader = command.ExecuteReader()) {
                        try {
                            while (reader.Read()) {
                                var comments = (String)(reader["Comments"]);
                                var createBy = (String)(reader["Create_By"]);
                                var createDate = (DateTime)(reader["Create_Date"]);
                                var description = (String)(reader["Description"]);
                                var eventDate = (DateTime)(reader["Event_Date"]);
                                var eventId = (int)(reader["Event_Id"]);
                                var goals = (String)(reader["Goals"]);
                                var modBy = (String)(reader["Mod_By"]);
                                var modDate = (DateTime)(reader["Mod_Date"]);
                                var eventPhases = new List<PhaseDTO>();
                                items.Add(new EventDTO(comments, createBy, createDate, description, eventDate, eventPhases, eventId, goals, modBy, modDate));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }

        public List<EventDTO> GetEvents(int eventIdToLookup) {

            // eventIdToLookup is not used currently, keeping it for future reference

            var items = new List<EventDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new EventCommandFactory();
                using (var command = cmd.GetEvents(connection)) {

                    using (var reader = command.ExecuteReader()) {
                        try {
                            while (reader.Read()) {
                                var comments = (String)(reader["Comments"]);
                                var createBy = (String)(reader["Create_By"]);
                                var createDate = (DateTime)(reader["Create_Date"]);
                                var description = (String)(reader["Description"]);
                                var eventDate = (DateTime)(reader["Event_Date"]);
                                var eventId = (int)(reader["Event_Id"]);
                                var goals = (String)(reader["Goals"]);
                                var modBy = (String)(reader["Mod_By"]);
                                var modDate = (DateTime)(reader["Mod_Date"]);
                                var eventPhases = new List<PhaseDTO>();

                                items.Add(new EventDTO(comments, createBy, createDate, description, eventDate, eventPhases, eventId, goals, modBy, modDate));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }

        public EventDTO GetEventGraph(int eventId) {

            var dataSet = 0;
            var eventGraph = new EventDTO();
            var phases = new List<PhaseDTO>();
            var iterations = new List<IterationDTO>();
            var microIterations = new List<Micro_IterationDTO>();

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new EventCommandFactory();
                using (var command = cmd.GetEventGraph(connection)) {

                    command.Parameters["@EventId"].Value = eventId;

                    using (var reader = command.ExecuteReader()) {
                        try {
                            while (reader.HasRows) {  // Load Event related data into local vars
                                if (dataSet == 0) {
                                    eventGraph = loadEventData(reader);
                                } else if (dataSet == 1) {
                                    phases = loadPhaseData(reader);
                                } else if (dataSet == 2) {
                                    iterations = loadIterationData(reader);
                                } else if (dataSet == 3) {
                                    microIterations = loadMicroIterationData(reader);
                                }

                                reader.NextResult();
                                dataSet += 1; // increment dataset

                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }

            phases.ForEach( p => p.Iterations = iterations.FindAll( i => i.PhaseId == p.PhaseId ));
            phases.ForEach( p => p.Iterations.ForEach( i => i.MicroIterations = microIterations.FindAll( mi => mi.IterationId == i.IterationId)));



            eventGraph.phases = phases;

            return eventGraph;


        }

        #region Load Event Graph Data
        private EventDTO loadEventData(SqlDataReader reader) {
            var eventGraph = new EventDTO();
            while (reader.Read()) {
                var comments = (String)(reader["Comments"]);
                var createBy = (String)(reader["Create_By"]);
                var createDate = (DateTime)(reader["Create_Date"]);
                var description = (String)(reader["Description"]);
                var eventDate = (DateTime)(reader["Event_Date"]);
                var eventId = (int)(reader["Event_Id"]);
                var goals = (String)(reader["Goals"]);
                var modBy = (String)(reader["Mod_By"]);
                var modDate = (DateTime)(reader["Mod_Date"]);
                var eventPhases = new List<PhaseDTO>();
                eventGraph = new EventDTO(comments, createBy, createDate, description, eventDate, eventPhases, eventId, goals, modBy, modDate);
            }
            return eventGraph;
        }
        private List<PhaseDTO> loadPhaseData(SqlDataReader reader) {
            var phases = new List<PhaseDTO>();
            while (reader.Read()) {
                var createBy = (String)(reader["Create_By"]);
                var createDate = (DateTime)(reader["Create_Date"]);
                var endDate = (DateTime?)(reader["End_Date"] == DBNull.Value ? null : reader["End_Date"]);
                var eventId = (int)(reader["Event_Id"]);
                var modBy = (String)(reader["Mod_By"]);
                var modDate = (DateTime)(reader["Mod_Date"]);
                var phaseId = (int)(reader["Phase_Id"]);
                var phaseName = (String)(reader["Phase_Name"]);
                var startDate = (DateTime?)(reader["Start_Date"] == DBNull.Value ? null : reader["Start_Date"]);
                var phaseIterations = new List<IterationDTO>();
                phases.Add(new PhaseDTO(createBy, createDate, endDate, eventId, modBy, modDate, phaseId, phaseIterations, phaseName, startDate));
            }
            return phases;
        }
        private List<IterationDTO> loadIterationData(SqlDataReader reader) {
            var iterations = new List<IterationDTO>();
            while (reader.Read()) {
                var comment = (String)(reader["Comment"]);
                var createBy = (String)(reader["Create_By"]);
                var createDate = (DateTime)(reader["Create_Date"]);
                var description = (String)(reader["Description"]);
                var endDate = (DateTime)(reader["End_Date"]);
                var iterationId = (int)(reader["Iteration_Id"]);
                var modBy = (String)(reader["Mod_By"]);
                var modDate = (DateTime)(reader["Mod_Date"]);
                var phaseId = (int)(reader["Phase_Id"]);
                var startDate = (DateTime)(reader["Start_Date"]);
                var iterationMicroIterations = new List<Micro_IterationDTO>();
                iterations.Add(new IterationDTO(comment, createBy, createDate, description, endDate, iterationId, iterationMicroIterations, modBy, modDate, phaseId, startDate));
            }
            return iterations;
        }
        private List<Micro_IterationDTO> loadMicroIterationData(SqlDataReader reader) {
            var microIterations = new List<Micro_IterationDTO>();
            while (reader.Read()) {
                var activities = new List<ActivityDTO>();
                var createBy = (String)(reader["Create_By"]);
                var createDate = (DateTime)(reader["Create_Date"]);
                var endDate = (DateTime?)(reader["End_Date"] == DBNull.Value ? null : reader["End_Date"]);
                var iterationId = (int)(reader["Iteration_Id"]);
                var microIterationId = (int)(reader["Micro_Iteration_Id"]);
                var microIterationTypeCode = (String)(reader["Micro_Iteration_Type_Code"]);
                var modBy = (String)(reader["Mod_By"]);
                var modDate = (DateTime)(reader["Mod_Date"]);
                var startDate = (DateTime?)(reader["Start_Date"] == DBNull.Value ? null : reader["Start_Date"]);
                microIterations.Add(new Micro_IterationDTO(activities, createBy, createDate, endDate, iterationId, microIterationId, microIterationTypeCode, modBy, modDate, startDate));
            }
            return microIterations;
        }
        #endregion

        private EventDTO getEventGraphData(EventDTO eventGraph) {








            return eventGraph;
        }

        public Boolean InsertEvent(EventDTO eventDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new EventCommandFactory();
                using (var command = cmd.InsertEvent(connection)) {

                    command.Parameters["@Comments"].Value = eventDTO.Comments;
                    command.Parameters["@CreateBy"].Value = eventDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = eventDTO.CreateDate;
                    command.Parameters["@Description"].Value = eventDTO.Description;
                    command.Parameters["@EventDate"].Value = eventDTO.EventDate;
                    command.Parameters["@EventId"].Value = eventDTO.EventId;
                    command.Parameters["@Goals"].Value = eventDTO.Goals;
                    command.Parameters["@ModBy"].Value = eventDTO.ModBy;
                    command.Parameters["@ModDate"].Value = eventDTO.ModDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdateEvent(EventDTO eventDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new EventCommandFactory();
                using (var command = cmd.UpdateEvent(connection)) {

                    command.Parameters["@Comments"].Value = eventDTO.Comments;
                    command.Parameters["@CreateBy"].Value = eventDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = eventDTO.CreateDate;
                    command.Parameters["@Description"].Value = eventDTO.Description;
                    command.Parameters["@EventDate"].Value = eventDTO.EventDate;
                    command.Parameters["@EventId"].Value = eventDTO.EventId;
                    command.Parameters["@Goals"].Value = eventDTO.Goals;
                    command.Parameters["@ModBy"].Value = eventDTO.ModBy;
                    command.Parameters["@ModDate"].Value = eventDTO.ModDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean DeleteEvent(int eventId) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {

                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var cmd = new EventCommandFactory();
                    using (var command = cmd.DeleteEvent(connection)) {
                        command.Parameters["@EventId"].Value = eventId;

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
    }

}
