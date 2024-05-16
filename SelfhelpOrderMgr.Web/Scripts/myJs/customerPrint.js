//用户列表清单
$('#test').datagrid({
    //title: '用户列表',
    iconCls: 'icon-save',
    //width: 790,
    height: $(window).height() * 0.9,
    fitColumns: false,
    nowrap: true,
    autoRowHeight: false,
    striped: true,
    collapsible: true,
    url: '/Super/GetSearchInvoices/' + $("#typeId").val(),
    //sortName: 'GCODE',       
    columns: [[
        { field: 'ck', checkbox: true },
        { field: 'InvoiceNo', title: '单号', width: 100, sortable: true },
        { field: 'FCriminal', title: '姓名', width: 80, sortable: true },
        { field: 'FCrimeCode', title: '编号', width: 80, sortable: true },
        { field: 'FAreaName', title: '队别', width: 100, sortable: true },
        {
            field: 'OrderDate', title: '日期', width: 100, sortable: true, formatter: function (value, row, index) {
                if (row.OrderDate != null) {
                    if (row.OrderDate != "") {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
        },
        { field: 'PType', title: '消费类型', width: 100 },
        { field: 'Amount', title: '金额', width: 60, align: 'right' },
        { field: 'Crtby', title: '消费机', width: 100 },
        { field: 'BankFlag', title: 'BankFlag', hidden: true, width: 100 },
        {
            field: 'TxtBankFlag', title: '扣款状态', width: 100, sortable: true, formatter: function (value, row, index) {
                if (row.BankFlag != null) {
                    if (row.BankFlag == "2") {
                        return '成功';
                    } else if (row.BankFlag == "1") {
                        return '已发送';
                    } else if (row.BankFlag == "3") {
                        return '手动成功';
                    } else if (row.BankFlag == "-1") {
                        return '失败';
                    } else if (row.BankFlag == "0") {
                        return '待发送';
                    }
                    else {
                        return '';
                    }
                } else {
                    return '';
                }
            }
        },
        { field: 'AmountA', title: 'A账户', width: 60, align: 'right' },
        { field: 'AmountB', title: 'B账户', width: 60, align: 'right' },
        { field: 'FTZSP_Money', title: '食品金额', width: 80, align: 'right' },
        { field: 'FreeAmountA', title: 'A不限额', width: 80, align: 'right' },
        { field: 'FreeAmountB', title: 'B不限额', width: 80, align: 'right' },
        { field: 'UserCyDesc', title: '处遇描述', width: 300, align: 'right' },

        
        { field: 'Remark', title: '备注', width: 150 }
    ]],
    sortOrder: 'asc',
    remoteSort: false,
    //singleSelect: true,
    //idField: 'GCODE',
    pageSize: 10,
    pageList: [10, 20,50,100,200,300,500],
    pagination: true

});
$(function () {
    $("#winTuiHuo").window("close");
});



function loadOrderDetail(invoiceNo) {
    if (invoiceNo == "") {
        $.messager.alert("提示", "消费主单号不能为空");
        return false;
    }
    $.post("/Super/GetInvoiceDtlByNo/", { "invoiceNo": invoiceNo }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            //alert(words[1]);
            dtlData = $.parseJSON(words[1]);
            $('#invDetails').datagrid({
                //title: '用户列表',
                iconCls: 'icon-edit',
                //width: 790,
                height: $(window).height() * 0.5,
                fitColumns: false,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                collapsible: true,
                //url: '/Super/GetInvoiceDtlByNo/' + invoiceNo,
                data: dtlData,
                //sortName: 'GCODE',       
                columns: [[
                    { field: 'ck', checkbox: true },
                    { field: 'seqno', title: '序号', width: 50, sortable: true },
                    { field: 'INVOICENO', title: '单号', width: 80, hidden: true, sortable: true },
                    { field: 'FCrimecode', title: '编号', width: 80, hidden: true,  sortable: true },
                    { field: 'GCODE', title: '商品编号', width: 100, hidden: true, sortable: true },
                    { field: 'GTXM', title: '条码', width: 100, hidden: true, sortable: true },
                    { field: 'SPShortCode', title: '简码', width: 100, hidden: true, sortable: true },
                    { field: 'GNAME', title: '商品名称', width: 100, sortable: true },
                    { field: 'GDJ', title: '单价', width: 60, sortable: true },
                    { field: 'QTY', title: '数量', width: 60, sortable: true },
                    { field: 'AMOUNT', title: '金额', width: 80, sortable: true },
                    { field: 'Remark', title: '备注', hidden: true, width: 150 }
                ]],
                sortOrder: 'asc',
                remoteSort: false,
                singleSelect: true,
                onSelect: function (rowIndex, rowData) {
                    $('#returnCount').numberbox("setValue","");
                },
                idField: 'seqno'
                //,
                //pageSize: 10,
                //pageList: [10, 20, 50, 100],
                //pagination: true

            });
        }
    });
    

}

function loadTuihuoDetail(invoiceNo) {
    $('#tuihuoDetail').datagrid({
        //title: '用户列表',
        iconCls: 'icon-edit',
        //width: 790,
        height: $(window).height() * 0.45,
        fitColumns: false,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        //url: '/Super/GetInvoicedtlByNo/' + invoiceNo,
        data:[],
        //sortName: 'GCODE',       
        columns: [[
            { field: 'ck', checkbox: true },
            { field: 'seqno', title: '序号', width: 50, hidden: true, sortable: true },
            { field: 'INVOICENO', title: '单号', width: 80, hidden: true, sortable: true },
            { field: 'FCrimecode', title: '编号', width: 80, hidden: true, sortable: true },
            { field: 'GCODE', title: '商品编号', width: 100, hidden: true, sortable: true },
            { field: 'GTXM', title: '条码', width: 100, hidden: true, sortable: true },
            { field: 'SPShortCode', title: '简码', width: 100, hidden: true, sortable: true },
            { field: 'GNAME', title: '商品名称', width: 100, sortable: true },
            { field: 'GDJ', title: '单价', width: 60, sortable: true },
            { field: 'QTY', title: '数量', width: 60, sortable: true },
            { field: 'AMOUNT', title: '金额', width: 100, sortable: true },
            { field: 'Remark', title: '备注', hidden: true, width: 150 }
        ]],
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'seqno'
        //,
        //pageSize: 10,
        //pageList: [10, 20, 50, 100],
        //pagination: true

    });

}


//消费退货
function SalesOrderReturn() {
    var rows = $("#test").datagrid("getSelections");
    if (rows.length > 1) {
        $.messager.alert("提示", "对不起只能选择一个主单");
        return false;
    }
    var row = $("#test").datagrid("getSelected");
    if (row == null) {
        $.messager.alert("提示", "消费主单号不能为空");
        return false;
    }    
    loadOrderDetail(row.InvoiceNo);
    loadTuihuoDetail('');
    $("#winTuiHuo").window("open");

}


function IsMoreThirtyDay(firstDate, secondDate) {
    //let first = this.data.date  //开始时间
    //let second = e.detail.value  //结束时间
    var data1 = Date.parse(firstDate.replace(/-/g, "/"));
    var data2 = Date.parse(secondDate.replace(/-/g, "/"));
    var datadiff = data2 - data1;
    var time = 31 * 24 * 60 * 60 * 1000;
    console.log(firstDate.length)
    console.log(secondDate.length)
    if (firstDate.length > 0 && secondDate.length > 0) {
        if (datadiff < 0 || datadiff > time) {
            return true;
        } else {
            return false;
        }
    }
}


//积分整单退货
//撤单
function SalesOrderAllReturn() {
    $.messager.confirm('提示', '您真要对执行退货操作吗?', function (r) {
        if (r) {
            var rows = $("#test").datagrid("getSelections");
            var selectRows = "";

            if (rows.length > 0) {
                $.messager.confirm('提示', '记录一共有' + rows.length + '条，对吗？', function (r) {
                    if (r) {
                        for (var i = 0; i < rows.length; i++) {
                            if (IsMoreThirtyDay(rows[i].OrderDate, Date())) {
                                $.messager.alert("提示", rows[i].InvoiceNo + ",消费时间超过30天不能退货");
                                return false;
                            }
                            if (selectRows == "") {
                                selectRows = rows[i].InvoiceNo;
                            } else {
                                selectRows = selectRows + "|" + rows[i].InvoiceNo;
                            }
                        }

                        $.post("/Super/SalesOrderAllReturn", { "Invoices": selectRows }, function (data, status) {
                            if (status != "success") {
                                return false;
                            } else {
                                var words = data.split("|");
                                if (words[0] == "OK") {
                                    $.messager.alert("提示", "退货成功请重新查询");
                                } else {
                                    $.messager.alert("提示", data);
                                }
                            }
                        });
                    }
                });


            } else {
                $.messager.alert("提示", "请至少选择一行记录");
                return false;
            }
        }
    });
}



//增加退货明细
function btnAddList() {
    if ($('#returnCount').numberbox("getValue") == "") {
        $.messager.alert("提示", "请输入退货数量");
        return false;
    }
    var sourRow = $('#invDetails').datagrid("getSelected");
    if (sourRow.QTY < $('#returnCount').numberbox("getValue")) {
        $.messager.alert("提示", "退货的数量不能大于购买的数量");
        return false;
    }
    $('#tuihuoDetail').datagrid('appendRow', {
        seqno: sourRow.seqno,
        INVOICENO: sourRow.INVOICENO,
        FCrimecode: sourRow.FCrimecode,
        GCODE: sourRow.GCODE,
        GNAME: sourRow.GNAME,
        GTXM:sourRow.GTXM,
        SPShortCode: sourRow.SPShortCode,
        GDJ: sourRow.GDJ,
        QTY: $('#returnCount').numberbox("getValue"),
        AMOUNT: sourRow.GDJ * $('#returnCount').numberbox("getValue"),
        Remark: sourRow.Remark
    });
    var thRows = $('#tuihuoDetail').datagrid('getRows');
    //alert(thRows.length);
    if (thRows.length > 0) {
        var thMoney = 0;
        for (var i = 0; i < thRows.length; i++) {
            //alert(thRows[i].AMOUNT);
            thMoney = thMoney + thRows[i].AMOUNT;
        }
        $('#returnMoney').numberbox("setValue", thMoney);
    }

}
//删除退货明细
function btnDelList() {
    if ($('#tuihuoDetail').datagrid("getSelected") == null) {
        $.messager.alert("提示", "请选择一行要移走的记录");
        return false;
    }
    var sourRow = $('#tuihuoDetail').datagrid("getSelected");
    var idx = $('#tuihuoDetail').datagrid("getRowIndex", sourRow);
    $('#tuihuoDetail').datagrid("deleteRow", idx);

    var thRows = $('#tuihuoDetail').datagrid('getRows');
    var thMoney = 0;
    if (thRows.length > 0) {        
        for (var i = 0; i < thRows.length; i++) {
            thMoney = thMoney + thRows[i].AMOUNT;
        }        
    }
    $('#returnMoney').numberbox("setValue", thMoney);

}

//保存退货单
function btnSubmitTuihuo() {
    var details = $('#tuihuoDetail').datagrid('getRows');
    if (details.length <= 0) {
        $.messager.alert("提示", "对不起，请录入相应的退货记录");
        return false;
    }
    var effectRow = new Object();
    if (details.length) {
        effectRow["details"] = JSON.stringify(details);
    }
    $.post("/Super/AddTuiHuoOrder", effectRow, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                $("#winTuiHuo").window("close");
                $.messager.alert("提示", data);
            } else {
                $.messager.alert("提示", data);
            }
            

            return false;
        }
    });
}

