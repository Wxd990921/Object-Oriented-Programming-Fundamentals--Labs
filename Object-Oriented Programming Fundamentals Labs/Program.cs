using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Product> products = GenerateFixedProducts();

        foreach (Product product in products)
        {
            Console.WriteLine($"Product Name: {product.Name}, Price: {product.Price}, Code: {product.Code}");
        }

        // Create a new instance of the VendingMachine class
        VendingMachine vendingMachine = new VendingMachine();

        // Stock the inventory with the "Chocolate-covered Beans" product and quantity
        Console.WriteLine(vendingMachine.StockItem(new Product("Chocolate-covered Beans", 2, "A12"), 3));

        // Stock the money float with four $1 coins
        Console.WriteLine(vendingMachine.StockFloat(1, 4));

        // Perform the vending process by entering the code "A12" and inserting a $5 bill
        List<int> money = new List<int> { 5 };
        Console.WriteLine(vendingMachine.VendItem("A12", money));

        // Display the updated quantities in the inventory
        foreach (Product product in vendingMachine.GetInventoryProducts())
        {
            int quantity = vendingMachine.GetInventoryQuantity(product);
            Console.WriteLine($"Product Name: {product.Name}, Quantity: {quantity}, Code: {product.Code}");
        }

        // Display the updated quantities in the money float
        foreach (KeyValuePair<int, int> kvp in vendingMachine.GetMoneyFloat())
        {
            Console.WriteLine($"Denomination: ${kvp.Key}, Quantity: {kvp.Value}");
        }

        Console.ReadLine();
    }

    // Generate a list of fixed products
    static List<Product> GenerateFixedProducts()
    {
        List<Product> products = new List<Product>();

        // Create product instances and add them to the list
        Product product1 = new Product("Chocolate Bar", 1, "A01");
        Product product2 = new Product("Soda", 3, "B02");
        Product product3 = new Product("Chips", 5, "C03");
        Product product4 = new Product("Candy", 1, "D04");
        Product product5 = new Product("Cookies", 2, "E05");

        // Add products to the list
        products.Add(product1);
        products.Add(product2);
        products.Add(product3);
        products.Add(product4);
        products.Add(product5);

        return products;
    }

    // Represents a product in the vending machine
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }

        // Constructor to initialize product properties
        public Product(string name, int price, string code)
        {
            Name = name;
            Price = price;
            Code = code;
        }
    }

    // Represents a vending machine
    public class VendingMachine
    {
        private static int NextSerialNumber = 1;

        public int SerialNumber { get; }
        public string Barcode { get; }

        private Dictionary<int, int> MoneyFloat { get; }
        private Dictionary<Product, int> Inventory { get; }

        public VendingMachine()
        {
            SerialNumber = NextSerialNumber;
            NextSerialNumber++;

            Barcode = GenerateBarcode();

            MoneyFloat = new Dictionary<int, int>
            {
                { 1, 0 },
                { 2, 0 },
                { 5, 0 },
                { 10, 0 },
                { 20, 0 }
            };

            Inventory = new Dictionary<Product, int>();
        }

        private string GenerateBarcode()
        {
            string barcode = $"VM{SerialNumber.ToString("D5")}";
            return barcode;
        }

        public string StockItem(Product product, int quantity)
        {
            if (Inventory.ContainsKey(product))
            {
                Inventory[product] += quantity;
            }
            else
            {
                Inventory.Add(product, quantity);
            }

            return $"Added {quantity} {product.Name} ({product.Code}) for ${product.Price} each to inventory.";
        }

        public string StockFloat(int moneyDenomination, int quantity)
        {
            MoneyFloat[moneyDenomination] += quantity;

            string floatStock = "Money float: ";
            foreach (KeyValuePair<int, int> kvp in MoneyFloat)
            {
                floatStock += $"{kvp.Value} x ${kvp.Key}, ";
            }
            floatStock = floatStock.TrimEnd(',', ' ');

            return floatStock;
        }

        public string VendItem(string code, List<int> money)
        {
            Product product = Inventory.Keys.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                return $"Error: No item with code {code} found.";
            }

            int quantity;
            if (!Inventory.TryGetValue(product, out quantity))
            {
                return $"Error: {product.Name} ({product.Code}) is out of stock.";
            }

            int totalMoney = money.Sum();

            if (totalMoney < product.Price)
            {
                return $"Error: Insufficient funds provided. {product.Name} ({product.Code}) costs ${product.Price}.";
            }

            int change = totalMoney - product.Price;
            Dictionary<int, int> changeCoins = CalculateChange(change);

            if (!HasEnoughChange(changeCoins))
            {
                return $"Error: Not enough change available to complete the transaction. Please try again later.";
            }

            Inventory[product]--;
            foreach (KeyValuePair<int, int> kvp in changeCoins)
            {
                MoneyFloat[kvp.Key] -= kvp.Value;
            }

            string changeString = string.Join(", ", changeCoins.Select(kvp => $"{kvp.Value} x ${kvp.Key}"));
            return $"Enjoy your {product.Name} ({product.Code}) and take your change: {changeString}.";
        }

        private Dictionary<int, int> CalculateChange(int change)
        {
            Dictionary<int, int> changeCoins = new Dictionary<int, int>
            {
                { 20, 0 },
                { 10, 0 },
                { 5, 0 },
                { 2, 0 },
                { 1, 0 }
            };

            while (change > 0)
            {
                if (change >= 20 && MoneyFloat[20] > 0)
                {
                    change -= 20;
                    changeCoins[20]++;
                }
                else if (change >= 10 && MoneyFloat[10] > 0)
                {
                    change -= 10;
                    changeCoins[10]++;
                }
                else if (change >= 5 && MoneyFloat[5] > 0)
                {
                    change -= 5;
                    changeCoins[5]++;
                }
                else if (change >= 2 && MoneyFloat[2] > 0)
                {
                    change -= 2;
                    changeCoins[2]++;
                }
                else if (change >= 1 && MoneyFloat[1] > 0)
                {
                    change -= 1;
                    changeCoins[1]++;
                }
                else
                {
                    break;
                }
            }

            return changeCoins;
        }

        private bool HasEnoughChange(Dictionary<int, int> changeCoins)
        {
            foreach (KeyValuePair<int, int> kvp in changeCoins)
            {
                if (MoneyFloat[kvp.Key] < kvp.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Product> GetInventoryProducts()
        {
            return Inventory.Keys;
        }

        public int GetInventoryQuantity(Product product)
        {
            return Inventory.TryGetValue(product, out int quantity) ? quantity : 0;
        }

        public Dictionary<int, int> GetMoneyFloat()
        {
            return new Dictionary<int, int>(MoneyFloat);
        }
    }
}

