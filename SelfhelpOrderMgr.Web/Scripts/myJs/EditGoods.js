﻿
$(function () {

    //$('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");

    //$('#FGtype').combobox({
    //    url: '../UI/Commons.ashx?action=GetGoodsType',
    //    valueField: 'Fcode',
    //    textField: 'Fname'
    //});

    //$('#FSupplyer').combobox({
    //    url: '/Super/GetSupplyer',
    //    valueField: 'sCode',
    //    textField: 'sName'
    //});


    $('#FGtype').combobox({
        url: '/Super/GetGoodsType',
        valueField: 'Fcode',
        textField: 'Fname'
    });

    $('#Ffreeflag').combobox({
        data: [{
            Fname: '是',
            Fcode: '0'
        },{
            Fname: '否',
            Fcode: '1'
        }],
        valueField: 'Fcode',
        textField: 'Fname'
    });

    
    //$('#FGoodsStatus').combobox({
    //    url: '../UI/Commons.ashx?action=GetCommonTypeTab&FType=GoodsZT',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});

    //$('#FGoodsIsXianE').combobox({
    //    url: '../UI/Commons.ashx?action=GetCommonTypeTab&FType=XianE',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});



    //用户列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.9,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Super/GetGoods/' + $("#typeId").val(),
        sortName: 'GCODE',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'GCODE',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        columns: [[
					{ field: 'ck', checkbox: true },
	                { field: 'GCODE', title: '编号',  width: 50, sortable: true },
	                { field: 'GNAME', title: '名称', width: 80, sortable: true },
                    { field: 'GTXM', title: '条形码', width: 100 },
                    { field: 'SPShortCode', title: '店内码', width: 80, sortable: true },
					{ field: 'GTYPE', title: '类别', width: 80 , sortable: true},
                    { field: 'GUnit', title: '单位', width: 60 },
					{ field: 'GStandard', title: '规格', width: 80 },
					{ field: 'GDJ', title: '单价', width: 80 },
					{ field: 'GSupplyer', title: '供应商', width: 120 },					
					{ field: 'CrtBy', title: '创建人', width: 80 },
					{ field: 'ModBy', title: '修改人', width: 80 },
					{ field: 'Moddt', title: '修改时间', width: 100 },
                    {
                        field: 'ACTIVE', width: 50, title: '状态',
                        formatter: function (value, row, index) {
                            if (value == "Y") {
                                return "正常";
                            } else {
                                return "下架";
                            }
                        }
                    },
					{ field: 'gindj', title: '进价', width: 80 },
					{ field: 'madein', title: '产地', width: 100 },
                    {
                        field: 'Ffreeflag', width: 50, title: '限额',
                        formatter: function (value, row, index) {
                            if (value == "1") {
                                return "否";
                            } else {
                                return "是";
                            }
                        }
                    },
                    { field: 'Xgsl', title: '限购量', width: 80 }
                    ,
                    { field: 'XgMode', title: '限购模式',hidden:true, width: 80 }

                    
                    					
        ]],
        //toolbar: [{
        //    text: '新增',
        //    iconCls: 'icon-add',
        //    handler: function () {
        //        AddGoodsContent();
        //    }
        //}, '-', {
        //    text: '编辑',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        AlterGoodsContent();
        //    }
        //}, '-', {
        //    text: '下架',
        //    iconCls: 'icon-cancel',
        //    handler: function () {
        //        SetGoodsContent("N");
        //    }
        //}, '-', {
        //    text: '上架',
        //    iconCls: 'icon-redo',
        //    handler: function () {
        //        SetGoodsContent("Y");
        //    }
        //}, '-', {
        //    text: 'Excel导入',
        //    iconCls: 'icon-reload',
        //    handler: function () {
        //        SetGoodsContent("Y");
        //    }
        //}, '-', {
        //    text: '批量图片上传',
        //    iconCls: 'icon-sum',
        //    handler: function () {
        //        window.open("/Super/MulitImageUpload");

        //    }
        //}],
        onSelect: function (rowIndex, rowData) {
            $("#GCODE").val(rowData.GCODE);
            $("#GNAME").val(rowData.GNAME);            
            
            $("#FGtype").combobox('setValue', rowData.GTYPE);		        
            $("#FUnit").val(rowData.GUnit);//  
            $("#FStandard").val(rowData.GStandard);//  
            $('#FPrice').numberbox('setValue', rowData.GDJ);
            $("#FSupplyer").combobox('setValue', rowData.GSupplyer); 
            $('#FGTXM').numberbox('setValue', rowData.GTXM);
            $("#FMadein").val(rowData.madein);// 
            //$("#FGtype").combobox('select', rowData.GTcode);
            //$("#FSupplyer").combobox('select', rowData.GScode);
            if (rowData.Ffreeflag == "1") {
                $("#Ffreeflag").combobox('setValue', rowData.Ffreeflag);
            } else {
                $("#Ffreeflag").combobox('setValue', rowData.Ffreeflag);
            }
            $("#Xgsl").val(rowData.Xgsl);
            $("#XgMode").combobox('setValue', rowData.XgMode);
            $("#picSrc").attr("src", rowData.src);
            $("#SPShortCode").val(rowData.SPShortCode);

            //商品属性框
            $("#attrGCode").val(rowData.GCODE);
            $("#attrGName").val(rowData.GNAME);
            $("#attrGTXM").val(rowData.GTXM);
            $("#attrSPShortCode").val(rowData.SPShortCode);
            $('#attrTable').datagrid('load', {
                GCode: rowData.GCODE
            });
            $("#GoodAttr").val('');
            $('#btnGoodAttrAdd').linkbutton('enable');
            $('#btnGoodAttrSave').linkbutton('disable');
            $('#btnGoodAttrDel').linkbutton('disable');
            
        }
    });

    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.ACTIVE == "N") {
                return 'background-color:brown;';
            }
        }
    });


    $('#editWindows').window({
        modal: true,
        closed: true
    });

    //加载商品规格型号列表
    LoadGoodAttrsTable();
    $('#editGoodAttr').window({
        modal: true,
        closed: true
    });




});


