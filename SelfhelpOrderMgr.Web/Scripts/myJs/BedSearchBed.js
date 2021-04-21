
$(function() {
    
    //$('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");FPriceNum
    var ffrom=$('#ff').form({   
                    url:"UI/GoodsEdit.ashx?action=SaveGoodsList",   
                    onSubmit: function(){   
                        // do some check   
                        // return false to prevent submit;   
                    },   
                    success:function(data){
                        alert(data);   
                    }   
                 });
    

   $('#FAreaCode').combobox({
                url: '../UI/Commons.ashx?action=GetAreaList',
                valueField: 'FCode',
                textField: 'FName'
            });
     $('#FHouseName').combobox({
                url: 'UI/SearchHouse.ashx?action=GetHouseList',
                valueField: 'ID',
                textField: 'FName'
            });       

    
    //床位列表清单
    $('#test').datagrid({
        //title: '床位列表',
        iconCls: 'icon-save',
        //width: 700,
        height: 400,        
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        //url: 'UI/SearchHouse.ashx?action=GetBedList',
        sortName: 'ID',
        sortOrder: 'asc',
        queryParams: {                        
                        action: 'GetBedList',                        
                        FCrimeName:"",
                        BedName:"",
                        FAreaCode:"",
                        FHouseName:"",
                        FFlag:"0"
                    },
        remoteSort: false,
        singleSelect:true,
        idField: 'ID',
        pageSize: 25,
        pageList: [25, 50],        
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '编号', field: 'ID', width: 50, sortable: true }					
				]],
        columns: [[
					{ field: 'FName', title: '号房', width: 100 },
					{ field: 'LevelName', title: '类别', width: 80 },
					{ field: 'FAreaCode', title: '监区号', width: 80 },
					{ field: 'FAreaName', title: '监区名称', width: 80 },
					{ field: 'FCrimeCode', title: '犯人编号', width: 80 },
					{ field: 'FCrimeName', title: '犯人姓名', width: 80 },
					{ field: 'FFlag', title: '状态', width: 80 },
					{ field: 'FRemark', title: '备注', width: 200 }													
				]]    
        });
        
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler:function(index,row){
            if (row.FFlag=="0"){
                return 'background-color:green;';
            }
        }
    });
    
    
    $('#editWindows').window({    
        modal:true,
        closed:true
    });


});


     
     //清空查询条件
     function clearSearch(){
        $("#FCrimeName").val('');
        $("#BedName").val('');
        $('#FAreaCode').combobox('setValue','');
        $('#FHouseName').combobox('setValue','');        
        $('#FAreaCode').combobox('setText','请选择监区');
        $('#FHouseName').combobox('setText','请选择大楼');        
     }
     
     //按条件查询
     function FilterSearch(){
        if($('#FAreaCode').combobox('getValue')=="请选择监区"){
            $('#FAreaCode').combobox('setValue','');
        }
        if($('#FHouseName').combobox('getValue')=="请选择大楼"){
            $('#FHouseName').combobox('setValue','');
        }
        $('#test').datagrid({
            url: 'UI/SearchHouse.ashx?action=GetBedList'
        });        
        $('#test').datagrid('load', {
            action: 'GetBedList',                        
            FCrimeName:$("#FCrimeName").val(),
            BedName:$("#BedName").val(),
            FAreaCode:$('#FAreaCode').combobox('getValue'),
            FHouseName:$('#FHouseName').combobox('getValue'),
            FFlag:$('#ccFFlag').combobox('getValue')
        });
     }