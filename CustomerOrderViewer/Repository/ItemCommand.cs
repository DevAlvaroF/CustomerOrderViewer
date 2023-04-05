using CustomerOrderViewer.Models;
using Dapper;
using System.Data.SqlClient;

namespace CustomerOrderViewer.Repository
{
    internal class ItemCommand
    {
        private string ConnectionString { set; get; }

        public ItemCommand(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IList<ItemModel> GetList()
        {
            // Create List
            List<ItemModel> listItemModels = new List<ItemModel>();

            // SPROC (Store Procedur Call)
            string sprocStatement = "Item_GetList";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Use Dapper to Map Query to ItemModel
                listItemModels = conn.Query<ItemModel>(sprocStatement).ToList();
            }
            return listItemModels;
        }

    }
}
