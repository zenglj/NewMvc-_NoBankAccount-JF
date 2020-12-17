var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    check: {
        enable: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    edit: {
        enable: false
    }
};



//$(document).ready(function () {
//    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
//});

var roles;
var areas;
var areasStr;

$(document).ready(function () {

    
    $.post("/BaseInfo/GetCyTypeInfo", { "dt": new Date() }, function (data, status) {
        if ("success" == status) {
            for (var i = 0; i < data.length; i++) {
                $("#FCyType").append("<option value='" + data[i].ID + "'>" + data[i].FName + "</option>");
            }
        }
    });
    $.post("/BaseInfo/GetAreaInfo", { "dt": new Date() }, function (data, status) {
        if ("success" == status) {
            for (var i = 0; i < data.length; i++) {
                $("#FAreaName").append("<option value='" + data[i].FName + "'>" + data[i].FName + "</option>");
                $("#schFAreaCode").append("<option value='" + data[i].FCode + "'>" + data[i].FName + "</option>");
            }
        }
    });


    //日期控件
    $('.some_class').datetimepicker();
});

function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
        + "' title='add node' onfocus='this.blur();'></span>";
    sObj.after(addStr);
    var btn = $("#addBtn_" + treeNode.tId);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        zTree.addNodes(treeNode, { id: (100 + newCount), pId: treeNode.id, name: "new node" + (newCount++) });
        return false;
    });
};
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.tId).unbind().remove();
};


function btnAddRole() {
    $("#keyId").val("");
    $("#FCode").val("");
    $("#FName").val("");
    $("#FCyType").val("");
    $("#FAreaName").val("");
    //通过文本方式设定选中项;
    //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);
    //通过数值方式设定选中项;

    $("#FTotalScore").val("");
    $("#FMonth1").val("");
    $("#FMonth2").val("");
    $("#FMonth3").val("");
    $("#FMonth4").val("");
    $("#FMonth5").val("");
    $("#FMonth6").val("");
    $("#FMonth7").val("");
    $("#FMonth8").val("");
    $("#FMonth9").val("");
    $("#FMonth10").val("");
    $("#FMonth11").val("");
    $("#FMonth12").val("");

    $("#FSalary").val("");
    $("#FConsume").val("");
    $("#FAmount").val("");

    $("#FYuanPan").val("");
    $("#FStartDate").val("");
    $("#FEndDate").val("");
    $("#FXF_Change").val("");

    $("#FJF_Info").val("");
    $("#FRewardText").val("");
    $("#FRewardScore").val("");
    $("#FRewardTime").val("");
    $("#FDeductText").val("");
    $("#FRewardScore").val("");
    $("#FRewardTime").val("");
    $("#BankCardNo").val("");
}


function SetEditMode() {
    $("#Remark").removeAttr("disabled");
    $("#vocFile").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}

function CloseEditMode() {
    $("#Remark").attr("disabled", "disabled");
    $("#vocFile").attr("disabled", "disabled");
    $("#btnSave").attr("disabled", "disabled");
}


function btnSaveRole() {
    if ($("#Remark").val() == "") {
        alert("对不起，狱情信息为空");
        return false;
    }
    if ($("#vocFile").val() == "") {
        alert("对不起，狱情文件不能为空");
        return false;
    }

    $("form").submit(function (e) {
        
    });
}

function roleRowEdit(row, flag) {
    $("#keyId").val(row.keyId);
    $("#FCode").val(row.FCode);
    $("#FName").val(row.FName);
    $("#FCyType").find("option:contains('" + row.FCyType + "')").attr("selected", true);
    $("#FAreaName").find("option:contains('" + row.FAreaName + "')").attr("selected", true);
    //通过文本方式设定选中项;
    //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);
    //通过数值方式设定选中项;
    
    $("#FTotalScore").val(row.FTotalScore);
    $("#FMonth1").val(row.FMonth1);
    $("#FMonth2").val(row.FMonth2);
    $("#FMonth3").val(row.FMonth3);
    $("#FMonth4").val(row.FMonth4);
    $("#FMonth5").val(row.FMonth5);
    $("#FMonth6").val(row.FMonth6);
    $("#FMonth7").val(row.FMonth7);
    $("#FMonth8").val(row.FMonth8);
    $("#FMonth9").val(row.FMonth9);
    $("#FMonth10").val(row.FMonth10);
    $("#FMonth11").val(row.FMonth11);
    $("#FMonth12").val(row.FMonth12);

    $("#FSalary").val(row.FSalary);
    $("#FConsume").val(row.FConsume);
    $("#FAmount").val(row.FAmount);

    $("#FYuanPan").val(row.FYuanPan);
    $("#FStartDate").val(getShortTime( row.FStartDate));
    $("#FEndDate").val(getShortTime(row.FEndDate));
    $("#FXF_Change").val(row.FXF_Change);
    
    $("#FJF_Info").val(row.FJF_Info);
    $("#FRewardText").val(row.FRewardText);
    $("#FRewardScore").val(row.FRewardScore);
    $("#FRewardTime").val(row.FRewardText);
    $("#FDeductText").val(row.FDeductText);
    $("#FRewardScore").val(row.FRewardScore);
    $("#FRewardTime").val(row.FRewardText);
    $("#BankCardNo").val(row.BankCardNo);


    //if (flag == true) {
    //    $("#Remark").removeAttr("disabled");
    //    $("#btnSave").removeAttr("disabled");
    //} else {
    //    $("#Remark").attr("disabled", "disabled");
    //    $("#btnSave").attr("disabled", "disabled");
    //}    
}

