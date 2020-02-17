using SmaregiAPP.APIExplorer.Blazor.Data.Resource;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Common
{
    public class ColumnInput : Column
    {
        public bool Select { get; set; } = true;
        public string ColumnType { get; set; }
        public string ConditionType { 
            get { return conditionPattern[conditionTypeValue]; }
            set { conditionTypeValue = int.Parse(value); }
        }
        public string ConditionValue { get; set; }

        private int conditionTypeValue { get; set; }

        public readonly string[] conditionPattern = { "", "=", "like", "<", "<=", ">", ">=" };
    }
}
