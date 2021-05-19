$(function () {
    //jquery js 当文本框获得焦点时，自动选中里面的文字
    $(function () {
        $(":text").focus(function () {
            this.select();
        });
    });

    //加载付款单
    LoadPayOrder();
    //加载存款明细
    loadDetailTable();
    //获取DataGrid Table页数据
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

    $('#winExcelInput').window('close');

    
    $('#txtType').combobox({
        onChange: function (row) {
            //console.log($("#"+$("#txtType").combobox('getValue')).data("fushuflag"));
            if ($("#" + $("#txtType").combobox('getValue')).data("fushuflag") == "1") {
                alert("注意：该类型是可以支持透支扣款的");
            }
        } 
    });
    $('#detail').datagrid({
        rowStyler: function (index, row) {
            if (row.Flag == 0) {

            } else if (row.Flag == "-2" ) {
                return 'background-color:brown;';
            }
        }
    });
});



//加载人员信息
function LoadPayOrder() {
    //加载并显示劳报明细列表
    $('#test').datagrid({
        queryParams: {
            FName: '',
            FCode: '0000000',
            AreaName: '',
            FFlag:'0',
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        singleSelect: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/CashPay/GetUserInfo',
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'FCode',
        pageSize: 5,
        pageList: [5, 10],
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '编号', field: 'FCode', width: 80, sortable: true }
        ]],
        columns: [[
        { field: 'FName', title: '姓名', width: 100 },
        { field: 'FAreaName', title: '监区', width: 150 },
        {
            field: 'FFlag', title: '状态', width: 60, formatter: function (value, row, index) {
                if (row.FFlag == "0") {
                    return "正常";
                } else {
                    return "离监";
                }
            }
        },
        { field: 'CardCode', title: 'IC卡号', width: 80 },
        { field: 'AmountA', title: '汇款金额', width: 80 },
        { field: 'AmountB', title: '报酬金额', width: 80 },
        { field: 'AmountC', title: '留存金额', width: 80 },
        { field: 'AllMoney', title: '总金额', width: 80 },
        { field: 'BankAccNo', title: '银行卡号', width: 150 }
        
        ]],
        onSelect: function (rowIndex, rowData) {
            $("#txtFCode").val(rowData.FCode);
            $("#txtFName").val(rowData.FName);
            $("#txtFFlag").val(rowData.FFlag);
            //$("#txtMoney").numberbox('clear');
            $("#txtMoney").val("");
            btnGetUserCashList(rowData.FCode);
        },
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnExcel',
            text: 'Excel批量导入',
            iconCls: 'icon-redo',
            handler: function () {
                //$('#winExcelInput').window('open');
                window.open("/CashPay/IndexExcelDR/"+ $("#saveTypeId").val());
            }
        }
        //, '-', {
        //    id: 'btnBatchPrint',
        //    text: '打印单打印',
        //    iconCls: 'icon-print',
        //    handler: function () {
                
        //    }
        //}
        ]
    });
}

//显示存款明细
function loadDetailTable() {
    $('#detail').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 165,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/CashPay/getVcrds/'+$("#saveTypeId").val(),
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 5,
        pageList: [5,10],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'BankFlag', title: '交易状态', width: 80, formatter: function (value, row, index) {
                    if (row.BankFlag == "3") {
                        return "手动成功";
                    } else if (row.BankFlag == "2") {
                        return "已成功";
                    } else if (row.BankFlag == "1") {
                        return "已发送";
                    } else {
                        return "待发送";
                    }
                }
            },
            { field: 'DType', title: '类型', width: 100 },
            { field: 'DAmount', title: '存款金额', width: 100 },
            { field: 'CAmount', title: '取款金额', width: 100 },
            {
                field: 'Flag', title: '状态', width: 80, formatter: function (value, row, index) {
                    if (row.Flag == "0") {
                        return "正常";
                    } else if (row.Flag == "-2") {
                        return "未审核";
                    } else {
                        return "异常";
                    }
                }
            },
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
            { field: 'Remark', title: '备注', width: 150 }
        ]],
        onSelect: function (rowIndex, rowData) {
            if (rowData.BankFlag==0) {
                $('#btndel').linkbutton('enable');
            } else {
                $('#btndel').linkbutton('disable');
            }
        },
        pagination: true,
        rownumbers: true
    });
}