//商品属性类型
var consumeType = [{ "value": "1", "text": "口味" }, { "value": "2", "text": "尺码" }, { "value": "3", "text": "规格" }];

function consumeformatter(value, rowData, rowIndex) {
    //if (value == 0) {
    //    return;
    //}
    for (var i = 0; i < consumeType.length; i++) {
        if (consumeType[i].value == value) {
            return consumeType[i].text;
        }
    }
}

//加载商品规格型号列表
function LoadGoodAttrsTable() {
    //用户列表清单
    $('#attrTable').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        width: 750,
        height: 350,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Super/GetGoodAttrs',
        sortName: 'ID',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'ID',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        columns: [[
					{ field: 'ck', checkbox: true },
					{ field: 'ID', width: 50, sortable: true, title: 'ID号' },
                    { field: 'GCode', width: 60, sortable: true, title: '编码' },
                    {
                        field: 'GoodsAttrId', width: 60, sortable: true, title: '类型', formatter: consumeformatter, editor: { type: 'combobox', options: { data: consumeType, valueField: "value", textField: "text" } }
                    },
                    { field: 'AttrInfo', width: 150, title: '规格型号信息',editor:'text' }

        ]],
        onSelect: function (rowIndex, rowData) {
            $("#goodsAttrType").combobox('setValue', rowData.GoodsAttrId);
            $("#GoodAttr").val(rowData.AttrInfo);
            $("#GoodAttrId").val(rowData.ID);
            $("#attrDoType").val("save");
            $('#btnGoodAttrAdd').linkbutton('disable');
            $('#btnGoodAttrSave').linkbutton('enable');
            $('#btnGoodAttrDel').linkbutton('enable');
        }
    });
}

//编辑商品属性
function EditGoodAttr() {
    $('#editGoodAttr').window('open');
}

//增加商品属性
function AddGoodsAtrr() {
    $("#goodsAttrType").combobox('setValue','');
    $("#GoodAttr").val('');
    $("#GoodAttrId").val('');
    $("#attrDoType").val('add')
    $('#btnGoodAttrAdd').linkbutton('disable');
    $('#btnGoodAttrSave').linkbutton('enable');
    $('#btnGoodAttrDel').linkbutton('enable');
}

