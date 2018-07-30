//数据初始化
var myDatatalbe = null; //初始化一个Datatable容器
var starPage; //起始页
var ResourceInfo = {
    RID: 0,
    MID: 0,
    ResourceName: "",
    TeacherName: "",
    CourceHour: 0,
    ResourceFrom: "",
    ResourcePic: "",
    ResourceDesc: "",
    ClassHour: 0,
    CreateDate: "",
    EditDate:""
};

$(function () {
    //ajax加载专业列表
    $.ajax({
        type: "post",
        url: "/Resource/GetAllMajorList",
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
        if (MID == "0") {
            noty({
                text: "请先选择专业！",
                layout: 'topRight',
                type: 'warning',
            });
            return false;
        }
        //2. AJAX动态调用后台接口生成练习题
        ajaxUrl = "/Resource/GetResourceBookList?MID=" + MID;

        //3.Datatable绑定数据
        BindDatatable(ajaxUrl);
    });

    //添加资源按钮被点击
    $('#btnAddResource').on('click', function (e) {
        //如果没有选择省份或城市，弹出提示
        
        if ($('#selMajor').val() == "0") {
           // myAlert("请先选择具体的专业！", "未选择具体的专业前不能添加项目", "#pop_modaldialog");
            noty({
                text: "未选择具体的专业前不能添加资源",
                layout: 'topRight',
                type: 'error',
            });
            return false;
        }
        //弹出添加窗口
        var ajaxUrl = "/Resource/Pop_Resource_Add";

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-resource').modal('show');
            },
            complete: function (html) {
                //$('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
                //$('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
                ////修复taginput显示
                feSelect();
                feBsFileInput();
            }
        });

    });

    $("#Resource-list").delegate(".btn-primary", "click", function () {
        if ($(this).attr("data-action") == "edit") {
            //加载编辑窗口
            var ajaxUrl = getRootPath() + "/Projects/Pop_Project_Edit?PID=" + $(this).attr("data-id");            
            $.ajax({
                type: "GET",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-project').modal('show');
                },
                complete: function (html) {
                    ////修复taginput显示
                    feTagsinput();
                    feSelect();
                    feBsFileInput();
                }
            });

        } else if ($(this).attr("data-action") == "delete"){
            //var managerName = $(this).parents("tr").find("td").eq(1).text();
            //删除管理员
            myConfirm("确定要删除当前选中的项目吗？", "该操作会删除该下面下的所有资源！", "DelProject(" + $(this).attr("data-id") + ")", "#pop_modaldialog");
        } else if ($(this).attr("data-action") == "detail") {
            //跳转到详细页
            window.location.href = "/Resource/Detail?RID=" + $(this).attr("data-id");
            return;
        }
    });
});

function DelProject(DelID)
{
    $("#pop_modaldialog").find(".message-box").removeClass("open");
    //test
    alert(DelID);
    return;

    
    var ajaxUrl = getRootPath() + "/SysManage/DelManager";

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: "MID=" + DelID,
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
                    type: 'error',
                });
            }
            BindDatatable(getRootPath() + "/SysManage/GetManagerList");
        }
    });
}
//弹出添加项目窗口中的添加按钮被点击时触发
function AddResource() {
    var Resource = Object.create(ResourceInfo);
    Resource.MID = $('#selMajor').val();
    Resource.ResourceName = $("#ResourceName").val();
    Resource.TeacherName = $("#TeacherName").val();
    Resource.CourceHour = 0;
    Resource.ResourceFrom = $("#ResourceFrom").val();
    Resource.ResourcePic = $("#ResourcePic").val();
    Resource.ResourceDesc = $("#ResourceDesc").val();
    //获取上传文件的扩展名
    var picExt = (/[.]/.exec(Resource.ResourcePic)) ? /[^.]+$/.exec(Resource.ResourcePic.toLowerCase()) : '';
    //检查文件格式
    if (Resource.ResourceName == "") {
        myNoty("请填写资源的名称后再提交！");
        return false;
    } else if (Resource.ResourceFrom == "") {
        myNoty("请填写资源来源！");
        return false;
    } else if (Resource.ResourcePic == 0) {
        myNoty("请选择了资源图片后再提交！");
        return false;
    } else if (picExt != "jpg" && picExt != "png" && picExt != "jpeg" && picExt != "gif") {
        myNoty("只允许上传jpg、png、gif格式的图片！")
        return false;
    } else if (Resource.ResourceDesc == 0) {
        myNoty("请填写资源描述后再提交！")
        return false;
    } 

    $("#btnAddResource").ajaxStart(function () {
        $(this).html("数据提交中...");
    }).ajaxComplete(function () {
        $(this).html("添加");
    });

    var ajaxUploadFile = "/Resource/Pop_Resource_Add";
    //上传excel文件
    $.ajaxFileUpload({
        url: ajaxUploadFile,
        secureuri: false,
        dataType: "json",
        type:"POST",
        //data: { ProjectName: project.ProjectName, MID: project.MID , TeacherID:project.TeacherID,UseTechnology:project.UseTechnology,ProjectDesc:project.ProjectDesc },
        data: Resource,
        fileElementId: 'ResourcePic',
        success: function (data, status)  //服务器成功响应处理函数
        {
            if (data.ResultState == 0) {
                $('#add-resource').modal('hide');
                $("#pop_modaldialog").empty();
                //$('#pop_modaldialog').append(html);

                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                
            } else {
                myNoty(data.ResultMsg)

            }
        },
        complete: function (data, status)  //服务器成功响应处理函数
        {
            var MID = $('#selMajor').val();
            ajaxUrl = "/Resource/GetResourceBookList?MID=" + MID;
            BindDatatable(ajaxUrl);
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            alert(e);
        }
    });
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
	            { "data": "ResourceName" },
	            { "data": "TeacherName" },
	            { "data": "ResourceFrom" },
	            { "data": "CourceHour" },
	            { "data": "CreateDate" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                  {
                      "targets": 1,
                      "class": "text-center",
                  },
                {
                    "targets": 3,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml = '<button class="btn btn-primary btn-sm" data-id="' + row['RID'] + '" data-action="detail"><span class="fa fa-clock-o"></span>' + row['CourceHour'] + '</button> '
                        return outHtml;
                    }
                },
                {
                    "targets": 4,
                    "render": function (data, type, row) {
                        var outHtml = GetLocalTime(row['CreateDate']);
                        return outHtml;
                    }
                },
                {
                    "targets": 5,
                    "render": function (data, type, row) {
                        var outHtml = '<button class="btn btn-primary btn-sm" data-id="' + row['RID'] + '" data-action="edit"><span class="fa fa-pencil"></span>编辑</button> ';
                        outHtml += ' <button class="btn btn-primary btn-sm" data-id="' + row['RID'] + '" data-action="delete"><span class="fa fa-pencil"></span>删除</button> '
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
