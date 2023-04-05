using CustomerOrderViewer.Models;
using CustomerOrderViewer.Repository;
namespace CustomerOrderViewer
{
    internal class Program
    {
        private static string DatabaseString = @"Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True";
        private static CustomerOrderDetailCommand CustomerOrderCmd { get; } = new CustomerOrderDetailCommand(DatabaseString);
        private static CustomerCommand CustomerCmd { get; } = new CustomerCommand(DatabaseString);
        private static ItemCommand ItemCmd { get; } = new ItemCommand(DatabaseString);


        public static void DisplayCustomerOrders()
        {
            // Get List
            IList<CustomerOrderDetailModel> allOrdersList = CustomerOrderCmd.GetList();

            if (allOrdersList.Any())
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-50} | {3,-10} | {4,-10}", "Order", "FullName (ID)", "Description", "Price", "ItemID");
                Console.WriteLine("");
                foreach (CustomerOrderDetailModel cstmrOrder in allOrdersList)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-50} | {3,-10} | {4,-10}",
                        cstmrOrder.CustomerOrderID,
                        cstmrOrder.FirstName + " " + cstmrOrder.LastName + " (" + cstmrOrder.CustomerID + ")",
                        cstmrOrder.Description,
                        cstmrOrder.Price,
                        cstmrOrder.ItemID);
                }
            }

        }

        public static void DisplayCustomers()
        {
            // Get List
            IList<CustomerModel> allItemsList = CustomerCmd.GetList();

            if (allItemsList.Any())
            {
                Console.WriteLine("{0,-12} | {1,-30} | {2,-10}", "CustomerID", "Full Name", "Age");
                Console.WriteLine("");
                foreach (CustomerModel custom in allItemsList)
                {
                    Console.WriteLine("{0,-12} | {1,-30} | {2,-10}",
                        custom.CustomerId,
                        custom.FirstName + " " + (custom.MiddleName ?? "") + " " + custom.LastName,
                        custom.Age);
                }

            }

        }
        public static void DisplayItems()
        {
            // Get List
            IList<ItemModel> allItemsList = ItemCmd.GetList();

            if (allItemsList.Any())
            {
                Console.WriteLine("{0,-8} | {1,-50} | {2,-10}", "ItemID", "Description", "Price");
                Console.WriteLine("");
                foreach (ItemModel item in allItemsList)
                {
                    Console.WriteLine("{0,-8} | {1,-50} | {2,-10}",
                        item.ItemID,
                        item.Description,
                        item.Price);
                }

            }
        }
        public static void ShowAll()
        {
            Console.WriteLine("\nAll Customer Orders:\n");
            DisplayCustomerOrders();
            Console.WriteLine("\nAll Customer:\n");
            DisplayCustomers();
            Console.WriteLine("\nAll Items:\n");
            DisplayItems();
            Console.WriteLine();

        }

        public static void UpsertCustomerOrder(string userId)
        {
            Console.WriteLine("----> NOTE: For updating type existing CustomerOrderID, for new entries enter -1");

            // Read Order ID
            Console.WriteLine("Enter Customer Order ID to Change");
            int customerOrderID;
            Int32.TryParse(Console.ReadLine(), out customerOrderID);

            // Read Customer ID
            Console.WriteLine("Enter new Customer ID");
            int customerID;
            Int32.TryParse(Console.ReadLine(), out customerID);

            // Read Customer ID
            Console.WriteLine("Enter new Item ID");
            int itemID;
            Int32.TryParse(Console.ReadLine(), out itemID);

            // Call Method
            CustomerOrderCmd.Upsert(customerOrderID, customerID, itemID, userId);

        }

        public static void DeleteCustomerOrder(string userId)
        {
            Console.WriteLine("Enter Customer Order ID");
            // Read Order ID
            int customerOrderID;
            Int32.TryParse(Console.ReadLine(), out customerOrderID);

            // Cal Delete Method
            CustomerOrderCmd.Delete(customerOrderID, userId);

        }

        static void Main(string[] args)
        {
            // Declare Database String
            try
            {
                bool continueManaging = true;

                Console.WriteLine("What is your User name?");
                string userId = Console.ReadLine();

                do
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("               MENU             ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1 - Show All");
                    Console.WriteLine("2 - Upsert Customer Order");
                    Console.WriteLine("3 - Delete Customer Order");
                    Console.WriteLine("4 - Exit");
                    Console.WriteLine("");

                    // Read Entry parameters
                    int option;
                    Int32.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1:
                            ShowAll();
                            break;
                        case 2:
                            UpsertCustomerOrder(userId);
                            break;
                        case 3:
                            DeleteCustomerOrder(userId);
                            break;
                        case 4:
                            continueManaging = false;
                            break;
                        default:
                            Console.WriteLine("Option not Found");
                            break;
                    }
                    Console.WriteLine("************************************************************");

                } while (continueManaging);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Woops! Something went wrong\n{ex.Message}");
            }


        }
    }
}