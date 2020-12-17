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

//查询用户信息
function btnQueryUserInfo(e, saleSort) {
    var cardNo = readCardNo();
    //alert(cardNo);
    //alert("e3232");
    $("#lblInfo").html(cardNo);
    $.post("/SelfDongJie/QueryUserPayeeAccount", { "FCardCode": cardNo, "checkFlag": "1", "FManagerCard": $("#FManagerCard").val(), "UserName": $("#UserName").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                $("#lblInfo").html(data);
                $("#userNameInfo").html("对不起，您所持的不是有效的IC消费卡，请与管教联系！");
                $("#btnNext").attr("disabled", "disabled");//启用按钮
                $("#inputOutBankCard").attr("disabled", "disabled");
                $("#inputBankUserName").attr("disabled", "disabled");
                $("#inputOpeningBank").attr("disabled", "disabled");
                $("#inputOutBankRemark").attr("disabled", "disabled");
                ClearUserDisplayInfo();
            } else if (words[0] == "There") {
                SetAndDisplayUserInfo(words[1]);
                $("#KKMoney").focus();
            }
            else {
                //SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
            }
        }
    });
}

function BtnFirst() {
    $("#divFirstArea").show();
    $("#divNextArea").hide();
}
function BtnNext() {
    if ($("#FCrimeCode").val().length != 10) {
        alert("请先读服刑人员管理卡，才能确认人员身份");
        return false;
    }
    if ($("#inputOutBankCard").val().length != 19) {
        alert("银行卡号必须是19位的");
        return false;
    }
    if ($("#inputBankUserName").val().length <2) {
        alert("姓名长度不足");
        return false;
    }
    if ($("#inputOpeningBank").val().length < 6) {
        alert("开户行长度不足");
        return false;
    }
    var s = BankCardFormat($("#inputOutBankCard").val());
    $("#displayAccount").html("收款 账 号:<u><strong>" + s + "</strong></u>");
    $("#displayPayeeName").html("收款人姓名:<u><strong>" + $("#inputBankUserName").val() + "</strong></u>");
    $("#displayOpeningBank").html("开户行名称:<u><strong>" + $("#inputOpeningBank").val() + "</strong></u>");
    $("#displayOutBankRemark").html("与收款人关系:<strong><u>" + $("#inputOutBankRemark").val() + "</strong></u>");

    $("#divFirstArea").hide();
    $("#divNextArea").show();
}

function BtnSubmit() {

    var strJson = "";
    $("#divFirstArea div input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    $.post("/SelfDongJie/SavePayeeAccount", { "SettleMode": $("#SettleMode").val(), "OutBankRemark": $("#inputOutBankRemark").val(), "FManagerCard": $("#FManagerCard").val(), "FCrimeCode": $("#FCrimeCode").val(), "OutBankCard": $("#inputOutBankCard").val(), "BankUserName": $("#inputBankUserName").val(), "OpeningBank": $("#inputOpeningBank").val() }, function (data, status) {
        if ("success" == status) {
            alert(data);
        }
    });
    
}
function BankCardFormat(cardno) {
    var s = "";
    for (var i = 0; i <= cardno.length; i+=4) {
        s += cardno.substr(i, 4)+" ";
    }
    return s;
}

function settleMode(mode) {
    if (mode == 'tran') {
        $("#divCashArea").hide();
        $("#divTranArea").show();        
        $("#chkCash").prop("checked", false);
        $("#chkTran").prop("checked", true);
        $("#SettleMode").val("0");
     } else if (mode == 'cash') {
        $("#divCashArea").show();
        $("#divTranArea").hide();
        $("#chkCash").prop("checked", true);
        $("#chkTran").prop("checked", false);
        $("#SettleMode").val("1");
    }
}

//显示用户相关信息
function SetAndDisplayUserInfo(words) {
    var rts = $.parseJSON(words);
    $("#tbodyOrderList").empty();
    if (rts.UserInfo == null) {
        //$("#lblInfo").html("银行卡号：" + rts.UserInfo.BankCardNo);
        ClearUserDisplayInfo();
    } else {
        var info = "你好, " + rts.UserInfo.FName + "<span class='label label-primary'>" + rts.UserInfo.CyName + "(" + rts.UserInfo.MonthStandard + ")" + "</span>,所在队别：<span class='label label-default'>" + rts.UserInfo.FAreaName + "</span>,您的账户信息如下：";
        $("#userNameInfo").html(info);
        var bankcardno = "";
        if (rts.UserInfo.BankCardNo.length >=19) {
            bankcardno = rts.UserInfo.BankCardNo.substr(0, 4) + " " + rts.UserInfo.BankCardNo.substr(4, 4) + " " + rts.UserInfo.BankCardNo.substr(8, 4) + " " + rts.UserInfo.BankCardNo.substr(12, 4) + " " + rts.UserInfo.BankCardNo.substr(16,10);
        }

        $("#FCrimeCode").val(rts.UserInfo.FCode);
        $("#FIcCardCode").val(rts.UserInfo.CardCode);
        $("#btnNext").removeAttr("disabled");//启用按钮

        if (rts.outbankcard != null) {
            $("#inputOutBankCard").val(rts.outbankcard.OutBankCard);
            $("#inputBankUserName").val(rts.outbankcard.BankUserName);
            $("#inputOpeningBank").val(rts.outbankcard.OpeningBank);
            $("#inputOutBankRemark").val(rts.outbankcard.OutBankRemark);
        }       

        $("#inputOutBankCard").removeAttr("disabled");
        $("#inputBankUserName").removeAttr("disabled");
        $("#inputOpeningBank").removeAttr("disabled");
        $("#inputOutBankRemark").removeAttr("disabled");

        if (rts.UserInfo.SettleMode == 1) {
            $("#chkCash").prop("checked", true);
            $("#chkTran").prop("checked", false);
            $("#divCashArea").show();
            $("#divTranArea").hide();
        } else {
            $("#chkCash").prop("checked",false);
            $("#chkTran").prop("checked", true);
            $("#divCashArea").hide();
            $("#divTranArea").show();
            $("#divFirstArea").show();
            $("#divNextArea").hide();
            
        }
    }
}

function ClearUserDisplayInfo() {
    
    $("#okUseAllMoney").html("0.00");
    $("#xiaofeiYuE").html("0.00");
    $("#amountC").html("0.00");
    $("#amountB").html("0.00");
    $("#amountA").html("0.00");
    $("#tbodyOrderList").empty();
}

$(document).ready(function () {

    $("#div_SaleInputKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });

});