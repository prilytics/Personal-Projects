using DevExpress.Web;
using ProductReviews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class AgentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridView(); 
        }

        private void BindGridView()
        {
            try
            {
                using (ProductReviewEntities entities = new ProductReviewEntities())
                {
                    List<Agent> agents = entities.Agents.OrderBy(c => c.FullName).ToList();
                    gvAgent.DataSource = agents;
                    gvAgent.DataBind();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        protected void gvAgent_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        { 
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                try
                {
                    ASPxGridView gridView = new ASPxGridView(); 
                    Agent ag = new Agent();
                    ag.AgentEmail = e.NewValues["AgentEmail"] != null ? e.NewValues["AgentEmail"].ToString() : null;
                    ag.AgentNotes = e.NewValues["AgentNotes"] != null ? e.NewValues["AgentNotes"].ToString() : null;
                    ag.AgentPhone = e.NewValues["AgentPhone"] != null ? e.NewValues["AgentPhone"].ToString() : null;
                    ag.FullName = e.NewValues["FullName"] != null ? e.NewValues["FullName"].ToString() : null;
                    ag.LastModified = DateTime.Now;
                    ag.MessengerLink = e.NewValues["MessengerLink"] != null ? e.NewValues["MessengerLink"].ToString() : null;
                    ag.ModifiedBy = Page.User.Identity.Name;
                    ag.WhatsAppID = e.NewValues["WhatsAppID"] != null ? e.NewValues["WhatsAppID"].ToString() : null;  
                    entities.Agents.Add(ag);
                    entities.SaveChanges();
                    gridView.CancelEdit();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException eve)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var ev in eve.EntityValidationErrors)
                    {
                        sb.Append(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            ev.Entry.Entity.GetType().Name, ev.Entry.State));
                        foreach (var ve in ev.ValidationErrors)
                        {
                            sb.AppendLine(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }

                    //ExceptionUtility.LogException(new System.Data.Entity.Validation.DbEntityValidationException(sb.ToString()), "Error: Inserting Educator Scanner info, please check error logs for more details");
                }
                catch (System.Exception ex)
                {
                    var data = new { Error = ex.Message };
                    // ExceptionUtility.LogException(ex, "ManageUsers.gvEducatorScanners_RowInserting");
                }
            }
        }

        protected void btnEditAgent_Command(object sender, CommandEventArgs e)
        { 
            try
            {
                if (e.CommandArgument != null)
                {
                    switch (e.CommandName)
                    {
                        case "EditAgent":
                            string sRowIndex = e.CommandArgument.ToString();
                            int iRowIndex = Convert.ToInt32(sRowIndex);
                            gvAgent.StartEdit(iRowIndex);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                var data = new { Error = ex.Message };
                //ExceptionUtility.LogException(ex, "ManageUsers - btnEducatorUsers_Command");
            }
        }
        protected void gvAgent_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            e.Editor.ReadOnly = false;
        }
    }
}