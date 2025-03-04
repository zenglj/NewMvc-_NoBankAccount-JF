
//根据表达Id获取表达的值，并转换成Json字符串
function getFormJson(fromId) {
    var jsonData = {};
    $('#' + fromId).serializeArray().forEach(function (item) {
        // 基础版本（单值）
        // jsonData[item.name] = item.value;

        // 多值处理版本
        if (item.value == "")
                    return;
        if (jsonData[item.name] !== undefined ) {
            if (!Array.isArray(jsonData[item.name])) {
                jsonData[item.name] = [jsonData[item.name]];
            }
            jsonData[item.name].push(item.value);
        } else {
            jsonData[item.name] = item.value;
        }
    });
    //return jsonData; // 返回JSON对象
    return JSON.stringify(jsonData); // 返回JSON字符串
}


function getDivInputsToJson(divId) {
    const jsonData = {};
    $("#" + divId + " input").each(function () {
        const $input = $(this);
        const name = $input.attr("name");
        let value;

        // 处理复选框和单选按钮的选中状态
        if ($input.attr("type") === "checkbox" || $input.attr("type") === "radio") {
            value = $input.prop("checked") ? $input.val() : undefined;
        } else {
            value = $input.val();
        }

        // 跳过无 name 属性的输入项
        if (!name) return;

        // 跳过 value 为空的项
        if (value=='') return;

        // 处理多值字段（如同名复选框组）
        if (jsonData[name] !== undefined) {
            if (!Array.isArray(jsonData[name])) {
                jsonData[name] = [jsonData[name]];
            }
            if (value !== undefined) {
                jsonData[name].push(value);
            }
        } else {
            jsonData[name] = value !== undefined ? value : "";
        }
    });
    //return jsonData; // 返回 JSON 对象
    return JSON.stringify(jsonData); //返回 JSON 字符串
}

