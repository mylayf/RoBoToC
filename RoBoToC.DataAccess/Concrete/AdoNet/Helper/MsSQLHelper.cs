using RoBoToC.Entity.Abstract;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace BinanceRobot.DataAccess.Concrete
{
    public class MsSQLHelper
    {
        public SqlConnection connection = new SqlConnection("Data Source=localhost\\MSSQLSERVER16;Initial Catalog=Robotoc;Trusted_Connection=True;MultipleActiveResultSets=true");

        protected void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                CloseConnection();
            }
            connection.Open();
        }
        protected void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        protected T GetData<T>(string cmd, params SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }
            OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            var schemaTable = reader.GetSchemaTable();
            if (reader.HasRows)
            {
                if (typeof(IList).IsAssignableFrom(typeof(T)))
                {
                    var list = (IList)Activator.CreateInstance(typeof(T));
                    while (reader.Read())
                    {
                        var genericargument = Activator.CreateInstance(list.GetType().GetGenericArguments()[0]);
                        var listElements = genericargument.GetType().GetProperties();
                        foreach (var item in listElements)
                        {
                            if (checkColumnExistance(reader, item.Name))
                            {
                                item.SetValue(genericargument, Convert.ChangeType(reader[item.Name], item.PropertyType), null);
                            }
                        }
                        list.Add(genericargument);
                    }
                    CloseConnection();
                    return (T)list;
                }
                else
                {
                    var classInstance = Activator.CreateInstance(typeof(T));
                    if (reader.Read())
                    {
                        var listElements = classInstance.GetType().GetProperties();
                        foreach (var item in listElements)
                        {
                            if (checkColumnExistance(reader, item.Name))
                            {
                                item.SetValue(classInstance, Convert.ChangeType(reader[item.Name], item.PropertyType), null);
                            }
                        }
                    }
                    CloseConnection();
                    return (T)classInstance;
                }
            }
            else
            {
                CloseConnection();
                return (T)default;
            }
        }
        protected int SaveData<T>(T entity, string? tableName = null)
        {
            var generic = Activator.CreateInstance(typeof(T));
            string _tableName = tableName != null ? tableName : generic.GetType().GetCustomAttribute<TableAttribute>().Name;
            SqlCommand sqlCommand = new SqlCommand("Insert into [" + _tableName + "]", connection);
            var elements = generic.GetType().GetProperties();
            string columns = "(";
            string values = " VALUES(";
            foreach (var item in elements)
            {
                if (item.GetCustomAttribute<NotMappedAttribute>() == null)
                {
                    columns += item.Name + ",";
                    values += "@" + item.Name + ",";
                    sqlCommand.Parameters.AddWithValue("@" + item.Name, item.GetValue(entity));
                }

            }
            columns = columns.Trim(',') + ")";
            values = values.Trim(',') + ")";
            sqlCommand.CommandText += " " + columns + values + "; SELECT SCOPE_IDENTITY();";
            OpenConnection();
            int response = Convert.ToInt32(sqlCommand.ExecuteScalar());
            CloseConnection();
            return response;
        }
        bool checkColumnExistance(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
