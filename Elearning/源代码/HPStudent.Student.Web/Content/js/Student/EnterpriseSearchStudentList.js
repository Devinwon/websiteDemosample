var iCount; //定时器
var myDatatalbe;
var EnterpriseSearchViewModelObj = {
    City: 0,
    SkillKey: '',
    state: '',
    Job: ''
};
$(function () {
    BindDatatable('/Student/QuerySimilarStudentInfoJson', Object.create(EnterpriseSearchViewModelObj));
});
//绑定Datatable
function BindDatatable(ajaxUrl, dataJson) {
    //清空Datatable
    $('#QA_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_question").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl
            },
            "serverMethod": 'POST',
            "columns": [
	            { "data": "Name" },
	            { "data": "Sex" },
                { "data": "Birthday" },
                { "data": "CityName" },
                { "data": "CurrentStatus" }
            ],
            "columnDefs": [{
                "targets": [5], "render": function (data, type, row) {
                    var htmlButton = "<button class='btn btn-primary' onclick='PreviewResume(" + row['StudentID'] + ")'>简历</button> ";
                    htmlButton += "<button class='btn btn-primary'  onclick=\"SendInvitation(" + row['StudentID'] + ",'" + row['Name'] + "')\">邀请</button>";
                    return "<td>" + htmlButton + "</td>";
                }
            }]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": dataJson
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}
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
//条件查询按钮
function SearchEnterpriseInfo() {
    var Obj = Object.create(EnterpriseSearchViewModelObj);
    Obj.City = $("#selCity").val();
    Obj.Job = $("#TxtJobName").val();
    Obj.SkillKey = $("#TxtSkillKey").val();
    Obj.state = $("#satusOpstion").val();
    BindDatatable('/Student/QuerySimilarStudentInfoJson', Obj);
}
//城市选择
$('#selProvince').change(function () {
    $('#selCity').empty();
    var ajaxUrl = "/Resume/GetCityListByParentAID";
    var ParentAID = $('#selProvince').val();
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: "ParentAID=" + ParentAID,
        success: function (data) {
            //填充城市下拉框
            $.each(data, function (i, item) {
                $('#selCity').append("<option value='" + item.AreaID + "'>" + item.AreaName + "</option>");
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
});
//显示简历详情
function PreviewResume(ID) {
    var ajaxUrl = getRootPath() + "/Resume/PreviewResume?SID=" + ID;
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
//错误信息弹窗
function errorAlter(ErrorInfo) {
    noty({
        text: ErrorInfo,
        layout: 'topRight',
        type: 'error',
    });
}
//技术关键词标签
//$('#TxtSkillKey').tagsInput();
