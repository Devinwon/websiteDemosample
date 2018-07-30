//数据初始化
var myDatatalbe = null; //初始化一个Datatable容器
var starPage; //起始页
var ProjectInfo = {
    PID: 0,
    MID: 0,
    ProjectName:"",
    TeacherName:"",
    TeacherID: 0,
    TeacherInfo:{TID:0, TeacherName:"", Password:"" , Level:0 , LastLoginTime : "",LastLoginIP : ""},
    selTeacherID:0,
    UseTechnology: "",
    ProjectPic:"",
    ProjectDesc:"",
    ClassHour: 0,
    CreateDate:"",
    EditDate:"" ,
    ShowPart: 0
};

$(function () {
    //2015年12月30日 涂建 增加记录历史的功能（用于浏览器返回按钮）
    if (history.pushState) {
        window.addEventListener("popstate", function () {
            initPage();
        });
    }

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

    //选择按钮被点击时触发
    $('#btnSelectMajor').on('click', function () {
        //1. 选择课程
        var MID = $('#selMajor').val();
        SelectMajor(MID);
        //2015年12月30日 涂建 增加记录历史的功能（用于浏览器返回按钮）
        history.pushState({ mid: MID }, MID, location.href.split("?")[0] + "?" + MID);
    });

    //添加项目按钮被点击
    //添加题目
    $('#btnAddProject').on('click', function (e) {
        //如果没有选择省份或城市，弹出提示
        
        if ($('#selMajor').val() == "0") {
           // myAlert("请先选择具体的专业！", "未选择具体的专业前不能添加项目", "#pop_modaldialog");
            noty({
                text: "未选择具体的专业前不能添加项目",
                layout: 'topRight',
                type: 'error',
            });
            return false;
        }
        //弹出添加窗口
        var ajaxUrl = "/Projects/Pop_Project_Add";

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-project').modal('show');
            },
            complete: function (html) {
                //$('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
                //$('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
                ////修复taginput显示
                feTagsinput();
                feSelect();
                feBsFileInput();

                //修复icheck无法显示样式的bug
                $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
            }
        });

    });

    $("#Project-list").delegate(".btn-primary","click",function(){
        if ($(this).attr("data-action") == "edit") {
            //加载编辑窗口
            var ajaxUrl = getRootPath() + "/Projects/Pop_Project_Edit?PID=" + $(this).attr("data-id") + "&" + Date.now();
            $.ajax({
                type: "GET",
                url: ajaxUrl,
                cache: false,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-project').modal('show');
                },
                complete: function (html) {
                    ////修复taginput显示
                    var _useTechnology = $("#UseTechnology").val();
                    //$('#UseTechnology').tagsInput({ width: 'auto' });
                    
                    //feTagsinput();

                    $('#UseTechnology').tagsInput();
                    if (_useTechnology != "undefined") {
                        $('#UseTechnology').importTags(_useTechnology);
                    }
                    feSelect();
                    feBsFileInput();

                    //修复icheck无法显示样式的bug
                    $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
                }
            });

        } else if ($(this).attr("data-action") == "delete"){
            //var managerName = $(this).parents("tr").find("td").eq(1).text();
            //删除管理员
            myConfirm("确定要删除当前选中的项目吗？", "该操作会删除该项目下面的所有资源！", "DelProject(" + $(this).attr("data-id") + ")", "#pop_modaldialog");
        } else if ($(this).attr("data-action") == "detail") {
            //跳转到详细页
            window.location.href = "/Projects/Detail?PID=" + $(this).attr("data-id");
            return;
        }
    });
});

 function initPage (target) {
    var query = location.href.split("?")[1], eleTarget = target || null;
    if (typeof query == "undefined") {
        
    } else {
        SelectMajor(query);
        //同步更新下拉列表项
        $("#selMajor option").removeAttr("selected");
        $("#selMajor option[value=" + query + "]").attr("selected", true);
        
    }
 };

 function SelectMajor(MID)
 {
     if (MID == "0") {
         noty({
             text: "请先选择专业！",
             layout: 'topRight',
             type: 'warning',
         });
         return false;
     }
     //2. AJAX动态调用后台接口生成练习题
     ajaxUrl = "/Projects/GetProjectBookList?MID=" + MID;

     //3.Datatable绑定数据
     BindDatatable(ajaxUrl);


 }

