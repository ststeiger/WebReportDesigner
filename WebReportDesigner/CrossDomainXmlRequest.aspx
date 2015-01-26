<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrossDomainXmlRequest.aspx.cs" Inherits="WebReportDesigner.CrossDomainXmlRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="./Scripts/libs/jquery.js"></script>

    <script type="text/javascript">
        // JQuery - Deserializing an XML object into a class type?

        var xml = "<rss version='2.0'><channel><ritle>RSS Title</ritle></channel></rss>",
        xmlDoc = $.parseXML(xml),
        $xml = $(xmlDoc),
        $title = $xml.find("ritle");
        // console.log($title);

        var dat = '<xml><test foo="bar">Cool!</test></xml>';
        var xml = $(dat);
        // alert(xml.find("test").text());  // shows "Cool!"
        // alert(xml.find("test").attr("foo"));  // shows "bar"



        //sample site that returns xml
        var site = 'http://goo.gl/9iQWyG';
        site = 'http://tel.search.ch/api/?was=john+meier';


        // var proxy_url = root + encodeURIComponent(yql) + '&format=json&diagnostics=false&callback=simpleAJAXLib.display';
        //var yql = 'http://query.yahooapis.com/v1/public/yql?q=' + encodeURIComponent('select * from xml where url="' + site + '"') + '&format=xml&callback=?';
        var yql = 'http://query.yahooapis.com/v1/public/yql?q=' + encodeURIComponent('select * from xml where url="' + site + '"') + '&format=json&callback=?';
        
        // Request that YSQL string, and run a callback function.
        // Pass a defined function to prevent cache-busting.
        $.getJSON(yql, function (data) {
            console.clear();
            console.log(JSON.stringify(data.query.results));
            //console.log(data.results[0]);
        });
        
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
