using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReviews.Data
{
    public class ProductReview
    {
        public string id { get; set; }
        public string externalId { get; set; }
        public long sortTimestamp { get; set; }
        public decimal rating { get; set; }
        public string visibility { get; set; }
        public int helpfulVotes { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string writtenInLanguageCode { get; set; }
        public string productAsin { get; set; }
        public string productTitle{ get; set; }
        public Nullable<bool> verifiedPurchase { get; set; }
        public Nullable<bool> vine { get; set; }
        public Nullable<bool> zebra { get; set; }
        public string type { get; set; }
    }
}