namespace CustomerOrderViewer.Models
{
    internal class CustomerOrderDetailModel
    {
        public int CustomerOrderID { set; get; }
        public int CustomerID { set; get; }
        public int ItemID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
    }
}
