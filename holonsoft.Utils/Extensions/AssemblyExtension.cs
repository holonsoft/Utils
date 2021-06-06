// unset

using System;
using System.Linq;

namespace holonsoft.Utils.Extensions
{
	public class AssemblyExtension
	{
		/// <summary>
		/// Search a type in all assemblies
		/// </summary>
		/// <param name="typeToSearch"></param>
		/// <returns></returns>
		public static Type SearchType(string typeToSearch)
		{
			var x = Type.GetType(typeToSearch);

			if (x != null) return x;

			var types = from a in AppDomain.CurrentDomain.GetAssemblies()
				from t in a.GetTypes()
				select t;

			return types.FirstOrDefault(type => type.FullName == typeToSearch);
		}
	}
}