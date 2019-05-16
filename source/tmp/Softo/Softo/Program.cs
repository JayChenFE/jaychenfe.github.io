using System;

namespace Softo
{
    class Program
    {
        static void Main(string[] args)
        {
            ChineseBonus bonus = new ChineseBonus();
            double bonusValue = bonus.Calculate();

            ChineseTax tax = new ChineseTax();
            double taxValue = tax.Calculate();

            double salary = Constant.BaseSalary + bonusValue - taxValue;

            Console.WriteLine("Chinese Salary is：" + salary);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 公用的常量
    /// </summary>
    public class Constant
    {
        public static double BaseSalary = 4000;
    }

    /// <summary>
    /// 计算中国个人所得税
    /// </summary>
    public class ChineseTax
    {
        public double Calculate()
        {
            return (Constant.BaseSalary + (Constant.BaseSalary * 0.1)) * 0.4;
        }
    }

    /// <summary>
    /// 计算中国个人奖金
    /// </summary>
    public class ChineseBonus
    {
        public double Calculate()
        {
            return Constant.BaseSalary * 0.1;
        }
    }
}
