
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


//根据DIV Id 获取其下面的Input的值并转成json string
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


//加载BootStrap的表单数据
function loadFormData(data, formId ) {
    $("#" + formId).find('.form-control').each(function () {
        const $elem = $(this);
        const fieldName = $elem.attr('name') || $elem.attr('id');

        if (!fieldName || !data.hasOwnProperty(fieldName)) return;

        // 文本类控件赋值
        if ($elem.is('input[type="text"][class*="date"')) {
            //$elem.val(data[fieldName]);
            // 刷新 Bootstrap 增强组件
            $elem.datepicker('setDate', timestampToDate( data[fieldName]));
        }
        // 文本类控件赋值
        else if ($elem.is('input[type="text"], textarea')) {
            $elem.val(data[fieldName]);
        }
        // 单选/复选框处理
        else if ($elem.is('input[type="radio"], input[type="checkbox"]')) {
            const val = $elem.val();
            $elem.prop('checked', val == data[fieldName]);
        }
        // 下拉框处理
        else if ($elem.is('select')) {
            $elem.val(data[fieldName]).trigger('change');
            // Bootstrap Select 插件兼容
            if ($elem.hasClass('selectpicker')) {
                $elem.selectpicker('refresh');
            }
        }
    });
    
}

// 调用示例
//const sampleData = {
//    username: 'JohnDoe',
//    gender: 'male',
//    newsletter: true
//};
//loadFormData(sampleData);


//日期格式化
function formatDateTime(val) {
    return val ? new Date(val).toLocaleString() : '';
}


 //日期格式化 时间戳格式的调用
function formatDate(value) {
    const timestamp = parseInt(value.match(/\d+/), 10); // 提取时间戳 ‌:ml-citation{ref="3" data="citationList"}
    const date = new Date(timestamp); // 创建日期对象 ‌:ml-citation{ref="1,2" data="citationList"}
    // 输出结果
    return date.toLocaleDateString('zh-CN'); // "2025/3/5" ‌:ml-citation{ref="6" data="citationList"}
}

//时间戳转成时间
function timestampToDate(value,tmFlag=0) {
    const timestamp = parseInt(value.match(/\d+/), 10); // 提取时间戳 ‌:ml-citation{ref="3" data="citationList"}
    const date = new Date(timestamp); // 创建日期对象 ‌:ml-citation{ref="1,2" data="citationList"}
    // 输出结果
    if (tmFlag = 0) {
        return date; // "2025/3/5" ‌:ml-citation{ref="6" data="citationList"}
    } else {
        //var sdate= date.toLocaleDateString(); // "2025/3/5" ‌:ml-citation{ref="6" data="citationList"}
        var sdate = date.getFullYear()+"-"+(date.getMonth()+1)+"-"+date.getDate();
        return sdate; 

    }
}