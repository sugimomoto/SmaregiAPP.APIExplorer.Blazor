window.tableDataManager = {
    getTableValues: function () {
        var tbl = document.getElementsByTagName("Table")[0];

        var results = [];

        for (var i = 1; i < tbl.children[1].children.length + 1; i++) {
            var row = tbl.children[1].children[i - 1];
            var result = {};

            result["Select"] = row.cells[0].getElementsByTagName("input")[0].checked;
            result["No"] = Number(this.trimInvalidCharactor(row.cells[1].innerHTML));
            result["Name"] = this.trimInvalidCharactor(row.cells[2].innerHTML);
            result["JapaneseLabel"] = this.trimInvalidCharactor(row.cells[3].innerHTML);
            result["Type"] = this.trimInvalidCharactor(row.cells[4].innerHTML);
            result["ConditionType"] = this.trimInvalidCharactor(row.cells[5].getElementsByTagName("select")[0].value);
            result["ConditionValue"] = this.trimInvalidCharactor(row.cells[6].getElementsByTagName("input")[0].value);
            result["Description"] = this.trimInvalidCharactor(row.cells[7].innerHTML);

            results.push(result);
        }

        return results;
    },
    // trim "<!--!-->" value. because when using Blazor component automatically generate each tag that include this value.
    trimInvalidCharactor: function (value) {
        return value.replace("<!--!-->", "").replace("<!--!-->", "").trim();
    },

    // Set all check box is true of false at select columns.
    setAllChecked: function (isCheck) {
        var tbl = document.getElementsByTagName("Table")[0];
        for (var i = 1; i < tbl.children[1].children.length + 1; i++) {
            var row = tbl.children[1].children[i - 1];
            row.cells[0].getElementsByTagName("input")[0].checked = isCheck;
        }
    }
};

var textarea = document.querySelector('textarea');

textarea.addEventListener('keydown', autosize);
x