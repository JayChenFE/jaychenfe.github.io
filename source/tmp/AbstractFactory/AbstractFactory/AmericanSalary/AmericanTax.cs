using System;

namespace AmericanSalary
{	
	/// <summary>
	/// ����������������˰
	/// </summary>
	public class AmericanTax
	{
		public double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
