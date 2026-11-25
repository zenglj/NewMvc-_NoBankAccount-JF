
$(function () {

    // 商品类别下拉
    $('#FGtype').combobox({
        url: '/Super/GetGoodsType',
        valueField: 'Fcode',
        textField: 'Fname'
    });

    // 限额下拉（本地字典：0=是(需要限额) / 1=否(不限额)）
    $('#Ffreeflag').combobox({
        data: [
            { Fname: '是', Fcode: '0' },
            { Fname: '否', Fcode: '1' }
        ],
        valueField: 'Fcode',
        textField: 'Fname'
    });

    // 商品主列表
    $('#test').datagrid({
        iconCls: 'icon-save',
        height: $(window).height() * 0.9,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Super/GetGoods/' + $('#typeId').val(),
        sortName: 'GCODE',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'GCODE',
        pageSize: 10,
        pageList: [10, 20, 30, 50],
        pagination: true,
        columns: [[
            { field: 'ck', checkbox: true },
            { field: 'GCODE', title: '编号', width: 50, sortable: true },
            { field: 'GNAME', title: '名称', width: 120, sortable: true },
            { field: 'GTXM', title: '条形码', width: 110 },
            { field: 'SPShortCode', title: '店内码', width: 80, sortable: true },
            { field: 'GTYPE', title: '类别', width: 100, sortable: true },
            { field: 'GUnit', title: '单位', width: 50 },
            { field: 'GStandard', title: '规格', width: 100 },
            { field: 'GDJ', title: '单价', width: 70 },
            { field: 'GSupplyer', title: '供应商', width: 120 },
            { field: 'LevelName', title: '分类等级', width: 80 },
            { field: 'CrtBy', title: '创建人', width: 70 },
            { field: 'ModBy', title: '修改人', width: 70 },
            { field: 'Moddt', title: '修改时间', width: 120 },
            {
                field: 'ACTIVE', width: 60, title: '状态', align: 'center',
                formatter: function (value) {
                    return value == 'Y' ? '<span style="color:green">上架</span>' : '<span style="color:red">下架</span>';
                }
            },
            { field: 'gindj', title: '进价', width: 70 },
            { field: 'madein', title: '产地', width: 100 },
            {
                field: 'Ffreeflag', width: 60, title: '限额', align: 'center',
                formatter: function (value) {
                    return value == '1' ? '否' : '是';
                }
            },
            { field: 'Xgsl', title: '限购量', width: 70 }
        ]],
        rowStyler: function (index, row) {
            if (row.ACTIVE == 'N') {
                return 'background-color:#fff0f0;';
            }
        },
        onCheck: function (rowIndex, rowData) {
            SetSelectRowData(rowData);
        },
        onUncheck: function () {
            ClearUnSelectRowData();
        }
    });

    // 编辑窗口
    $('#editWindows').window({
        modal: true,
        closed: true,
        minimizable: false
    });

    // 商品属性编辑窗口
    LoadGoodAttrsTable();
    $('#editGoodAttr').window({
        modal: true,
        closed: true,
        minimizable: false
    });
});

// =================== 公共方法 ===================

/** 收集查询条件（主列表 & Excel导出共用） */
function buildQueryParams() {
    var typeVal = $('#FGoodsType').combobox('getValue');
    var statusVal = $('#FGoodsStatus').combobox('getValue');
    var freeVal = $('#FGoodsIsXianE').combobox('getValue');
    return {
        GName: $('#FGoodsName').val(),
        GTXM: $('#FGoodsGTXM').val(),
        GType: (typeVal == '0') ? '' : typeVal,
        Active: (statusVal == '-2') ? '' : statusVal,
        FFreeFlag: (freeVal == '2') ? '' : freeVal,
        selSupplyer: $('#selSupplyer').combobox('getValue'),
        FGoodsShortCode: $('#FGoodsShortCode').val(),
        FLevelName: $('#FLevelName').combobox('getValue')
    };
}

/** 用当前筛选条件刷新主表 */
function refreshGrid() {
    $('#test').datagrid('clearSelections');
    $('#test').datagrid('load', buildQueryParams());
}