function PrintCustomers(e) {
    if(e=="mul"){
        var rows = $("#test").datagrid("getSelections");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = selectRows + "|"+ rows[i].InvoiceNo;
                }            
            }
        } else {
            $.messager.alert("提示", "请至少选择一行记录");
            return false;
        }

    } else if (e == "one") {
        var row = $("#test").datagrid("getSelected");
        var selectRows = row.InvoiceNo;
    } else if (e == "all") {        
        $("#test").datagrid("selectAll");
        var rows = $("#test").datagrid("getRows");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = rows[i].InvoiceNo + "|" + selectRows;
                }
            }
        }
    } else {
        return false;
    }

    $.post("/Home/GetInvoices", {
        "invoices": selectRows
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var invs = $.parseJSON(data);
            $("#template").empty();//清空
            $("#PrintXPItemS").val('');
            var PrintXPItemS = "";
            if (invs.length > 0) {

                var k = 0;
                var pageRows = 20;
                for (var i = 0; i < invs.length; i++) {
                    var content = invs[i];
                    var inv = content;

                    //获得小票总页数
                    var pages = Math.ceil(inv.details.length / pageRows);

                    //alert(pages);
                    for (var p = 1; p <= pages; p++) {

                        var startRow = (p - 1) * pageRows;
                        var endRow = p * pageRows;
                        if (endRow > inv.details.length) {
                            endRow = inv.details.length
                        }
                        if (p == 1) {//如果是第一页
                            //在这里打印小票的头部
                            if (PrintXPItemS == "") {
                                PrintXPItemS = "temp" + k;
                            } else {
                                PrintXPItemS = PrintXPItemS + "|temp" + k;
                            }
                            var printTitle = "<tr><td>后台首次打印</td></tr>";
                            if (inv.invoice.printCount > 0) {
                                var printCount = inv.invoice.printCount + 1;
                                printTitle = "<tr><td>后台第" + printCount + "次重打</td></tr>";
                            }
                            var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:left;font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 3) + "px;'>消费一卡通系统</div>"
                            + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px; width:200px;'>"
                            + "<tbody>"
                            + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td></tr>"
                            + "<tr><td>日期：" + getLongTime(inv.invoice.OrderDate) + "</td></tr>"
                            + "<tr><td>编号：" + inv.invoice.FCrimeCode + "</td></tr>"
                            + "<tr><td>姓名：" + inv.invoice.FCriminal + "</td></tr>"
                            + "<tr><td colspan='2'>队别：" + inv.invoice.FAreaName + "（房号:" + inv.invoice.RoomNo + "）</td></tr>"
                            + printTitle
                            + "<tr><td>打印时间：" + getDateLongTime(new Date()) + "</td></tr>"
                            + "</tbody>"
                            + "</table>"
                            + "<hr />"
                            + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
                            + "<thead>"
                            + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:40px;'>金额</th></tr>"
                            + "</thead>"
                            + "<tbody>";
                        } else {
                            //打印次页小票的头部
                            //后20行=====================

                            if (PrintXPItemS == "") {
                                PrintXPItemS = "temp" + k;
                            } else {
                                PrintXPItemS = PrintXPItemS + "|temp" + k;
                            }
                            var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:center'>" + inv.invoice.InvoiceNo + " (第" + p + "页)</div>"
                            + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
                            + "<thead>"
                            + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:40px;'>金额</th></tr>"
                            + "</thead>"
                            + "<tbody>";
                        }
                        //小票的主体商品信息
                        for (var j = startRow; j < endRow; j++) {
                            var remark="";
                            if (inv.details[j].Remark != "") {
                                remark = "【" + inv.details[j].Remark + "】";
                            }
                            xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].GTXM + "</td><td align='center'>" + inv.details[j].GDJ + "</td><td align='center'>" + inv.details[j].QTY + "</td><td align='right'>" + inv.details[j].AMOUNT + "</td></tr>"
                            + "<tr><td colspan='4' style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + remark + "</td></tr>"
                        }
                        //小票的结尾部份
                        if (p == pages) {
                            //打印小票最后一页结尾
                            xiaopiao = xiaopiao + "</tbody>"
                                + "</table>"
                                + "<hr />"
                                + "<span>消费合计：" + inv.invoice.Amount + "元</span><hr/>"
                                + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>存款账户余额:" + inv.criminal.AmountAmoney + "元</div>"
                                + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>报酬账户余额:" + inv.criminal.AmountBmoney + "元</div>"
                                + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>留存账户余额:" + inv.criminal.AmountCmoney + "元</div>"
                                + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>账户总余额:" + toDecimal2(inv.criminal.AmountAmoney + inv.criminal.AmountBmoney + inv.criminal.AmountCmoney) + "元</div>"
                                + "</div><br/></div>";

                            $("#template").append(xiaopiao);
                            k++;
                        } else {
                            //提示小票后面还有下一页
                            //tipNextPageInfo(xiaopiao, p);
                            xiaopiao = xiaopiao + "</tbody>"
                                + "</table>"
                                + "<div>（这里第" + p + "页），下面还有内容</div>"
                                + "</div><br/></div>";

                            $("#template").append(xiaopiao);
                            k++;
                        }
                    }

                    //======结束========================
                }
            }
            $("#PrintXPItemS").val(PrintXPItemS);
        }
    });
}



