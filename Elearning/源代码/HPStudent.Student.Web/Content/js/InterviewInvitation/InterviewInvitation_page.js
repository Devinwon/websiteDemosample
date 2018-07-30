var myDatatalbe = null;
var myDetailTable = null;


$(function () {

    DataBindTable(getRootPath() + "/InterviewInvitation/GetInterviewInvitationTable");



});


//绑定表格
function DataBindTable(ajaxUrl) {


    //if(sqlWhere!=null&&sqlWhere!=undefined&&sqlWhere!="")
    //{
    //    where += "";
    //}
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_interviewInvitation").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "CompanyName" },
                { "data": "Address" },
                { "data": "TelPhone" },
                { "data": "SendDate" },
                { "data": "Name" },
                { "data": "City" },
                { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 3,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['SendDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                }
                //,
                //{
                //    "targets": 3,
                //    "class": "text-center",
                //    "render": function (data, type, row) {
                //        var postdate = row['EndDate'];
                //        var outHtml = GetLocalTime(postdate);
                //        return outHtml;
                //    }
                //},
            ]
        });
        onresize();
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


//测试列表的测试和查看项添加事件
$("#question-list").delegate(".btn-primary", "click", function () {
    var IID = $(this).attr("data-id");
    var ajaxUrl;
    if ($(this).attr("data-action") == "seeResume") {
        //查看按钮
        ajaxUrl = "/InterviewInvitation/EnterpriseDetail?IID=" + IID + "";
    } 
    //Ajax获取页面
    $.ajax({
        type: "Post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-detail').modal('show');
        }
    });
});





function GetLocalTime(nS) {
    nS = nS.replace("/Date(", "").replace(")/", "");

    var newDate = new Date(parseInt(nS));

    var year = newDate.getFullYear();
    var month = newDate.getMonth() + 1;
    var date = newDate.getDate();
    var hour = newDate.getHours();
    var minute = newDate.getMinutes();
    var second = newDate.getSeconds();
    return year + "-" + month + "-" + date;
}


//弹出确认窗口关闭 -- 取消
$("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {
    $(this).parents(".message-box").removeClass("open");
});

//弹出确认窗口关闭  -- 确定
$("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {
    $(this).parents(".message-box").removeClass("open");
    // alert('2');
});
