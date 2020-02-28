using DevExpress.Web;
using ProductReviews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews.Admin
{
    public partial class Refunds : System.Web.UI.Page
    { 
        public int RefundID
        {
            get { return Convert.ToInt32(Session["RefundID"] ?? 0); }
            set { Session["RefundID"] = value; }
        }
        public int ProductID
        {
            get { return Convert.ToInt32(Session["ProductID"] ?? 0); }
            set { Session["ProductID"] = value; }
        }
        public int Varience
        {
            get { return Convert.ToInt32(Session["Varience"] ?? 15); }
            set { Session["Varience"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRefundsGridView();
                var refundID = Request.QueryString["rid"];
                var productID = Request.QueryString["pid"];
                int rid = 0;
                int pid = 0;
                if (refundID != null && productID == null)
                {
                    if (int.TryParse(refundID, out rid))
                    {
                        RefundID = rid;
                    }
                }
                if (refundID != null && productID != null)
                {
                    if (int.TryParse(refundID, out rid))
                    {
                        RefundID = rid;
                        if (int.TryParse(productID, out pid))
                        {
                            ProductID = pid;
                        }
                    }
                }

                if (RefundID != 0 && ProductID != 0)
                {
                    SetProductIdOnRefund(ProductID);
                }
                if (RefundID != 0)
                {
                    BindPossibleProductsGridView();
                }
                txtVariance.Text = Varience.ToString();
            }
            if (Page.IsCallback)
            {
                BindRefundsGridView();
                if (RefundID != 0)
                {
                    BindPossibleProductsGridView();
                }
            }
        }

        private void BindPossibleProductsGridView()
        {
            try
            {
                using (ProductReviewEntities entities = new ProductReviewEntities())
                {
                    List<Refund> refunds = entities.Refunds.Where(r => r.RefundID == RefundID).ToList();
                    gvRefunds.DataSource = refunds;
                    gvRefunds.DataBind();

                    int minMultiplier = 0;
                    int maxMultiplier = 0;
                    minMultiplier = 100 - Varience;
                    maxMultiplier = 100 + Varience;
                     
                    var minSubTotal = (refunds.FirstOrDefault().Amount.Value * minMultiplier) / 100;
                    var maxSubTotal = (refunds.FirstOrDefault().Amount.Value * maxMultiplier) / 100;
                    List<vRefundsNeedMappedToProduct> possibleProducts = entities.vRefundsNeedMappedToProducts.Where(r => r.ItemSubtotal > minSubTotal && r.ItemSubtotal < maxSubTotal).OrderByDescending(c => c.ItemSubtotal).ToList();
                    gvPossibleProducts.DataSource = possibleProducts;
                    gvPossibleProducts.DataBind();
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        private void SetProductIdOnRefund(int productID)
        { 
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                Refund refund = entities.Refunds.Where(r => r.RefundID == RefundID).FirstOrDefault();
                refund.ProductID = productID;
                entities.SaveChanges();
            }
        }
        protected void hyperLinkProductID_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (templateContainer.KeyValue != null)
            {
                int productID = Convert.ToInt32(templateContainer.KeyValue);
                string linkUrl = string.Format("/Admin/Refunds.aspx?rid={0}&pid={1}", RefundID, productID);
                string js = string.Format("window.open('{0}','_self')", linkUrl);

                link.NavigateUrl = string.Format("javascript:{0};", js);
                link.Text = string.Format("{0}", productID); 
            }
        }

        protected void hyperLinkMessenger_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;
            int rowVisibleIndex = templateContainer.VisibleIndex;
            string msgLink = string.Empty;
            if (templateContainer.Grid.GetRowValues(rowVisibleIndex, "MessengerLink") != null)
            {
                msgLink = templateContainer.Grid.GetRowValues(rowVisibleIndex, "MessengerLink").ToString();
            }
            if (!string.IsNullOrEmpty(msgLink))
            {
                string js = string.Format("window.open('//{0}')", msgLink);
                link.NavigateUrl = "javascript:void(0);";
                link.Text = msgLink;
                link.ClientSideEvents.Click = string.Format("function(s, e) {{ {0} }}", js);
            }
        }

        private void BindRefundsGridView()
        {
            try
            {
                using (ProductReviewEntities entities = new ProductReviewEntities())
                {
                    List<Refund> offers = entities.Refunds.Where(r => r.ProductID == 1 && r.RefundID != 46).OrderByDescending(c => c.RefundID).ToList();
                    gvRefunds.DataSource = offers;
                    gvRefunds.DataBind();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        protected void hyperLink_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (templateContainer.KeyValue != null)
            {
                int refundID = Convert.ToInt32(templateContainer.KeyValue);
                string linkUrl = string.Format("/Admin/Refunds.aspx?rid={0}", refundID); 
                string js = string.Format("window.open('{0}','_self')", linkUrl);

                link.NavigateUrl = string.Format("javascript:{0};", js);
                link.Text = string.Format("{0}", refundID); 
            }
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            RefundID = 0;
            ProductID = 0;
            Page.Response.Redirect("/Admin/Refunds.aspx");
        }
         

        protected void btnChangeVariance_Click(object sender, EventArgs e)
        { 
            int v = 0;
            if (int.TryParse(txtVariance.Text, out v))
            {
                Varience = v;
                BindPossibleProductsGridView();
            }
        }
    }
}