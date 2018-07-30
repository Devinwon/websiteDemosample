var myDatatalbe;
var ProjectItem = {
    PID: 0,
    ProjectName: "",
    PPT: "",
    WORD: "",
    PDF: "",
    Video: "",
    URL:"",
    CreateDate: "",
    EditDate: "",
};

/// <reference path="page_projects_detail.js" />
$(function () {
    //获取PID的参数
    var PID = GetQueryString("PID");
    //获取项目的信息
    GetProjectDetail(PID);
    //获取项目子项的列表
    var ajaxUrl = "/Projects/GetProjectItemListByPID?PID=" + PID;
    BindDatatable(ajaxUrl);

    //添加按钮被点击时触发
    $('#btnAddProjectItem').on('click', function (e) {
        var ajaxUrl = "/Projects/Pop_ProjectItem_Add?PID" + PID;
        OpenProjectItemWin(ajaxUrl);
    });

    //弹出窗口中的添加按钮被点击
    $("#pop_modaldialog").delegate("#btnAddProjectItem", "click", function () {
        AddProjectItem(PID);

    });

    //弹出窗口中的修改按钮被点击
    $("#pop_modaldialog").delegate("#btnEditProjectItem", "click", function () {
        AjaxEditProjectItem();

    });

    //列表
    $("#Project-list").delegate(".btn-primary", "click", function () {
        var ClickItemID = $(this).attr("data-id");
        var ClickItemType = $(this).attr("data-type");

        if (ClickItemType == "edit") {
            //编辑按钮，弹出修改界面
            AjaxGetPop_ProjectItem_Edit(ClickItemID);

        } else if (ClickItemType == "delete")
        {
            //删除按钮
            myConfirm("确定要删除当前选中的课程吗？", "该操作会删除该课程下面的所有资源！", "DelProjectItem(" + ClickItemID + ")", "#pop_modaldialog");
        }

    });
});

