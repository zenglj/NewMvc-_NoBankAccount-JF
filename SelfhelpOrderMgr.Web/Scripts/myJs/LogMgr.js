


$(function () {
    loadDetailTable(); //显示存款明细


    $("#winChangeList").window('close');
    
    //动态改变行颜色

    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.PayAuditFlag == 0) {

            } else if (row.PayAuditFlag == "1" && row.DAmount > 0) {
                return 'background-color:aqua;';
            } else if (row.PayAuditFlag == "2" && row.DAmount > 0) {
                return 'background-color:brown;';
            } else if (row.PayAuditFlag == "3" && row.DAmount > 0) {
                return 'background-color:green;';
            } 
        }
    });


    $('#tbPay').datagrid({
        rowStyler: function (index, row) {
            if (row.TranStatus == "2") {
                return 'background-color:gray;';
            }else if (row.TranStatus == "3") {
                return 'background-color:red;';
            } else if (row.TranStatus == "4") {
                return 'background-color:brown;';
            } else if (row.TranStatus == "5") {
                return 'background-color:coral;';
            } else if (row.AuditFlag == "1") {
                return 'background-color:green;';
            } 

        }
    });

}); 


function doSearch() {
    $('#tt').datagrid('load', {
        itemid: $('#itemid').val(),
        productid: $('#productid').val()
    });
}

//修改消费记录为调账取款
function btnInvListChange() {
    $("#winChangeList").window('open');
}


//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        //height: $(window).height()*1.0,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/Power/GetLogInfoList/',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200,2000,20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'CrtDate', title: '操作日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
                            if (value  != "/Date(-62135596800000)/") {
                                /*var dt = getLongTime(value);*/
                                var dt = value;
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
            { field: 'ControlName', title: '模块', sortable: true, width: 100 },
            { field: 'ActionName', title: '动作', sortable: true, width: 100 },            
            { field: 'ReqJson', title: '请求', width: 100, sortable: true },
            { field: 'RtnJson', title: '结果', width: 100, sortable: true },
            { field: 'UserCode', title: '用户', width: 100, sortable: true },
            { field: 'Remark', title: '备注', width: 100, sortable: true }
        ]],

        pagination: true,
        rownumbers: true
    });


}





function btnSearch() {

    funcSearch("formVcrdSearch", "test");
    //funcSearchByPost("formVcrdSearch", '/Power/GetLogInfoList/');
    
}





//执行查询加载dataGrid数据的方法
function funcSearch(formId,gridId) {
    var strJson = GetSearchJson(formId);
    $('#' + gridId).datagrid('load', {
        strJsonWhere: strJson
    });
}

//执行查询加载dataGrid数据的方法
function funcSearchByPost(formId,urlAddress) {
    var strJson = GetSearchJson(formId);
    $.post(urlAddress, { "strJsonWhere": strJson }, function (data, status) {
        if ("success" == status) {
            var d = $.parseJSON(data);
            $("#schSumMoney").html("结果金额:<u>" + d.sumMoney + " 元</u>");
        }
    });
}

//获取表单的查询条件
function GetSearchJson(formId) {
    var strJson = "";
    $("#" + formId + " table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            //console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    return strJson;
}



//清空条件
function btnClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formVcrdSearch").form('clear');
}







function GetGridSelectIds(gridId) {
    var selectIds = "";     
    var rows = $('#' + gridId).datagrid('getSelections');
    rows.forEach(function (element, index) {
        if (selectIds == "") {
            selectIds = element.Id;
        } else {
            selectIds = selectIds + "," + element.Id;
        }        
    });
    return selectIds;
}