function btnDelRole() {
    if ($("#FCode").val()=="") {
        alert("犯人编号不能为空");
        return false;
    }
    if (confirm("确定删除该犯的狱务公开信息吗?")) {
        //点击确定后操作
        $.post("/OpenInfo/DeletePrisonOpenInfo", { "FCode": $("#FCode").val() }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tb').bootstrapTable('removeByUniqueId', $("#FCode").val());
                }
                alert(data);
            }
        });
    }    
}

//按条件查询数据
function btnSearch() {
    //var fareaName = "";
    //if ($("#schFAreaCode").val() != "") {
    //    fareaName = $("#schFAreaCode").find("option:selected").text();
    //}
    $.post("/OpenInfo/GetPrisonOpenInfos",
        {
            "FCode": $("#schFCode").val(),
            "FName": $("#schFName").val(),
            "FAreaName": $("#schFAreaCode").val()
        },
        function (data, status) {
        if ("success" == status) {
            if (data.length != 0) {
                $('#tb').bootstrapTable('load', data);
            } else {
                $('#tb').bootstrapTable('removeAll');
                alert("对不起，没有相应条件的数据");
            }
        }
    });
}

$(document).ready(function () {
    var options = {
        target: '#output1',   // 用服务器返回的数据 更新 id为output1的内容.
        beforeSubmit: showRequest,  // 提交前
        success: showResponse,  // 提交后 
        //另外的一些属性: 
        //url:       url         // 默认是form的action，如果写的话，会覆盖from的action. 
        //type:      type        // 默认是form的method，如果写的话，会覆盖from的method.('get' or 'post').
        //dataType:  null        // 'xml', 'script', or 'json' (接受服务端返回的类型.) 
        //clearForm: true        // 成功提交后，清除所有的表单元素的值.
        resetForm: true        // 成功提交后，重置所有的表单元素的值.
        //由于某种原因,提交陷入无限等待之中,timeout参数就是用来限制请求的时间,
        //当请求大于3秒后，跳出请求. 
        //timeout:   3000 
    };

    //'ajaxForm' 方式的表单 .
    $('#vocForm').ajaxForm(options);
    //或者 'ajaxSubmit' 方式的提交.
    //$('#myForm').submit(function() { 
    //    $(this).ajaxSubmit(options); 
    //    return false; //来阻止浏览器提交.
    //}); 
});

// 提交前
function showRequest(formData, jqForm, options) {
    // formdata是数组对象,在这里，我们使用$.param()方法把他转化为字符串.
    var queryString = $.param(formData); //组装数据，插件会自动提交数据
    //alert(queryString); //类似 ： name=1&add=2  
    return true;
}

//  提交后
function showResponse(responseText, statusText) {
    //alert('状态: ' + statusText + '\n 返回的内容是: \n' + responseText);
    formDataCheck(responseText, statusText);
}

