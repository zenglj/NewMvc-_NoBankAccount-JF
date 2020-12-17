
function showDiv(divName) {
    $("#cardNo").val("350628197903176017");
    if ($("#cardNo").val() == "") {
        $(".modal-body").html("请先刷二代证查询，谢谢！");
        $("#myModal").modal("show");
        return false;
    } else{
        $("#bodyArea").hide();
        $(".divNoShow").hide();
        $("#" + divName).show();
        $("#divDocList").show();
    }
    
}

//安全退出
function securityExit() {
    showDiv('bodyArea');
    $("#searchTile").html("请刷二代证开始查询");
    $("#cardNo").val("");    
}

function openmydoc(fileName) {    
    var doc = new ActiveXObject("Word.Application");
    doc.visible = true;
    doc.Documents.Open(fileName);

}

function displayIFrameDoc(filename,flag) {
    $("#iframeDoc").attr("src", filename);
    if (flag == 0) {
        $("#divDocList").show();
        $("#divDocContent").hide();
    } else {
        $("#divDocList").hide();
        $("#divDocContent").show();
    }
    
}


function readCard(cardNo,userName) {
    //alert(userName);
    //cardNo = '350122195702156410';
    if (cardNo!="") {
        if (cardNo != $("#cardNo").val()) {
            $.post("/SelfSearch/GetPrisonInfoByCardNo", { "cardNo": cardNo }, function (data, status) {
                if ("success" == status) {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        $("#searchTile").html("请刷二代证开始查询<small>"+userName+",您好!现在请开始按功能模块查询...</small>");
                        var notes = $.parseJSON(words[1]);
                        var row = notes[0];
                        //$("#keyId").val(row.keyId);
                        $("#FCode").val(row.FCode);
                        $("#FName").val(row.FName);
                        $("#FCyType").find("option:contains('" + row.FCyType + "')").attr("selected", true);
                        $("#FAreaName").find("option:contains('" + row.FAreaName + "')").attr("selected", true);
                        //通过文本方式设定选中项;
                        //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);
                        //通过数值方式设定选中项;

                        $("#FTotalScore").val(row.FTotalScore);
                        $("#FMonth1").val(row.FMonth1);
                        $("#FMonth2").val(row.FMonth2);
                        $("#FMonth3").val(row.FMonth3);
                        $("#FMonth4").val(row.FMonth4);
                        $("#FMonth5").val(row.FMonth5);
                        $("#FMonth6").val(row.FMonth6);
                        $("#FMonth7").val(row.FMonth7);
                        $("#FMonth8").val(row.FMonth8);
                        $("#FMonth9").val(row.FMonth9);
                        $("#FMonth10").val(row.FMonth10);
                        $("#FMonth11").val(row.FMonth11);
                        $("#FMonth12").val(row.FMonth12);

                        $("#FSalary").val(row.FSalary);
                        $("#FConsume").val(row.FConsume);
                        $("#FAmount").val(row.FAmount);

                        $("#FYuanPan").val(row.FYuanPan.substr(0, 2) + "年" + row.FYuanPan.substr(3, 2) + "月" + row.FYuanPan.substr(6, 2) + "天");
                        $("#FStartDate").val(getShortTime(row.FStartDate));
                        $("#FEndDate").val(getShortTime(row.FEndDate));
                        $("#FXF_Change").val(row.FXF_Change);

                        $("#FJF_Info").val(row.FJF_Info);
                        $("#FRewardText").val(row.FRewardText);
                        $("#FRewardScore").val(row.FRewardScore);
                        $("#FRewardTime").val(row.FRewardText);
                        $("#FDeductText").val(row.FDeductText);
                        $("#FRewardScore").val(row.FRewardScore);
                        $("#FRewardTime").val(row.FRewardText);
                        $("#BankCardNo").html(row.BankCardNo);
                        speechTTS("" + userName + ",您好!您可以开始查询了。");
                    } else {
                        //alert(data);
                        $("#searchTile").html("请刷二代证开始查询<small>" + data + "</small>");
                        speechTTS(words[1]);
                    }

                }
            });
            $("#cardNo").val(cardNo);
        }
    }
    
}