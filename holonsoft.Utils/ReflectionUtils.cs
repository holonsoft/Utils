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
						.Where(x => x != null));

			_allAssemblies = allAssemblies;
			return _allAssemblies;
		}

		private static Dictionary<string, Type> _allNonAbstractTypes;
		public static Dictionary<string, Type> AllNonAbstractTypes => _allNonAbstractTypes ?? GenerateAllNonAbstractTypes();

		private static Dictionary<string, Type> GenerateAllNonAbstractTypes()
		{
			_allNonAbstractTypes =
				 AllAssemblies
						.SelectMany(x => x.GetTypes())
						.Where(x => !x.IsInterface && !x.IsAbstract)
						.DistinctBy(x => x.FullName)
						.ToDictionary(x => x.FullName);
			return _allNonAbstractTypes;
		}



		/// <summary>
		/// Looks in all loaded - non dynamic - assemblies for the given type.
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="Type"/> found; null if not found.</returns>
		public static Type FindTypeByNameInAnyNonDynamicAssembly(string typeName)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
											.Where(a => !a.IsDynamic)
											.SelectMany(a => a.GetTypes())
											.FirstOrDefault(t => t.Name.Equals(typeName) || t.FullName.Equals(typeName));
		}


		/// <summary>
		/// Looks in all loaded assemblies (including dynamic assemblies) for the given type.
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="Type"/> found; null if not found.</returns>
		public static Type FindTypeByNameInAnyAssembly(string typeName)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(a => a.GetTypes())
											.FirstOrDefault(t => t.Name.Equals(typeName) || t.FullName.Equals(typeName));
		}


		/// <summary>
		/// Looks in all loaded assemblies (including dynamic assemblies) for the given type.
		/// If just a name is provided the result may contain more than one value
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="IEnumerable<>"/> found; empty enumeration if not found.</returns>
		public static IEnumerable<Type> FindAllTypesByNameInAnyAssembly(string typeName)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(a => a.GetTypes()
											.Where(t => t.Name.Equals(typeName) || t.FullName.Equals(typeName)));
		}

		/// <summary>
		/// Looks in all loaded - non dynamic - assemblies for the given type.
		/// If just a name is provided the result may contain more than one value
		/// </summary>
		/// <param name="typeName">The name or fullname of the type.</param>
		/// <returns>The <see cref="IEnumerable<>"/> found; empty enumeration if not found.</returns>
		public static IEnumerable<Type> FindAllTypesByNameInAnyNonDynamicAssembly(string typeName)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
											.Where(a => !a.IsDynamic)
											.SelectMany(a => a.GetTypes()
											.Where(t => t.Name.Equals(typeName) || t.FullName.Equals(typeName)));
		}

	}
}
