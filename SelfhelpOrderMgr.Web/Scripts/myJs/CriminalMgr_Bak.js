﻿
var bankCombogridJson;

$(function () {
    //console.log("ddddddddddddddddddddddddddddddddddddddddddddddddd");
    loadDetailTable(); //显示存款明细
    //$("#FPayTypes").combobox('clear');
    //$("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');
    $("#rjStartDate").datetimebox('clear');
    $("#rjEndDate").datetimebox('clear');
    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');
    $('#txtFCode').textbox('readonly', true);

    $("#win").window('close');
    $("#winChangeEdu").window('close');
    $("#winSearch").window('close');
    $("#winBank").window('close');
    $('#winTP_YingYanyCan').window('close');  // open a window  
    $('#winOutBankInfo').window('close');
    

    $("#ccBankOpenName").combogrid({
        panelWidth: 450,
        toolbar: '#tbByBank',
        value: '',
        idField: 'Id',
        textField: 'BankOpenName',
        url: bankCombogridJson,
        columns: [[
            { field: 'Id', title: 'Id', width: 60 },
            { field: 'CNAPS', title: '开户行号', width: 100 },
            { field: 'BankOpenName', title: '开户行网点', width: 120 },
            { field: 'BankAddress', title: '银行地址', width: 100 }
        ]]
    });
}); 



//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.7,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: false,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Criminal/GetSearchUsers',
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'FCode',
        pageSize: 10,
        pageList: [5,10, 20,50,100],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '编号', field: 'FCode', width: 80, sortable: true },
            { field: 'FName', title: '姓名', sortable: true, width: 100 }
        ]],
        columns: [[//DataGrid表格数据列
            
            { field: 'CyName', title: '处遇', sortable: true, width: 100 },            
            { field: 'FAreaName', title: '队别', width: 100, sortable: true },
            { field: 'FSex', title: '性别', width: 100, sortable: true },
            {
                field: 'fflag', title: '状态', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (row.fflag == "1") {
                        return "离监";
                    } else {
                        return "在押";
                    }
                }
            },
            {
                field: 'flimitflag', title: '冻结', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (row.flimitflag == "1") {
                        return "是";
                    } else {
                        return "否";
                    }
                }
            },
            { field: 'flimitamt', title: '冻结金额', width: 100, sortable: true },
            { field: 'FAddr', title: '地址', width: 200, sortable: true },
            { field: 'FTerm', title: '刑期', width: 100, sortable: true },
            {
                field: 'FInDate', title: '进监日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.FInDate != null) {
                        if (row.FInDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            {
                field: 'FOuDate', title: '刑满日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.FOuDate != null) {
                        if (row.FOuDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            { field: 'FIdenNo', title: '身份证号', sortable: true, width: 100 },
            { field: 'FAge', title: '年龄', sortable: true, width: 100 },
            { field: 'FDesc', title: '描述', sortable: true, width: 100 },
            { field: 'FCZY', title: '录入员', sortable: true, width: 150 },
            { field: 'CardCode', title: 'IC卡', sortable: true, width: 100 },
            { field: 'BankCardNo', title: '银行卡', sortable: true, width: 200 },
            { field: 'TP_YingYangCan_Money', title: '特批金额', sortable: true, width: 200 },
            {
                field: 'RSB_Flag', title: '入监/所包', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (value == 1) {
                        return "已扣款";
                    } else {
                        return "未扣款";
                    }
                }
            }
        ]],
        onSelect: function (rowIndex, rowData) {
            loadTP_YYCList(rowData);//营养餐记录
            setDataRowInfo(rowData);
            //$("#txtFlimitFlag").textbox('setValue', rowData.flimitflag);

            //alert(rowData.flimitamt);
            //if (rowData.BankFlag == 0) {
            //    $('#btndel').linkbutton('enable');
            //} else {
            //    $('#btndel').linkbutton('disable');
            //}
            
        },
        pagination: true,
        rownumbers: true
    });
}




//查询开户银行的网点信息
function searchBankOpenName() {

    var q='福州 南门';
    $.post("/Criminal/GetPagelistByBankOpenName", { 'q': q }, function (data, status) {
        if ("success" = status) {
            bankCombogridJson = data;
            console.log(data);
        }
    });  

}


