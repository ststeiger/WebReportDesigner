
using System;


namespace WebReportDesigner
{


	public abstract class oldNodeState
	{


		[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum jsbool
		{
			@false
			,@true
		}


		public class opened_t : oldNodeState
		{
			public opened_t() : this(jsbool.@true)
			{}

			public opened_t(jsbool b)
			{
				this.opened = b;
			}

			public jsbool opened;
		}

		public class closed_t : oldNodeState
		{
			public closed_t() : this(false)
			{}

			public closed_t(bool b)
			{
				this.closed = b;
			}

			public bool closed;
		}

		public class disabled_t : oldNodeState
		{
			public disabled_t() : this(jsbool.@true)
			{}

			public disabled_t(jsbool b)
			{
				this.disabled = b;
			}
			public jsbool disabled;
		}

		public class leaf_t : oldNodeState
		{
			public leaf_t() : this(jsbool.@true)
			{}

			public leaf_t(jsbool b)
			{
				this.leaf = b;
			}

			public jsbool leaf;
		}


		public class selected_t : oldNodeState
		{
			public selected_t() : this(jsbool.@true)
			{}

			public selected_t(jsbool b)
			{
				this.selected = b;
			}

			public jsbool selected;
		}



		public static oldNodeState Opened 
		{
			get{ return new opened_t(); }
		}

		public static oldNodeState Closed 
		{
			get{ return new closed_t(); }
		}

		public static oldNodeState Disabled 
		{
			get{ return new disabled_t(); }
		}

		public static oldNodeState Leaf 
		{
			get{ return new leaf_t(); }
		}

		public static oldNodeState Selected 
		{
			get{ return new selected_t(); }
		}


	} // End abstract Class oldNodeState


} // End Namespace WebReportDesigner
