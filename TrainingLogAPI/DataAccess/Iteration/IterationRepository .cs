using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Common;
using TrainingLog.DataAccess.Micro_Iteration;

namespace TrainingLog.DataAccess.Iteration {
    internal class IterationRepository : RepositoryBase {
        public List<IterationDTO> GetIteration(int iterationIdToLookup) {

            var items = new List<IterationDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new IterationCommandFactory();
                using (var command = cmd.GetIteration(connection)) {

                    command.Parameters["@IterationId"].Value = iterationIdToLookup;

                    using (var reader = command.ExecuteReader()) {
                        try {
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
                                var microIterations = new List<Micro_IterationDTO>();

                                items.Add(new IterationDTO(comment, createBy, createDate, description, endDate, iterationId, microIterations, modBy, modDate, phaseId, startDate));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }
        public Boolean InsertIteration(IterationDTO iterationDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new IterationCommandFactory();
                using (var command = cmd.InsertIteration(connection)) {

                    command.Parameters["@Comment"].Value = iterationDTO.Comment;
                    command.Parameters["@CreateBy"].Value = iterationDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = iterationDTO.CreateDate;
                    command.Parameters["@Description"].Value = iterationDTO.Description;
                    command.Parameters["@EndDate"].Value = iterationDTO.EndDate;
                    command.Parameters["@IterationId"].Value = iterationDTO.IterationId;
                    command.Parameters["@ModBy"].Value = iterationDTO.ModBy;
                    command.Parameters["@ModDate"].Value = iterationDTO.ModDate;
                    command.Parameters["@PhaseId"].Value = iterationDTO.PhaseId;
                    command.Parameters["@StartDate"].Value = iterationDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdateIteration(IterationDTO iterationDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new IterationCommandFactory();
                using (var command = cmd.UpdateIteration(connection)) {

                    command.Parameters["@Comment"].Value = iterationDTO.Comment;
                    command.Parameters["@CreateBy"].Value = iterationDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = iterationDTO.CreateDate;
                    command.Parameters["@Description"].Value = iterationDTO.Description;
                    command.Parameters["@EndDate"].Value = iterationDTO.EndDate;
                    command.Parameters["@IterationId"].Value = iterationDTO.IterationId;
                    command.Parameters["@ModBy"].Value = iterationDTO.ModBy;
                    command.Parameters["@ModDate"].Value = iterationDTO.ModDate;
                    command.Parameters["@PhaseId"].Value = iterationDTO.PhaseId;
                    command.Parameters["@StartDate"].Value = iterationDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean DeleteIteration(int iterationId) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {

                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var cmd = new IterationCommandFactory();
                    using (var command = cmd.DeleteIteration(connection)) {
                        command.Parameters["@IterationId"].Value = iterationId;

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
