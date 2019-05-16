using System;

namespace AbstractFactory
{
	/// <summary>
	/// 计算美国个人所得税
	/// </summary>
	public class AmericanTax:Tax
	{
		public override double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
