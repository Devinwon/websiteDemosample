var myDatatalbe;
var ProjectItem = {
    PID: 0,
    ProjectName: "",
    PPT: "",
    WORD: "",
    PDF: "",
    Video: "",
    CreateDate: "",
    EditDate: "",
};

/// <reference path="page_projects_detail.js" />
$(function () {
    //获取PID的参数
    var RID = GetQueryString("RID");
    //获取项目的信息
    GetRsourceDetail(RID);
    //获取项目子项的列表
    var ajaxUrl = "/Resource/GetResourceItemListByRID?RID=" + RID;
    BindDatatable(ajaxUrl);

    //添加按钮被点击时触发
    $('#btnAddProjectItem').on('click', function (e) {
        var ajaxUrl = "/Projects/Pop_ProjectItem_Add?PID" + PID;
        OpenProjectItemWin(ajaxUrl);
    });

    //弹出窗口中的添加按钮被点击
    $("#pop_modaldialog").delegate("#btnAddProjectItem", "click", function () {
        AddProjectItem(PID);
        BindDatatable(ajaxUrl);
    });
});

function AddProjectItem(PID) {
    var projectitem = Object.create(ProjectItem);
    projectitem.PID = PID;
    projectitem.ProjectName = $("#ProjectName").val();
    projectitem.PPT = $("#PPT").val();
    projectitem.PDF = $("#PDF").val();
    projectitem.Video = $("#Video").val();

    $.ajax({
        type: "post",
        url: "/Projects/AddProjectItem",
        dataType: "json",
        data: projectitem,
        success: function (data) {
            if (data.ResultState == 0) {
                $('#add-projectitem').modal('hide');
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
}

function OpenProjectItemWin(ajaxUrl) {
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#add-projectitem').modal('show');
        },
        complete: function (html) {
            //修复taginput显示
            //feSelect();
            var filename = getNowFormatDate();
            $("#ProjectName").val();
            $("#PDF").val("/"+filename+".pdf");
            $("#PPT").val("/" + filename + ".pdf");
            $("#Video").val("/" + filename + ".mp4");
        }
    });
}


function GetRsourceDetail(RID)
{
    $.ajax({
        type: "post",
        url: "/Resource/GetResourceDetailByRID",
        dataType: "json",
        data: {RID:RID},
        success: function (data) {
            $('#lbResourceName').html(data.ResourceName);
            $('#lbTeacherName').html(data.TeacherName);
            $('#lbCourceHour').html(data.CourceHour);
            $('#lbResourceFrom').html(data.ResourceFrom);            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

//绑定Datatable
function BindDatatable(ajaxUrl) {
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
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "CourseName" },
	            { "data": "CreateDate" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "render": function (data, type, row) {
                        return "<a href=\"" + row.URL +"\">" + row.CourseName + "</a>";
                    }
                },
                {
                    "targets": 1,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        return GetLocalTime(row.CreateDate);
                    }
                },
                {
                    "targets":2,
                    "render": function (data, type, row) {
                        var pdfDisable = "";
                        var pptDisable = "";
                        var VideoDisable = "";                        
                        return "<button class=\"btn btn-primary btn-sm \" data-type=\"pdf\" data-action=\"edit\">"
                                +"    <span class=\"fa fa-paste\"></span> 修改"
                                +"</button>"
                                + " <button class=\"btn btn-primary btn-sm \" data-type=\"pdf\" data-action=\"delete\">"
                                + "    <span class=\"glyphicon glyphicon-picture\"></span> 删除"
                                + "</button>"
                    }
                },
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() +""+ month + "/" + date.getFullYear() + month + strDate
            + "_" + date.getHours() + date.getMinutes() + date.getSeconds()
            + "_" + date.getMilliseconds();
    return currentdate;
}