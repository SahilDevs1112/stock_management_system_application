using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using StockManagementSystem.Models;
using System.Windows.Forms;

namespace StockManagementSystem.Repository
{
    public class CategoryRepository
    {
        string connectionString;
        SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        SqlDataAdapter sda;
        DataTable dataTable;

        public CategoryRepository()
        {
            connectionString = @"Data Source=USHYDSAKHUNTIA4;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            sqlConnection = new SqlConnection(connectionString);
        }

        public DataTable LoadCategoryDataGridView()
        {

            try
            {
                commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Categorys]";
                sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlConnection.Open();

                sda = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sda.Fill(dataTable);
                sqlConnection.Close();

                return dataTable;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return null;
            }
           
        }

        public bool ValidationCheck(Category category)
        {
            commandString = @"SELECT * FROM [StockManagementSystemDB].[dbo].[Categorys] WHERE Name = '" + category.Name + "'";
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

        public int InsertCategory(Category category)
        {
            commandString = @"INSERT INTO [StockManagementSystemDB].[dbo].[Categorys] (Name) VALUES ('" + category.Name + "')";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            int isExecuted = 0;

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }

        public int UpdateCategory(Category category)
        {
            commandString = "UPDATE [StockManagementSystemDB].[dbo].[Categorys] SET Name = '" + category.Name + "' WHERE ID = " + category.ID + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();

            int isExecuted = 0;

            isExecuted = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return isExecuted;
        }
    }
}
