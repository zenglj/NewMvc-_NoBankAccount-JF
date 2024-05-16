//读取IC卡号
function readCardNo() {
    //$("#lblInfo").html("0000000");
    var cardNo = "";
    Connect();//连接读卡器
    //$("#lblInfo").html("连接成功");
    var xunka = Scard();//寻卡
    if (xunka != "") {
        //$("#lblInfo").html(xunka);
        var rlk = ReaderLoadKey();//装载密码
        if (rlk == "1") {
            //$("#lblInfo").html("装载密码成功");
            var athk = authenticationKey();//验证密码
            if (athk == "1") {
                // $("#lblInfo").html("验证密码成功");
                cardNo = readDataHex();//以16进制的方式读卡
                if (cardNo != "") {
                    //$("#lblInfo").html("读卡完成");
                }
            }
        }
    }
    //$("#lblInfo").html(xunka);
    //alert(cardNo);
    exit();//断开读卡器连接
    return cardNo;
}

 
//获取用户IC卡信息
function getUserCardNo(saleSort) {
    var getUserCardNo = "";
    var userCode = $("#UserCode").val();
    var userPwd = $("#UserPwd").val();
    $.post("/JfShopping/GetCardInfo", { "userCode": userCode, "userPwd": userPwd }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "OK") {
                var cardNo = words[1];

                $("#lblInfo").html(cardNo);
                $.post("/JfShopping/AddOrder", { "FCardCode": cardNo, "checkFlag": "1", "saleTypeId": $("#saleTypeId").val() }, function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        var words = data.split("|")
                        if (words[0] == "Error") {
                            $("#orderNumber").val("");
                            $("#tbodyOrderList").empty();
                            $("#myPromptBoxInfo").html(data);
                            $("#myPromptBox").modal("show");
                        } else if (words[0] == "There") {
                            $("#currUserCardNo").val(cardNo);
                            $("#currOrderInfo").val(words[1]);
                            $("#currSaleSort").val(saleSort);
                            $('#askOrderHandle').modal('show');
                        }
                        else {
                            SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
                        }
                        if (saleSort != "selfOrder") {
                            $("#inputGtxm").focus();
                        }
                    }
                });

            } else {
                alert(data);
            }
            
        }
    });
}


//新增一个订单
function btnAddOrder(e, saleSort) {
    var cardNo = "";
    if ($("#loginModeFlag").val() == "1") {
        cardNo = getUserCardNo(saleSort);

    }
    else {
        cardNo = readCardNo();

        $("#lblInfo").html(cardNo);
        $.post("/JfShopping/AddOrder", { "FCardCode": cardNo, "checkFlag": "1", "saleTypeId": $("#saleTypeId").val() }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|")
                if (words[0] == "Error") {
                    $("#orderNumber").val("");
                    $("#tbodyOrderList").empty();
                    $("#myPromptBoxInfo").html(data);
                    $("#myPromptBox").modal("show");
                }
                else if (words[0] == "There") {
                    $("#currUserCardNo").val(cardNo);
                    $("#currOrderInfo").val(words[1]);
                    $("#currSaleSort").val(saleSort);
                    $('#askOrderHandle').modal('show');
                }
                else {
                    SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
                }
                if (saleSort != "selfOrder") {
                    $("#inputGtxm").focus();
                }
            }
        });

    }
    

}



//取消当前订单信息，新一个信订单
function CancelCurrOrderInfo() {
    //alert("取消");  
    var saleSort = $("#currSaleSort").val();
    var cardNo = $("#currUserCardNo").val();
    $.post("/JfShopping/AddOrder", { "FCardCode": cardNo, "checkFlag": "0", "saleTypeId": $("#saleTypeId").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                //alert(data);
                //$("#lblInfo").html(data);
                $("#myPromptBoxInfo").html(data);
                $("#myPromptBox").modal("show");
            } else {
                SetAndDisplayCriminalInfo(words[1]);//显示抬头栏的账户余额信息
            }
        }
    });

    if (saleSort != "selfOrder") {
        $("#inputGtxm").focus();
    }
}

