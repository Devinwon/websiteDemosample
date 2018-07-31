$(function () {
    $("#ChageValidateCode").click(function () {
        $("#validatecodeGraphic").attr("src", "/Account/GetValidateCode?" + new Date());
    });

    $(".btn-alipay").click(function () {        
        window.location.href = "/Webpage/Alipay";
    });


});