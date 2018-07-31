var UserRoleNavPowrer = {
    RID: 0,
    Navigation: '',
    SortCode: 0
};
//保存用户菜单权限
function SaveUserMenu() {
    //封装数据
    var UserRoleNavPowrerObj = Object.create(UserRoleNavPowrer);
    UserRoleNavPowrerObj.RID = $("#hiddenUserRoleID").attr("data-id");
    $(".icheckbox").each(function () {
        if ($(this).is(':checked') == true) {
            UserRoleNavPowrerObj.Navigation += $(this).attr("data-id") + ",";
        }
    });
    var ajaxUrl = getRootPath() + "/UserRole/UserMenuRoleEdit";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: UserRoleNavPowrerObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#EditUserMenuRoleModal').modal('hide');
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