//表单提交后数据处理
function formDataCheck(data,status) {
    if ("success" == status) {
        var words = data.split("|");
        if ("OK" == words[0]) {
            info = $.parseJSON(words[3]);
            if (words[2] == "Add") {                
                $('#tb').bootstrapTable('append', [
                    {
                        keyId:info.keyId,
                        FCode: info.FCode,
                        FName: info.FName,
                        FCyType: info.FCyType,
                        FAreaName: info.FAreaName,
                        FYuanPan: info.FYuanPan,
                        FXianPan: info.FXianPan,
                        FStartDate: info.FStartDate,
                        FEndDate: info.FEndDate,
                        FXingQiAdd: info.FXingQiAdd,
                        FXF_Change: info.FXF_Change,
                        FJF_Info: info.FJF_Info,
                        FTotalScore: info.FTotalScore,
                        FMonth1: info.FMonth1,
                        FMonth2: info.FMonth2,
                        FMonth3: info.FMonth3,
                        FMonth4: info.FMonth4,
                        FMonth5: info.FMonth5,
                        FMonth6: info.FMonth6,
                        FMonth7: info.FMonth7,
                        FMonth8: info.FMonth8,
                        FMonth9: info.FMonth9,
                        FMonth10: info.FMonth10,
                        FMonth11: info.FMonth11,
                        FMonth12: info.FMonth12,
                        FSalary: info.FSalary,
                        FConsume: info.FConsume,
                        FAmount: info.FAmount,
                        FRewardScore: info.FRewardScore,
                        FRewardTime: info.FRewardTime,
                        FRewardText: info.FRewardText,
                        FDeductScore: info.FDeductScore,
                        FDeductTime: info.FDeductTime,
                        FDeductText: info.FDeductText,
                        CreateDate:  info.CreateDate,
                        BankCardNo: info.BankCardNo
                    }
                ]);
            } else {
                var allTableData = $("#tb").bootstrapTable('getData');
                for (var i = 0; i < allTableData.length; i++) {
                    if (allTableData[i].FCode == info.FCode) {
                        $('#tb').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                keyId: info.keyId,
                                FCode: info.FCode,
                                FName: info.FName,
                                FCyType: info.FCyType,
                                FAreaName: info.FAreaName,
                                FYuanPan: info.FYuanPan,
                                FXianPan: info.FXianPan,
                                FStartDate: info.FStartDate,
                                FEndDate: info.FEndDate,
                                FXingQiAdd: info.FXingQiAdd,
                                FXF_Change: info.FXF_Change,
                                FJF_Info: info.FJF_Info,
                                FTotalScore: info.FTotalScore,
                                FMonth1: info.FMonth1,
                                FMonth2: info.FMonth2,
                                FMonth3: info.FMonth3,
                                FMonth4: info.FMonth4,
                                FMonth5: info.FMonth5,
                                FMonth6: info.FMonth6,
                                FMonth7: info.FMonth7,
                                FMonth8: info.FMonth8,
                                FMonth9: info.FMonth9,
                                FMonth10: info.FMonth10,
                                FMonth11: info.FMonth11,
                                FMonth12: info.FMonth12,
                                FSalary: info.FSalary,
                                FConsume: info.FConsume,
                                FAmount: info.FAmount,
                                FRewardScore: info.FRewardScore,
                                FRewardTime: info.FRewardTime,
                                FRewardText: info.FRewardText,
                                FDeductScore: info.FDeductScore,
                                FDeductTime: info.FDeductTime,
                                FDeductText: info.FDeductText,
                                CreateDate: info.CreateDate,
                                BankCardNo: info.BankCardNo
                            }
                        });
                    }
                }
            }
            //CloseEditMode();
            $("#myModal").modal('hide');
            alert(words[1]);
        } else {
            alert(data);
        }
        
    }
    
}


//===========================================================
//导入Excel文件
//===========================================================
$(document).ready(function () {
    var options = {
        target: '#output1',   // 用服务器返回的数据 更新 id为output1的内容.
        beforeSubmit: showRequest,  // 提交前
        success: showExcelResponse,  // 提交后 
        //另外的一些属性: 
        //url:       url         // 默认是form的action，如果写的话，会覆盖from的action. 
        //type:      type        // 默认是form的method，如果写的话，会覆盖from的method.('get' or 'post').
        //dataType:  null        // 'xml', 'script', or 'json' (接受服务端返回的类型.) 
        //clearForm: true        // 成功提交后，清除所有的表单元素的值.
        resetForm: true        // 成功提交后，重置所有的表单元素的值.
        //由于某种原因,提交陷入无限等待之中,timeout参数就是用来限制请求的时间,
        //当请求大于3秒后，跳出请求. 
        //timeout:   3000 
    };

    //'ajaxForm' 方式的表单 .
    $('#ExcelForm').ajaxForm(options);
    //或者 'ajaxSubmit' 方式的提交.
    //$('#myForm').submit(function() { 
    //    $(this).ajaxSubmit(options); 
    //    return false; //来阻止浏览器提交.
    //}); 
});

// 提交前
function showRequest(formData, jqForm, options) {
    // formdata是数组对象,在这里，我们使用$.param()方法把他转化为字符串.
    var queryString = $.param(formData); //组装数据，插件会自动提交数据
    //alert(queryString); //类似 ： name=1&add=2  
    return true;
}

//  提交后
function showExcelResponse(responseText, statusText) {
    //alert('状态: ' + statusText + '\n 返回的内容是: \n' + responseText);
    formExcelDataCheck(responseText, statusText);
}

//表单提交后数据处理
function formExcelDataCheck(data, status) {
    if ("success" == status) {
        var words = data.split("|");
        if ("OK" == words[0]) {
            var infos = $.parseJSON(words[1]);
            $('#tb').bootstrapTable('load', infos);
            $("#myModalExcel").modal('hide');
            alert(words[2]);
        } else {
            alert(data);
        }

    }

}