//延续未提交订单信息
function ContinueOrderInfo() {
    //alert（"确定"）    
    var wordsInfo=$("#currOrderInfo").val();
    var saleSort = $("#currSaleSort").val();
    //console.info("载入:"+saleSort);
    SetAndDisplayCriminalInfo(wordsInfo, saleSort);//显示抬头栏的账户余额信息
    $('#askOrderHandle').modal('hide');

    if (saleSort != "selfOrder") {
        $("#inputGtxm").focus();
    }
}

//显示抬头栏的账户余额信息
function SetAndDisplayCriminalInfo(words, saleSort) {
    var rts = $.parseJSON(words);
    $("#orderFcrimeCode").val(rts.FCrimeCode);
    
    $("#orderFcrimeName").val(rts.FName);
    $("#orderCyName").val(rts.cyName);
    $("#orderFareaName").val(rts.FAreaName);
    $("#orderNumber").val(rts.orderId);

    $("#criminalCyInfo").html("您好，" + rts.FName + "<span class='label label-default'>" + rts.cyName + "</span>,可消费积分：<span class='label label-default' id='xiaofeiYuE'>" + rts.AccPoints + "</span>,账户总积分：<span class='label label-default' id='okUseAllMoney'>" + rts.AccPoints + "</span>,本月已经消费：<span class='label label-warning' id='curMonthXFE'>" + rts.XiaoFeiPoints + "</span>");
    
    $("#orderMoney").html(toDecimal2(rts.orderMoney));//订单金额    
    $("#submitMoney").html(toDecimal2(rts.orderMoney));//结算显示金额
    $("#curMonthXFE").html(toDecimal2(rts.XiaoFeiPoints));//本月已经消金额
    $("#tbodyOrderList").empty();
    $("#tbodyJieShuanList").empty();
    if (rts.lists !== null) {
        var details = rts.lists;
        if (details.length > 0) {
            for (var i = 0; i < details.length; i++) {
                var detail = details[i];
                var checkflag = "";

                if (detail.FreeFlag == "1") {
                    checkflag = "checked='checked'"
                }

                if ("selfOrder" == saleSort) {
                    option = "<tr><td><input type='checkbox'" + checkflag + "/></td><td>" + detail.SPShortCode + "</td><td>" + detail.GName + "</td><td>" + detail.GCount + "</td><td>" + detail.GAmount + "</td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + detail.ID + ",\"" + saleSort + "\")'>删除</a></td></tr>";
                } else {
                    option = "<tr><td>" + detail.SPShortCode + "</td><td>" + detail.GName + "</td><td>" + detail.SPShortCode + "</td><td>" + detail.GPrice + "</td><td>" + detail.GCount + "</td><td>" + detail.GAmount + "</td><td><input type='checkbox'" + checkflag + "/></td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + detail.ID + ")'>删除</a></td></tr>";
                }                
                $("#tbodyOrderList").append(option);
                optionJS = "<tr><td><img src='" + detail.src + "' style='height:40px;' /></td><td>" + detail.SPShortCode + "</td><td>" + detail.GName + "</td><td>" + detail.GTXM + "</td><td>" + detail.GCount + "</td><td>" + detail.GAmount + "</td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + detail.ID + ")'>删除</a></td></tr>";
                $("#tbodyJieShuanList").append(optionJS);
            }
        }
    }
}

//删除订单中的一条商品
function btnDelDetailById(detailId, saleSort) {
    $('#myOrderList').modal('hide');
    //alert(detailId);
    var orderId = $("#orderNumber").val();
    $.post("/JfShopping/DelOrderDetail", { "OrderId": orderId, "Id": detailId }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            //alert(data);
            if (words[0] == "Error") {
                //alert(data);
            } else {
                //删除一条记细记录
                
                SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
                //更新当前余额

                //$('#myOrderList').modal('show');
            }
        }
    });
}


