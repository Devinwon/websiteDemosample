var myDatatalbe = null; //初始化一个Datatable容器


$(function () {

        DataBindTable(getRootPath() + "/Users/GetUsersTable");

});



function DataBindTable(ajaxUrl) {

    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_users").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/Js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "NickName" },
                { "data": "Email" },
                { "data": "Password" },
                { "data": "CreateDate" },
                { "data": "LastLogin" },
                { "data": "LastIP" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 3,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['CreateDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
                {
                    "targets": 4,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['LastLogin'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
            ]
        });
        onresize();
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


//操作
$("#class-list").delegate(".btn-primary", "click", function () {

    var UID = $(this).attr("data-id");
    if ($(this).attr("data-action") == "edit") {
        //编辑按钮
        var ajaxUrl = getRootPath() + "/Users/UsersEdit";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-manager').modal('show');
            }
            , complete: function (data) {
                UsersBind("Update", getRootPath() + "/Users/UsersBind?UID=" + UID);
            }

        });
    }
    else if ($(this).attr("data-action") == "delete") {
        myConfirm("确定要删除当前用户吗？", "用户删除后将无法恢复！", "DelUsers(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

        return false;
    }

});


//删除问卷
function DelUsers(val) {
    $.ajax({
        type: "post",
        url: "/Users/DeleteUsers",
        dataType: "json",
        data: "UID=" + val,
        success: function (data) {
            DataBindTable(getRootPath() + "/Users/GetUsersTable");

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}




$("#btnAddUsers").click(function () {
    var ajaxUrl = getRootPath() + "/Users/UsersEdit";

    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        }
           , complete: function (data) {
               UsersBind("Add", getRootPath() + "/Users/UsersBind");
           }

    });



});

function UsersBind(type, ajaxUrl)
{
    if (type!="Add") {
    
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (data) {
                $("#txt_uid").val(data.UID);
                $("#txt_nickName").val(data.NickName);
                $("#txt_email").val(data.Email);
                $("#txt_password").val(data.Password);
                $("#txt_email").attr("readonly", "readonly");
            }
        });
    } else
    {
    $("#txt_uid").val("");
    $("#txt_nickName").val("");
    $("#txt_email").val("");
    $("#txt_password").val("");
    }


 



}


function emailValidation(email)
{
    var falg = false;
    var ajaxUrl = getRootPath() + "/Users/UsersEmailValidation?email=" + email;

    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (data) {
            if (data == 0) {
                falg = false;
            } else
            {
                falg = true;
            }
        }, error: function (XMLHttpRequest, textStatus, errorThrown)
        {
            flag = true;
        }
    });
    return falg;
}


    function EditUsers() {

        var uid = $("#txt_uid").val();
        var nickName = $("#txt_nickName").val();
        var email = $("#txt_email").val();
        var password = $("#txt_password").val();

        if (nickName == "" || nickName == undefined || nickName == null) {
           
            noty({
                text: '未填写昵称',
                layout: 'topRight',
                type: 'fail',
            });

        }
        else if (email == "" || email == undefined || email == null) {
            noty({
                text: '未填写邮箱帐号',
                layout: 'topRight',
                type: 'fail',
            });
        }
        else if (!email.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/))
        {
            noty({
                text: '邮箱帐号格式不正确',
                layout: 'topRight',
                type: 'fail',
            });
        }
        else if (password == "" || password == undefined || password == null) {
            noty({
                text: '未填写登录密码',
                layout: 'topRight',
                type: 'fail',
            });
        } else {
        if (uid != "") { 
            $.ajax({
                type: "post",
                url: "/Users/UpdateUsers",
                data: { UID: uid, nickName: nickName, email: email, password: password },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "用户修改成功！", "#pop_modaldialog");
                        $('#edit-projectitem').modal('hide');
                        $("#pop_modaldialog").empty();
                        DataBindTable(getRootPath() + "/Users/GetUsersTable");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        } else {
            if (emailValidation(email)) {
                noty({
                    text: '邮箱帐号已注册',
                    layout: 'topRight',
                    type: 'fail',
                });
            } else { 

            $.ajax({
                type: "post",
                url: "/Users/AddUsers",
                data: { nickName: nickName, email: email, password: password },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "用户添加成功！", "#pop_modaldialog");
                        $('#edit-projectitem').modal('hide');
                        $("#pop_modaldialog").empty();
                        DataBindTable(getRootPath() + "/Users/GetUsersTable");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    alert(errorThrown);
                }
            });
            }
}
    }





}