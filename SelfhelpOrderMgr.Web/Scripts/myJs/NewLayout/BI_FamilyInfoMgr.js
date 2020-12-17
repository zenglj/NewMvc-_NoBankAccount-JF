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

var cyTypes;
var crimes;
var areas;
var areasStr;

$(document).ready(function () {

    $.post("/BaseInfo/GetCyTypeInfo", { "DT": new Date() }, function (data, status) {
        cyTypes = data;
        //装载角色的Select控件
        for (var i = 0; i < cyTypes.length; i++) {
            $("#FCYCode").append("<option value='" + cyTypes[i].FCode + "'>" + cyTypes[i].FName + "</option>");
            $("#schFCYCode").append("<option value='" + cyTypes[i].FCode + "'>" + cyTypes[i].FName + "</option>");
        }
    });
    $.post("/BaseInfo/GetAreaInfo", { "DT": new Date() }, function (data, status) {
        areas = data;
        //装载角色的Select控件
        for (var i = 0; i < areas.length; i++) {
            $("#FAreaCode").append("<option value='" + areas[i].FCode + "'>" + areas[i].FName + "</option>");
            $("#schFAreaCode").append("<option value='" + areas[i].FCode + "'>" + areas[i].FName + "</option>");
        }
    });

    $.post("/BaseInfo/GetCrimeInfo", { "DT": new Date() }, function (data, status) {
        //装载角色的Select控件
        crimes = data;
        for (var i = 0; i < crimes.length; i++) {
            $("#FCrimeCode").append("<option value='" + crimes[i].FCode + "'>" + crimes[i].FName + "</option>")
        }
    });
    $.post("/BaseInfo/GetRelationInfo", { "DT": new Date() }, function (data, status) {
        //装载角色的Select控件
        for (var i = 0; i < data.length; i++) {
            $("#FRelation").append("<option value='" + data[i].FCode + "'>" + data[i].FName + "</option>")
        }
    });

    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FSex', title: '性别', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FIdenNo', title: '身份证号', align: 'center', valign: 'middle', width: 200, sortable: true },
            
            {
                field: 'FAreaCode', title: '所在队别', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    for (var i = 0; i < areas.length; i++) {
                        if (value == areas[i].FCode) {
                            return areas[i].FName;
                        }
                    }
                }
            },
            {
                field: 'FCYCode', title: '处遇类型', align: 'center', valign: 'middle', width: 100, sortable: true, formatter: function (value, row, index) {
                    for (var i = 0; i < cyTypes.length; i++) {
                        if (value == cyTypes[i].FCode) {
                            return cyTypes[i].FName;
                        }
                    }
                }
            },
            
            { field: 'FTerm', title: '刑期', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'FInDate', title: '进监日期', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLocalTime(value);
                    }
                }
            },
            {
                field: 'FOuDate', title: '刑满日期', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value!=null) {
                        return getLocalTime(value);
                    }
                }
            },
            {
                field: 'fflag', title: '状态', align: 'center', valign: 'middle', width: 80, sortable: true, formatter: function (value, row, index) {
                    if (value == 1) {
                        return '离监';
                    } else {
                        return '在押';
                    }
                }
            },
            {
                field: 'FCrimeCode', title: '罪名', align: 'center', valign: 'middle', width: 250, sortable: true, formatter: function (value, row, index) {
                    for (var i = 0; i < crimes.length; i++) {
                        if (value == crimes[i].FCode) {
                            return crimes[i].FName;
                        }
                    }
                }
            },
            { field: 'FAddr', title: '地址', align: 'center', valign: 'middle', width: 400, sortable: true },
            
            { field: 'FDesc', title: '备注', align: 'center', valign: 'middle', width: 200, sortable: true }
            //,{ field: 'URL', title: 'URL地址', align: 'center', valign: 'middle', width: 200, sortable: true, visible: true }

        ],
        url: "/BaseInfo/GetCriminalInfoStr",
        uniqueId: "FCode",
        //toolbar:"#toolbar",
        //search:true,
        striped: true,
        pagination: true,
        height: 226,
        pageSize: 3,                       //每页的记录行数（*）
        pageList: [3,5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        //showExport: true,                     //是否显示导出
        //exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            //alert(row.RoleName);
            //roleRowEdit(row, false);//显示该行相应的数据
            selDisplayFamily(row);//显示家属列表
        }

    });



    var $table = $('#tbfamily').bootstrapTable({
        columns: [
            { field: 'FCode', title: 'ID', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FRelation', title: '亲属关系', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FIdenNo', title: '身份证号', align: 'center', valign: 'middle', width: 200, sortable: true },
            { field: 'CrtBy', title: '录入员', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'Crtdate', title: '录入日期', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLocalTime(value);
                    }
                }
            },
            { field: 'Modfy', title: '修改员', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'ModDate', title: '修改日期', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLocalTime(value);
                    }
                }
            },
            {
                field: 'fingerflag', title: '指纹', align: 'center', valign: 'middle', width: 80, sortable: true, formatter: function (value, row, index) {
                    if (value == 1) {
                        return '已采集';
                    } else {
                        return '未采集';
                    }
                }
            },
            { field: 'FMobile', title: '手机', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FTEL', title: '电话', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FAddr', title: '地址', align: 'center', valign: 'middle', width: 300, sortable: true },

            { field: 'FDesc', title: '备注', align: 'center', valign: 'middle', width: 200, sortable: true }
            //,{ field: 'URL', title: 'URL地址', align: 'center', valign: 'middle', width: 200, sortable: true, visible: true }

        ],
        //url: "/BaseInfo/GetCriminalInfoStr",
        uniqueId: "FCode",
        toolbar:"#toolbar",
        search: true,
        striped: true,
        pagination: true,
        height: 256,
        pageSize: 5,                       //每页的记录行数（*）
        pageList: [5, 10, 25, 50],        //可供选择的每页的行数（*）
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


    //点击选中行，改变选中行的背景颜色
    $('#tbfamily').on('click-row.bs.table', function (e, row, element) {
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
    $("#FCode").val("");
    $("#FName").val("");
    $("#FCrimeCode").val($("#mySelFCrimeCode").val());
    $("#FSex").val("");
    $("#FRelation").val(-1);
    $("#fingerflag").val(0);
    $("#FIdenNo").val("");
    $("#FMobile").val("");
    $("#FTEL").val("");
    $("#FAddr").val("");
    $("#Crtby").val("");
    $("#Crtdate").val("");
    $("#Modfy").val("");
    $("#ModDate").val("");
    $("#FDesc").val("");
    //默认取消所有权限功能
    //var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    //zTree.checkAllNodes(false);

    SetEditMode();
}


function SetEditMode() {

    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#FCrimeCode").removeAttr("disabled");
    $("#FSex").removeAttr("disabled");
    $("#FRelation").removeAttr("disabled");
    //$("#fingerflag").removeAttr("disabled");
    $("#FIdenNo").removeAttr("disabled");
    $("#FMobile").removeAttr("disabled");
    $("#FTEL").removeAttr("disabled");
    $("#FAddr").removeAttr("disabled");
    $("#Crtby").removeAttr("disabled");
    $("#Crtdate").removeAttr("disabled");
    $("#Modfy").removeAttr("disabled");
    $("#ModDate").removeAttr("disabled");
    $("#FDesc").removeAttr("disabled");

    $("#btnSave").removeAttr("disabled");
}

function CloseEditMode() {

    $("#FCode").attr("disabled", "disabled");
    $("#FName").attr("disabled", "disabled");
    $("#FCrimeCode").attr("disabled", "disabled");
    $("#FSex").attr("disabled", "disabled");
    $("#FRelation").attr("disabled", "disabled");
    $("#fingerflag").attr("disabled", "disabled");
    $("#FIdenNo").attr("disabled", "disabled");
    $("#FMobile").attr("disabled", "disabled");
    $("#FTEL").attr("disabled", "disabled");
    $("#FAddr").attr("disabled", "disabled");
    $("#Crtby").attr("disabled", "disabled");
    $("#Crtdate").attr("disabled", "disabled");
    $("#Modfy").attr("disabled", "disabled");
    $("#ModDate").attr("disabled", "disabled");
    $("#FDesc").attr("disabled", "disabled");

    $("#btnSave").attr("disabled", "disabled");
}

function btnEditInfo() {
    if ($("#FCode").val() == "") {
        alert("请选择一条记录，谢谢");
        return false;
    }
    SetEditMode();
    $("#myModal").modal("show");
}

function btnSaveRole() {
    if ($("#FCrimeCode").val() == "") {
        alert("对不起，犯人编号不能为空");
        return false;
    }
    if ($("#FName").val() == "") {
        alert("对不起，名称不能为空");
        return false;
    }
    if ($("#FSex").val() == "") {
        alert("对不起，性别不能为空");
        return false;
    }
    var frelation = "";
    if ($("#FRelation").val() != "-1") {
        frelation = $("#FRelation").find("option:selected").text();
    }
    $.post("/BaseInfo/SaveFamilyInfo", {
        "FCode": $("#FCode").val()
        , "FName": $("#FName").val()
        , "FSex": $("#FSex").find("option:selected").text()
        , "FCrimeCode":$("#FCrimeCode").val()
        , "FRelation": $("#FRelation").find("option:selected").text()
        , "fingerflag": $("#fingerflag").val()
        , "FIdenNo": $("#FIdenNo").val()
        , "FMobile": $("#FMobile").val()
        , "FTEL": $("#FTEL").val()
        , "FAddr": $("#FAddr").val()
       //, "Crtby": $("#Crtby").val()
       // , "Crtdate": $("#Crtdate").val()
       // , "Modfy": $("#Modfy").val()
       // , "ModDate": $("#ModDate").val()
        , "FDesc": $("#FDesc").val()

        //, "UserMgrAreas": selectRoleMenuIds
    }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                model = $.parseJSON(words[3]);

                if (words[2] == "Add") {
                    $('#tbfamily').bootstrapTable('append', [
                        {
                            FCode: model.FCode
                            , FName: $("#FName").val()
                            , FSex: $("#FSex").find("option:selected").text()
                            , FCrimeCode: $("#FCrimeCode").val()
                            , FRelation: $("#FRelation").find("option:selected").text()
                            , fingerflag: $("#fingerflag").find("option:selected").text()
                            , FIdenNo: $("#FIdenNo").val()
                            , FMobile: $("#FMobile").val()
                            , FTEL: $("#FTEL").val()
                            , FAddr: $("#FAddr").val()
                            , FDesc: $("#FDesc").val()
                            , Crtby: model.Crtby
                            , Crtdate: model.Crtdate
                            , Modfy: model.Modfy
                            , ModDate: model.ModDate
                        }
                    ]);
                } else {
                    var allTableData = $("#tbfamily").bootstrapTable('getData');
                    for (var i = 0; i < allTableData.length; i++) {
                        if (allTableData[i].FCode == $("#FCode").val()) {
                            $('#tbfamily').bootstrapTable('updateRow', {
                                index: i,
                                row: {
                                    FCode: $("#FCode").val()
                                    , FName: $("#FName").val()
                                    , FSex: $("#FSex").find("option:selected").text()
                                    , FCrimeCode: $("#FCrimeCode").val()
                                    , FRelation: $("#FRelation").find("option:selected").text()
                                    , fingerflag: $("#fingerflag").find("option:selected").text()
                                    , FIdenNo: $("#FIdenNo").val()
                                    , FMobile: $("#FMobile").val()
                                    , FTEL: $("#FTEL").val()
                                    , FAddr: $("#FAddr").val()
                                    , FDesc: $("#FDesc").val()
                                    , Crtby: model.Crtby
                                    , Crtdate: model.Crtdate
                                    , Modfy: model.Modfy
                                    , ModDate: model.ModDate
                                }
                            });
                        }
                    }

                }
                CloseEditMode();
                $("#myModal").modal('hide');
            }            
            alert(words[1]);
            
        }
    });
}

