using holonsoft.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace holonsoft.Utils
{
	public static class ReflectionUtils
	{
		public static bool IsAssembly(string path)
		{
			try
			{
				AssemblyName.GetAssemblyName(path);
				return true;
			}
			catch (BadImageFormatException)
			{
			}

			return false;
		}

		public static Assembly LoadAssemblyOrNull(string path)
		{
			if (IsAssembly(path))
			{
				try
				{
					AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);
					return LoadAssemblyOrNull(assemblyName);
				}
				catch (Exception)
				{
				}
			}

			return null;
		}

		public static Assembly LoadAssemblyOrNull(AssemblyName assemblyRef)
		{
			try
			{
				return Assembly.Load(assemblyRef);
			}
			catch (Exception)
			{
			}

			return null;
		}

		public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
			{
				throw new ArgumentNullException(nameof(propertyExpression));
			}

			if (propertyExpression.Body is not MemberExpression body)
			{
				throw new ArgumentException("Invalid argument", nameof(propertyExpression));
			}

			if (body.Member is not PropertyInfo member)
			{
				throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
			}

			return member.Name;
		}

		//Common.TypeScanning

		private static HashSet<Assembly> _allAssemblies;
		public static HashSet<Assembly> AllAssemblies => _allAssemblies ?? GenerateAllAssemblies();

		private static HashSet<Assembly> GenerateAllAssemblies()
		{
			HashSet<Assembly> allAssemblies = new();

			void DescendAssemblies(Assembly assembly)
			{
				allAssemblies.Add(assembly);

				Assembly[] referencedAssemblies =
					assembly
						.GetReferencedAssemblies()
						.Select(LoadAssemblyOrNull)
						.Where(x => x != null && !allAssemblies.Contains(x))
						.ToArray();

				foreach (var referencedAssembly in referencedAssemblies)
				{
					if (!allAssemblies.Contains(referencedAssembly))
					{
						DescendAssemblies(referencedAssembly);
					}
				}
			}

			var entryAssembly = Assembly.GetEntryAssembly();

			DescendAssemblies(entryAssembly);

			allAssemblies.UnionWith(
				Directory
					.GetFiles(Path.GetDirectoryName(entryAssembly.Location))
					.Where(ReflectionUtils.IsAssembly)
					.Select(ReflectionUtils.LoadAssemblyOrNull)
					.Where(x => x != null)
					.Where(x => !x.IsDynamic));

			_allAssemblies = allAssemblies;
			return _allAssemblies;
		}

		private static Dictionary<string, Type> _allTypes;
		public static Dictionary<string, Type> AllTypes => _allTypes ?? GenerateAllTypes();

		private static Dictionary<string, Type> GenerateAllTypes()
		{
			_allTypes =
				AllAssemblies
					.SelectMany(x => x.GetTypes())
					.DistinctBy(x => x.FullName)
					.ToDictionary(x => x.FullName);
			return _allTypes;
		}

		private static Dictionary<string, Type> _allNonAbstractTypes;
		public static Dictionary<string, Type> AllNonAbstractTypes => _allNonAbstractTypes ?? GenerateAllNonAbstractTypes();

		private static Dictionary<string, Type> GenerateAllNonAbstractTypes()
		{
			_allNonAbstractTypes =
				AllTypes
					.Where(x => !x.Value.IsInterface && !x.Value.IsAbstract)
					.ToDictionary(x => x.Key, x => x.Value);
			return _allNonAbstractTypes;
		}


		/// <summary>
		/// Looks in all loaded - dynamic - assemblies for the given type.
		/// </summary>
		/// <param name="typeName">The fullname of the type.</param>
		/// <returns>The <see cref="Type"/> found; null if not found.</returns>
		public static Type FindTypeByNameInAnyDynamicAssembly(string typeName)
		{
			return AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(x => x.IsDynamic)
				.SelectMany(x => x.GetTypes())
				.FirstOrDefault(x => x.FullName == typeName);
		}

		/// <summary>
		/// Looks in all loaded - non dynamic - assemblies for the given type.
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="Type"/> found; null if not found.</returns>
		public static Type FindTypeByNameInAnyNonDynamicAssembly(string typeName)
		{
			if (AllTypes.TryGetValue(typeName, out Type result))
				return result;
			return null;
		}


		/// <summary>
		/// Looks in all loaded assemblies (including dynamic assemblies) for the given type.
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="Type"/> found; null if not found.</returns>
		public static Type FindTypeByNameInAnyAssembly(string typeName)
		{
			return FindTypeByNameInAnyNonDynamicAssembly(typeName) ?? FindTypeByNameInAnyDynamicAssembly(typeName);
		}


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
