using System;

namespace InterfaceSalary
{	
	/// <summary>
	/// �����й���������˰
	/// </summary>
	public class ChineseTax:Tax
	{
		public override double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
