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


    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'seqno', title: '序号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'Remark', title: '狱情信息', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'VocName', title: '文件位置', align: 'center', valign: 'middle', width: 400, sortable: true }
        ],
        url: "/PrisonInfo/GetPrisonVocsStr",
        uniqueId: "Remark",
        toolbar:"#toolbar",
        search:true,
        striped: true,
        pagination: true,
        height: 296,
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
    $("#seqno").val("");
    $("#Remark").val("");
    $("#vocFile").val("");

    //默认取消所有权限功能
    //var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    //zTree.checkAllNodes(false);

    $("#Remark").removeAttr("disabled");
    $("#vocFile").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
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

function roleRowEdit(row,flag) {
    $("#seqno").val(row.seqno);
    $("#Remark").val(row.Remark);
    //通过文本方式设定选中项;
    //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);
    //通过数值方式设定选中项;

    if (flag == true) {
        $("#Remark").removeAttr("disabled");
        $("#btnSave").removeAttr("disabled");
    } else {
        $("#Remark").attr("disabled", "disabled");
        $("#btnSave").attr("disabled", "disabled");
    }    
}



function btnDelRole() {
    if ($("#Remark").val()=="") {
        alert("狱情文件不能为空");
        return false;
    }
    if (confirm("确定删除该狱情信息吗?")) {
        //点击确定后操作
        $.post("/PrisonInfo/DeletePrisonVoc", { "Remark": $("#Remark").val()}, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tb').bootstrapTable('removeByUniqueId', $("#Remark").val());
                }
                alert(data);
            }
        });
    }    
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
                        seqno: $("#seqno").val(),
                        Remark: info.Remark,
                        VocName: info.VocName
                    }
                ]);
            } else {
                var allTableData = $("#tb").bootstrapTable('getData');
                for (var i = 0; i < allTableData.length; i++) {
                    if (allTableData[i].Remark == $("#Remark").val()) {
                        $('#tb').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                seqno: info.seqno,
                                Remark: info.Remark,
                                VocName: info.VocName
                            }
                        });
                    }
                }
            }
            CloseEditMode();
            alert(words[1]);
        } else {
            alert(data);
        }
        
    }
    
}