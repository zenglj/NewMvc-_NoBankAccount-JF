//数字键盘，用户信息查询，触屏方式输入
var focusInputBoxId = "";
function btnInuptBoxEnt(e, g) {
    if (btnInuptBoxEnt != "") {
        if (e == "确认") {
            var key = jQuery.Event("keydown");//模拟一个键盘事件
            key.keyCode = 13;//keyCode=13是回车
            $("#" + focusInputBoxId).trigger(key);
            $("#tmpGount").val("");
        } else if (e == "清空") {
            $('#' + focusInputBoxId).val("");
            $('#' + focusInputBoxId).focus();
        } else if (e == "删除") {
            if ($("#" + focusInputBoxId).val() != "") {
                var vLength = $("#" + focusInputBoxId).val().length;
                $("#" + focusInputBoxId).val($("#" + focusInputBoxId).val().substr(0, vLength - 1));
            }
            $("#" + focusInputBoxId).focus();
        } else if (e == "/") {
            //空，不处理
        } else {
            if ($("#tmpGount").val() == "") {
                $("#" + focusInputBoxId).val(e);
                $("#tmpGount").val("1");
                $("#" + focusInputBoxId).focus();
            } else {
                var usercode = $("#" + focusInputBoxId).val();
                $("#" + focusInputBoxId).val(usercode + e);
                $("#" + focusInputBoxId).focus();
            }
            
        }
    }

}


$(document).ready(function () {

    //$("#div_UserQueryKeyBoard div span button").click(function () {
    //    btnInuptBoxEnt($.trim($(this).html()));
    //});
    $("input").focus(function () {
        focusInputBoxId = $(this).attr("id");
    });
});