//设置行信息
function setDataRowInfo(rowData) {
    $("#txtFCode").textbox('setValue', rowData.FCode);
    $("#txtFName").textbox('setValue', rowData.FName);
    $("#txtFSex").combobox('setValue', rowData.FSex);
    $("#txtFCyCode").combobox('setValue', rowData.FCYCode);
    $("#txtFIdenNo").textbox('setValue', rowData.FIdenNo);
    $("#txtFAddr").textbox('setValue', rowData.FAddr);
    $("#txtFAreaCode").combobox('setValue', rowData.FAreaCode);
    $("#txtFCrimeCode").combobox('setValue', rowData.FCrimeCode);
    $("#txtFTerm").textbox('setValue', rowData.FTerm);    
    if (rowData.fflag == 1) {
        $("#txtFFlag").textbox('setValue', "离监");
    } else {
        $("#txtFFlag").textbox('setValue', "在押");
    }
    if (rowData.flimitflag == 1) {
        $("#txtFlimitFlag").attr('checked', 'checked');
    } else {
        $("#txtFlimitFlag").removeAttr('checked');
    }
    $("#txtFlimitAmt").textbox('setValue', rowData.flimitamt);
    $("#txtFDesc").textbox("setValue", rowData.FDesc);
    $("#txtFInDate").val(getShortTime(rowData.FInDate));
    $("#txtFOuDate").val(getShortTime(rowData.FOuDate));
    $("#TP_YingYangCan_Money").textbox("setValue", rowData.TP_YingYangCan_Money);

    if ($("#areaMset").val() == "0") {
        $("#txtFAreaCode").combobox('enable');
    }
    if ($("#cyMset").val() == "0") {
        $("#txtFCyCode").combobox('enable');
    }
    
}

function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }
    //alert($("#FCashTypes").combobox('getValues'));

    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType ;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }


    $('#test').datagrid('load', {
        FCode: $("#FCode").numberbox('getValue'),
        endFCode: $("#endFCode").numberbox('getValue'),
        FName :$("#FName").textbox('getText'),
        cyName: $("#FCyName").combobox('getValue'),
        rjStartTime: $("#rjStartDate").datetimebox('getValue'),
        rjEndTime: $("#rjEndDate").datetimebox('getValue'),
        startTime:$("#StartDate").datetimebox('getValue'),
        endTime:$("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy:$("#FCrtBy").combobox('getValue'),
        CriminalFlag:$("#FCriminalFlag").combobox('getValue'),
        //CashTypes:selCashTypes,
        //PayTypes:selPayTypes,
        AccTypes:selAccTypes,
        BankFlags:selBankFlags

    });


}

function btnAddCriminal() {
    $("#txtFCode").textbox("clear");
    $('#txtFCode').textbox('readonly', false);
    $("#txtFName").textbox("clear");
    $("#txtFIdenNo").textbox("clear");
    $("#txtFAddr").textbox("clear");
    $("#txtFCode").textbox("clear");
    $("#txtFTerm").textbox("clear");
    $("#txtFlimitAmt").textbox("clear");
    $("#txtFDesc").textbox("clear");
    $("#txtFFlag").textbox("clear");
    $("#txtFSex").combobox('setValue', "");
    $("#txtFAreaCode").combobox('setValue', "");
    
    $("#txtFCrimeCode").combobox('setValue', "");
    $("#txtFCyCode").combobox('setValue', "");
    $("#txtFInDate").val('');
    $("#txtFOuDate").val('');
    $("#doType").val('add');

    $("#txtFAreaCode").combobox('enable');
    $("#txtFCyCode").combobox('enable');
}

