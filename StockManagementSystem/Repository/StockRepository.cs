using StockManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Repository
{
    public class StockRepository
    {
        string connectionString;
        SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        SqlDataAdapter sda;
        DataTable dataTable;
        SqlDataReader reader;

        string reorderLevel;
        string availableQuantity;
        int itemId;

        public StockRepository()
        {
            connectionString = @"Data Source=USHYDSAKHUNTIA4;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            sqlConnection = new SqlConnection(connectionString);
        }

        public DataTable LoadCompany()
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Companys]";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }

        public DataTable LoadCategory()
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Categorys]";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }

        public DataTable LoadItem(int companyId, int categoryId)
        {
            commandString = @"SELECT Items.ItemName FROM (([StockManagementSystemDB].[dbo].[Items] LEFT JOIN [StockManagementSystemDB].[dbo].[Companys] ON Items.CompanyID = Companys.ID) LEFT JOIN [StockManagementSystemDB].[dbo].[Categorys] ON Items.CategoryID = Categorys.ID) WHERE Companys.ID = '" + companyId + "' AND Categorys.ID = '" + categoryId + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }

        public string LoadReorder(Stock stock)
        {
            commandString = @"SELECT ReorderLevel FROM [StockManagementSystemDB].[dbo].[Items] WHERE ItemName = '" + stock.ItemName + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                reorderLevel = (reader["ReorderLevel"]).ToString();
            }

            sqlConnection.Close();

            return reorderLevel;
        }

        public string LoadQuantity(Stock stock)
        {
            commandString = @"SELECT AvailableQuantity FROM [StockManagementSystemDB].[dbo].[Items] WHERE ItemName = '" + stock.ItemName + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                availableQuantity = (reader["AvailableQuantity"]).ToString();
            }

            sqlConnection.Close();

            return availableQuantity;
        }

        public int LoadItemID(Stock stock)
        {
            commandString = @"SELECT ID FROM [StockManagementSystemDB].[dbo].[Items] WHERE ItemName = '" + stock.ItemName + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                itemId = Convert.ToInt32(reader["ID"]);
            }

            sqlConnection.Close();

            return itemId;
        }

        public int StockIn(Stock stock)
        {
            int isExecuted = 0;

            commandString = "INSERT INTO [StockManagementSystemDB].[dbo].[Stocks] (ItemID, Quantity, In_date, Status) VALUES (" + stock.ItemID + ", " + stock.Quantity + ", '" + stock.Date + "', '" + stock.Status + "')";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public int UpdateStock(Stock stock)
        {
            int isExecuted = 0;

            string query = "UPDATE [StockManagementSystemDB].[dbo].[Stocks] SET Quantity = '" + stock.Quantity + "' WHERE ID = '" + stock.ID + "'";
            sqlCommand = new SqlCommand(query, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public int InsertAvailableQuantity(Item item)
        {
            int isExecuted = 0;

            string query = "UPDATE [StockManagementSystemDB].[dbo].[Items] SET AvailableQuantity = '" + item.AvailableQuantity + "' WHERE ID = '" + item.ID + "'";
            sqlCommand = new SqlCommand(query, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public DataTable DisplayStock(Stock stock)
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[StocksView] WHERE ItemName = '" + stock.ItemName + "' ORDER BY Date DESC";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }

        public DataTable GetQuantity(Item item)
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Items] WHERE ID = " + item.ID + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

	        sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

	        return dataTable;
	    }

        public int StockOut(Stock stock)
        {
            int isExecuted = 0;

            commandString = "INSERT INTO [StockManagementSystemDB].[dbo].[Stocks] (ItemID, Quantity, In_date, Status) VALUES (" + stock.ItemID + ", " + stock.Quantity + ", '" + stock.Date + "', '" + stock.Status + "')";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public DataTable ViewReport(string fromDate, string toDate, string status)
        {
            commandString = @"SELECT i.ItemName AS ItemName, com.Name AS Company, SUM(s.Quantity) AS Quantity 
                              FROM [StockManagementSystemDB].[dbo].[Stocks] As s, [StockManagementSystemDB].[dbo].[Companys] AS com, [StockManagementSystemDB].[dbo].[Items] AS i 
                              WHERE s.ItemID = i.ID AND i.CompanyID = com.ID AND s.Status = '" + status + "' AND s.In_date BETWEEN '" + fromDate + "' AND '" + toDate + "' GROUP BY i.ItemName, com.Name";

            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }
    }
}
