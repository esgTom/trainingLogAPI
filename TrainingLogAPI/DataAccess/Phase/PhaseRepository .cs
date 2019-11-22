using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Common;
using TrainingLog.DataAccess.Iteration;

namespace TrainingLog.DataAccess.Phase {
    internal class PhaseRepository : RepositoryBase {
        public List<PhaseDTO> GetPhase(int phaseIdToLookup) {

            var items = new List<PhaseDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new PhaseCommandFactory();
                using (var command = cmd.GetPhase(connection)) {

                    command.Parameters["@PhaseId"].Value = phaseIdToLookup;

                    using (var reader = command.ExecuteReader()) {
                        try {
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

                                items.Add(new PhaseDTO(createBy, createDate, endDate, eventId, modBy, modDate, phaseId, phaseIterations, phaseName, startDate));

                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }
        public Boolean InsertPhase(PhaseDTO phaseDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new PhaseCommandFactory();
                using (var command = cmd.InsertPhase(connection)) {

                    command.Parameters["@CreateBy"].Value = phaseDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = phaseDTO.CreateDate;
                    command.Parameters["@EndDate"].Value = phaseDTO.EndDate;
                    command.Parameters["@EventId"].Value = phaseDTO.EventId;
                    command.Parameters["@ModBy"].Value = phaseDTO.ModBy;
                    command.Parameters["@ModDate"].Value = phaseDTO.ModDate;
                    command.Parameters["@PhaseName"].Value = phaseDTO.PhaseName;
                    command.Parameters["@StartDate"].Value = phaseDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdatePhase(PhaseDTO phaseDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new PhaseCommandFactory();
                using (var command = cmd.UpdatePhase(connection)) {

                    command.Parameters["@CreateBy"].Value = phaseDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = phaseDTO.CreateDate;
                    command.Parameters["@EndDate"].Value = phaseDTO.EndDate;
                    command.Parameters["@EventId"].Value = phaseDTO.EventId;
                    command.Parameters["@ModBy"].Value = phaseDTO.ModBy;
                    command.Parameters["@ModDate"].Value = phaseDTO.ModDate;
                    command.Parameters["@PhaseId"].Value = phaseDTO.PhaseId;
                    command.Parameters["@PhaseName"].Value = phaseDTO.PhaseName;
                    command.Parameters["@StartDate"].Value = phaseDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean DeletePhase(int phaseId) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {

                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var cmd = new PhaseCommandFactory();
                    using (var command = cmd.DeletePhase(connection)) {
                        command.Parameters["@PhaseId"].Value = phaseId;

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
