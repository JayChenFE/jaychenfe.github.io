using System;

namespace AbstractFactory
{
	/// <summary>
	/// �����й����˽���
	/// </summary>
	public class ChineseBonus:Bonus
	{
		public override double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
