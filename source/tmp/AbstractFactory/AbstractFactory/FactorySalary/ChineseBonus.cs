using System;

namespace FactorySalary
{
	/// <summary>
	/// 计算中国个人奖金
	/// </summary>
	public class ChineseBonus:Bonus
	{
		public override double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