function roleRowEdit(row,flag) {
    $("#FCode").val(row.FCode);
    $("#FName").val(row.FName);
    $("#FCrimeCode").val(row.FCrimeCode);
    //$("#FSex").val(row.FSex);
    $("#FSex").find("option:contains('" + row.FSex + "')").attr("selected", true);
    $("#FRelation").find("option:contains('" + row.FRelation + "')").attr("selected", true);
    //通过文本方式设定选中项;
    //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);

    $("#fingerflag").val(row.fingerflag);

    //通过数值方式设定选中项;
    $("#FIdenNo").val(row.FIdenNo);
    $("#FMobile").val(row.FMobile);
    $("#FTEL").val(row.FTEL);
    $("#FAddr").val(row.FAddr);
    $("#Crtby").val(row.Crtby);
    $("#Crtdate").val(getShortTime(row.Crtdate));
    $("#Modfy").val(row.Modfy);
    $("#ModDate").val(getShortTime(row.ModDate));
    $("#FDesc").val(row.FDesc);

    if (flag == true) {
        SetEditMode();
    } else {
        CloseEditMode();
    }
    
}



function btnDelRole() {

    if ($("#FCode").val()=="") {
        alert("要删除的ID不能为空");
        return false;
    }
    if ($("#FName").val() == "") {
        alert("要删除的家属名称不能为空");
        return false;
    }
    if (confirm("确定删除该家属吗?")) {
        //点击确定后操作
        $.post("/BaseInfo/DeleteFamilyInfo", { "FCode": $("#FCode").val(), "FName": $("#FName").val(), "FCrimeCode": $("#mySelFCrimeCode").val() }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tbfamily').bootstrapTable('removeByUniqueId', $("#FCode").val());
                }
                alert(data);
            }
        });
    }
    
}


function btnSearch() {
    $.post("/BaseInfo/GetCriminalInfo", {
        "schFName": $("#schFName").val()
        , "schFCode": $("#schFCode").val()
        , "schFAreaCode": $("#schFAreaCode").val()
        , "schFFlag": $("#schFFlag").val()
        , "schFCYCode": $("#schFCYCode").val()
    }, function (data, status) {
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

//选择并显示家属信息
function selDisplayFamily(row) {
    $("#mySelFCrimeCode").val(row.FCode);
    $("#FCode").val("");
    $.post("/BaseInfo/GetFamilyByFCrimeCode", {
        "FCrimeCode": row.FCode
    }, function (data, status) {
        if ("success" == status) {
            if (data.length != 0) {
                $('#tbfamily').bootstrapTable('load', data);
            } else {
                $('#tbfamily').bootstrapTable('removeAll');
                alert("对不起，没有相应条件的数据");
            }
        }
    });
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
            //var infos = $.parseJSON(words[1]);
            //$('#tb').bootstrapTable('load', infos);
            $("#myModalExcel").modal('hide');
            alert(words[2]);
        } else {
            alert(data);
        }

    }

}