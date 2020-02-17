using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SmaregiAPP.APIExplorer.Blazor.Data.Resource;
using SmaregiAPP.APIExplorer.Blazor.Data.Request;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Common
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        public string AlertMessage { get; set; }

        public List<Table> tables = new List<Table>();

        public TableInput tableInput = new TableInput();

        public string RequestBody { get; set; }

        public string Response { get; set; }

        public string ContactId { get; set; } = "";

        public string AccessToken { get; set; } = "";


        /// <summary>
        /// Table List の取得
        /// </summary>
        protected override void OnInitialized()
        {
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\wwwroot\csv\Table.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                tables.AddRange(csv.GetRecords<Table>());
            }
        }

        /// <summary>
        /// 選択されたテーブル名を元に、カラム一覧をCSVから取得して、リスト表示
        /// </summary>
        /// <param name="e"></param>
        public void ChangeColumnList(ChangeEventArgs e)
        {
            this.AlertMessage = "";
            tableInput.columnInput.Clear();
            tableInput.tableName = "";

            try
            {
                var targetTable = tables.FirstOrDefault(x => x.CDataTableName == e.Value.ToString());

                if (targetTable == null)
                {
                    this.AlertMessage = $"Please select table name.";
                    return;
                }

                if(!targetTable.IsReferenceSupported)
                {
                    this.AlertMessage = $"{targetTable.CDataTableName} table is not support Reference functions.";
                    return;
                }

                var filePath = Directory.GetCurrentDirectory() + @"\wwwroot\csv\" + targetTable.CDataTableName + ".csv";

                if (!File.Exists(filePath))
                {
                    this.AlertMessage = $"{targetTable.CDataTableName} file is not exists.";
                    return;
                }

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    var columns = csv.GetRecords<Column>();

                    foreach (var column in columns)
                    {
                        var input = new ColumnInput() {
                            No = column.No,
                            Name = column.Name,
                            SmaregiType = column.SmaregiType,
                            Type = column.Type,
                            Required = column.Required,
                            Readonly = column.Readonly,
                            Filterable = column.Filterable,
                            JapaneseLabel = column.JapaneseLabel,
                            Description = column.Description
                        };
                        tableInput.columnInput.Add(input);
                    }

                    tableInput.tableName = targetTable.CDataTableName;
                }
            }
            catch (Exception ex)
            {
                this.AlertMessage = $"{ex.Message}";
            }
        }

        private Dictionary<string, string>[] GetConditionsRequest()
        {
            var condtions = new List<Dictionary<string, string>>();

            foreach (var item in tableInput.columnInput)
            {
                var dict = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(item.ConditionValue))
                {
                    dict.Add(item.Name + (string.IsNullOrEmpty(item.ConditionType) ? item.ConditionType : " " + item.ConditionType), item.ConditionValue);
                    condtions.Add(dict);
                }
            }

            return condtions.ToArray();
        }

        private string[] GetFiledsRequest()
        {
            return tableInput.columnInput.Where(x => x.Select).Select(x => x.Name).ToArray();
        }

        private string[] GetOrderRequest()
        {
            var orders = new List<string>();

            if (!string.IsNullOrEmpty(tableInput.OrderByColumn1) && tableInput.OrderByColumn1 != "none")
                orders.Add($"{tableInput.OrderByColumn1} {(string.IsNullOrEmpty(tableInput.OrderByType1) ? "ASC" : tableInput.OrderByType1)}");

            if (!string.IsNullOrEmpty(tableInput.OrderByColumn2) && tableInput.OrderByColumn2 != "none")
                orders.Add($"{tableInput.OrderByColumn2} {(string.IsNullOrEmpty(tableInput.OrderByType2) ? "ASC" : tableInput.OrderByType2)}");

            if (orders.Count == 0)
                return null;

            else
                return orders.ToArray();
        }

        public void GenerateRequest()
        {
            var procName = tables.Find(x => x.CDataTableName == tableInput.tableName);
            if (procName == null)
                return;

            var referenceParams = new ReferenceParams();

            referenceParams.Fileds = GetFiledsRequest();
            referenceParams.Order = GetOrderRequest();
            referenceParams.Limit = tableInput.Limit;
            referenceParams.Page = tableInput.Page;
            referenceParams.TableName = procName.TableName;
            referenceParams.Conditions = GetConditionsRequest();

            var jsonParams = JsonConvert.SerializeObject(referenceParams, Formatting.Indented);

            RequestBody = $"proc_name={procName.ReferenceProcName}&params=\n{jsonParams}";

            this.StateHasChanged();
        }

        public void SetAllCheck() => tableInput.columnInput.ForEach(x => x.Select = true);

        public void SetAllClear() => tableInput.columnInput.ForEach(x => x.Select = false);

        public async void Send()
        {
            if (String.IsNullOrEmpty(RequestBody))
                return;
            
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("https://webapi.smaregi.jp/access/"),
                Content = new StringContent(RequestBody)
            };

            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            requestMessage.Content.Headers.TryAddWithoutValidation("X_contract_id", ContactId);
            requestMessage.Content.Headers.TryAddWithoutValidation("X_access_token", AccessToken);

            var response = await Http.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var  jsonData = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject(jsonData);
            Response = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            this.StateHasChanged();

        }
    }
}
