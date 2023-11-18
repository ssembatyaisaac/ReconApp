
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace ReconApp.Controller
{
    public class Controller
    {
        public DataTable ConvertCsvToDataTable(string csvFilePath)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser csvParser = new TextFieldParser(csvFilePath))
            {
                csvParser.SetDelimiters(",");

                // Read the first row as the header
                string[] headers = csvParser.ReadFields() ?? new string[0];
                if (headers != null)
                {
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                }

                // Read the remaining rows as data
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields() ?? new string[0];
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;
        }


        public static string GetUserSelectedHeader(DataTable dataTable)
        {
            string[] headers = dataTable.Columns.Cast<DataColumn>()
                                                .Select(column => column.ColumnName)
                                                .ToArray();

            Console.WriteLine("Select a header:");

            for (int i = 0; i < headers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {headers[i]}");
            }

            Console.Write("Enter the number corresponding to the header: ");
            string userInput = Console.ReadLine() ?? "";

            if (int.TryParse(userInput, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= headers.Length)
            {
                return headers[selectedIndex - 1];
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return ""; // or throw an exception, depending on your requirements
            }
        }


        public static List<ColumnMapping> GetColumnMappings(DataTable sourceDataTable, DataTable destinationDataTable)
        {
            List<ColumnMapping> columnMappings = new List<ColumnMapping>();

            Console.WriteLine("Source DataTable Columns:");
            for (int i = 0; i < sourceDataTable.Columns.Count; i++)
            {
                DataColumn column = sourceDataTable.Columns[i];
                Console.WriteLine($"{i + 1}. {column.ColumnName}");
            }

            Console.WriteLine();

            Console.WriteLine("Destination DataTable Columns:");
            for (int i = 0; i < destinationDataTable.Columns.Count; i++)
            {
                DataColumn column = destinationDataTable.Columns[i];
                Console.WriteLine($"{i + 1}. {column.ColumnName}");
            }

            Console.WriteLine();

            Console.WriteLine("Enter column mappings (SourceIndex DestinationIndex):");
            Console.WriteLine("(Press Enter to finish)");

            while (true)
            {
                Console.Write("Mapping: ");
                string input = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                string[] mappingParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (mappingParts.Length != 2 ||
                    !int.TryParse(mappingParts[0], out int sourceIndex) ||
                    !int.TryParse(mappingParts[1], out int destinationIndex) ||
                    sourceIndex < 1 || sourceIndex > sourceDataTable.Columns.Count ||
                    destinationIndex < 1 || destinationIndex > destinationDataTable.Columns.Count)
                {
                    Console.WriteLine("Invalid mapping. Please try again.");
                    continue;
                }

                string sourceColumn = sourceDataTable.Columns[sourceIndex - 1].ColumnName;
                string destinationColumn = destinationDataTable.Columns[destinationIndex - 1].ColumnName;

                ColumnMapping columnMapping = new ColumnMapping
                {
                    SourceColumn = sourceColumn,
                    DestinationColumn = destinationColumn
                };

                columnMappings.Add(columnMapping);
            }

            return columnMappings;
        }
    }
}
