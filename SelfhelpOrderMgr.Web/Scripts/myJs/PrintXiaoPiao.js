﻿function PrintCustomers(e) {

    var selectRows= $("#InvoiceNos").val();
    $.post("/Home/GetInvoices", {
        "invoices": selectRows
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            //$.messager.alert('提示', data);
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
                            var startRow = (p-1) * pageRows;
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
                                var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:center'>" + inv.invoice.InvoiceNo + " (第"+ p +"页)</div>"
                                + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
                                + "<thead>"
                                + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:40px;'>金额</th></tr>"
                                + "</thead>"
                                + "<tbody>";
                            }         
                            //小票的主体商品信息
                            for (var j = startRow; j < endRow; j++) {
                                var remark = "";
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




function show() {
    //// 获取id为 mytext 的普通元素值   
    //var mytext = document.getElementById("test").value;

    ////用标记id的td元素 获取值方法   
    //var td1 = document.getElementById("td1").innerHTML;

    //用获取table(通过其id ) 获取指定的行，列   
    var mytable = document.getElementById("test").rows[1].cells[2].innerHTML;

    //遍历 table表格   
    var s3 = document.getElementsByTagName("table")[0]; //获取第一个表格   

    //alert(td1);    //第一行第一列   
    alert(mytable);  //第二行第三列  
    for (var i = 0; i < s3.rows.length; i++) {
        for (var j = 0; j < s3.rows[i].cells.length; j++) {
            alert(s3.rows[i].cells[j].innerHTML);
        }
    }
}


function btnSearch() {
    var areaname = "";
    if ("请选择队别" != $("#FAreaName").find("option:selected").text())
    {
        areaname = $("#FAreaName").find("option:selected").text();
    }
    $("#tbody").empty();
    $.post("/Home/GetSearchInvoices", {
        "startTime": $("#startTime").val(), "endTime": $("#endTime").val(), "areaName": areaname, "FCode": $("#FCode").val(), "FName": $("#FName").val(), "FDate": $("#FDate").val()
        , "managerCardNo": $("#managerCardNo").val(), "FSaleType": $("#FSaleType").find("option:selected").val()
    },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] != "OK") {
                    alert(data);
                    return false;
                }
                var invs = $.parseJSON(words[1]);
                var InvoiceNos = "";
                for (var i = 0; i < invs.length; i++) {
                    var inv = invs[i];
                    var tr = "";
                    if (invs[i].Checkflag == 1) {
                        tr = "<tr><td>" + (i + 1) + "</td><td>" + invs[i].InvoiceNo + "</td><td>" + invs[i].FCriminal + "</td><td>" + invs[i].FCrimeCode + "</td><td>" + invs[i].FAreaName + "</td><td>" + invs[i].RoomNo + "</td><td>" + getLocalTime(invs[i].OrderDate) + "</td><td>" + invs[i].PType + "</td><td>" + invs[i].Amount + "</td><td><input class='btn btn-danger' type='button' value='取消' disabled='disabled' onclick='CancelOrder(\"" + invs[i].InvoiceNo + "\")'></td><td><input class='btn btn-warning' type='button' value='改单' disabled='disabled' onclick='ChangeOrder(\"" + invs[i].InvoiceNo + "\")'></td></tr>";
                    } else {
                        tr = "<tr><td>" + (i + 1) + "</td><td>" + invs[i].InvoiceNo + "</td><td>" + invs[i].FCriminal + "</td><td>" + invs[i].FCrimeCode + "</td><td>" + invs[i].FAreaName + "</td><td>" + invs[i].RoomNo + "</td><td>" + getLocalTime(invs[i].OrderDate) + "</td><td>" + invs[i].PType + "</td><td>" + invs[i].Amount + "</td><td><input class='btn btn-danger' type='button' value='取消' onclick='CancelOrder(\"" + invs[i].InvoiceNo + "\")'></td><td><input class='btn btn-warning' type='button' value='改单' onclick='ChangeOrder(\"" + invs[i].InvoiceNo + "\")'></td></tr>";
                    }

                    $("#tbody").append(tr);
                    if (InvoiceNos == "") {
                        InvoiceNos = inv.InvoiceNo;
                    } else {
                        InvoiceNos = InvoiceNos + "|" + inv.InvoiceNo;
                    }
                }
                $("#InvoiceNos").val(InvoiceNos);
                
            }
        }
    );
}

