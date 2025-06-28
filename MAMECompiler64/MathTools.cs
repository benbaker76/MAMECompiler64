using System;
using System.Collections.Generic;
using System.Text;

namespace MAMECompiler64
{
	public class MathTools
	{
		public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
		{
			if (value.CompareTo(min) < 0)
				return min;
			else if (value.CompareTo(max) > 0)
				return max;
			else return value;
		}
	}
}
