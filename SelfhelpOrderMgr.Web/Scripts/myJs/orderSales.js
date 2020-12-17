//数字键盘，代购模式下单，触屏方式输入
function btnEntAccountGTXM(e, g) {
    if (e == "确认") {
        if ($("#orderNumber").val() == "")//订单号为空时
        {
            var usercode = $("#UserCode").val();
            if (usercode.length >= 10) {
                $('#UserPwd').focus();
                if ($("#UserPwd").val().length >= 0) {//密码长度默认为0位
                    btnAddOrder('3504013494');
                }                
            } else {
                $('#UserCode').focus();
            }
        } else {
            if ($("#inputGcount").val() == "") {
                if ($("#inputGtxm").val()!="") {
                    gtxmEnter();
                    $("#tmpGount").val("");
                }
            } else {
                gcountEnter();
                $("#inputGcount").val("");
            }
        }
    } else if (e == "清空") {
            if ($("#orderNumber").val() == "")//订单号为空时
            {
                if($('#UserPwd').val()!=""){
                    $('#UserPwd').val("");
                    $('#UserCode').focus();
                } else {
                    $('#UserCode').val("");
                    $('#UserCode').focus();
                }
            } else {
                if ($("#inputGcount").val() == "") {
                    $("#inputGtxm").val("");
                    $("#inputGname").val("");
                    $('#inputGtxm').focus();
                } else {
                    $("#inputGcount").val("");
                    $('#inputGcount').focus();
                }
            }
    } else if (e == "删除") {
        if ($("#orderNumber").val() == "")//订单号为空时
        {
            if ($("#UserPwd").val() != "") {
                var vLength = $("#UserPwd").val().length;
                $("#UserPwd").val($("#UserPwd").val().substr(0, vLength - 1));
            } else {
                if ($("#UserCode").val() != "") {
                    var vLength = $("#UserCode").val().length;
                    $("#UserCode").val($("#UserCode").val().substr(0, vLength - 1));
                }                
            }
        } else {
            if ($("#inputGcount").val() != "") {
                var vLength = $("#inputGcount").val().length;
                $("#inputGcount").val($("#inputGcount").val().substr(0, vLength - 1));
            } else {
                if ($("#inputGtxm").val() != "") {
                    var vLength = $("#inputGtxm").val().length;
                    $("#inputGtxm").val($("#inputGtxm").val().substr(0, vLength - 1));
                }
            }
        }
    } else if (e == "/") {
        //空，不处理
    } else {
        if ($("#orderNumber").val() == "")//订单号为空时
        {
            var usercode = $("#UserCode").val();
            var userpwd = $("#UserPwd").val();
            if (usercode.length < 10) {
                $("#UserCode").val(usercode + e);
                $("#UserCode").focus();
            } else {
                $("#UserPwd").val(userpwd + e);
                $("#UserPwd").focus();
            }
        } else {
            if ($("#inputGname") .val()== "") {
                $("#inputGtxm").val($("#inputGtxm").val() + e);
                $("#inputGtxm").focus();
            } else {
                if ($("#tmpGount").val() == "") {
                    $("#inputGcount").val(e);
                    $("#tmpGount").val("1");
                    $("#inputGcount").focus();
                } else {
                    $("#inputGcount").val($("#inputGcount").val() + e);
                    $("#inputGcount").focus();
                }
            }
        }
    }
    

}





//商品条码框回车
function gtxmEnter(e) {
    var code = $("#inputGtxm").val();
    var SaleTypeId = $("#SaleTypeId").val();
    if ("" == code) {
        $("#lblInfo").text('请输入相应的编号!');
        $("#inputGtxm").focus();
    } else {
        $("#inputGcount").removeAttr("disabled");
        $.post("/Sales/GetGoodsInfo", { "Gtxm": code, "SaleTypeId": SaleTypeId }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {
                    var rts = $.parseJSON(words[1]);
                    //console.info(rts);
                    $("#inputGname").val(rts.GNAME);
                    $("#inputPrice").html(rts.GDJ);
                    $("#inputXgsl").val(rts.Xgsl);
                    $("#goodStockNumber").val(rts.Balance);
                    $("#strGoodFreeFlag").val(rts.Ffreeflag);
                    //设商品的属性规格
                    $("#goodAttribute").empty();
                    var attrs = $.parseJSON(words[2]);
                    $("#goodRemark").val('');
                    $("#goodAttrFlag").val('0');                    
                    for(var i=0;i<attrs.length;i++){
                        var attr = attrs[i];
                        //if (i == 0) {
                        //    $("#goodRemark").val(attr.AttrInfo);
                        //}
                        $("#goodAttrFlag").val('1');
                        $("#lblInfo").text("");
                        var li = "<li><a href='#' onclick=\"setAttr('" + attr.AttrInfo + "')\">" + attr.AttrInfo + "</a></li>";
                        $("#goodAttribute").append(li);
                    }
                    $("#inputGcount").val('1');
                    $("#inputGcount").select();
                    $("#inputGcount").focus();
                } else {
                    alert(data);
                }
            }
        });
    }

}

//数量框按回车
function gcountEnter(e) {
    
    var count = $("#inputGcount").val();
    $("#inputGcount").attr("disabled", "disabled");
    if ($("#SaleTypeId").val() == "1")
    {
        if (parseInt(count) != count) {
            $("#lblInfo").text("错误：数量不能有小数，必须是整数");
            return false;
        }
    } else {
        if (isNaN(count)) {
            $("#lblInfo").text("错误：数量必须是数字的，不能其他字母");
            return false;
        }
    }
    

    if ("" == count) {
        $("#lblInfo").text('请输入购买数量!');
        $("#inputGcount").focus();
    } else if (parseFloat(count) > parseFloat($("#goodStockNumber").html())) {
        $("#lblInfo").text("您购买数量当前最大库存量," + $("#goodStockNumber").html() + "个!");
    }else if(parseFloat( count)>parseFloat( $("#inputXgsl").val())){
        $("#lblInfo").text("您超过了商品最大限购数量," + $("#inputXgsl").val() + "个!");
    } else if ($("#goodAttrFlag").val() == "1" && $("#goodRemark").val() == ""){
        $("#lblInfo").text("该商品有多种规格,请选择相应产品的规格");
    } else {
        $("#lblInfo").text('...');
        CheckAndAddBuyList();
    }

}

//设定属性的值
function setAttr(e) {
    $("#goodRemark").val(e);
}


$(document).ready(function () {
    
    $("#div_SaleInputKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });

    $("#div_JSKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });
});