//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var starPage; //起始页
var childDatatable;
$(function () {
    //省份下拉框变化
    $('#selProvince').change(function () {
        $('#selSchool').empty();
        $('#selSchool').append("<option value='0'>== 选择校区 ==</option>");
        $("#selYear").empty();
        $('#selYear').append("<option value='0'>== 选择年级 ==</option>");

        var ajaxUrl = "/Student/GetComSchoolByParentAID";
        var ParentAID = $('#selProvince').val();

        if (ParentAID == "0") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }

        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            data: "ParentAID=" + ParentAID,

            success: function (data) {
                $('#selCity').empty();
                $('#selCity').append("<option value='0'>== 选择城市 ==</option>");
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

    //城市下拉框变化		
    $('#selCity').change(function () {
        $('#selSchool').empty();
        $('#selSchool').append("<option value='0'>== 选择校区 ==</option>");
        var ParentAID = $('#selProvince').val();
        var AreaID = $('#selCity').val();
        if (ParentAID == "0") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        if (AreaID == "0") {
            myAlert("选择的城市不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        var ajaxUrl = "/Student/GetSchoolListByAreaID?ParentAID=" + ParentAID + "&AreaID=" + AreaID;

        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            success: function (data) {
                //填充校区下拉框
                $.each(data, function (i, item) {
                    $('#selSchool').append("<option value='" + item.SchoolID + "'>" + item.SchoolName + "</option>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });

    });

    //校区下拉框变化
    $('#selSchool').change(function () {

        $("#selYear").empty();
        $('#selYear').append("<option value='0'>== 选择年级 ==</option>");
        var SchoolID = $('#selSchool').val();
        if (SchoolID == "0") {
            myAlert("选择的校区不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        var date = new Date;
        var year = date.getFullYear();
        var startYear = 2009;
        for (var i = year - startYear; i >0 ; i--) {
            $('#selYear').append("<option value='" + (startYear + i) + "'>" + (startYear + i) + "</option>");
        }
    });
    //校区下拉框变化
    $('#selYear').change(function () {
        $('#selClass').empty();
        $('#selClass').append("<option value='0'>== 选择班级 ==</option>");
        var SchoolID = $('#selSchool').val();
        var Year = $("#selYear").val();
        if (Year == "0") {
            myAlert("选择的年级不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }

        //获取班级
        var ajaxUrl = "/Student/GetClassListBySchoolIDAndYear?SchoolID=" + SchoolID + "&Year=" + Year;

        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            success: function (data) {
                //填充校区下拉框
                $.each(data, function (i, item) {
                    $('#selClass').append("<option value='" + item.CID + "'>" + item.CName + "</option>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });

    });

    //查询
    $('#btnSelect').on('click', function (e) {
        if ($('#selProvince').val() == "0") {
            myAlert("请先选择省！", "省份未选择", "#pop_modaldialog");
            return false;
        }
        if ($('#selCity').val() == "0") {
            myAlert("请先选择城市！", "城市未选择", "#pop_modaldialog");
            return false;
        }

        var SchoolID = $('#selSchool').val();
        if (SchoolID == "0") {
            myAlert("请先选择校区！", "校区未选择", "#pop_modaldialog");
            return false;
        }
        var StartYear = $("#selYear").val();
        var CID = $('#selClass').val();
        var StudentName = $('#StudentName').val();
        if (StartYear == "0") {
            StartYear = "";
        }
        if (CID == "0") {
            CID = "";
        }

        var IsCheck = $('#IsCheck').is(':checked');

        var ajaxUrl = "/Student/GetStudentFeeList?SchoolID=" + SchoolID + "&CID=" + CID + "&StartYear=" + StartYear + "&RealName=" + encodeURI(StudentName) + "&IsCheck=" + IsCheck;

        BindDatatable(ajaxUrl);
    });

    //学生费用记录列表的缴费记录
    $("#class-list").delegate(".btn-primary", "click", function () {  
        if ($(this).attr("data-action") == "view") {
            //查询按钮
            var ajaxUrl = "Pop_Student_Feelog?StudentID=" + $(this).attr("data-id") + "&" + Date.now();
            $.get(ajaxUrl, function (html, status) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#view-student-fee').modal('show');
            });
        }
    }); 
   
    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {     
        $(this).parents(".message-box").removeClass("open");
    });

    //弹出确认窗口关闭  -- 确定
    $("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {     
        $(this).parents(".message-box").removeClass("open");      
    });

});



function Close() {    
    $('#view-student-fee').modal('hide');
    //刷新页面
    var SchoolID = $('#selSchool').val(); 
    var StartYear = $("#selYear").val();
    var CID = $('#selClass').val();
    var StudentName = $('#StudentName').val();
    if (StartYear == "0") {
        StartYear = "";
    }
    if (CID == "0") {
        CID = "";
    }
    var IsCheck = $('#IsCheck').is(':checked');
    var ajaxUrl = "/Student/GetStudentFeeList?SchoolID=" + SchoolID + "&CID=" + CID + "&StartYear=" + StartYear + "&RealName=" + encodeURI(StudentName) + "&IsCheck=" + IsCheck;

    BindDatatable(ajaxUrl);
}
//添加班级信息
function CancleStudentFeeCheck(FeeID,StudentID,IsCheck) {
    var ajaxUrl = "/Student/Pop_Student_Feelog?StudentID=" + StudentID + "&FeeID=" + FeeID + "&IsCheck=" + IsCheck;
    $.post(ajaxUrl, function (html, status) {
        $("#pop_modaldialog").empty();
        $('#pop_modaldialog').append(html);
        $('#view-student-fee').modal('show');
    });
}
function PassStudentFeeCheck(FeeID, StudentID, IsCheck) {
    var ajaxUrl = "/Student/Pop_Student_Feelog?StudentID=" + StudentID + "&FeeID=" + FeeID + "&IsCheck=" + IsCheck;
    $.post(ajaxUrl, function (html, status) {
        $("#pop_modaldialog").empty();
        $('#pop_modaldialog').append(html);
        $('#view-student-fee').modal('show');
    });
}
function BackStudentFeeCheck(FeeID, StudentID, IsCheck) {
    var ajaxUrl = "/Student/Pop_Student_Feelog?StudentID=" + StudentID + "&FeeID=" + FeeID + "&IsCheck=" + IsCheck;
    $.post(ajaxUrl, function (html, status) {
        $("#pop_modaldialog").empty();
        $('#pop_modaldialog').append(html);
        $('#view-student-fee').modal('show');
    });

}

function BindDatatable(ajaxUrl) {
    //清空Datatable
    $('#StudentFeeList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_fee").dataTable({
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
                  { "data": "Avatar" },
                  { "data": "RealName" },
                  { "data": "Sex" },
                  { "data": "CName" },
                  { "data": "PaidFee" },
                  { "data": "AllCount" },
                  { "data": "StudentID" },
            ],
            "columnDefs": [
                  {
                      "targets": 0, "render": function (data, type, row) {
                          return '<img src="' + data + '" width="36" />';
                      }
                  },
                   {
                       "targets": 2, "render": function (data, type, row) {
                           return data == 0 ? '男' : '女';
                       }
                   },
                {
                    "targets": 5, "render": function (data, type, row) {
                        return data == row['CheckCount'] ? "  <span class='label label-info'><span class='fa fa-check'></span></span>" : " <span class='label label-danger'><span class='fa fa-times'></span>";
                    }
                },
                   {
                       "targets": 6, "render": function (data, type, row) {
                           return "  <button class='btn btn-primary btn-sm' data-id='" + data + "' data-action='view'><span class='glyphicon glyphicon-list'></span> 缴费记录</button>"
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

function BindChildDatatable(ajaxUrl) {
    var StudentID = $('#hdnStudentID').val();
    //清空Datatable
    $('#StudentFeeList').empty();
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
            "serverMethod": 'GET',
            "columns": [
                { "data": "Year" },
                { "data": "Dateline" },
                { "data": "FeeTitle" },
                { "data": "PaidFee" },
                { "data": "Attachment" },
                { "data": "IsCheck" },
                { "data": "FeeID" },

            ],
            "columnDefs": [
                {
                    "targets": 0, "render": function (data, type, row) {
                        if (data == 2) { data = "二" } else if (data == 3) { data = "三" } else { data = "一" }
                        return "第 " + data + " 学年";
                    }
                },
                  {
                      "targets": 5, "render": function (data, type, row) {
                          return data == "1" ? "  <span class='label label-info'><span class='fa fa-check'></span></span>" : " <span class='label label-danger'><span class='fa fa-times'></span>";
                      }
                  },
                   {
                       "targets": 6, "render": function (data, type, row) {
                           var DisplayButton;
                           switch (row['IsCheck']) {
                               case 1:
                                   DisplayButton = " <button class='btn btn-primary btn-sm' data-id=' " + data + "' data-action='Cancel'>取消</button>";
                                   break;
                               case 2:
                                   DisplayButton = "";
                                   break;
                               default:
                                   DisplayButton = " <button class='btn btn-success btn-sm' data-id=' " + data + "' data-action='Pass'>通过</button><button class='btn btn-danger btn-sm' data-id=' " + data + "' data-action='Back'>退回</button>";
                                   break;
                           }                          
                           return DisplayButton;
                           //"<button class='btn btn-primary btn-sm' type='button' data-action='edit' data-id='" + StudentID + "," + data + "'><span class='fa fa-pencil'></span> 编辑 </button> <button class='btn btn-primary btn-sm' type='button' data-id='" + StudentID + "," + data + "'>                                                                    <span class='fa fa-times'></span> 删除 </button>"
                           //  return "<button id='btn-edit' data-action='edit' class='btn btn-primary btn-sm' data-id='" + StudentID + "," + data + "'><span class='fa fa-pencil'></span>编辑</button><button  class='btn btn-primary btn-sm' data-id='" + StudentID + "," + data + "'><span class='fa fa-times'></span>删除</button>";
                       }
                   },
            ]
        });
    } else {
        var oSettings = childDatatable.fnSettings();
        oSettings.ajax = ajaxUrl;
        childDatatable.fnClearTable(0);
        childDatatable.fnDraw();
    }
}

