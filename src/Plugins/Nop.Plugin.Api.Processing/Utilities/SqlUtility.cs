using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;

namespace Nop.Plugin.Api.Processing.Utilities
{
    public class SqlUtility
    {
        public static (SqlDataReader rd, SqlConnection connection) GetSqlDataReader(string conString, string query, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(query, sqlConnection);
            cmd.CommandTimeout = 0;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
            sqlConnection.Open();
            var rd = cmd.ExecuteReader();
            return (rd, sqlConnection);
        }

        public static DataTable GetSqlDataTable(string conString, string query, params SqlParameter[] parameters)
        {
            var dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    foreach (var parameter in parameters)
                        cmd.Parameters.Add(parameter);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Connection = con;

                        sda.SelectCommand = cmd;

                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static (SqlDataReader rd, SqlConnection connection) GetSPSqlDataReader(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
            sqlConnection.Open();
            var rd = cmd.ExecuteReader();
            return (rd, sqlConnection);
        }

        public static async Task<(SqlDataReader rd, SqlConnection connection)> GetSPSqlDataReaderAsync(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);

            await sqlConnection.OpenAsync();
            var rd = await cmd.ExecuteReaderAsync();
            return (rd, sqlConnection);
        }
        public static DataTable GetSPSqlDataTable(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName))
                {
                    foreach (var parameter in parameters)
                        cmd.Parameters.Add(parameter);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Connection = con;

                        sda.SelectCommand = cmd;

                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static async Task<DataTable> GetSPSqlDataTableAsync(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName))
                {
                    foreach (var parameter in parameters)
                        cmd.Parameters.Add(parameter);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Connection = con;

                        sda.SelectCommand = cmd;

                        await Task.Run(() => sda.Fill(dt));
                    }
                }
            }

            return dt;
        }

        public static DataTable GetSPSqlDataTable2(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
            sqlConnection.Open();
            var rd = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(rd);
            rd.Close();
            sqlConnection.Close();
            return dt;
        }

        public static int ExecuteSP(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
            sqlConnection.Open();
            var result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return result;
        }

        public static async Task<int> ExecuteSPAsync(string conString, string procedureName, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);

            await sqlConnection.OpenAsync();
            var result = await cmd.ExecuteNonQueryAsync();
            sqlConnection.Close();
            return result;
        }

        public static int ExecuteSP(string conString, string procedureName, ref SqlParameterCollection cmdParameters, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(procedureName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
            sqlConnection.Open();
            var result = cmd.ExecuteNonQuery();
            cmdParameters = cmd.Parameters;
            sqlConnection.Close();
            return result;
        }

        public static int ExecuteSQL(string conString, string sqlQuery, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);

            sqlConnection.Open();
            var result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return result;
        }


        public static async Task<int> ExecuteSQLAsync(string conString, string sqlQuery, params SqlParameter[] parameters)
        {
            var sqlConnection = new SqlConnection(conString);
            var cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);

            sqlConnection.Open();
            var result = await cmd.ExecuteNonQueryAsync();
            sqlConnection.Close();
            return result;
        }

        //public static bool ExecuteTransactions(string conString, List<SqlSupportTransaction> transactions)
        //{
        //    var success = true;

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        connection.Open();

        //        SqlCommand command = connection.CreateCommand();

        //        SqlTransaction transaction;

        //        transaction = connection.BeginTransaction("ApplicationTransaction");

        //        command.Connection = connection;
        //        command.Transaction = transaction;

        //        try
        //        {
        //            foreach (var item in transactions)
        //            {
        //                command.CommandText = item.Query;
        //                foreach (var parameter in item.Parameters)
        //                    command.Parameters.Add(parameter);

        //                command.CommandType = item.CommandType;
        //                command.ExecuteNonQuery();

        //                item.Parameters.Clear();

        //                foreach (var pItem in command.Parameters)
        //                    item.Parameters.Add(pItem);

        //                command.Parameters.Clear();
        //            }

        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;

        //            try
        //            {
        //                transaction.Rollback();
        //                success = false;
        //            }
        //            catch (Exception ex2)
        //            {
        //                throw ex2;
        //            }
        //        }
        //    }

        //    return success;
        //}
        public static SqlParameter SqlParam(string paramName, object paramValue)
        {
            SqlParameter param = new SqlParameter(paramName, paramValue);
            return param;
        }

        public static SqlParameter ParamOut(string paramName, SqlDbType dbType)
        {
            SqlParameter param = new SqlParameter(paramName, dbType);
            param.Direction = ParameterDirection.Output;
            return param;
        }

        public static SqlParameter Param(string paramName, object paramValue, SqlDbType dbType, string typeName)
        {
            SqlParameter paramVar = new SqlParameter(paramName, dbType);
            paramVar.Value = paramValue;
            paramVar.TypeName = typeName;
            return paramVar;
        }
    }
}
