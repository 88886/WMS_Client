﻿using MySql.Data.MySqlClient;
using System.Data;

namespace Phicomm_WMS.DB
{
    public class CreatePartialPickTask : BaseDbStoredProcedureCaller
    {
        private string _result = "Error";

        public CreatePartialPickTask(string woid, int wotype)
            : base(
                  DbName, "P_CreatePartialPickTask",
                  new MySqlParameter("@pWoId", MySqlDbType.String) { Value = woid },
                  new MySqlParameter("@pStockOutType", MySqlDbType.Int32) { Value = wotype })
        {

        }

        protected override void ProcessResult(MySqlDataReader reader)
        {
            try
            {
                if (reader.Read())
                {
                    _result = reader.IsDBNull(0)?"":reader.GetString(0);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader?.Close();
            }
        }

        protected override void ProcessParms()
        {
            try
            {
                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public string GetResult()
        {
            return _result;
        }
    }
}