﻿$(function () {
    //jquery js 当文本框获得焦点时，自动选中里面的文字
    $(function () {
        $(":text").focus(function () {
            this.select();
        });
    });

    //加载并显示劳报明细列表
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 850,
        height: $(window).height() * 0.9,
        queryParams: {
            fName: '',
            fCode: '00000',
            fAreaName: '',
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        singleSelect: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/BatchCustomer/getLaobaoDetailByBid',
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 10,
        pageList: [10,20,50, 100],
        onSelect: function (rowIndex, rowData) {
            var iRowId = rowIndex;
            //$('#test').datagrid('clearSelections'); //清除所有的选择项
            //$('#test').datagrid('selectRow', iRowId);
            //$('#test').datagrid('checkRow', iRowId);                     
        },
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '用户编号', field: 'FCrimeCode', width: 80, sortable: true }
        ]],
        columns: [[
        { field: 'seqno', title: 'seqno', width: 120, hidden: true },
        { field: 'FCriminal', title: '姓名', width: 120 },
        {
            field: 'FareaName', title: '队别名称', width: 220, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        //{ field: 'FBankAccNo', title: '银行卡号', width: 150 },
        //{ field: 'FAmountA', title: '存款账户', width: 150 },
        //{ field: 'FAmountB', title: '报酬账户', width: 150 },
        //{ field: 'FAmountC', title: '留存金额', width: 150 },
        { field: 'FAmount', title: '实发余额', width: 150, editor: 'numberbox'},
        { field: 'AmountC', title: '留存金额', width: 150 },
        { field: 'Remark', title: '备注', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnDetailDel',
            text: '删除记录',
            iconCls: 'icon-cancel',
            handler: function () {
                var delRow = $("#test").datagrid('getSelected');
                var delIdx = $("#test").datagrid('getRowIndex', delRow);
                DelDetailList(delRow.seqno, delIdx);//删除一条明细记
            }
        }]
    });

    //获取DataGrid Table页数据
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

    //加载显示队别列表
    //$('#cc').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});


    //加载显示队别列表
    //$('#mainAreaName').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});

    $("#winShenHuYuanYing").window('close');
    $("#winBankReturn").window('close');
    //加载主单列表
    loadMainOrderTable();

});
function resize() {
    $('#test').datagrid('resize', {
        width: 700,
        height: 400
    });
}

function getSelected() {
    var selected = $('#test').datagrid('getSelected');
    if (selected) {
        alert(selected.code + ":" + selected.name + ":" + selected.addr + ":" + selected.col4);
    }
}

function getSelections() {
    var ids = [];
    var rows = $('#test').datagrid('getSelections');
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].code);
    }
    alert(ids.join(':'));
}
function clearSelections() {
    $('#test').datagrid('clearSelections');
}
function selectRow() {
    $('#test').datagrid('selectRow', 2);
}
function selectRecord() {
    $('#test').datagrid('selectRecord', '002');
}
function unselectRow() {
    $('#test').datagrid('unselectRow', 2);
}
function mergeCells() {
    $('#test').datagrid('mergeCells', {
        index: 2,
        field: 'addr',
        rowspan: 2,
        colspan: 2
    });
}
//清空查询条件
function clearSearch() {
    $("#crimeSearch input").val("");
}
//过滤查询
function FilterSearch() {
    $('#mainOrderTable').datagrid('load', {
        fCrimeName: $("#crimeSearch input[name=fCrimeName]").val(),
        fCrimeCode: $("#crimeSearch input[name=fCrimeCode]").val(),
        fRemark: $("#crimeSearch input[name=fRemark]").val(),
        PType: $('#PType').combobox('getText'),
        FCourseType: $('#FCourseType').combobox('getText'),
        startDate: $("#crimeSearch input[name=startDate]").val(),
        endDate: $("#crimeSearch input[name=endDate]").val(),
        action: 'GetSearchMainOrder'
    });
}

