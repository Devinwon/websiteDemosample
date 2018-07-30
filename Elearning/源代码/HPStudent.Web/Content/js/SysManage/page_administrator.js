//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var ManagerInfo = {
    MID: 0,
    ManagerName: "",
    Level: "",
    Password: "",
    RePassword: "",
    AdminRoleRelation: ""
};
$(function () {
    //绑定Datatable
    BindDatatable(getRootPath() + "/SysManage/GetManagerList");

    //添加管理员
    $('#btnAddManager').on('click', function (e) {
        //弹出添加窗口
        var ajaxUrl = getRootPath() + "/SysManage/Pop_Admin_Add?" + Date.now();
        $.get(ajaxUrl, function (html, status) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        });
    });

    //管理员列表的编辑和删除项触发事件
    $("#class-list").delegate(".btn-primary", "click", function () {
        if ($(this).attr("data-action") == "edit") {
            //加载编辑窗口
            var ajaxUrl = getRootPath() + "/SysManage/Pop_Admin_Edit?MID=" + $(this).attr("data-id");
            $.ajax({
                type: "post",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-manager').modal('show');
                },
                complete: function (html) {
                    feiCheckbox();
                }
            });
        } else {
            var managerName = $(this).parents("tr").find("td").eq(1).text();
            //删除管理员
            myConfirm("确定要删除当前选中的管理员吗？", "管理员删除后将无法恢复！", "DelManager(" + $(this).attr("data-id") + ")", "#pop_modaldialog");



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
//修复动态加载的Checkbox无法显示样式的BUG
var feiCheckbox = function () {
    if ($(".icheckbox").length > 0) {
        $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
    }
}
//删除管理员信息
function DelManager(DelID) {
    $("#pop_modaldialog").find(".message-box").removeClass("open");
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

//添加管理员信息
function AddManager() {

    //初始化管理员对象
    var manager = Object.create(ManagerInfo);
    manager.ManagerName = $('input[name = ManagerName]').val();
    manager.Password = $('input[name = Password]').val();
    manager.RePassword = $('input[name = RePassword]').val();
    manager.Level = $('select[name = Level]').val();
    manager.LastLoginIP = "127.0.0.1";
    manager.LastLoginTime = "1900-01-01 00:00:00";
    manager.RndCheckCode = "0000";


    //校验2次密码是否一致
    if (manager.Password != manager.RePassword) {
        noty({
            text: "2次输入的密码不正确,请重新输入！",
            layout: 'topRight',
            type: 'error',
        });
        return false;
    } else if (manager.ManagerName == "") {
        noty({
            text: "管理员名称不能为空！",
            layout: 'topRight',
            type: 'error',
        });
        return false;

    } else if (manager.Password == "") {
        noty({
            text: "管理员密码不能为空！",
            layout: 'topRight',
            type: 'error',
        });
        return false;

    }

    //设置AJAX访问地址
    var ajaxUrl = "/SysManage/AddManager";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: manager,
        success: function (html) {
            if (html.ResultState == 0) {
                $('#edit-manager').modal('hide');
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);

                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'error',
                });

            }
        },
        complete: function (html) {
            //$('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
            //$('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
            //修复icheck无法显示样式的bug
            //$(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
            //alert(1);
            //绑定Datatable
            BindDatatable(getRootPath() + "/SysManage/GetManagerList");


        }
    });


}

//修改管理员
function EditManager() {
    //初始化管理员对象
    var manager = Object.create(ManagerInfo);
    manager.MID = $('input[name = MID]').val();
    manager.ManagerName = $('input[name = ManagerName]').val();
    manager.Password = $('input[name = Password]').val();
    manager.RePassword = $('input[name = RePassword]').val();
    manager.Level = $('select[name = Level]').val();

    $(".icheckbox").each(function () {
        if ($(this).is(':checked') == true) {
            manager.AdminRoleRelation += $(this).attr("data-id") + ",";
        }
    });
    if (manager.Password != manager.RePassword) {
        noty({
            text: "2次输入的密码不正确,请重新输入！",
            layout: 'topRight',
            type: 'error',
        });
        return false;
    } else if (manager.Password == "") {
        noty({
            text: "管理员密码不能为空！",
            layout: 'topRight',
            type: 'error',
        });
        return false;

    }
    //设置AJAX访问地址
    var ajaxUrl = "/SysManage/EditManager";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: manager,
        success: function (html) {
            if (html.ResultState == 0) {
                $('#edit-manager').modal('hide');
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);

                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
            } else {
                noty({
                    text: html.ResultMsg,
                    layout: 'topRight',
                    type: 'error',
                });

            }
        },
        complete: function (html) {
            //绑定Datatable
            BindDatatable(getRootPath() + "/SysManage/GetManagerList");


        }
    });


}

//绑定Datatable
function BindDatatable(ajaxUrl) {
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_managers").dataTable({
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
            "serverMethod": 'GET',
            "columns": [
	            { "data": "MID" },
	            { "data": "ManagerName" },
	            { "data": "Level" },
	            { "data": "LastLoginTime" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                { "targets": 2, "class": "text-center" },
                { "targets": 3, "class": "text-center" },
                {
                    "targets": 0,
                    "render": function (data, type, row) {
                        return data + ' (' + row['ManagerName'] + ')';
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

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}

