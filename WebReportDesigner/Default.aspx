<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebReportDesigner._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link type="text/css"  rel="stylesheet" href="./assets/dist/themes/default/style.min.css" />
    
    <script type="text/javascript" src="./Scripts/libs/jquery.js"></script>
    <script type="text/javascript" src="./Scripts/jstree.js"></script>




    <script type="text/javascript" src="./Scripts/wgxpath.install.js"></script>

    <script type="text/javascript">
        // Then call wgxpath.install() from your JavaScript code, 
        // which will ensure document.evaluate, the XPath evaluation function, is defined on the window object. 
        // To install the library on a different window, pass that window as an argument to the install function.
        e1 = document.evaluate;
        wgxpath.install();
        console.log("Installed customized Wicked Good XPath.");
        e2 = document.evaluate;
        if (e1 === e2) {
            console.log("I don't see any difference in document.evaluate().");
        } else {
            console.log("Nice, I see that document.evaluate() implementation has changed.");
        }
        console.log("Installed customized Wicked Good XPath.");


        /////////////////////////////////////////////////

        var parseXml;

        if (typeof window.DOMParser != "undefined") {
            parseXml = function (xmlStr) {
                return (new window.DOMParser()).parseFromString(xmlStr, "text/xml");
            };
        }
        else if (typeof window.ActiveXObject != "undefined" && new window.ActiveXObject("Microsoft.XMLDOM")) {
            parseXml = function (xmlStr) {
                var xmlDoc = new window.ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = "false";
                xmlDoc.loadXML(xmlStr);
                return xmlDoc;
            };
        }
        else {
            throw new Error("No XML parser found");
        }
    </script>