function printMulXiaoPiao()//打印多个小票
{
    //var s3 = document.getElementById("test");
    //for (var i = 1; i < s3.rows.length; i++) {
    //    var tid = i - 1;
    //    myTestHtml("#temp"+tid);
    //}

    var PrintXPItemS = $("#PrintXPItemS").val();
    var pItems = PrintXPItemS.split("|");
    for (var i = 0; i < pItems.length; i++) {
        var item = pItems[i];
        //myTestHtml("#"+item,$("#xiaoPiaoPageWidth").val());
        printXiaoPiaoXinxi(item, $("#xiaoPiaoPageWidth").val());
    }

    var selectRows = $("#InvoiceNos").val();
    $.post("/Home/UpdatePrintCount", { "Invoices": selectRows }, function (data, status) {
        if ("success" == data) {
            //alert(data);
            $.messager.alert("提示", data);
        }
    });
}

//取消订单
function CancelOrder(invoiceNo) {
    var a = confirm("您确认要取消订单吗？");
    if (a == true) {
        /*document.write("恭喜你答对了！");*/
        $.post("/Home/CancelOrder", {
            "InvoiceNo": invoiceNo
            , "managerCardNo": $("#managerCardNo").val()
        },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                alert(data);
            }
        }
    );
    }
    
}

//修改订单，取消下单回到未下单状态 ChangeOrder
function ChangeOrder(invoiceNo) {
    var a = confirm("您确认要退回去修改订单吗？");
    if (a == true) {
        /*document.write("恭喜你答对了！");*/
        $.post("/Home/ChangeOrder", {
            "InvoiceNo": invoiceNo
            , "managerCardNo": $("#managerCardNo").val()
        },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                alert(data);
            }
        }
    );
    }

}

//消费统计 0按队别,1按号房
function XiaoFeiTongji(roomNoFlag) {
    var areaname = "";
    if ("请选择队别" != $("#tjFAreaName").find("option:selected").text()) {
        areaname = $("#tjFAreaName").find("option:selected").val();
    }
    $("#tjThead").empty();
    var th = "";
    if (areaname == "") {
        th = "<tr><th>序号</th><th>简码</th><th>品名</th><th>用户选择规格</th><th>商品厂家标准化条码</th><th>数量</th><th>金额</th></tr>";
    } else {
        th = "<tr><th>序号</th><th>队别</th><th>房号</th><th>简码</th><th>品名</th><th>规格</th><th>条码</th><th>数量</th><th>金额</th></tr>";
    }
    $("#tjThead").append(th);

    $("#tjTBody").empty();

    $("#tjXiaoTBody").empty();
    if ($("#tjRoomNo").val() != "") {
        roomNoFlag = "1";
    }
    $.post("/Home/XiaoFeiTongji", {
        "tjStartDate": $("#tjStartDate").val(), "tjEndDate": $("#tjEndDate").val(), "tjFAreaName": areaname
         , "roomNoFlag": roomNoFlag, "tjSPShortCode": $("#tjSPShortCode").val(), "managerCardNo": $("#managerCardNo").val()
        , "tjFSaleType": $("#tjFSaleType").find("option:selected").val(), "tjRoomNo": $("#tjRoomNo").val()
    },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {
                    var roomNoFlag = words[1];
                    var invs = $.parseJSON(words[2]);
                    var sumMoney = 0;
                    var roomMoney = 0;
                    var roomNo="";
                    for (var i = 0; i < invs.length; i++) {

                        if (i > 0) {
                            if (roomNo != invs[i].RoomNo) {
                                var groupby = "<tr><td>号房小计：</td><td>" + toDecimal2(roomMoney) + "</td></tr>"
                                $("#tjTBody").append(groupby);

                                var groupby = "<tr><td>号房小计：</td><td>" + toDecimal2(roomMoney) + "</td></tr>"
                                $("#tjXiaoTBody").append(groupby);

                                roomMoney = 0;
                            }
                        }
                        var inv = invs[i];
                        var tr = "";
                        var trXiao = "";
                        if (areaname != "") {//按监区或是号房统计
                            tr = "<tr><td>" + (i + 1) + "</td><td>" + invs[i].FAreaName + "</td><td>" + invs[i].RoomNo + "</td><td>" + invs[i].SPShortCode + "</td><td style='width:100px;'>" + invs[i].GName + "</td><td>" + invs[i].Remark + "</td><td>" + invs[i].GTXM + "</td><td>" + invs[i].FCount + "</td><td>" + invs[i].FMoney + "</td></tr>";
                            trXiao = "<tr><td>" + invs[i].RoomNo + "</td><td>" + invs[i].SPShortCode + "</td><td style='width:100px;'>" + invs[i].GName + "</td><td>" + invs[i].FCount + "</td><td>" + invs[i].FMoney + "</td></tr>";
                        } else {//按商品货号进行统计
                            tr = "<tr><td>" + (i + 1) + "</td><td>" + invs[i].SPShortCode + "</td><td style='width:100px;'>" + invs[i].GName + "</td><td>" + invs[i].Remark + "</td><td>" + invs[i].GTXM + "</td><td style='width:80px;'>" + invs[i].FCount + "</td><td>" + invs[i].FMoney + "</td></tr>";
                            trXiao = "<tr><td>" + invs[i].SPShortCode + "</td><td style='width:100px;'>" + invs[i].GName + "</td><td style='width:80px;'>" + invs[i].FCount + "</td><td>" + invs[i].FMoney + "</td></tr>";
                        }
                        $("#tjTBody").append(tr);
                        $("#tjXiaoTBody").append(trXiao);
                        roomMoney = roomMoney + invs[i].FMoney;
                        roomNo = invs[i].RoomNo;

                        
                        
                        sumMoney = sumMoney + invs[i].FMoney;
                    }
                    var tfoot = "<tr><td>总计：</td><td>" + toDecimal2(sumMoney) + "</td></tr>";
                    $("#tjTBody").append(tfoot);
                    $("#tjXiaoTBody").append(tfoot);
                } else {
                    alert(data);
                }
                

            }
        }
    );
}

