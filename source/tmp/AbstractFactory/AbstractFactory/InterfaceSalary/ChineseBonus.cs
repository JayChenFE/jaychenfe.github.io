using System;

namespace InterfaceSalary
{
	/// <summary>
	/// �����й����˽���
	/// </summary>
	public class ChineseBonus:Bonus
	{
		public override double Calculate()
		{
			return Constant.BaseSalary * 0.1;
		}
	}
}