function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }

    var selTypeFlags = rtnMulSaleTypes();

    // 获取查询条件
    var obj = getObjSearchWhere();
    $('#test').datagrid('load', {
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getText'),
        FCode: $("#FCode").numberbox('getValue'),
        FName: $("#FName").val(),
        FCrtBy: $("#FCrtBy").combobox('getValue'),
        TypeFlag: selTypeFlags,
        Flag: $("#Flag").combobox('getValue'),
        GoodsType: $("#GoodsType").combobox('getValue'),
        GoodName: $("#GoodName").val(),
        GoodGTXM: $("#GoodGTXM").val(),
        SpShortCode: $("#SpShortCode").val()
    });

}


function rtnMulSaleTypes() {
    var TypeFlags = $("#TypeFlag").combobox('getValues');
    var selTypeFlags = "";
    for (var i = 0; i < TypeFlags.length; i++) {
        var TypeFlag = TypeFlags[i];
        if (selTypeFlags == "") {
            selTypeFlags = "'" + TypeFlag + "'";;
        } else {
            selTypeFlags = selTypeFlags + ",'" + TypeFlag + "'";
        }
    }
    return selTypeFlags;
}

function printMulXiaoPiao()//打印多个小票
{
    //var rows = $("#test").datagrid('getSelections');
    //if (rows.length <= 0) {
    //    $.messager.alert("提示", "请至少选择一行记录");
    //    return false;
    //}
    //var InvoiceNos = "";
    //for (var i = 0; i < rows.length; i++) {
    //    var tid = i;

    //    if (InvoiceNos == "") {
    //        InvoiceNos = rows[i].InvoiceNo;
    //    } else {
    //        InvoiceNos = InvoiceNos + "|" + rows[i].InvoiceNo;
    //    }
    //    myTestHtml("#temp" + tid,$("#xiaoPiaoPageWidth").val());
    //}

    var PrintXPItemS = $("#PrintXPItemS").val();
    var pItems = PrintXPItemS.split("|");
    for (var i = 0; i < pItems.length; i++) {
        var item = pItems[i];
        //myTestHtml("#" + item, $("#xiaoPiaoPageWidth").val());
        printXiaoPiaoXinxi(item, $("#xiaoPiaoPageWidth").val());
    }

    //更新小票的次数
    //1、找出小票的单号（多个）
    var rows = $("#test").datagrid('getSelections');
    if (rows.length <= 0) {
        $.messager.alert("提示", "请至少选择一行记录");
        return false;
    }
    var InvoiceNos = "";
    
    for (var i = 0; i < rows.length; i++) {
        var tid = i;

        if (InvoiceNos == "") {
            InvoiceNos = rows[i].InvoiceNo;
        } else {
            InvoiceNos = InvoiceNos + "|" + rows[i].InvoiceNo;
        }
    }

    //2、后台更新小票的打印次数（多个）
    var selectRows = InvoiceNos;
    $.post("/Home/UpdatePrintCount", { "Invoices": selectRows }, function (data, status) {
        if ("success" == data) {
            //alert(data);
            $.messager.alert("提示", data);
        }
    });
}




