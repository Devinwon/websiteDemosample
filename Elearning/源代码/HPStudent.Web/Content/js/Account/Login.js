$("#ChageValidateCode").click(function () {
    $("#validatecodeGraphic").attr("src", "/Account/GetValidateCode?" + new Date());
})