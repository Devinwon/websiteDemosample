//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var starPage; //起始页
var childDatatable;

$(function () {
    $("#selSchool").change(function () {
        //加载年级
        $("#selGrade").empty();
        $('#selGrade').append("<option>== 选择年级 ==</option>");

        var date = new Date;
        var year = date.getFullYear();
        var startYear = 2009;
        for (var i = year - startYear; i > 0; i--) {
            $('#selGrade').append("<option value='" + (startYear + i) + "'>" + (startYear + i) + "</option>");
        }
    });

    $("#selGrade").change(function () {
        //加载班级
        $("#selCLass").empty();
        $('#selCLass').append("<option>== 选择班级 ==</option>");

        var SchoolID = $('#selSchool').val();
        var Year = $('#selGrade').val();

        if (SchoolID == "== 选择校区 ==") {
            myAlert("请先选择校区！", "", "#pop_modaldialog");
            return false;
        }
        if (Year == "== 选择年级 ==") {
            myAlert("请选择年级！", "", "#pop_modaldialog");
            return false;
        }

        $.ajax({
            type: "post",
            url: "/Student/GetStudentClassListBySchoolIDAndYear",
            dataType: "json",
            data: "SchoolID=" + SchoolID + "&Year=" + Year,
            success: function (data) {

                $.each(data, function (i, item) {
                    $('#selCLass').append("<option value='" + item.CID + "'>" + item.CName + "</option>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    });

    $("#exam-list").delegate(".btn-primary", "click", function () {
        var arr = $(this).attr("data-id");
        var StudentID = arr.split(',')[0];
        var Term = arr.split(',')[1];
        if ($(this).attr("data-action") == "edit") {
            var ajaxUrl = "Pop_Student_Index_Edit_Edit?StudentID=" + StudentID + "&Term=" + Term;
            $.ajax({
                type: "GET",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-student-mark').modal('show');
                },
                complete: function (html) {
                    //$('#Pop_selSchool').val($('#selSchool').find("option:selected").text());
                    //$('#Pop_selYear').val($('#selYear').find("option:selected").text());
                }
            });

        } else {
            myConfirm("确定要删除此考试成绩吗？", "数据删除后将无法恢复！", "DeleteMark(" + StudentID + "," + Term + ")", "#pop_modaldialog");
            return false;
        }

    });
    //成绩列表的编辑项添加事件
    $("#student-list").delegate(".btn-primary", "click", function () {

        if ($(this).attr("data-action") == "edit") {
            //编辑按钮
            var EditUrl = "Student_Index_Edit?id=" + $(this).attr("data-id") + "&" + Date.now();
            window.location.href = EditUrl;

        } else {


        }
    });


    $('#btnAddMark').on('click', function (e) {
        var ajaxUrl = "Pop_Student_Index_Edit_Add";
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-student-mark').modal('show');
            }
        });



    });
    $('#btnSearch').on('click', function (e) {

        var SchoolID = $('#selSchool').val();
        if (SchoolID == "== 选择校区 ==") {
            myAlert("请先选择校区！", "", "#pop_modaldialog");
            return false;
        }
        var Year = $('#selGrade').val();
        var CID = $('#selCLass').val();
        var StudentName = $('#StudentName').val();
        if (Year == "== 选择年级 ==") {
            Year = "";
        }
        if (CID == "== 选择班级 ==") {
            CID = "";
        }
        //   

        var ajaxUrl = "/Student/GetStudentList?SchoolID=" + SchoolID + "&CID=" + CID + "&StartYear=" + Year + "&RealName=" + encodeURI(StudentName);

        BindDatatable(ajaxUrl);


    });

    $('#btnImport').on('click', function (e) {
        //alert("导入");
        //var SchoolID = $('#selSchool').val();
        //var Year = $('#selGrade').val();
        //var CID = $('#selCLass').val();
        //var StudentName = $('#StudentName').val();

        //if (SchoolID == "== 选择校区 ==") {
        //    myAlert("请先选择校区！", "", "#pop_modaldialog");
        //    return false;
        //} else if (Year == "== 选择年级 ==") {

        //    myAlert("请选择年级！", "", "#pop_modaldialog");
        //    return false;
        //} else if (CID == "== 选择班级 ==") {
        //    myAlert("请选择年级！", "", "#pop_modaldialog");
        //    return false;
        //}


        var ajaxUrl = "Pop_Student_Import";
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#import-student').modal('show');
            },
            complete: function (html) {
                //修复上传文件样式缺失
                if ($("input.fileinput").length > 0)
                    $("input.fileinput").bootstrapFileInput();
            }
        });


    });
});







//在弹出窗口中点击导入按钮触发
function ImportStudent() {

    var SchoolID = $('#selImportSchool').val();
    if (SchoolID == "0") {
        myAlert("请选择要导入数据的校区！", "校区选择错误", "#pop_modaldialog");
        return false;
    }

    var Year = $('#selImportGrade').val();
    if (Year == "0") {
        myAlert("请选择要导入数据的班级！", "班级选择错误", "#pop_modaldialog");
        return false;
    }
    var fileName = $('#uploadFile').val();
    if (fileName == "") {
        myAlert("请选择要导入的文件！", "", "#pop_modaldialog");
        return false;
    }

    var datanumber = 0;

    var file = $('#uploadFile').val();
    //检查是否已选择上传文件
    if (file != '') {
        var filename = file.replace(/.*(\/|\\)/, '');
        var fileext = (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename.toLowerCase()) : '';
        //检查文件格式
        if (fileext == 'xlsx' || fileext == 'xls') {
            //展示等待信息
            $('#loading').ajaxStart(function () {
                $(this).show();
            }).ajaxComplete(function () {
                $(this).hide();
            });
            var ajaxUploadFile = "/Student/Upload?SchoolID=" + SchoolID + "&Year=" + Year;
            //上传excel文件
            $.ajaxFileUpload({
                url: ajaxUploadFile, //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: 'uploadFile', //文件上传域的ID
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status)  //服务器成功响应处理函数
                {

                    $.each(data, function (i, item) {
                        //myAlert((i + 1) + " 班" + "共导入数据 " + item.successNum + " 条", "导入完成", "#pop_modaldialog");
                        myAlert(item.className + " 班" + "共导入数据 " + item.successNum + " 条", "导入完成", "#pop_modaldialog");
                    })
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert(e);
                }


            });

            //$.ajaxFileUpload({
            //    url: ajaxUploadFile,
            //    secureuri: false,
            //    dataType: "json",
            //    fileElementId: 'uploadFile',
            //    success: function (data) {
            //        debugger;
            //        $.each(data, function (i, item) {
            //            myAlert(i + " 班" + "共导入数据 " + item + " 条", "导入完成", "#pop_modaldialog");
            //        })
            //    },
            //});
        }
        else {
            alert('文件格式必须是*.xls或*.xlsx');
        }
    }
    else {
        alert('请选择excel文件！')
    }

    //隐藏弹出的添加题目窗口
    $('#import-student').modal('hide');

    //重新刷新Datatable
    //。。。
}


