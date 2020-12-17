

$(function () {

    $.post("/SelfSearch/GetCyTypeInfo", { "dt": new Date() }, function (data, status) {
        if ("success" == status) {
            for (var i = 0; i < data.length; i++) {
                $("#FCyType").append("<option value='" + data[i].FName + "'>" + data[i].FName + "</option>");
            }
        }
    });

    $.post("/SelfSearch/GetAreaInfo", { "dt": new Date() }, function (data, status) {
        if ("success" == status) {
            for (var i = 0; i < data.length; i++) {
                $("#FAreaName").append("<option value='" + data[i].FName + "'>" + data[i].FName + "</option>");
                //$("#schFAreaCode").append("<option value='" + data[i].FCode + "'>" + data[i].FName + "</option>");
            }
        }
    });

    $('#myButton').on('click', function () {
        var $btn = $(this).button('loading')
        // business logic...
        $btn.button('reset')
    });

    $.post("/SelfSearch/GetNotifyFiles", { "dt": new Date() }, function (data, status) {
        if (status == "success") {
            $("#divDocList").empty();
            for (var i = 0; i < data.length; i++) {
                var a = "<div class='col-sm-6'><div class='list-group'><a href='#' class='list-group-item' onclick=\"displayIFrameDoc('../" + data[i].LinkWebFile + "',1)\">"
                + "<h4 class='list-group-item-heading'>" + data[i].FTitle + "(日期:" +getShortTime( data[i].FDate) + ")</h4>"
                + "<p class='list-group-item-text'>" + data[i].FAbstract + "</p>"
            + "</a></div></div>";
                $("#divDocList").append(a);
            }
        }

    });

    setTimeout('myrefresh()', 1000); //指定1秒刷新一次
});

function myrefresh() {
    $(".h3_timeDiv").html("<a name='exe' class='colorWhite'>当前时间" + getTimeMin() + "</a>");
}


