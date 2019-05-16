using System;

namespace InterfaceSalary
{	
	/// <summary>
	/// 计算中国个人所得税
	/// </summary>
	public class ChineseTax:Tax
	{
		public override double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