function BindDatatable(ajaxUrl) {
    //清空Datatable
    $('#StudentInfoList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_index").dataTable({
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
                { "data": "SchoolName" },
                { "data": "CName" },
                { "data": "Avatar" },
                { "data": "RealName" },
                { "data": "PaidFee" },
                { "data": "Credits" },
                { "data": "Sex" },
                { "data": "IsActivated" },
                { "data": "StudentID" },
            ],
            "columnDefs": [
                  {
                      "targets": 2, "render": function (data, type, row) {
                          return '<img src="' + data + '" width="36" />';
                      }
                  },
                   {
                       "targets": 6, "render": function (data, type, row) {
                           return data == 0 ? '男' : '女';
                       }
                   },
                {
                    "targets": 7, "render": function (data, type, row) {
                        return data == 1 ? "  <span class='label label-info'><span class='fa fa-check'></span></span>" : " <span class='label label-danger'><span class='fa fa-times'></span>";
                    }
                },
                   {
                       "targets": 8, "render": function (data, type, row) {
                           return "<button id='btn-edit' data-action='edit' class='btn btn-primary btn-sm' data-id='" + data + "'><span class='fa fa-pencil'></span>编辑</button>";
                       }
                   },
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

function getRootPath() {
    //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
    var curWwwPath = window.document.location.href;
    //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
    var pathName = window.document.location.pathname;
    var pos = curWwwPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPaht = curWwwPath.substring(0, pos);
    return (localhostPaht);
}

function UpdateBaseInfo() {
    var MajorID = $('#selMajor').val();
    var CID = $('#selCLass').val();
    var StudentID = $('#hdnStudentID').val();
    $.ajax({
        type: "post",
        url: "/Student/UpdateBaseInfo",
        dataType: "json",
        data: "MajorID=" + MajorID + "&CID=" + CID + "&StudentID=" + StudentID,
        success: function (data) {
            myAlert("修改成功！", "", "#pop_modaldialog");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}

function UpdateStudentInfo() {

    var RealName = $('#txtName').val();
    var Brithday = $('#txtBrithday').val();
    var StudentID = $('#hdnStudentID').val();

    var StartYear = $('#selStartYear').val();
    var selUserRole = $('#selUserRole').val();
    var Sex = $('input:radio[name="optionsRadios"]:checked').val();
    $.ajax({
        type: "post",
        url: "/Student/UpdateStudentInfo",
        dataType: "json",
        data: "RealName=" + RealName + "&Brithday=" + Brithday + "&Sex=" + Sex + "&StartYear=" + StartYear + "&StudentID=" + StudentID + "&UserRole=" + selUserRole,
        success: function (data) {
            myAlert("修改成功！", "", "#pop_modaldialog");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function EditMark() {

    var txtTerm = $('#txtTerm').val();
    var Term = 1;
    if (txtTerm == "第二学年") {
        Term = 2
    } else if (txtTerm == "第三学年") {
        Term = 3
    } else {
        Term = 1;
    }
    var Examination1 = $('#txtExamination1').val();
    var Examination2 = $('#txtExamination2').val();
    var Evaluate = $('#txtEvaluate').val();
    var StudentID = $('#hdnStudentID').val();
    $.ajax({
        type: "post",
        url: "/Student/EditMark",
        dataType: "json",
        data: "Term=" + Term + "&Examination1=" + Examination1 + "&Examination2=" + Examination2 + "&Evaluate=" + Evaluate + "&StudentID=" + StudentID,
        success: function (data) {
            myAlert("修改成功！", "", "#pop_modaldialog");
            var ajaxUrl = "/Student/GetStudentScore?StudentID=" + StudentID;
            BindChildDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function AddMark() {

    var Term = $('#selTerm').val();
    var StudentID = $('#hdnStudentID').val();
    if (Term == "== 选择学年 ==") {
        myAlert("学年错误", "请选择正确学年", "#pop_modaldialog");
        return false;
    }

    //判断当前学年是否有成绩，如果已有，不能再添加成绩记录
    var haveData = false;
    var ajaxURL = "/Student/GetStudentScoreByStudentIDAndTerm?StudentID=" + StudentID + "&Term=" + Term;
    $.ajax({
        type: "POST",
        url: ajaxURL,
        dataType: "json",
        success: function (data) {
            if (data.Term == Term) {
                haveData = true;
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });


    if (haveData != false) {
        myAlert("学年错误", "此学年已有成绩，不能再次添加", "#pop_modaldialog");
        return false;
    }
    var Examination1 = $('#txtExamination1').val();
    var Examination2 = $('#txtExamination2').val();
    var Evaluate = $('#txtEvaluate').val();

    $.ajax({
        type: "post",
        url: "/Student/AddMark",
        dataType: "json",
        data: "Term=" + Term + "&Examination1=" + Examination1 + "&Examination2=" + Examination2 + "&Evaluate=" + Evaluate + "&StudentID=" + StudentID,
        success: function (data) {
            myAlert("添加成功！", "", "#pop_modaldialog");
            var ajaxUrl = "/Student/GetStudentScore?StudentID=" + StudentID;
            BindChildDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DeleteMark(StudentID, Term) {

    $.ajax({
        type: "post",
        url: "/Student/DeleteMark",
        dataType: "json",
        data: "StudentID=" + StudentID + "&Term=" + Term,
        success: function (data) {
            myAlert("删除成功！", "", "#pop_modaldialog");
            var ajaxUrl = "/Student/GetStudentScore?StudentID=" + StudentID;
            BindChildDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var StudentID = $('#hdnStudentID').val();
    var target = $(e.target).attr("href")
    if (target == "#tab-third") {
        var ajaxUrl = "/Student/GetStudentScore?StudentID=" + StudentID;
        BindChildDatatable(ajaxUrl);
        //if (!$(target).is(':empty')) {

        //    $(target).html("数据加载中...");
        //    $.ajax({
        //        type: "Post",
        //        url: ajaxUrl + "?" + Date.now(),
        //        data: "StudentID=" + StudentID,

        //        error: function (data) {
        //            alert("There was a problem");
        //        },
        //        success: function (data) {
        //            $(target).html(data);
        //        }
        //    })
        // }
    }
});



function BindChildDatatable(ajaxUrl) {
    var StudentID = $('#hdnStudentID').val();
    //清空Datatable
    $('#StudentExamList').empty();
    if (childDatatable == null) {
        childDatatable = $(".datatable_student").dataTable({
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
                { "data": "Term" },
                { "data": "Examination1" },
                { "data": "Examination2" },
                { "data": "Evaluate" },
                { "data": "Term" },

            ],
            "columnDefs": [
                {
                    "targets": 0, "render": function (data, type, row) {
                        if (data == 2) { data = "二" } else if (data == 3) { data = "三" } else { data = "一" }
                        return "第 " + data + " 学年";
                    }
                },
                   {
                       "targets": 4, "render": function (data, type, row) {
                           return "<button class='btn btn-primary btn-sm' type='button' data-action='edit' data-id='" + StudentID + "," + data + "'><span class='fa fa-pencil'></span> 编辑 </button> <button class='btn btn-primary btn-sm' type='button' data-id='" + StudentID + "," + data + "'>                                                                    <span class='fa fa-times'></span> 删除 </button>"
                           //  return "<button id='btn-edit' data-action='edit' class='btn btn-primary btn-sm' data-id='" + StudentID + "," + data + "'><span class='fa fa-pencil'></span>编辑</button><button  class='btn btn-primary btn-sm' data-id='" + StudentID + "," + data + "'><span class='fa fa-times'></span>删除</button>";
                       }
                   },
            ]
        });
        onresize();
    } else {
        var oSettings = childDatatable.fnSettings();
        oSettings.ajax = ajaxUrl;
        childDatatable.fnClearTable(0);
        childDatatable.fnDraw();
        onresize();
    }
}
