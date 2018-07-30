var CompanyInfoObj = {
    SID: 0,
    CompanyName: '',
    CompanyProfile: '',
    Address: '',
    Scale: '',
    TelPhone: '',
    Email: '',
    WebSite: ''
};
//保存公司信息
function SaveCompanyInfo() {
    //数据验证
    var checkMenu = $("#CompanyForm").validationEngine("validate");
    if (!checkMenu) {
        return;
    }
    var Obj = Object.create(CompanyInfoObj);
    Obj.SID = $("#hidSID").val();
    Obj.Address = $("#EditAddress").val();
    Obj.CompanyName = $("#EditCompanyName").val();
    Obj.CompanyProfile = $("#EditCompanyProfile").val();
    Obj.Email = $("#EditEmail").val();
    Obj.Scale = $("#selEditScale").val();
    Obj.TelPhone = $("#EditTelPhone").val();
    Obj.WebSite = $("#EditWebSite").val();
    var ajaxUrl = getRootPath() + "/Enterprise/SaveCompanyInfo";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        data: Obj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#edit-Company').modal('hide');
                    $("#pop_modaldialog").empty();
                    SearchEnterpriseInfo();
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        }
    });
}