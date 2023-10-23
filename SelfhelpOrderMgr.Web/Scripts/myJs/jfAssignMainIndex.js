
$(function () {
    //注册专项劳酬提取比例
    $("#editboxBaseRatio").numberbox({
        "onChange": function () {
            SetBaseAmount();
        }
    });

    //注册专项劳酬提取比例
    $("#editboxExtRatio").numberbox({
        "onChange": function () {
            SetExtAmount();
        }
    });

    loadMainTable(); //显示主单明细
    loadDetailTable("JFN00000")
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.Flag == "1") {
                return 'background-color:brown;';
            }
        }
    });

    $('#addFCrimeName').textbox({
        inputEvents: $.extend({}, $.fn.textbox.defaults.inputEvents, {
            keypress: function test() {
                //alert(event.keyCode);
                if (event.keyCode == 13) {
                    findFCrimeCode();
                }
            }
        })
    });
}); 




//显示存款明细
function loadMainTable() {
    $('#test').datagrid({
        //title: '存款记录',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height()*0.4,
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
        url: '/JfAssignMain/GetSearchList/',
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
            { field: 'YearMonth', title: '年月', sortable: true, width: 100 },
            { field: 'CompleteMoney', title: '生产完成金额', sortable: true, width: 100 },
            { field: 'WorkDay', title: '出勤天数', sortable: true, width: 100 },
            { field: 'ExaminePersonNum', title: '考核人数', sortable: true, width: 100 },
            { field: 'BaseAmount', title: '基本报酬', sortable: true, width: 100 },
            { field: 'ExtAmount', title: '专项报酬', sortable: true, width: 100 },
            { field: 'Amount', title: '实发金额', sortable: true, width: 100 },
            { field: 'SalaryPersonNum', title: '实发人数', sortable: true, width: 100 },
            { field: 'AvgSalary', title: '平均工资', sortable: true, width: 100 },
            { field: 'SumWorkMark', title: '总绩效(工分)', sortable: true, width: 100 },
            { field: 'AvgWorkMark', title: '平均绩效', sortable: true, width: 100 },
            { field: 'SumWorkRatio', title: '总积分系数', sortable: true, width: 100 },
            { field: 'Points', title: '实发总积分', sortable: true, width: 100 },
            {
                field: 'OrderStatus', title: '单据状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag != null) {
                        if (row.ImportFlag == 1) {
                            return '已入账';
                        } if (row.ImportFlag == 3) {
                            return '已退回';
                        } if (row.ImportFlag == 2) {
                            return '公账';
                        } else {
                            return '否';
                        }
                    } else {
                        return '否';
                    }
                }
            },
            {
                field: 'CreateDate', title: '建档日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CreateDate != null) {
                        if (row.CreateDate != "") {
                            if (value != "/Date(-62135596800000)/") {
                                var dt = getLocalTime(value);
                                if (dt == 'Invalid Date') {
                                    return '';
                                } else {
                                    return dt;
                                }
                            } else {
                                return '';
                            }
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'CreateBy', title: '创建人', sortable: true, width: 100 },
            { field: 'Remark', title: '备注', sortable: true, width: 100 },         
            {
                field: 'Opration', title: '操作', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag == 0) {
                        return "<input type='button' value='设为公账' onclick='SetImportFlag(2," + row.Id + ")'/>"
                    } else if (row.ImportFlag == 2){
                        return "<input type='button' value='设为个人账' onclick='SetImportFlag(0," + row.Id + ")'/>";
                    }else {
                    return "";
                }
                }
            }
        ]],
        onSelect: function (rowIndex, rowData) {
            $("#editboxId").val(rowData.Id);
            $("#editboxYearMonth").textbox('setValue', rowData.YearMonth);
            $("#editboxCompleteMoney").textbox('setValue', rowData.CompleteMoney);
            $("#editboxBaseRatio").textbox('setValue', "");
            $("#editboxExtRatio").textbox('setValue', "");
            $("#editboxBaseAmount").textbox('setValue', rowData.BaseAmount);
            $("#editboxExtAmount").textbox('setValue', rowData.ExtAmount);
            loadDetailTable(rowData.YearMonth);
        },
        pagination: true,
        rownumbers: true
    });
}




