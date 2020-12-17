

$(document).ready(function () {
   

    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'keyId', title: '序号', visible: false, align: 'center', valign: 'middle', width: 70, sortable: true },
            {
                field: 'CreateDate', title: '更新时间', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLongTime(value);
                    }
                }
            },
            { field: 'FCode', title: '狱号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '姓名', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FCyType', title: '处遇类型', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FAreaName', title: '所在队别', align: 'center', valign: 'middle', width: 150, sortable: true },
            { field: 'FYuanPan', title: '原判', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FXianPan', title: '现判', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'FStartDate', title: '起日', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getShortTime(value);
                    }
                }
            },
            {
                field: 'FEndDate', title: '止日', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getShortTime(value);
                    }
                }
            },
            { field: 'FXingQiAdd', title: '附加刑', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'FXF_Change', title: '刑罚变动', align: 'center', valign: 'middle', width: 400, sortable: true },
            { field: 'FJF_Info', title: '奖罚信息', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'FTotalScore', title: '总积分', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FMonth1', title: '一月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth2', title: '二月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth3', title: '三月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth4', title: '四月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth5', title: '五月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth6', title: '六月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth7', title: '七月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth8', title: '八月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth9', title: '九月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth10', title: '十月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth11', title: '11月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FMonth12', title: '12月', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FSalary', title: '上月工资', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FConsume', title: '超市消费', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FAmount', title: '账上余额', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FRewardScore', title: '奖分', visible: false, align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FRewardTime', title: '奖分时间', visible: false, align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FRewardText', title: '奖分事实', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'FDeductScore', title: '扣分', visible: false, align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FDeductTime', title: '扣分时间', visible: false, align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FDeductText', title: '扣分事实', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'BankCardNo', title: '银行账号', align: 'center', valign: 'middle', width: 150, sortable: true }
        ],
        url: "/OpenInfo/GetPrisonOpenInfosStr",
        uniqueId: "FCode",
        //toolbar:"#toolbar",
        search:true,
        striped: true,
        pagination: true,
        height: 396,
        pageSize: 5,                       //每页的记录行数（*）
        pageList: [5,10,25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        showExport: true,                     //是否显示导出
        exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            //alert(row.RoleName);
            roleRowEdit(row, false);//显示该行相应的数据
        }

    });


    //点击选中行，改变选中行的背景颜色
    $('#tb').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });

});
