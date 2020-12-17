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
    $.post("/SelfDongJie/QueryUserInfo", { "FCardCode": cardNo, "checkFlag": "1", "FManagerCard": $("#FManagerCard").val(), "UserName": $("#UserName").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                $("#lblInfo").html(data);
                $("#userNameInfo").html("对不起，您所持的不是有效的IC消费卡，请与管教联系！");
                $("#btnSubmitKK").attr("disabled", "disabled");//禁用扣款提交按钮
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
        //if (rts.UserInfo.BankCardNo.length >=19) {
        //    bankcardno = rts.UserInfo.BankCardNo.substr(0, 4) + " " + rts.UserInfo.BankCardNo.substr(4, 4) + " " + rts.UserInfo.BankCardNo.substr(8, 4) + " " + rts.UserInfo.BankCardNo.substr(12, 4) + " " + rts.UserInfo.BankCardNo.substr(16,10);
        //}
        $("#lblInfo").html("旧烛光卡号：" + BankCardFormat(rts.UserInfo.BankCardNo) + "<br/> 中银结算卡号：" + BankCardFormat(rts.UserInfo.SecondaryBankCard));
        $("#curMonthXFE").html(rts.UserInfo.Xiaofeimoney);
        $("#okUseAllMoney").html(rts.UserInfo.OkUseAllMoney);
        $("#xiaofeiYuE").html(rts.UserInfo.NoXiaofeimoney);
        $("#amountC").html(rts.UserInfo.AmountCmoney);
        $("#amountB").html(rts.UserInfo.AmountBmoney);
        $("#amountA").html(rts.UserInfo.AmountAmoney);
        $("#dongJieMoney").html(rts.UserInfo.flimitamt);
        
        $("#FCrimeCode").val(rts.UserInfo.FCode);
        $("#FIcCardCode").val(rts.UserInfo.CardCode);
        $("#btnSubmitKK").removeAttr("disabled");//启用扣款提交按钮
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