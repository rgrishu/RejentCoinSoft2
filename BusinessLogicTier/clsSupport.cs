using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataTier;

namespace BusinessLogicTier
{
    public class clsSupport
    {
        Data ObjData = new Data();

        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Messagetitle { get; set; }
        public string MessageDescription { get; set; }
        public string MentionBy { get; set; }
        public string ImageName { get; set; }
        public string Message { get; set; }
        public string TicketID { get; private set; }
        public int Status { get; set; }

        public DataTable getInbox(clsSupport objsupport, Boolean IsAdmin = false)
        {
            string str_query = "";
            //select t.*,ud.UserName,case  when Status = 1 then 'Open' else 'close' end as statustext from SupportTicket T
            //left join userdetail ud on ud.UserId = t.EntryBy
            //T(nolock) left join userdetail ud on ud.UserId = t.EntryBy  where t.EntryBy = '" + objsupport.ToId + "' order by t.EntryBy desc ";
            if (IsAdmin)
            {
                //str_query = "select t.*,ud.UserName,case  when Status = 1 then 'Open' else 'Close' end as statustext ,(select IIF(t.ModifyBy=999,'Admin',UserName) as UserName from userdetail where userId=t.ModifyBy) as LastReply from SupportTicket T (nolock) left join userdetail ud on ud.UserId = t.EntryBy  order by t.EntryBy desc";
                str_query = "select t.*,ud.UserName,case  when Status = 1 then 'Open' else 'Close' end as statustext ,IIF(t.ModifyBy=999,'Admin',(select UserName as UserName from userdetail where userId=t.ModifyBy) )as LastReply  from SupportTicket T (nolock) left join userdetail ud on ud.UserId = t.EntryBy  order by t.EntryBy desc";
            }
            else
            {
                str_query = "select t.*,ud.UserName,case  when Status = 1 then 'Open' else 'Close' end as statustext ,IIF(t.ModifyBy=999,'Admin',(select UserName as UserName from userdetail where userId=t.ModifyBy) ) as LastReply from SupportTicket T (nolock) left join userdetail ud on ud.UserId = t.EntryBy  where t.EntryBy = '" + objsupport.ToId + "' order by t.EntryBy desc";
            }


            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable getSentbox(clsSupport objsupport)
        {
            string str_query = "select * from SupportDetail where FromId='" + objsupport.FromId + "' order by mentiondate desc ";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        //select d.* , ud.UserName from SupportDetail D   left join userdetail ud on ud.UserId = d.MentionBy


        //public DataTable getMessage(int TicketID)
        //{
        //    string str_query = "select d.* ,IIF(d.MentionBy=999,'Admin',ud.UserName) as UserName from SupportDetail  D   left join userdetail ud on ud.UserId = d.MentionBy    where D.TicketID='" + TicketID + "' order by mentiondate desc ";

        //    DataTable dt = null;
        //    ObjData.StartConnection();
        //    try
        //    {
        //        dt = ObjData.RunDataTable(str_query);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //    }
        //    ObjData.EndConnection();
        //    return dt;
        //}


        public DataTable ViewTicket(clsSupport objsupport)
        {
            string str_query = "select * from SupportTicket where EntryBy='" + objsupport.FromId + "' order by mentiondate desc ";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable GetTicketAttachment(object p1, object p2)
        {
            throw new NotImplementedException();
        }

        public int SendMessage(clsSupport objsupport)
        {
            int i = 0;
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                s2 = " declare @id int; SELECT @id= isnull(max(id),0)+1 from SupportDetail  ; insert into SupportDetail (id, FromId ,ToId  ,MessageTitle ,MessageDescription, MentionBy  ,MentionDate  ) values (@id, '" + objsupport.FromId + "', '" + objsupport.ToId + "','" + objsupport.Messagetitle + "','" + objsupport.MessageDescription + "','" + objsupport.MentionBy + "',[dbo].[getIndianDate]()) ";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                i = 1;

                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }

        //public void InsertSupportTicketReply(string v, string pathdoc, object p)
        //{
        //    throw new NotImplementedException();
        //}

        public int SendMessageByAdmin(clsSupport objsupport)
        {
            int i = 0;
            string s2 = "";
            string s3 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans("select * from logindetail where MentionBy='" + objsupport.ToId + "'", tr);
                if (dt.Rows.Count > 0)
                {
                    s3 = "declare @Ticketid int; INSERT INTO SupportTicket (EntryBy, ModifyBy, EntryDate, ModifyDate, Status, Messagetitle) values ('" + objsupport.FromId + "','" + objsupport.FromId + "', [dbo].[getIndianDate](), [dbo].[getIndianDate](), 1, '" + objsupport.Messagetitle + "')  SELECT @Ticketid=SCOPE_IDENTITY() ";

                    s2 = " declare @id int; SELECT @id= isnull(max(id),0)+1 from SupportDetail  ; insert into SupportDetail (TicketID,id, FromId ,ToId  ,MessageTitle ,MessageDescription, MentionBy  ,MentionDate,imagename) values (@Ticketid,@id, '" + objsupport.FromId + "', '" + objsupport.ToId + "','" + objsupport.Messagetitle + "','" + objsupport.MessageDescription + "','" + objsupport.MentionBy + "',[dbo].[getIndianDate](),'" + objsupport.ImageName + "') ";
                    s2 = s3 + s2;
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    i = 1;
                }
                else
                {
                    i = 2;
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }


        public DataTable InsertSupportTicketReply(int userid, int TicketID, string MessageTitle, string MessageDescription, string str_image)
        {
            DataTable res = new DataTable();
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();


            try
            {

                s2 = "sp_ReplyTicket";
                SqlParameter[] parameter = {
                new SqlParameter("@userid", userid),
                new SqlParameter("@TicketID", TicketID),
                new SqlParameter("@MessageDescription", MessageDescription),
                new SqlParameter("@ImageName", str_image)

                };
                res = ObjData.RunDataTableProcedure(s2, parameter);

            }
            catch (Exception ex)
            {

                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();

            }
            return res;
        }
        public DataTable UpdateTicketStatus(int TicketID, int Status)
        {
            DataTable res = new DataTable();
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();


            try
            {

                s2 = "sp_CloseTicket";
                SqlParameter[] parameter = {
                new SqlParameter("@TicketID", TicketID),
                new SqlParameter("@Status", Status)
                };
                res = ObjData.RunDataTableProcedure(s2, parameter);

            }
            catch (Exception ex)
            {


            }
            finally
            {
                ObjData.EndConnection();

            }
            return res;
        }



        public DataTable getMessage(int userid, int TicketID)
        {
            DataTable res = new DataTable();
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            ObjData.StartConnection();

            try
            {

                s2 = "getreplymsg";
                SqlParameter[] parameter = {
                new SqlParameter("@userid", userid),
                new SqlParameter("@TicketID", TicketID),

                };
                res = ObjData.RunDataTableProcedure(s2, parameter);

            }
            catch (Exception ex)
            {


            }
            finally
            {
                ObjData.EndConnection();

            }
            return res;
        }

    }




}
