using ProductReviews.Data;
using ProductReviews.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews.Admin
{
    public partial class AmazonOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        { 
            if (ctlFileUpload.HasFile)
            {
                try
                {
                    string fileType = ctlFileUpload.FileName.Substring(ctlFileUpload.FileName.LastIndexOf(".") + 1).ToLower();

                    if (fileType.Equals("xlsx"))
                    {
                        List<OrderUploadStatus> orderUploadStatuses = processDocument(ctlFileUpload.PostedFile);
                        gvorderUploadStatuses.DataSource = orderUploadStatuses;
                        gvorderUploadStatuses.DataBind();
                    } 
                }
                catch (Exception ex)
                {
                    string exMessage = string.Format("Upload status: The file could not be uploaded due to the following error: " + ex.Message);
                }
            }
        }
         
        private List<OrderUploadStatus> processDocument(HttpPostedFile fileToProcess)
        {
            List<OrderUploadStatus> orderUploadStatuses = new List<OrderUploadStatus>();

            MemoryStream str = new MemoryStream();
            fileToProcess.InputStream.CopyTo(str);
            str.Position = 0;

            SLExcelReader reader = new SLExcelReader();
            SLExcelData dta = reader.ReadExcelFromStream(str);

            OrderUploadStatus oStatus = new OrderUploadStatus();
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                for (int i =1; i < dta.DataRows.Count; i++)
                {  
                    try
                    {
                        List<string> currentRow = dta.DataRows[i]; 
                        oStatus = UpsertOrder(entities, currentRow);
                        UpsertProduct(entities, currentRow, ref oStatus);
                        UpsertAgentOffer(entities, currentRow, ref oStatus);
                    }
                    catch (DbEntityValidationException e)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (var eve in e.EntityValidationErrors)
                        {
                            sb.Append(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage));
                            }
                        } 
                        oStatus.ErrorMessage = string.Format("ERROR: {0}", sb.ToString()); 
                    }
                    catch (Exception ex)
                    { 
                        oStatus.ErrorMessage = string.Format("ERROR: {0}", ex.Message);
                    }
                    orderUploadStatuses.Add(oStatus);
                }
            }
            return orderUploadStatuses;
        }

        private OrderUploadStatus UpsertOrder(ProductReviewEntities entities, List<string> currentRow)
        {
            OrderUploadStatus status = new OrderUploadStatus();
            string orderID = currentRow[1];
            status.AmazonOrderID = orderID; 
             
            if (entities.Orders.Where(e => e.AmazonOrderID == orderID).Any())
            {
                // update
                Order ao = entities.Orders.Where(e => e.AmazonOrderID == orderID).FirstOrDefault();
                ao.LastModified = DateTime.Now;
                ao.ModifiedBy = Page.User.Identity.Name;
                Double dOrderDate;
                if (Double.TryParse(currentRow[0], out dOrderDate))
                {
                    DateTime orderDate = DateTime.FromOADate(dOrderDate);
                    ao.OrderDate = orderDate;
                    status.OrderDate = orderDate;
                }
                ao.PaymentInfo = currentRow[14];
                Decimal dSubTotal;
                if (Decimal.TryParse(currentRow[27], out dSubTotal))
                {
                    ao.SubTotal = dSubTotal;
                }
                Decimal dTax;
                if (Decimal.TryParse(currentRow[28], out dTax))
                {
                    ao.Tax = dTax;
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    ao.Total = dTotal;
                }
                entities.SaveChanges();
                status.OrderStatus = "Updated";
                status.OrderID = ao.OrderID;
            }
            else
            {
                // insert
                Order ao = new Order();
                // temp agent assigned on new orders
                ao.AgentID = 1;
                ao.LastModified = DateTime.Now;
                ao.AmazonOrderID = orderID;
                ao.ModifiedBy = Page.User.Identity.Name;
                Double dOrderDate;
                if (Double.TryParse(currentRow[0], out dOrderDate))
                {
                    DateTime orderDate = DateTime.FromOADate(dOrderDate);
                    ao.OrderDate = orderDate;
                    status.OrderDate = orderDate;
                }
                ao.PaymentInfo = currentRow[14];
                Decimal dSubTotal;
                if (Decimal.TryParse(currentRow[27], out dSubTotal))
                {
                    ao.SubTotal = dSubTotal;
                }
                Decimal dTax;
                if (Decimal.TryParse(currentRow[28], out dTax))
                {
                    ao.Tax = dTax;
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    ao.Total = dTotal;
                }
                entities.Orders.Add(ao);
                entities.SaveChanges();
                status.OrderStatus = "Inserted";
                status.OrderID = ao.OrderID;
            }
            return status;
        }
        private void UpsertProduct(ProductReviewEntities entities, List<string> currentRow, ref OrderUploadStatus status)
        { 
            string amazonOrderID = currentRow[1];
            string isbn = currentRow[4];
            int orderID = status.OrderID;

            if (entities.Products.Where(e => e.OrderID == orderID && e.ASIN_ISBN == isbn).Any())
            {
                // update
                Product ap = entities.Products.Where(e => e.OrderID == orderID && e.ASIN_ISBN == isbn).FirstOrDefault();
                ap.LastModified = DateTime.Now;
                ap.ModifiedBy = Page.User.Identity.Name;
                ap.ASIN_ISBN = currentRow[4];
                Decimal dSubTotal;
                if (Decimal.TryParse(currentRow[27], out dSubTotal))
                {
                    ap.ItemSubtotal = dSubTotal;
                }
                Decimal dTax;
                if (Decimal.TryParse(currentRow[28], out dTax))
                {
                    ap.ItemTax = dTax;
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    ap.ItemTotal = dTotal;
                }
                Decimal dListPrice;
                if (Decimal.TryParse(currentRow[11], out dListPrice))
                {
                    ap.ListPrice = dListPrice;
                }
                ap.OrderID = status.OrderID;
                ap.ProductCategory = currentRow[3];
                ap.ProductTitle = currentRow[2];
                Decimal dPurchasePrice;
                if (Decimal.TryParse(currentRow[12], out dPurchasePrice))
                {
                    ap.ListPrice = dPurchasePrice;
                }
                Decimal dQuantity;
                if (Decimal.TryParse(currentRow[13], out dQuantity))
                {
                    ap.ListPrice = dQuantity;
                }
                ap.Seller = currentRow[9];
                ap.ShipStatus = currentRow[25];
                ap.TrackingInfo = currentRow[26];
                ap.UNSPSC_Code = currentRow[5];
                entities.SaveChanges();
                status.ProductStatus = "Updated";
                status.ProductTitle = ap.ProductTitle;
                status.ProductID = ap.ProductID;
            }
            else
            {
                // insert
                Product ap = new Product();
                ap.LastModified = DateTime.Now;
                ap.ModifiedBy = Page.User.Identity.Name;
                ap.ASIN_ISBN = currentRow[4];
                Decimal dSubTotal;
                if (Decimal.TryParse(currentRow[27], out dSubTotal))
                {
                    ap.ItemSubtotal = dSubTotal;
                }
                Decimal dTax;
                if (Decimal.TryParse(currentRow[28], out dTax))
                {
                    ap.ItemTax = dTax;
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    ap.ItemTotal = dTotal;
                }
                Decimal dListPrice;
                if (Decimal.TryParse(currentRow[11], out dListPrice))
                {
                    ap.ListPrice = dListPrice;
                }
                ap.OrderID = orderID;
                ap.ProductCategory = currentRow[3];
                ap.ProductTitle = currentRow[2];
                Decimal dPurchasePrice;
                if (Decimal.TryParse(currentRow[12], out dPurchasePrice))
                {
                    ap.ListPrice = dPurchasePrice;
                }
                Decimal dQuantity;
                if (Decimal.TryParse(currentRow[13], out dQuantity))
                {
                    ap.ListPrice = dQuantity;
                }
                ap.Seller = currentRow[9];
                ap.ShipStatus = currentRow[25];
                ap.TrackingInfo = currentRow[26];
                ap.UNSPSC_Code = currentRow[5];
                entities.Products.Add(ap);
                entities.SaveChanges();
                status.ProductStatus = "Inserted";
                status.ProductTitle = ap.ProductTitle;
                status.ProductID = ap.ProductID;
            }
        }
        private void UpsertAgentOffer(ProductReviewEntities entities, List<string> currentRow, ref OrderUploadStatus status)
        {
            int productID = status.ProductID;
            if (entities.OfferStatus.Where(e => e.ProductID == productID).Any())
            {
                // update
                OfferStatu os = entities.OfferStatus.Where(e => e.ProductID == productID).OrderByDescending(o => o.OrderDate).FirstOrDefault();
                Double dOrderDate;
                if (Double.TryParse(currentRow[0], out dOrderDate))
                {
                    DateTime orderDate = DateTime.FromOADate(dOrderDate);
                    os.OrderDate = orderDate; 
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    os.ProductCost = dTotal;
                }  
                entities.SaveChanges();
                status.OfferStatus = "Updated";
            }
            else
            {
                // insert
                OfferStatu os = new OfferStatu();
                os.AgentID = 1;
                os.OfferType = "Not Set";
                os.ProductID = status.ProductID;
                Double dOrderDate;
                if (Double.TryParse(currentRow[0], out dOrderDate))
                {
                    DateTime orderDate = DateTime.FromOADate(dOrderDate);
                    os.OrderDate = orderDate;
                    os.OfferDate = orderDate;
                }
                Decimal dTotal;
                if (Decimal.TryParse(currentRow[29], out dTotal))
                {
                    os.ProductCost = dTotal;
                } 
                entities.OfferStatus.Add(os);
                entities.SaveChanges();
                status.OfferStatus = "Inserted";
            } 
        }
    }
}