function getStrSearchWhere() {

    var selTypeFlags = rtnMulSaleTypes();

    var strWhere = "";
    strWhere="startTime="+ $("#StartDate").datetimebox('getValue')
    +"&endTime="+ $("#EndDate").datetimebox('getValue')
    +"&areaName="+ $("#FAreaName").combobox('getText')
    +"&FCode="+ $("#FCode").numberbox('getValue')
    +"&FName="+ $("#FName").val()
        + "&FCrtBy=" + $("#FCrtBy").combobox('getValue')
    + "&Flag=" + $("#Flag").combobox('getValue')
    + "&TypeFlag=" + selTypeFlags
    + "&GoodsType="+ $("#GoodsType").combobox('getValue')
    + "&GoodName="+ $("#GoodName").val()
    + "&GoodGTXM="+ $("#GoodGTXM").val()
    + "&SpShortCode="+ $("#SpShortCode").val();

    return strWhere;    
}

function getObjSearchWhere() {

    var selTypeFlags = rtnMulSaleTypes();

    var obj = {
        "startTime": $("#StartDate").datetimebox('getValue'),
        "endTime": $("#EndDate").datetimebox('getValue'),
        "areaName": $("#FAreaName").combobox('getText'),
        "FCode": $("#FCode").numberbox('getValue'),
        "FName": $("#FName").val(),
        "FCrtBy": $("#FCrtBy").combobox('getValue'),
        "TypeFlag": selTypeFlags,
        "GoodsType": $("#GoodsType").combobox('getValue'),
        "GoodName": $("#GoodName").val(),
        "GoodGTXM": $("#GoodGTXM").val(),
        "SpShortCode": $("#SpShortCode").val()
    };
    return obj;
}

