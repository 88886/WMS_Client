﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace Phicomm_WMS.DB
{
    /// <summary>
    /// 根据ip获取当前设备的名称和类型
    /// </summary>
    public class SearchInventoryCheckByMaterialNo : BaseDbQuery
    {
        private DataTable _dt = new DataTable();

        public SearchInventoryCheckByMaterialNo(Dictionary<string, object> dic)
            : base(
                "Select check_date, material_no, plant, stock_id, status from inventorycheckbymaterialno ", DbName)
        {
            _dt.Columns.Add("CheckDate", typeof(string));
            _dt.Columns.Add("KpNo", typeof(string));
            _dt.Columns.Add("Plant", typeof(string));
            _dt.Columns.Add("Stock_Id", typeof(string));
            _dt.Columns.Add("Status", typeof(int));

            if (dic != null && dic.Count > 0)
            {
                Sql += " Where ";
                foreach (KeyValuePair<string, object> obj in dic)
                {
                    Sql += " " + obj.Key + "='" + obj.Value + "' AND ";
                }
                Sql = Sql.Substring(0, Sql.Length - 5);
            }
        }

        protected override List<string> ProcessSql(string sql)
        {
            List<string> sqlList = new List<string> { sql };

            return sqlList;
        }

        protected override void ProcessResultSet(MySqlDataReader reader)
        {
            try
            {
                while (reader.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CheckDate"] = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    dr["KpNo"] = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    dr["Plant"] = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    dr["Stock_Id"] = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    dr["Status"] = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                    _dt.Rows.Add(dr);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            finally
            {
                reader?.Close();
            }
        }

        public DataTable GetResult()
        {
            return _dt;
        }
    }
}
