
using System;

namespace FactorySalary
{
	/// <summary>
	/// �ͻ��˳������
	/// </summary>
	public class Calculator 
	{
		public static void Main(string[] args) 
		{
			Bonus bonus = new Factory().CreateBonus();
			double bonusValue  = bonus.Calculate();
	
			Tax tax = new Factory().CreateTax();
			double taxValue = tax.Calculate();
	
			double salary = 4000 + bonusValue - taxValue; 

			Console.WriteLine("Chinaese Salary is��" + salary);
			Console.ReadLine();
		}
	}
}