/** 商品表单校验（add / update 共用） */
function validateGoodsForm() {
    if ($('#GNAME').val() == '') {
        $.messager.alert('提示', '商品名称不能为空');
        return false;
    }
    if ($('#FGTXM').numberbox('getValue') == '' || $('#FGTXM').numberbox('getValue') == null) {
        $.messager.alert('提示', '商品条码不能为空');
        return false;
    }
    var price = $('#FPrice').numberbox('getValue');
    if (price == '' || price == null) {
        $.messager.alert('提示', '商品单价不能为空');
        return false;
    }
    if ($('#FGtype').combobox('getText') == '' || $('#FGtype').combobox('getText') == '请选择类别') {
        $.messager.alert('提示', '请指定商品类别');
        return false;
    }
    if ($('#FSupplyer').combobox('getText') == '' || $('#FSupplyer').combobox('getText') == '请选择商家') {
        $.messager.alert('提示', '请指定供应商');
        return false;
    }
    return true;
}

// =================== 表单回填 / 清空 ===================

function SetSelectRowData(rowData) {
    $('#GCODE').val(rowData.GCODE);
    $('#GNAME').val(rowData.GNAME);
    $('#FGtype').combobox('setValue', rowData.GTYPE);
    $('#FUnit').val(rowData.GUnit);
    $('#FStandard').val(rowData.GStandard);
    $('#FPrice').numberbox('setValue', rowData.GDJ);
    $('#FSupplyer').combobox('setValue', rowData.GSupplyer);
    $('#FGTXM').numberbox('setValue', rowData.GTXM);
    $('#FMadein').val(rowData.madein);
    $('#Ffreeflag').combobox('setValue', rowData.Ffreeflag);
    $('#Xgsl').val(rowData.Xgsl);
    $('#XgMode').combobox('setValue', rowData.XgMode);
    $('#SPShortCode').val(rowData.SPShortCode);
    $('#LevelName').combobox('setValue', rowData.LevelName);
    $('#picSrc').attr('src', rowData.src || '/Content/GoodsImages/null.png');

    // 商品属性区
    $('#attrGCode').val(rowData.GCODE);
    $('#attrGName').val(rowData.GNAME);
    $('#attrGTXM').val(rowData.GTXM);
    $('#attrSPShortCode').val(rowData.SPShortCode);
    $('#attrTable').datagrid('load', { GCode: rowData.GCODE });
    $('#GoodAttr').val('');
    $('#btnGoodAttrAdd').linkbutton('enable');
    $('#btnGoodAttrSave').linkbutton('disable');
    $('#btnGoodAttrDel').linkbutton('disable');
}

function ClearUnSelectRowData() {
    $('#GCODE').val('');
    $('#GNAME').val('');
    $('#FGtype').combobox('setValue', '');
    $('#FUnit').val('');
    $('#FStandard').val('');
    $('#FPrice').numberbox('setValue', '');
    $('#FSupplyer').combobox('setValue', '');
    $('#FGTXM').numberbox('setValue', '');
    $('#FMadein').val('');
    $('#Ffreeflag').combobox('setValue', '');
    $('#Xgsl').val('');
    $('#XgMode').combobox('setValue', '');
    $('#SPShortCode').val('');
    $('#LevelName').combobox('setValue', '');
    $('#picSrc').attr('src', '');

    $('#attrGCode').val('');
    $('#attrGName').val('');
    $('#attrGTXM').val('');
    $('#attrSPShortCode').val('');
    $('#GoodAttr').val('');
    $('#btnGoodAttrAdd').linkbutton('enable');
    $('#btnGoodAttrSave').linkbutton('disable');
    $('#btnGoodAttrDel').linkbutton('disable');
}

// =================== 工具栏按钮 ===================

function AddGoodsContent() {
    ClearUnSelectRowData();
    $('#dotype').val('add');
    $('#editWindows').window({ title: '新增商品' }).window('open');
    $('#FPrice').numberbox('enable');
    $('#Xgsl').val('9999');
    $('#picSrc').attr('src', '/Content/GoodsImages/null.png');
}

