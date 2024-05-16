


//弹出式数字键盘，数量处理，商品购买的数量
function btnNumer(e, g) {
    if (e == "") {
        $("#inputGcount").val('');
    } else if (e == "-1") {
        var ss = $("#inputGcount").val();
        $("#inputGcount").val(ss.substr(0, ss.length - 1));
    } else if (e == "+") {
        var ss = $("#inputGcount").val();
        if (ss == "") {
            $("#inputGcount").val("1");
            return false;
        }
        var i = parseInt(ss);
        $("#inputGcount").val(i + 1);
    } else if (e == "-") {
        var ss = $("#inputGcount").val();
        if (ss == "") {
            $("#inputGcount").val("");
            return false;
        }
        var i = parseInt(ss);
        if (i <= 1) {
            $("#inputGcount").val("");
        } else {
            $("#inputGcount").val(i - 1);
        }
        
    } else if (e == "init") {//初始化加载

    } else if (e == "OK") {//
        //检测并增加一条购买商品记录
        $('#myModal').modal('hide');//隐藏数字框
        if ($("#inputGcount").val() == "") {
            alert("请输入数量");
            return false;
        }
        if ($("#goodAttrFlag").val() == "1" && $("#goodRemark").val() == "") {
            alert("该商品有多种规格,请选择相应产品的规格");
        }

        //if ("" == $("#orderNumber").val()) {
        //    alert("订单号不能为空，请先刷卡，谢谢");
        //    return false;
        //}
        if (parseFloat($("#inputGcount").val()) > parseFloat($("#inputXgsl").val())) {
            alert("'您超过了商品最大限购数量," + $("#inputXgsl").val() + "个!'");
        } else {
            CheckAndAddBuyList('selfOrder');
        }
        
    } else if (e == "Cancel") {//取消
        //$('#myModal').modal('hide');//隐藏数字框
        $("#goodDetail").hide();
        $("#goodInfoView").show();

    } else {
        $("#inputGcount").val($("#inputGcount").val() + e);
    }
}

//弹出式数字键盘，数量处理，商品购买的数量
function btnRoomNo(e, g) {
    if (e == "") {
        $("#userRoomNO").val('');
    } else if (e == "-1") {
        var ss = $("#userRoomNO").val();
        $("#userRoomNO").val(ss.substr(0, ss.length - 1));
    } else if (e == "+") {
        var ss = $("#userRoomNO").val();
        if (ss == "") {
            $("#userRoomNO").val("1");
            return false;
        }
        var i = parseInt(ss);
        $("#userRoomNO").val(i + 1);
    } else if (e == "-") {
        var ss = $("#userRoomNO").val();
        if (ss == "") {
            $("#userRoomNO").val("");
            return false;
        }
        var i = parseInt(ss);
        if (i <= 1) {
            $("#userRoomNO").val("");
        } else {
            $("#userRoomNO").val(i - 1);
        }

    } else if (e == "init") {//初始化加载

    } else if (e == "OK") {//        

    } else if (e == "Cancel") {//取消

    } else {
        $("#userRoomNO").val($("#userRoomNO").val() + e);
    }
}

$(function () {
    //$('#myModal').modal({
    //    keyboard: true
    //})

    $('#myModal').on('show.bs.modal', function () {
        $("#inputGcount").val("1");
        //$("#orderMoney").html('0.00');
    })
});


//点击购买时按钮
function btnBuy(e) {
    var words = e.split("|");
    $("#inputGtxm").html(words[0]);
    $("#inputPrice").html(words[1]);
    $("#inputGname").html(words[2]);    
    $("#strGoodFreeFlag").val(words[3]);
    $("#inputXgsl").val(words[4]);
    $("#inputGStandard").html(words[5]);       
    $("#inputImageSrc").attr("src", words[6]);
    $("#inputGcount").val("");
    $("#goodAttributes").empty();//清空商品属性内容
    $("#goodRemark").html("");//清空商品备注内容
    $.post("/Shopping/GetGoodsStockNumber", { "GTXM": words[0] }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                alert(data);
            } else {
                if (parseFloat(words[1]) < 10) {
                    $("#goodStockNumber").html(toDecimal2(words[1]));
                } else {
                    $("#goodStockNumber").html(words[1]);
                }
                if (words[2] != null) {
                    var attrs = $.parseJSON(words[2]);
                    for (var i = 0; i < attrs.length; i++) {
                        var attr = attrs[i];
                        var div = "";
                        if (i == 0) {
                            $("#goodRemark").html(attr.AttrInfo);
                            div = "<div class='radio'><label><input type='radio' name='optionsRadios' id='" + attr.ID + "' onclick='setGoodRemark(\"" + attr.AttrInfo + "\")' value='option1' checked>" + attr.AttrInfo + "　</label></div>";
                        } else {
                            div = "<div class='radio'><label><input type='radio' name='optionsRadios' id='" + attr.ID + "' onclick='setGoodRemark(\"" + attr.AttrInfo + "\")' value='option1'>" + attr.AttrInfo + "　</label></div>";
                        }
                        $("#goodAttributes").append(div);
                    }                    
                }
            }
        }
    });
    //$("#goodStockNumber").html("2.00");
    //$('#myModal').modal('show');
    //$('#myAttribute').modal('show');
    
    $("#goodInfoView").hide();
    $("#goodDetail").show();
    window.location.href="#goodDetail";
}

//选中尺码、口味并设定值

function setGoodRemark(e) {
    $("#goodRemark").html(e);
}

//显示我的订单明细记录
function btnMyOrder() {
    $('#myOrderList').modal('show');
}

//在自助框里商品信息查询
function btnGoodsSearch() {
    if ($("#gtxmSearchBox").val() == "") {
        //alert("商品编号不能为空");
        $("#myPromptBoxInfo").html("商品编号不能为空");
        $("#myPromptBox").modal("show");
        return false;
    }

    $.post("/Shopping/SearchGoodsInfo", { "saleTypeId": $("#saleTypeId").val(), "Gtxm": $("#gtxmSearchBox").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                //alert(words[1]);
                $("#myPromptBoxInfo").html(words[1]);
                $("#myPromptBox").modal("show");
                return false;
            } else {
                var good = $.parseJSON(words[1]);
                var goodstr = good.GTXM + "|" + good.GDJ + "|" + good.GNAME + "|" + good.Ffreeflag + "|" + good.Xgsl + "|" + good.GStandard + "|" + good.src;
                $("#gtxmSearchBox").val("");
                $("#gtxmSearchBox").tooltip("destroy");
                btnBuy(goodstr);//显示抬头栏的账户余额信息
            }
        }
    });

}



$(function () { $("[data-toggle='tooltip']").tooltip(); });

function setTooltip1() {
    $('#gtxmSearchBox').tooltip('destroy');  
}
function setTooltip() {
    var options = {
        animation: false,
        trigger: 'focus', //触发tooltip的事件
        title: "<h3>" + $("#gtxmSearchBox").val() + "</h3>",
        html: true
        //delay:{ show: 500, hide: 1000 },
        
    }
    $("#gtxmSearchBox").tooltip(options)
    $("#gtxmSearchBox").tooltip('show');
}