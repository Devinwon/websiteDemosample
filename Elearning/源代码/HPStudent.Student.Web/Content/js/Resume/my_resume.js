//jquery页面加载事件
$(function () {
    //移出html类
    $("html").removeClass("body-full-height");
    var workExpObj = {
        WID: 0,
        SID: 0,
        Company: '',
        Position: '',
        startDate: '',
        EndDate: '',
        JobContent: ''
    };
    var projectExpObj = {
        PID: 0,
        SID: 0,
        ProjectName: '',
        Position: '',
        startDate: '',
        EndDate: '',
        JobContent: ''
    };
    var educationExpObj = {
        Did: 0,
        StudentID: 0,
        StartDate: '',
        EndDate: '',
        School: '',
        Major: '',
        Year: 0
    };
    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {
        $(this).parents(".message-box").removeClass("open");
    });
    //弹出确认窗口关闭  -- 确定
    $("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {
        $(this).parents(".message-box").removeClass("open");
    });
    //悬浮框
    var toc = $("#tocify").tocify({ context: ".tocify-content", showEffect: "fadeIn", extendPage: false, selectors: "h2, h3, h4" });
    //显示编辑区域
    $(".FormAdd").click(function () {
        var nowid = $(this).attr("id");
        if ($(this).children("span").attr("class") == "fa fa-plus") {
            ShowEidt(nowid);
        }
        else {
            DisplayEidt(nowid);
        }
    });
    //删除按钮
    $(".FormDelete").click(function () {
        var nowid = $(this).attr("id");
        DeletAlert(nowid);
    });
    //取消按钮
    $(".FormRenounce").click(function () {
        var nowid = $(this).attr("id");
        DisplayEidt(nowid);
    });
    //工作经验显示的编辑按钮
    $(".workingEidt").click(function () {
        ShowEidt("workingAdd");
        $(".FormDelete").show();

        var vObjID = $(this).attr('tag');
        var v1 = $(this).parents('.list-group-item').find('.item-company').text();
        var v2 = $(this).parents('.list-group-item').find('.item-position').text();
        var v3 = $(this).parents('.list-group-item').find('.item-startdate').attr('tag');
        var v4 = $(this).parents('.list-group-item').find('.item-enddate').attr('tag');
        var v5 = $(this).parents('.list-group-item').find('.item-jobcontent').text();
        $('#workingSave').attr('tag', vObjID);
        $("#CompanyName").val(v1);
        $('#JobPosition').val(v2);
        $('#job_startdate').val(v3);
        $('#job_enddate').val(v4);
        $('#jobContent').val(v5);
    });
    //项目经验显示的编辑按钮
    $(".projectEidt").click(function () {
        ShowEidt("projectAdd");
        $(".FormDelete").show();
        var vObjID = $(this).attr('tag');
        var v1 = $(this).parents('.list-group-item').find('.item-proname').text();
        var v2 = $(this).parents('.list-group-item').find('.item-position').text();
        var v3 = $(this).parents('.list-group-item').find('.item-startdate').attr('tag');
        var v4 = $(this).parents('.list-group-item').find('.item-enddate').attr('tag');
        var v5 = $(this).parents('.list-group-item').find('.item-jobcontent').text();
        $('#projectSave').attr('tag', vObjID);
        $("#projectName").val(v1);
        $('#projectJob').val(v2);
        $('#pro_startdate').val(v3);
        $('#pro_enddate').val(v4);
        $('#proContent').val(v5);
    });
    //学习履历显示的编辑按钮
    $(".studyEidt").click(function () {
        ShowEidt("studyAdd");
        $(".FormDelete").show();
        var vObjID = $(this).attr('tag');
        var v1 = $(this).parents('.list-group-item').find('.item-school').text();
        var v2 = $(this).parents('.list-group-item').find('.item-year').text();
        var v3 = $(this).parents('.list-group-item').find('.item-startdate').attr('tag');
        var v4 = $(this).parents('.list-group-item').find('.item-enddate').attr('tag');
        var v5 = $(this).parents('.list-group-item').find('.item-major').text();

        $('#studySave').attr('tag', vObjID);
        $("#schoolname").val(v1);
        $('#edu_year').val(v2);
        $('#edu_startdate').val(v3);
        $('#edu_enddate').val(v4);
        $('#major').val(v5);
    });
    //工作经验保存按钮
    $('#workingSave').click(function () {
        var vObjID = $(this).attr('tag');
        var WorkExpObj = Object.create(workExpObj);
        WorkExpObj.WID = $(this).attr('tag');
        WorkExpObj.SID = $('#divWorkingShow').attr('tag');
        WorkExpObj.Company = $('#CompanyName').val();
        WorkExpObj.Position = $('#JobPosition').val();
        WorkExpObj.startDate = $('#job_startdate').val();
        WorkExpObj.EndDate = $('#job_enddate').val();
        WorkExpObj.JobContent = $('#jobContent').val();
        //添加必填条件验证
        if (WorkExpObj.Company == "") {
            noteError("公司名称不能为空！");
            return;
        }
        if (WorkExpObj.Position == "") {
            noteError("职位不能为空！");
            return;
        }
        if (WorkExpObj.startDate == "") {
            noteError("起始时间不能为空！");
            return;
        }
        if (WorkExpObj.EndDate == "") {
            noteError("结束时间不能为空！");
            return;
        }
        var endTime1 = new Date(Date.parse(WorkExpObj.EndDate));
        var startTime1 = new Date(Date.parse(WorkExpObj.startDate));
        if (startTime1 > endTime1) {
            noteError("结束时间不能小于开始时间！");
            return;
        }
        if (vObjID == "undefined" || vObjID < 1) {  //添加              
            var ajaxUrl = '/Resume/CreateWorkExperience';
            if (WorkExpObj.EndDate == "undefined" || WorkExpObj.EndDate == "") {
                WorkExpObj.EndDate = '';
            }
            //执行异步添加
            $.ajax({
                type: "post",
                url: ajaxUrl,
                dataType: "json",
                data: WorkExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'fail',
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    noteError(errorThrown);
                }
            });

        } else {
            var ajaxUrl = '/Resume/UpdateWorkExperienceByID';
            //update some object 
            //执行异步添加
            $.ajax({
                type: "post",
                url: ajaxUrl,
                dataType: "json",
                data: WorkExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'fail',
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    noteError(errorThrown);
                }
            });
            //
        }
    });
    //项目经验保存按钮
    $('#projectSave').click(function () {
        var vObjID = $(this).attr('tag');
        var ProjectExpObj = Object.create(projectExpObj);
        ProjectExpObj.PID = $(this).attr('tag');
        ProjectExpObj.SID = $('#divProjectShow').attr('tag');
        ProjectExpObj.ProjectName = $('#projectName').val();
        ProjectExpObj.Position = $('#projectJob').val();
        ProjectExpObj.startDate = $('#pro_startdate').val();
        ProjectExpObj.EndDate = $('#pro_enddate').val();
        ProjectExpObj.JobContent = $('#proContent').val();
        //添加必填条件验证
        if (ProjectExpObj.ProjectName == "") {
            noteError("项目名称不能为空！");
            return;
        }
        if (ProjectExpObj.Position == "") {
            noteError("职责名称不能为空！");
            return;
        }
        if (ProjectExpObj.startDate == "") {
            noteError("起始时间不能为空！");
            return;
        }
        if (ProjectExpObj.EndDate == "") {
            noteError("结束时间不能为空！");
            return;
        }
        var endTime1 = new Date(Date.parse(ProjectExpObj.EndDate));
        var startTime1 = new Date(Date.parse(ProjectExpObj.startDate));
        if (startTime1 > endTime1) {
            noteError("结束时间不能小于开始时间！");
            return;
        }
        if (typeof (vObjID) == "undefined" || vObjID < 1) {  //添加              
            var ajaxUrl = '/Resume/CreateProjectExperience';
            if (ProjectExpObj.EndDate == "undefined" || ProjectExpObj.EndDate == "") {
                ProjectExpObj.EndDate = '';
            }
            //执行异步添加
            $.ajax({
                type: "post",
                url: ajaxUrl,
                dataType: "json",
                data: ProjectExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
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

        } else {
            var ajaxUrl = '/Resume/UpdateProjectExperienceByID';
            //update some object 
            //执行异步添加
            $.ajax({
                type: "post",
                url: ajaxUrl,
                dataType: "json",
                data: ProjectExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'fail',
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    noteError(errorThrown);
                }
            });
            //
        }
    })
    //教育经历保存按钮
    $('#studySave').click(function () {
        var vObjID = $(this).attr('tag');
        var EducationExpObj = Object.create(educationExpObj);
        EducationExpObj.Did = $(this).attr('tag');
        EducationExpObj.StudentID = $('#divStudyShow').attr('tag');
        EducationExpObj.School = $('#schoolname').val();
        EducationExpObj.Major = $('#major').val();
        EducationExpObj.StartDate = $('#edu_startdate').val();
        EducationExpObj.EndDate = $('#edu_enddate').val();
        EducationExpObj.Year = $('#edu_year').val();
        //添加必填条件验证 
        if (EducationExpObj.StartDate == "") {
            noteError("起始时间不能为空！");
            return;
        }
        if (EducationExpObj.EndDate == "") {
            noteError("结束时间不能为空！");
            return;
        }
        var endTime1 = new Date(Date.parse(EducationExpObj.EndDate));
        var startTime1 = new Date(Date.parse(EducationExpObj.StartDate));
        if (startTime1 > endTime1) {
            noteError("结束时间不能小于开始时间！");
            return;
        }
        if (EducationExpObj.School == "") {
            noteError("学校名称不能为空！");
            return;
        }
        if (EducationExpObj.Major == "") {
            noteError("专业名称不能为空！");
            return;
        }
        if (EducationExpObj.Year == "") {
            noteError("毕业年份不能为空！");
            return;
        }
        if (EducationExpObj.EndDate == "undefined" || EducationExpObj.EndDate == "") {
            EducationExpObj.EndDate = '';
        }
        if (typeof (vObjID) == "undefined" || vObjID < 1) {  //添加    
            var ajaxUrl = '/Resume/CreateEducationRecord';
            //执行异步添加
            $.ajax({
                type: "Post",
                url: ajaxUrl,
                dataType: "json",
                data: EducationExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'fail',
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    noteError(errorThrown);
                }
            });

        } else {
            var ajaxUrl = '/Resume/UpdateEducationRecordByID';
            //update some object 
            //执行异步添加
            $.ajax({
                type: "post",
                url: ajaxUrl,
                dataType: "json",
                data: EducationExpObj,
                success: function (data) {
                    if (data.ResultState == 0) {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'success',
                        });
                        var actioinFun = window.setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        noty({
                            text: data.ResultMsg,
                            layout: 'topRight',
                            type: 'fail',
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    noteError(errorThrown);
                }
            });
            //
        }
    });
    //城市选择
    $('#selProvince').change(function () {
        $('#selCity').empty();
        var ajaxUrl = "/Resume/GetCityListByParentAID";
        var ParentAID = $('#selProvince').val();

        if (ParentAID == "== 选择省 ==") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }

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
                noteError(errorThrown);
            }
        });
    });

    $('#SaveResumeChoice').click(function () {
        var sid = $(this).attr('tag');
        var cityID = $('#selCity').val();
        if (cityID == "undefined" || cityID == "" || cityID <= 0) {
            noty({
                text: '无效的城市地区选项!',
                layout: 'topRight',
                type: 'error',
            });
            return false;
        }
        var statusID = $('#satusOpstion').val();
        if (statusID == "undefined" || statusID == "" || statusID < 0) {
            noty({
                text: '无效的求职状态选项!',
                layout: 'topRight',
                type: 'error',
            });
            return false;
        }
        var ajaxUrl = '/Resume/CreateOrUpdateResumeBasic';
        var ResumeBasic = { SID: sid, City: cityID, Status: statusID };
        //执行异步添加
        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            data: ResumeBasic,
            success: function (data) {
                if (data.ResultState == 0) {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'success',
                    });
                    var actioinFun = window.setTimeout(function () {
                        window.location.reload();
                    }, 500);
                } else {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'fail',
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                noteError(errorThrown);
            }
        });
    });

});
//显示编辑区共用方法
function ShowEidt(idText) {
    //禁用其他的增加按钮
    if (!$("span").hasClass("fa-minus")) {
        switch (idText) {
            //工作经验
            case "workingAdd":
                $("#divWorkingShow").hide();
                $("#divWorkingWrite").show();
                $("#workingAdd").children("span").removeClass('fa fa-plus');
                $("#workingAdd").children("span").addClass('fa fa-minus');
                break;
                //项目经验
            case "projectAdd":
                $("#divProjectShow").hide();
                $("#divProjectWrite").show();
                $("#projectAdd").children("span").removeClass('fa fa-plus');
                $("#projectAdd").children("span").addClass('fa fa-minus');
                break;
                //学习履历
            case "studyAdd":
                $("#divStudyShow").hide();
                $("#divStudyWrite").show();
                $("#studyAdd").children("span").removeClass('fa fa-plus');
                $("#studyAdd").children("span").addClass('fa fa-minus');
                break;
        }
    }
}
//隐藏编辑区共用方法
function DisplayEidt(idText) {
    switch (idText) {
        //工作经验
        case "workingAdd":
        case "workingSave":
        case "workingRenounce":
        case "workingDelete":
            $("#divWorkingWrite").hide();
            $("#divWorkingShow").show();
            $("#workingAdd").children("span").removeClass('fa fa-minus');
            $("#workingAdd").children("span").addClass('fa fa-plus');
            $(".FormDelete").hide();
            break;
            //项目经验
        case "projectAdd":
        case "projectSave":
        case "projectRenounce":
        case "projectDelete":
            $("#divProjectWrite").hide();
            $("#divProjectShow").show();
            $("#projectAdd").children("span").removeClass('fa fa-minus');
            $("#projectAdd").children("span").addClass('fa fa-plus');
            $(".FormDelete").hide();
            break;
            //学习履历
        case "studyAdd":
        case "studySave":
        case "studyRenounce":
        case "studyDelete":
            $("#divStudyWrite").hide();
            $("#divStudyShow").show();
            $("#studyAdd").children("span").removeClass('fa fa-minus');
            $("#studyAdd").children("span").addClass('fa fa-plus');
            $(".FormDelete").hide();
            break;
    }
}
//删除共用弹窗
function DeletAlert(deleteInfo) {
    var DeleteFun = "";
    switch (deleteInfo) {
        case "workingDelete":
            DeleteFun = "DeleteWorkingInfo()";
            break;
        case "projectDelete":
            DeleteFun = "DeleteProjectInfo()";
            break;
        case "studyDelete":
            DeleteFun = "DeleteStudyInfo()";
            break;
    }
    myConfirm("确定要删除此信息吗？", "数据删除后将无法恢复！", DeleteFun, "#pop_modaldialog");
}
//工作经验删除方法
function DeleteWorkingInfo() {
    //删除后隐藏
    DisplayEidt("workingDelete");
    $("#pop_modaldialog").find(".message-box").removeClass("open");
    //执行数据删除操作
    var ajaxUrl = "/Resume/DeletWorkExperienceByID?id=" + $('#workingSave').attr('tag');
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    window.location.reload();
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            noteError(errorThrown);
        }
    });
}
//项目经验删除方法
function DeleteProjectInfo() {
    //删除后隐藏
    DisplayEidt("projectDelete");
    $("#pop_modaldialog").find(".message-box").removeClass("open");

    //执行数据删除操作
    var ajaxUrl = "/Resume/DeleteProjectExperienceByID?id=" + $('#projectSave').attr('tag');
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    window.location.reload();
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            noteError(errorThrown);
        }
    });

}
//学习履历删除方法
function DeleteStudyInfo() {
    //删除后隐藏
    DisplayEidt("studyDelete");
    $("#pop_modaldialog").find(".message-box").removeClass("open");
    //执行数据删除操作
    var ajaxUrl = "/Resume/DeleteEducationRecordByID?id=" + $('#studySave').attr('tag');
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    window.location.reload();
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            noteError(errorThrown);
        }
    });
}
//新增工作经验时清空文本
function ClearJobText() {
    $('#workingSave').attr('tag', '0');
    $('#CompanyName').val('');
    $('#JobPosition').val('');
    $('#job_startdate').val('');
    $('#job_enddate').val('');
    $('#jobContent').val('');
}
//新增项目经验时清空文本
function ClearProjectText() {
    $('#projectSave').attr('tag', '0');
    $('#projectName').val('');
    $('#projectJob').val('');
    $('#pro_startdate').val('');
    $('#pro_enddate').val('');
    $('#proContent').val('');
}
//新增教育经历时清空文本
function ClearEducationText() {
    $('#studySave').attr('tag', '0');
    $('#schoolname').val('');
    $('#major').val('');
    $('#edu_startdate').val('');
    $('#edu_enddate').val('');
    $('#edu_year').val('');
}

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
//基本信息编辑按钮
function basicOnclick() {
    //重新绑定城市
    var hiddenCity = $("#hiddenCity").val();
    //如果已经绑定城市就直接选中
    var hiddenIsCheckProvince = $("#hiddenIsCheckProvince").val();
    if (hiddenIsCheckProvince != "" && hiddenIsCheckProvince != 0) {
        $('#selProvince').val(hiddenIsCheckProvince);
        $('#selCity').empty();
        var ajaxUrl = "/Resume/GetCityListByParentAID";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            data: "ParentAID=" + hiddenIsCheckProvince,
            success: function (data) {
                //填充城市下拉框
                $.each(data, function (i, item) {
                    $('#selCity').append("<option value='" + item.AreaID + "'>" + item.AreaName + "</option>");
                    $('#selCity').val($("#hiddenCity").val());
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                noteError(errorThrown);
            }
        });
    }
}
function noteError(msg) {
    noty({
        text: msg,
        layout: 'topRight',
        type: 'fail',
    });
}