//显示存款明细
function loadDetailTable(yearMonth) {
    $('#detail').datagrid({
        //title: '存款记录',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.4,
        queryParams: {
            yearMonth: yearMonth
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/JfAssignMain/GetDetailList/',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            { field: 'OrderId', title: '主单号', sortable: true, width: 80 },
            { field: 'TypeFlag', title: '类型标志', sortable: true, width: 100 },
            { field: 'YearMonth', title: '年月', sortable: true, width: 100 },
            { field: 'FAreaCode', title: '队别编号', sortable: true, hidden: true, width: 100 },
            { field: 'FAreaName', title: '队别名称', sortable: true, width: 100 },
            { field: 'CompleteMoney', title: '生产完成金额', sortable: true, width: 100 },
            { field: 'WorkDay', title: '出勤天数', sortable: true, width: 100 },
            { field: 'ExaminePersonNum', title: '考核人数', sortable: true, width: 100 },
            { field: 'TichengBilv', title: '提成比例', sortable: true, width: 100, editor: 'text' },
            { field: 'Ranking', title: '排名', sortable: true, width: 100 },
            { field: 'BaseAmount', title: '基本报酬', sortable: true, width: 100 },
            { field: 'ExtAmount', title: '专项报酬', sortable: true, width: 100 },
            { field: 'Amount', title: '实发金额', sortable: true, width: 100 },
            { field: 'Points', title: '实发积分', sortable: true, width: 100 },
            {
                field: 'OrderStatus', title: '单据状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag != null) {
                        if (row.ImportFlag == 1) {
                            return '已入账';
                        } if (row.ImportFlag == 3) {
                            return '已退回';
                        } if (row.ImportFlag == 2) {
                            return '公账';
                        } else {
                            return '否';
                        }
                    } else {
                        return '否';
                    }
                }
            },
            { field: 'WorkChangeRatio', title: '工分兑换系数', sortable: true, width: 100 },
            {
                field: 'CreateDate', title: '建档日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CreateDate != null) {
                        if (row.CreateDate != "") {
                            if (value != "/Date(-62135596800000)/") {
                                var dt = getLocalTime(value);
                                if (dt == 'Invalid Date') {
                                    return '';
                                } else {
                                    return dt;
                                }
                            } else {
                                return '';
                            }
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'CreateBy', title: '创建人', sortable: true, width: 100 },
            {
                field: 'AuditDate', title: '审核日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AuditDate != null) {
                        if (row.AuditDate != "") {
                            if (value != "/Date(-62135596800000)/") {
                                var dt = getLocalTime(value);
                                if (dt == 'Invalid Date') {
                                    return '';
                                } else {
                                    return dt;
                                }
                            } else {
                                return '';
                            }
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'AuditBy', title: '审核人', sortable: true, width: 100 },
            { field: 'Remark', title: '备注', sortable: true, width: 100 }
        ]],
        onLoadSuccess: function (data) {
            //添加“合计”列
             $('#detail').datagrid('appendRow', {
                 YearMonth: '<span class="subtotal">合计</span>',
                 CompleteMoney: '<span class="subtotal">' + getAll("CompleteMoney") + '</span>',
                 WorkDay: '<span class="subtotal">' + getAll("WorkDay") + '</span>',
                 BaseAmount: '<span class="subtotal">' + getAll("BaseAmount") + '</span>',
                 ExtAmount: '<span class="subtotal">' + getAll("ExtAmount") + '</span>'
            });

            //合计
        },
        pagination: true,
        rownumbers: true
    });
}


//指定列求和
function getAll(colName) {
    var rows = $('#detail').datagrid('getRows');
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += parseFloat(rows[i][colName]);
    }
    return total.toFixed(2);
}



//扩展Editer
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            var ed = $(this).datagrid('getEditor', param);
            if (ed) {
                if ($(ed.target).hasClass('textbox-f')) {
                    $(ed.target).textbox('textbox').focus();
                } else {
                    $(ed.target).focus();
                }
            }
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    enableCellEditing: function (jq) {
        return jq.each(function () {
            var dg = $(this);
            var opts = dg.datagrid('options');
            opts.oldOnClickCell = opts.onClickCell;
            opts.onClickCell = function (index, field) {
                if (opts.editIndex != undefined) {
                    if (dg.datagrid('validateRow', opts.editIndex)) {
                        dg.datagrid('endEdit', opts.editIndex);
                        opts.editIndex = undefined;
                    } else {
                        return;
                    }
                }
                dg.datagrid('selectRow', index).datagrid('editCell', {
                    index: index,
                    field: field
                });
                opts.editIndex = index;
                opts.oldOnClickCell.call(this, index, field);
            }
        });
    }
});

$(function () {
    $('#detail').datagrid().datagrid('enableCellEditing');
})

var $dg = $("#detail");

//批量保存数据
function BatchSaveDg() {
    endEdit();
    if ($dg.datagrid('getChanges').length) {
        var inserted = $dg.datagrid('getChanges', "inserted");
        var deleted = $dg.datagrid('getChanges', "deleted");
        var updated = $dg.datagrid('getChanges', "updated");

        var effectRow = new Object();
        if (inserted.length) {
            effectRow["inserted"] = JSON.stringify(inserted);
        }
        if (deleted.length) {
            effectRow["deleted"] = JSON.stringify(deleted);
        }
        if (updated.length) {
            effectRow["updated"] = JSON.stringify(updated);
        }

        $.post("/JfAssignMain/SaveAreaMainInfoList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        UpdateDGInfo(words[1]);
                        $dg.datagrid('acceptChanges');
                        $.messager.alert("提示", "保存成功！");
                    }
                    else {
                        $.messager.alert("提示", data);
                    }
                }
            });
    }
}

