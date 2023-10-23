
//执行查询加载dataGrid数据的方法
function funcSearch(formId, gridId) {
    var strJson = GetSearchJson(formId);
    $('#' + gridId).datagrid('load', {
        strJsonWhere: strJson
    });
}

//执行查询加载dataGrid数据的方法
function funcSearchByPost(formId, urlAddress, schSumMoneyId) {
    var strJson = GetSearchJson(formId);
    $.post(urlAddress, { "strJsonWhere": strJson }, function (data, status) {
        if ("success" == status) {
            var d = $.parseJSON(data);
            //$("#schSumMoney").html("结果金额:<u>" + d.sumMoney + " 元</u>");
            $("#" + schSumMoneyId).html("结果金额:<u>" + d.sumMoney + " 元</u>");
        }
    });
}



//获取表单的查询条件
function GetSearchJson(formId) {
    var strJson = "";
    $("#" + formId + " table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        console.log(this);
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


//清除表单的所有数据项
function ClearFormSearch(formSearchId) {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    //$("#formPaySearch").form('clear');
    $("#" + formSearchId).form('clear');

}


//获取所有的选择Id号
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