//消费明细查询
function SearchXFMingxi() {
    if ("" == $("#mxFSaleType").find("option:selected").val()) {
        alert("请选择一个消费类型");
        return false;
    }
    var areaname = "";
    if ("请选择队别" != $("#mxFAreaName").find("option:selected").text()) {
        areaname = $("#mxFAreaName").find("option:selected").val();
    }
    $("#mxTBody").empty();

    $("#tsInfo").show();
    $.post("/Home/xfMingxiSearch", {
        "mxStartDate": $("#mxStartDate").val()
        , "mxEndDate": $("#mxEndDate").val()
        , "mxFAreaName": areaname
         , "mxFGoodsType": $("#mxFGoodsType").find("option:selected").val()
        , "mxSPShortCode": $("#mxSPShortCode").val()
        , "managerCardNo": $("#managerCardNo").val()
        , "mxFSaleType": $("#mxFSaleType").find("option:selected").val()
        ,"mxFCrimeCode":$("#mxFCrimeCode").val()
    },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {
                    var invs = $.parseJSON(words[1]);
                    var sumMoney = 0;
                    for (var i = 0; i < invs.length; i++) {

                        var inv = invs[i];
                        var tr = "<tr><td>" + (i + 1) + "</td><td>" + invs[i].FAreaName + "</td><td>" + invs[i].RoomNo + "</td><td>" + invs[i].FCriminal + "</td><td>" + invs[i].SPShortCode + "</td><td style='width:100px;'>" + invs[i].GName + "</td><td>" + invs[i].Remark + "</td><td>" + invs[i].Qty + "</td><td>" + invs[i].Amount + "</td></tr>";
                        $("#mxTBody").append(tr);
                        sumMoney = sumMoney + invs[i].Amount;
                    }
                    var tfoot = "<tr><td>总计：</td><td>" + toDecimal2(sumMoney) + "</td></tr>";
                    $("#mxTBody").append(tfoot);
                } else {
                    alert(data);
                }
                $("#tsInfo").hide();
            }
        }
    );
}

//消费明细查询
function SearchXFMingxiExcelOut() {
    if ("" == $("#mxFSaleType").find("option:selected").val()) {
        alert("请选择一个消费类型");
        return false;
    }
    var areaname = "";
    if ("请选择队别" != $("#mxFAreaName").find("option:selected").text()) {
        areaname = $("#mxFAreaName").find("option:selected").val();
    }
    $("#mxTBody").empty();

    $("#tsInfo").show();
    $.post("/Home/SearchXFMingxiExcelOut", {
        "mxStartDate": $("#mxStartDate").val()
        , "mxEndDate": $("#mxEndDate").val()
        , "mxFAreaName": areaname
         , "mxFGoodsType": $("#mxFGoodsType").find("option:selected").val()
        , "mxSPShortCode": $("#mxSPShortCode").val()
        , "managerCardNo": $("#managerCardNo").val()
        , "mxFSaleType": $("#mxFSaleType").find("option:selected").val()
        , "mxFCrimeCode": $("#mxFCrimeCode").val()
    },
        function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {
                    window.open("/Upload/" + words[1]);
                } else {
                    alert(data);
                }
                $("#tsInfo").hide();
            }
        }
    );
}