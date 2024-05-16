$(function () {
    //jquery js 当文本框获得焦点时，自动选中里面的文字
    $(function () {
        $(":text").focus(function () {
            this.select();
        });
    });

    //加载主单列表
    loadMainOrderTable();
    //加载付款单
    LoadPayOrder();

    //获取DataGrid Table页数据
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

    //获取数据的总金额
    //getAllMoney();

    mulSelectComobox();

    $('#win').window('close');//关闭编辑框
});

//实现多选下拉框
function mulSelectComobox() {
    $('#payKemu').combobox({
        url: '/FinaPay/GetPayType',
        method: 'get',
        valueField: 'FCode',
        textField: 'FName',
        //panelHeight: 'auto',
        multiple: true,
        formatter: function (row) {
            var opts = $(this).combobox('options');
            return '<input type="checkbox" class="combobox-checkbox">' + row[opts.textField]
        },

        onShowPanel: function () {
            var opts = $(this).combobox('options');
            var target = this;
            var values = $(target).combobox('getValues');
            $.map(values, function (value) {
                var el = opts.finder.getEl(target, value);
                el.find('input.combobox-checkbox')._propAttr('checked', true);
            })
        },
        onLoadSuccess: function () {
            var opts = $(this).combobox('options');
            var target = this;
            var values = $(target).combobox('getValues');
            $.map(values, function (value) {
                var el = opts.finder.getEl(target, value);
                el.find('input.combobox-checkbox')._propAttr('checked', true);
            })
        },
        onSelect: function (row) {
            //console.log(row);
            var opts = $(this).combobox('options');
            var el = opts.finder.getEl(this, row[opts.valueField]);
            el.find('input.combobox-checkbox')._propAttr('checked', true);
        },
        onUnselect: function (row) {
            var opts = $(this).combobox('options');
            var el = opts.finder.getEl(this, row[opts.valueField]);
            el.find('input.combobox-checkbox')._propAttr('checked', false);
        }
    });

    $('#mainAreaName').combobox({
        url: '/FinaPay/GetAreaName',
        method: 'get',
        valueField: 'FCode',
        textField: 'FName',
        //panelHeight: 'auto',
        multiple: true,
        formatter: function (row) {
            var opts = $(this).combobox('options');
            return '<input type="checkbox" class="combobox-checkbox">' + row[opts.textField]
        },

        onShowPanel: function () {
            var opts = $(this).combobox('options');
            var target = this;
            var values = $(target).combobox('getValues');
            $.map(values, function (value) {
                var el = opts.finder.getEl(target, value);
                el.find('input.combobox-checkbox')._propAttr('checked', true);
            })
        },
        onLoadSuccess: function () {
            var opts = $(this).combobox('options');
            var target = this;
            var values = $(target).combobox('getValues');
            $.map(values, function (value) {
                var el = opts.finder.getEl(target, value);
                el.find('input.combobox-checkbox')._propAttr('checked', true);
            })
        },
        onSelect: function (row) {
            //console.log(row);
            var opts = $(this).combobox('options');
            var el = opts.finder.getEl(this, row[opts.valueField]);
            el.find('input.combobox-checkbox')._propAttr('checked', true);
        },
        onUnselect: function (row) {
            var opts = $(this).combobox('options');
            var el = opts.finder.getEl(this, row[opts.valueField]);
            el.find('input.combobox-checkbox')._propAttr('checked', false);
        }
    });
}

//加载主单列表函数
function loadMainOrderTable() {
    $('#mainOrderTable').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 220,
        queryParams: {
            fName: '',
            fCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/FinaPay/getVcrds',
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 50,
        pageList: [50, 100],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'BankFlag', title: '回款状态', width: 60, formatter: function (value, row, index) {
                    if (row.BankFlag == "3") {
                        return "手动成功";
                    } else if (row.BankFlag == "2") {
                        return "成功收款";
                    } else if (row.BankFlag == "1") {
                        return "已发送";
                    } else {
                        return "待发送";
                    }
                }
            },
            { field: 'DType', title: '科目类型', width: 100 },
            { field: 'CAmount', title: '应付金额', width: 100 },
            { field: 'CrtBy', title: '操作员', width: 100 },
            {
                field: 'CrtDate', title: '创建日期', width: 100, formatter: function (value, row, index) {
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
            { field: 'FCrimeCode', title: '狱号', width: 100 },
            { field: 'FCriminal', title: '姓名', width: 100 },
            { field: 'FAreaName', title: '队别', width: 100 },

            {
                field: 'FinancePayFlag', title: '财务付款', width: 60, formatter: function (value, row, index) {
                    if (row.FinancePayFlag == "1") {
                        return "已付";
                    } else {
                        return "未付";
                    }
                }
            },
            { field: 'Remark', title: '备注', width: 150 }
        ]],
        pagination: true,
        rownumbers: true
    });
}

