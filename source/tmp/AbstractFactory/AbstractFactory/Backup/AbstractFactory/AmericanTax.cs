using System;

namespace AbstractFactory
{
	/// <summary>
	/// ����������������˰
	/// </summary>
	public class AmericanTax:Tax
	{
		public override double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
