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

    var $table = $('#tb').bootstrapTable({
        columns: [
            { field: 'ID', title: 'ID', align: 'center', valign: 'middle', width: 70, sortable: true },
            { field: 'FTitle', title: '标题', align: 'center', valign: 'middle', width: 250, sortable: true },
            { field: 'FAbstract', title: '摘要', align: 'center', valign: 'middle', width: 500, sortable: true },
            { field: 'FAuthor', title: '作者', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'FDate', title: '发布日期', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLongTime(value);
                    }
                }
            },
            { field: 'LinkWebFile', title: '文件位置', align: 'center', valign: 'middle', width: 400, sortable: true },
            { field: 'Remark', title: '备注', align: 'center', valign: 'middle', width: 400, sortable: true }
        ],
        url: "/OpenInfo/GetNotifyFileStr",
        uniqueId: "ID",
        toolbar:"#toolbar",
        search: true,
        striped: true,
        pagination: true,
        height: 396,
        pageSize: 5,                       //每页的记录行数（*）
        pageList: [5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
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
    $("#ID").val("");
    $("#FTitle").val("");
    $("#FAbstract").val("");
    $("#FAuthor").val("");
    $("#Remark").val("");
    $("#LinkWebFile").val("");
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
    $("#ID").val(row.ID);
    $("#FTitle").val(row.FTitle);
    $("#FAbstract").val(row.FAbstract);
    $("#FAuthor").val(row.FAuthor);
    $("#Remark").val(row.Remark);
   
}

function btnDelRole() {
    if ($("#ID").val()=="") {
        alert("ID号不能为空");
        return false;
    }
    if (confirm("确定删除该通知信息吗?")) {
        //点击确定后操作
        $.post("/OpenInfo/DeleteNotify", { "ID": $("#ID").val() }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tb').bootstrapTable('removeByUniqueId', $("#ID").val());
                }
                alert(data);
            }
        });
    }    
}



//===========================================================
//导入通知文件
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
            info = $.parseJSON(words[3]);
            if (words[2] == "Add") {
                $('#tb').bootstrapTable('append', [
                    {
                        ID: info.ID,
                        FTitle: info.FTitle,
                        FAbstract: info.FAbstract,
                        FAuthor: info.FAuthor,
                        FDate: info.FDate,
                        LinkWebFile: info.LinkWebFile,
                        Remark: info.Remark
                    }
                ]);
            } else {
                var allTableData = $("#tb").bootstrapTable('getData');
                for (var i = 0; i < allTableData.length; i++) {
                    if (allTableData[i].ID == info.ID) {
                        $('#tb').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                ID: info.ID,
                                FTitle: info.FTitle,
                                FAbstract: info.FAbstract,
                                FAuthor: info.FAuthor,
                                FDate: info.FDate,
                                LinkWebFile: info.LinkWebFile,
                                Remark: info.Remark
                            }
                        });
                    }
                }
            }
            $("#myModalExcel").modal('hide');
        } else {
            alert(data);
        }

    }

}