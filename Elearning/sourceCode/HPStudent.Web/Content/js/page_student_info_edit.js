$(function(){  

/* ajax实现选项卡功能
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
  var target = $(e.target).attr("href") // activated tab
  if ($(target).is(':empty')) {
    $.ajax({
      type: "GET",
      url: "/article/",
      error: function(data){
        alert("There was a problem");
      },
      success: function(data){
        $(target).html(data);
      }
  })
 }
 var target = $(e.target).attr("href");
 $(target).html("数据加载中...");
});
*/
		//添加成绩
		$('#btnAddMark').on('click', function ( e ) {
			var ajaxUrl = "pop-student-info-add.html?id="+$(this).attr("data-id") +"&"+ Date.now();
        	$.get(ajaxUrl,function(html,status){
				$("#pop_modaldialog").empty();
			    $('#pop_modaldialog').append(html);
			    $('#edit-student-mark').modal('show'); 
			});
		});

		//成绩列表的编辑和删除项添加事件
		$('#exam-list').find('.btn-primary').on('click', function ( e ) {
			if($(this).attr("data-action")=="edit"){
				//编辑按钮
				var ajaxUrl = "pop-student-info-edit.html?id="+$(this).attr("data-id") +"&"+ Date.now();
            	$.get(ajaxUrl,function(html,status){
					$("#pop_modaldialog").empty();
				    $('#pop_modaldialog').append(html);
				    $('#edit-student-mark').modal('show'); 
				});

			}else{
				//删除按钮

				var ajaxUrl = "pop-warning-confirm.html?" + Date.now();
            	$.get(ajaxUrl,function(html,status){
            		//替换html中的关键内容
            		html = html.replace("##WarningTitle##","确定要删除此考试成绩吗？");
            		html = html.replace("##WarningContent##","数据删除后将无法恢复！");
            		html = html.replace("##WarningURL##","javascript:DelMark()");
					$("#pop_modaldialog").empty();
				    $('#pop_modaldialog').append(html); 
				});

		        return false;
			}

			
        });

    
	//弹出确认窗口关闭 -- 取消
	$("#pop_modaldialog").delegate(".pop-confirm-warning-sure","click",function(){
       $(this).parents(".message-box").removeClass("open");       
    });    

	//弹出确认窗口关闭  -- 确定
	$("#pop_modaldialog").delegate(".pop-confirm-warning-close","click",function(){
       $(this).parents(".message-box").removeClass("open");
       // alert('2');
    });  

});  

function DelMark() {
      alert("数据删除成功");
      $("#pop_modaldialog").find(".message-box").removeClass("open");       
}