function btnSaveCriminal() {
    if ($("#txtFCode").val() == "") {
        $.messager.alert("提示", "用户编号不用为空");
        return false;
    }
    if ($("#txtFName").val() == "") {
        $.messager.alert("提示", "姓名不用为空");
        return false;
    }
    if ($("#txtFSex").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择性别");
        return false;
    }
    $("#txtFAreaCode").combobox('enable');
    if ($("#txtFAreaCode").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择所在队别");
        return false;
    }
    $("#txtFCyCode").combobox('enable');
    if ($("#txtFCyCode").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择一个处遇级别");
        return false;
    }
    if($("#txtFCode").val() != "" ||$("#txtFName").val() != ""){
        $('#ff').form({    
            url:'/Criminal/SaveCriminal',    
            onSubmit: function(){    
                // do some check    
                // return false to prevent submit;    
            },    
            success: function (data) {
                var words = data.split("|");
                if (words[0] == "OK") {
                    if ($("#doType").val() == "add") {
                        var flieds = $.parseJSON(words[1]);
                        for (var i = 0; i < flieds.length; i++) {
                            var criminal = flieds[i];
                            $('#test').datagrid('appendRow', {
                                FCode: criminal.FCode,
                                FName: criminal.FName,
                                FAreaCode: criminal.FAreaCode,
                                FCYCode: criminal.FCYCode,
                                FCrimeCode: criminal.FCrimeCode,
                                FAddr: criminal.FAddr,
                                FTerm: criminal.FTerm,
                                FInDate: criminal.FInDate,
                                FOuDate: criminal.FOuDate,
                                FIdenNo: criminal.FIdenNo,
                                FSex: criminal.FSex,
                                FDesc: criminal.FDesc
                            });
                        }
                    }
                                        
                    //清空输入框

                    $("#doType").val("");
                    $('#txtFCode').textbox('readonly', true);
                    $("#txtFAreaCode").combobox('disable');
                    $("#txtFCyCode").combobox('disable');
                    $.messager.alert("提示", "保存成功,请刷新记录");
                } else {
                    $.messager.alert("提示", data);
                }
            }    
        });    
        // submit the form    
        $('#ff').submit(); 
    }

        
}

function btnCancelEdit() {
    var rowData = $("#test").datagrid('getSelected');
    setDataRowInfo(rowData);
    $("#doType").val('');
}

function btnDelCriminal() {
    var row = $("#test").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一行要删除的记录");
        return false;
    }
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.post('/Criminal/DelCriminal', { "txtFCode": $("#txtFCode").textbox('getValue') }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    var words = data.split("|");
                    if ("OK" == words[0]) {
                        var idx = $("#test").datagrid('getRowIndex', row);
                        $("#test").datagrid('deleteRow', idx);
                        $.messager.alert("提示", "删除成功");
                    } else {
                        $.messager.alert("提示",data);
                    }
                }
            });
        }
    });
}

//恢复离监犯人
function btnRecCriminal() {
    var row = $("#test").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一行要删除的记录");
        return false;
    }
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.post('/Criminal/RecCriminal', { "txtFCode": $("#txtFCode").textbox('getValue') }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    var words = data.split("|");
                    if ("OK" == words[0]) {
                        $("#txtFFlag").textbox('setValue', '在押');
                        $.messager.alert("提示", data);
                    } else {
                        $.messager.alert("提示", data);
                    }
                }
            });
        }
    });
}

function btnTP_YingYangCan() {
    $('#winTP_YingYanyCan').window('open');  // open a window    
}

//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {

    var strWhere = getSearchCondition();
    window.open("/Report/PrintCriminalSumOrder/" + id + "?" + strWhere);

}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getSearchObjCondition();
    //alert("dddddd");
    $.post("/Criminal/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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

//Excel信息导入
function InExcelCriminalInfo() {
    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#ffExcel').form({
        url: '/Criminal/ExcelInport',
        onSubmit: function(){    
            // do some check    
            // return false to prevent submit;    
        },    
        success: function (data) {
            $.messager.progress('close');
            $("#win").window('close');
            alert(data)    
        }    
    });    
    // submit the form    
    $('#ffExcel').submit();
}


function InExcelChangeEDu() {
    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#ffExcelChgED').form({
        url: '/Criminal/ExcelChangeEdu',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            $.messager.progress('close');
            $("#winChangeEdu").window('close');
            alert(data)
        }
    });
    // submit the form    
    $('#ffExcelChgED').submit();
}


//Excel批量查询名单
function InExcelSearchInfo() {
    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#ffExcelSearch').form({
        url: '/Criminal/ExcelSearch',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            $.messager.progress('close');
            $("#winSearch").window('close');
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
    // submit the form    
    $('#ffExcelSearch').submit();
}