//删除一条明细记
function DelDetailList(seqno, idx) {
    $.post("/BatchCustomer/DelOrderDetail", {  "seqno": seqno, "sBid": $("#sBid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split(".");
            if (words[0] == "OK") {
                //更新主单数量及金额
                var infos = words[1].split("|");
                UpdateMainOrderMoneyCount(infos[1], infos[2]);
                $("#test").datagrid('deleteRow', idx);
                $.messager.alert('提示', "删除成功");
                return false;
            } else {
                $.messager.alert('提示', data);
            }
        } else {
            alert("通信失败");
        }
    });
}


//确定提交主单
function SubmitMainOrder(Bid) {
    $.post("/BatchCustomer/PostMainOrder/"+$("#PowerId").val(), { "sBid": Bid }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //更新确认状态                                                
                //UpdateMainOrderStatus("1", "FCheckFlag");

                var mainRow = $('#mainOrderTable').datagrid('getSelected');
                var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
                var bouns=$.parseJSON(words[2]);
                $('#mainOrderTable').datagrid('updateRow', {
                    index: idx,
                    row: {
                        FCheckFlag: bouns.FCheckFlag,
                        auditflag: bouns.auditflag,
                        Fdbcheckflag: bouns.Fdbcheckflag,
                        FLAG: bouns.FLAG
                    }
                });

                $('#btnDel').linkbutton('disable');
                $('#btnSubmit').linkbutton('disable');
                $.messager.alert('提示', "提交成功");
                return false;
            } else {
                $.messager.alert('提示', data);
            }
        } else {
            alert("通信失败");
        }
    });
}



//审核已经提交的主单
function AuditMain(Bid) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    if (row == null) {
        $.messager.alert("请选择一条劳报主单记录");
        return false;
    }
    if (row.FCheckFlag == 1) {
        if (row.auditflag == 0) {
            $.post("/BatchCustomer/AuditMainOrder", { "sBid": row.Bid }, function (data, status) {
                if (status == "success") {
                    var words = data.split(".");
                    if (words[0] == "OK") {
                        //更新确认状态                                                
                        UpdateMainOrderStatus("1","auditflag");
                        $.messager.alert('提示', words[1]);
                        return false;
                    } else {
                        $.messager.alert('提示', data);
                    }
                } else {
                    alert("通信失败");
                }
            });
        } else {
            $.messager.alert("该主单已经审核，无需再审核");
            return false;
        }

    } else {
        $.messager.alert("该定单未提交确认，不能审核");
        return false;
    }
    
}

//复核已经提交的主单
function DbCheckMain(Bid) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    if (row == null) {
        $.messager.alert("请选择一条劳报主单记录");
        return false;
    }
    if (row.auditflag == 1) {
        if (row.Fdbcheckflag == 0) {
            $.post("/BatchCustomer/DbCheckMainOrder", { "sBid": row.Bid }, function (data, status) {
                if (status == "success") {
                    var words = data.split(".");
                    if (words[0] == "OK") {
                        //更新确认状态                                                
                        UpdateMainOrderStatus("1","Fdbcheckflag");
                        $.messager.alert('提示', "提交成功");
                        return false;
                    } else {
                        $.messager.alert('提示', data);
                    }
                } else {
                    alert("通信失败");
                }
            });
        } else {
            $.messager.alert("该主单已经审核，无需再审核");
            return false;
        }

    } else {
        $.messager.alert("该定单未提交确认，不能审核");
        return false;
    }

}

//财务入账到VCRD的主单
function InDbMain(Bid) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    if (row == null) {
        $.messager.alert("提示", "请选择一条劳报主单记录");
        return false;
    }
    if (row.Fdbcheckflag == 1) {
        if (row.FLAG == 0) {
            $.messager.progress({
                title: '导入数可能需要几分钟，请耐心等待',
                msg: '数据正在导入中...'
            });
            $.post("/BatchCustomer/InDbMainOrder", { "sBid": row.Bid }, function (data, status) {
                if (status == "success") {
                    $.messager.progress('close');
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        //更新确认状态                                                
                        UpdateMainOrderStatus("1","FLAG");
                        $.messager.alert('提示', words[1]);
                        return false;
                    } else {
                        $.messager.alert('提示', data);
                    }
                } else {
                    $.messager.progress('close');
                    alert("通信失败");
                }
            });
        } else {
            $.messager.alert("提示", "该主单已经审核，无需再审核");
            return false;
        }

    } else {
        $.messager.alert("提示", "请选择一条劳报主单记录");
        return false;
    }

}

