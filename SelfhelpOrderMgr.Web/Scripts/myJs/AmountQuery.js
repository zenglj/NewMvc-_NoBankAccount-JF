$(function () {
    $('#test').datagrid({
        title: '账户余额列表',
        iconCls: 'icon-save',
        width: 900,
        height: 320,
        queryParams: {
            fName: '',
            fCode: '00000',
            fAreaName: '',
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Infomgr/AmountSearch',
        sortName: 'fname',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'fcrimecode',
        pageSize: 50,
        pageList: [50, 100],
        onClickRow: function (rowIndex, rowData) {
            var iRowId = rowIndex;
            $('#test').datagrid('clearSelections'); //清除所有的选择项
            $('#test').datagrid('selectRow', iRowId);
            $('#test').datagrid('checkRow', iRowId);
        },
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '用户编号', field: 'fcrimecode', width: 80, sortable: true }
        ]],
        columns: [[
        { field: 'fname', title: '用户姓名', width: 120 },
        {
            field: 'fareaName', title: '队别名称', width: 220, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        {
            field: 'cardflaga', width: 100, sortable: true, title: '卡状态', editor: Text,
            formatter: function (value, row, index) {
                if (value == "1") {
                    return "正常";
                } else if (value == "2") {
                    return "挂失";
                } else if (value == "3") {
                    return "作废";
                } else if (value == "4") {
                    return "停用";
                } else {
                    return "异常";
                }
            }
        },
        { field: 'BankAccNo', title: '银行卡号', width: 250 },
        { field: 'amounta', title: '存款账户', width: 150 },
        { field: 'amountb', title: '报酬账户', width: 150 },
        { field: 'amountc', title: '留存金额', width: 150 },
        { field: 'fmoney', title: '总余额', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnprint',
            text: '余额列表',
            iconCls: 'icon-print',
            handler: function () {
                $('#btnprint').linkbutton('enable');
                window.open("/Infomgr/PrintCardAmountList?action=NewSystem&fCode=" + $("#crimeSearch input[name=fCrimeCode]").val() + "&fName=" + $("#crimeSearch input[name=fCrimeName]").val() + "&fAreaName=" + $('#cc').combobox('getValue'));
            }
        }, '-', {
            id: 'btnprint_bank',
            text: '存款指南',
            iconCls: 'icon-print',
            handler: function () {
                ////console.info();
                var selected = $('#test').datagrid('getSelected');
                if (selected) {
                    window.open("/Infomgr/DepositGuide?fName=" + selected.fname + "&fCode=" + selected.fcrimecode + "&fareaName=" + selected.fareaName + "&bankAccNo=" + selected.BankAccNo);
                }

            }
        }, '-', {
            id: 'btnGSB',
            text: '消费公示表',
            iconCls: 'icon-print',
            handler: function () {
                $('#btnGSB').linkbutton('enable');
                window.open("/Infomgr/PrintXFGBList?action=NewSystem&fCode=" + $("#crimeSearch input[name=fCrimeCode]").val() + "&fName=" + $("#crimeSearch input[name=fCrimeName]").val() + "&fAreaName=" + $('#cc').combobox('getValue') + "&cardStatus=" + $('#cardStatus').combobox('getValue') + "&StartDate=" + $('#StartDate').datetimebox('getValue') + "&EndDate=" + $('#EndDate').datetimebox('getValue'));
            }
        }, '-', {
            id: 'btnExcel',
            text: 'Excel导出',
            iconCls: 'icon-redo',
            handler: function () {
                $('#btnExcel').linkbutton('enable');
                $.post("/Infomgr/ExcelGSBB", {
                    "action":"NewSystem",
                    "fCode":$("#crimeSearch input[name=fCrimeCode]").val(),
                    "fName":$("#crimeSearch input[name=fCrimeName]").val(),
                    "fAreaName": $('#cc').combobox('getValue'),
                    "cardStatus": $('#cardStatus').combobox('getValue'),
                    "StartDate": $('#StartDate').datetimebox('getValue') ,
                    "EndDate": $('#EndDate').datetimebox('getValue')
                    
                }, function (data, status) {
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
        }]
    });
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

    //$('#cc').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});
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

function clearSearch() {
    $("#crimeSearch input").val("");
}

function FilterSearch() {
    if ("" == $("#crimeSearch input[name=fCrimeName]").val() && "" == $("#crimeSearch input[name=fCrimeCode]").val() && "000" == $("#cc").combobox('getValue') && "000" == $("#cardStatus").combobox('getValue')) {
        $.messager.alert('提示', '请输入或是选择相应的查询条件!');

    } else {
        $('#test').datagrid('load', {
            fName: $("#crimeSearch input[name=fCrimeName]").val(),
            fCode: $("#crimeSearch input[name=fCrimeCode]").val(),
            cardStatus: $('#cardStatus').combobox('getValue'),
            fAreaName: $('#cc').combobox('getValue'),
            action: 'NewSystem'
        });
    }
}