//导出Excel数据，id 是的不同报表的参数
function ExcelMain(id) {
    alert(id);

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

    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }

    strSearchWhere = "FCode=" + $("#FCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&endFCode=" + $("#endFCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&FName=" + $("#FName").textbox('getText');
    strSearchWhere = strSearchWhere + "&cyName=" + $("#FCyName").combobox('getValue');
    strSearchWhere = strSearchWhere + "&rjStartTime=" + $("#rjStartDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&rjEndTime=" + $("#rjEndDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&startTime=" + $("#StartDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&endTime=" + $("#EndDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&areaName=" + $("#FAreaName").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CrtBy=" + $("#FCrtBy").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CriminalFlag=" + $("#FCriminalFlag").combobox('getValue');
    //strSearchWhere = strSearchWhere + "&CashTypes=" + selCashTypes;
    //strSearchWhere = strSearchWhere + "&PayTypes=" + selPayTypes;
    strSearchWhere = strSearchWhere + "&AccTypes=" + selAccTypes;
    strSearchWhere = strSearchWhere + "&BankFlags=" + selBankFlags;
    return strSearchWhere;
}

//获取Obj查询条件,用于Post方式
function getSearchObjCondition() {
    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }

    var objWhere = {
        FCode: $("#FCode").numberbox('getValue'),
        endFCode: $("#endFCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        cyName: $("#FCyName").combobox('getValue'),
        rjStartTime: $("#rjStartDate").datetimebox('getValue'),
        rjEndTime: $("#rjEndDate").datetimebox('getValue'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy: $("#FCrtBy").combobox('getValue'),
        CriminalFlag: $("#FCriminalFlag").combobox('getValue'),
        //CashTypes: selCashTypes,
        //PayTypes: selPayTypes,
        AccTypes: selAccTypes,
        BankFlags: selBankFlags
    };
    
    console.log(objWhere);
    return objWhere;
}

//清空条件
function btnClear() {
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');

    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#FName").textbox('clear');
    $("#FCyName").combobox('clear');
    $("#FAreaName").combobox('clear');
    $("#FCrtBy").combobox('clear');
    $("#FCriminalFlag").combobox('clear');
 
}

//A4打印消费单
//printType 0表示消费清单，1表示签字确认单
function printMulXiaofeiDan(printType) {

    CreateXiaofeiDanReport('mul', printType)

    //var rows = $("#test").datagrid('getSelections');
    //for (var i = 0; i < rows.length; i++) {
    //    var tid = i;
    //    myTestHtml("#temp" + tid);
    //}
}

//生成消费单
//printType 0表示消费清单，1表示签字确认单
function CreateXiaofeiDanReport(e,printType) {
    if (e == "mul") {
        var rows = $("#test").datagrid("getSelections");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = selectRows + "|" + rows[i].InvoiceNo;
                }
            }
        }
    } else if (e == "one") {
        var row = $("#test").datagrid("getSelected");
        var selectRows = row.InvoiceNo;
    } else if (e == "all") {
        $("#test").datagrid("selectAll");
        var rows = $("#test").datagrid("getRows");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = rows[i].InvoiceNo + "|" + selectRows;
                }
            }
        }
    } else {
        return false;
    }
    window.open("/Super/PrintXiaofeiDan?invoices=" + selectRows + "&printType=" + printType);
    //$.post("/Home/GetInvoices", {
    //    "invoices": selectRows
    //}, function (data, status) {
    //    if ("success" != status) {
    //        return false;
    //    } else {
    //        //$.messager.alert('提示', data);
    //        var invs = $.parseJSON(data);
    //        $("#template").empty();//清空
    //        if (invs.length > 0) {
    //            for (var i = 0; i < invs.length; i++) {
    //                var content = invs[i];
    //                var inv = content;

    //                var xiaopiao = "<div id='temp" + i + "'><div> <div style='text-align:center'><h2>监狱一卡通系统</h2></div>"
    //                + "<table >"
    //                + "<tbody>"
    //                + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td><td>编号：" + inv.invoice.FCrimeCode + "</td><td>姓名：" + inv.invoice.FCriminal + "</td><td >监区：" + inv.invoice.FAreaName + "</td><td>日期：" + getLocalTime(inv.invoice.OrderDate) + "</td></tr>"

    //                + "</tbody>"
    //                + "</table>"
    //                + "<hr />"
    //                + "<table style='font-size:11px;'>"
    //                + "<thead>"
    //                + "<tr><th>货号</th><th>品名</th><th style='width:80px;'>型号</th><th style='width:50px;'>单价</th><th style='width:50px;'>数量</th><th>金额</th></tr>"
    //                + "</thead>"
    //                + "<tbody>";

    //                for (var j = 0; j < inv.details.length; j++) {
    //                    xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].SPShortCode + "</td><td  style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + "</td><td>" + inv.details[j].Remark + "</td><td align='center'>" + inv.details[j].GDJ + "</td><td align='center'>" + inv.details[j].QTY + "</td><td align='right'>" + inv.details[j].AMOUNT + "</td></tr>"
    //                    + ""

    //                }

    //                xiaopiao = xiaopiao + "</tbody>"
    //                + "</table>"
    //                + "<hr />"
    //                + "<h3><span>消费合计：" + inv.invoice.Amount + "元</span></h3><hr/>"
                    
    //                + "</div><br/></div>";

    //                $("#template").append(xiaopiao);
    //            }
    //        }

    //    }
    //});
}

