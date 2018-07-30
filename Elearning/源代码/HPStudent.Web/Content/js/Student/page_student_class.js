//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var starPage; //起始页

$(function () {
    $('#btnSelect').on('click', function () {
        
        var SchoolID = $('#selSchool').val();
        var Year = $('#selYear').val();

        if (SchoolID == "== 选择校区 ==") {
            alert("请选择校区");
            return false;
        }
        if (Year == "== 入学年份 ==") {
            alert("请选择入学年份");
            return false;
        }
        var ajaxUrl = "/Student/GetStudentClassListBySchoolID?SchoolID=" + SchoolID + "&Year=" + Year;
       BindDatatable(ajaxUrl);

        //$.ajax({
        //    type: "post",
        //    url: "/Student/GetStudentClassListBySchoolID?SchoolID=" + SchoolID + "&Year=" + Year,
        //    dataType: "json",
        //    data: "SchoolID=" + SchoolID + "&Year=" + Year,
        //    success: function (data) {
        //        $('#StudentClassList').empty();
        //        $.each(data, function (i, item) {
        //            $('#StudentClassList').append(" <tr> <td>" + item.CID + "</td> <td>" + item.SchoolName + "</td><td>" + item.Year + "</td><td>" + item.CName + "</td> <td>" + item.CCode + "</td>  <td>  <button class=\"btn btn-primary btn-sm\" data-id="
        //                + item.CID + "   data-action='edit'>  <span class='fa fa-pencil'  ></span>编辑 </button> "
        //                + "<button class='btn btn-primary btn-sm' type='button'   data-id="
        //                + item.CID + ">   <span class='fa fa-times'></span>"
        //                + " 删除  </button>  </td> </tr>");
        //        });
        //    },
        //    error: function (XMLHttpRequest, textStatus, errorThrown) {
        //        alert(errorThrown);
        //    }
        //});
    });

    //添加成绩
    $('#btnAddClass').on('click', function (e) {

        var SchoolID = $('#selSchool').val();
        var Year = $('#selYear').val();
        if (SchoolID == "== 选择校区 ==") {
            alert("请选择校区");
            return false;
        }
        if (Year == "== 入学年份 ==") {
            alert("请选择入学年份");
            return false;
        }

        var ajaxUrl = "Pop_Class_Add";
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-student-class').modal('show');
            },
            complete: function (html) {
                $('#Pop_selSchool').val($('#selSchool').find("option:selected").text());
                $('#Pop_selYear').val($('#selYear').find("option:selected").text());
            }
        });
    });

    //成绩列表的编辑和删除项添加事件
    //  $('#class-list').find('.btn-primary').on('click', function (e) {

    $("#class-list").delegate(".btn-primary", "click", function () {     
        if ($(this).attr("data-action") == "edit") {
            //编辑按钮
            var ajaxUrl = "Pop_Class_Edit?CID=" + $(this).attr("data-id") + "&" + Date.now();

            $.ajax({
                type: "GET",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-student-class').modal('show');
                },
                complete: function (html) {
                    $('#Pop_selSchool').val($('#selSchool').find("option:selected").text());
                    $('#Pop_selYear').val($('#selYear').find("option:selected").text());
                }
            });

        } else {
            //删除按钮
            myConfirm("确定要删除此班级吗？", "数据删除后将无法恢复！", "DelClass(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

            return false;
        }

    });


    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {
        $(this).parents(".message-box").removeClass("open");
    });

    //弹出确认窗口关闭  -- 确定
    $("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {
        $(this).parents(".message-box").removeClass("open");
        // alert('2');
    });

});

//删除班级信息
function DelClass(CID) {
    var SchoolID = $('#selSchool').val();
    var Year = $('#selYear').val();
    $.ajax({
        type: "post",
        url: "/Student/DeleteStudentClass",
        dataType: "json",
        data: { CID: CID },
        success: function (data) {
            if (data > 0) {
                noty({
                    text: "删除成功！",
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: "删除失败！",
                    layout: 'topRight',
                    type: 'fail',
                });
            }
             SchoolID = $('#selSchool').val();
             Year = $('#selYear').val();
            var ajaxUrl = "/Student/GetStudentClassListBySchoolID?SchoolID=" + SchoolID + "&Year=" + Year;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}

//添加班级信息
function AddClass() {

    var SchoolID = $('#selSchool').val();
    var Year = $("#selYear").val();
    var CCode = $("#Pop_CCode").val();
    var CName = $("#Pop_CName").val();

    if (CCode == "") {
        alert("请填写班级编码！");
        return false;
    }
    if (CName == "") {
        alert("请填写班级名称！");
        return false;
    }
    $.ajax({
        type: "post",
        url: "/Student/AddStudentClass",
        dataType: "json",
        data: { SchoolID: SchoolID, Year: Year, CCode: CCode, CName: CName },
        success: function (data) {
            if (data> 0) {
                noty({
                    text: "操作成功！",
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: "操作失败！",
                    layout: 'topRight',
                    type: 'fail',
                });
            }
            SchoolID = $('#selSchool').val();
            Year = $('#selYear').val();
            var ajaxUrl = "/Student/GetStudentClassListBySchoolID?SchoolID=" + SchoolID + "&Year=" + Year;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $('#edit-student-class').modal('hide');

     $("#pop_modaldialog").delegate(".message-box").removeClass("open");
}

//添加班级信息
function EditClass() {

    //题目编号
    var CID = $('#hdnCID').val();
    //课程ID
    var SchoolID = $('#selSchool').val();
    var Year = $("#selYear").val();
    if (SchoolID == "== 选择校区 ==") {
        return false;
    }
    if (Year == "== 入学年份 ==") {
        return false;
    }
    var CCode = $("#Pop_CCode").val();
    var CName = $("#Pop_CName").val();
    $.ajax({
        type: "post",
        url: "/Student/EditStudentClass",
        dataType: "json",
        data: { CID: CID, SchoolID: SchoolID, Year: Year, CCode: CCode, CName: CName },
        success: function (data) {
            if (data > 0) {
                noty({
                    text: "操作成功！",
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: "操作失败！",
                    layout: 'topRight',
                    type: 'fail',
                });
            }
            SchoolID = $('#selSchool').val();
            Year = $('#selYear').val();
            var ajaxUrl = "/Student/GetStudentClassListBySchoolID?SchoolID=" + SchoolID + "&Year=" + Year;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $('#edit-student-class').modal('hide');
    $("#pop_modaldialog").delegate(".message-box").removeClass("open");
}

function BindDatatable(ajaxUrl) { 
    //清空Datatable
    $('#StudentClassList').empty();
    //$('#StudentClassList').append(" <tr> <td>" + item.CID + "</td> <td>" + item.SchoolName + "</td><td>" + item.Year + "</td><td>" + item.CName + "</td> <td>" + item.CCode + "</td>  <td>  <button class=\"btn btn-primary btn-sm\" data-id="
    //                 + item.CID + "   data-action='edit'>  <span class='fa fa-pencil'  ></span>编辑 </button> "
    //                 + "<button class='btn btn-primary btn-sm' type='button'   data-id="
    //                 + item.CID + ">   <span class='fa fa-times'></span>"
    //                 + " 删除  </button>  </td> </tr>");
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_class").dataTable({
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
	            { "data": "CID" },
	            { "data": "SchoolName" },
	            { "data": "Year" },
                { "data": "CName" },
                { "data": "CCode" },
	            { "data": "Operation" },
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
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