$(function () {
    $("#toolbar").empty();
    var btn = "<button type='button' class='btn btn-danger' data-toggle='modal' data-target='#myModalReadList' onclick='ViewPlayList()'><i class='fa fa-plus'></i> 回放记录（ViewList）</button>";
    $("#toolbar").append(btn);

    var $table = $('#tb').bootstrapTable({
        columns: [
            { field: 'FAreaName', title: '监区', align: 'center', valign: 'middle', width: 150, sortable: true },
            { field: 'readcount', title: '查听次数', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FILEPATH', title: '录音文件', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'STARTTIME', title: '会见时间', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLongTime(value);
                    }
                }
            },
            { field: 'remark', title: '复听摘要内容', align: 'center', valign: 'middle', width: 300, sortable: true },
            { field: 'family', title: '会见亲属', align: 'center', valign: 'middle', width: 300, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FMeetType', title: '会见类型', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'CrimeTypeName', title: '犯罪名称', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FMeetRoom', title: '会见室', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'PNO', title: '批次', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'WNO', title: '窗口', align: 'center', valign: 'middle', width: 80, sortable: true }

        ],
        url: "/RecordMgr/GetRecordInfoStr",
        uniqueId: "FCode",
        toolbar: "#toolbar",
        search: true,
        striped: true,
        pagination: true,
        height: 556,
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        showExport: true,                     //是否显示导出
        exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            selectedRow = row;
        }

    });


    //点击选中行，改变选中行的背景颜色
    $('#tb').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });
});

function ViewPlayList() {
    $("#tbodyReadList").empty();
    if (selectedRow == null) {
        alert("请先选择一条录音记录，谢谢");
        return false;
    }
    if (selectedRow.readcount <=0) {
        alert("该录音目前没有回放记录");
        return false;
    }
    if (selectedRow != null) {
        $.post("/RecordMgr/ViewPlayList", { "FCode": selectedRow.FCode, "FilePath": selectedRow.FILEPATH }, function (data, status) {
            if ("success" == status) {                
                for (var i = 0; i < data.length; i++) {
                    var tr = "<tr><td>" + (i+1)+ "</td><td>" + data[i].FLoginName + "</td><td style='width:20%;'>" + getLongTime(data[i].FReadDate) + "</td><td style='width:20%;'>" + getLongTime(data[i].FCloseTime) + "</td><td>" + data[i].FLoginIP + "</td><td>" + getTimeDiff(data[i].FReadDate, data[i].FCloseTime) + "</td></tr>";
                    $("#tbodyReadList").append(tr);
                }
            }
        });
    }
}