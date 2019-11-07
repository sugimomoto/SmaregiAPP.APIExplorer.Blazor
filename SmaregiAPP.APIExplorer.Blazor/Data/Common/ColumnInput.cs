using SmaregiAPP.APIExplorer.Blazor.Data.Resource;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Common
{
    public class ColumnInput : Column
    {
        public bool Select { get; set; } = true;
        public string ColumnType { get; set; }
        public string ConditionType { get; set; }
        public string ConditionValue { get; set; }
    }
}
