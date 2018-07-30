var EnterpriseRegModel = { NAME: '', PWD: '' };
var EnterpriseProfile = { SID: 0, CompanyName: '', CompanyProfile: '', Address: '', Scale: '', TelPhone: '', Email: '', WebSite: '' };

function EditEnterpriseProfile() {
    var ajaxUrl = getRootPath() + "/Enterprise/UpdateEnterpriseInfo";
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#UpdateEnterpriseModal').modal('show');
        },
        complete: function (html) {
        }
    });
}
$(function () {
    $('#FormSave').click(function () {
        var name = $('#EnterpriseName').val();
        var id = $('#EnterpriseName').attr('tag');
        var desc = $('#EnterpriseProfile').val();
        var address = $('#EnterpriseAddress').val();
        var email = $('#EnterpriseEmail').val();
        var telPhone = $('#EnterpriseTelPhone').val();
        var scale = $('#EnterpriseScale').val();
        var website = $('#EnterpriseWebSite').val();
        //请在此处添加验证
        //注意添加对名称和描述的验证，有效非空
        var dataModel = Object.create(EnterpriseProfile);
        dataModel.SID = id;
        dataModel.CompanyName = name;
        dataModel.CompanyProfile = desc;
        dataModel.Address = address;
        dataModel.Scale = scale;
        dataModel.TelPhone = telPhone;
        dataModel.Email = email;
        dataModel.WebSite = website;

        $.ajax({
            url: "/Enterprise/UpdateEnterpriseAction",
            type: "POST",
            data: dataModel,
            success: function (data) {
                if (data.ResultState == 0) {
                    $('#UpdateEnterpriseModal').modal('hide');
                    $("#pop_modaldialog").empty();
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'success',
                    });
                } else {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'fail',
                    });

                }
            },
            error: function () {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
            }
        });
    });

    $('#FormClose').click(function () {
        $('.modal-header > button.close').click();
    });

    $('#btnQueryAction').click(function () {
        var keyword = $('#QueryName').val();
        $.ajax({
            url: '/Enterprise/QuerySimilarEnterpriseJson',
            data: { keyWord: keyword },
            type: 'POST',
            success: function (data) {
                var content = "";
                for (var index = 0; index < data.length; index++) {
                    var item = data[index];
                    content += item.CompanyName + ';';
                }
                noty({
                    text: content,
                    layout: 'topRight',
                    type: 'success',
                });
            },
            error: function (xhr, responseText) {
                console.log(xhr);
            }
        });
    });
    //显示职位详情
    $("#AJobInfo").click(function () {
        var cominfo = $(this).attr("title");
        window.location.href = ' ../Job/Index?company=' + escape(cominfo);
    })
});