function btnBankCardEdit() {
    var row = $("#test").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一个指定的人员");
        return false;
    }
    $("#FUserCode").val(row.FCode);
    $("#FUserName").val(row.FName);
    $("#FBankCardNo").val("");
    $("#FAmountAMoney").val("");
    $("#FAmountBMoney").val("");
    $("#FAmountCMoney").val("");
    GetUserBankInfo(row);
    $("#winBank").window('open');
}

function btnOutBankInfoEdit() {
    var row = $("#test").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一个指定的人员");
        return false;
    }
    $("#FOutUserCode").val(row.FCode);
    GetOutBankInfo(row.FCode);
    //$("#OutBankCard").val("");
    //$("#BankUserName").val("");
    //$("#OpeningBank").val("");
    //$("#OutBankRemark").val("");
    $("#winOutBankInfo").window('open');
}

function OutBankCardSave() {
    $('#ffOutBankInfo').form({    
        url: "/Criminal/AddOutBankInfo",
        onSubmit: function(){    
            // do some check    
            // return false to prevent submit;    
        },    
        success:function(data){    
            alert(data)    
        }    
    });    
    // submit the form    
    $('#ffOutBankInfo').submit();
}

//获取离监银行卡信息
function GetOutBankInfo(fcode) {
    //var FOutUserCode=$("#FOutUserCode").val();
    if($("#FOutUserCode").val()!=""){
        $.post("/Criminal/GetOutBankInfo", { "FOutUserCode": fcode }, function (data, status) {
            var words = data.split("|");
            if ("OK" == words[0]) {
                var card = $.parseJSON(words[1]);
                $("#OutBankCard").val(card.OutBankCard);
                $("#BankUserName").val(card.BankUserName);
                $("#OpeningBank").val(card.OpeningBank);
                $("#OutBankRemark").val(card.OutBankRemark);

            } else {
                $("#OutBankCard").val("");
                $("#BankUserName").val("");
                $("#OpeningBank").val("");
                $("#OutBankRemark").val("");
            }
        });
    }
}

//获取银行卡信息
function GetUserBankInfo(row) {
    
    $.post('/Criminal/GetUserBankInfo', { "FCode": row.FCode }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if ("OK" == words[0]) {
                card = $.parseJSON(words[1]);
                $("#FUserCode").val(row.FCode);
                $("#FUserName").val(row.FName);
                $("#FBankCardNo").val(card.BankAccNo);
                if (card.AmountA < 0) {
                    $("#FAmountAMoney").val(card.AmountA).css("color","red");
                } else {
                    $("#FAmountAMoney").val(card.AmountA);
                }                
                $("#FAmountBMoney").val(card.AmountB);
                $("#FAmountCMoney").val(card.AmountC);
                               
            } else {
                $.messager.alert("提示", data);
            }
        }
    });
}

//保存银行信息
function BankCardSave() {
    $('#ffBankInfoSet').form({
        url: '/Criminal/BankCardSave',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            //$("#winBank").window('close');
            alert(data)
        }
    });
    // submit the form    
    $('#ffBankInfoSet').submit();
}



