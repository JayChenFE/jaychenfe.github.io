using System;
using System.Configuration;

namespace AbstractFactory
{
	/// <summary>
	/// ���õĳ���
	/// </summary>
	public class Constant
	{
		public static double BASE_SALARY = 4000;

		public static string STR_FACTORYNAME = ConfigurationSettings.AppSettings["factoryName"];
	}
}
