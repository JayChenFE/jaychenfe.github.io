using System;

namespace ChineseSalary
{	
	/// <summary>
	/// �����й���������˰
	/// </summary>
	public class ChineseTax
	{
		public double Calculate()
		{
			return (Constant.BASE_SALARY + (Constant.BASE_SALARY * 0.1)) * 0.4;
		}
	}
}