//结束编辑
function endEdit() {
    var rows = $dg.datagrid('getRows');
    for (var i = 0; i < rows.length; i++) {
        $dg.datagrid('endEdit', i);
    }
}

//更新DataGrid记录信息

function UpdateDGInfo(word) {
    var invs = $.parseJSON(word);
    //清空现有记录
    var item = $('#detail').datagrid('getRows');
    if (item) {
        for (var i = item.length - 1; i >= 0; i--) {
            var index = $('#detail').datagrid('getRowIndex', item[i]);
            $('#detail').datagrid('deleteRow', index);
        }
    }
    for (var i = 0; i < invs.length; i++) {
        $('#detail').datagrid('appendRow', {
            Id: invs[i].Id,
            TypeCode: invs[i].TypeCode,
            TypeName: invs[i].TypeName,
            FCode: invs[i].FCode,
            FName: invs[i].FName,
            MgrValue: invs[i].MgrValue,
            RatioValue: invs[i].RatioValue,
            AreaStart: invs[i].AreaStart,
            AreaEnd: invs[i].AreaEnd,
            Remark: invs[i].Remark
        });
    }
}


function btnSearch() {
    var searchInfo = {
        FromActAccount: 1221211,
        TranAmount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    var inpunts = $("#crimeSearch:input");
    var json = {};
    var message = [];
    var strJson = "";
        $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
            //var username = $(this).find("input[name='username']").val();
            //var email = $(this).find("input[name='email']").val();
            //json = {
            //    "username":username,
            //    "email":email
            //};
            //message.push(json);
            //if ($(this).val() != 'undefined' && $(this).attr("name") != "undefined") {
            //    //console.log(this);
            //    console.log($(this).attr("name") +":" +$(this).val());
                
            //}
            
            if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
                console.log($(this).attr("name") + ":" + $(this).val());
                if (strJson == "") {
                    strJson = "\""+$(this).attr("name") + "\":\"" + $(this).val()+ "\"";
                } else {
                    strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
                }                
            }
        });
        strJson = "{" + strJson + "}";
    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });

    $.post("/JfAssignMain/GetListSumAmount", { "strJsonWhere": strJson }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#schSumMoney").html("查询结果总额:<u>" + data.DataInfo + " 元</u>");
            }
        }
    });
    
}


//添加主单
function btnAddSumOrder() {
    //校验
    if ($("#sremark").val() == "") {
        $.messager.alert("提示", "备注不能为空");
        return false;
    }

    if ($("#FBcExcelFile").val() == "") {
        $.messager.alert("提示", "请选择一个要导入的文件");
        return false;
    }

    //设置年月
    var ym = "";
    if ($("#smonth").val() >= 10) {
        ym = $("#syear").val() + "" + $("#smonth").val().toString();
    }
    else {
        ym = $("#syear").val() + "0" + $("#smonth").val().toString();
    }    
    $("#crtYearMonth").val(ym);

    $('#ffMainOrder').form({
        url: "/JfAssignMain/AddModel",
    onSubmit: function() {
    // do some check
        // return false to prevent submit;
        },
        success: function (data) {
            var rs = $.parseJSON(data);
            $.messager.alert("提示", rs.ReMsg)
        }
    });
    // submit the form
    $('#ffMainOrder').submit();

}


//编辑主单
function EditMain() {
    $('#winEdit').window('open');
}


function SetBaseAmount() {
    var baseRatio = $("#editboxBaseRatio").numberbox('getValue');
    //如果提取比例是空的
    if (baseRatio == "") {
        return false;
    }
    var shuilv=$("#editboxShuilv").val();
    var comMoney = $("#editboxCompleteMoney").textbox('getValue');
    var baseAmount = comMoney / (1 + parseFloat(shuilv)) * baseRatio / 100;
    $("#editboxBaseAmount").textbox('setValue', baseAmount.toFixed(0));//零位小数点
}


function SetExtAmount() {
    var extRatio = $("#editboxExtRatio").numberbox('getValue');
    //如果提取比例是空的
    if (extRatio == "") {
        return false;
    }
    var shuilv = $("#editboxShuilv").val();
    var comMoney = $("#editboxCompleteMoney").textbox('getValue');
    var extAmount = comMoney / (1 + parseFloat(shuilv)) * extRatio / 100;
    $("#editboxExtAmount").textbox('setValue', extAmount.toFixed(0));//零位小数点
}


