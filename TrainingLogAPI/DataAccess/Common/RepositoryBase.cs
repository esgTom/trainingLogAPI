using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TrainingLog.DataAccess.Common {
    public class RepositoryBase {

        private string connectionString;
        public string ConnectionString {
            get{
                if (connectionString == null) {
                    connectionString = GetConnectionString();
                    if (connectionString == null) {
                        throw new Exception("API Connection string not found");
                    }
                }
                return connectionString;
            }
        }

        private string GetConnectionString() {
            //return WebConfigurationManager.ConnectionStrings["trainingLogSQLConnection"].ConnectionString.Trim();
            return System.Configuration.ConfigurationManager.ConnectionStrings["trainingLogSQLConnection"].ConnectionString;
            //return System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
        }
    }
    
}