//莆田版财务入账，根据银行返回结果
function BankReturnMain() {
    //判断主单判断
    var row = $("#mainOrderTable").datagrid("getSelected");
    if (row == null) {
        $.messager.alert("提示","请选择一条劳报主单记录");
        return false;
    }
    if(row.FLAG==0)
    {
        $("#BankResultExcelBid").val(row.Bid);
        $("#winBankReturn").window('open');
    } else {
        $.messager.alert("提示", "该主单已经导入过到不能重复导入");
        return false;
    }
    

    //显示导入框

}

//莆田版,导入银行结果文件,写入到数据库
function ImportBankExcelResult() {
    //首先判断Excel文件是否为空
    //其次判断主单判断
    //上传到后台去执行
    if ($("#BankResultExcelBid").val() == "")
    {
        $.messager.alert('提示', "主单号不能为空!");
        return false;
    }
    if ($("#BankResultFileName").val() == "") {
        $.messager.alert('提示', "请选择一个Excel文件!");
        return false;
    } else {
        $('#ffBankExcel').form({
            url: "/BatchCustomer/PuTianExcelInport",
            onSubmit: function () {
                // do some check    
                // return false to prevent submit;    
            },
            success: function (data) {
                $.messager.progress('close');
                var words = data.split("|");
                $("#BankResultExcelBid").val('');
                if (words[0] == "OK") {
                    $.messager.alert('提示', words[1]);
                } else {
                    $.messager.alert('提示', data);
                }

            }
        });
        $('#ffBankExcel').submit();
        $.messager.progress({
            title: '导入数可能需要几分钟，请耐心等待',
            msg: '数据正在导入中...'
        });
    }

    
}
//删除主单
function DeleteMainOrder(Bid, idx) {
    $.post("/BatchCustomer/DelMainOrder", {"sBid": Bid }, function (data, status) {
        if (status == "success") {
            //alert(data);
            var words = data.split(".");
            if (words[0] == "OK") {
                $.messager.alert('提示', "删除成功");
                $("#mainOrderTable").datagrid('deleteRow', idx);

            } else {
                $.messager.alert('提示', "删除失败");
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}


//获取指定单的明细记录并显示在Test列表中
function GetOrderDetailToTable(Bid) {
    $('#test').datagrid('load', {
        FBid: Bid,
        action: 'GetOrderDetail'
    });
}

//设定明细框的编辑状态
function SetDetailTextBox(Flag) {
    //alert(Flag);
    if (Flag == "1") {
        $('#iCode').attr("disabled", "disabled");
        $('#iName').attr("disabled", "disabled");
        $('#iMoney').attr("disabled", "disabled");
        $('#btnDetailDel').linkbutton('disable');
        $('#btnsave').linkbutton('disable');
        //$('#btndel').linkbutton('disable'); 
        $('#btnsubmit').linkbutton('disable');
        $('#btnExcelImport').linkbutton('disable');
        $('#CreateAreaList').linkbutton('disable');
        $('#SaveList').linkbutton('disable');

    } else {
        $('#iCode').removeAttr("disabled");
        $('#iName').removeAttr("disabled");
        $('#iMoney').removeAttr("disabled");
        $('#btnDetailDel').linkbutton('enable');
        $('#btnsave').linkbutton('enable');
        //$('#btndel').linkbutton('enable'); 
        $('#btnsubmit').linkbutton('enable');
        $('#btnExcelImport').linkbutton('enable');
        $('#CreateAreaList').linkbutton('enable');
        $('#SaveList').linkbutton('enable');

    }
}

//主单审核的工具按钮状态
function SetMainToolBox(row) {
    //alert(Flag);
    //确认之后不能删除

    $('#btnSave').linkbutton('disable');
    if (row.FCheckFlag == "1") {
        $('#btnDel').linkbutton('disable');
        $('#btnSubmit').linkbutton('disable');
        $('#btnBankReturn').linkbutton('enable');
        
    } else {
        $('#btnDel').linkbutton('enable');
        $('#btnSubmit').linkbutton('enable');
        $('#btnBankReturn').linkbutton('disable');
    }
    //审核之后不能退回监区,即取消提交
    if (row.auditflag == "1") {
        $('#btnAudit').linkbutton('disable');
        $('#btnUndoSubmit').linkbutton('disable');
    } else {
        $('#btnAudit').linkbutton('enable');
        $('#btnUndoSubmit').linkbutton('enable');
    }
    //复核之后不能退回审核,即取消审核
    if (row.Fdbcheckflag == "1") {
        $('#btnDbCheck').linkbutton('disable');
        $('#btnUndoAudit').linkbutton('disable');
    } else {
        $('#btnDbCheck').linkbutton('enable');
        $('#btnUndoAudit').linkbutton('enable');
    }
    //入账之后不能退回复核,即取消复核
    if (row.FLAG == "1") {
        $('#btnInDb').linkbutton('disable');
        $('#btnUndoDbCheck').linkbutton('disable');
        $('#btnBankReturn').linkbutton('disable');
        
    } else {
        $('#btnInDb').linkbutton('enable');
        $('#btnUndoDbCheck').linkbutton('enable');
        //$('#btnBankReturn').linkbutton('enable');//莆田银行返回验证按钮
    }
}
//加载主单列表函数
function loadMainOrderTable() {
    $('#mainOrderTable').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 150,
        queryParams: {
            fName: '',
            fCode: '00000',
            fAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BatchCustomer/getBonus/'+$("#PowerId").val(),
        sortName: 'Bid',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Bid',
        //                pageSize: 50,
        //                pageList: [50, 100],
        onSelect: function (rowIndex, rowData) {
            //var iRowId = rowIndex;
            //$('#test').datagrid('clearSelections'); //清除所有的选择项
            //$('#mainOrderTable').datagrid('selectRow', rowIndex);
            $("#sBid").val(rowData.Bid);
            $("#sOrderStatus").val(rowData.FCheckFlag);
            SetDetailTextBox(rowData.FCheckFlag);//设定明细框的编辑状态                    
            SetMainToolBox(rowData);//主单审核的工具按钮状态
            GetOrderDetailToTable(rowData.Bid);//获取指定单的明细记录并显示在Test列表中
            $("#excelFileName").val("");//清空所选文件名
            SetMainOrderTooltip(rowData);//设定主单的状态提示信息
        },
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'Bid', width: 100, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
        {
            field: 'UDate', title: '月份', width: 100, formatter: function (value, row, index) {
                if (row.UDate != null) {
                    if (row.UDate != "") {
                        nowyear = getShortTime(value);
                        var years = nowyear.split('-');
                    return years[0] + "年" + years[1] + "月";
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
        },
        {
            field: 'ApplyBy', title: '申请人', width: 100, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        {
            field: 'FCheckFlag', title: '确认', width: 60, formatter: function (value, row, index) {
                if (row.FCheckFlag == "1") {
                    return "√";
                } else {
                    return value;
                }
            }
        },
        { field: 'CHECKBY', title: '确认人', width: 100 },
        {
            field: 'Auditflag', title: '审核', width: 60, formatter: function (value, row, index) {
                if (row.Auditflag == "1") {
                    return "√";
                } else {
                    return value;
                }
            }
        },
        { field: 'Auditby', title: '审核人', width: 100 },
        {
            field: 'Fdbcheckflag', title: '复核', width: 60, formatter: function (value, row, index) {
                if (row.Fdbcheckflag == "1") {
                    return "√";
                } else {
                    return value;
                }
            }
        },
        {
            field: 'Flag', title: '入账', width: 60, formatter: function (value, row, index) {
                if (row.Flag == "1") {
                    return "√";
                } else {
                    return value;
                }
            }
        },
        { field: 'FPostBy', title: '入账人', width: 100, sortable: true },
        { field: 'FrealAreaName', title: '审批结果', width: 100, hidden: true, sortable: true },
        { field: 'FAreaName', title: '所属监区', width: 150, sortable: true },
        { field: 'cnt', title: '人数', width: 80 },
        { field: 'FAmount', title: '总余额', width: 100 },
        { field: 'Crtby', title: '发放人', width: 100 },
        {
            field: 'crtdt', title: '创建日期', width: 100, formatter: function (value, row, index) {
        if (row.crtdt != null) {
            if (row.crtdt != "") {
                return getLocalTime(value);
            } else {
                return value;
            }
        } else {
            return value;
        }
    }

        },
        { field: 'Remark', title: '备注', width: 150 }
        ]]
    });
}

//设定主单的状态提示信息
function SetMainOrderTooltip(rowData) {
    var lczt = "";//流程状态
    if (rowData.FLAG == "1") {
        lczt = "已入账";
    } else if (rowData.Fdbcheckflag == "1") {
        lczt = "已复核,等待财务入账中";
    } else if (rowData.AuditFlag == "1") {
        lczt = "已审核,等待财务复核中";
    } else if (rowData.FCheckFlag == "1") {
        lczt = "已确认,等待生科审核中";
    } else {
        lczt = "未确认提交，请尽快确认";
    }

    $('#ddts').tooltip({
        position: 'right',
        content: "<span style='color:#fff'>流程状态：" + lczt + ",原因：" + rowData.FrealAreaName + "</span>",
        onShow: function () {
            $(this).tooltip('tip').css({ backgroundColor: '#666', borderColor: '#666' });
        }
    });
}

//增加主单
function AddMain() {
    $("#saleTypeName").combobox('setValue', "");
    $("#custTypeName").combobox('setValue', "");

    var dt = new Date();
    
    $("#MainDoType").val("Add");
    $('#btnSave').linkbutton('enable');
    $('#btnAdd').linkbutton('disable');
}

//保存主单
function SaveMain() {
    if ($("#sapplyby").val() == "") {
        $.messager.alert('提示', '申请人不能为空');
        return false;
    }
    if ($("#sremark").val() == "") {
        $.messager.alert('提示', '批扣摘要不能为空');
        return false;
    }
    if ($("#MainDoType").val() == "Add") {
        $("#MainAction").val("AddMainOrder");
        $("#saleTypeNameMain").val($("#saleTypeName").combobox('getText'));
        $("#saleTypeFlagId").val($("#saleTypeName").combobox('getValue'));
        $("#custTypeNameMain").val($("#custTypeName").combobox('getText'));
        $("#custTypeCodeMain").val($("#custTypeName").combobox('getValue'));
        $('#ffMainOrder').form({
            url: "/BatchCustomer/AddMainOrder",
            onSubmit: function () {
            },
            success: function (data) {
                var words = data.split('|');
                if (words[0] == "OK") {
                    var infos = $.parseJSON(words[1]);
                    $('#sBid').val(infos.Bid);
                    $('#mainOrderTable').datagrid('insertRow', {
                        index: 0,
                        row: {
                            Bid: infos.Bid,
                            udate: infos.udate,
                            ApplyBy: $("#sapplyby").val(),
                            FCheckFlag: "0",
                            CHECKBY: "",
                            auditflag: "",
                            auditby: "",
                            Fdbcheckflag: "",
                            FLAG: "",
                            FPostBy: "",
                            fareaName: infos.FAreaName,
                            cnt: "0",
                            fAMOUNT: "0",
                            Crtby: infos.Crtby,
                            crtdt: infos.crtdt,
                            Reamrk: $("#sremark").val()
                        }
                    });
                    $("#sapplyby").val('');
                    $("#MainDoType").val('');
                    $('#btnsave').linkbutton('disable');
                    $.messager.alert('提示', '保存成功');
                } else {
                    $('#sBid').val("");
                    $.messager.alert('提示', data);
                }
            }
        });
        $('#MainDoType').submit();
    } else {
        $.messager.alert('提示', '请点增加，才填写主单');
    }
    $('#btnSave').linkbutton('disable');
    $('#btnAdd').linkbutton('enable');
}

//提交主单
function SubmitMain() {
    var okRow = $("#mainOrderTable").datagrid('getSelected');
    if (okRow == null) {
        $.messager.alert("提示", "请选择一条主单");
        return false;
    }
    SubmitMainOrder(okRow.Bid);//确认提交主单
}

//取消提交确认，退回监区
function undoSubmitAction(undoAction) {
    var okRow = $("#mainOrderTable").datagrid('getSelected');
    if (okRow == null)
    {
        $.messager.alert("提示", "请选择一条主单");
        return false;
    }
    $("#FrealAreaName").val('');
    $("#undoAction").val(undoAction);
    $("#winShenHuYuanYing").window('open');

    //SubmitMainOrder(okRow.Bid);//确认提交主单
}

//提交退出操作，到后台去执行
function undoYuanYingTJ() {
    var okRow = $("#mainOrderTable").datagrid('getSelected');
    if (okRow == null) {
        $.messager.alert("提示", "请选择一条主单");
        return false;
    }
    var undoAction = $("#undoAction").val();
    var undoRemark = $("#FrealAreaName").val();
    $.post("/BatchCustomer/undoYuanYingTJ/" + $("#PowerId").val(), {
        "Bid": okRow.Bid,
        "undoAction": undoAction,
        "undoRemark": undoRemark
    }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //$.messager.alert('提示', "删除成功");
                var bonus = $.parseJSON(words[1]);
                var idx = $('#mainOrderTable').datagrid('getRowIndex', okRow);
                $('#mainOrderTable').datagrid('updateRow', {
                    index: idx,
                    row: {
                        FCheckFlag: bonus.FCheckFlag,
                        auditflag: bonus.auditflag,
                        Fdbcheckflag: bonus.Fdbcheckflag,
                        FLAG: bonus.FLAG,
                        FrealAreaName: bonus.FrealAreaName
                    }
                });
                var rowData = $("#mainOrderTable").datagrid('getSelected');
                SetMainOrderTooltip(rowData);//设定主单的状态提示信息
                $("#undoAction").val('');
                $("#FrealAreaName").val('');
                $("#winShenHuYuanYing").window('close');
            } else {
                $.messager.alert('提示', words[1]);
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}

//删除主单
function DeleteMain() {
    $.messager.confirm('提示', '您真的删除该主单吗?', function (r) {
        if (r) {
            var delRow = $("#mainOrderTable").datagrid('getSelected');
            if (delRow.auditflag == 1) {
                $.messager.alert('提示', '主单已经审核不能删除!');
                return false;
            } else {
                var idx = $("#mainOrderTable").datagrid('getRowIndex', delRow);
                DeleteMainOrder(delRow.Bid, idx);//删除主单
            }
        }
    });
}

//打印主单
function PrintMain() {
    //$('#btnprint').linkbutton('enable');
    var row = $("#mainOrderTable").datagrid("getSelected");
    window.open("/BatchCustomer/PrintReportList?Bid=" + row.Bid);
}

//Excel导出主单
function ExcelMain(id) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    $("#action").val("ExcelOut");    
    $("#FBidExcel").val(row.Bid);
    id = $("#PowerId").val();
    $('#ffExcel').form({
        url: "/BatchCustomer/ExcelOut/"+id,
        onSubmit: function () {
                        
        },
        success: function (data) {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
            else {
                $.messager.alert("提示", data);
            }
        }
    });
    $('#ffExcel').submit();
}

//狱号框内按回车
function noNumbers() {
    var code = $("#iCode").val();
    //var name=$("#iName").val();
    if ("" == code) {
        //console.info($("#InCY").val());
        //$.messager.alert('提示', '请输入相应的编号!'); 
        $("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
    } else {
        $.post("/BatchCustomer/QueryCrimeCode", {
            "FCode": code
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split(".");
                if (words[0] == "OK") {
                    var flieds = words[1].split("|");
                    if (flieds[2] == 1) {
                        //$.messager.alert('提示', "该犯人已经离监结算了，不能再存!");
                        $("#lblInfo").text('该犯人已经离监结算了，不能再存!');
                        $("#iCode").focus();
                    } else {
                        if (flieds[3] == 1) {
                            $("#iName").val(flieds[1]);
                            $("#iCpct").val(flieds[4]);
                            $("#lblInfo").text('');
                            $("#iMoney").focus();
                        } else {
                            $.messager.alert('提示', flieds[4]);
                        }
                    }
                } else {
                    //$.messager.alert('提示', "对不起,您输入的狱号不存在!");
                    $("#lblInfo").text('对不起,您输入的狱号不存在!');
                    $("#iCode").focus();
                }
            }
        });
    }
}
//金额框内按回车，可以直接发放钱
function noMoneyEnter() {
    
    if ($("#sOrderStatus").val() == "1") {
        $.messager.alert('提示', '主单已经确认,不可以再编辑!');
        return false;
    }
    if ($("#iCode").val() == "") {
        //$.messager.alert('提示', '请输入相应的编号!');

        //$("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
        return false;
    }
    if (isNaN($("#iMoney").val())) {
        $("#lblInfo").text('金额必须是数值的!');
        $("#iMoney").focus();
        return false;
    } else {
    }
    if ($("#iName").val() == "") {
        //$.messager.alert('提示', '请输入相应的姓名!');
        $("#lblInfo").text('请输入相应的姓名!');
        $("#iName").focus();
        return false;
    }
    if ($("#iMoney").val() == "") {
        //$.messager.alert('提示', '请输入相应的金额!');
        $("#lblInfo").text('请输入相应的金额!');
        $("#iMoney").focus();
        return false;
    }
    if ($("#iCpct").val() == "") {
        //$.messager.alert('提示', '处遇信息不正确!');
        $("#lblInfo").text('处遇信息不正确!');
        $("#iCode").focus();
        return false;
    }
    var code = $("#iCode").val();
    //var name=$("#iName").val();

    if ("" == code) {
        //console.info($("#InCY").val());
        //$.messager.alert('提示', '请输入相应的编号!');
        $("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
    } else {
        $("#iMoney").val(toDecimal2($("#iMoney").val()));
        $.post("/BatchCustomer/AddOrderDetail", {
            "FCode": code, "FName": $("#iName").val(), "FMoney": $("#iMoney").val(), "FBid": $("#sBid").val()
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split(".");
                if (words[0] == "OK") {
                    words[1] = data.substring(3);
                    var infos = words[1].split("|");
                    //增加一条记录
                    $('#test').datagrid('appendRow', {
                        FCrimeCode: infos[0],
                        FCriminal: infos[1],
                        FareaName: infos[2],
                        FAmount: infos[3],
                        AmountC: toDecimal2(infos[4]),
                        Remark: infos[5]
                    });

                    //$('#test').datagrid('selectRow',0);
                    //更新主单数量及金额
                    UpdateMainOrderMoneyCount(infos[6], infos[7]);
                    $("#iCode").val("");
                    $("#iName").val("");
                    //$("iMoney").val("");
                    $("#iCode").focus();
                    $("#iCode").val("");
                } else {
                    //$.messager.alert('提示', words[1]);
                    $("#lblInfo").text(words[1]);
                    $("#iCode").focus();
                }
            }
        });
    }
}

//Excel文件导入
function ExcelFileInport() {

    if ($("#excelFileName").val() == "") {
        $.messager.alert('提示', "请选择一个Excel文件!");
        return false;
    } else {

        var row = $('#mainOrderTable').datagrid("getSelected");
        if (row) {
            $('#excelBid').val(row.Bid);//设定主单号Bid
            $('#excelAction').val("ExcelInport");//设定action动作

            $('#ffDetailEditBoxExcel').form({
                url: "/BatchCustomer/ExcelInport",
                onSubmit: function () {
                    // do some check    
                    // return false to prevent submit;    
                },
                success: function (data) {
                    //alert(data);
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var rtn = $.parseJSON(words[2]);                        
                        //更新主单的金额及数量
                        var mainRow = $('#mainOrderTable').datagrid('getSelected');
                        var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
                        $('#mainOrderTable').datagrid('updateRow', {
                            index: idx,
                            row: {
                                cnt: rtn.trade.cnt,
                                FAmount: rtn.trade.FAmount
                            }
                        });
                        //插入明细记录
                        for (var i = 0; i < rtn.dtls.length; i++) {
                            var dtl = rtn.dtls[i];
                            $('#test').datagrid('appendRow', {
                                FCrimeCode: rtn.dtls[i].FCrimeCode,
                                seqno: rtn.dtls[i].seqno,
                                FCriminal: rtn.dtls[i].FCriminal,
                                FareaName: rtn.dtls[i].FareaName,
                                FAmount: rtn.dtls[i].FAmount,
                                AmountC: rtn.dtls[i].AmountC,
                                Remark: rtn.dtls[i].Remark
                            });


                        }
                        $.messager.alert('提示', words[1]);
                    }
                }
            });
            $('#ffDetailEditBoxExcel').submit();
        } else {
            $.messager.alert('提示', "请选择一个主单!");
        }
    }
}

//批量生成劳报记录
function CreateAreaList() {
    $.post("/BatchCustomer/CreateAreaList", { "sBid": $("#sBid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //$.messager.alert('提示', "删除成功");
                var lists = $.parseJSON(words[1]);
                if (lists.length > 0) {
                    for (var i = 0; i < lists.length; i++) {
                        var list = lists[i];
                        //增加一条记录
                        $('#test').datagrid('appendRow', {
                            FCRIMECODE: list.FCRIMECODE,
                            fcriminal: list.fcriminal,
                            fareaName: list.fareaName,
                            FAMOUNT: list.FAMOUNT,
                            AmountC: toDecimal2(list.AmountC),
                            remark: list.remark
                        });
                    }
                }
                //$("#mainOrderTable").datagrid('deleteRow', idx);

            } else {
                $.messager.alert('提示', words[1]);
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}

var $dg = $("#test");

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

        $.post("/BatchCustomer/BatchSaveDtlList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var model = $.parseJSON(words[1]);
                        var row = $("#mainOrderTable").datagrid('getSelected');
                        var idx = $("#mainOrderTable").datagrid('getRowIndex', row);
                        $dg.datagrid('acceptChanges');
                        $('#mainOrderTable').datagrid('updateRow', {
                            index: idx,
                            row: {
                                cnt: model.cnt,
                                FAmount: model.FAmount
                            }
                        });

                        
                        
                        $.messager.alert("提示", "保存成功！");
                    }
                    else {
                        $dg.datagrid('rejectChanges');
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

//导出Excel导入的失败记录
function ErrorListExcelOut() {
    var row = $('#mainOrderTable').datagrid("getSelected");
    if (row) {
        $('#excelBid').val(row.Bid);//设定主单号Bid
        $('#excelAction').val("ErrorListOutport");//设定action动作

        $('#ffDetailEditBoxExcel').form({
            url: "/BatchCustomer/ErrorListOutport",
            onSubmit: function () {
                
            },
            success: function (data) {
                //alert(data)
                var words = data.split("|");
                if (words[0] == "OK") {
                    window.open("/Upload/" + words[1]);
                }
            }
        });
        $('#ffDetailEditBoxExcel').submit();
    } else {
        $.messager.alert('提示', "请选择一个主单!");
    }
}

//更新主单数量及金额
function UpdateMainOrderMoneyCount(cnt, famount) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);

    $('#mainOrderTable').datagrid('updateRow', {
        index: idx,
        row: {
            cnt: cnt,
            FAmount: famount
        }
    });
}

//更新主单确认状态 ,ChangeField
function UpdateMainOrderStatus(checkflag,ChangeField) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
    if (ChangeField == "FCheckFlag") {
        $('#mainOrderTable').datagrid('updateRow', {
            index: idx,
            row: {
                FCheckFlag: checkflag
            }
        });
    } else if (ChangeField == "auditflag") {
        $('#mainOrderTable').datagrid('updateRow', {
            index: idx,
            row: {
                auditflag: checkflag
            }
        });
    } else if (ChangeField == "Fdbcheckflag") {
        $('#mainOrderTable').datagrid('updateRow', {
            index: idx,
            row: {
                Fdbcheckflag: checkflag
            }
        });
    } else if (ChangeField == "FLAG") {
        $('#mainOrderTable').datagrid('updateRow', {
            index: idx,
            row: {
                FLAG: checkflag
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