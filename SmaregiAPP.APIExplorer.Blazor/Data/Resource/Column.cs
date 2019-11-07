using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Resource
{
    public class Column
    {
        [Name("No")]
        public int No { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        [Name("SumareziType")]
        public string SmaregiType { get; set; }

        [Name("Type")]
        public string Type { get; set; }

        [Name("Key")]
        public bool? Key { get; set; }

        [Name("Required")]
        public bool? Required { get; set; }

        [Name("Readonly")]
        public bool? Readonly { get; set; }

        [Name("Filterable")]
        public bool? Filterable { get; set; }

        [Name("Japanese Label")]
        public string JapaneseLabel { get; set; }

        [Name("Desc")]
        public string Description { get; set; }
    }
}