//保存分配结果
function SaveAssignResult() {
    var baseRatio = $("#editboxBaseRatio").numberbox('getValue');
    if (baseRatio == "") {
        $.messager.alert("提示", "劳酬比例不能为空");
        return false;
    }

    var extRatio = $("#editboxExtRatio").numberbox('getValue');
    if (extRatio == "") {
        $.messager.alert("提示", "专项比例不能为空");
        return false;
    }

    var row = $("#test").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一行总单记录");
        return false;
    }
    $('#ffEdit').form({
        url: "/JfAssignMain/SaveAssignResult",
        onSubmit: function () {
            // do some check
            // return false to prevent submit;
        },
        success: function (data) {
            var rs = $.parseJSON(data);
            var idx = $("#test").datagrid('getRowIndex',row);
            $('#test').datagrid('updateRow', {
                index: idx,
                row: {
                    BaseAmount: rs.DataInfo.BaseAmount.toString(),
                    ExtAmount: rs.DataInfo.ExtAmount.toString(),
                }
            });

            $.messager.alert("提示", rs.ReMsg)
        }
    });
    // submit the form
    $('#ffEdit').submit();
}


//打印汇总单
function btnPrintSum() {
    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });
    window.open("/JfAssignMain/PrintSumList/?strJsonWhere=" +   strJson);
}


//打印明细清单
function btnPrintList() {
    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });
    window.open("/JfAssignMain/PrintDetailList/?strJsonWhere=" + strJson);
}


function btnExcelSave(id) {


    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });


    $.post("/JfAssignMain/ExcelDetailList/", { "strJsonWhere": strJson}, function (data, status) {
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


//月统计报表 期初 期间  期末
function btnExcelReportSave() {

    $.post("/JfAssignMain/ExcelReportDetailList/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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


function btnPrintBankDateReport() {

    //$.post("/BankRcv/BankDateReport/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
    //    if (status != "success") {
    //        return false;
    //    } else {
    //        var words = data.split("|");
    //        if (words[0] == "OK") {
    //            window.open("/Upload/" + words[1]);
    //        }
    //    }
    //});

    window.open("/JfAssignMain/BankDateReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue'));
}


function btnPrintBankMonthReport() {
    window.open("/JfAssignMain/BankMonthReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue') + "&endDate="+ $("#SearchCreateDate_End").datetimebox('getValue'));
}

function btnPrintBankYearReport() {
    window.open("/JfAssignMain/BankYearReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue') + "&endDate=" + $("#SearchCreateDate_End").datetimebox('getValue'));
}


function btnExcelOutBankTransList() {
    $.post("/JfAssignMain/ExcelOutBankTransList/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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

function btnExcelOutBankDuizhang() {
    $.post("/JfAssignMain/ExcelOutBankDuizhang/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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
function btnExcelBankDtlSch() {

    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#frmBankExcel').form({
        url: '/JfAssignMain/ExcelInport',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            $.messager.progress('close');
            //$("#winSearch").window('close');
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            } else {
                alert(data);
            }
        }
    });
    // submit the form    
    $('#frmBankExcel').submit();

}



//清空条件
function btnClear() {
    $("#formSearch").form('clear');
}

//导出未识别的存款记录单
function btnExcelNoFindCashReport() {
    var startTime = $("#SearchCreateDate_Start").datetimebox('getValue');
    var endTime = $("#SearchCreateDate_End").datetimebox('getValue');
    if (startTime == '') {
        alert("请选择一个时间段,谢谢");
        return false;
    }

    $.post("/JfAssignMain/ExcelNoFindCashReport/", { "startTime": startTime, "endTime": endTime }, function (data, status) {
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

//手动识别的流水号
function btnCheckVchnum() {
    $.messager.confirm('确认框', '记录不能是退款记录，不能是看守所转款，是否真的要识别该记录?', function (r) {
        if (r) {
            var vchnum = $("#search_vchnum").textbox('getValue');
            if (vchnum == '') {
                alert("请输入流水号,谢谢");
                return false;
            }

            $.post("/JfAssignMain/btnCheckVchnum/", { "vchnum": vchnum }, function (data, status) {
                if (status != "success") {
                    return false;
                } else {
                    var words = data.split("|");
                    alert(data);
                }
            });
        }
    });
    
}


function findFCrimeCode() {
    console.log("dsfdsfsdfsafserew");
    var fname = $("#addFCrimeName").textbox('getValue');
    $.post("/JfAssignMain/findFCrimeCode", { "fname": fname }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#addFCrimeCode").textbox('setValue', data.DataInfo);
            } else {
                $("#addFCrimeCode").textbox('setValue', '');
                alert(data.ReMsg);
            }
        }
    });
}