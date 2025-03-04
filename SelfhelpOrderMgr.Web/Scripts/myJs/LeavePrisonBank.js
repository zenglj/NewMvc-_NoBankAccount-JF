

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
    $.post("/LeavePrisonBank/CardLogin", { "FManagerCard": cardNo }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            if (data.Flag == true) {
                $("#FManagerCard").val(cardNo);
            }
            else {
                $("#FManagerCard").val("");
            }

            alert(data.ReMsg);
        }
    });
}



//账户密码方式查询
function UserLoginCheck() {

    //var cardNo = readCardNo();
    $.post("/LeavePrisonBank/UserPwdLogin", { "fname": $("#userName").val(), "pwd": $("#userPwd").val() }, function (data, status) {
        if ("success" == status) {
            console.log(data);
            $("#selRecvAccount").html("<option value='' selected='selected'>请选择收款账号</option>");

            if (data.Flag == false) {
                ClearDisplayInfo();
                alert(data.ReMsg);
            } else {
                SetDispUserInfo(data);
                if (data.DataInfo.criminal.fflag == 1) {
                    //===========设置结算显示区域的信息 start=============
                    $("#JieSuanUserCode").html(data.DataInfo.criminal.FCode);
                    $("#JieSuanUserName").html(data.DataInfo.criminal.FName);
                    if (data.DataInfo.paymentRecord == null) {
                        $("#JieSuanPayMode").html("现金结算");
                        $("#JieSuanPayPwd").html("");
                        $("#JieSuanMoney").html("");
                        $("#JieSuanQianMing").attr('src', "");
                    }
                    else {
                        if (data.DataInfo.paymentRecord.PayMode == 0) {
                            $("#JieSuanPayMode").html("现金结算");
                        } else if (data.DataInfo.paymentRecord.TranMoney) {
                            $("#JieSuanPayMode").html("ATM机取款");
                        } else {
                            $("#JieSuanPayMode").html("转账支付");
                        }

                        $("#JieSuanPayPwd").html(data.DataInfo.paymentRecord.WithdrawalPassword);
                        $("#JieSuanMoney").html(data.DataInfo.paymentRecord.TranMoney);
                        $("#JieSuanQianMing").attr('src', data.DataInfo.recvBankAccount.QianMing);
                    //===========设置结算显示区域的信息 End=============
                    }
                    

                    $("#divJieSuanResult").show();
                    $("#divPaySelArea").hide();
                }
                else {

                    $("#divJieSuanResult").hide();
                    $("#divPaySelArea").show();

                    if (data.DataInfo.recvBankAccount != null) {
                        if (data.DataInfo.jieqingFlag == false) {
                            alert("您的账户尚有未结清的记录，请与供应站或相关管理科室联系");
                            return false;
                        }
                        $("#selRecvAccount").append("<option value='" + data.DataInfo.recvBankAccount.Id + "'>" + data.DataInfo.recvBankAccount.BankUserName + "(" + data.DataInfo.recvBankAccount.OutBankRemark + ")" + data.DataInfo.recvBankAccount.OutBankCard + "</option>");
                        $("#selPayMode").val(data.DataInfo.recvBankAccount.PayMode);
                        $("#BankUserName").html(data.DataInfo.recvBankAccount.BankUserName);
                        $("#OutBankRemark").html(data.DataInfo.recvBankAccount.OutBankRemark);
                        $("#OpeningBank").html(data.DataInfo.recvBankAccount.OpeningBank);
                        $("#OutBankCard").html(data.DataInfo.recvBankAccount.OutBankCard);

                        

                        if (data.DataInfo.recvBankAccount.CheckFlag == 4) {
                            //$("#btnNext").attr('disabled', 'disabled');
                            //$("#FIcCardCode").val("");
                            //alert("用户已经确认不能再修改");
                            //return false;

                            $("#btnNext").removeAttr('disabled');
                            $("#FIcCardCode").val(cardNo);
                            //直接显示余额查询情况
                            $("#divPayDisplayInfo").hide();
                            $("#divPaySelArea").hide();
                            $("#divPayDisplayBalanceQeury").show();
                            $("#balanceQueryResult").html(data.ReMsg);
                            $("#btnSubmitCheck").hide();
                        }
                        else if (data.DataInfo.recvBankAccount.CheckFlag == 1 || data.DataInfo.recvBankAccount.CheckFlag == 2 || data.DataInfo.recvBankAccount.CheckFlag == 3) {
                            $("#btnNext").removeAttr('disabled');
                            $("#FIcCardCode").val(cardNo);
                            //直接显示余额查询情况
                            $("#divPayDisplayInfo").hide();
                            $("#divPaySelArea").hide();
                            $("#divPayDisplayBalanceQeury").show();
                            $("#balanceQueryResult").html(data.ReMsg);
                            if (data.DataInfo.recvBankAccount.CheckFlag <= 2) {
                                $("#btnSubmitCheck").hide();
                            } else {
                                $("#btnSubmitCheck").show();
                            }
                        }
                        else {

                            $("#btnNext").removeAttr('disabled');
                            $("#FIcCardCode").val(cardNo);

                        }
                    } else {
                        //$("#FIcCardCode").val("");
                        $("#FIcCardCode").val(cardNo);

                        //ClearAccountEditArea();
                    }
                }
                
            }

        }
    });
}

