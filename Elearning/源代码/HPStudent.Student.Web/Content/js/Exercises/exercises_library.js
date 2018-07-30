var iCount; //定时器
var myDatatalbe;

$(function () {
    //ajax加载专业列表
    $.ajax({
        type: "post",
        url: "/Projects/GetAllMajorList",
        dataType: "json",
        data: "",

        success: function (data) {
            $('#selMajor').empty();
            $('#selMajor').append("<option value='0'>== 选择专业 ==</option>");
            $.each(data, function (i, item) {
                $('#selMajor').append("<option value='" + item.MID + "'>" + item.MajorName + "</option>");
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

    //加载完成后，绑定DATATABLE
    BindDatatable("/Exercises/BindTestPaper");
    //专业下拉框变化
    $('#selMajor').change(function () {
        var MajorID = $(this).val();
        if (MajorID == "0") {
            $('#selCourse').empty();
            $('#selCourse').append("<option value='== 选择课程 =='>== 选择课程 ==</option>");
            return;
        }

        //ajax加载课程数据
        $.ajax({
            type: "post",
            url: "/Exercises/GetCategoryListByMajorIDNotNone",
            dataType: "json",
            data: "MajorID=" + MajorID,

            success: function (data) {
                $('#selCourse').empty();
                $('#selCourse').append("<option value='0'>== 选择课程 ==</option>");
                $.each(data, function (i, item) {
                    $('#selCourse').append("<option value='" + item.CID + "'>" + item.CategoryName + "</option>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    });

    //生成测试题按钮被点击
    $('#btnCreateExercises').on('click', function () {
        //选择课程
        var CID = $('#selCourse').val();
        if (CID == "0") {
            noty({
                text: "请选择专业和课程！",
                layout: 'topRight',
                type: 'warning',
            });
            return false;
        }

        //3. AJAX动态调用后台接口生成练习题
        var ajaxUrl = "/Exercises/Pop_Progressbar?" + Date.now();

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            data: { CID: CID },
            success: function (html, data) {  
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#Create-Exercises').modal('show');
            },
            complete: function (html,data) {
                if ($('#hdnTID').val() == "0") {
                    noty({
                        text: "生成测试题失败！",
                        layout: 'topRight',
                        type: 'warning',
                    });
                    $("#pop_modaldialog").empty();
                    return false;
                }else{
                //2.定时显示进度条
                iCount = setInterval(DoProgress, 100);
            }
            }
        });



    });

    //测试列表的测试和查看项添加事件
    $("#question-list").delegate(".btn-primary", "click", function () {
        var ajaxUrl;
        if ($(this).attr("data-action") == "show") {
            //查看按钮
            ajaxUrl = "/Exercises/Pop_ShowTest?TID=" + $(this).attr("data-id");

        } else if ($(this).attr("data-action") == "doit") {
            //测试按钮  
            ajaxUrl = "/Exercises/Pop_DoTest?TID=" + $(this).attr("data-id");
        }
        //Ajax获取页面
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#Exercises_Details').modal('show');
            },
            complete: function (html) {
                //修复icheck无法显示样式的bug
                $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
            }
        });
    });

    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".close", "click", function () {
        $('#Exercises_Details').modal('hide');
        $('#Exercises_Details').modal('hide');
        //  $(this).parents(".message-box").removeClass("open");
    });

});

//完成测试
function FinishTest() {
    //1.ajax提交
    var TID = $("#hdnTID").val();
    //获取
    var arr1 = $("input[type=checkbox]");
    var answer = "";
    $.each(arr1, function (i, val) {
        if (i % 4 == 0 && i != 0) {
            answer = answer + "|";
        }
        if (arr1[i].checked == true) {
            answer = answer + arr1[i].value + ",";
        }

    });

    if (answer.substring(answer.length - 1, answer.length) == ",") {
        answer = answer.substring(0, answer.length - 1)
    }


    var ajaxUrl = "/Exercises/SubmitDoTest";

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: { TID: TID, Answer: answer },
        success: function (data) {
            noty({
                text: "测试试卷已完成",
                layout: 'topRight',
                type: 'success',
            });

        },

    });

    //$("input[type=checkbox]").each(function () {
    //    for (var i = 0; i < 4; i++) {
    //        alert(this.attr('checked').val());
    //        //if (this.attr('checked') == true) {

    //        //}
    //    }

    //});
    //   "A,|D,||||B,|||C,|C,D";


    //2.隐藏弹出框
    $('#Exercises_Details').modal('hide');
    BindDatatable("/Exercises/BindTestPaper");
}

//关闭测试
function CloseTest() {
    $('#Exercises_Details').modal('hide');
}

//进度条效果
function DoProgress() {
    var p = $(".progress-bar").attr("aria-valuenow");
    if (p < 100) {
        var showProgress = parseInt(p) + 10;
        var showWidth = showProgress + 30;
        if (showWidth > 100) {
            showWidth = 100;
        }
        $(".progress-bar").attr("aria-valuenow", showProgress);
        $(".progress-bar").attr("style", "width:" + showWidth + "%;");
        //$(".progress-bar").html(showProgress);
    } else {
        //隐藏生成提示框
        $('#Create-Exercises').modal('hide');
        //停止定时器
        clearInterval(iCount);
        //弹出提示
        noty({
            text: "测试题目生成成功！",
            layout: 'topRight',
            type: 'success',
        });

        //3.1 Ajax调用成功后重新刷新DataTable
        BindDatatable("/Exercises/BindTestPaper");
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
	            { "data": "TID" },
	            { "data": "Range" },
	            { "data": "CreateDate" },
                { "data": "Score" },
                { "data": "EndDate" },
	            { "data": "IsComplete" },
                { "data": "IsComplete" },
            ],
            "columnDefs": [
                 {
                     "targets": 2, "render": function (data, type, row) {

                         var javascriptDate = new Date(new Date(parseInt(data.substr(6))));
                         javascriptDate = javascriptDate.getFullYear() + "-" + (javascriptDate.getMonth() + 1) + "-" + javascriptDate.getDate();
                         return javascriptDate;
                     }
                 },
                  {
                      "targets": 4, "render": function (data, type, row) {
                          var javascriptDate = new Date(new Date(parseInt(data.substr(6))));
                          javascriptDate = javascriptDate.getFullYear() + "-" + (javascriptDate.getMonth() + 1) + "-" + javascriptDate.getDate();
                          if (javascriptDate == "1900-1-1") {
                              javascriptDate = "";
                          }
                          return javascriptDate;
                      }
                  },
                   {
                       "targets": 5, "render": function (data, type, row) {
                           return data == 1 ? "<td class='text-center'><span class='label label-success'>已完成</span></td>" : " <td class='text-center'><span class='label label-warning'>未完成</span></td>";
                       }
                   },
                {
                    "targets": 6, "render": function (data, type, row) {
                        return data == 1 ? "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['TID'] + "'><span class='fa fa-search'></span>查看</button> </td>" : "<td><button data-action='doit' class='btn btn-primary btn-sm' data-id='" + row['TID'] + "'><span class='fa fa-pencil'></span>测试</button></td>";
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