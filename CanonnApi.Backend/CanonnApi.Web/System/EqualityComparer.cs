using System;
using System.Collections.Generic;
namespace CanonnApi.Web.System
{
	public class EqualityComparer<T> : IEqualityComparer<T>
	{
		public Func<T, T, bool> CompareFunction { get; set; }

		public EqualityComparer(Func<T, T, bool> compareFunction)
		{
			this.CompareFunction = compareFunction;
		}

		public bool Equals(T x, T y)
		{
			return CompareFunction(x, y);
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}
	}
}