//查询信息
function btnSearchUser() {
    $('#test').datagrid('load', {
        FCode: $("#selFCode").val(),
        FName: $("#selFName").val(),
        AreaName: $('#selArea').combobox('getText'),
        FFlag: $('#selFFlag').combobox('getValue'),
        action: 'GetSearchMainOrder'
    });

}


//查询存款记录
function btnGetUserCashList(FCode) {

    $('#detail').datagrid('load', {
        FCode: FCode,
        FName: '',
        AreaName: '',
        FFlag: '',
        action: 'GetSearchMainOrder'
    });
}
//保存记录
function btnSaveDetail() {
    if ($("#txtFFlag").val() == "1") {
        $.messager.alert("提示", "用户已离监不能存款");
        return false;
    }
    if ($("#txtType").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择一个类型");
        return false;
    }
    if ($("#txtFCode").val() == "") {
        $.messager.alert("提示", "用户编号不能为空");
        return false;
    }
    if ($("#txtFName").val() == "") {
        $.messager.alert("提示", "用户姓名不能为空");
        return false;
    }
    //if ($("#txtApply").val() == "") {
    //    $.messager.alert("提示", "申请人不能为空");
    //    return false;
    //}
    if ($("#txtMoney").val() == "") {
        $.messager.alert("提示", "请输入金额");
        return false;
    } else {
        $.post("/CashPay/SavePayDetail/" + $("#saveTypeId").val(), {
            "FCode": $("#txtFCode").val(),
            "FName": $("#txtFName").val(),
            "DType": $("#txtType").combobox('getValue'),
            //"FMoney": $("#txtMoney").numberbox('getValue'),
            "FMoney": $("#txtMoney").val(),
            "Apply": $("#txtApply").val(),
            "Remark": $("#txtRemark").val()
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split("|");
                if (words[0] == "OK") {
                    var flieds = $.parseJSON(words[1]);
                    for (var i=0; i < flieds.length; i++) {
                        var vcrd = flieds[i];
                        $('#detail').datagrid('appendRow', {
                            seqno: vcrd.seqno,
                            BankFlag: vcrd.BankFlag,
                            DType: vcrd.DType,
                            DAmount: vcrd.DAmount,
                            CAmount: vcrd.CAmount,
                            CrtBy: vcrd.CrtBy,
                            CrtDate: vcrd.CrtDate,
                            FCrimeCode: vcrd.FCrimeCode,
                            FCriminal: vcrd.FCriminal,
                            FAreaName: vcrd.FAreaName,
                            Remark: vcrd.Remark
                        });
                    }                    
                    
                    //更新用户表的金额
                    var userInfo = $.parseJSON(words[2]);
                    var userRow = $('#test').datagrid('getSelected');
                    var idx = $('#test').datagrid('getRowIndex', userRow);
                    if (idx >= 0) {//有记录再更新
                        $('#test').datagrid('updateRow', {
                            index: idx,
                            row: {
                                AmountA: userInfo.AmountA,
                                AmountB: userInfo.AmountB,
                                AmountC: userInfo.AmountC,
                                AllMoney: userInfo.AllMoney
                            }
                        });
                    }
                    

                    //清空输入框
                    $("#txtFCode").val('');
                    $("#txtFName").val("");
                    $("#txtFFlag").val("");
                    //$("#txtMoney").numberbox('clear');
                    $("#txtMoney").val("");
                    //$.messager.alert("提示", "保存成功");
                    $("#lblDoRusultInfo").text("结果:OK保存成功");
                    $("#txtFCode").focus();
                } else {
                    $("#lblDoRusultInfo").text("结果:" + data);
                    $.messager.alert("提示",data);
                }
            }
        });
    }
}

