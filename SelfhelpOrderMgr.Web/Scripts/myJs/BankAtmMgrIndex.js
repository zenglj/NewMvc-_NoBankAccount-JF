//====================================================
//=====定义模块变量===================================
var selCashTypes = "";//存款类型
var selPayTypes = "";//取款类型
var selAccTypes = "";//账户类型
var selBankFlags = "";//银行处理状态
var selFFlags = "";//有效状态
//===================================================




$(function () {
    loadDetailTable(); //显示存款明细

    
    //动态改变行颜色


    $('#tbPay').datagrid({
        rowStyler: function (index, row) {
            if (row.AuditFlag == 0) {
            } else if (row.AuditFlag == "1") {
                return 'background-color:green;';
            } 
        }
    });

}); 


function doSearch() {
    $('#tt').datagrid('load', {
        itemid: $('#itemid').val(),
        productid: $('#productid').val()
    });
}



//显示存款明细
function loadDetailTable() {
    $('#tbPay').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height()*0.7,
        queryParams: {
            CrtDate_Start: '2020-05-01',
            CrtDate_End: '2020-05-21'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/BankAtmMgr/GetBankDealList/',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200,2000,20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'StatusFlag', title: '交易状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.StatusFlag == "1") {
                        return "对账成功";
                    } else if (row.StatusFlag == "0") {
                        return "未对账";
                    } else if (row.StatusFlag == "-1") {
                        return "对账失败";
                    } else {
                        return "未知";
                    }
                }
            },
            { field: 'AtmSerialNo', title: '流水号', sortable: true, width: 100 },
            { field: 'ActionType', title: '动作类型', sortable: true, width: 100 },
            { field: 'ChangeAmount', title: '变动金额', width: 100, sortable: true },
            { field: 'MachineBalance', title: '机器余额', width: 100, sortable: true },
            {
                field: 'CrtDate', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            
            { field: 'MacId', title: '机器号', sortable: true, width: 100 },

            { field: 'Remark', title: '备注', sortable: true, width: 300 }
        ]],
        onSelect: function (rowIndex, rowData) {
           
        },
        pagination: true,
        rownumbers: true
    });


}





//执行查询加载dataGrid数据的方法
function funcSearch(formId,gridId) {
    var strJson = GetSearchJson(formId);
    $('#' + gridId).datagrid('load', {
        strJsonWhere: strJson
    });
}



//获取表单的查询条件
function GetSearchJson(formId) {
    var strJson = "";
    $("#" + formId + " table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            //console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    return strJson;
}



//清空条件
function btnClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formVcrdSearch").form('clear');
}

function btnPaySearch() {
    var searchInfo = {
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    var inpunts = $("#formPaySearch:input");
    var json = {};
    var message = [];
    var strJson = "";
    $("#formPaySearch table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    console.log(strJson);
    $('#tbPay').datagrid('load', {
        strJsonWhere: strJson
    });


    GetMachineBalance();
}

function GetMachineBalance() {
    $.post("/BankAtmMgr/GetMachineBalance", {}, function (data, status) {
        if ("success" == status) {
            console.log(data);
            if (data.Flag == true) {
                console.log("111111111111111111111111111111111111");
                var _value = data.DataInfo;
                console.log(_value.Id);
                $("#formId").val(_value.Id);
                $("#formMachineName").textbox("setValue", _value.MachineName);
                $("#formMachineBalance").textbox("setValue", _value.MachineBalance);
                $("#formReconciliationDate").datetimebox("setValue", getLongTime(_value.ReconciliationDate));
                $("#formAtmSerialNo").textbox("setValue", _value.AtmSerialNo);
                $("#formStatusFlag").textbox("setValue", _value.StatusFlag);
                $("#formRemark").textbox("setValue", _value.Remark);
            }
        }
    });
}

//清空条件
function btnPayClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formPaySearch").form('clear');
}


