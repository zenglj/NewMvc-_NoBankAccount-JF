$(function () {
    $('#test').datagrid({
        title: '消费记录',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.9,
        queryParams: {
            fName: $("#crimeSearch input[name=fCrimeName]").val(),
            fCode: $("#crimeSearch input[name=fCrimeCode]").val(),
            //fAreaName: $("#crimeSearch input[name=fAreaName]").val(),
            startDate: $("#crimeSearch input[name=fStartDate]").val(),
            endDate: $("#crimeSearch input[name=fEndDate]").val(),
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Infomgr/SearchInfo',
        sortName: 'CDate',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'CDate',
        pageSize: 50,
        pageList: [50, 100],
        frozenColumns: [[
            { field: 'ck', checkbox: true },
            { title: '用户编号', field: 'fcrimecode', width: 80, sortable: true }
        ]],
        columns: [[
            { field: 'fname', title: '姓名', width: 120 },
            {
                field: 'CDate', title: '日期', width: 220, sortable: true, //rowspan: 2,
                formatter: function (value, row, index) {
                    if (row.CDate) {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                }
            },
            { field: 'Cmoney', title: '金额', width: 150 },
            { field: 'Dtype', title: '类型', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnprint',
            text: '打印报表',
            iconCls: 'icon-print',
            handler: function () {
                $('#btnprint').linkbutton('enable');
                window.open("/Infomgr/InfoListPrint?action=" + $("#action").val() + "&fCode=" + $("#fCrimeCode").val() + "&fName=" + $("#fCrimeName").val() + "&startDate=" + $("#fStartDate").datetimebox('getValue') + "&endDate=" + $("#fEndDate").datetimebox('getValue'));
            }
        }, '-', {
            id: 'btnExcel',
            text: 'Excel导出',
            iconCls: 'icon-save',
            handler: function () {
                $.post("/Infomgr/ExcelOutList/", { "fcrimecode": $("#fCrimeCode").val(), "fcrimename": $("#fCrimeName").val(), "StartDate": $("#fStartDate").datetimebox('getValue'), "EndDate": $("#fEndDate").datetimebox('getValue') }, function (data, status) {
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

    //队别信息
    $('#cc').combo({
        required: true,
        multiple: true
    });

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
    $.post("/Infomgr/ExistsCriminalCheck", {
        fName: $("#crimeSearch input[name=fCrimeName]").val(),
        fCode: $("#crimeSearch input[name=fCrimeCode]").val()
    }, function (data,status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                //再去执行查询消费记录
                $('#test').datagrid('load', {
                    fName: $("#crimeSearch input[name=fCrimeName]").val(),
                    fCode: $("#crimeSearch input[name=fCrimeCode]").val(),
                    //fAreaName: $("#crimeSearch input[name=fAreaName]").val(),
                    startDate: $("#crimeSearch input[name=fStartDate]").val(),
                    endDate: $("#crimeSearch input[name=fEndDate]").val(),
                    action: 'NewSystem'
                });
            } else {
                $.messager.alert("提示",data);
            }
        }
    });
}