</head>
<body>
    
    <h1>Hi there</h1>

    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>

    <div id="jstree_demo"></div>

    
    <br />
    <hr />
    <!---
    <div id="html1">
      <ul>
        <li>Root node 1
          <ul>
            <li>Child node 1</li>
            <li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
			<li><a href="#">Child node 2</a></li>
            <li><a href="#">Child node 2</a></li>
          </ul>
        </li>
      </ul>
    </div>
    -->
    
    <br />
    <hr />

       <div id="jstree2"></div>



    <script type="text/javascript">














        /*
        // http://stackoverflow.com/questions/649614/xml-parsing-of-a-variable-string-in-javascript
        var xml = parseXml("<root><h1>Howdy</h1><h1>Cowboy</h1></root>");

        //var result = document.evaluate('//h1/text()[1]', document, null, 2, null);
        
        //var result = document.evaluate('//h1/text()', xml, null, 2, null);
        //var result = document.evaluate('//h1/text()', xml, null, XPathResult.ANY_TYPE, null);

        var iterator = document.evaluate('//h1/text()', xml, null, XPathResult.UNORDERED_NODE_ITERATOR_TYPE, null);

        try 
        {
	        var thisNode = iterator.iterateNext();

	        while (thisNode) 
	        {
	        	alert(thisNode.textContent);
	        	thisNode = iterator.iterateNext();
	        }
        }
        catch (e) 
        {
        	dump('Error: Document tree modified during iteration ' + e);
        }


        var nodesSnapshot = document.evaluate('//h1/text()', xml, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);

        for (var i = 0; i < nodesSnapshot.snapshotLength; i++) {
        // alert(JSON.stringify(nodesSnapshot));
        // alert(JSON.stringify(nodesSnapshot.snapshotItem(i)));
            
        alert(nodesSnapshot.snapshotItem(i).textContent);
        }



        //alert(XPathResult.ANY_TYPE); 0

        // alert(JSON.stringify(result));

        // numberValue
        // stringValue
        // booleanValue

        //var result = document.evaluate('//h1/text()[1]', xml, null, 2, null);
        //result.resultType
        // alert(result.stringValue); // JavaScript-XPath
        */



        Date.prototype.getTicksUTC = function () {
            return Date.parse(this.toUTCString()) + this.getUTCMilliseconds();
        } // End Function getTicksUTC



        function OnNodesRetrievedError(xhr, textstatus, errorThrown) {
        	console.log("OnNodesRetrievedError");

            if (xhr != null) 
            {
            	// console.log("xhr != null"); 
                if (xhr.status != 200) 
                {
                    if (xhr.status == 0) 
                    {
                    	// console.log("xhr.status == 0");
                        // var strText = "status 0 appears when an ajax call was CANCELLED \n";
                        // strText += "before getting the response by REFRESHING the page \n";
                        // strText += "or by requesting a URL that is UNREACHABLE. \n";
                        // strText += "this status is not documented but exist over ajax and makeRequest call's from gadget.io.\n";
                    } // End if(xhr.status == 0)
                    else
                        alert(textstatus + "\n" + "HTTP " + xhr.status + "\n" + errorThrown);
                } // End if(xhr.status != 200)
				else
				{
					// Parse error...
					alert("Non-Protocol-Error: " + textstatus);
				}
            }
            else
                alert("Error: xhr is null");
        } // End Function OnNodesRetrievedError


        function OnNodesRetrievedSuccess(result, textstatus, xhr) {
            // alert(JSON.stringify(result));
            console.log("OnNodesRetrievedSuccess");

            if (result != null && result.hasError) {
                if (result.error.sessionExpired) {
                    if (parent) {
                    	alert("Session expired");
                    	
                        //if (parent.frmDMSmain)
                        //    parent.frmDMSmain.window.location = "/COR_Basic_Wincasa/Portal/SessionExpired.aspx?no_cache_LastWriteTimeUtc=1362405852160?instance=B5214DB1-4340-4733-92AA-2BF224B39A9A&message=" + result.error.originalMessage;
                        //else
                        //    parent.location = "/COR_Basic_Wincasa/Portal/SessionExpired.aspx?no_cache_LastWriteTimeUtc=1362405852160?instance=B5214DB1-4340-4733-92AA-2BF224B39A9B&message=" + result.error.originalMessage;
                    }
                    else
                        window.location = "/COR_Basic_Wincasa/Portal/SessionExpired.aspx?no_cache_LastWriteTimeUtc=1362405852160?instance=B5214DB1-4340-4733-92AA-2BF224B39A9C&message=" + result.error.originalMessage;
                }
                else {
                    alert("Beim Verarbeiten der Navigations-TreeView ist ein Fehler aufgetreten. \n\nFehlerbeschreibung: \n" + result.error.message + "\n\nStacktrace: \n" + result.error.stackTrace);
                    return null;
                }
            }

            // Has no effect anymore
            // alert(result.data[0].id);
            // return result.data;

            //the ".d" is a security feature, annoying, but necessary
            //return Sys.Serialization.JavaScriptSerializer.deserialize(data.d);
        } // End Function OnNodesRetrievedSuccess


        $(document).ready(function () {
            $.ajaxSetup({ cache: false });
            localStorage.clear();

            $('#html1').jstree({
                "core":
                {
                    "theme":
                    {
                        "variant": "large"
                    }
                }

                , "checkbox":
                {
                    "keep_selected_style": false
                }

                , "plugins": [
                // "wholerow"], 
                // "checkbox" 
                ]
            });



            $('#jstree2').jstree({
                'plugins': [] //"wholerow", "checkbox"]
            , 'core':
            {
                'data': [
				    {
				        "text": "Same but with checkboxes",
				        "children": [
						    { "text": "initially selected", "state": { "selected": true} },
						    //{ "text": "custom icon URL", "icon": "http://jstree.com/tree-icon.png" },
						    { "text": "initially open", "state": { "opened": true, disabled: true }, "children": ["Another node"] },
						    { "id": "NULL", "text": "RootNode", "state": { "disabled": true} },

                            { "text": "foo", "state": { "closed": true }, "children": ["Nextz node"] },
                            { "text": "bar", "state": { "disabled": true }, "children": ["Nextz node"] },

						    { "text": "custom icon class", "icon": "glyphicon glyphicon-leaf" }
					    ]
				    }
                    , "And wholerow selection"
			    ]
            }
            });



            $('#jstree_demo').jstree({
                "core":
                {
                    "animation": 0
                    , "check_callback": true
                    , "themes": {
                        //"stripes": true 
                        "dots": true 
                    }
                    ,'data': // formerly json_data
                    {
	                    'url': function (node) 
	                    {
	                        // console.log(node.id));
	                        // console.log(node.data);

	                        // console.log(node.li_attr);

	                        // console.log(node);

	                        // return node.id === '#' ? 'ajax_demo_roots.json' : 'ajax_demo_children.json';
	                        // return node.id === '#' ? "MyAjax.ashx?file=ajax_demo_roots.json" + cache : 'MyAjax.ashx?file=ajax_demo_children.json' + cache;

	                        var cache = "?no_cache=" + ((new Date()).getTicksUTC());
	                        var strURL = "MyAjax.ashx" + cache; // "&id=" + node.id + "&
	                        //var strURL = "DbLessAjax.ashx?file=ajax_demo_roots.json" + cache; // "&id=" + node.id + "&


	                        console.log(strURL);

	                        return strURL;
	                    }
	                    , "async": true
	                    //, dataType: "text json"
						//, dataType: "jsonp"

						//,contentType: "application/json", 

			            , type: "POST"


			            , "check_callback": function (node)
			            {
			            	alert("node");
			            }

	                    , 'data': function (node) 
	                    {
	                    	console.log("data: ");
	                    	console.log(node.type);
	                    	console.log(node);
					        
					        var obj = {
					             id: node.id  ? node.id  : "NULL"
					            //,rel: node.attr ? node.attr("rel") : "NULL"
					            ,data: node.data ? node.data : "NULL"
					            //,proc: strProc 
					        } // End obj

                        	return obj; // { 'id': node.id };
                    	}


	                    ,
	                    success: function (data, textstatus, xhr) {
	                    //alert(data);
	                    //alert(textstatus);
	                    //alert(xhr);
	                    	console.log(data);
	                    	OnNodesRetrievedSuccess(data, textstatus, xhr);
	                    } // End success



	                    , "error": function (xhr, textstatus, errorThrown) {
	                        OnNodesRetrievedError(xhr, textstatus, errorThrown);
	                        //var err = $.parseJSON(x.responseText); 
	                        /*
	                        if (err!="") 
	                        { 
	                        alert(err); 
	                        } 
	                        */
	                    }


                	}
            }

            ,
            "types": 
            {
	            "#": 
	            {
		             "max_children": 1 
		            ,"max_depth": 4 
		            ,"valid_children": ["root"] 
	            }
	            , "root": 
	            {
		             "icon": "./assets/images/tree_icon.png"
		            ,"valid_children": ["default"]
	            }
	            , "default": 
	            {
	            	"valid_children": ["default", "file"]
	            }
	            , "SO": 
	            {
	            	// "icon": "myicon.png", 
	            	"valid_children": ["default", "GB"]
	            }
	            , "GB": 
	            {
	            	"valid_children": ["default", "GS"]
	            }
	            , "GS": 
	            {
	            	"valid_children": ["default", "RM"]
	            }
	            ,"file": 
	            {
	            	 "icon": "glyphicon glyphicon-file" 
	            	,"valid_children": [] 
	            }
            }


            ,
            "themes": 
            {
            	 // "theme": "neoclassic-portal"
             	 "name": "neoclassic-portal"
            	,"dots": true
            	,"icons": true
            	,"url": "/Content/jsTree/themes/apple/style.css"
            }

			//,"plugins": ["themes", "contextmenu", "dnd", "search", "state", "types", "wholerow"]
			,"plugins": ["themes", "contextmenu", "dnd", "search", "state", "types"]
        })
        .bind("select_node.jstree", function (e, data) {
        	console.log(data.node.id);
        	console.log(data.node);
        	console.log(data);
        }
        );

    }); // End $(document).ready(function () {
    </script>



    <a href="https://code.google.com/p/wicked-good-xpath/">https://code.google.com/p/wicked-good-xpath/</a><br />
    <a href="http://coderepos.org/share/wiki/JavaScript-XPath">http://coderepos.org/share/wiki/JavaScript-XPath</a><br />
    <a href="http://jsfiddle.net/sG3J4/1/">http://jsfiddle.net/sG3J4/1/</a><br />
    <a href="http://stackoverflow.com/questions/16116785/how-to-write-cross-browser-xpath-queries-using-wicked-good-xpath-library">http://stackoverflow.com/questions/16116785/how-to-write-cross-browser-xpath-queries-using-wicked-good-xpath-library</a><br />
    <a href="https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript">https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript</a><br />
    <a href=""></a><br />

</body>
</html>
