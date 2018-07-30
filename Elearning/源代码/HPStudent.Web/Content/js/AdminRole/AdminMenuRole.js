var AdminRoleNavPowrer = {
    RID: 0,
    Navigation: '',
    SortCode: 0
};
//保存用户菜单权限
function SaveAdminMenu() {
    //封装数据
    var AdminRoleNavPowrerObj = Object.create(AdminRoleNavPowrer);
    AdminRoleNavPowrerObj.RID = $("#hiddenAdminRoleID").attr("data-id");
    $(".icheckbox").each(function () {
        if ($(this).is(':checked') == true) {
            AdminRoleNavPowrerObj.Navigation += $(this).attr("data-id") + ",";
        }
    });
    var ajaxUrl = getRootPath() + "/AdminRole/AdminMenuRoleEdit";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: AdminRoleNavPowrerObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#EditAdminMenuRoleModal').modal('hide');
                    $("#pop_modaldialogMenu").empty();
                    Loading();
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