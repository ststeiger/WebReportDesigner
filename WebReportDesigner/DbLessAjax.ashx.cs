
namespace WebReportDesigner
{
	using System;
	using System.Collections.Generic;

	using jQuery.Plugins.jsTree.v3;


	public class DbLessAjax : System.Web.IHttpHandler
	{


		public bool IsReusable {
			get {
				return false;
			}
		}


		public void ProcessRequest (System.Web.HttpContext context)
		{
			string strJSON = null;

			try
			{
				string strId = context.Request.Params["id"];
				System.Diagnostics.Debug.WriteLine(strId);

				List<TreeItem> ls = new List<TreeItem>();

				TreeItem root = new TreeItem();

				if(string.IsNullOrEmpty(strId))
				{
					root.id="1";
					root.text = "RootNode ";
				}
				else
				{
					root.id = strId + ".1";
					root.text = "Node " + strId;
				}
				// root.state = "closed";
				//root.id = System.Guid.NewGuid().ToString();
				root.children=true;

				//root.state = new KeyValuePair<string, jsbool>("closed", jsbool.@true);
				//root.state = oldNodeState.Disabled;

				// root.newstate.Add(NodeState.leaf, jsbool.@true);
				// root.newstate.Add(NodeState.disabled, jsbool.@true);

				// root.newstate.Add(NodeState.opened | NodeState.closed, jsbool.@true);


				// root.state = NodeState.closed | NodeState.selected | NodeState.disabled;
				//root.state = NodeState.closed;


				bool bRandomChildren = true;
				if (bRandomChildren)
				{
					for (int i = 0; i < 10; ++i)
					{
						TreeItem ChildNode = new TreeItem();
						ChildNode.id = i.ToString();
						ChildNode.text = "ChildNode " + i.ToString();
						//ChildNode.state="closed";

						if (i == 1)
						{
							TreeItem  ChildChild = new TreeItem ();
							ChildChild.id = "lol";
							ChildChild.text = "hello";

							//ChildNode.children.Add(ChildChild);
						} // End if (i == 1)

						//root.children.Add(ChildNode);
					} // Next i

				} // End if(bRandomChildren)

				ls.Add(root);
				TreeItem root2 = new TreeItem();

				if(string.IsNullOrEmpty(strId))
				{
					root2.id="bla";
					root2.text = "Ho;ha";
				}
				else
				{
					root2.id = strId + ".1";
					root2.text="More blabla";
				}

				ls.Add(root2);

				strJSON = Tools.Serialization.JSON.Serialize(ls);
			} // End Try
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				Tools.AJAX.cAjaxResult AjaxResult = new Tools.AJAX.cAjaxResult();
				AjaxResult.error = new Tools.AJAX.AJAXException(ex);
				strJSON = Tools.Serialization.JSON.Serialize(AjaxResult);
			} // End Catch

			System.Diagnostics.Debug.WriteLine(strJSON);
			context.Response.ContentType = "application/json";
			context.Response.Write(strJSON);
		} // End Sub ProcessRequest


	} // End Class DbLessAjax : System.Web.IHttpHandler


} // End Namespace WebReportDesigner 
