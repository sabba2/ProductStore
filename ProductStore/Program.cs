using System.Runtime.InteropServices;
using static System.Console;

namespace ProductStore
{
    class Program
    {
        static void Main(string[] args)
        {
            int numProducts;
            numProducts = InputValue(1, 30);
            Product[] products = new Product[numProducts];           
            GetProductData(products);
            DisplayAllProducts(products);
            CalculateRevenue(products);
            GetProductLists(products);
        }
        // Allows user to define the number of products.
        public static int InputValue(int min, int max)
        {
            int value;
            string inputString;
            Write("Input a value for the number of products from {0} to {1} >> ", min, max);
            inputString = ReadLine() ?? string.Empty;
            // Keep prompting until user enters valid value.
            while(!IsNonNegativeInt(inputString, out value) || value < min || value > max)
            {
                Write("Please input a valid non negative value from {0} to {1} >> ", min, max);
                inputString = ReadLine() ?? string.Empty;
            }
            return value;
        }
        // Validates productID as per requirements.
        public static bool CheckString(string id)
        {
            return id.Length == 4 &&
                   char.IsLower(id[0]) &&
                   id[1] == '-' &&
                   char.IsDigit(id[2]) &&
                   char.IsDigit(id[3]);
        }
        // Validates category codes as per requirements.
        public static bool CheckCategoryCode(string code)
        {           
            return Product.categoryCodes.Contains(code);
        }
        // Validates number inputs as numbers and >= 0.
        public static bool IsNonNegativeDouble(string input, out double number)
        {
            if (double.TryParse(input, out number) && number >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNonNegativeInt(string input, out int number)
        {
            if (int.TryParse(input, out number) && number >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Prompts user to enter product information for each product.
        private static void GetProductData(Product[] products)
        {;
            string productName, productID, inputString;
            int productQuantity;
            double productPrice;           
            for (int i = 0; i < products.Length; i++)
            {
                WriteLine("\n------- Input Product {0} -------", i + 1);
                Write("Input Product Name >> ");           
                productName = ReadLine() ?? string.Empty;
                Write("Input Product Quantity >> ");
                inputString = ReadLine() ?? string.Empty;
                while (!IsNonNegativeInt(inputString, out productQuantity))
                {
                    Write("Please enter a non-negative number >> ");
                    inputString = ReadLine() ?? string.Empty;
                }
                Write("Input Product Price >> ");
                inputString = ReadLine() ?? string.Empty;
                while (!IsNonNegativeDouble(inputString, out productPrice))
                {
                    Write("Please enter a non-negative number >> ");
                    inputString = ReadLine() ?? string.Empty;
                }
                Write("Input Product ID\n" +
                    "Valid categories include: Electronics (e-), Books (b-), Apparel (a-), Toys (t-), Others (o-)\n"
                     +
                    "Input the ID in the format of a lower case letter followed by a hyphen and 2 digits, (eg. e-32)\n>> ");
                productID = ReadLine() ?? string.Empty;
                while (!CheckString(productID))
                {
                    Write("Please enter a productID in a valid format (e.g. e-32) >> ");
                    productID = ReadLine() ?? string.Empty;
                }
                products[i] = new Product(productID, productName, productQuantity, productPrice);          
            }
        }
        // Displays information of every product.
        public static void DisplayAllProducts(Product[] products)
        {
            WriteLine("\n*** List of all products ***");
            for(int i = 0; i < products.Length; i++)
            {
                Write("\n");
                WriteLine("------- Product {0} -------", i + 1);
                WriteLine(products[i].ToString());
            }
            Write("\n");
        }
        // Calculates the total revenue based on quantity and price of each product.
        public static double CalculateRevenue(Product[] products)
        {
            int quantity;
            double price;
            double revenue = 0;
            for (int i = 0; i < products.Length;i++)
            {
                quantity = products[i].ProductQuantity;
                price = products[i].ProductPrice;
                revenue += (quantity * price);
            }
            WriteLine("Total Revenue: {0:C2}\n", revenue);
            return revenue;
        }
        // Gets a list of products depending on category requested by user. 
        private static void GetProductLists(Product[] products)
        {
            string inputString;
            const string QUIT = "Q";
            WriteLine("Product categories include: Electronics (e-), Books (b-), Apparel (a-), Toys (t-), Others (o-)");
            // Main loop to continuously prompt user for category codes.
            do
            {
                Write("Enter a category code (e.g. b-) to display information for products in that category (or enter Q to quit) >> ");
                inputString = ReadLine() ?? string.Empty;
                // Check validity of category code entered.
                 while (!CheckCategoryCode(inputString) && inputString != QUIT)
                {
                    Write("Please enter a category code in a valid format (e.g., e-) or enter Q to quit >> ");
                    inputString = ReadLine() ?? string.Empty;
                } 

                if (inputString != QUIT)
                {
                    int productCount = 0;
                    // Display information for each product in the category.
                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i].ProductID.Substring(0, 2) == inputString)
                        {
                            Write("\n");
                            productCount++;
                            WriteLine(products[i].ToString());
                        }
                    } 
                    // Reassign inputString to the associated category name
                    for (int i = 0; i < Product.categoryCodes.Length; i++)
                    {
                        if(inputString == Product.categoryCodes[i])
                        {
                            inputString = Product.categoryNames[i];
                            break;
                        }
                    }
                    WriteLine("\nNumber of products in \"{0}\" category: {1}.\n", inputString, productCount);
                }
            } while (inputString != QUIT);
        }
    }
}

