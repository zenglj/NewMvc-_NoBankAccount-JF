//---------------------------------Lodop打印小票相关操作函数 开始-------------------------------------
//Lodop打印小票

var LODOP; //声明为全局变量
function myPrint() {
    CreatePrintPage();
    LODOP.PRINT();
};
function myPrintA() {
    CreatePrintPage();
    LODOP.PRINTA();
};
function myPreview() {
    CreatePrintPage();
    LODOP.PREVIEW();
};
function mySetup() {
    CreatePrintPage();
    LODOP.PRINT_SETUP();
};
function myDesign() {
    CreatePrintPage();
    LODOP.PRINT_DESIGN();
};
function myBlankDesign() {
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    LODOP.PRINT_INIT("打印控件功能演示_Lodop功能_空白练习");
    LODOP.PRINT_DESIGN();
};
function CreatePrintPage() {
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    LODOP.PRINT_INIT("打印控件功能演示_Lodop功能_名片");
    LODOP.ADD_PRINT_RECT(10, 55, 360, 220, 0, 1);
    LODOP.SET_PRINT_STYLE("FontSize", 11);
    LODOP.ADD_PRINT_TEXT(20, 180, 100, 25, "郭德强");
    LODOP.SET_PRINT_STYLEA(2, "FontName", "隶书");
    LODOP.SET_PRINT_STYLEA(2, "FontSize", 15);
    LODOP.ADD_PRINT_TEXT(53, 187, 75, 20, "科学家");
    LODOP.ADD_PRINT_TEXT(100, 131, 272, 20, "地址：中国北京社会科学院附近东大街西胡同");
    LODOP.ADD_PRINT_TEXT(138, 132, 166, 20, "电话：010-88811888");
};
function myAddHtml() {
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    LODOP.PRINT_INIT("");
    LODOP.ADD_PRINT_HTM(10, 55, "100%", "100%", document.getElementById("textarea01").value);
};
function myTestHtml(tempName,pageWidth) {
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    LODOP.PRINT_INIT("");
    LODOP.SET_PRINT_PAGESIZE(3, pageWidth, 10, "");
    if (tempName == "") {
        LODOP.ADD_PRINT_HTM(10, 1, "100%", "100%", $("#template").html());
    } else {
        LODOP.ADD_PRINT_HTM(10, 1, "100%", "100%", $(tempName).html());
    }
    LODOP.PRINT();



};

function printMyAddHtml(e) {
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    LODOP.PRINT_INIT("");
    LODOP.ADD_PRINT_HTM(10, 20, "100%", "100%", $("#" + e).html());
    LODOP.PREVIEW();
};
//---------------------------------Lodop打印小票相关操作函数 结束-------------------------------------