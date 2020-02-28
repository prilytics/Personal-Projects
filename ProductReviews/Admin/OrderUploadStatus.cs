using System;

namespace ProductReviews.Admin
{
    internal class OrderUploadStatus
    {
        public DateTime OrderDate { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string AmazonOrderID { get; set; }
        public string ProductTitle { get; set; }
        public string OrderStatus { get; set; }
        public string ProductStatus { get; set; }
        public string OfferStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}