function AlterGoodsContent() {
    var selected = $('#test').datagrid('getSelected');
    var checked = $('#test').datagrid('getChecked');
    if (checked.length === 0) {
        if (!selected) {
            $.messager.alert('提示', '请选择一条要编辑的记录');
            return;
        }
        checked = [selected];
    }
    if (checked.length > 1) {
        $.messager.alert('提示', '编辑只能选择一条记录，当前已选择 ' + checked.length + ' 条');
        return;
    }

    var row = checked[0];
    SetSelectRowData(row);

    $('#dotype').val('update');
    $('#editWindows').window({ title: '编辑商品' }).window('open');
    // 按系统参数决定价格是否可编辑
    if ($('#goodPriceMset').val() == '0') {
        $('#FPrice').numberbox('enable');
    } else {
        $('#FPrice').numberbox('disable');
    }
}

function SetGoodsContent(e) {
    var rows = $('#test').datagrid('getChecked');
    if (rows.length === 0) {
        $.messager.alert('提示', '请先勾选要上架/下架的商品');
        return;
    }
    $.messager.confirm('确认', '您确认要对 ' + rows.length + ' 条商品进行 ' + (e == 'Y' ? '上架' : '下架') + ' 操作吗？', function (r) {
        if (!r) return;
        var gtxms = rows.map(function (row) { return row.GTXM; });
        $.post('/Super/ChangeMulGoodsStatus', {
            action: 'ChangeGoodsStatus',
            gtxms: JSON.stringify(gtxms),
            GActive: e
        }, function (data, status) {
            if (status != 'success') return;
            $.messager.alert('提示', data);
            refreshGrid();
        });
    });
}

function DeleteGood() {
    var rows = $('#test').datagrid('getSelections');
    if (rows.length === 0) {
        $.messager.alert('提示', '请先勾选要删除的商品');
        return;
    }
    $.messager.confirm('确认', '您确认要删除选中的 ' + rows.length + ' 条商品吗？', function (r) {
        if (!r) return;
        var gtxmStr = rows.map(function (row) { return row.GTXM; }).join(',');
        $.post('/Super/DeleteGood', {
            action: 'DeleteGood',
            GTXM: gtxmStr
        }, function (data, status) {
            if (status != 'success') return;
            var words = (data || '').split('|');
            if (words[0] == 'OK') {
                $.messager.alert('提示', words[1] || '删除成功');
                refreshGrid();
            } else {
                $.messager.alert('提示', data);
            }
        });
    });
}

function submitContent() {
    if (!validateGoodsForm()) return false;
    // 强制启用价格控件以参与表单提交
    $('#FPrice').numberbox('enable');

    $('#ffGoodEdit').form({
        url: '/Super/SaveGoods/',
        onSubmit: function () { return true; },
        success: function (data) {
            var words = (data || '').split('|');
            if (words[0] == 'OK') {
                $.messager.alert('提示', words[1] || '保存成功');
                $('#editWindows').window('close');
                refreshGrid();
            } else {
                $.messager.alert('提示', data);
            }
            // 按系统参数恢复价格控件禁用状态
            if ($('#goodPriceMset').val() != '0') {
                $('#FPrice').numberbox('disable');
            }
        }
    });
    $('#ffGoodEdit').submit();
    return false;
}

// =================== 查询 / 清空 ===================

function FilterSearch() {
    refreshGrid();
}

function clearSearch() {
    $('#FGoodsName').val('');
    $('#FGoodsGTXM').val('');
    $('#FGoodsShortCode').val('');
    $('#FGoodsType').combobox('setValue', '0');
    $('#FGoodsStatus').combobox('setValue', '-2');
    $('#FGoodsIsXianE').combobox('setValue', '2');
    $('#FLevelName').combobox('setValue', '0');
    $('#selSupplyer').combobox('clear');
    refreshGrid();
}

// =================== Excel 导入 / 导出 ===================

function ExcelFileInport() {
    if ($('#excelFileName').val() == '') {
        $.messager.alert('提示', '请选择一个 Excel 文件!');
        return false;
    }
    $('#ffExcel').form({
        url: '/Super/ExcelGoodsInport/' + $('#typeId').val(),
        onSubmit: function () { return true; },
        success: function (data) {
            // 导入通常耗时较长，给个提示并刷新
            $.messager.alert('提示', data, function () { refreshGrid(); });
        }
    });
    $('#ffExcel').submit();
    return false;
}

