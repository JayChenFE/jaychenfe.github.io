using System;

namespace ChineseSalary
{
	/// <summary>
	/// 计算中国个人奖金
	/// </summary>
	public class ChineseBonus
	{
		public double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
