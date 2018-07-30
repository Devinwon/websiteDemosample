//数据初始化
var myDatatalbe = null; //初始化一个Datatable容器
var starPage; //起始页


$(function () {

    //专业下拉框变化
    $('#selMajor').change(function () {
        var MajorName = $(this).val();
        if (MajorName == "== 选择专业 ==") {
            $('#selCourse').empty();
            $('#selCourse').append("<option value='== 选择课程 =='>== 选择课程 ==</option>");
            return;
        }
        //加载数据前提示
        $('#selCourse').empty();
        $('#selCourse').append("<option value='数据加载中...'>数据加载中...</option>");
        //设置按钮为只读
        $("#btnSelExercise").attr("readonly", "readonly");

        //ajax加载课程数据
        $.ajax({
            type: "post",
            url: "/Exercises/GetCategoryListByMajorName",
            dataType: "json",
            data: "MajorName=" + MajorName,

            success: function (data) {
                $('#selCourse').empty();

                $('#selCourse').append("<option value='== 选择课程 =='>== 选择课程 ==</option>");
                $.each(data, function (i, item) {
                    $('#selCourse').append("<option value='" + item.CID + "'>" + item.CategoryName + "</option>");
                });

                //取消按钮只读
                $("#btnSelExercise").removeAttr("readonly");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    });

    //课程发生改变，题库相应变化 
    $('#btnSelExercise').on('click', function () {
        var CID = $('#selCourse').val();
        if (CID == "== 选择课程 ==") {
            myAlert("选择的专业或课程不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        ajaxUrl = "/Exercises/GetQA_SelectListByCID?CID=" + CID;

        //Datatable绑定数据
        BindDatatable(ajaxUrl);

        //$.ajax({
        //    type: "post",
        //    url: "/Exercises/GetQA_SelectListByCID?selMajor=" + $('#selMajor').find("option:selected").text() + "&selCourse=" + $('#selCourse').find("option:selected").text(),
        //    dataType: "json",
        //    data: "CID=" + CID,

        //    success: function (data) {
        //        //填充Datatable



        //        $.each(data, function (i, item) {
        //            $('#QA_SelectList').append(" <tr> <td>" + item.QID + "</td><td>" + item.Title + "</td>  <td><img src='/content/img/ui/star_0"
        //                + item.Level + ".gif'/></td> <td>  <button class=\"btn btn-primary btn-sm\" data-id="
        //                + item.QID + "  data-action=\"edit\">  <span class=\"fa fa-pencil\"  ></span>编辑 </button> "
        //                + "<button class=\"btn btn-primary btn-sm\" type=\"button\"   data-id="
        //                + item.QID + ">   <span class=\"fa fa-times\"></span>"
        //                + " 删除  </button>  </td> </tr>");
        //        });
        //    },
        //    error: function (XMLHttpRequest, textStatus, errorThrown) {
        //        alert(errorThrown);
        //    }

        //});
        return false;
    });

    //添加题目
    $('#btnAddExercise').on('click', function (e) {
        //如果没有选择省份或城市，弹出提示
        if ($('#selMajor').val() == "== 选择专业 ==" || $("#selCourse").val() == "== 选择课程 ==") {
            myAlert("请先选择具体的专业和课程！", "未选择具体的专业和课程前不能添加题目", "#pop_modaldialog");
            return false;
        }

        //弹出添加窗口
        var ajaxUrl = "/Exercises/Pop_Index_Add";

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-exercises-library').modal('show');
            },
            complete: function (html) {
                $('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
                $('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
                //修复icheck无法显示样式的bug
                $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
            }
        });

    });

    $('#btnImport').on('click', function (e) {
        if ($('#selMajor').val() == "== 选择专业 ==" || $("#selCourse").val() == "== 选择课程 ==") {
            myAlert("请先选择具体的专业和课程！", "未选择具体的专业和课程前不能导入题目", "#pop_modaldialog");
            return false;
        }
        var ajaxUrl = "/Exercises/Pop_Exercises_Import";
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#import-exercises').modal('show');
            },
            complete: function (html) {
                //修复上传文件样式缺失
                if ($("input.fileinput").length > 0)
                    $("input.fileinput").bootstrapFileInput();
                //获取课程名称和ID
                $('#pop_SelMajor').val($('#selMajor').find("option:selected").text());
                $('#pop_SelCourse').val($('#selCourse').find("option:selected").text());
                $('#pop_SelCourseID').val($('#selCourse').find("option:selected").val());
            }
        });
    });

    //成绩列表的编辑和删除项添加事件
    $("#class-list").delegate(".btn-primary", "click", function () {

        if ($(this).attr("data-action") == "edit") {
            //编辑按钮
            var ajaxUrl = "/Exercises/Pop_Index_Edit?QID=" + $(this).attr("data-id") + "&" + Date.now();
            $.ajax({
                type: "GET",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-exercises-library').modal('show');
                },
                complete: function (html) {
                    $('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
                    $('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
                    //修复icheck无法显示样式的bug
                    $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
                }
            });
        } else {
            //删除按钮  
            //if( confirm("确定要删除当前选中的题目吗？"))
            //{
            //    DelQuestion($(this).attr("data-id"));
            //}
            myConfirm("确定要删除当前选中的题目吗？", "题目删除后将无法恢复！", "DelQuestion(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

            return false;
        }
    });



    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".close", "click", function () {
        $('#add-exercises-library').modal('hide');
        $('#edit-exercises-library').modal('hide');
        //  $(this).parents(".message-box").removeClass("open");
    });



});



//删除题库
function DelQuestion(val) {
    $.ajax({
        type: "post",
        url: "/Exercises/DelQA_Select",
        dataType: "json",
        data: "QID=" + val,
        success: function (data) {
            //填充Datatable
            var CID = $('#selCourse').val();
            ajaxUrl = "/Exercises/GetQA_SelectListByCID?CID=" + CID;
            //Datatable绑定数据
            BindDatatable(ajaxUrl);


            //$.ajax({
            //    type: "post",
            //    url: "/Exercises/GetQA_SelectListByCID?selMajor=" + $('#selMajor').find("option:selected").text() + "&selCourse=" + $('#selCourse').find("option:selected").text(),
            //    dataType: "json",
            //    data: "CID=" + $('#selCourse').val(),
            //    success: function (data) {
            //        //填充Datatable


            //        //$('#QA_SelectList').empty();
            //        //$.each(data, function (i, item) {                       
            //        //    $('#QA_SelectList').append(" <tr> <td>" + item.QID + "</td><td>" + item.Title + "</td>  <td><img src='/content/img/ui/star_0"
            //        //        + item.Level + ".gif'/></td> <td>  <button class=\"btn btn-primary btn-sm\" data-id="
            //        //        + item.QID + "  data-action=\"edit\">  <span class=\"fa fa-pencil\"  ></span>编辑 </button> "
            //        //        + "<button class=\"btn btn-primary btn-sm\" type=\"button\"   data-id="
            //        //        + item.QID + ">   <span class=\"fa fa-times\"></span>"
            //        //        + " 删除  </button>  </td> </tr>");
            //        //});
            //    },
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert(errorThrown);
            //    }

            //});

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}

//添加题目(点击弹出框中的添加按钮时触发）
function AddQuestion() {
    //课程ID
    var CID = $('#selCourse').val();
    var Title = $("#QA_Select_Title").val();
    var A = $('#QA_Select_A').val();
    var B = $('#QA_Select_B').val();
    var C = $('#QA_Select_C').val();
    var D = $('#QA_Select_D').val();

    if (Title == "") {
        noty({
            text: "题目不能为空！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }
    //答案（可能是多选）
    var Answer = "";
    $('input:checkbox[name="checkbox"]:checked').each(function () {
        Answer += $(this).attr("value") + ",";
    });
    if (Answer == "") {
        noty({
            text: "请设置题目答案！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }
    var Answer = Answer.substring(0, Answer.length - 1);
    var AnswerAnalysis = $("#AnswerAnalysis").val();
    //题目难易程度
    var level = $('input:radio[name="level"]:checked').val();
    $.ajax({
        type: "post",
        url: "/Exercises/InsertQA_Select",
        dataType: "json",
        data: { CID: CID, Title: Title, A: A, B: B, C: C, D: D, Level: level, Answer: Answer, AnswerAnalysis: AnswerAnalysis },
        success: function (data) {
            if (data > 0) {
                noty({
                    text: "添加成功！",
                    layout: 'topRight',
                    type: 'success',
                });
            }
            else {
                noty({
                    text: "添加失败！",
                    layout: 'topRight',
                    type: 'fail',
                });
            }
            //隐藏弹出的添加题目窗口
            $('#add-exercises-library').modal('hide');
            //重新绑定题目列表
            var ajaxUrl = "/Exercises/GetQA_SelectListByCID?CID=" + CID
            BindDatatable(ajaxUrl);


            //$.ajax({
            //    type: "post",
            //    url: "/Exercises/GetQA_SelectListByCID?selMajor=" + $('#selMajor').find("option:selected").text() + "&selCourse=" + $('#selCourse').find("option:selected").text(),
            //    dataType: "json",
            //    data: "CID=" + CID,

            //    success: function (data) {
            //        //1.填充Datatable

            //        //2.关闭弹出的添加窗口
            //        $('#QA_SelectList').empty();
            //        $.each(data, function (i, item) {
            //            $('#QA_SelectList').append("<tr><td>" + item.QID + "</td><td>" + item.Title + "</td><td><img src='/content/img/ui/star_0"
            //          + item.Level + ".gif'/></td> <td><button class='btn btn-primary btn-sm' data-id="
            //          + item.QID + "  data-action='edit'> <span class='fa fa-pencil' ></span>编辑 </button> "
            //          + "<button class=\"btn btn-primary btn-sm\" type=\"button\" data-id="
            //          + item.QID + "><span class=\"fa fa-times\"></span>"
            //          + " 删除</button></td></tr>");
            //        });
            //    },
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert(errorThrown);
            //    }

            //});
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}

//修改题目
function EditQuestion() {
    //题目编号
    var QID = $('#hdnQID').val();
    //课程ID
    var CID = $('#selCourse').val();
    var Title = $("#QA_Select_Title").val();
    var A = $('#QA_Select_A').val();
    var B = $('#QA_Select_B').val();
    var C = $('#QA_Select_C').val();
    var D = $('#QA_Select_D').val();
    if (Title == "") {
        noty({
            text: "题目不能为空！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }
    //答案（可能是多选）
    var Answer = "";
    $('input:checkbox[name="checkbox"]:checked').each(function () {
        Answer += $(this).attr("value") + ",";
    });
    var Answer = Answer.substring(0, Answer.length - 1);
    if (Answer == "") {
        noty({
            text: "请设置题目答案！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }
    var AnswerAnalysis = $("#AnswerAnalysis").val();
    //题目难易程度
    var level = $('input:radio[name="level"]:checked').val();
    $.ajax({
        type: "post",
        url: "/Exercises/EditQA_Select",
        dataType: "json",
        data: { QID: QID, CID: CID, Title: Title, A: A, B: B, C: C, D: D, Level: level, Answer: Answer, AnswerAnalysis: AnswerAnalysis },
        success: function (data) {
            if (data > 0) {
                noty({
                    text: "修改成功！",
                    layout: 'topRight',
                    type: 'success',
                });
            }
            else {
                noty({
                    text: "修改失败！",
                    layout: 'topRight',
                    type: 'fail',
                });
            }
            ajaxUrl = "/Exercises/GetQA_SelectListByCID?CID=" + CID;
            //Datatable绑定数据
            BindDatatable(ajaxUrl);

            //$.ajax({
            //    type: "post",
            //    url: "/Exercises/GetQA_SelectListByCID?selMajor=" + $('#selMajor').find("option:selected").text() + "&selCourse=" + $('#selCourse').find("option:selected").text(),
            //    dataType: "json",
            //    data: "CID=" + CID,

            //    success: function (data) {
            //        $('#QA_SelectList').empty();
            //        $.each(data, function (i, item) {
            //            $('#QA_SelectList').append("<tr><td>" + item.QID + "</td><td>" + item.Title + "</td><td><img src='/content/img/ui/star_0"
            //          + item.Level + ".gif'/></td> <td><button class='btn btn-primary btn-sm' data-id="
            //          + item.QID + "  data-action='edit'> <span class='fa fa-pencil' ></span>编辑 </button> "
            //          + "<button class=\"btn btn-primary btn-sm\" type=\"button\" data-id="
            //          + item.QID + "><span class=\"fa fa-times\"></span>"
            //          + " 删除</button></td></tr>");
            //        });
            //    },
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert(errorThrown);
            //    }

            //});
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $('#edit-exercises-library').modal('hide');
}

//在弹出窗口中点击导入按钮触发
function ImportExercises() {

    var CID = $('#pop_SelCourseID').val();
    if (CID == "0") {
        noty({
            text: "无法获取正确的课程编号！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }

    var datanumber = 0;

    var file = $('#uploadFile').val();
    //检查是否已选择上传文件
    if (file != '') {
        //设置按钮为导入中
        $(".modal-footer .btn-primary").html("导入中...").attr("readonly", "readonly");


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
            var ajaxUploadFile = "/Exercises/Upload?CID=" + CID;
            //上传excel文件
            $.ajaxFileUpload({
                url: ajaxUploadFile,
                secureuri: false,
                dataType: "json",
                fileElementId: 'uploadFile',
                success: function (data, status)  //服务器成功响应处理函数
                {

                    noty({
                        text: "成功导入【" + data.ResultMsg + "】条数据！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    //隐藏弹出的导入题库窗口
                    //$("#pop_modaldialog").empty();
                    $('#import-exercises').modal('hide');

                    //重新刷新Datatable
                    ajaxUrl = "/Exercises/GetQA_SelectListByCID?CID=" + CID;
                    BindDatatable(ajaxUrl);

                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert(e);
                    $(".modal-footer .btn-primary").html("导入").removeAttr("readonly");
                }
            });

        }
        else {

            noty({
                text: "上传的文件必须是.xls或者.xlsx！",
                layout: 'topRight',
                type: 'warning',
            });
        }
    }
    else {
        noty({
            text: "请先选择正确的Excel文件！",
            layout: 'topRight',
            type: 'warning',
        });
    }


}

//绑定Datatable
function BindDatatable(ajaxUrl) {

    //清空Datatable
    $('#QA_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_question").dataTable({
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
	            { "data": "QID" },
	            { "data": "Title" },
	            { "data": "Level" },
	            { "data": "Operation" },
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

//js获取项目根路径，如： http://localhost:8083/
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