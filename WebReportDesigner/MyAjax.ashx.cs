
using System;
using System.Collections.Generic;
using System.Web;


using jQuery.Plugins.jsTree.v3;


namespace WebReportDesigner
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für MyAjax
    /// </summary>
    public class MyAjax : IHttpHandler
    {


       

		public static string GetSQL(string strId, type_t EntityType)
        {
            string strSQL = null;
            
            strId = strId.Replace("'", "''");

			if(EntityType == type_t.NULL)
			{
            	strSQL = @"
SELECT 
	 site_uid AS obj_uid 
	,site_no || ' ' || site_text AS caption 
	,EXISTS(SELECT * FROM building WHERE building_site_uid = site_uid) AS HasChildren 
	,'" + type_t.SO.ToString() + @"' AS objtype 
FROM site 
ORDER BY CAST(site_no as integer) 
";

				return strSQL;
			}


			if(EntityType == type_t.SO)
            {
                strSQL = @"
SELECT 
	 building_uid AS obj_uid
	--,building_nr || ' - ' || building_text AS caption 
	--,'GB' || RIGHT('00' || building_no, 2) || ' - ' || building_text AS caption  -- PG 9.1+ only
    --,'GB' || substring('00' || building_no, 1 + char_length('00' || building_no) - 2) || ' - ' || building_text AS caption 
    ,'GB' || LPAD(building_no::text, 2, '0') || ' - ' || building_text AS caption -- much easier 

	,EXISTS(SELECT * FROM floor WHERE floor_building_uid = building_uid) AS HasChildren 
	,'" + type_t.GB.ToString() + @"' AS objtype 
FROM building 
WHERE building_site_uid = '" + strId + @"' 
ORDER BY building_nr 
";
				return strSQL;
            }

			if(EntityType == type_t.GB)
            {
                strSQL = @"

SELECT 
	 MAX(CAST(floor_uid AS varchar(36))) AS obj_uid 
	,floor_building_uid 
	
	,
	CASE 
		WHEN floortype_code = 'EG' THEN floortype_short_en 
		--ELSE floortype_short_en || RIGHT('00' || floor_no, 2)  -- PG 9.1+ only
        --ELSE floortype_short_en || substring('00' || floor_no, 1 + char_length('00' || floor_no) - 2) 
        ELSE floortype_short_en || LPAD(floor_no::text, 2, '0') -- much easier
	END AS caption 

	,0 AS HasChildren 
	,'" + type_t.GS.ToString() + @"' AS objtype 
	
	--,floor_isexterior
	--,floortype_sort
	--,floortype_multiplicatorno
	--,floortype_mez_sort 
FROM floor 

LEFT JOIN floortype 
	ON floortype_uid = floor_floortype_uid 
	
WHERE floor_building_uid = '" + strId + @"'

GROUP BY 
 floor_building_uid
,floor_no
,floortype_uid 
,floortype_code
,floortype_short_DE
,floortype_short_FR
,floortype_short_IT
,floortype_short_EN
,floortype_sort
,floortype_multiplicatorno
,floortype_mez_sort

ORDER BY 
	 floor_building_uid
    --,floor_isexterior 
	,floortype_sort
	,floortype_multiplicatorno * floor_no  
	,floortype_mez_sort
";

				return strSQL;
            }



            // ,ROW_NUMBER() OVER(ORDER BY SO_Nr, GB_Nr, GS_IsAussengeschoss, GST_Sort, GST_GS_NrMultiplikator * GS_Nr, GST_ZG_Sort) AS RPT_GS_Sort 
            // ,ROW_NUMBER() OVER(ORDER BY Site_No, Building_No, Floor_IsExterior, FloorType_Sort, FloorType_MultiplicatorNo * Floor_No, FloorType_Mez_Sort) AS Floor_Sort 
            // ,ROW_NUMBER() OVER(ORDER BY Floor_Building_UID, FloorType_Sort, FloorType_MultiplicatorNo * Floor_No, FloorType_Mez_Sort) AS Floor_Sort 

            if ("a" == "B".ToLower())
            {
                strSQL = @"
SELECT 
	 Site.Site_UID
	,Site.Site_No
	,Site.Site_Text
	
	,Building.Building_UID
	,Building.Building_Site_UID
	,Building.Building_Nr
	,Building.Building_Text
	,Building.Building_No
	
	,Floor.Floor_UID
	,Floor.Floor_Building_UID
	,Floor.Floor_No
	,Floor.Floor_Isexterior
	
	,FloorType.FloorType_UID
	,FloorType.FloorType_Code
	,FloorType.FloorType_Short_DE
	,FloorType.FloorType_Short_FR
	,FloorType.FloorType_Short_IT
	,FloorType.FloorType_Short_EN
	,FloorType.FloorType_long_DE
	,FloorType.FloorType_long_FR
	,FloorType.FloorType_long_IT
	,FloorType.FloorType_long_EN
	,FloorType.FloorType_Sort
	,FloorType.FloorType_MultiplicatorNo
	,FloorType.FloorType_Mez_Sort
	
	,ROW_NUMBER() OVER(ORDER BY Site_No, Building_No, Floor_IsExterior, FloorType_Sort, FloorType_MultiplicatorNo * Floor_No, FloorType_Mez_Sort) AS Floor_Sort 
    -- ,ROW_NUMBER() OVER(ORDER BY SO_Nr, GB_Nr, GS_IsAussengeschoss, GST_Sort, GST_GS_NrMultiplikator * GS_Nr, GST_ZG_Sort) AS RPT_GS_Sort 
    -- ,ROW_NUMBER() OVER(ORDER BY Floor_Building_UID, FloorType_Sort, FloorType_MultiplicatorNo * Floor_No, FloorType_Mez_Sort) AS Floor_Sort 
FROM Site 

LEFT JOIN Building 
	ON Building.Building_Site_UID = Site.Site_UID 
	
LEFT JOIN Floor 
	ON Floor.Floor_Building_UID = Building.Building_UID 
	
LEFT JOIN FloorType 
	ON FloorType.FloorType_UID = Floor.Floor_FloorType_UID 
	
ORDER BY 
 -- Site_No, Building_No, Floor_No 
  Floor_Sort 
";
            }

            // http://stackoverflow.com/questions/1481476/when-to-use-on-update-cascade

            // http://www.postgresql.org/docs/9.1/static/functions-string.html
            // http://www.w3resource.com/PostgreSQL/postgresql-string-functions-part1.php
            // http://www.w3resource.com/PostgreSQL/postgresql-string-functions-part3.php


            // http://de.wikipedia.org/wiki/Normalisierung_(Datenbank)
            // http://en.wikipedia.org/wiki/Fourth_normal_form

            return strSQL;
        }


		public enum type_t
		{
			NULL = 0,
			SO,
			GB,
			GS,
		}


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
			catch(Exception ex)
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


    } // End Class MyAjax : IHttpHandler


} // End Namespace WebReportDesigner 
