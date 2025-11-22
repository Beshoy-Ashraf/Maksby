namespace Factory.Data.Modules;

public class Treasury
{
      public int Id { get; set; }

      public double OpenAmount { get; set; }
      public double MoneyFromSeller { get; set; }//فلوس علينا
      public double MoneyForTrader { get; set; }// فلوس لينا
      public double TotalExpenses { get; set; }//المصاريف
      public double Sallary { get; set; }// قبض العمال

}