//保存商品属性
function SaveGoodsAtrr() {
    if ($("#goodsAttrType").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择一个类型");
        return false;
    }
    if($("#GoodAttr").val()==""){
        $.messager.alert("提示", "商品规格不能为空");
        return false;
    }
    $.post("/Super/AddGoodAttrs", {
        "GCode": $("#attrGCode").val()
        , "GoodsAttrId": $("#goodsAttrType").combobox('getValue')
        , "AttrInfo": $("#GoodAttr").val()
        , "ID": $("#GoodAttrId").val()
        , "attrDoType": $("#attrDoType").val()
    }, function (data, status) {
        if ("success" != status) {
            return false;
        }
        else {
            var words = data.split("|");
            if (words[0] == "OK") {
                var info = $.parseJSON(words[1]);
                if ($("#attrDoType").val()=="add") {
                    $('#attrTable').datagrid('appendRow', {
                        ID: info.ID,
                        GCode: info.GCode,
                        GoodsAttrId: info.GoodsAttrId,
                        AttrInfo: info.AttrInfo
                    });
                } else {
                    var row = $('#attrTable').datagrid('getSelected');
                    var idx = $('#attrTable').datagrid('getRowIndex',row);
                    $('#attrTable').datagrid('updateRow', {
                        index: idx,
                        row: {
                            ID: info.ID,
                            GCode: info.GCode,
                            GoodsAttrId: info.GoodsAttrId,
                            AttrInfo: info.AttrInfo
                        }
                    });
                }
                $("#attrDoType").val('');
                $("#GoodAttr").val('');
                $('#btnGoodAttrAdd').linkbutton('enable');
                $('#btnGoodAttrSave').linkbutton('disable');
                $('#btnGoodAttrDel').linkbutton('disable');
                $.messager.alert("提示", "保存成功");
            } else {
                $.messager.alert("提示", data);
            }
        }
    });
}

//删除商品属性
function DelGoodsAtrr() {
    var row = $('#attrTable').datagrid('getSelected');
    var idx = $('#attrTable').datagrid('getRowIndex', row);
    $.post("/Super/DelGoodAttrs", {"ID":row.ID}, function (data, status) {
        if ("success" != status) {
            return false;
        }
        else {
            var words = data.split("|");
            if(words[0]=="OK"){
                $('#attrTable').datagrid('deleteRow', idx);
                $.messager.alert("提示", "删除成功");
            } else {
                $.messager.alert("提示", data);
            }
        }
    });
    

}

//保存修改内容
function saveContent() {
    var fflag = "0";
    fflag = $("#Ffreeflag").combobox('getValue');
    if ($("#GNAME").val() == "") {
        $.messager.alert('提示', '商品名称不能为空，谢谢！');
        return false;
    }
    if ($("#FPrice").val() == "") {
        $.messager.alert('提示', '商品单价不能为空，谢谢！');
        return false;
    }
    if ($("#FGTXM").val() == "") {
        $.messager.alert('提示', '商品条码不能为空，谢谢！');
        return false;
    }
    if ($("#FGtype").combobox('getText') == "请选择类别") {
        $.messager.alert('提示', '请指定类别，谢谢！');
        return false;
    }
    if ($("#FSupplyer").combobox('getText') == "请选择商家") {
        $.messager.alert('提示', '请指定商家，谢谢！');
        return false;
    }
    $.post("/Super/SaveGoods", {
        "action": "SaveGoodsList",
        "Dotype": $("#dotype").val(),
        "GCode": $("#GCODE").val(),
        "GName": $("#GNAME").val(),
        "GType": $("#FGtype").combobox('getValue'),
        "GUnit": $("#FUnit").val(),
        "GStandard": $("#FStandard").val(),
        "GDJ": $("#FPrice").numberbox('getValue'),
        "GSupplyer": $("#FSupplyer").combobox('getValue'),
        "GTXM": $("#FGTXM").numberbox('getValue'),
        "GMadein": $("#FMadein").val(),
        "GFreeflag": fflag,
        "GXgsl": $("#Xgsl").val(),
        "XgMode": $("#XgMode").combobox('getValue'),
        "SPShortCode": $("#SPShortCode").val()
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            $.messager.alert('提示', data);
        }
    });
    //$('#btnSave').linkbutton('disable');
    $('#editWindows').window({
        closed: true
    });
    $('#test').datagrid('load', {
        action: 'GetAllList'
    });

}


