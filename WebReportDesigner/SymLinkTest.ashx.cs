
using System;
using System.Collections.Generic;
using System.Web;


using jQuery.Plugins.jsTree.v3;


namespace WebReportDesigner
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für SymLinkTest
    /// </summary>
    public class SymLinkTest : IHttpHandler
    {


        public enum type_t
        {
            NULL = 0,
            Folder,
            File,
            Symlink,
        } // End enum type_t 


        public static string GetSQL(string strId, type_t EntityType)
        {
            string strSQL = null;

            strId = strId.Replace("'", "''");

            if (EntityType == type_t.NULL)
            {
                strSQL = @"
SELECT 
	 site_uid AS obj_uid 
	,site_no || ' ' || site_text AS caption 
	,EXISTS(SELECT * FROM building WHERE building_site_uid = site_uid) AS HasChildren 
	,'" + type_t.Folder.ToString() + @"' AS objtype 
FROM site 
ORDER BY CAST(site_no as integer) 
";

                return strSQL;
            }

            return "";
        } // End Function GetSQL


        public void ProcessRequest(HttpContext context)
        {
            string strJSON = null;

            try
            {
                string strId = context.Request.Params["id"];
                string strData = context.Request.Params["data"];


                type_t EntityType = (type_t)Enum.Parse(typeof(type_t), strData, true);

                System.Diagnostics.Debug.WriteLine(strId);
                System.Diagnostics.Debug.WriteLine(strData);
                System.Diagnostics.Debug.WriteLine(EntityType);

                
                string strSQL = GetSQL(strId, EntityType);
                System.Data.DataTable dt = SQL.GetDataTable(strSQL);


                List<TreeItem> ls = new List<TreeItem>();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    TreeItem root = new TreeItem();
                    root.id = System.Convert.ToString(dr["obj_uid"]);
                    root.text = System.Convert.ToString(dr["caption"]);
                    root.children = System.Convert.ToBoolean(dr["HasChildren"]);
                    root.state = NodeState.closed;
                    root.data = System.Convert.ToString(dr["objtype"]);

                    // root.type = 123;
                    // root.type = type_t.SO;
                    // root.type = (type_t)Enum.Parse(typeof(type_t), System.Convert.ToString(dr["objtype"]));
                    root.type = System.Convert.ToString(dr["objtype"]);

                    // root.a_attr.target = "_blank";
                    // root.a_attr.href = "http://127.0.0.1";

                    ls.Add(root);
                } // Next dr 

                strJSON = Tools.Serialization.JSON.Serialize(ls);
                // List<TreeItem > obj = Tools.Serialization.JSON.Deserialize<List<TreeItem >>(strJSON);
                // Console.WriteLine(obj);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Tools.AJAX.cAjaxResult AjaxResult = new Tools.AJAX.cAjaxResult();
                AjaxResult.error = new Tools.AJAX.AJAXException(ex);
                strJSON = Tools.Serialization.JSON.Serialize(AjaxResult);
            }


            System.Diagnostics.Debug.WriteLine(strJSON);
            context.Response.ContentType = "application/json";
            context.Response.Write(strJSON);
        } // End Sub ProcessRequest 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        } // End Property IsReusable 


    } // End Class SymLinkTest


} // End Namespace WebReportDesigner 
