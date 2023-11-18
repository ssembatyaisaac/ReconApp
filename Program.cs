using System;
using System.Data;
using System.IO;
namespace ReconApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.Controller controller = new Controller.Controller();
            string schoolPath = "/home/dvcky/Projects/C#/ConsoleApps/Recon/Files/SchoolFiles/Statement.csv";
            string bankPath = "/home/dvcky/Projects/C#/ConsoleApps/Recon/Files/BankFiles/BankStatement.csv";
            try
            {
                DataTable schoolDataTable = controller.ConvertCsvToDataTable(schoolPath);
                DataTable bankDataTable = controller.ConvertCsvToDataTable(bankPath);

                //Map the data
                List<DataTable> mapppedDataTable = Logic.Processor.MappedDataTable(schoolDataTable, bankDataTable);

                //Reconciliation next

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
