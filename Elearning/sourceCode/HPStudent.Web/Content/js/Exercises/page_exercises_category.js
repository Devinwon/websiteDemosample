$(function(){  

    //省份下拉框变化
    $('#selMajor').change(function () {
        var MajorName = $(this).val();
        $('#IncludeCategory').empty();
        if (MajorName == "××请选择专业××") {
            return;
        } else {
            //显示提示
            $('#IncludeCategory').append("<label class=\"check col-md-3 dataLoading\">数据加载中,请稍后...</label>");
        }
        //ajax加载数据
        $.ajax({
            type:"post",
            url: "/Exercises/GetCategoryByMajoyName",
            dataType: "json",
            data: "MajorName=" + MajorName,
            success: function (data) {
                $('#IncludeCategory').empty();

                $.each(data, function (i, item) {
                    var strCheck = "";
                    if (item.IsChecked) { strCheck = "checked"; }
                    $('#IncludeCategory').append("<label class=\"check col-md-3\"><input type=\"checkbox\" data-id =\"" + item.CID + "\" class=\"icheckbox\" " + strCheck + "/> " + item.CategoryName + "</label>");
                });
                feiCheckbox();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    });

    //点击修改按钮
    $('#btnEdit').click(function () {
        var selectCategory = "";
        var MajorName = $('#selMajor').val();
        $('#IncludeCategory input:checked').each(function () {
            selectCategory += $(this).attr("data-id") + ",";
        });
        if (selectCategory == "") {
            return;
        } 
        //异步调用修改接口
        $.ajax({
            type: "post",
            url: "/Exercises/EditCategoryByMajoyName",
            dataType: "json",
            data: { MajorName: MajorName, selectCategory: selectCategory },
            success: function (data) {
                if (data.ResultState == 0) {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'success',
                        timeout: 2000
                    });
                } else {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'error',
                        timeout: 2000
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    });

    //修复动态加载的Checkbox无法显示样式的BUG
    var feiCheckbox = function () {
        if ($(".icheckbox").length > 0) {
            //$(".icheckbox,.iradio").iCheck({checkboxClass: 'icheckbox_minimal-grey',radioClass: 'iradio_minimal-grey'});
            $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
        }
    }
});
        //if ($(this).val() == "Dotnet软件技术") {

        //    $("#selCourse").empty();
        //    $('#selCourse').append("<label class=\"check col-md-3\"><input type=\"checkbox\" class=\"icheckbox\" /> "++"</label>");
        //    $('#selCourse').append("<option>HTML网页设计</option>");
        //    $('#selCourse').append("<option>JAVA编程技术基础</option>");
        //    $('#selCourse').append("<option>SQLServer数据库基础</option>");
        //    $('#selCourse').append("<option>C#编程技术基础</option>");
        //    $('#selCourse').append("<option>WinForm技术应用</option>");
        //    $('#selCourse').append("<option>SQLServer数据库开发</option>");
        //    $('#selCourse').append("<option>JavaScript技术应用</option>");
        //    $('#selCourse').append("<option>Asp.Net网站制作</option>");
        //    $('#selCourse').append("<option>JQuery技术应用</option>");
        //    $('#selCourse').append("<option>Oracle数据库开发</option>");
        //    $('#selCourse').append("<option>XML基础</option>");
        //    $('#selCourse').append("<option>Asp.net网站开发</option>");
        //    $('#selCourse').append("<option>ASP.NET-MVC编程技术</option>");
        //}


//		//添加题目
//		$('#btnAddExercise').on('click', function ( e ) {
//			//如果没有选择省份或城市，弹出提示
//			if($('#selMajor').val()=="== 选择专业 ==" || $("#selCourse").val()=="== 选择课程 =="){
//				alert("请先选择专业和课程后再添加题目");
//				return false;
//			}

//			//弹出添加窗口
//			var ajaxUrl = "pop-exercises-library-add.html?"+ Date.now();
//        	$.get(ajaxUrl,function(html,status){
//				$("#pop_modaldialog").empty();
//			    $('#pop_modaldialog').append(html);
//			    $('#add-exercises-library').modal('show'); 
//			});
//		});

//		//成绩列表的编辑和删除项添加事件
//		$('#class-list').find('.btn-primary').on('click', function ( e ) {
//			if($(this).attr("data-action")=="edit"){
//				//编辑按钮
//				var ajaxUrl = "pop-exercises-library-edit.html?id="+$(this).attr("data-id") +"&"+ Date.now();
//            	$.get(ajaxUrl,function(html,status){
//					$("#pop_modaldialog").empty();
//				    $('#pop_modaldialog').append(html);
//				    $('#edit-exercises-library').modal('show'); 
//				});

//			}else{
//				//删除按钮
//				myConfirm("确定要删除当前选中的题目吗？","题目删除后将无法恢复！","DelQuestion()","#pop_modaldialog");

//		        return false;
//			}

			
//        });

    
//	//弹出确认窗口关闭 -- 取消
//	$("#pop_modaldialog").delegate(".pop-confirm-warning-sure","click",function(){
//       $(this).parents(".message-box").removeClass("open");       
//    });    

//	//弹出确认窗口关闭  -- 确定
//	$("#pop_modaldialog").delegate(".pop-confirm-warning-close","click",function(){
//       $(this).parents(".message-box").removeClass("open");
//       // alert('2');
//    });  

//});  

////删除班级信息
//function DelQuestion() {
//    alert("数据删除成功");
//    $("#pop_modaldialog").find(".message-box").removeClass("open");       
//}

////添加班级信息
//function AddQuestion(){
//	alert("数据添加成功");
//    $('#add-exercises-library').modal('hide'); 
//}

////添加班级信息
//function EditQuestion(){
//	alert("数据修改成功");
//    $('#edit-exercises-library').modal('hide'); 
//}