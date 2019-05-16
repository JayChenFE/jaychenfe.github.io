using System;

namespace AmericanSalary
{
	/// <summary>
	/// 计算美国个人奖金
	/// </summary>
	public class AmericanBonus
	{
		public double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