function AjaxEditProjectItem()
{
    var projectitem = Object.create(ProjectItem);
    projectitem.ID = $("#hideID").val();
    projectitem.ProjectName = $("#ProjectName").val();
    projectitem.PPT = $("#PPT").val();
    projectitem.PDF = $("#PDF").val();
    projectitem.Video = $("#Video").val();
    projectitem.URL = $("#URL").val();
    //课程名称必填项基本验证
    if (typeof projectitem.ProjectName == undefined || projectitem.ProjectName == "") {     
        noty({
            text: "课程名称为必填项！",
            layout: 'topRight',
            type: 'error',
        });
        return false;
    }

    $.ajax({
        type: "post",
        url: "/Projects/EditProjectItem",
        dataType: "json",
        data: projectitem,
        success: function (data) {
            if (data.ResultState == 0) {
                $('#edit-projectitem').modal('hide');
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
        complete: function (data) {
            var PID = GetQueryString("PID");
            var ajaxUrl = "/Projects/GetProjectItemListByPID?PID=" + PID;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function AjaxGetPop_ProjectItem_Edit(ID)
{
    var ajaxUrl = getRootPath() + "/Projects/Pop_ProjectItem_Edit?ID=" + ID;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-projectitem').modal('show');
        },
        complete: function (html) {
            ////修复taginput显示
            //feTagsinput();
            //feSelect();
            //feBsFileInput();
        }
    });
}
//删除项目子项（课程）
function DelProjectItem(ID) {
    //关闭弹出提示
    $("#pop_modaldialog").find(".message-box").removeClass("open");
    var ajaxUrl = getRootPath() + "/Projects/DelProjectItem";

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: "ID=" + ID,
        dataType: "json",
        success: function (html) {
            if (html.ResultState == 0) {
                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        complete: function (ResultJson) {
            
            //重新绑定Datatable
            var PID = GetQueryString("PID");
            var ajaxUrl = "/Projects/GetProjectItemListByPID?PID=" + PID;
            BindDatatable(ajaxUrl);
        }
    });
}

//添加项目子项（课程）
function AddProjectItem(PID) {
    var projectitem = Object.create(ProjectItem);
    projectitem.PID = PID;
    projectitem.ProjectName = $("#ProjectName").val();
    projectitem.PPT = $("#PPT").val();
    projectitem.PDF = $("#PDF").val();
    projectitem.Video = $("#Video").val();
    projectitem.URL = $("#URL").val();
    // 课程名称必填项基本验证
    if (typeof projectitem.ProjectName == undefined ||  projectitem.ProjectName=="") {        
        noty({
            text: "课程名称为必填项！",
            layout: 'topRight',
            type: 'error',
        });
        return false;
    }

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
        complete: function (data) {
            var PID = GetQueryString("PID");
            var ajaxUrl = "/Projects/GetProjectItemListByPID?PID=" + PID;
            BindDatatable(ajaxUrl);
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
            $("#URL").val("");
        }
    });
}


function GetProjectDetail(PID)
{
    $.ajax({
        type: "post",
        url: "/Projects/GetProjectDetailByPID",
        dataType: "json",
        data: {PID:PID},
        success: function (data) {
            $('#lbProjectName').html(data.ProjectName);
            $('#lbTeacherName').html(data.TeacherName);
            $('#lbClassHour').html(data.ClassHour);

            var arrayTech = data.UseTechnology.split(",");
            $('#lbUseTechnology').empty();
            for (var techItem in arrayTech) {
                $('#lbUseTechnology').append('<li><a href="#"><span class="fa fa-tag"></span> ' + arrayTech[techItem] + '</a></li>');
            }
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
	            { "data": "ProjectName" },
	            { "data": "CreateDate" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 1,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        return GetLocalTime(row.CreateDate);
                    }
                },
                {
                    "targets": 2,
                    "render": function (data, type, row) {
                        var pdfDisable = "";
                        var pptDisable = "";
                        var VideoDisable = "";
                        var URLDisable = "";
                        if (row.PDF == "") {
                            pdfDisable = "disabled";
                        }
                        if (row.PPT == "") {
                            pptDisable = "disabled";
                        }
                        if (row.Video == "") {
                            VideoDisable = "disabled";
                        }
                        if (row.URL == "") {
                            URLDisable = "disabled";
                        }
                        return "<button class=\"btn btn-info btn-sm " + pdfDisable + "\" data-type=\"pdf\" data-src=\"" + row.PDF + "\">"
                                + "    <span class=\"fa fa-cloud-download\"></span>下载"
                                + "</button>"
                                + " <button class=\"btn btn-info btn-sm " + pptDisable + "\" data-type=\"ppt\" data-src=\"" + row.PPT + "\">"
                                + "    <span class=\"glyphicon glyphicon-picture\"></span>PPT"
                                + "</button>"
                                + " <button class=\"btn btn-info btn-sm " + VideoDisable + "\" data-type=\"video\" data-src=\"" + row.Video + "\">"
                                + "    <span class=\"fa fa-film\"></span>视频"
                                + "</button>"
                                + " <button class=\"btn btn-info btn-sm " + URLDisable + "\" data-type=\"url\" data-src=\"" + row.URL + "\">"
                                + "    <span class=\"fa fa-globe\"></span>网站"
                                + "</button>"
                    }
                }, {
                    "targets": 3,
                    "render": function (data, type, row) {
                        return "<button class=\"btn btn-primary btn-sm \" data-type=\"edit\" data-id=\"" + row.ID + "\">"
                                + "    <span class=\"fa fa-pencil\"></span> 编辑"
                                + "</button>"
                                + " <button class=\"btn btn-primary btn-sm disabled\" data-type=\"delete\" data-id=\"" + row.ID + "\">"
                                + "    <span class=\"fa fa-times\"></span> 删除"
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

//操作菜单弹出功能; 
//任务列表的文档、PPT和视频项添加事件

$("#Data_SelectList").delegate(".btn-info", "click", function () {
    var ajaxUrl;
    var fileType = $(this).attr("data-type");

    if (fileType == "ppt") {
        //查看按钮
        ajaxUrl = "/Player/pdf?file=" + $(this).attr("data-src");
        openWin(ajaxUrl, fileType + "View");
    } else if (fileType == "video") {
        //测试按钮  
        ajaxUrl = "/Player/video?src=" + $(this).attr("data-src");
        openWin(ajaxUrl, fileType + "View");
    } else if (fileType == "url") {
        //测试按钮  
        ajaxUrl = $(this).attr("data-src");
        openWin(ajaxUrl, fileType + "View");
    } else if (fileType == "pdf") {
        //测试按钮  
        ajaxUrl = "/Player/Download?f=" + $(this).attr("data-src");
        window.open(ajaxUrl);
    }

    //openWin(ajaxUrl, fileType + "View");

    //alert(fileType + " : " + ajaxUrl);
    //在新窗口中打开PDF或者VIDEO
  
    //Ajax获取页面
    //$.ajax({
    //    type: "GET",
    //    url: ajaxUrl,
    //    success: function (html) {
    //        $("#pop_modaldialog").empty();
    //        $('#pop_modaldialog').append(html);
    //        $('#Task_Details').modal('show');
    //    },
    //    complete: function (html) {
    //        //修复icheck无法显示样式的bug
    //        $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
    //    }
    //});
});
