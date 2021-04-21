
$(function() {
    
    //$('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");FPriceNum
    var ffrom=$('#ff').form({   
                    url:"/BedMgr/SaveGoodsList",   
                    onSubmit: function(){   
                        // do some check   
                        // return false to prevent submit;   
                    },   
                    success:function(data){
                        alert(data);   
                    }   
                 });

   $('#FAreaCode').combobox({
                url: '/BedMgr/GetAreaList',
                valueField: 'FCode',
                textField: 'FName'
            });
            
    $('#FAreaCodeRoom').combobox({
        url: '/BedMgr/GetAreaList',
        valueField: 'FCode',
        textField: 'FName'
    });
    
    $('#FAreaCodeBed').combobox({
        url: '/BedMgr/GetAreaList',
        valueField: 'FCode',
        textField: 'FName'
    });

    
    //用户列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        width: 500,
        height: 200,        
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/BedMgr/GetAllList',
        sortName: 'Id',
        sortOrder: 'asc',
        queryParams: {                        
                        action: 'GetAllList',                        
                        Id:"",
                        FName:"",
                        LevelId:"",
                        FAreaCode:"",
                        FRemark:""
                    },
        remoteSort: false,
        singleSelect:true,
        idField: 'Id',
        pageSize: 25,
        pageList: [25, 50],        
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '编号', field: 'Id', width: 50, sortable: true }					
				]],
        columns: [[
					{ field: 'FName', title: '楼号', width: 60 },
					{ field: 'LevelName', title: '类别', width: 80 },
					{ field: 'FAreaCode', title: '监区号', width: 50 },
					{ field: 'FAreaName', title: '监区名称', width: 80 },
					{ field: 'FRemark', title: '备注', width: 200 }													
				]],
		toolbar: [{
	                text:'新增',
		            iconCls: 'icon-add',
		            handler: function(){
		                AddGoodsContent();
		            }
	            },'-',{
	                text:'编辑',
		            iconCls: 'icon-edit',
		            handler: function(){
		                AlterGoodsContent();
		            }
		        },'-',{
	                text:'删除',
		            iconCls: 'icon-cancel',
		            handler: function(){
		                DeleteHouse();
		            }
		        },'-',{
	                text:'批量生成号房',
		            iconCls: 'icon-ok',
		            handler: function(){
		                BatchAddEditContent();		                
		            }
		        }],
		onClickRow: function(rowIndex, rowData){		        
		        $("#ID").val(rowData.Id);		            
		        $("#FName").val(rowData.FName);
		        $("#LevelID").combobox('setValue', rowData.LevelId);	
		        $("#FAreaCode").combobox('setValue', rowData.FAreaCode);
		        $("#FRemark").val( rowData.FRemark);
		        displayRoomList(rowData.Id);		        
            }    
        });
        

    //号房列表清单
    $('#room').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        width: 500,
        height: 330,        
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        //url: 'UI/EditHouseUI.ashx?action=GetRoomList',
        sortName: 'Id',
        sortOrder: 'asc',
        queryParams: {                        
                        action: 'GetRoomList',                        
                        Id:"",
                        FName:"",
                        LevelId:"",
                        FAreaCode:"",
                        FRemark:""
                    },
        remoteSort: false,
        singleSelect:true,
        idField: 'Id',
        pageSize: 25,
        pageList: [25, 50],        
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '编号', field: 'Id', width: 50, sortable: true }					
				]],
        columns: [[
					{ field: 'FName', title: '号房', width: 100 },
					{ field: 'LevelName', title: '类别', width: 80 },
					{ field: 'FAreaCode', title: '监区号', width: 50 },
					{ field: 'FAreaName', title: '监区名称', width: 80 },
					{ field: 'FRemark', title: '备注', width: 200 }																	
				]],
		toolbar: [{
	                text:'增加号房',
		            iconCls: 'icon-add',
		            handler: function(){
		                AddRoomContent();
		            }
	            },'-',{
	                text:'修改号房',
		            iconCls: 'icon-edit',
		            handler: function(){
		                AlterRoomContent();
		            }
		        },'-',{
	                text:'删除号房',
		            iconCls: 'icon-cancel',
		            handler: function(){
		                DeleteRoom();
		            }
		        },'-',{
	                text:'批量号房生成',
		            iconCls: 'icon-ok',
		            handler: function(){
		                BatchAddEditBed();	                
		            }
		        }],
		onClickRow: function(rowIndex, rowData){		        		        
		        $("#IDRoom").val(rowData.Id);		            
		        $("#FNameRoom").val(rowData.FName);
		        $("#LevelIDRoom").combobox('setValue', rowData.LevelId);	
		        $("#FAreaCodeRoom").combobox('setValue', rowData.FAreaCode);
		        $("#FRemarkRoom").val(rowData.FRemark);
		        displayBedList(rowData.Id);        
            }    
        });
    
    //床位列表清单
    $('#bed').datagrid({
        //title: '床位列表',
        iconCls: 'icon-save',
        //width: 600,
        height: 400,        
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        //url: 'UI/EditHouseUI.ashx?action=GetRoomList',
        sortName: 'Id',
        sortOrder: 'asc',
        queryParams: {                        
                        action: 'GetBedList',                        
                        Id:"",
                        FName:"",
                        LevelId:"",
                        FAreaCode:"",
                        FRemark:""
                    },
        remoteSort: false,
        singleSelect:true,
        idField: 'ID',
        pageSize: 25,
        pageList: [25, 50],        
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '编号', field: 'Id', width: 50, sortable: true }					
				]],
        columns: [[
					{ field: 'FName', title: '号房', width: 100 },
					{ field: 'LevelName', title: '类别', width: 80 },
					{ field: 'FAreaCode', title: '监区号', width: 50 },
					{ field: 'FAreaName', title: '监区名称', width: 80 },
					{ field: 'FCrimeCode', title: '犯人编号', width: 80 },
					{ field: 'FCrimeName', title: '犯人姓名', width: 80 },
					{ field: 'FFlag', title: '状态', width: 80 },
					{ field: 'FRemark', title: '备注', width: 200 }													
				]],
		toolbar: [{
	                text:'增加床位',
		            iconCls: 'icon-add',
		            handler: function(){
		                AddBedContent();
		            }
	            },'-',{
	                text:'修改床位',
		            iconCls: 'icon-edit',
		            handler: function(){
		                AlterBedContent();
		            }
		        },'-',{
	                text:'删除床位',
		            iconCls: 'icon-cancel',
		            handler: function(){
		                DeleteBed();
		            }
		        }],
		onClickRow: function(rowIndex, rowData){		        		        
		        $("#IDBed").val(rowData.Id);		            
		        $("#FNameBed").val(rowData.FName);
		        $("#LevelIDBed").combobox('setValue', rowData.LevelId);	
		        $("#FAreaCodeBed").combobox('setValue', rowData.FAreaCode);
		        $("#FCrimeCodeBed").val(rowData.FCrimeCode);
		        $("#FCrimeNameBed").val(rowData.FCrimeName);		                
		        $("#FRemarkBed").val(rowData.FRemark);		        
            }    
        });
        
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler:function(index,row){
            if (row.ACTIVE=="N"){
                return 'background-color:red;';
            }
        }
    });
    
    
    //动态改变行颜色
    $('#bed').datagrid({
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

    //号床列表清单
    function displayRoomList(e)
    {
        //alert("qqw");
        $('#room').datagrid({           
            url: '/BedMgr/GetRoomList',
            queryParams: {
                fid: e
            }
        });
    }
    
    //床位列表清单
    function displayBedList(e)
    {
        alert(e);
        $('#bed').datagrid({           
            url: '/BedMgr/GetBedList',            
            queryParams: {
                fid: e
            }
            
        });
    }

    //保存提交修改内容
    function submitsaveContent(){        
        $('#LevelName').val($("#LevelID").combobox('getText'));
        $('#FAreaName').val($("#FAreaCode").combobox('getText'));
        $('#ID').removeAttr('disabled');    
        $('#ff').submit();
        
        $('#editWindows').window({             
            closed:true
        });
        $('#test').datagrid('load', {
            action: 'GetAllList'
        });
    }
    
    //保存修改大楼内容
    function saveContent(){
        
        if($("#ID").val()==""){
            $.messager.alert('提示','ID不能为空，谢谢！');
            return false; 
        }
        if($("#FName").val()==""){
            $.messager.alert('提示','大楼名称不能为空，谢谢！');
            return false; 
        }
        
        if($("#LevelID").combobox('getText')=="请选择类别"){
            $.messager.alert('提示','请指定类别，谢谢！');
            return false; 
        }
        if($("#FAreaCode").combobox('getText')=="请选择队别"){
            $.messager.alert('提示','请指定队别，谢谢！');
            return false;
        }
        var bedinfo = {
            "Id": $("#ID").val(),
            "FName": $("#FName").val(),
            "LevelName": $("#LevelID").combobox('getText'),
            "LevelId": $("#LevelID").combobox('getValue'),
            "FAreaCode": $("#FAreaCode").combobox('getValue'),
            "FAreaName": $("#FAreaCode").combobox('getText'),
            "FId": 0,
            "FCrimeCode": "",
            "FCrimeName": "",
            "FFlag": 0,
            "FRemark": $("#FRemark").val()
        }
        $.post("/BedMgr/SaveGoodsList", {
            "Dotype": $("#dotype").val(),
            "op": bedinfo
                                }, function(data, status) {
            if ("success" != status) {
                return false;
            } else {
                if (date.flag == true) {
                    $.messager.alert('提示', data);
                } else {
                    $.messager.alert('提示', data.reMsg);
                }
                                                    
            }
        });
        //$('#btnSave').linkbutton('disable');
        $('#editWindows').window({             
            closed:true
        });
        $('#test').datagrid('load', {
            action: 'GetAllList'
        });
                
    }
        
    
    //新增商品
    function AddGoodsContent(){
        $('#editWindows').window({    
            modal:true,
            closed:false
        });
        $("#dotype").val("add");//        
        $("#ID").val('');//   
        $("#FName").val('');// 
        $("#FRemark").val('');//        
    }
    
    //修改模式
    function AlterGoodsContent(){
        $('#editWindows').window({
            modal:true,
            closed:false
        });
        $("#dotype").val("update");//
        $('#ID').attr('disabled',"disabled");    
    }
    
    //删除楼房
    function DeleteHouse(){
        $.messager.confirm('提示','您是否真的要删除该记录?',function(r){   
            if (r){   
                    $.post("/BedMgr/Delete", {"ID":$("#ID").val()                                
                                            }, function(data, status) {
                        if ("success" != status) {
                            return false;
                        }else{
                            if(data=="OK.Success!"){
                                $.messager.alert('提示', data);
                                var row=$('#test').datagrid('getSelected');
                                var index=$('#test').datagrid('getRowIndex',row);
                                $('#test').datagrid('deleteRow',index);
                            }else{
                                $.messager.alert('提示', data.reMsg); 
                            }
                        }
                    });
            }   
        });

    }
    
     
//     //清空查询条件
//     function clearSearch(){
//        $("#FGoodsName").val('');
//        $("#FGoodsGTXM").val('');
//        $('#FGoodsType').combobox('setValue','');
//        $('#FGoodsStatus').combobox('setValue','');
//        $('#FGoodsIsXianE').combobox('setValue','');
//        $('#FGoodsType').combobox('setText','请选择类别');
//        $('#FGoodsStatus').combobox('setText','请选择状态');
//        $('#FGoodsIsXianE').combobox('setText','请选择限额');
//     }
//     
     //按条件查询
     function FilterSearch(){
        if($('#FGoodsType').combobox('getValue')=="请选择类别"){
            $('#FGoodsType').combobox('setValue','');
        }
        if($('#FGoodsStatus').combobox('getValue')=="请选择状态"){
            $('#FGoodsStatus').combobox('setValue','');
        }
        if($('#FGoodsIsXianE').combobox('getValue')=="请选择限额"){
            $('#FGoodsIsXianE').combobox('setValue','');
        }

        $('#test').datagrid('load', {
            action: 'GetAllList',                        
            FName:$("#FName").val(),
            FRemark:$("#FRemark").val(),
            LevelID:$('#LevelID').combobox('getValue'),
            LevelName:$('#LevelID').combobox('getText'),
            FAreaCode:$('#FAreaCode').combobox('getValue')            
        });
     }
     
    //修改模式
    function BatchAddEditContent(){
        $('#editBatchRoom').window({
            modal:true,
            closed:false
        });        
        $('#Flood').val(""); 
        $('#RoomNum').val("");   
    }
     
    //批量生成大楼号房
    function BatchAddHouse(){
        var selrow=$('#test').datagrid('getSelected');
        if (selrow==null){
            $.messager.alert('提示', "请选择一个大楼"); 
            return false;
        }
        if($("#Flood").val()==""){
            $.messager.alert('提示', "请输入楼层数"); 
            return false;
        }
        if($("#RoomNum").val()==""){
            $.messager.alert('提示', "每层的房间数"); 
            return false;
        }
        $.messager.confirm('提示','您是否真的要批量生成记录?',function(r){   
            if (r){   
                $.post("/BedMgr/BatchAddHouse", {                             
                                            "FID":$("#ID").val(),
                                            "Flood":$("#Flood").val(),
                                            "RoomNum":$("#RoomNum").val(),                                        
                                            "FAreaCode":$("#FAreaCode").combobox('getValue'),
                                            "FAreaName":$("#FAreaCode").combobox('getText')                                            
                                            }, function(data, status) {
                        if ("success" != status) {
                            return false;
                        }else{
                            if (data.flag == true) {
                                $('#room').datagrid('loadData', data.dataInfo);
                                $.messager.alert('提示', data.reMsg);

                                $('#editBatchRoom').window({             
                                    closed:true
                                });                                
                            }else{
                                $.messager.alert('提示', data.reMsg); 
                            }
                        }
                    });
            }   
        });
    }
     
     
         //新增号房
    function AddRoomContent(){
        $('#editRoom').window({    
            modal:true,
            closed:false
        });
        $("#dotypeRoom").val("add");//        
        $("#IDRoom").val('');//   
        $("#FNameRoom").val('');// 
        $("#FRemarkRoom").val('');// 
        $('#IDRoom').removeAttr('disabled');         
    }
    
        //修改号房
    function AlterRoomContent(){
        $('#editRoom').window({
            modal:true,
            closed:false
        });
        $("#dotypeRoom").val("update");//
        $('#IDRoom').attr('disabled',"disabled");    
    }
    
    
        //保存提交号房修改内容
    function submitRoomContent(){         
        $('#LevelNameRoom').val($("#LevelIDRoom").combobox('getText'));
        $('#FAreaNameRoom').val($("#FAreaCodeRoom").combobox('getText'));
        $('#IDRoom').removeAttr('disabled');
        $('#room').datagrid({           
            url: '/BedMgr/GetRoomList'
        });
        $('#ffRoom').submit();
        
        $('#editRoom').window({             
            closed:true
        });
        $('#room').datagrid('load', {
            action: 'GetAllList'
        });
    }
    
        //保存修改的号房
    function saveRoomContent(){
        
        if($("#IDRoom").val()==""){
            $.messager.alert('提示','ID不能为空，谢谢！');
            return false; 
        }
        if($("#FNameRoom").val()==""){
            $.messager.alert('提示','大楼名称不能为空，谢谢！');
            return false; 
        }
        
        if($("#LevelIDRoom").combobox('getText')=="请选择类别"){
            $.messager.alert('提示','请指定类别，谢谢！');
            return false; 
        }
        if($("#FAreaCodeRoom").combobox('getText')=="请选择队别"){
            $.messager.alert('提示','请指定队别，谢谢！');
            return false;
        }
        var bedinfo = {
            "Id": $("#IDRoom").val(),
            "FName": $("#FNameRoom").val(),
            "LevelName": $("#LevelIDRoom").combobox('getText'),
            "LevelId": $("#LevelIDRoom").combobox('getValue'),
            "FAreaCode": $("#FAreaCodeRoom").combobox('getValue'),
            "FAreaName": $("#FAreaCodeRoom").combobox('getText'),
            "FId": $("#ID").val(),
            "FCrimeCode": "",
            "FCrimeName": "",
            "FFlag": 0,
            "FRemark": $("#FRemarkRoom").val()
        }
        $.post("/BedMgr/SaveGoodsList", { 
                                "Dotype":$("#dotypeRoom").val(), 
            "op": bedinfo
                                }, function(data, status) {
            if ("success" != status) {
                return false;
            }else{                                                            
                $.messager.alert('提示', data);                                     
            }
        });
        //$('#btnSave').linkbutton('disable');
        $('#editRoom').window({             
            closed:true
        });
        $('#room').datagrid('load', {
            action: 'GetAllList'
        });
                
    }
    
        //删除号房
    function DeleteRoom(){
        $.messager.confirm('提示','您是否真的要删除该记录?',function(r){   
            if (r){   
                    $.post("/BedMgr/Delete", {                               
                                            "ID":$("#IDRoom").val()                                
                                            }, function(data, status) {
                        if ("success" != status) {
                            return false;
                        }else{
                            if(data=="OK.Success!"){
                                $.messager.alert('提示', data);
                                var row=$('#room').datagrid('getSelected');
                                var index=$('#room').datagrid('getRowIndex',row);
                                $('#room').datagrid('deleteRow',index);
                            }else{
                                $.messager.alert('提示', data); 
                            }
                        }
                    });
            }   
        });

    }
    
    //修改模式
    function BatchAddEditBed(){
        $('#editBatchBed').window({
            modal:true,
            closed:false
        });        
        $('#BedNum').val(""); 
    }
    
    //批量生成号房床位
    function BatchAddRoom(){
        var selrow=$('#room').datagrid('getSelected');
        if (selrow==null){
            $.messager.alert('提示', "请选择一个号房"); 
            return false;
        }
        if($('#BedNum').val()==""){
            $.messager.alert('提示', "请输入每个号房铁架床的数量."); 
            return false;
        }
        $.messager.confirm('提示','您是否真的要批量生成记录?',function(r){   
            if (r){   
                $.post("/BedMgr/BatchAddBed", {                              
                                            "FID":$("#IDRoom").val(),
                                            "BedNum":$("#BedNum").val(),
                                            "FAreaCode":$("#FAreaCodeRoom").combobox('getValue'),
                                            "FAreaName":$("#FAreaCodeRoom").combobox('getText')                                            
                                            }, function(data, status) {
                        if ("success" != status) {
                            return false;
                        }else{
                            if(data=="批量生成成功！"){
                                $.messager.alert('提示', data);
                                $('#editBatchBed').window({             
                                    closed:true
                                });
                            }else{
                                $.messager.alert('提示', data); 
                            }
                        }
                    });
            }   
        });
    }
    
    
    //新增床位
    function AddBedContent(){
        $('#editBed').window({    
            modal:true,
            closed:false
        });
        $("#dotypeBed").val("add");//        
        $("#IDBed").val('');//   
        $("#FNameBed").val('');// 
        $("#FRemarkBed").val('');// 
        $('#IDBed').removeAttr('disabled');         
    }
    
        //修改号房
    function AlterBedContent(){
        $('#editBed').window({
            modal:true,
            closed:false
        });
        $("#dotypeBed").val("update");//
        $('#IDBed').attr('disabled',"disabled");    
    }
    
    
        //保存提交号房修改内容
    function submitBedContent(){         
        $('#LevelNameBed').val($("#LevelIDBed").combobox('getText'));
        $('#FAreaNameBed').val($("#FAreaCodeBed").combobox('getText'));
        $('#IDBed').removeAttr('disabled');
        $('#bed').datagrid({           
            url: '/BedMgr/GetBedList'
        });
        $('#ffBed').submit();
        
        $('#editBed').window({             
            closed:true
        });
        $('#bed').datagrid('load', {
            action: 'GetAllList'
        });
    }
    
        //保存修改的床铺
    function saveBedContent(){
        
        if($("#IDBed").val()==""){
            $.messager.alert('提示','ID不能为空，谢谢！');
            return false; 
        }
        if($("#FNameBed").val()==""){
            $.messager.alert('提示','大楼名称不能为空，谢谢！');
            return false; 
        }
        
        if($("#LevelIDBed").combobox('getText')=="请选择类别"){
            $.messager.alert('提示','请指定类别，谢谢！');
            return false; 
        }
        if($("#FAreaCodeBed").combobox('getText')=="请选择队别"){
            $.messager.alert('提示','请指定队别，谢谢！');
            return false;
        }
        var bedinfo = {
            "Id": $("#IDBed").val(),
            "FName": $("#FNameBed").val(),
            "LevelName": $("#LevelIDBed").combobox('getText'),
            "LevelId": $("#LevelIDBed").combobox('getValue'),
            "FAreaCode": $("#FCrimeCodeBed").val(),
            "FAreaName": $("#FCrimeNameBed").val(),
            "FId": $("#IDRoom").val(),
            "FCrimeCode": "",
            "FCrimeName": "",
            "FFlag": 0,
            "FRemark": $("#FRemarkBed").val()
        }
        $.post("UI/EditHouseUI.ashx", { 
                                "Dotype":$("#dotypeBed").val(), 
                                "op":bedinfo
                                }, function(data, status) {
            if ("success" != status) {
                return false;
            }else{                                                            
                $.messager.alert('提示', data);                                     
            }
        });
        //$('#btnSave').linkbutton('disable');
        $('#editBed').window({             
            closed:true
        });
        $('#bed').datagrid('load', {
            action: 'GetAllList'
        });
                
    }
    
        //删除号房
    function DeleteBed(){
        $.messager.confirm('提示','您是否真的要删除该记录?',function(r){   
            if (r){   
                    $.post("/BedMgr/Delele", {                                
                                            "ID":$("#IDBed").val()                                
                                            }, function(data, status) {
                        if ("success" != status) {
                            return false;
                        }else{
                            if(data=="OK.Success!"){
                                $.messager.alert('提示', data);
                                var row=$('#bed').datagrid('getSelected');
                                var index=$('#bed').datagrid('getRowIndex',row);
                                $('#bed').datagrid('deleteRow',index);
                            }else{
                                $.messager.alert('提示', data.reMsg); 
                            }
                        }
                    });
            }   
        });

    }