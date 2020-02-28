//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductReviews.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferStatu
    {
        public int OfferStatusID { get; set; }
        public System.DateTime OfferDate { get; set; }
        public int AgentID { get; set; }
        public int ProductID { get; set; }
        public string OfferType { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<System.DateTime> ReviewSubmittedDate { get; set; }
        public Nullable<System.DateTime> ReviewLiveDate { get; set; }
        public Nullable<System.DateTime> RefundRequestedDate { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<decimal> ProductCost { get; set; }
        public Nullable<decimal> RefundedAmount { get; set; }
        public string OfferNotes { get; set; }
        public Nullable<int> RefundID { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
        public virtual Refund Refund { get; set; }
    }
}
