using CustomerOrderViewer.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace CustomerOrderViewer.Repository
{

    internal class CustomerOrderDetailCommand
    {
        private string ConnectionString { set; get; }

        public CustomerOrderDetailCommand(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Upsert(int customerOrderId, int customerId, int itemId, string userId)
        {
            // SPROC (Store Procedur Call)
            string sprocStatement = "CustomerOrderDetail_Upsert";

            // Create DataTable for the Type Created in The SQL Database
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerOrderID", typeof(int));
            dt.Columns.Add("CustomerID", typeof(int));
            dt.Columns.Add("ItemID", typeof(int));
            dt.Rows.Add(customerOrderId, customerId, itemId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Use Dapper to Map Query to ItemModel
                conn.Execute(sprocStatement, new
                {
                    @CustomerOrderType = dt.AsTableValuedParameter("CustomerOrderType"),
                    @UserId = userId
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Delete(int customerOrderID, string userID)
        {
            // SPROC (Store Procedur Call)
            string sprocStatement = "CustomerOrderDetail_Delete";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Use Dapper to Map Query to ItemModel
                conn.Execute(sprocStatement, new { @CustomerOrderId = customerOrderID, @UserID = userID }, commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        public IList<CustomerOrderDetailModel> GetList()
        {
            // Create List of Model type
            List<CustomerOrderDetailModel> listCustomerOrderDetailModels = new List<CustomerOrderDetailModel>();

            // SPROC (Store Procedur Call)
            string sprocStatement = "CustomerOrderDetail_GetList";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Use Dapper to Map Query to ItemModel
                listCustomerOrderDetailModels = conn.Query<CustomerOrderDetailModel>(sprocStatement).ToList();
            }

            return listCustomerOrderDetailModels;

        }
    }
}
