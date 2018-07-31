$(function () {
    //验证表单
    $("#EnterpriseFrom").validationEngine({
        //是否是异步
        ajaxFormValidation: true,
        ajaxFormValidationMethod: "post",
        onAjaxFormComplete: ajaxValidationCallback
    });
    //验证表单
    $("#StudentFrom").validationEngine({
        //是否是异步
        ajaxFormValidation: true,
        ajaxFormValidationMethod: "post",
        onAjaxFormComplete: ajaxValidationCallback
    });
    //切换表单事件
    $("#ChangeRole").click(function () {
        if ($("#tab-Student").is(":hidden")) {
            $("#tab-Student").show();
            $("#tab-Enterprise").hide();
            $("#titleText").text("学生注册");
        }
        else {
            $("#tab-Student").hide();
            $("#tab-Enterprise").show();
            $("#titleText").text("企业注册");
        }
    })
})
//返回结果
function ajaxValidationCallback(status, form, json, options) {
    if (status === true) {
        if (json.ResultState == 0) {
            noty({
                text: "注册成功，请等待工作人员审核！",
                layout: 'topRight',
                type: 'success',
            });
        }
        else {
            noty({
                text: json.ResultMsg,
                layout: 'topRight',
                type: 'fail',
            });
        }
    } else {
        noty({
            text: "对不起，内部出现错误，请重新注册!",
            layout: 'topRight',
            type: 'fail',
        });
    }
}
//取消注册
$(".GiveUpRes").click(function () {
    noty({
        text: '是否放弃注册回到首页?',
        layout: 'topRight',
        buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: '确定', onClick: function ($noty) {
                        $noty.close();
                        window.location.href = "../Account/Login";
                    }
                },
                {
                    addClass: 'btn btn-danger btn-clean', text: '取消', onClick: function ($noty) {
                        $noty.close();
                    }
                }
        ]
    })
})