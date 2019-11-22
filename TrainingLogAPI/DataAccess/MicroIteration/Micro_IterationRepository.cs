using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainingLog.DataAccess.Activity;
using TrainingLog.DataAccess.Common;

namespace TrainingLog.DataAccess.Micro_Iteration {
    internal class Micro_IterationRepository : RepositoryBase {
        public List<Micro_IterationDTO> GetMicro_Iteration(int micro_IterationIdToLookup) {

            var items = new List<Micro_IterationDTO>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new Micro_IterationCommandFactory();
                using (var command = cmd.GetMicro_Iteration(connection)) {

                    command.Parameters["@Micro_IterationId"].Value = micro_IterationIdToLookup;

                    using (var reader = command.ExecuteReader()) {
                        try {
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

                                items.Add(new Micro_IterationDTO(activities, createBy, createDate, endDate, iterationId, microIterationId, microIterationTypeCode, modBy, modDate, startDate));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }
        public Boolean InsertMicro_Iteration(Micro_IterationDTO micro_IterationDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new Micro_IterationCommandFactory();
                using (var command = cmd.InsertMicro_Iteration(connection)) {

                    command.Parameters["@CreateBy"].Value = micro_IterationDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = micro_IterationDTO.CreateDate;
                    command.Parameters["@EndDate"].Value = micro_IterationDTO.EndDate;
                    command.Parameters["@IterationId"].Value = micro_IterationDTO.IterationId;
                    command.Parameters["@MicroIterationId"].Value = micro_IterationDTO.MicroIterationId;
                    command.Parameters["@MicroIterationTypeCode"].Value = micro_IterationDTO.MicroIterationTypeCode;
                    command.Parameters["@ModBy"].Value = micro_IterationDTO.ModBy;
                    command.Parameters["@ModDate"].Value = micro_IterationDTO.ModDate;
                    command.Parameters["@StartDate"].Value = micro_IterationDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdateMicro_Iteration(Micro_IterationDTO micro_IterationDTO) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new Micro_IterationCommandFactory();
                using (var command = cmd.UpdateMicro_Iteration(connection)) {

                    command.Parameters["@CreateBy"].Value = micro_IterationDTO.CreateBy;
                    command.Parameters["@CreateDate"].Value = micro_IterationDTO.CreateDate;
                    command.Parameters["@EndDate"].Value = micro_IterationDTO.EndDate;
                    command.Parameters["@IterationId"].Value = micro_IterationDTO.IterationId;
                    command.Parameters["@MicroIterationId"].Value = micro_IterationDTO.MicroIterationId;
                    command.Parameters["@MicroIterationTypeCode"].Value = micro_IterationDTO.MicroIterationTypeCode;
                    command.Parameters["@ModBy"].Value = micro_IterationDTO.ModBy;
                    command.Parameters["@ModDate"].Value = micro_IterationDTO.ModDate;
                    command.Parameters["@StartDate"].Value = micro_IterationDTO.StartDate;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean DeleteMicro_Iteration(int micro_IterationId) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {

                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var cmd = new Micro_IterationCommandFactory();
                    using (var command = cmd.DeleteMicro_Iteration(connection)) {
                        command.Parameters["@Micro_IterationId"].Value = micro_IterationId;

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
