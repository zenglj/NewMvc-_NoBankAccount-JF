$(function () {
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
        height: 280,
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
        url: '/Peihuo/GetOutDetailsById/' + $("#xfTypeFlag").val(),
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 50,
        pageList: [50, 100],
        onClickRow: function (rowIndex, rowData) {
            var iRowId = rowIndex;
            //$('#test').datagrid('clearSelections'); //清除所有的选择项
            $('#test').datagrid('selectRow', iRowId);
            //$('#test').datagrid('checkRow', iRowId);                     
        },
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '编号', field: 'Fcrimecode', width: 80, sortable: true }
        ]],
        columns: [[
        { field: 'seqno', title: 'seqno', width: 120, hidden: true },
        { field: 'fsn', title: '流水号', width: 120, hidden: true },
        { field: 'fcriminal', title: '姓名', width: 120 },
        {
            field: 'fareaName', title: '队别名称', width: 220, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        //{ field: 'FBankAccNo', title: '银行卡号', width: 150 },
        //{ field: 'FAmountA', title: '存款账户', width: 150 },
        //{ field: 'FAmountB', title: '报酬账户', width: 150 },
        //{ field: 'FAmountC', title: '留存金额', width: 150 },
        {
            field: 'OrderDate', title: '日期', width: 100, formatter: function (value, row, index) {
                if (row.OrderDate != null) {
                    if (row.OrderDate != "") {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
        },
        { field: 'Amount', title: '金额', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnDetailDel',
            text: '单条删除',
            iconCls: 'icon-cancel',
            handler: function () {
                var delRow = $("#test").datagrid('getSelected');
                var delIdx = $("#test").datagrid('getRowIndex', delRow);
                DelDetailList(delRow.seqno, delIdx);//删除一条明细记
            }
        }, {
            id: 'btnDetailDelAll',
            text: '全部删除',
            iconCls: 'icon-cancel',
            handler: function () {
                $.messager.confirm('提示', '您真的要删除全部的配货明细单吗?', function (r) {
                    if (r) {
                        $("#test").datagrid('selectAll');                        
                        var delRow = $("#test").datagrid('getSelected');
                        var delIdx = $("#test").datagrid('getRowIndex', delRow);
                        DelDetailAllList(delRow.seqno, delIdx);//删除全部明细记录
                    }
                });
                
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

    

    //加载主单列表
    loadMainOrderTable();

    loadInvDetailTable();

    //初始化各种按钮状态
    initBtnStatus();

    $("#winCustList").window('close');
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
        fAreaName: $('#cc').combobox('getText'),
        startDate: $("#crimeSearch input[name=startDate]").val(),
        endDate: $("#crimeSearch input[name=endDate]").val(),
        searchFlag: $("#searchFlag").combobox('getValue'),
        action: 'GetSearchMainOrder'
    });
}

//删除一条明细记
function DelDetailList(seqno, idx) {
    $.post("/Peihuo/DelOrderDetail", {  "seqno": seqno, "sbid": $("#sbid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //更新主单数量及金额
                UpdateMainOrderMoneyCount(words[2]);
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

//删除全部明细记录
function DelDetailAllList(seqno, idx) {
    $.post("/Peihuo/DelDetailAllList", { "seqno": seqno, "sbid": $("#sbid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //更新主单数量及金额
                UpdateMainOrderMoneyCount(words[2]);
                
                var rows = $("#test").datagrid('getRows');
                for (var i = rows.length-1; i >=0; i--) {
                    $("#test").datagrid('deleteRow', i);
                }
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
function SubmitMainOrder(bid,flag) {
    $.post("/Peihuo/PostMainOrder", { "sbid": bid, "setFlag": flag,"srcvby":$("#srcvby").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //更新确认状态                                                
                UpdateMainOrderStatus(flag);
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

//删除主单
function DeleteMainOrder(bid, idx) {
    $.post("/Peihuo/DelMainOrder", {"sbid": bid }, function (data, status) {
        if (status == "success") {
            //alert(data);
            var words = data.split("|");
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
function GetOrderDetailToTable(bid) {
    $('#test').datagrid('load', {
        FBid: bid,
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
function SetMainToolBox(Flag) {
    //alert(Flag);
    if (Flag == "1") {
        $('#btndel').linkbutton('disable');
    } else {
        $('#btndel').linkbutton('enable');
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
            fCrimeCode: '00000',
            fAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Peihuo/getInvOuts/'+ $("#xfTypeFlag").val(),
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        //                pageSize: 50,
        //                pageList: [50, 100],
        onClickRow: function (rowIndex, rowData) {
            //selectAndDisplayDetail(rowIndex, rowData);
        },
        onSelect: function (rowIndex, rowData) {
            selectAndDisplayDetail(rowIndex, rowData);
        },
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 100, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
        { field: 'fsn', title: '主单号', width: 100 },
        { field: 'Amount', title: '金额', width: 100 },
        { field: 'CrtBy', title: '创建人', width: 100 },
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
        { field: 'RcvBy', title: '收货人', width: 100 },
        { field: 'remark', title: '摘要', width: 100 },
        {
            field: 'Flag', title: '状态', width: 100, formatter: function (value, row, index) {
                if (row.Flag != null) {
                    if (row.Flag == "0") {
                        return "未配货";
                    } else if (row.Flag == "1") {
                        return "已配货";
                    } else {
                        return "已收货";
                    }
                } else {
                    return value;
                }
            }

        },
        {
            field: 'typeflag', title: '类型标志', width: 100, formatter: function (value, row, index) {
                if (row.typeflag != null) {
            if (row.typeflag == "7") {
                return "超市消费";
            } else if (row.typeflag == "16") {
                return "医院消费";
            } else if (row.typeflag == "21") {
                return "加餐消费";
            } else if (row.typeflag == "22") {
                return "书报消费";
            } else if (row.typeflag == "23") {
                return "被服消费";
            } else if (row.typeflag == "24") {
                return "订购水果";
            } else {
                return value;
            }
        } else {
            return value;
        }
    }

        }        
        ]]        
    });
}

//增加主单
function AddPeihuoOrder() {    
    $("#MainDoType").val("Add");
    $('#btnsave').linkbutton('enable');
    //$("#mainAreaName").combobox('setValue', "");
}

//保存主单
function SavePeihuoOrder() {
    if ($("#sremark").val() == "") {
        $.messager.alert('提示', '请输入配货单摘要内容');
        return false;
    }
    
    if ($("#MainDoType").val() == "Add") {
        $("#MainAction").val("AddMainOrder");
        //$("#sAreaName").val($("#mainAreaName").combobox('getText'));
        //$("#sAreaCode").val($("#mainAreaName").combobox('getValue'));
        $('#ffMainOrder').form({
            url: "/Peihuo/AddMainOrder/" + $("#xfTypeFlag").val(),
            onSubmit: function () {
            },
            success: function (data) {
                var words = data.split('|');
                if (words[0] == "OK") {
                    var infos = $.parseJSON(words[1]);
                    $('#sbid').val(infos[0]);
                    $('#mainOrderTable').datagrid('insertRow', {
                        index: 0,
                        row: {
                            seqno: infos.seqno,
                            fsn: infos.fsn,
                            Amount: infos.Amount,
                            CrtBy: infos.CrtBy,
                            crtdt: infos.crtdt,
                            RcvBy: infos.RcvBy,
                            Flag: infos.Flag,
                            remark: infos.remark,
                            typeflag: infos.typeflag
                        }
                    });
                    $("#sapplyby").val('');
                    $("#MainDoType").val('');
                    $('#btnsave').linkbutton('disable');
                    $.messager.alert('提示', '保存成功');
                } else {
                    $('#sbid').val("");
                    $.messager.alert('提示', data);
                }
            }
        });
        $('#MainDoType').submit();
    } else {
        $.messager.alert('提示', '请点增加，才填写主单');
    }
}

//载入明细
function LoadPeihuoDetail() {
    var row = $('#mainOrderTable').datagrid('getSelected');
    if (row == null) {
        $.messager.alert('提示', '请选择一条记录');
        return false;
    } else {
        if (row.Flag >= 1) {
            $.messager.alert('提示', '已经确认配货的不能再导入');
            return false;
        } else {
            //loadInvDetailTable();
            //$('#invDetails').datagrid('loadData', { total: 0, rows: [] });
            var rows = $('#invDetails').datagrid('getRows');
            if (rows != null) {
                for (var i = rows.length - 1; i >= 0; i--) {
                    var idx = $('#invDetails').datagrid('getRowIndex', rows[i]);
                    $('#invDetails').datagrid('deleteRow', idx);
                }
            }
            $("#wselOutId").val(row.fsn);
            $("#winCustList").window('open');
        }
    }
}

//确认配货
function CheckPeihuo() {
    var okRow = $("#mainOrderTable").datagrid('getSelected');
    SubmitMainOrder(okRow.seqno, '1');//确认提交主单
}

//确认收货
function CheckShouhuo() {
    var okRow = $("#mainOrderTable").datagrid('getSelected');
    SubmitMainOrder(okRow.seqno, '2');//确认提交主单
}

//删除主单
function DelPeihuoOrder() {
    $.messager.confirm('提示', '您真的删除该主单吗?', function (r) {
        if (r) {
            var delRow = $("#mainOrderTable").datagrid('getSelected');
            if (delRow.Flag != 0) {
                $.messager.alert('提示', '主单已经配货了不能删除!');
                return false;
            } else {
                var idx = $("#mainOrderTable").datagrid('getRowIndex', delRow);
                DeleteMainOrder(delRow.seqno, idx);//删除主单
            }
        }
    });
}

//打印配货单,0是按队别，1是按号房，2是按机号，3是按个人
function PrintOutArea(roomNoFlag) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    if (roomNoFlag == 0) {
        window.open("/Peihuo/PrintOutArea?FBidExcel=" + row.fsn);

    } else if(roomNoFlag==1) {
        window.open("/Peihuo/PrintOutArea?FBidExcel=" + row.fsn + "&roomNoFlag=" + roomNoFlag);
    } else if(roomNoFlag==2) {
        window.open("/Peihuo/PrintOutArea?FBidExcel=" + row.fsn + "&roomNoFlag=" + roomNoFlag);
    }else {
        window.open("/Peihuo/PrintOutArea?FBidExcel=" + row.fsn + "&roomNoFlag=" + roomNoFlag);
    }
}
//Excel导出配货单,0是按队别，1是按号房
function ExcelSumOrder(roomNoFlag) {
    var row = $("#mainOrderTable").datagrid("getSelected");
    $("#action").val("ExcelOut");
    $("#FBidExcel").val(row.fsn);
    $('#ffExcel').form({
        url: "/Peihuo/ExcelSumOrder/" + roomNoFlag + "?FBidExcel=" + row.fsn,
        onSubmit: function () {

        },
        success: function (data) {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
    $('#ffExcel').submit();

}

//初始化各种按钮状态
function initBtnStatus() {
    $('#btnLoadInv').linkbutton('disable');
    $('#btnPeihuo').linkbutton('disable');
    $('#btnShouhuo').linkbutton('disable');

    $('#btnDetailDel').linkbutton('disable');
    $('#btndel').linkbutton('disable');
}

//选择主单一行时显示列表记录
function selectAndDisplayDetail(rowIndex, rowData) {
    //$('#mainOrderTable').datagrid('selectRow', rowIndex);
    
    $("#sbid").val(rowData.fsn);
    $("#sOrderStatus").val(rowData.FCheckFlag);
    GetOrderDetailToTable(rowData.fsn);//获取指定单的明细记录并显示在Test列表中
    if (rowData.Flag >= 1) {//让按钮不能用
        $('#btnLoadInv').linkbutton('disable');
        $('#btnDetailDel').linkbutton('disable');
        $('#btnPeihuo').linkbutton('disable');
        if (rowData.Flag=="1") {
            $('#btnShouhuo').linkbutton('enable');
        } else {
            $('#btnShouhuo').linkbutton('disable');
        }
        $('#btndel').linkbutton('disable');
    } else {
        $('#btnLoadInv').linkbutton('enable');
        $('#btnDetailDel').linkbutton('enable');
        $('#btnPeihuo').linkbutton('enable');
        $('#btnShouhuo').linkbutton('disable');
        $('#btndel').linkbutton('enable');
    }
}
//加载Inv明细记录
function loadInvDetailTable() {
    $('#invDetails').datagrid({
        title: '消费记录',
        iconCls: 'icon-save',
        //width: 900,
        height: 250,
        queryParams: {
            fName: '',
            fCode: '00000',
            fAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        //singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Peihuo/getInvDetails/' + $("#xfTypeFlag").val(),
        sortName: 'InvoiceNo',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'InvoiceNo',
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '流水号', field: 'InvoiceNo', width: 100, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'BankFlag', title: '回款状态', width: 100, formatter: function (value, row, index) {
                    if (row.BankFlag != null) {
                        if (row.BankFlag >1) {
                            return '已成功';
                        } else {
                            return '未回款';
                        }
                    } else {
                        return value;
                    }
                }

            },
        { field: 'FCrimeCode', title: '编号', width: 100 },
        { field: 'FCriminal', title: '姓名', width: 100 },
        { field: 'Amount', title: '金额', width: 100 },
        {
            field: 'OrderDate', title: '消费日期', width: 100, formatter: function (value, row, index) {
                if (row.OrderDate != null) {
                    if (row.OrderDate != "") {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }

        },
        { field: 'PType', title: '消费类型', width: 100 },
        { field: 'FAreaName', title: '队别', width: 100 }

        ]],
        pageSize: 300,
        pageList: [100, 200, 300, 500],
        pagination: true
        //rownumbers: true
    });

}

//弹出窗口查询Invoice记录
function wselSelectInvs() {
    $('#invDetails').datagrid('load', {
        fCrimeName: $("#wselFName").val(),
        fCrimeCode: $("#wselFCode").val(),
        fRemark: $("#wselRemark").val(),
        fAreaName: $('#wselAreaName').combobox('getText'),
        startDate: $("#wselStartDate").datetimebox('getValue'),
        endDate: $("#wselEndDate").datetimebox('getValue'),
        wselCrtBy: $('#wselCrtBy').combobox('getValue'),
        typeFlag: $("#xfTypeFlag").val(),
        rtnBankFlag: $('#rtnBankFlag').combobox('getValue'),
        action: 'GetSearchMainOrder'
    });
      
    
}

//选择全部记录
function wSelectAll() {
    $("#invDetails").datagrid('selectAll');
}

//载入Invoice到配货单
function wLoadPeihuoList() {
    var invoiceNos = "";
    var rows = $("#invDetails").datagrid('getSelections');
    if (rows != null) {        
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (invoiceNos == "") {
                invoiceNos = row.InvoiceNo;
            } else {
                invoiceNos = invoiceNos + "|" +row.InvoiceNo;
            }            
        }
        $.post("/Peihuo/AddOrderDetail/" + $("#xfTypeFlag").val(), {
            "outId": $("#wselOutId").val(), "invoiceNos": invoiceNos
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split("|");
                if (words[0] == "OK") {
                    var mainRow = $('#mainOrderTable').datagrid('getSelected');
                    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
                    $('#mainOrderTable').datagrid('updateRow', {
                        index: idx,
                        row: {
                            Amount: words[2]
                        }
                    });
                    $("#winCustList").window('close');
                    GetOrderDetailToTable($("#wselOutId").val());

                    
                    $.messager.alert('提示', words[1]);
                } else {
                    $.messager.alert('提示', words[1]);
                }
            }
        });
    }

    
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
        $.post("/Peihuo/QueryCrimeCode", {
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
        $.post("/Peihuo/AddOrderDetail", {
            "FCode": code, "FName": $("#iName").val(), "FMoney": $("#iMoney").val(), "FBid": $("#sbid").val()
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
                        FCRIMECODE: infos[0],
                        fcriminal: infos[1],
                        fareaName: infos[2],
                        FAMOUNT: infos[3],
                        AmountC: toDecimal2(infos[4]),
                        remark: infos[5]
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
            $('#excelBid').val(row.BID);//设定主单号Bid
            $('#excelAction').val("ExcelInport");//设定action动作

            $('#ffDetailEditBoxExcel').form({
                url: "/Peihuo/ExcelInport",
                onSubmit: function () {
                    // do some check    
                    // return false to prevent submit;    
                },
                success: function (data) {
                    alert(data)
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
    $.post("/Peihuo/CreateAreaList", { "sbid": $("#sbid").val() }, function (data, status) {
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

        $.post("/Peihuo/BatchSaveDtlList", effectRow,
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
                                fAMOUNT: model.fAMOUNT
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
        $('#excelBid').val(row.BID);//设定主单号Bid
        $('#excelAction').val("ErrorListOutport");//设定action动作

        $('#ffDetailEditBoxExcel').form({
            url: "/Peihuo/ErrorListOutport",
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
function UpdateMainOrderMoneyCount(famount) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);

    $('#mainOrderTable').datagrid('updateRow', {
        index: idx,
        row: {
            Amount: famount
        }
    });
}

//更新主单确认状态
function UpdateMainOrderStatus(checkflag) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
    $('#mainOrderTable').datagrid('updateRow', {
        index: idx,
        row: {
            Flag: checkflag
        }
    });
    if (checkflag == "2") {
        $('#btnLoadInv').linkbutton('disable');
        $('#btnPeihuo').linkbutton('disable');
        $('#btnShouhuo').linkbutton('disable');
        $('#btnDetailDel').linkbutton('disable');
        $('#btndel').linkbutton('disable');
    } else if (checkflag == "1") {
        $('#btnLoadInv').linkbutton('disable');
        $('#btnPeihuo').linkbutton('disable');
        $('#btnShouhuo').linkbutton('enable');

        $('#btnDetailDel').linkbutton('disable');
        $('#btndel').linkbutton('disable');
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