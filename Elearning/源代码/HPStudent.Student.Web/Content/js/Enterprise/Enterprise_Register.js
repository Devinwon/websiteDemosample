$(function () {
    //验证表单
    $("#fromvalidate").validationEngine({
        //是否是异步
        ajaxFormValidation: true,
        ajaxFormValidationMethod: "post",
        onAjaxFormComplete: ajaxValidationCallback
    });

})
function ajaxValidationCallback(status, form, json, options) {
    if (status === true) {
        if (json.ResultState == 0) {
            if (confirm("企业账号注册成功，是否直接进入系统?")) {
                window.location = "/Home/Index";
            }
        }
        else
        {
            noty({
                text: json.ResultMsg,
                layout: 'topRight',
                type: 'success',
            });
        }
    } else {
        layer.alert('对不起，内部出现错误，请重新注册!', 3);
    }
}