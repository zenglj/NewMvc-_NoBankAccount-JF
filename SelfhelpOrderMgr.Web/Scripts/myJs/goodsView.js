
$(function () {
    //$('#myModal').modal({
    //    keyboard: true
    //})
        

    //$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //    // 获取已激活的标签页的名称
    //    var activeTab = $(e.target).text(); 
    //    // 获取前一个激活的标签页的名称
    //    var previousTab = $(e.relatedTarget).text();        
    //    $(".active-tab span").html(activeTab);
    //    $(".previous-tab span").html(previousTab);
    //});

    $('#myModal').on('show.bs.modal', function () {
        $("#inumber").val("1");
        //$("#orderMoney").html('0.00');
    });
});




//显示我的订单明细记录
function btnMyOrder() {
    $('#myOrderList').modal('show');
}


