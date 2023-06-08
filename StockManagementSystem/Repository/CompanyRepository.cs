using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repository
{
    public class CompanyRepository
    {
        string connectionString;
        SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        SqlDataAdapter sda;
        DataTable dataTable;

        public CompanyRepository()
        {
            connectionString = @"Data Source=USHYDSAKHUNTIA4;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            sqlConnection = new SqlConnection(connectionString);
        }

        public DataTable LoadCompanyDataGridView()
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Companys]";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }

        public int InsertCompany(Company company)
        {
            commandString = @"INSERT INTO [StockManagementSystemDB].[dbo].[Companys] (Name) VALUES ('" + company.Name + "')";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            int isExecuted = 0;

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public int UpdateCompany(Company company)
        {
            commandString = "UPDATE Companys SET Name = '" + company.Name + "' WHERE ID = " + company.ID + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            
            sqlConnection.Open();

            int isExecuted = 0;

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public bool ValidationCheck(Company company)
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Companys] WHERE Name = '" + company.Name + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            sda = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sda.Fill(dataTable);

            bool isExist = false;
            if (dataTable.Rows.Count > 0)
            {
                isExist = true;
            }

            sqlConnection.Close();

            return isExist;
        }
    }
}
