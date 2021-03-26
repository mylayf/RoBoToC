using BinanceRobot.DataAccess.Concrete;
using RoBoToC.Core.Models;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RoBoToC.DataAccess.Concrete.AdoNet
{
    public class CurrencyDal : MsSQLHelper
    {
        object locker = new object();
        public void Save(string Currency, PriceModel priceModel)
        {
            lock (locker)
            {
                SaveData(new PriceModel()
                {
                    Price = priceModel.Price,
                    Date = priceModel.Date
                }, Currency);
            }
        }
        public void MultipleSave(string Currency, List<PriceModel> priceModel)
        {
            lock (locker)
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                for (int i = 0; i < priceModel.Count; i++)
                {
                    sqlCommand.CommandText += "Insert into [" + Currency + "](Price,Date) VALUES(@P" + i + ",@D" + i + ")";
                    sqlCommand.Parameters.AddWithValue("@P" + i, priceModel[i].Price);
                    sqlCommand.Parameters.AddWithValue("@D" + i, priceModel[i].Date);
                    priceModel[i].IsRecorded = true;
                }
                OpenConnection();
                sqlCommand.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public List<PriceModel> GetAll(string Currency)
        {
            lock (locker)
            {
                return GetData<List<PriceModel>>("Select Price,Date from [" + Currency + "]");
            }
        }

        public bool CheckTableExistance(string tableName)
        {
            lock (locker)
            {
                SqlCommand sqlCommand = new SqlCommand(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + tableName + "') BEGIN Select 1 as Exist  END");
                sqlCommand.Connection = connection;
                OpenConnection();
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.Read())
                {
                    CloseConnection();
                    return true;
                }
                CloseConnection();
                return false;
            }
        }

        public void CreateTable(string tableName)
        {
            lock (locker)
            {
                SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[" + tableName + @"] (
                [Id] int IDENTITY(1,1) NOT NULL,
                [Price] decimal(18, 9) NULL,
                [Date] datetime2 NULL,
                PRIMARY KEY CLUSTERED([Id])
                WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
            )");
                sqlCommand.Connection = connection;
                OpenConnection();
                sqlCommand.ExecuteNonQuery();
                CloseConnection();
            }
        }
    }
}
