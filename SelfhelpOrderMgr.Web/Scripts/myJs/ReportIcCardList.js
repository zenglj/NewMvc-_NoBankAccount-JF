//====================================================
//=====定义模块变量===================================
var selCashTypes = "";//存款类型
//===================================================

//获取查询栏里多选框里的值
function GetQueryComboboxValues() {
    var CashTypes = $("#FCashTypes").combobox('getValues');
    selCashTypes = "";
    for (var i = 0; i < CashTypes.length; i++) {
        var CashType = CashTypes[i];
        if (selCashTypes == "") {
            selCashTypes = "'" + CashType + "'";;
        } else {
            selCashTypes = selCashTypes + ",'" + CashType + "'";
        }
    }

}



$(function () {
    loadDetailTable(); //显示存款明细
    $("#FCashTypes").combobox('clear');
    $("#winChangeList").window('close');
    
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.Flag == "1") {
                return 'background-color:brown;';
            }
        }
    });
}); 


//修改消费记录为调账取款
function btnInvListChange() {
    $("#winChangeList").window('open');
}


//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.9,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Report/GetIcCardList/',
        sortName: 'FRDate',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'CardCode',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '卡号', field: 'CardCode', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            { title: '狱号', field: 'FCrimeCode', width: 100, sortable: true },
            { title: '姓名', field: 'fcriminal', width: 150, sortable: true },
            { title: '队别', field: 'fareaName', width: 150, sortable: true },
            { title: '操作员', field: 'FRCZY', width: 150, sortable: true },
            {
                field: 'FRDate', title: '办卡日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.FRDate != null) {
                        if (row.FRDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            }
        ]],
        pagination: true,
        rownumbers: true
    });
}



function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }
    //alert($("#FCashTypes").combobox('getValues'));

    GetQueryComboboxValues();//获取查询栏里多选框里的值

    $('#test').datagrid('load', {
        FCode: $("#FCode").numberbox('getValue'),
        FName :$("#FName").textbox('getText'),
        startTime:$("#StartDate").datetimebox('getValue'),
        endTime:$("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy:$("#FCrtBy").combobox('getValue'),
        CriminalFlag:$("#FCriminalFlag").combobox('getValue'),
        CashTypes:selCashTypes

    });


}

//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {

    var strWhere = getSearchCondition();
    window.open("/Report/PrintIcCardList/" + id + "?" + strWhere);

}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getSearchObjCondition();

    $.post("/Report/ExcelIcCardList/" + id, objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
}

//导出Excel数据，id 是的不同报表的参数
function ExcelMain(id) {

    var objWhere = getSearchObjCondition();
    //window.open("/Report/ExcelCriminalSumOrder/" + id + "?" + strWhere);

    $.post("/Report/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });


}

//获取查询条件，用于直接开链接方式
function getSearchCondition() {

    GetQueryComboboxValues();//获取查询栏里多选框里的值

    strSearchWhere = "FCode=" + $("#FCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&FName=" + $("#FName").textbox('getText');
    strSearchWhere = strSearchWhere + "&startTime=" + $("#StartDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&endTime=" + $("#EndDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&areaName=" + $("#FAreaName").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CrtBy=" + $("#FCrtBy").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CriminalFlag=" + $("#FCriminalFlag").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CashTypes=" + selCashTypes;
    return strSearchWhere;
}

//获取Obj查询条件,用于Post方式
function getSearchObjCondition() {

    GetQueryComboboxValues();//获取查询栏里多选框里的值

    var objWhere = {
        FCode: $("#FCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy: $("#FCrtBy").combobox('getValue'),
        CriminalFlag: $("#FCriminalFlag").combobox('getValue'),
        CashTypes: selCashTypes
    };
    
    return objWhere;
}

//清空条件
function btnClear() {
    $("#FCashTypes").combobox('clear');

    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#FName").textbox('clear');
    $("#FAreaName").combobox('clear');
    $("#FCrtBy").combobox('clear');
    $("#FCriminalFlag").combobox('clear');
 
}

//A4打印消费单
//printType 0表示消费清单，1表示签字确认单
function printMulXiaofeiDan(printType) {

    CreateXiaofeiDanReport('mul', printType)

}