//保存修改内容(表单模式提交)
function submitContent() {

    var fflag = "0";
    fflag = $("#Ffreeflag").combobox('getValue');
    if ($("#GNAME").val() == "") {
        $.messager.alert('提示', '商品名称不能为空，谢谢！');
        return false;
    }
    if ($("#FPrice").val() == "") {
        $.messager.alert('提示', '商品单价不能为空，谢谢！');
        return false;
    }
    if ($("#FGTXM").val() == "") {
        $.messager.alert('提示', '商品条码不能为空，谢谢！');
        return false;
    }
    if ($("#FGtype").combobox('getText') == "请选择类别") {
        $.messager.alert('提示', '请指定类别，谢谢！');
        return false;
    }
    if ($("#FSupplyer").combobox('getText') == "请选择商家") {
        $.messager.alert('提示', '请指定商家，谢谢！');
        return false;
    }
    
    $("#FPrice").numberbox('enable');
    //提交表单
    $('#ffGoodEdit').form({
        url: '/Super/SaveGoods/',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            if ("0" == $("#goodPriceMset").val()) {
                $("#FPrice").numberbox('enable');
            } else {
                $("#FPrice").numberbox('disable');
            }
            alert(data)
        }
    });
    $('#ffGoodEdit').submit();

    //$('#btnSave').linkbutton('disable');
    $('#editWindows').window({
        closed: true
    });
    $('#test').datagrid('load', {
        action: 'GetAllList'
    });
}



//新增商品
function AddGoodsContent() {
    $('#editWindows').window({
        modal: true,
        closed: false
    });
    $("#dotype").val("add");//        
    $("#GCODE").val('');//   
    $("#GNAME").val('');// 
    $("#FGtype").val('');//  
    $("#FUnit").val('');//  
    $("#FStandard").val('');//  
    $("#FPrice").numberbox('setValue', '');//  
    $("#FSupplyer").val('');//  
    $("#FGTXM").numberbox('setValue','');//  
    $("#FMadein").val('');//  
    $("#Ffreeflag").val('');//
    $("#Xgsl").val('9999');//
    $('#FPrice').removeAttr('disabled');
    $('#picSrc').attr('src','/Content/GoodsImages/null.png');
    $("#SPShortCode").val('');// 

    $("#FPrice").numberbox('enable');
}

//修改模式
function AlterGoodsContent() {
    $('#editWindows').window({
        modal: true,
        closed: false
    });
    $("#dotype").val("update");//
    $('#FPrice').attr('disabled', "disabled");
    if ("0" == $("#goodPriceMset").val()) {
        $("#FPrice").numberbox('enable');
    } else {
        $("#FPrice").numberbox('disable');
    }
    
}


//调整商品价格
function ChangeGoodsPrice() {
    $('#ChangePrice').window({
        modal: true,
        closed: false
    });
    //$("#dotype").val("update");//        
}

//停用商品
function SetGoodsContent(e) {
    $.messager.confirm('Confirm', '您确认要改变该商品吗?', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');
            var idx = $('#test').datagrid('getRowIndex', row);
            //console.info(row);
            $.post("/Super/ChangeGoodsStatus", {
                "action": "ChangeGoodsStatus",
                "GTXM": row.GTXM,
                "GActive": e
            }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    $.messager.alert('提示', data);
                    if (data == "更新成功") {
                        if (e == "N") {
                            $('#test').datagrid('updateRow', {
                                index: idx,
                                row: {
                                    ACTIVE: 'N'
                                }
                            });
                        } else {
                            $('#test').datagrid('updateRow', {
                                index: idx,
                                row: {
                                    ACTIVE: 'Y'
                                }
                            });
                        }
                    }
                }
            })
        }
    });
}


