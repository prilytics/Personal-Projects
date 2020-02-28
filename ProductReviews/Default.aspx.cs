using ProductReviews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews {
    public partial class _Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            BindGridView(); 
        }

        private void BindGridView()
        {
            try
            {
                using (ProductReviewEntities entities = new ProductReviewEntities())
                {
                    List<OfferStatu> offers = entities.OfferStatus.OrderByDescending(c=> c.OfferStatusID).ToList();
                    gvOfferStatus.DataSource = offers;
                    gvOfferStatus.DataBind();
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}