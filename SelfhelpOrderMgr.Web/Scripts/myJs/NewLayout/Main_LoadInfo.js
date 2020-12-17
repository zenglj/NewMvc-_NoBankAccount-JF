function loadSystemSet(Controller, pageMdu) {
    //$.post("/" + Controller + "/LoadPartialPage", { "pageMdu": pageMdu }, function (data, status) {
    //    if ("success" == status) {
    //        $("#bodyArea").empty();
    //        $("#bodyArea").html(data);

    //    }
    //});
    $("#myIframe").attr("src", "/" + Controller + "/" + pageMdu);
}