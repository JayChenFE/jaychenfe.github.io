using System;

namespace AbstractFactory
{
	/// <summary>
	/// 计算美国奖金
	/// </summary>
	public class AmericanBonus:Bonus
	{
		public override double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
