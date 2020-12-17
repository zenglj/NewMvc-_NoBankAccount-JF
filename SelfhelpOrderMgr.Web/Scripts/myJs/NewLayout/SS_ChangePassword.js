$(function () {

    //注册单击保存的事件
    $("#saveUserPassword").click(function () {
        if ($("#userPassword").val() == "") {
            alert("错误,新密码不能为空");
            return false;
        }
        if ($("#userPassword").val() != $("#reUserPassword").val()) {
            alert("错识,您两次输入的密码不一致");
            return false;
        }
        $.post("/SystemSet/SaveUserPassword", { "OldPassword": $("#oldPassword").val(), "NewPassword": $("#userPassword").val() }, function (data, status) {
            if ("success" == status) {
                alert(data);
            }
        });

    });
});

