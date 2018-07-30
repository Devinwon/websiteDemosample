//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var childDatatalbe
var MenuViewModel = {
    SearchType: 0,
    MID: 0,
    PID: 0,
    MoveType: 0,
    MenuName: '',
    Category: 0
};
$(function () {
    //加载数据
    Loading();
    //查询按钮
    $("#btnSearch").click(function () {
        Loading();
    })
});
//绑定父菜单Datatable
function BindDatatable(ajaxUrl, dataJson) {
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_Menus").dataTable({
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
	            { "data": "MenuName" },
	            { "data": "Controller" },
	            { "data": "Action" },
	            { "data": "ChildNum" },
	            { "data": "Icon" }
            ],
            "columnDefs": [
                {
                    "targets": 5,
                    "render": function (data, type, row) {
                        var outHtml = row['Category'] == 0 ? "学生端" : "管理员端";
                        return outHtml;
                    }
                },
                {
                    "targets": [6], "render": function (data, type, row) {
                        var returninfo = "<button class='btn btn-primary' onclick='Move(" + row['MID'] + ",0,0)'  data-action='Pass'><i class='fa fa-arrow-up'></i></button> "
                        returninfo += "<button class='btn btn-primary' onclick='Move(" + row['MID'] + ",1,0)'  data-action='Pass'><i class='fa fa-arrow-down'></i></button> ";
                        returninfo += "<button class='btn btn-primary' onclick='EditMenuProfile(" + row['MID'] + ")'  data-action='Pass'><span class='fa fa-pencil'></span>编辑</button> ";
                        returninfo += "<button class='btn btn-primary' onclick='ShowChildMenuProfile(" + row['MID'] + "," + row['Category'] + ")'  data-action='Pass'>设置子菜单</button>";
                        return returninfo;
                    }
                }
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": dataJson
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}
//菜单移动
function Move(id, type, pid) {
    var ajaxUrl = getRootPath() + "/SysMenu/MenuMove";
    var MenuViewModelObj = Object.create(MenuViewModel);
    MenuViewModelObj.MID = id;
    MenuViewModelObj.MoveType = type;
    MenuViewModelObj.PID = pid
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: MenuViewModelObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    if (pid != 0) {
                        LoadingChilde(pid);
                    } else {
                        Loading();
                    }
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
//加载父菜单数据
function Loading() {
    var MenuViewModelObj = Object.create(MenuViewModel);
    MenuViewModelObj.Category = $("#selCategory").val();
    MenuViewModelObj.MenuName = $("#TxtMenuNameSearch").val();
    //绑定Datatable
    BindDatatable(getRootPath() + "/SysMenu/GetNavationMenuFList", MenuViewModelObj);
}
//弹出编辑菜单窗口
function EditMenuProfile(id) {
    var ajaxUrl = "";
    if (id == 0 || id == null) {
        ajaxUrl = getRootPath() + "/SysMenu/EditMenu";
    } else {
        ajaxUrl = getRootPath() + "/SysMenu/EditMenu?MID=" + id;
    }
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#EditMenuModal').modal('show');
        },
        complete: function (html) {
            //验证表单
            $("#MenuFrom").validationEngine({
                //是否是异步
                ajaxFormValidation: true,
                ajaxFormValidationMethod: "post"
            });
            //如果是子菜单删除菜单适用范围选择
            var PCategorycode = $("#dispalyPCategory").attr("data-id");
            if (PCategorycode != null && PCategorycode != "" && PCategorycode != "undefined") {
                $("#selCategory").empty();
                if (PCategorycode == 1) {
                    $("#selCategory").append("<option value='1'>管理员端</option>");
                }
                else {
                    $("#selCategory").append("<option value='0'>学生端</option>");
                }
            }
            //加载图标设置触发事件
            AlertIcon();
        }
    });
}
//弹出子菜单列表窗口
function ShowChildMenuProfile(id, Category) {
    var ajaxUrl = getRootPath() + "/SysMenu/ShowChildMenu?PID=" + id + "&Category=" + Category;
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialogChild").empty();
            $('#pop_modaldialogChild').append(html);
            $('#ChildMenuModal').modal('show');
        },
        complete: function (html) {
            LoadingChilde(id);
        }
    });
}
//加载子菜单数据
function LoadingChilde(id) {
    //绑定Datatable
    BindChildDatatable(getRootPath() + "/SysMenu/GetNavationMenuchildList?PID=" + id);
}
//绑定子菜单的Datatable
function BindChildDatatable(ajaxUrl) {
    if (childDatatalbe == null) {
        childDatatalbe = $(".datatable_ChildMenus").dataTable({
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
                {
                    "data": "MenuName"
                },
            {
                "data": "Controller"
            },
            {
                "data": "Action"
            },
            {
                "data": "ChildNum"
            },
            {
                "data": "Icon"
            }
            ],
            "columnDefs": [
                {
                    "targets": 5,
                    "render": function (data, type, row) {
                        var outHtml = row['Category'] == 0 ? "学生端" : "管理员端";
                        return outHtml;
                    }
                },
            {
                "targets": [6], "render": function (data, type, row) {
                    var returninfo = "<button class='btn btn-primary' onclick='Move(" + row['MID'] + ",0," + row['PID'] + ")'  data-action='Pass'><i class='fa fa-arrow-up'></i></button> "
                    returninfo += "<button class='btn btn-primary' onclick='Move(" + row['MID'] + ",1," + row['PID'] + ")'  data-action='Pass'><i class='fa fa-arrow-down'></i></button> ";
                    returninfo += "<button class='btn btn-primary' onclick='EditMenuProfile(" + row['MID'] + ")'  data-action='Pass'><span class='fa fa-pencil'></span>编辑</button>";
                    return returninfo;
                }
            }
            ]
        });
    } else {
        var oSettings = childDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        childDatatalbe.fnClearTable(0);
        childDatatalbe.fnDraw();
    }
}
//关闭子菜单列表
function Close() {
    $('#ChildMenuModal').modal('hide');
    $('#ChildMenuModal').empty();
    //关闭清空容器
    childDatatalbe = null;
}
//弹出子菜单编辑菜单窗口
function EditChildMenuProfile(pid) {
    var ajaxUrl = "";
    ajaxUrl = getRootPath() + "/SysMenu/EditMenu?PID=" + pid;
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#EditMenuModal').modal('show');
        },
        complete: function (html) {
            //如果是子菜单删除菜单适用范围选择
            var PCategorycode = $("#dispalyPCategory").attr("data-id");
            if (PCategorycode != null && PCategorycode != "" && PCategorycode != "undefined") {
                $("#selCategory").empty();
                if (PCategorycode == 1) {
                    $("#selCategory").append("<option value='1'>管理员端</option>");
                }
                else {
                    $("#selCategory").append("<option value='0'>学生端</option>");
                }
            }
            //加载图标设置触发事件
            AlertIcon();
        }
    });
}
//加载图标设置触发事件
function AlertIcon() {
    $("#TxtIcon").click(function () {
        var ajaxUrl = getRootPath() + "/SysMenu/SetIcons";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialogIcons").empty();
                $('#pop_modaldialogIcons').append(html);
                $('#IconsModal').modal('show');
            },
            complete: function (html) {
                $(".icons-list li").on("click", function () {
                    var iClass = $(this).find("i").attr("class");
                    $("#TxtIcon").val(iClass);
                    $('#IconsModal').modal('hide');
                    $('#IconsModal').empty();
                })
            }
        });
    });
}
//关闭图标设置
function CloseIconsModal() {
    $('#IconsModal').modal('hide');
    $('#IconsModal').empty();
}