function DelProject(PID)
{
    $("#pop_modaldialog").find(".message-box").removeClass("open");
    

    
    var ajaxUrl = getRootPath() + "/Projects/DelProject";

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: "PID=" + PID,
        dataType: "json",
        success: function (html) {

        },
        complete: function (ResultJson) {
            //重新绑定Datatable
            if (ResultJson.responseJSON.ResultState == 1) {
                noty({
                    text: ResultJson.responseJSON.ResultMsg,
                    layout: 'topRight',
                    type: 'error',
                });

            } else {

                noty({
                    text: ResultJson.responseJSON.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
            }
            var MID = $('#selMajor').val();
            ajaxUrl = "/Projects/GetProjectBookList?MID=" + MID;
            BindDatatable(ajaxUrl);
        }
    });
}
//弹出添加项目窗口中的添加按钮被点击时触发
function AddProject() {
    var check_a, check_b;
    check_a = 0;
    check_b = 0;
    var project = Object.create(ProjectInfo);
    project.MID =  $('#selMajor').val();
    project.ProjectName =  $("#ProjectName").val();
    project.TeacherID =$("#selTeacher").val();
    project.ClassHour = 0;
    project.UseTechnology = $('input[name=UseTechnology]').val();
    project.ProjectPic = $("#ProjectPic").val();
    project.ProjectDesc = $("#ProjectDesc").val();
    var picExt = (/[.]/.exec(project.ProjectPic)) ? /[^.]+$/.exec(project.ProjectPic.toLowerCase()) : '';
    if ($("#cbShowTeacher").is(':checked')) {
        check_a = 1;
    }
    if ($("#cbShowShort").is(':checked')) {
        check_b = 2;
    }
    project.ShowPart = check_a + check_b;

    //检查文件格式
    if (project.ProjectName == "") {
        myNoty("请填写项目的名称后再提交！");
        return false;
    } else if (project.TeacherID == 0) {
        myNoty("请选择老师后再提交！");
        return false;
    } else if (project.UseTechnology == 0) {
        myNoty("请填写使用到的技术后再提交！");
        return false;
    } else if (project.ProjectPic == "") {
        myNoty("请选择了项目图片后再提交！");
        return false;
    } else if (picExt != "jpg" && picExt != "png" && picExt != "jpeg" && picExt != "gif") {
        myNoty("只允许上传jpg、png、gif格式的图片！")
        return false;
    } else if (project.ProjectDesc == "") {
        myNoty("请填写项目描述后再提交！")
        return false;
    } 



    $("#btnPopAddProject").html("提交中...");
    $("#btnPopAddProject").addClass("disabled");

    var ajaxUploadFile = "/Projects/Pop_Project_Add";
    //上传excel文件
    $.ajaxFileUpload({
        url: ajaxUploadFile,
        secureuri: false,
        dataType: "json",
        type:"POST",
        //data: { ProjectName: project.ProjectName, MID: project.MID , TeacherID:project.TeacherID,UseTechnology:project.UseTechnology,ProjectDesc:project.ProjectDesc },
        data:project,
        fileElementId: 'ProjectPic',
        success: function (data, status)  //服务器成功响应处理函数
        {
            if (data.ResultState == 0) {
                $('#add-project').modal('hide');
                $("#pop_modaldialog").empty();
                //$('#pop_modaldialog').append(html);

                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var MID = $('#selMajor').val();
                ajaxUrl = "/Projects/GetProjectBookList?MID=" + MID;
                BindDatatable(ajaxUrl);
            } else {
                myNoty(data.ResultMsg)

            }
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            alert(e);

            $("#btnPopAddProject").html("添加");
            $("#btnPopAddProject").removeClass("disabled");
        }
    });
}

