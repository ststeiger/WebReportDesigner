
using System;
using System.Collections.Generic;
using System.Web;


namespace Tools.Serialization
{


	public class JSON
    {
        

		public static T Deserialize<T>(string strValue)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strValue);
		}


		//Cynosura.Base.JsonHelper.SerializeUnpretty(target)
		public static string SerializeUnpretty(object target)
		{
			return SerializeUnpretty(target, null);
		}

		public static string SerializeUnpretty(object target, string strCallback)
		{
			return Serialize(target, false, null);
		}


		//Cynosura.Base.JsonHelper.SerializePretty(target)
		public static string SerializePretty(object target)
		{
			return SerializePretty(target, null);
		}


		public static string SerializePretty(object target, string strCallback)
		{
			return Serialize(target, true, strCallback);
		}

		public static string Serialize(object target)
		{
			#if DEBUG
			return Serialize(target, true);
			#else
			return Serialize(target, false);
			#endif
		}

		public static string Serialize(object target, bool prettyPrint)
		{
			return Serialize(target, prettyPrint, null);
		}


		public static string Serialize(object target, bool prettyPrint, string strCallback)
		{
			string strResult = null;

			// http://james.newtonking.com/archive/2009/10/23/efficient-json-with-json-net-reducing-serialized-json-size.aspx
			Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };
			if (!prettyPrint) {
				settings.Formatting = Newtonsoft.Json.Formatting.None;
			} else {
				settings.Formatting = Newtonsoft.Json.Formatting.Indented;
			}


			settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;

			//context.Response.Write(strCallback + " && " + strCallback + "(")

			if (string.IsNullOrEmpty(strCallback)) {
				strResult = Newtonsoft.Json.JsonConvert.SerializeObject(target, settings);
			// JSONP
			} 
			else 
			{
				// https://github.com/visionmedia/express/pull/1374
				//strResult = strCallback + " && " + strCallback + "(" + Newtonsoft.Json.JsonConvert.SerializeObject(target, settings) + "); " + Environment.NewLine
				//typeof bla1 != "undefined" ? alert(bla1(3)) : alert("foo undefined");
				strResult = "typeof " + strCallback + " != 'undefined' ? " + strCallback + "(" + Newtonsoft.Json.JsonConvert.SerializeObject(target, settings) + ") : alert('Callback-Funktion \"" + strCallback + "\" undefiniert...'); " + Environment.NewLine;
			}

			settings = null;
			return strResult;



			//Dim sbSerialized As New StringBuilder()
			//Dim js As New JavaScriptSerializer()

			//js.Serialize(target, sbSerialized)

			//If prettyPrint Then
			//    Dim prettyPrintedResult As New StringBuilder()
			//    prettyPrintedResult.EnsureCapacity(sbSerialized.Length)

			//    Dim pp As New JsonPrettyPrinter()
			//    pp.PrettyPrint(sbSerialized, prettyPrintedResult)

			//    Return prettyPrintedResult.ToString()
			//Else
			//    Return sbSerialized.ToString()
			//End If
		}
    }
}