//显示结算单框
function DisplayPayModel(mode) {
    $("#userRoom").show();
    $("#btnPayCheckSubmit").removeAttr("disabled");//显示结算提交按钮
    $("#submitOrderNumber").html($("#orderNumber").val());
    $("#submitFCrimeName").html($("#orderFcrimeName").val());
    $("#submitAreaName").html($("#orderFareaName").val());
    $("#submitMoney").html($("#orderMoney").html());
    $("#userRoomNO").val('');
    //加载消费明细列表
    //tbodyJieShuanList
    var option;
    $("#tbodyJieShuanList").empty();
    $.post("/JfShopping/GetOrderDetails", { "orderId": $("#orderNumber").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                var infos = $.parseJSON(words[1]);
                for (var i = 0; i < infos.length; i++) {
                    var detail = infos[i];
                    option = "<tr><td><img src='" + detail.src + "' style='height:40px;' /></td><td>" + detail.SPShortCode + "</td><td>" + detail.GName + "</td><td>" + detail.GTXM + "</td><td>" + detail.GCount + "</td><td>" + detail.GAmount + "</td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + detail.ID + ")'>删除</a></td></tr>";
                    $("#tbodyJieShuanList").append(option);
                }


            }
        }
    });




    $("#Head_Top").hide();
    window.location.href = "#userRoom";
    $("#userRoomNO").focus();
    //if (mode == 'sale') {
    //    $("#Head_Top").hide();
        
    //    window.location.href = "#userRoom";
    //    $("#userRoomNO").focus();
    //} else {
    //    $('#myOrderInfo').modal('show');
    //}
}

//结算提交
function PayCheckSubmit() {
    if ($("#userRoomNO").val()=="") {
        alert("请输入收货房间号");
        $("#userRoomNO").focus();
        return false;
    }
    var rno = $("#userRoomNO").val();

    if (parseInt(rno) < 0 || parseInt(rno) > 999) {
        alert("请输入正确的房间号");
        $("#userRoomNO").focus();
        return false;
    }

    var submitMoney = $("#submitMoney").html();
    if (parseInt(submitMoney) <= 0) {
        alert("该订单金额为0不能提交");
        return false;
    }

    $("#btnPayCheckSubmit").attr("disabled", "disabled");//禁用结算提交按钮
    $('#myOrderInfo').modal('hide');
    var fcrimecode = $("#orderFcrimeCode").val();
    var printFlag = "";
    $.post("/JfShopping/GetPrintXiaoPiaoFlag", { "OrderId": "" }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            if (data == "1") {
                printFlag = data;
            }
        }
    });
    $.post("/JfShopping/SubmitOrder", { "OrderId": $("#orderNumber").val(), "FCrimeCode": fcrimecode, "userRoomNo": $("#userRoomNO").val(), "crtby": "crtby" }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                $("#orderNumber").val('');
                $("#orderMoney").html('');
                $("#orderFcrimeName").val('');
                $("#orderFareaName").val('');
                if (printFlag == "1")
                {
                    var inv = $.parseJSON(words[2]);
                    SetPrintPayXiaopiao(words[2]);
                    //myTestHtml("#template");
                    var PrintXPItemS = $("#PrintXPItemS").val();
                    var pItems = PrintXPItemS.split("|");
                    for (var i = 0; i < pItems.length; i++) {
                        var item = pItems[i];
                        /*myTestHtml("#" + item, $("#xiaoPiaoPageWidth").val());*/

                        printXiaoPiaoXinxi(item, $("#xiaoPiaoPageWidth").val());
                        $.post("/Home/UpdatePrintCount", { "Invoices": inv.invoice.InvoiceNo }, function (data, status) {
                            if ("success" == data) {
                                //alert(data);
                                //$.messager.alert("提示", data);
                            }
                        });
                    }
                }                
            }
            //alert(data);
            $("#myPromptBoxInfo").html(words[1]);
            $("#myPromptBox").modal("show");
            goSaleHead();
        }
    });
}

//房间号录入检测验证
function roomNoCheckInput(keyCode,id) {
    if (keyCode == 13) {
        PayCheckSubmit();
    } else {
        checkRate(id)
    }
        
}

