
            $(function() {
                $('#test').datagrid({
                    title: '收付列表',
                    iconCls: 'icon-save',
                    width: 900,
                    height: 320,
                    queryParams: {
                        startDate: $("#crimeSearch input[name=startDate]").val(),
                        endDate: $("#crimeSearch input[name=endDate]").val(),
                        action: 'GetTmpRcvPay',
                        rcvpay: 'all'
                    },
                    fitColumns: true,
                    nowrap: true,
                    autoRowHeight: false,
                    striped: true,
                    collapsible: true,
                    url: '/BankRpt/GetEdiMainRcvPayInfo',
                    remoteSort: false,
                    idField: 'AccNo',
                    pageSize: 50,
                    pageList: [50, 100],
                    toolbar: [{
                        iconCls: 'icon-print',
                        text:'Excel导出',
                        handler: function(){
                            $('#ff').form('submit', {   
                                url: '/BankRpt/GetEdiMainRcvPayInfo?action=ExcelOutport',
                                onSubmit: function(){   
                                    // do some check   
                                    // return false to prevent submit;   
                                },   
                                success:function(data){   
                                    //alert(data) 
                                    var words = data.split("|");
                                    if (words[0] == "OK") {
                                        window.open("/Upload/" + words[1]);
                                    }
                                }   
                            });
                        }
                    }],
                    onClickRow: function(rowIndex, rowData) {
                        var iRowId = rowIndex;
                        $('#test').datagrid('clearSelections'); //清除所有的选择项
                        $('#test').datagrid('selectRow', iRowId);
                        $('#test').datagrid('checkRow', iRowId); 
                    },
                    frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '银行账户', field: 'BankName', width: 120, sortable: true },
	                { title: '主单号', field: 'AccNo', width: 80, sortable: true }
                    ]],
                    columns: [[
					{ field: 'Dtype', title: '类型', width: 120 },
                    {
                        field: 'paydate', title: '日期', width: 150, sortable: true, formatter: function (value, row, index) {
                            if (row.paydate != null) {
                                if (row.paydate != "") {
                                    var dt = getLocalTime(value);
                                    if (dt == 'Invalid Date') {
                                        return '';
                                    } else {
                                        return dt;
                                    }

                                } else {
                                    return '';
                                }
                            } else {
                                return '';
                            }
                        }
                    },
					{ field: 'Fmoney', title: '总扣金额', width: 150 },
					{ field: 'SucMoney', title: '成功金额', width: 150 },
					{ field: 'ErrMoney', title: '失败金额', width: 220, sortable: true, //rowspan: 2,
					    sorter: function(a, b) {
					        return (a > b ? 1 : -1);
					    }
					},
					{ field: 'Remark', title: '备注', width: 150 }
                    ]],
                    pagination: true,
                    rownumbers: true
                });
                    
                    
                var p = $('#test').datagrid('getPager');
                $(p).pagination({
                    onBeforeRefresh: function() {
                        alert('before refresh');
                    }
                });
                    

                //加载队别信息
                //                    $.post("../Answer/GetAreaName.ashx", { "action": "GetAreaName" }, function(data, status) {
                //                        if (status == "success") {
                //                            if (data == "") {
                //                                return false;
                //                            }
                //                            ////console.info(data);
                //                            var areanames = $.parseJSON(data);
                //                            for (var i = 0; i < areanames.length; i++) {
                //                                var areaname = areanames[i];                                
                //                                var option = $("<option value='" + areaname.AreaCode + "'>" + areaname.AreaName + "</option>");
                //                                //alert(areaname.AreaName);
                //                                $("#cc").append(option);
                //                            }
                //                        }
                //                        else {
                //                            $.messager.alert('Warning', "加载数据失败，请重试");

                //                        }
                //                    });


            });
function resize() {
    $('#test').datagrid('resize', {
        width: 700,
        height: 400
    });
}
function getSelected() {
    var selected = $('#test').datagrid('getSelected');
    if (selected) {
        alert(selected.code + ":" + selected.name + ":" + selected.addr + ":" + selected.col4);
    }
}
function getSelections() {
    var ids = [];
    var rows = $('#test').datagrid('getSelections');
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].code);
    }
    alert(ids.join(':'));
}
function clearSelections() {
    $('#test').datagrid('clearSelections');
}
function selectRow() {
    $('#test').datagrid('selectRow', 2);
}
function selectRecord() {
    $('#test').datagrid('selectRecord', '002');
}
function unselectRow() {
    $('#test').datagrid('unselectRow', 2);
}
function mergeCells() {
    $('#test').datagrid('mergeCells', {
        index: 2,
        field: 'addr',
        rowspan: 2,
        colspan: 2
    });
}

function clearSearch() {
    $("#crimeSearch input").val("");
}

function FilterSearch() {
    if ("" == $("#crimeSearch input[name=fCrimeName]").val() && "" == $("#crimeSearch input[name=fCrimeCode]").val() && "000" == $("#cc").val()) {
        $.messager.alert('提示', '请输入或是选择相应的查询条件!');   

    } else {
        var rp;                        
        if($("#chkDS").attr('checked')=="checked" ){                            
            if($("#chkDF").attr('checked')=="checked"){
                rp="all";                                 
            }else{
                rp="rcv";                                 
            }
        }else if($("#chkDF").attr('checked')=="checked"){                            
            if($("#chkDS").attr('checked')=="checked"){
                rp="all";                                 
            }else{
                rp="pay";                                 
            }
        }else{            
            rp="all";            
        }                        
        $('#test').datagrid('load', {
            startDate: $("#crimeSearch input[name=startDate]").val(),
            endDate: $("#crimeSearch input[name=endDate]").val(),
            action: 'GetTmpRcvPay',
            rcvpay: rp
        });
    }


}