function EditProject() {
    var check_a, check_b;
    check_a = 0;
    check_b = 0;
    var project = Object.create(ProjectInfo);
    project.PID = $('#hidePID').val();
    project.MID = $('#selMajor').val();
    project.ProjectName = $("#ProjectName").val();
    project.TeacherID = $("#selTeacher").val();
    project.ClassHour = 0;
    project.UseTechnology = $('input[name=UseTechnology]').val();
    project.ProjectPic = $("#ProjectPic").val();

    if ($("#cbShowTeacher").is(':checked')) {
        check_a = 1;
    }
    if ($("#cbShowShort").is(':checked')) {
        check_b = 2;
    }
    project.ShowPart = check_a + check_b;

    if (project.ProjectPic == "") {
        project.ProjectPic = $("#hideProjectPic").val();
    }
    project.ProjectDesc = $("#ProjectDesc").val();
    //alert(project.UseTechnology);
    //alert(project.ProjectPic);
    //return;

    var picExt = (/[.]/.exec(project.ProjectPic)) ? /[^.]+$/.exec(project.ProjectPic.toLowerCase()) : '';
    //检查文件格式
    if (project.ProjectName == "") {
        myNoty("请填写项目的名称后再提交！");
        return false;
    } else if (project.TeacherID == 0) {
        myNoty("请选择老师后再提交！");
        return false;
    } else if (project.UseTechnology == "") {
        myNoty("请填写使用到的技术后再提交！");
        return false;
    } else if (project.ProjectPic != "" && picExt != "jpg" && picExt != "png" && picExt != "jpeg" && picExt != "gif") {
        myNoty("只允许上传jpg、png、gif格式的图片！")
        return false;
    } else if (project.ProjectDesc == "") {
        myNoty("请填写项目描述后再提交！")
        return false;
    }
    
   

    $("#btnEditProject").html("提交中...");
    $("#btnEditProject").addClass("disabled");

    var ajaxUploadFile = "/Projects/Pop_Project_Edit?" + Date.now();
    //上传excel文件
    $.ajaxFileUpload({
        url: ajaxUploadFile,
        secureuri: false,
        dataType: "json",
        type: "POST",        
        data: project,
        fileElementId: 'ProjectPic',
        success: function (data, status)  //服务器成功响应处理函数
        {
            if (data.ResultState == 0) {
                $('#edit-project').modal('hide');
                $("#pop_modaldialog").empty();
                //$('#pop_modaldialog').append(html);

                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var MID = $('#selMajor').val();
                ajaxUrl = "/Projects/GetProjectBookList?MID=" + MID;
                BindDatatable(ajaxUrl);
            } else {
                myNoty(data.ResultMsg)

            }
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            alert(e);

            $("#btnEditProject").html("添加");
            $("#btnEditProject").removeClass("disabled");
        }
    });

    onload();
}
function myNoty(message) {
    noty({
        text: message,
        layout: 'topRight',
        type: 'error',
    });
}
//绑定Datatable
function BindDatatable(ajaxUrl) {

    //清空Datatable
    $('#Data_SelectList').empty();
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
	            { "data": "ProjectName" },
	            { "data": "TeacherName" },
	            { "data": "ClassHour" },
	            { "data": "CreateDate" },
	            { "data": "CreateDate" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "render": function (data, type, row) {
                        var outHtml = '<a href="/Projects/Detail?PID=' + row['PID'] + '">' + row['ProjectName'] + '</a> '
                        return outHtml;
                    }
                },
                  {
                      "targets": 1,
                      "class": "text-center",
                  },
                {
                    "targets": 2,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml = '<button class="btn btn-primary btn-sm" data-id="' + row['PID'] + '" data-action="detail"><span class="fa fa-clock-o"></span>' + row['ClassHour'] + '</button> '
                        return outHtml;
                    }
                },
                {
                    "targets": 3,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml = GetLocalTime(row['CreateDate']);
                        return outHtml;
                    }
                },
                {
                    "targets": 4,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml = GetLocalTime(row['EditDate']);
                        return outHtml;
                    }
                },
                {
                    "targets": 5,
                    "render": function (data, type, row) {
                        var outHtml = '<button class="btn btn-primary btn-sm" data-id="' + row['PID'] + '" data-action="edit"><span class="fa fa-pencil"></span>编辑</button> ';
                        outHtml += ' <button class="btn btn-primary btn-sm disabled" data-id="' + row['PID'] + '" data-action="delete"><span class="fa fa-pencil"></span>删除</button> '
                        return outHtml;
                    }
                }
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

//Tagsinput
var feTagsinput = function () {
    if ($(".tagsinput").length > 0) {

        $(".tagsinput").each(function () {

            if ($(this).data("placeholder") != '') {
                var dt = $(this).data("placeholder");
            } else
                var dt = 'add a tag';

            $(this).tagsInput({ width: '100%', height: 'auto', defaultText: dt });
        });

    }
}// END Tagsinput

//Bootstrap select
var feSelect = function () {
    if ($(".select").length > 0) {
        $(".select").selectpicker();

        $(".select").on("change", function () {
            if ($(this).val() == "" || null === $(this).val()) {
                if (!$(this).attr("multiple"))
                    $(this).val("").find("option").removeAttr("selected").prop("selected", false);
            } else {
                $(this).find("option[value=" + $(this).val() + "]").attr("selected", true);
            }
        });
    }
}//END Bootstrap select

var feBsFileInput = function () {

    if ($("input.fileinput").length > 0)
        $("input.fileinput").bootstrapFileInput();

}



