using ProductReviews.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Helpers;

namespace ProductReviews.Admin
{
    public partial class UpdateAgentsWithMessenger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessOrders();
        }

        private void ProcessOrders()
        {
            List<MessengerInfoOrderID> msgInfoStatus = new List<MessengerInfoOrderID>();
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                List<Order> ords = entities.Orders.Where(po => po.AgentID == 1 && po.OrderID == 173).OrderByDescending(o=>o.OrderID).ToList();
                foreach (Order o in ords)
                {
                    string aoid = o.AmazonOrderID;
                    MessengerInfo msgInfo = GetAgentInfo(aoid);
                    MessengerInfoOrderID mio = new MessengerInfoOrderID(msgInfo);
                    try
                    {
                        if (msgInfo.MessengerID == null || msgInfo.MessengerID == string.Empty)
                        {
                            mio.UpdateStatus = string.Format("Order ID {0} was not found when searching.", o.AmazonOrderID);
                        }
                        else
                        {
                            string status = UpdateDB(msgInfo, o.OrderID, o.AgentID);
                            mio.UpdateStatus = string.Format("SUCCESSFUL ({0}) - Messenger Name: {1}, Amazon Order ID: {2}", status, msgInfo.MessengerName, o.AmazonOrderID);
                        }
                        mio.OrderID = o.OrderID;
                        msgInfoStatus.Add(mio);
                    }
                    catch (Exception ex)
                    {
                        mio.UpdateStatus = string.Format("ERROR: {0}", ex.Message);
                        mio.OrderID = o.OrderID;
                        msgInfoStatus.Add(mio);
                    }
                }
            }
            gvagentStatuses.DataSource = msgInfoStatus;
            gvagentStatuses.DataBind();
        }

        private string UpdateDB(MessengerInfo msgInfo, int orderID, int? agentID)
        {
            string status = string.Empty;
            using (ProductReviewEntities entities = new ProductReviewEntities())
            {
                if (agentID.HasValue && agentID.Value > 1)
                { 
                    if (entities.Agents.Any(a => a.AgentID == agentID))
                    {
                        Agent ag = entities.Agents.Where(a => a.AgentID == agentID).FirstOrDefault(); 
                        ag.MessengerID = msgInfo.MessengerID;
                        ag.MessengerIsUser = msgInfo.MessengerIsUser;
                        ag.MessengerIsVerified = msgInfo.MessengerIsVerified;
                        ag.MessengerName = msgInfo.MessengerName;
                        ag.MessengerType = msgInfo.MessengerType;
                        ag.MessengerUserName = msgInfo.MessengerUserName;
                        ag.AgentNotes = string.Format("{0}; Order ID {1} -- {2}", ag.AgentNotes, orderID, msgInfo.ThreadName);
                        entities.SaveChanges();
                        status = "agent updated";
                    }
                }
                else
                {
                    if (msgInfo.MessengerID != null)
                    {
                        if (entities.Agents.Any(a => a.MessengerID == msgInfo.MessengerID || a.MessengerLink.Contains(msgInfo.MessengerID)))
                        {
                            Agent ag = entities.Agents.Where(a => a.MessengerID == msgInfo.MessengerID || a.MessengerLink.Contains(msgInfo.MessengerID)).FirstOrDefault();
                            agentID = ag.AgentID;
                            ag.MessengerID = msgInfo.MessengerID;
                            ag.MessengerIsUser = msgInfo.MessengerIsUser;
                            ag.MessengerIsVerified = msgInfo.MessengerIsVerified;
                            ag.MessengerName = msgInfo.MessengerName;
                            ag.MessengerType = msgInfo.MessengerType;
                            ag.MessengerUserName = msgInfo.MessengerUserName;
                            ag.AgentNotes = string.Format("{0}; Order ID {1} -- {2}", ag.AgentNotes, orderID, msgInfo.ThreadName);
                            entities.SaveChanges();
                            status = "agent updated";
                        }
                        else
                        {
                            Agent ag = new Agent();
                            ag.MessengerID = msgInfo.MessengerID;
                            ag.MessengerIsUser = msgInfo.MessengerIsUser;
                            ag.MessengerIsVerified = msgInfo.MessengerIsVerified;
                            ag.MessengerName = msgInfo.MessengerName;
                            ag.MessengerType = msgInfo.MessengerType;
                            ag.MessengerUserName = msgInfo.MessengerUserName;
                            ag.AgentNotes = string.Format("Order ID {1} -- {2}", ag.AgentNotes, orderID, msgInfo.ThreadName);
                            if (msgInfo.MessengerUserName != null && msgInfo.MessengerUserName != string.Empty)
                            {
                                ag.MessengerLink = string.Format("m.me/{0}", msgInfo.MessengerUserName);
                            }
                            else
                            {
                                ag.MessengerLink = string.Format("m.me/{0}", msgInfo.MessengerID);
                            }
                            ag.ModifiedBy = "System Account";
                            ag.LastModified = DateTime.Now;
                            entities.Agents.Add(ag);
                            entities.SaveChanges();
                            status = "agent inserted";
                            agentID = ag.AgentID;
                        }
                    }


                    Order ord = entities.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
                    ord.AgentID = agentID.Value;
                    entities.SaveChanges();
                    status += " and order updated";
                }
            }
            return status;
        } 
        private MessengerInfo GetAgentInfo(string orderID)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.facebook.com/api/graphqlbatch/");
            var postData = "__user=1062948409&__a=1&__dyn=7AgNeQ4qmfxd2u6Xolg9odoKEW74jFwgoqzob4q2i5U4e2C3Cm7WUC3q2OUuKewXyE4mdwJKdx3wnoOEiwvUe8hxG18wzwxgeGwbqq0FEhKEtxy5U4m1owHwbG78-2u1Kwwx62W7Uy11Ax2fz9rw4XG7VXAy8aEaoGqfwl8cErwlU9kbxS7K0gG2-2WE9Ejw822KfxW686-1dwoU&__csr=&__req=1f&__beoa=0&__pc=PHASED%3Amessengerdotcom_pkg&dpr=1.5&__rev=1001747481&__s=m768lj%3A8olsg8%3Ab0lfrj&__hsi=0-0&__comet_req=0&fb_dtsg=AQErn74dmJZ5%3AAQH2Rlna5lMZ&jazoest=22023&queries=%7B%22o0%22%3A%7B%22doc_id%22%3A%222706854202719826%22%2C%22query_params%22%3A%7B%22search_query%22%3A%22[[ORDER_ID]]%22%2C%22result_limit%22%3A3%2C%22exclude_ids%22%3A[]%2C%22context%22%3A%22web_messenger_composer%22%7D%7D%7D";
            string pd = postData.Replace("[[ORDER_ID]]", orderID); 
            var data = Encoding.ASCII.GetBytes(pd); 
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0"; 
            request.Headers["Cookie"] = "sb=Z04LXmCA8elbX_adh6evRlsP; datr=aE4LXuJwuhAlIaTFyWEAJ6Ti; c_user=1062948409; xs=25%3AVUgMc7QsMbPuFg%3A2%3A1577799276%3A1386%3A2662; dpr=1.25; wd=1536x775; presence=EDvF3EtimeF1582386876EuserFA21062948409A2EstateFDutF0CEchFDp_5f1062948409F0CC";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length); 
            } 

            var response = (HttpWebResponse)request.GetResponse();
                    
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            string[] sepratorNewLine = new string[1];
            sepratorNewLine[0] ="\r\n";
            string[] responseParts = responseString.Split(sepratorNewLine, StringSplitOptions.None); 
            dynamic d = Json.Decode(responseParts[0]);
            MessengerInfo mi = new MessengerInfo();


            var errors = d.o0.errors;

            if (errors != null && errors.Length > 0)
            {
                mi.MessengerIsUser = false;
            }
            else
            {
                var array = d.o0.data.entities_named.search_results.edges as DynamicJsonArray;
                if (array != null && array.Length > 0)
                {
                    int i = 0; 
                    var ma_uname = d.o0.data.entities_named.search_results.edges[i].node.message_thread.thread.all_participants.nodes[0].messaging_actor.username;
                    var ma_name = d.o0.data.entities_named.search_results.edges[i].node.message_thread.thread.all_participants.nodes[0].messaging_actor.name;
                    if (ma_uname == "Jeff Meredith" || ma_name == "Jeff Meredith")
                    {
                        i++;
                    }
                    mi.MessengerIsUser = true;
                    mi.ThreadName = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.thread_name;
                    mi.MessengerIsUser = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.is_messenger_user;
                    mi.MessengerIsVerified = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.is_verified;
                    if (i == 0)
                    {
                        mi.MessengerID = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].id;
                    }
                    else
                    {
                        mi.MessengerID = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.id;
                    }
                    mi.MessengerName = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.name;
                    mi.MessengerUserName = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.username;
                    mi.MessengerType = d.o0.data.entities_named.search_results.edges[0].node.message_thread.thread.all_participants.nodes[i].messaging_actor.__typename;
                }
                else
                {
                    mi.MessengerIsUser = false;
                }
            }
            return mi;
        } 
    }
}