//查询用户信息
function btnReadCardQuery(e, saleSort) {
    var cardNo = readCardNo();
    $.post("/LeavePrisonBank/GetUserInfo", { "cardno": cardNo }, function (data, status) {
        if ("success" == status) {
            console.log(data);
            $("#selRecvAccount").html("<option value='' selected='selected'>请选择收款账号</option>");

            if (data.Flag == false) {
                ClearDisplayInfo();
                alert(data.ReMsg);
            } else {
                
                SetDispUserInfo(data);
                if (data.DataInfo.recvBankAccount != null) {
                    if (data.DataInfo.jieqingFlag == false) {
                        alert("您的账户尚有未结清的记录，请与供应站或相关管理科室联系");
                        return false;
                    }
                    $("#selRecvAccount").append("<option value='" + data.DataInfo.recvBankAccount.Id + "'>" + data.DataInfo.recvBankAccount.BankUserName + "(" + data.DataInfo.recvBankAccount.OutBankRemark + ")" + data.DataInfo.recvBankAccount.OutBankCard+"</option>");
                    $("#selPayMode").val(data.DataInfo.recvBankAccount.PayMode);
                    $("#BankUserName").html(data.DataInfo.recvBankAccount.BankUserName);
                    $("#OutBankRemark").html(data.DataInfo.recvBankAccount.OutBankRemark);
                    $("#OpeningBank").html(data.DataInfo.recvBankAccount.OpeningBank);
                    $("#OutBankCard").html(data.DataInfo.recvBankAccount.OutBankCard);

                    if (data.DataInfo.recvBankAccount.CheckFlag == 4) {
                        //$("#btnNext").attr('disabled', 'disabled');
                        //$("#FIcCardCode").val("");
                        //alert("用户已经确认不能再修改");
                        //return false;

                        $("#btnNext").removeAttr('disabled');
                        $("#FIcCardCode").val(cardNo);
                        //直接显示余额查询情况
                        $("#divPayDisplayInfo").hide();
                        $("#divPaySelArea").hide();
                        $("#divPayDisplayBalanceQeury").show();
                        $("#balanceQueryResult").html(data.ReMsg);
                        $("#btnSubmitCheck").hide();
                    }
                    else if (data.DataInfo.recvBankAccount.CheckFlag == 1 || data.DataInfo.recvBankAccount.CheckFlag == 2 || data.DataInfo.recvBankAccount.CheckFlag == 3) {
                        $("#btnNext").removeAttr('disabled');
                        $("#FIcCardCode").val(cardNo);
                        //直接显示余额查询情况
                        $("#divPayDisplayInfo").hide();
                        $("#divPaySelArea").hide();
                        $("#divPayDisplayBalanceQeury").show();
                        $("#balanceQueryResult").html(data.ReMsg);
                        if (data.DataInfo.recvBankAccount.CheckFlag <= 2) {
                            $("#btnSubmitCheck").hide();
                        } else {
                            $("#btnSubmitCheck").show();
                        }
                    }
                    else {
                        
                        $("#btnNext").removeAttr('disabled');
                        $("#FIcCardCode").val(cardNo);
                        
                    }
                } else {
                    $("#FIcCardCode").val("");
                    //ClearAccountEditArea();
                }
            }

        }
    });
}