//加载人员营养餐特批记录
function loadTP_YYCList(rowData) {
    //console.info(fcode);
    $("#yycFCode").val(rowData.FCode);
    $("#yycFName").val(rowData.FName);
    $("#yycTPMoney").val("");
    $("#yycEffectiveDate").val("");
    $("#yycFileName").val("");
    $("#yycRemark").val("");
    $('#yycTable').datagrid({
        title: '营养餐领导特批记录',
        iconCls: 'icon-save',
        width: 560,
        height: 250,
        fitColumns: false,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Criminal/GetUsersTP_YYCList?fcode=' + rowData.FCode,
        sortName: 'id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'id',
        pageSize: 10,
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'id', field: 'id', width: 40, sortable: true },
            { field: 'FCode', title: '编号', width: 80, sortable: true },
            { field: 'FName', title: '姓名', sortable: true, width: 100 }
        ]],
        columns: [[//DataGrid表格数据列

            { field: 'TPMoney', title: '特批金额', sortable: true, width: 80 },
            {
                field: 'EffectiveDate', title: '有效期到', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.EffectiveDate != null) {
                        if (row.EffectiveDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            {
                field: 'MoneyUseFlag', title: '是否在处遇标准内', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.MoneyUseFlag == 1) {
                        return "是";
                    } else {
                        return '否';
                    }
                }
            },
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
            {
                field: 'CrtDate', title: '操作日期', width: 100, sortable: true, formatter: function (value, row, index) {
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
            { field: 'FifoFlag', title: '标志', width: 200, sortable: true },
            { field: 'Remark', title: '特批原因', width: 100, sortable: true },
            {
            field: 'SrcFileName', title: '审批文件下载', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.id != null) {
                        return "<input type='button' value='下载文件' onclick='downLoadTPFile(\"" + value + "\")' />";
                    } else {
                        return "";
                    }
                }
            },
            {
                field: 'action', title: '操作', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.id != null) {
                        return "<input type='button' id='btnYycDelete' value='删除' onclick='DelTPList(" + row.id + ",\"" + row.FCode + "\")' />";
                    } else {
                        return "";
                    }
                }
            }
        ]],
        //onSelect: function (rowIndex, rowData) {
        //    setDataRowInfo(rowData);

        //},
        pagination: true,
        rownumbers: true
    });
}


//增加特批营养餐
function btnYycSave() {
    if ($("#yycFCode").val() == "") {
        alert("用户编号不能为空");
        return false;
    }
    if ($("#yycFName").val() == "") {
        alert("用户姓名不能为空");
        return false;
    }
    if ($("#yycTPMoney").val() == "") {
        alert("特批金额不能为空");
        return false;
    }
    if ($("#yycEffectiveDate").datebox('getValue') == "") {
        alert("有效期不能为空");
        return false;
    }
    if ($("#yycRemark").val() == "") {
        alert("特批原因不能为空");
        return false;
    }

    $('#ffYYC').form({
        url: "/Criminal/AddTPList",
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            alert(data)
        }
    });

    if ($("#yycFileName").val() == "") {
        $.messager.confirm('警告', '您没有审批文件，造成的责任将由您本人负责，是否继续提交？', function (r) {
            if (r) {                
                // submit the form    
                $('#ffYYC').submit();
            }
        });
    }
    if ($("#yycFileName").val() != "") {
        $.messager.confirm('提示', '您是否确认要提交该审批记录？', function (r) {
            if (r) {
                // submit the form    
                $('#ffYYC').submit();
            }
        });
    }

     

}

//删除特批营养餐
function DelTPList(id, fcode) {
    $.messager.confirm('提示', '您是否真的要删除该审批记录？', function (r) {
        if (r) {
            $.post('/Criminal/DelTPList', { "id": id, "FCode": fcode }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    var words = data.split("|");
                    if ("OK" == words[0]) {
                        $.messager.alert("提示", "删除成功，请重新加载并刷新记录");

                    } else {
                        $.messager.alert("提示", data);
                    }
                }
            });
        }
    });
    
}

//下载审批文件
function downLoadTPFile(file) {
    window.open(file);
}

//入监所包扣款，可以透支，只扣一次
function btnRSB_Koukuan() {
    var row = $("#test").datagrid('getSelected');
    if (row != null) {
        if (row.RSB_Flag == 1) {
            $.messager.alert('提示', '该学员已经扣过“入监/所包”了，不能重复扣款');
            return false;
        } else {

            $.messager.confirm('提示', '您是否真的要对该犯扣“入监/所包”款？', function (r) {
                if (r) {
                    $.post("/Criminal/btnRSB_Koukuan", { "FCrimeCode": row.FCode }, function (data, status) {
                        if ("success" == status) {
                            $.messager.alert('提示', data);
                        } else {
                            $.messager.alert('提示', "提交失败，服务器无响应");
                        }
                    });
                }
            });
            
        }
    }
}