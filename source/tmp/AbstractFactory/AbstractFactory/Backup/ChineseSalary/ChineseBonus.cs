using System;

namespace ChineseSalary
{
	/// <summary>
	/// �����й����˽���
	/// </summary>
	public class ChineseBonus
	{
		public double Calculate()
		{
			return Constant.BASE_SALARY * 0.1;
		}
	}
}
