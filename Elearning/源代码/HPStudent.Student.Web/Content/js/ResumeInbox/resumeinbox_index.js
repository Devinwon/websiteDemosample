var myDatatalbe = null;

$(function () {

    DataBindTable(getRootPath() + "/ResumeInbox/GetResumeInboxTable");



});



//测试列表的测试和查看项添加事件
$("#question-list").delegate(".btn-primary", "click", function () {
    var ajaxUrl;
    var ID = $(this).attr("data-id");
    var Name = $(this).attr("data-name");
    if ($(this).attr("data-action") == "seeResume") {
        //查看简历
         ajaxUrl = getRootPath() + "/Resume/PreviewResume?SID=" + ID;
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#previewResumepanel').modal('show');
            },
            complete: function (html) {
            }
        });

    } else if ($(this).attr("data-action") == "senderInvite") {
        //测试按钮  
        SendInvitation(ID, Name);
    }

   
});

//弹出发送邀请详情
function SendInvitation(StudentID, StudentName) {
    var ajaxUrl = getRootPath() + "/Student/SendInvitation?StudentID=" + StudentID + "&StudentName=" + StudentName;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#SendInvitationPanel').modal('show');
        },
        complete: function (html) {
            InvitationSend();
        }
    });
}
//发送邀请按钮
function InvitationSend() {
    var InterviewInvitationObj = {
        ID: 0,
        SID: 0,
        SenderID: 0,
        JobTitleID: 0,
        Content: '',
        IsRead: 0,
        SendDate: '',
        Receiver: ''
    };
    $("#InvitationSend").click(function () {
        var Obj = Object.create(InterviewInvitationObj);
        Obj.Content = $("#InvitationContent").val();
        Obj.JobTitleID = $("#selJob").val();
        if (Obj.JobTitleID == -1) {
            errorAlter("职位不能为空！");
            return;
        }
        Obj.SID = $(this).attr("data-id");
        Obj.Receiver = $("#HiddenStudentName").val();
        //执行异步添加
        $.ajax({
            type: "post",
            url: "/Student/SendInvitationInfo",
            dataType: "json",
            data: Obj,
            success: function (data) {
                if (data.ResultState == 0) {
                    $('#SendInvitationPanel').modal('hide');
                    $("#pop_modaldialog").empty();
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'success',
                    });
                } else {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'fail',
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    })
}


//绑定表格
function DataBindTable(ajaxUrl) {


    //if(sqlWhere!=null&&sqlWhere!=undefined&&sqlWhere!="")
    //{
    //    where += "";
    //}
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_resumeinbox").dataTable({
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
	            { "data": "RealName" },
                { "data": "Sex" },
                { "data": "Age" },
                { "data": "Education" },
                { "data": "Name" },
                { "data": "SendDate" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "class": "text-center"
                },
                 {
                     "targets": 1,
                     "class": "text-center"
                 },
                  {
                      "targets": 2,
                      "class": "text-center"
                  },
                   {
                       "targets": 3,
                       "class": "text-center"
                   },
                    {
                        "targets": 4,
                        "class": "text-center"
                    },
                {
                    "targets": 5,
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