function SetDispUserInfo(data) {
    $("#FIcCardCode").val(data.DataInfo.criminal.CardCode);
    $("#tdFCrimeCode").html(data.DataInfo.criminal.FCode);
    $("#tdFName").html(data.DataInfo.criminal.FName);
    $("#tdFAreaName").html(data.DataInfo.criminal.FAreaName);
    $("#tdFCyName").html(data.DataInfo.criminal.CyName);
    $("#tdFOuDate").html(getLongTime(data.DataInfo.criminal.FOuDate));
    $("#tdAmountA").html((data.DataInfo.criminal.AmountAmoney));
    $("#tdAmountB").html((data.DataInfo.criminal.AmountBmoney));
    $("#tdAmountC").html((data.DataInfo.criminal.AmountCmoney));
    $("#tdAmountALL").html((data.DataInfo.criminal.AmountAmoney + data.DataInfo.criminal.AmountBmoney + data.DataInfo.criminal.AmountCmoney));
    if (data.DataInfo.criminal.fflag == 1) {
        $("#tdFFlag").html("离监");
    }else if (data.DataInfo.criminal.fflag == 2) {
        $("#tdFFlag").html("保外");
    } else {
        $("#tdFFlag").html("在押");
    }
}

function ClearDisplayInfo() {
    ClearUserInfo();

    ClearAccountEditArea();
    $("#btnNext").attr('disabled', 'disabled');

}

function ClearUserInfo() {
    $("#tdFCrimeCode").html("");
    $("#tdFName").html("");
    $("#tdFAreaName").html("");
    $("#tdFCyName").html("");
    $("#tdFOuDate").html("");
    $("#tdFFlag").html("未知");
}

function ClearAccountEditArea() {
    $("#selBankUserName").val("");
    $("#selOutBankRemark").val("");
    $("#selOpeningBank").val("");
    $("#selOutBankCard").val("");
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
        if (rts.UserInfo.BankCardNo.length >= 19) {
            bankcardno = rts.UserInfo.BankCardNo.substr(0, 4) + " " + rts.UserInfo.BankCardNo.substr(4, 4) + " " + rts.UserInfo.BankCardNo.substr(8, 4) + " " + rts.UserInfo.BankCardNo.substr(12, 4) + " " + rts.UserInfo.BankCardNo.substr(16, 10);
        }
        $("#lblInfo").html("银行卡号：" + bankcardno);
        $("#curMonthXFE").html(rts.UserInfo.Xiaofeimoney);
        $("#okUseAllMoney").html(rts.UserInfo.OkUseAllMoney);
        $("#xiaofeiYuE").html(rts.UserInfo.NoXiaofeimoney);
        $("#amountC").html(rts.UserInfo.AmountCmoney);
        $("#amountB").html(rts.UserInfo.AmountBmoney);
        $("#amountA").html(rts.UserInfo.AmountAmoney);

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



function selectKKType() {
    var paymode = $("#selPayMode").find("option:selected").val()
    if (paymode == "0") {
        $("#selRecvAccount").val("");
        $("#selRecvAccount").attr("readonly", "readonly");
        $("#BankUserName").html("");
        $("#OutBankRemark").html("");
        $("#OpeningBank").html("");
        $("#OutBankCard").html("");
        $("#PayMode").html("柜台领取");
    } else {
        $("#selRecvAccount").removeAttr("readonly");
        $("#PayMode").html("转账支付");
    }

}

$(document).ready(function () {

    $("#div_SaleInputKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });

    $("#div_SaleInputKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });

    //如果有编号
    if ($("#userName").val() != "") {
        UserLoginCheck();
    }
});