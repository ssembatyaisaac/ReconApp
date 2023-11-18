using System.Data;

namespace ReconApp.Logic
{
    public class Processor
    {
        public static List<DataTable> MappedDataTable(DataTable main, DataTable vendor)
        {

            List<ColumnMapping> columnMappings = Controller.Controller.GetColumnMappings(main, vendor);

            // Create arrays to store property values
            string[] mainColumns = new string[columnMappings.Count];
            string[] vendorColumns = new string[columnMappings.Count];

            // Iterate over objects in the list, retrieve Name and Age properties, and store them in the arrays
            for (int i = 0; i < columnMappings.Count; i++)
            {
                mainColumns[i] = columnMappings[i].SourceColumn ?? "";
                vendorColumns[i] = columnMappings[i].DestinationColumn ?? "";
            }

            //Create datatable for the selected columns
            DataTable mainRequiredDataTable = Controller.Controller.CreateRequiredDataTable(main, mainColumns);
            DataTable vendorRequiredDataTable = Controller.Controller.CreateRequiredDataTable(vendor, vendorColumns);

            List<DataTable> mappedDataTable = new List<DataTable>
            {
                mainRequiredDataTable,
                vendorRequiredDataTable
            };

            return mappedDataTable;
        }
    }
}