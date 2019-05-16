using System;

namespace InterfaceSalary
{
	/// <summary>
	/// 计算中国个人奖金
	/// </summary>
	public class ChineseBonus:Bonus
	{
		public override double Calculate()
		{
			return Constant.BaseSalary * 0.1;
		}
	}
}