//检测并增加一条购买商品记录
function CheckAndAddBuyList(saleSort) {
    if (parseFloat($("#inputGcount").val()) > parseFloat($("#goodStockNumber").html())) {
        //alert("对不起，库存量不足!");
        $("#myPromptBoxInfo").html("对不起，库存量不足!");
        $("#myPromptBox").modal("show");
        return false;
    }
    var iorderMoney = $("#orderMoney").html();
    var inum = $("#inputGcount").val();
    var iName = $("#inputGname").html();
    var iGtxm = $("#inputGtxm").html();
    var iprice = $("#inputPrice").html();
    var imoney = toDecimal2(parseFloat(iprice) * parseFloat(inum));//转成两位小数点

    var iallMoney =parseFloat( imoney) + parseFloat(iorderMoney);
    var xiaofeiYue = $("#xiaofeiYuE").html();
    var okUseAllMoney = $("#okUseAllMoney").html();
    var nowYue = parseFloat(xiaofeiYue) - imoney;
    if ($("#strGoodFreeFlag").val() == "1") {
        if ((parseFloat(okUseAllMoney) - iallMoney) < 0) {
            //alert("对不起，您的账户总余额不足");
            $("#myPromptBoxInfo").html("对不起，您的账户总余额不足");
            $("#myPromptBox").modal("show");
            return false;
        } else {
            //提交到后台执行插入购买记录操作
            DoInsertBuyDetail(iallMoney, nowYue, saleSort);
        }
    } else {
        if (nowYue < 0) {//判断金额是否够扣
            //alert("对不起，您的可消费金额不足");
            $("#myPromptBoxInfo").html("对不起，您的可消费金额不足");
            $("#myPromptBox").modal("show");
            return false;
        } else {
            //提交到后台执行插入购买记录操作
            DoInsertBuyDetail(iallMoney, nowYue, saleSort);
        }
    }
    
}

//提交到后台执行插入购买记录操作DoInsertBuyDetail(iallMoney, nowYue)
function DoInsertBuyDetail(iallMoney, nowYue, saleSort) {
    var gtxm = "";
    var gRemark = "";
    if ("selfOrder" == saleSort) {
        gtxm = $("#inputGtxm").html();
        gRemark = $("#goodRemark").html();
    } else {
        gtxm = $("#inputGtxm").val();
        gRemark = $("#goodRemark").val();
    }

    $.post("/JfShopping/AddOrderDetail", { "OrderId": $("#orderNumber").val(), "GTXM": gtxm, "GCount": $("#inputGcount").val(), "goodRemark": gRemark }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if ("OK" == words[0]) {
                $("#orderMoney").html(toDecimal2(iallMoney));
                if ($("#strGoodFreeFlag").val() != "1") {
                    $("#xiaofeiYuE").html(toDecimal2(nowYue));
                }
                var inum = $("#inputGcount").val();
                var iprice = $("#inputPrice").html();
                if ("selfOrder" == saleSort) {                    
                    var iName = $("#inputGname").html();
                    var iGtxm = $("#inputGtxm").html();
                    
                } else {
                    var iName = $("#inputGname").val();
                    var iGtxm = $("#inputGtxm").val();                    
                }
                
                var checkflag = "";
                if ($("#strGoodFreeFlag").val() == "1") {
                    checkflag = "checked='checked'"
                }
                var imoney = toDecimal2(parseFloat(iprice) * parseFloat(inum));
                
                //console.info("插入:" + saleSort);
                //option = "<tr><td>" + iGtxm + "</td><td>" + iName + "</td><td>" + iprice + "</td><td>" + inum + "</td><td>" + imoney + "</td><td><input type='checkbox'" + checkflag + "/></td><td><a class='btn btn-default btn-sm' href='#' role='button' " + "onclick=\'btnDelDetailById(" + words[1] + ")\'" + ">删除</a></td></tr>";
                if ("selfOrder" == saleSort) {
                    option = "<tr><td><input type='checkbox'/></td><td>" + iGtxm + "</td><td>" + iName + "</td><td>" + inum + "</td><td>" + imoney + "</td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + words[1] +",\""+saleSort+"\")'>删除</a></td></tr>";
                } else {
                    var infos = $.parseJSON(words[4]);
                    option = "<tr><td>" + infos.SPShortCode + "</td><td>" + iName + "</td><td>" + infos.GTXM + "</td><td>" + iprice + "</td><td>" + inum + "</td><td>" + imoney + "</td><td><input type='checkbox'" + checkflag + "/></td><td><a class='btn btn-default btn-sm' href='#' role='button' onclick='btnDelDetailById(" + words[1] + ")'>删除</a></td></tr>";
                }
                var t01 = $("#tbodyOrderList tr").length;
                if (t01 > 0) {
                    var $tr = $("#tbodyOrderList tr").eq(0);
                    if ($tr.size() == 0) {
                        alert("当前行数为0，无法插入");
                    } else {
                        $tr.before(option);
                    }
                }
                else {
                    $("#tbodyOrderList").append(option);
                }
                

                

                //$("#tbodyOrderList").append(option);
                $("#inputGcount").val('');
                //$("#inputGtxm").focus();
                
                if ("selfOrder" == saleSort) {
                    $("#inputGtxm").html('');
                    $("#inputGname").html('');
                    $("#goodInfoView").show();
                    $("#goodDetail").hide();
                    
                    //window.location.href("#topInfo");
                    window.location.href=("#topInfo");
                } else {
                    $("#inputGtxm").val('');
                    $("#inputGname").val('');
                    $("#inputGtxm").focus();
                }
                
                
            } else {
                //alert(data);
                $("#myPromptBoxInfo").html(data);
                $("#myPromptBox").modal("show");
            }
        }
    });
}