//真正删除商品
function DeleteGood() {
    $.messager.confirm('Confirm', '您确认要删除该商品吗?', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');
            var idx = $('#test').datagrid('getRowIndex', row);
            //console.info(row);
            $.post("/Super/DeleteGood", {
                "action": "DeleteGood",
                "GTXM": row.GTXM,
            }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    $.messager.alert('提示', data);
                    if (data == "OK|删除成功") {
                        //var idx = $('#test').datagrid('getRowIndex', row);
                        $('#test').datagrid('deleteRow', idx);
                    }
                }
            })
        }
    });
}
//按条件查询
function FilterSearch() {
    if ($('#FGoodsType').combobox('getValue') == "0") {
        $('#FGoodsType').combobox('setValue', '');
    }
    if ($('#FGoodsStatus').combobox('getValue') == "") {
        $('#FGoodsStatus').combobox('setValue', '-2');
    }
    if ($('#FGoodsIsXianE').combobox('getValue') == "") {
        $('#FGoodsIsXianE').combobox('setValue', '2');
    }
    $('#test').datagrid('load', {
        action: 'GetAllList',
        GName: $("#FGoodsName").val(),
        GTXM: $("#FGoodsGTXM").val(),
        GType: $('#FGoodsType').combobox('getValue'),
        Active: $('#FGoodsStatus').combobox('getValue'),
        FFreeFlag: $('#FGoodsIsXianE').combobox('getValue'),
        selSupplyer: $('#selSupplyer').combobox('getValue'),
        FGoodsShortCode: $('#FGoodsShortCode').val()
        
    });
}


//清空查询条件
function clearSearch() {
    $("#FGoodsName").val('');
    $("#FGoodsGTXM").val('');
    $('#FGoodsType').combobox('setValue', '0');
    $('#FGoodsIsXianE').combobox('setValue', '2');
    $('#FGoodsStatus').combobox('setValue', '-2');
}



//Excel文件导入
function ExcelFileInport() {
    if ($("#excelFileName").val() == "") {
        $.messager.alert('提示', "请选择一个Excel文件!");
        return false;
    } else {
        $('#ffExcel').form({
            url: '/Super/ExcelGoodsInport/'+$("#typeId").val(),
            onSubmit: function () {
                // do some check    
                // return false to prevent submit;    
            },
            success: function (data) {
                alert(data)
            }
        });
        $('#ffExcel').submit();
    }
}

//Excel文件导出
function ExcelOutSave() {
    $.post('/Super/ExcelGoodsOut/'+$("#typeId").val(), {
        GName: $("#FGoodsName").val(),
        GTXM: $("#FGoodsGTXM").val(),
        GType: $('#FGoodsType').combobox('getValue'),
        Active: $('#FGoodsStatus').combobox('getValue'),
        FFreeFlag: $('#FGoodsIsXianE').combobox('getValue'),
        selSupplyer: $('#selSupplyer').combobox('getValue'),
        FGoodsShortCode: $('#FGoodsShortCode').val()
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK")
            {
                window.open("/Upload/" + words[1]);
            }
        }
    });
}



//扩展Editer
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            var ed = $(this).datagrid('getEditor', param);
            if (ed) {
                if ($(ed.target).hasClass('textbox-f')) {
                    $(ed.target).textbox('textbox').focus();
                } else {
                    $(ed.target).focus();
                }
            }
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    enableCellEditing: function (jq) {
        return jq.each(function () {
            var dg = $(this);
            var opts = dg.datagrid('options');
            opts.oldOnClickCell = opts.onClickCell;
            opts.onClickCell = function (index, field) {
                if (opts.editIndex != undefined) {
                    if (dg.datagrid('validateRow', opts.editIndex)) {
                        dg.datagrid('endEdit', opts.editIndex);
                        opts.editIndex = undefined;
                    } else {
                        return;
                    }
                }
                dg.datagrid('selectRow', index).datagrid('editCell', {
                    index: index,
                    field: field
                });
                opts.editIndex = index;
                opts.oldOnClickCell.call(this, index, field);
            }
        });
    }
});

$(function () {
    $('#test').datagrid().datagrid('enableCellEditing');
})



//结束编辑
function endEdit() {
    var rows = $dg.datagrid('getRows');
    for (var i = 0; i < rows.length; i++) {
        $dg.datagrid('endEdit', i);
    }
}
