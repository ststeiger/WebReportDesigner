
using System;
using System.Collections.Generic;


namespace jQuery.Plugins.jsTree.v3
{

    /*
    // http://msdn.microsoft.com/en-us/library/system.dynamic.dynamicobject.aspx
    // http://stackoverflow.com/questions/5797778/creating-a-class-like-asp-net-mvc-3-viewbag
    public class MyBag : DynamicObject
    {
        private readonly Dictionary<string, dynamic> _properties = new Dictionary<string, dynamic>( StringComparer.InvariantCultureIgnoreCase );

        public override bool TryGetMember( GetMemberBinder binder, out dynamic result )
        {
            result = this._properties.ContainsKey( binder.Name ) ? this._properties[ binder.Name ] : null;

            return true;
        }

        public override bool TrySetMember( SetMemberBinder binder, dynamic value )
        {
            if( value == null )
            {
                if( _properties.ContainsKey( binder.Name ) )
                    _properties.Remove( binder.Name );
            }
            else
                _properties[ binder.Name ] = value;

            return true;
        }
    }
    */


    [Flags]
    public enum NodeState : int
    {
        none = 0x0
       ,
        opened = 0x1
            ,
        closed = 0x2
            ,
        leaf = 0x4
            ,
        selected = 0x8
            , disabled = 0x10
    } // Emd Enum NodeState


    public class TreeItem
    {


        //  li_attr		: { id : false },
        // Attributes to the list elementobje
        public class li_attr_t
        {
            public string id;
            public string rel;
        } // End Class li_attr_t


        // a_attr		: { href : '#' },
        // Attributes to the link
        public class a_attr_t
        {
            // http://www.w3schools.com/tags/tag_link.asp
            public string rel;
            public string href;
            public string hreflang;
            public string media;

            public string target;
            public string @class;

            // public string foo="foo";
            // public string foo_bar="foobar";

            // [Newtonsoft.Json.JsonProperty(PropertyName = "my-data")]
            // public string mimi = "lala";

            // Custom def possible ? 
        } // End Class a_attr_t


        public class ObjectStringEnumSerializer : Newtonsoft.Json.JsonConverter
        {


            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value is Enum)
                {
                    var ConvEnumToString = new Newtonsoft.Json.Converters.StringEnumConverter();
                    ConvEnumToString.WriteJson(writer, value, serializer);
                    ConvEnumToString = null;
                    return;
                }
                //else
                serializer.Serialize(writer, value);
            } // End Sub WriteJson


            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {

                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                {
                    Newtonsoft.Json.Linq.JObject jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
                    return (object)jsonObject;
                }


                // this.
                // var CurVal = ()Enum.Parse(typeof(NodeState), CurProp.Name);


                object obj = reader.Value;
                return obj;

                //NodeState RetVal = NodeState.none;

                /*
                System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JProperty> properties = jsonObject.Properties(); //.ToList();  


            foreach (Newtonsoft.Json.Linq.JProperty CurProp in properties)
            {
                // Console.WriteLine(CurProp.Name);
                // Console.WriteLine(CurProp.Value);

                NodeState CurVal = (NodeState)Enum.Parse(typeof(NodeState), CurProp.Name);

                RetVal = RetVal | CurVal;
            } // Next CurProp
*/
                // return RetVal;
            } // End Function ReadJson


            public override bool CanConvert(Type objectType)
            {
                return typeof(NodeState).IsAssignableFrom(objectType);
            } // End Function CanConvert


        } // End Class MultiEnumSerializer




        // http://blog.maskalik.com/asp-net/json-net-implement-custom-serialization/
        public class MultiEnumSerializer : Newtonsoft.Json.JsonConverter
        {

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                NodeState NodeStateFlags = (NodeState)value;
                //Newtonsoft.Json.JsonConvert.SerializeObject

                writer.WriteStartObject();

                //foreach (NodeState CurrentFlag in Enum.GetValues(NodeStateFlags.GetType()))
                foreach (NodeState CurrentFlag in Enum.GetValues(typeof(NodeState)))
                {

                    if ((NodeStateFlags & CurrentFlag) != 0) // if (name.HasFlag(val))
                    {
                        writer.WritePropertyName(CurrentFlag.ToString());
                        serializer.Serialize(writer, "true");
                    } // End if( (NodeStateFlags & CurrentFlag) != 0)

                } // Next CurrentFlag

                writer.WriteEndObject();
            } // End Sub WriteJson


            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                NodeState RetVal = NodeState.none;

                Newtonsoft.Json.Linq.JObject jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
                System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JProperty> properties = jsonObject.Properties(); //.ToList();  

                foreach (Newtonsoft.Json.Linq.JProperty CurProp in properties)
                {
                    // Console.WriteLine(CurProp.Name);
                    // Console.WriteLine(CurProp.Value);

                    NodeState CurVal = (NodeState)Enum.Parse(typeof(NodeState), CurProp.Name);

                    RetVal = RetVal | CurVal;
                } // Next CurProp

                return RetVal;
            } // End Function ReadJson


            public override bool CanConvert(Type objectType)
            {
                return typeof(NodeState).IsAssignableFrom(objectType);
            } // End Function CanConvert


        } // End Class MultiEnumSerializer





        //public class TreeItem
        //{
        public string id;
        public string real_id;
        public string path_id;
        public string text;
        public string icon;
        //public string type;


        //[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [Newtonsoft.Json.JsonConverter(typeof(ObjectStringEnumSerializer))]
        public object type;

        public string parent;
        public bool children;

        //public Dictionary<NodeState, jsbool> newstate = new Dictionary<NodeState, jsbool>();
        [Newtonsoft.Json.JsonConverter(typeof(MultiEnumSerializer))]
        public NodeState? state; // = NodeState.none;


        public bool? original = null;


        public string data;

        // 'li_attr' : obj.li_attr,
        // 'a_attr' : obj.a_attr,

        // public string li_attr ="myli";
        // public string a_attr = "mya";

        public li_attr_t li_attr = new li_attr_t();
        public a_attr_t a_attr = new a_attr_t();


        //public List<TreeItem> children; // = new List<TreeItem>();

        //} // End Class TreeItem


    }


}
