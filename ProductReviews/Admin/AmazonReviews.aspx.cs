using IronWebScraper;
using ProductReviews.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews.Admin
{
    public partial class AmazonReviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var url = "https://www.amazon.com/";
                var cookie = "";
                txtCookie.Text = cookie;
                txtUrl.Text = url;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string acceptEncoding = "identity";
            string xRequestedWith = "XMLHttpRequest";
            string accept = "*/*";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
            var request = (HttpWebRequest)WebRequest.Create(txtUrl.Text); 
            request.UserAgent = userAgent;
            request.Accept = accept;
            request.Headers["Cookie"] = txtCookie.Text;
            request.Headers["Accept-Encoding"] = acceptEncoding;
            request.Headers["X-Requested-With"] = xRequestedWith;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dynamic d = Json.Decode(json);
            List<ProductReview> reviews = new List<ProductReview>();

            var array = d.Contributions as DynamicJsonArray;
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    ProductReview pr = new ProductReview();
                    pr.externalId = array[i].externalId;
                    pr.helpfulVotes = array[i].helpfulVotes;
                    pr.id = array[i].id;
                    pr.productAsin = array[i].product.asin;
                    pr.productTitle = array[i].product.title;
                    pr.rating = array[i].rating;
                    pr.sortTimestamp = array[i].sortTimestamp;
                    pr.text = array[i].text;
                    pr.title = array[i].title;
                    pr.type = array[i].type;
                    pr.verifiedPurchase = array[i].verifiedPurchase;
                    pr.vine = array[i].vine;
                    pr.visibility = array[i].visibility;
                    pr.writtenInLanguageCode = array[i].writtenInLanguageCode;
                    pr.zebra = array[i].zebra;
                    reviews.Add(pr);
                }
            }

            UpdateDB(reviews);
        }


        private void UpdateDB(List<ProductReview> reviews)
        {
            string status = string.Empty;
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                List<Product> products = entities.Products.ToList();
                foreach (ProductReview pr in reviews)
                {
                    if (entities.Reviews.Any(a => a.ExternalId == pr.externalId))
                    {
                        Review r = entities.Reviews.Where(a => a.ExternalId == pr.externalId).FirstOrDefault(); 
                        r.ExternalId = pr.externalId;
                        r.Headline = pr.title;
                        r.LastModified = DateTime.Now;
                        r.OverallRating = Convert.ToDouble(pr.rating);
                        r.ProductASIN = pr.productAsin;
                        r.ProductTitle = pr.productTitle;
                        r.PublicReviewLink =  string.Format("https://www.amazon.com/gp/customer-reviews/{0}", pr.externalId);
                        r.ReviewDetail = pr.text;
                        r.Reviewer = "Jeff";
                        r.ReviewType = pr.type;
                        r.VerifiedPurchase = pr.verifiedPurchase;
                        r.Vine = pr.vine;
                        r.Visibility = pr.visibility;
                        r.Zebra = pr.zebra;
                        entities.SaveChanges(); 
                    }
                    else
                    {
                        Review r = new Review();
                        r.ExternalId = pr.externalId;
                        r.Headline = pr.title;
                        r.LastModified = DateTime.Now;
                        r.OverallRating = Convert.ToDouble(pr.rating);
                        r.ProductASIN = pr.productAsin;
                        r.ProductTitle = pr.productTitle;
                        r.PublicReviewLink = string.Format("https://www.amazon.com/gp/customer-reviews/{0}", pr.externalId);
                        r.ReviewDetail = pr.text;
                        r.Reviewer = "Jeff";
                        r.ReviewType = pr.type;
                        r.VerifiedPurchase = pr.verifiedPurchase;
                        r.Vine = pr.vine;
                        r.Visibility = pr.visibility;
                        r.Zebra = pr.zebra;
                        if (products.Any(d => d.ASIN_ISBN.Contains(pr.productAsin)))
                        {
                            Product p = products.Where(d => d.ASIN_ISBN.Contains(pr.productAsin)).OrderByDescending(o => o.ModifiedBy).FirstOrDefault();
                            r.ProductID = p.ProductID;
                        }
                        else
                        {
                            r.ProductID = 1;
                        }
                        entities.Reviews.Add(r);
                        entities.SaveChanges(); 
                    }
                }

            }
        }
    }
}