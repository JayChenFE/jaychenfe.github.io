using System;

namespace AmericanSalary
{	
	/// <summary>
	/// 计算美国个人所得税
	/// </summary>
	public class AmericanTax
	{
		public double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