function ExcelOutSave() {
    $.post('/Super/ExcelGoodsOut/' + $('#typeId').val(),
        buildQueryParams(),
        function (data, status) {
            if (status != 'success') return;
            var words = (data || '').split('|');
            if (words[0] == 'OK') {
                window.open('/Upload/' + words[1]);
            } else {
                $.messager.alert('提示', data);
            }
        });
}

// =================== 商品属性 ===================

function LoadGoodAttrsTable() {
    $('#attrTable').datagrid({
        width: 750,
        height: 280,
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
            { field: 'GCode', width: 80, sortable: true, title: '编码' },
            {
                field: 'GoodsAttrId', width: 100, sortable: true, title: '类型',
                formatter: function (value) {
                    var map = { '1': '口味', '2': '尺码', '3': '规格' };
                    return map[value] || value;
                }
            },
            { field: 'AttrInfo', width: 200, title: '规格型号信息' }
        ]],
        onSelect: function (rowIndex, rowData) {
            $('#goodsAttrType').combobox('setValue', rowData.GoodsAttrId);
            $('#GoodAttr').val(rowData.AttrInfo);
            $('#GoodAttrId').val(rowData.ID);
            $('#attrDoType').val('save');
            $('#btnGoodAttrAdd').linkbutton('disable');
            $('#btnGoodAttrSave').linkbutton('enable');
            $('#btnGoodAttrDel').linkbutton('enable');
        }
    });
}

function EditGoodAttr() {
    $('#editGoodAttr').window('open');
}

function AddGoodsAtrr() {
    $('#goodsAttrType').combobox('clear');
    $('#GoodAttr').val('');
    $('#GoodAttrId').val('');
    $('#attrDoType').val('add');
    $('#btnGoodAttrAdd').linkbutton('disable');
    $('#btnGoodAttrSave').linkbutton('enable');
    $('#btnGoodAttrDel').linkbutton('enable');
}

function SaveGoodsAtrr() {
    if ($('#goodsAttrType').combobox('getValue') == '') {
        $.messager.alert('提示', '请选择一个类型');
        return false;
    }
    if ($('#GoodAttr').val() == '') {
        $.messager.alert('提示', '商品规格不能为空');
        return false;
    }
    $.post('/Super/AddGoodAttrs', {
        GCode: $('#attrGCode').val(),
        GoodsAttrId: $('#goodsAttrType').combobox('getValue'),
        AttrInfo: $('#GoodAttr').val(),
        ID: $('#GoodAttrId').val(),
        attrDoType: $('#attrDoType').val()
    }, function (data, status) {
        if (status != 'success') return;
        var words = (data || '').split('|');
        if (words[0] == 'OK') {
            var info = $.parseJSON(words[1]);
            if ($('#attrDoType').val() == 'add') {
                $('#attrTable').datagrid('appendRow', info);
            } else {
                var row = $('#attrTable').datagrid('getSelected');
                var idx = $('#attrTable').datagrid('getRowIndex', row);
                $('#attrTable').datagrid('updateRow', { index: idx, row: info });
            }
            $('#attrDoType').val('');
            $('#GoodAttr').val('');
            $('#btnGoodAttrAdd').linkbutton('enable');
            $('#btnGoodAttrSave').linkbutton('disable');
            $('#btnGoodAttrDel').linkbutton('disable');
            $.messager.alert('提示', '保存成功');
        } else {
            $.messager.alert('提示', data);
        }
    });
}

function DelGoodsAtrr() {
    var row = $('#attrTable').datagrid('getSelected');
    if (!row) {
        $.messager.alert('提示', '请先勾选一条规格属性');
        return;
    }
    $.messager.confirm('确认', '您确认删除该规格属性吗？', function (r) {
        if (!r) return;
        var idx = $('#attrTable').datagrid('getRowIndex', row);
        $.post('/Super/DelGoodAttrs', { ID: row.ID }, function (data, status) {
            if (status != 'success') return;
            var words = (data || '').split('|');
            if (words[0] == 'OK') {
                $('#attrTable').datagrid('deleteRow', idx);
                $.messager.alert('提示', '删除成功');
            } else {
                $.messager.alert('提示', data);
            }
        });
    });
}
