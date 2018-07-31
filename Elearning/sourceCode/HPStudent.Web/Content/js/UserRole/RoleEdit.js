var UserRoleViewModel = {
    RID: 0,
    RoleName: 0,
    SortCode: 0
};
//编辑按钮
function EidtInfo(id) {
    //数据验证(未完成)
    var checkMenu = $("#UserRoleFrom").validationEngine("validate");
    if (!checkMenu) {
        return;
    }
    //封装数据
    var UserRoleViewModelObj = Object.create(UserRoleViewModel);
    UserRoleViewModelObj.RoleName = $("#TxtUserRoleName").val();
    var ajaxUrl = "";
    if (id == 0 || id == null) {
        //添加
        ajaxUrl = getRootPath() + "/UserRole/UserRoleAdd";
    }
    else {
        UserRoleViewModelObj.RID = id;
        //修改
        ajaxUrl = getRootPath() + "/UserRole/UserRoleUpdate";
    }
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: UserRoleViewModelObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#EditUserRoleModal').modal('hide');
                    $("#pop_modaldialog").empty();
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