//设定填写结算小票的内容
function SetPrintPayXiaopiao(words) {
    var inv = $.parseJSON(words);
   

    $("#template").empty();//清空
    $("#PrintXPItemS").val('');
    var PrintXPItemS = "";
    var k = 0;
    //如果长度大于22行
    if (inv.details.length > 20) {
        //前22行=====================
        if (PrintXPItemS == "") {
            PrintXPItemS = "temp" + k;
        } else {
            PrintXPItemS = PrintXPItemS + "|temp" + k;
        }
        var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:left;font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 3) + "px;'>消费一卡通系统</div>"
        + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px; width:200px;'>"
        + "<tbody>"
        + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td></tr>"
        + "<tr><td>日期：" + getLongTime(inv.invoice.OrderDate) + "</td></tr>"
        + "<tr><td>编号：" + inv.invoice.FCrimeCode + "</td></tr>"
        + "<tr><td>姓名：" + inv.invoice.FCriminal + "</td></tr>"
        + "<tr><td colspan='2'>队别：" + inv.invoice.FAreaName + "（房号:" + inv.invoice.RoomNo + "）</td></tr>"
        + "<tr><td>消费时首次打印</td></tr>"
        + "</tbody>"
        + "</table>"
        + "<hr />"
        + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
        + "<thead>"
        + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:30px;'>金额</th></tr>"
        + "</thead>"
        + "<tbody>";

        for (var j = 0; j < 20; j++) {
            var remark = "";
            if (inv.details[j].Remark != "") {
                remark = "【" + inv.details[j].Remark + "】";
            }
            xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].GTXM + "</td><td align='center'>" + inv.details[j].GDJ + "</td><td align='center'>" + inv.details[j].QTY + "</td><td align='right'>" + inv.details[j].AMOUNT + "</td></tr>"
            + "<tr><td colspan='4' style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + remark + "</td></tr>"

        }

        xiaopiao = xiaopiao + "</tbody>"
        + "</table>"
        + "<div>（这里第1页），下面还有内容</div>"
        + "</div><br/></div>";

        $("#template").append(xiaopiao);

        k++;
        //后20行=====================

        if (PrintXPItemS == "") {
            PrintXPItemS = "temp" + k;
        } else {
            PrintXPItemS = PrintXPItemS + "|temp" + k;
        }
        var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:center'>" + inv.invoice.InvoiceNo + " (第2页)</div>"
        + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
        + "<thead>"
        + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:40px;'>金额</th></tr>"
        + "</thead>"
        + "<tbody>";

        for (var j = 20; j < inv.details.length; j++) {
            var remark = "";
            if (inv.details[j].Remark != "") {
                remark = "【" + inv.details[j].Remark + "】";
            }
            xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].GTXM + "</td><td align='center'>" + inv.details[j].GDJ + "</td><td align='center'>" + inv.details[j].QTY + "</td><td align='right'>" + inv.details[j].AMOUNT + "</td></tr>"
            + "<tr><td colspan='4' style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + remark + "</td></tr>"

        }

        xiaopiao = xiaopiao + "</tbody>"
        + "</table>"
        + "<hr />"
        + "<span>消费合计：" + inv.invoice.Amount + "分</span><hr/>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>存款账户余额:" + inv.criminal.AmountA + "元</div>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>报酬账户余额:" + inv.criminal.AmountB + "元</div>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>留存账户余额:" + inv.criminal.AmountC + "元</div>"
            + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>账户总积分:" + toDecimal2(inv.criminal.AccPoints) + "分</div>"
        + "</div><br/></div>";

        $("#template").append(xiaopiao);
        k++;
        //======结束========================

    } else {

        if (PrintXPItemS == "") {
            PrintXPItemS = "temp" + k;
        } else {
            PrintXPItemS = PrintXPItemS + "|temp" + k;
        }
        var xiaopiao = "<div id='temp" + k + "'><div> <div style='text-align:left;font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 3) + "px;'>监狱一卡通系统</div>"
        + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px; width:200px;'>"
        + "<tbody>"
        + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td></tr>"
        + "<tr><td>日期：" + getLongTime(inv.invoice.OrderDate) + "</td></tr>"
        + "<tr><td>编号：" + inv.invoice.FCrimeCode + "</td></tr>"
        + "<tr><td>姓名：" + inv.invoice.FCriminal + "</td></tr>"
        + "<tr><td colspan='2'>队别：" + inv.invoice.FAreaName + "（房号:" + inv.invoice.RoomNo + "）</td></tr>"
        + "<tr><td>消费时首次打印</td></tr>"
        + "</tbody>"
        + "</table>"
        + "<hr />"
        + "<table style='font-size:" + $("#xiaoPiaoFontSize").val() + "px;'>"
        + "<thead>"
        + "<tr><th>品名</th><th style='width:30px;'>单价</th><th style='width:30px;'>数量</th><th style='width:40px;'>金额</th></tr>"
        + "</thead>"
        + "<tbody>";

        for (var j = 0; j < inv.details.length; j++) {
            var remark = "";
            if (inv.details[j].Remark != "") {
                remark = "【" + inv.details[j].Remark + "】";
            }
            xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].GTXM + "</td><td align='center' style='width:30px;'>" + inv.details[j].GDJ + "</td><td align='center' style='width:30px;'>" + inv.details[j].QTY + "</td><td align='right' style='width:30px;'>" + inv.details[j].AMOUNT + "</td></tr>"
            + "<tr><td colspan='4' style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + remark + "</td></tr>"

        }

        xiaopiao = xiaopiao + "</tbody>"
        + "</table>"
        + "<hr />"
        + "<span>消费合计：" + inv.invoice.Amount + "分</span><hr/>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>存款账户余额:" + inv.criminal.AmountA + "元</div>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>报酬账户余额:" + inv.criminal.AmountB + "元</div>"
        //+ "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>留存账户余额:" + inv.criminal.AmountC + "元</div>"
            + "<div style='font-size:" + (parseInt($("#xiaoPiaoFontSize").val()) + 1) + "px;'>账户总积分:" + toDecimal2(inv.criminal.AccPoints ) + "分</div>"
        + "</div><br/></div>";

        $("#template").append(xiaopiao);
        k++;
    }
    $("#PrintXPItemS").val(PrintXPItemS);
}

//返回销售
function goSaleHead() {    
    $("#userRoom").hide();
    $("#Head_Top").show();
    window.location.href = "#Head_Top";
}



//输入房间号
function btnEntRoomNo(e, g) {
    if (e == "确认") {
        $('#btnPayCheckSubmit').focus();
    } else if (e == "清空") {
        $("#userRoomNO").val("");
    } else if (e == "删除") {
        if ($("#userRoomNO").val() != "") {
            var roomLength = $("#userRoomNO").val().length;
            $("#userRoomNO").val($("#userRoomNO").val().substr(0, roomLength - 1));
        }
    } else if (e == "/") {
        //无
    } else {
        $("#userRoomNO").val($("#userRoomNO").val() + e);
    }


}

//=========================明华读卡器=========================================

