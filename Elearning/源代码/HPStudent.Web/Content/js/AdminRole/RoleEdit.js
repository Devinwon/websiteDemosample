var AdminRoleViewModel = {
    RID: 0,
    RoleName: 0,
    SortCode: 0
};
//编辑按钮
function EidtInfo(id) {
    //数据验证(未完成)
    var checkMenu = $("#AdminRoleFrom").validationEngine("validate");
    if (!checkMenu) {
        return;
    }
    //封装数据
    var AdminRoleViewModelObj = Object.create(AdminRoleViewModel);
    AdminRoleViewModelObj.RoleName = $("#TxtAdminRoleName").val();
    var ajaxUrl = "";
    if (id == 0 || id == null) {
        //添加
        ajaxUrl = getRootPath() + "/AdminRole/AdminRoleAdd";
    }
    else {
        AdminRoleViewModelObj.RID = id;
        //修改
        ajaxUrl = getRootPath() + "/AdminRole/AdminRoleUpdate";
    }
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: AdminRoleViewModelObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#EditAdminRoleModal').modal('hide');
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