//狱号框内按回车
function noNumbers() {
    var code = $("#txtFCode").val();
    if ("" == code) { 
        $("#txtFCode").focus();
    } else {
        $.post("/CashPay/GetFcrimeInfo", {
            "FCode": code
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split("|");
                if (words[0] == "OK") {
                    var flieds = $.parseJSON( words[1]);
                    if (flieds.FFlag == 1) {
                        $.messager.alert('提示', '该犯人已经离监结算了，不能再存!');
                        $("#txtFCode").focus();
                    } else {
                        $("#txtFName").val(flieds.FName);                        
                        //$('#txtMoney').numberbox('textbox').focus();
                        $("#txtMoney").focus();
                    }
                } else {
                    $.messager.alert('提示', '该编号不存在!');
                    $("#txtFCode").focus();
                }
            }
        });
    }
}

//姓名框内按回车
function noEntFName() {
    var name = $("#txtFName").val();
    if ("" == name) {
        $("#txtFName").focus();
    } else {
        $.post("/CashPay/GetFNameInfo", {
            "FName": name
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split("|");
                if (words[0] == "OK") {
                    var flieds = $.parseJSON(words[1]);
                    if (flieds.FFlag == 1) {
                        $.messager.alert('提示', '该犯人已经离监结算了，不能再存!');
                        $("#txtFCode").focus();
                    } else {
                        $("#txtFCode").val(flieds.FCode);
                        //$("#txtMoney").numberbox('textbox').focus();
                        $("#txtMoney").focus();
                    }
                } else {
                    $.messager.alert('提示',words[1]);
                    $("#txtFName").focus();
                }
            }
        });
    }
}

//金额框内按回车，可以直接发放钱
function noMoneyEnter() {
    var checkRst = "Err";
    if (isNaN($("#txtMoney").val())) {
        $("#lblDoRusultInfo").text('提示:金额必须是数值的!');
        $("#txtMoney").focus();        
        return false;
    } else {
        checkRst = "OK";
    }

    if ($("#txtMoney").val() == "") {
        //$.messager.alert('提示', '请输入相应的金额!');
        $("#lblDoRusultInfo").text('提示:请输入相应的金额!');
        $("#txtMoney").focus();
        return false;
    } else {
        checkRst = "OK";
    }
    if (checkRst == "OK") {
        $("#btnsave").focus();
    }
    

}


//删除存款明细记录
function DelDetailList() {
    
    var row = $("#detail").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一条要删除的记录");
        return false;
    }    
    if (row.BankFlag!=0) {
        $.messager.alert("提示", "2该记录已经发送到银行了，不能删除");
        return false;
    } else {
        $.messager.confirm('确认', '您是否真的要删除记录吗?', function (r) {
            if (r) {
                $.post("/CashPay/DelDetailList/" + $("#saveTypeId").val(), {
                    "FCode": row.FCrimeCode,
                    "seqno": row.seqno
                }, function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        //$.messager.alert('提示', data); 
                        var words = data.split("|");
                        if (words[0] == "OK") {
                            var index = $('#detail').datagrid('getRowIndex', row);
                            //var flieds = $.parseJSON(words[1]);
                            $('#detail').datagrid('deleteRow', index);

                            //更新用户表的金额
                            var userInfo = $.parseJSON(words[2]);
                            var userRow = $('#test').datagrid('getSelected');
                            var idx = $('#test').datagrid('getRowIndex', userRow);
                            $('#test').datagrid('updateRow', {
                                index: idx,
                                row: {
                                    AmountA: userInfo.AmountA,
                                    AmountB: userInfo.AmountB,
                                    AmountC: userInfo.AmountC,
                                    AllMoney: userInfo.AllMoney
                                }
                            });
                            $.messager.alert("提示", "删除成功");
                        } else {
                            $.messager.alert("提示", data);
                        }
                    }
                });
            }
        });
        
    }
}

//打印存记录
function PrintSavePayList(savePayFlag) {
    var row = $("#detail").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一条要打印的记录");
        return false;
    }
    window.open("/CashPay/PrintSavePayList/" + $("#saveTypeId").val() + "?seqno=" + row.seqno + "&FCode=" + row.FCode + "&savePayFlag=" + savePayFlag);
    
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