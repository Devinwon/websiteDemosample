var MenuViewModel = {
    MID: 0,
    PID: 0,
    MenuName: '',
    SortCode: 0,
    Controller: '',
    Action: '',
    ChildNum: '',
    Icon: 0,
    Category: 0
};
$(function () {
    $('#FormClose').click(function () {
        $('.modal-header > button.close').click();
    });
})
//保存按钮事件
function EidtInfo(mid) {
    //数据验证(未完成)
    var checkMenu = $("#MenuFrom").validationEngine("validate");
    if (!checkMenu) {
        return;
    }
    var TxtMenuName = $("#TxtMenuName").val();
    var TxtControl = $("#TxtControl").val();
    var TxtAction = $("#TxtAction").val();
    var TxtSonNum = $("#TxtSonNum").val();
    var TxtIcon = $("#TxtIcon").val();
    var selCategory = $("#selCategory").val();
    //封装数据
    var MenuViewModelObj = Object.create(MenuViewModel);
    MenuViewModelObj.Action = TxtAction;
    MenuViewModelObj.ChildNum = TxtSonNum;
    MenuViewModelObj.Controller = TxtControl;
    MenuViewModelObj.Icon = TxtIcon;
    MenuViewModelObj.MenuName = TxtMenuName;
    MenuViewModelObj.Category = selCategory;
    var MID = mid;
    var PID = $("#dispalyPID").attr("data-id");
    var ajaxUrl = "";
    if (PID != null && PID != 0) {
        MenuViewModelObj.PID = PID;
    }
    if (MID != null && MID != 0) {
        ajaxUrl = getRootPath() + "/SysMenu/MenuUpdate";
        MenuViewModelObj.MID = MID;
    } else {
        ajaxUrl = getRootPath() + "/SysMenu/MenuAdd";
    }
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
                    $('#UpdateEnterpriseModal').modal('hide');
                    $("#pop_modaldialog").empty();
                    //加载Datetable数据 有父类的加载子菜单数据
                    if (PID != null && PID != 0) {
                        LoadingChilde(PID);
                    }
                    else {
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
//错误信息弹窗
function errorAlter(ErrorInfo) {
    noty({
        text: ErrorInfo,
        layout: 'topRight',
        type: 'error',
    });
}
//获取地址栏的值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + escape(name) + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function CloseEditMenuModal() {
    $('#EditMenuModal').modal('hide');
}
