using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Resource
{
    public class Table
    {
        [Name("No")]
        public int No { get; set; }

        [Name("Document")]
        public string Document { get; set; }

        [Name("Category Name JP")]
        public string CategoryNameJp { get; set; }

        [Name("Category Name")]
        public string CategoryName { get; set; }

        [Name("Table Name JP")]
        public string TableNameJp { get; set; }

        [Name("Table Name")]
        public string TableName { get; set; }

        [Name("CData TableName")]
        public string CDataTableName { get; set; }

        [Name("Insert")]
        public string InsertProcName { get; set; }

        [Name("Update")]
        public string UpdateProcName { get; set; }

        [Name("Delete")]
        public string DeleteProcName { get; set; }

        [Name("Reference")]
        public string ReferenceProcName { get; set; }

        [Name("Send")]
        public string SendProcName { get; set; }

        [Name("Memo")]
        public string Memo { get; set; }

        [Ignore]
        public bool IsReferenceSupported { 
            get {
                return this.ReferenceProcName != "Not Supported";
            }
        }

        [Ignore]
        public bool IsInsertSupported
        {
            get
            {
                return this.InsertProcName != "Not Supported";
            }
        }

        [Ignore]
        public bool IsUpdateSupported
        {
            get
            {
                return this.UpdateProcName != "Not Supported";
            }
        }

        [Ignore]
        public bool IsDeleteSupported
        {
            get
            {
                return this.DeleteProcName != "Not Supported";
            }
        }
    }
}
