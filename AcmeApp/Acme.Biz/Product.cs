using System;
 
namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        #region Constructors
        
        public Product()
        {
            Console.WriteLine("Product instance created");
        }

        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;

            Console.WriteLine("Product instance has a name: " + ProductName);
        }

        #endregion

        #region Properties

        
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        #endregion

        #region Methods

        public string SayHello()
        {
            return "Hello " + ProductName + " (" + ProductId + "): " + Description;
        }

        #endregion

    }
}
