using System;
using System.Reflection.Metadata;

namespace FactorySalary
{
    class Program
    {
        static void Main(string[] args)
        {
            Bonus bonus = new Factory().CreateBonus();
            double bonusValue = bonus.Calculate();

            Tax tax = new Factory().CreateTax();
            double taxValue = tax.Calculate();

            double salary = Constant.BaseSalary + bonusValue - taxValue;

            Console.WriteLine("Chinese Salary is：" + salary);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Factory类
    /// </summary>
    public class Factory
    {
        public Tax CreateTax()
        {
            return new ChineseTax();
        }

        public Bonus CreateBonus()
        {
            return new ChineseBonus();
        }
    }

    /// <summary>
    /// 个人所得税抽象类
    /// </summary>
    public abstract class Tax
    {
        public abstract double Calculate();
    }

    /// <summary>
    /// 奖金抽象类
    /// </summary>
    public abstract class Bonus
    {
        public abstract double Calculate();
    }

    /// <summary>
    /// 计算中国个人所得税
    /// </summary>
    public class ChineseTax : Tax
    {
        public override double Calculate()
        {
            return (Constant.BaseSalary + (Constant.BaseSalary * 0.1)) * 0.4;
        }
    }

    /// <summary>
    /// 计算中国个人奖金
    /// </summary>
    public class ChineseBonus : Bonus
    {
        public override double Calculate()
        {
            return Constant.BaseSalary * 0.1;
        }
    }

    /// <summary>
    /// 公用的常量
    /// </summary>
    public class Constant
    {
        public static double BaseSalary = 4000;
    }
}
