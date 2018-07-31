$(function () {
    $("#ChageValidateCode").click(function () {
        $("#validatecodeGraphic").attr("src", "/Account/GetValidateCode?" + new Date());
    });

    $(".btn-alipay").click(function () {        
        window.location.href = "/Webpage/Alipay";
    });
    $("#Register").click(function () {
        window.location.href = "../Account/Register";
    })

});