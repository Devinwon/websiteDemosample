//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var starPage; //起始页

$(function () {
    //省份下拉框变化
    $('#selProvince').change(function () {
        $('#selCity').empty();
        var ajaxUrl = "/Student/GetComSchoolByParentAID";
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
                alert(errorThrown);
            }
        });
    });

    $("#btnSelect").on('click', function (e) {

        var ParentAID = $('#selProvince').val();
        var AreaID = $('#selCity').val();
        if (ParentAID == "== 选择省 ==") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        if (AreaID == "== 选择城市 ==") {
            AreaID = "";
        }
        var ajaxUrl = "/Student/GetSchoolByAreaID?ParentAID=" + ParentAID + "&AreaID=" + AreaID;
        BindDatatable(ajaxUrl);
        return false;
    });

    //添加校区
    $('#btnAddSchool').on('click', function (e) {      
        //如果没有选择省份或城市，弹出提示
        if ($('#selProvince').val() == "== 选择省 ==" || $("#selCity").val() == "== 选择城市 ==") {
            alert("请先选择省份和城市后再添加校区");
            return false;
        }

        //弹出添加窗口
        var ajaxUrl = "Pop_School_Add?" + Date.now();

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-student-school').modal('show');
            },
            complete: function (html) {
                $('#pop_SelProvince').val($('#selProvince').find("option:selected").text());
                $('#pop_SelCity').val($('#selCity').find("option:selected").text());
            }
        });

    });

    //成绩列表的编辑和删除项添加事件
    $("#class-list").delegate(".btn-primary", "click", function () {
       
        if ($(this).attr("data-action") == "edit") {
            //编辑按钮
            var ajaxUrl = "Pop_School_Edit?SchoolID=" + $(this).attr("data-id") + "&" + Date.now();
            $.get(ajaxUrl, function (html, status) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-student-school').modal('show');
            });

        } else {          
            //删除按钮
            myConfirm("确定要删除此校区吗？", "数据删除后将无法恢复！", "DelSchool(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

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
function DelSchool(SchoolID) {
    var ParentAID = $('#selProvince').val();
    var AreaID = $('#selCity').val();
    $.ajax({
        type: "post",
        url: "/Student/DeleteStudentSchool",
        dataType: "json",
        data: { SchoolID: SchoolID },
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
            //隐藏弹出的添加校区窗口          
            var ajaxUrl = "/Student/GetSchoolByAreaID?ParentAID=" + ParentAID + "&AreaID=" + AreaID;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}

//添加班级信息
function AddSchool() {
    var SchoolName = $('#pop_SchoolName').val();
    var AreaID = $('#selCity').val();
    var ParentAID = $('#selProvince').val();
    if (SchoolName == "") {
        myAlert("请填写校区名称！", "校区名称不正确", "#pop_modaldialog");
        return false;
    }
    $.ajax({
        type: "post",
        url: "/Student/AddStudentSchool",
        dataType: "json",
        data: {AreaID:AreaID, SchoolName: SchoolName },
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
            //隐藏弹出的添加校区窗口
            $('#add-student-school').modal('hide');
            var ajaxUrl = "/Student/GetSchoolByAreaID?ParentAID=" + ParentAID + "&AreaID=" + AreaID;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}

//添加班级信息
function EditSchool() {
    var AreaID = $('#selCity').val();
    var ParentAID = $('#selProvince').val();
    var SchoolName = $('#pop_SchoolName').val();
    var SchoolID = $('#hdnSchoolID').val();
    if (SchoolName == "") {
        myAlert("请填写校区名称！", "校区名称不正确", "#pop_modaldialog");
        return false;
    }
    $.ajax({
        type: "post",
        url: "/Student/EditStudentSchool",
        dataType: "json",
        data: {SchoolID:SchoolID, SchoolName: SchoolName },
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
            //隐藏弹出的编辑校区窗口
            $('#edit-student-school').modal('hide');
            var ajaxUrl = "/Student/GetSchoolByAreaID?ParentAID=" + ParentAID + "&AreaID=" + AreaID;
            BindDatatable(ajaxUrl);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function BindDatatable(ajaxUrl) {

    //清空Datatable
    $('#CommonSchoolList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_school").dataTable({
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
	            { "data": "SchoolID" },
	            { "data": "AreaName" },
	            { "data": "CityName" },
                { "data": "SchoolName" },
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