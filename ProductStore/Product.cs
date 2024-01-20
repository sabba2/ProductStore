using static System.Console;

namespace ProductStore
{
    class Product
    {
        public static string[] categoryCodes = { "e-", "b-", "a-", "t-", "o-" };
        public static string[] categoryNames = { "Electronics", "Books", "Apparel", "Toys", "Others" };
        string productID;
        string productCategoryName;
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public string ProductID
        {
            get { return productID; }
            set
            {
                // Retain product number.
                string productNum = value.Substring(2);
                // Set "o-" and "Others" as default for when no match is found.
                productID = "o-" + productNum; 
                productCategoryName = "Others";
                for (int i = 0; i < categoryCodes.Length; i++)
                {
                    // Check if the first 2 characters of input match any of the categoryCodes.
                    if (value.Substring(0, 2) == categoryCodes[i])
                    { 
                        productID = categoryCodes[i] + productNum;
                        productCategoryName = categoryNames[i];
                        break;
                    }
                }
            }
        }
        public string ProductCategoryName { get; }
        public Product()
        {

        }
        public Product(string id, string name, int quantity, double price)
        {
            ProductID = id;
            ProductName = name;
            ProductQuantity = quantity;
            ProductPrice = price;          
        }
        public new string ToString()
        {
            return "Product Name: " + ProductName + "\nProduct Quantity: " + ProductQuantity + "\nProduct Price: "
            + ProductPrice.ToString("C2") + "\nProductID: " + productID + "\nProduct Category Name: " + productCategoryName;
        }
    }
}


