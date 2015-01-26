
using System;


namespace Tools.AJAX
{


	public class cAjaxResult
	{
		public object data;

		public AJAXException error = null;

		public bool hasError {
			get 
			{
				if (error != null) 
				{
					return true;
				}

				return false;
			}
		}

	} // cAjaxResult


}

