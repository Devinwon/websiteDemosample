//数据初始化
var myDatatalbe; //初始化一个Datatable容器
//页面加载事件
$(function () {
    Loading();
})
//绑定用户角色Datatable
function BindDatatable(ajaxUrl) {
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_UserRole").dataTable({
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
            "serverMethod": 'post',
            "columns": [
	            { "data": "RoleName" }
            ],
            "columnDefs": [
                {
                    "targets": [1], "render": function (data, type, row) {
                        var returninfo = "<button class='btn btn-primary' onclick='EidtUserRole(" + row['RID'] + ")'  data-action='Pass'>编辑</button> "
                        returninfo += "<button class='btn btn-primary' onclick='SetMenuLoad(" + row['RID'] + ")'  data-action='Pass'>菜单权限</button> "
                        return returninfo;
                    }
                }
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}
//加载用户角色数据
function Loading() {
    //绑定Datatable
    BindDatatable(getRootPath() + "/UserRole/GetUserRoleList");
}
//编辑事件
function EidtUserRole(id) {
    var ajaxUrl = "";
    if (id == 0 || id == null) {
        ajaxUrl = getRootPath() + "/UserRole/EditRole";
    } else {
        ajaxUrl = getRootPath() + "/UserRole/EditRole?RID=" + id;
    }
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#EditUserRoleModal').modal('show');
        },
        complete: function (html) {
            //验证表单
            $("#UserRoleFrom").validationEngine({
                //是否是异步
                ajaxFormValidation: true,
                ajaxFormValidationMethod: "post"
            });
        }
    });
}
//菜单权限设置按钮
function SetMenuLoad(id) {
    var ajaxUrl = "";
    if (id == 0 || id == null) {
        ajaxUrl = getRootPath() + "/UserRole/UserMenuRole";
    } else {
        ajaxUrl = getRootPath() + "/UserRole/UserMenuRole?RID=" + id;
    }
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialogMenu").empty();
            $('#pop_modaldialogMenu').append(html);
            $('#EditUserMenuRoleModal').modal('show');
        },
        complete: function (html) {
            feiCheckbox();
        }
    });
}
//修复动态加载的Checkbox无法显示样式的BUG
var feiCheckbox = function () {
    if ($(".icheckbox").length > 0) {
        $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
    }
}
//关闭角色编辑窗口
function CloseEditUserRoelModal() {
    $('#EditUserRoleModal').modal('hide');
    $('#EditUserRoleModal').empty();
}
//关闭菜单权限编辑窗口
function CloseEditUserMenuRoelModal() {
    $('#EditUserMenuRoleModal').modal('hide');
    $('#EditUserMenuRoleModal').empty();
}