function getAllMoney() {
    //GetAllMoney
    $.post("/FinaPay/getVcrds", {
        "action": "GetAllMoney",
        "FCrimeName": $("#vcrd_FCrimeName").val(),
        "FCrimeCode": $("#vcrd_FCrimeCode").val(),
        "FRemark": $("#sremark").val(),
        "FPayKemu": $('#payKemu').combobox('getText'),
        "FAreaName": $('#mainAreaName').combobox('getText'),
        "startDate": $("#vcrd_startDate").datetimebox('getValue'),
        "endDate": $("#vcrd_endDate").datetimebox('getValue')
    }, function (data, status) {
        if (status == "success") {
            $("#txtAllMoney").html(data)
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}
//加载并显示劳报明细列表
function LoadPayOrder() {
    //加载并显示劳报明细列表
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 850,
        height: 280,
        queryParams: {
            fName: '',
            fCode: '00000',
            FAreaName: '',
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        singleSelect: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/FinaPay/GetPayList',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 50,
        pageList: [50, 100],
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '单号', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[
        { field: 'FType', title: '付款类型', width: 120 },
        { field: 'FTitle', title: '摘要', width: 150 },
        { field: 'FCount', title: '记录数', width: 60 },
        { field: 'FMoney', title: '金额', width: 80 },
        { field: 'CrtBy', title: '创建人', width: 80 },
        {
            field: 'CrtDt', title: '创建日期', width: 130, formatter: function (value, row, index) {
                if (row.CrtDt != null) {
                    if (row.CrtDt != "") {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
        },
        { field: 'PosName', title: '收款人', width: 150 },
        { field: 'BankCard', title: '收款账号', width: 150},
        {
            field: 'Flag', title: '付款', width: 60, formatter: function (value, row, index) {
                if (row.Flag == "1") {
                    return "已付";
                } else {
                    return "未付";
                }
            }
        },
        { field: 'PayBy', title: '付款人', width: 150, hidden: true },
        { field: 'PayDate', title: '付款日期', width: 150 ,hidden:true},
        { field: 'Remark', title: '备注', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnprint',
            text: '打印付款单',
            iconCls: 'icon-print',
            handler: function () {
                //$('#btnprint').linkbutton('enable');
                var row = $("#test").datagrid("getSelected");
                window.open("/FinaPay/PrintReportList?bid=" + row.Id);
            }
        }, '-', {
            id: 'btnsubmit',
            text: 'Excel 清单导出',
            iconCls: 'icon-save',
            handler: function () {
                PayListExcelOut();
            }
        }, '-', {
            id: 'btnPrintList',
            text: '打印清单',
            iconCls: 'icon-redo',
            handler: function () {
                //$('#btnprint').linkbutton('enable');
                var row = $("#test").datagrid("getSelected");
                window.open("/FinaPay/PrintPayList?bid=" + row.Id);
            }
            }, '-', {
                id: 'btnDelMainOrder',
                text: '删除主单',
                iconCls: 'icon-cancel',
                handler: function () {
                    $.messager.confirm('询问', '您确认要删除主付款单吗?', function (r) {
                        if (r) {
                            var row = $("#test").datagrid("getSelected");
                            if (row != null && row != undefined) {
                                $.post("/FinaPay/DelMainOrder", { "bid": row.Id }, function (data, status) {
                                    $.messager.alert("提示", data.ReMsg);
                                });
                            }
                        }
                    });
                    

                }
            }
        ]
    });
}

//Vcrd记录查询
function btnGetVcrds() {
    $('#mainOrderTable').datagrid('load', {
        FCrimeName: $("#vcrd_FCrimeName").val(),
        FCrimeCode: $("#vcrd_FCrimeCode").val(),
        FRemark: $("#sremark").val(),        
        FPayKemu: $('#payKemu').combobox('getText'),
        FAreaName: $('#mainAreaName').combobox('getText'),
        startDate: $("#vcrd_startDate").datetimebox('getValue'),
        endDate: $("#vcrd_endDate").datetimebox('getValue'),
        rtnMoneyFlag: $("#rtnMoneyFlag").combobox('getValue'),
        Fdate: "Date"+Date(),
        action: 'GetSearchMainOrder'
    });
    //获取数据的总金额
    getAllMoney();
    $("#btnCreatePayOrder").linkbutton('enable');
}

//Pay付款单查询
function btnGetPaies() {
    $('#test').datagrid('load', {
        FPayId: $("#FPayId").val(),
        FPayTitle: $("#FPayTitle").val(),
        FPayRemark: $("#FPayRemark").val(),
        startDate: $("#startDate").datetimebox('getValue'),
        endDate: $("#endDate").datetimebox('getValue'),
        action: 'GetSearchMainOrder'
    });
}
//创建付款单
function btnCreatePayOrder() {
    var allmoney= $('#txtAllMoney').html();
    if (allmoney == 0) {
        $.messager.alert('警告', '付款金额不能为0');
        return false;
    } else {
        $("#payMoney").val($('#txtAllMoney').html());
        $("#payType").val($('#payKemu').combobox("getText"));
        $('#win').window('open');//关闭编辑框
    }
    
}

//保存付款单并写到库
function btnSavePayOrder() {
    if ($("#payTitle").val() == "") {
        $.messager.alert('提示', '请填写付款摘要说明');
        return false;
    }
    else {
        $.post("/FinaPay/SavePayOrder", {
            "action": "Add",
            "payType": $("#payType").val(),
            "payTitle": $("#payTitle").val(),
            "payMoney": $("#payMoney").val(),
            "payPosName": $("#payPosName").val(),
            "payBankCard": $("#payBankCard").val(),
            "payRemark": $("#payRemark").val(),
            "FCrimeName": $("#vcrd_FCrimeName").val(),
            "FCrimeCode": $("#vcrd_FCrimeCode").val(),
            "FRemark": $("#sremark").val(),
            "FPayKemu": $('#payKemu').combobox('getText'),
            "FAreaName": $('#mainAreaName').combobox('getText'),
            "startDate": $("#vcrd_startDate").datetimebox('getValue'),
            "endDate": $("#vcrd_endDate").datetimebox('getValue')
        }, function (data, status) {
            if (status == "success") {
                var words = data.split("|");
                if (words[0] == "OK") {
                    var infos = $.parseJSON(words[1]);
                    $('#test').datagrid('insertRow', {
                        index: 0,
                        row: {
                            Id: infos.Id,
                            FType: infos.FType,
                            FTitle: infos.FTitle,
                            FCount: infos.FCount,
                            FMoney: infos.FMoney,
                            CrtBy: infos.CrtBy,
                            CrtDt: infos.CrtDt,
                            PosName: infos.PosName,
                            BankCard: infos.BankCard,
                            Flag: infos.Flag,
                            PayBy: infos.PayBy,
                            PayDate: infos.PayDate,
                            Remark: infos.Remark,
                        }
                    });
                    //清空查询条件
                    $("#txtAllMoney").html("0.00");
                    $("#btnCreatePayOrder").linkbutton('disable');
                    $("#payTitle").val('');
                    $("#payPosName").val('');
                    $("#payBankCard").val('');
                    $("#payRemark").val('');
                    $('#win').window('close');                    
                }
                else {
                    $.messager.alert('提示', data);
                }
            } else {
                $.messager.alert('提示', "通信失败");
            }
        });
    }
}
//取消定单
function btnCancelSave() {
    $("#payTitle").val('');
    $("#payPosName").val('');
    $("#payBankCard").val('');
    $("#payRemark").val('');
    $('#win').window('close');
}
//清空查询条件
function clearSearch() {
    $("#crimeSearch input").val("");
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

    $.post("/BankRcv/GetListSumAmount", { "strJsonWhere": strJson }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#schSumMoney").html("查询结果总额:<u>" + data.DataInfo + " 元</u>");
            }
        }
    });

}

//Vcrd查询结果导出
function VcrdExcelOut() {
    $.post("/FinaPay/VcrdExcelOut", {
        "FCrimeName": $("#vcrd_FCrimeName").val(),
        "FCrimeCode": $("#vcrd_FCrimeCode").val(),
        "FRemark": $("#sremark").val(),
        "FPayKemu": $('#payKemu').combobox('getText'),
        "FAreaName": $('#mainAreaName').combobox('getText'),
        "startDate": $("#vcrd_startDate").datetimebox('getValue'),
        "endDate": $("#vcrd_endDate").datetimebox('getValue'),
        "rtnMoneyFlag": $("#rtnMoneyFlag").combobox('getValue'),
        "Fdate": "Date" + Date(),
        "action": 'GetSearchMainOrder'
    }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}

//PayId Vcrd清单导出
function PayListExcelOut() {
    var row =$("#test").datagrid('getSelected');
    if(row!=null){
        $.post("/FinaPay/PayListOutport", {
            "payId": row.Id,
            "Fdate": "Date" + Date(),
            "action": 'GetSearchMainOrder'
        }, function (data, status) {
            if (status == "success") {
                var words = data.split("|");
                if (words[0] == "OK") {
                    window.open("/Upload/" + words[1]);
                }
            } else {
                $.messager.alert('提示', "通信失败");
            }
        });
    }
    
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
    $('#test').datagrid().datagrid('enableCellEditing');
})