using CustomerOrderViewer.Models;
using Dapper;
using System.Data.SqlClient;


namespace CustomerOrderViewer.Repository
{
    internal class CustomerCommand
    {
        private string ConnectionString { set; get; }

        public CustomerCommand(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            // Create List
            List<CustomerModel> listCustomerModels = new List<CustomerModel>();

            // SPROC (Store Procedur Call)
            string sprocStatement = "Customer_GetList";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Use Dapper to Map Query to ItemModel
                listCustomerModels = conn.Query<CustomerModel>(sprocStatement).ToList();
            }
            return listCustomerModels;
        }
    }
}
