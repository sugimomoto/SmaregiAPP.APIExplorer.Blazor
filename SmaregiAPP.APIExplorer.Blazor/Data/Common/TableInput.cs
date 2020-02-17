using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Common
{
    public class TableInput
    {
        public string tableName;

        public string OrderByColumn1 { get; set; }

        public string OrderByColumn2 { get; set; }

        public string OrderByType1 { get; set; }

        public string OrderByType2 { get; set; }

        public int Limit { get; set; } = 100;

        public int Page { get; set; } = 1;

        public List<ColumnInput> columnInput = new List<ColumnInput>();
    }
}
