using Acme.Common;
using static Acme.Common.LoggingService;
using System.Collections.Generic;
using System;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {

        #region Constants and Readonly Fields
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;
        #endregion

        #region Constructors

        public Product()
        {
            this.MinimumPrice = .96m;
            this.Category = "Tools";
            //var colorOptions = new List<string>();
            //colorOptions.Add("Red");
            //colorOptions.Add("Espresso");
            //colorOptions.Add("White");
            //colorOptions.Add("Navy");
            //colorOptions.Insert(2, "Purple");
            //colorOptions.Remove("White");

            var colorOptions = new List<string>() { "Red", "Espresso", "White", "Navy" };

            Console.WriteLine(colorOptions);
        }

        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }

        #endregion

        #region Properties

        public decimal Cost { get; set; }


        private string productName;

        public string ProductName
        {
            get
            {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }
            }
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

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get
            {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }

                return productVendor;
            }
            set { productVendor = value; }
        }

        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";

        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;


        public string ValidationMessage { get; private set; }



        #endregion

        #region Methods
        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost.</param>
        /// <returns></returns>
        public OperationResult<decimal> CalculateSuggestedPrice(decimal markupPercent)
        {
            var message = "";

            if (markupPercent <= 0m)
            {
                message = "Invalid markup percentage";
            }
            else if (markupPercent < 10)
            {
                message = "Below recommended markup percentage";
            }
            var value = this.Cost + (this.Cost * markupPercent / 100);

            var operationResult = new OperationResult<decimal>(value, message);
            return operationResult;
        }


        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");
            var result = LogAction("saying Hello");

            return "Hello " + ProductName +
                " (" + ProductId + "): " +
                Description +
                " Available on: " +
                AvailabilityDate?.ToShortDateString();
        }

        public override string ToString() =>
            this.ProductName + " (" + this.ProductId + ")";
        

        #endregion

    }
}