function PrintSumOrder(id) {
    var strWhere = getStrSearchWhere();
    window.open("/Super/PrintSumOrder/" + id + "?" + strWhere);

}

function ExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getObjSearchWhere();
    $.post("/Super/ExcelSumOrder/" + id, objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });

}
//A4打印消费单
//printType 0表示消费清单，1表示签字确认单,2表示队别汇总单,3表示号房汇总单
function printMulXiaofeiDan(printType) {

    CreateXiaofeiDanReport('mul', printType);

    //var rows = $("#test").datagrid('getSelections');
    //for (var i = 0; i < rows.length; i++) {
    //    var tid = i;
    //    myTestHtml("#temp" + tid);
    //}
}

//打印所有签字确认单
function printAllXiaofeiDan() {
    
    var strWhere = getStrSearchWhere();
    //alert(strWhere);
    window.open("/Super/PrintAllXiaofeiDan?" + strWhere);
}


//Excel导出所有签字确认单
function ExcelAllXiaofeiDan() {
    //alert("ddddd");
    var objWhere = getObjSearchWhere();
    console.log(objWhere);
    $.post("/Super/ExcelAllXiaofeiDan/1", objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
}

//生成消费单
//printType 0表示消费清单，1表示签字确认单
function CreateXiaofeiDanReport(e, printType) {
    //var SearchWhere="&startTime="+ $("#StartDate").datetimebox('getValue')+ "&endTime="+ $("#EndDate").datetimebox('getValue')+"&areaName="+ $("#FAreaName").combobox('getText')+"&FCode="+ $("#FCode").numberbox('getValue')+ "&FName="+ $("#FName").val();
    if (e == "mul") {
        var rows = $("#test").datagrid("getSelections");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = selectRows + "|" + rows[i].InvoiceNo;
                }
            }
        } else {
            $.messager.alert("提示", "请至少选择一行记录");
            return false;
        }
    } else if (e == "one") {
        var row = $("#test").datagrid("getSelected");
        var selectRows = row.InvoiceNo;
    } else if (e == "all") {
        $("#test").datagrid("selectAll");
        var rows = $("#test").datagrid("getRows");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = rows[i].InvoiceNo + "|" + selectRows;
                }
            }
        }
    } else {
        return false;
    }
    window.open("/Super/PrintXiaofeiDan?invoices=" + selectRows + "&printType=" + printType );

}

//撤单
function CancelInvoiceOrder() {
    $.messager.confirm('提示', '您真要对执行撤单操作吗?', function (r) {
        if (r) {
            var rows = $("#test").datagrid("getSelections");
            var selectRows = "";
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].BankFlag >= 1) {
                        $.messager.alert("提示", rows[i].InvoiceNo + ",已经发送到银行不能撤消");
                        return false;
                    }
                    if (selectRows == "") {
                        selectRows = rows[i].InvoiceNo;
                    } else {
                        selectRows = selectRows + "|" + rows[i].InvoiceNo;
                    }
                }
                
                $.post("/Super/CancelInvoiceOrder", { "Invoices": selectRows }, function (data, status) {
                    if (status != "success") {
                        return false;
                    } else {
                        var words = data.split("|");
                        if (words[0] == "OK") {
                            $.messager.alert("提示", "撤消成功请重新查询");
                        } else {
                            $.messager.alert("提示", data);
                        }
                    }
                });
            } else {
                $.messager.alert("提示", "请至少选择一行记录");
                return false;
            }
        }
    });
}

