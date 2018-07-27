/**
 * Created by lenovo on 2018/4/11.
 */
$(function(){
    var address = "";//http://192.168.10.182:8000
    var phoneValue = $("#loginPhone").val();
    var passValue = $("#loginPass").val();
    $("#loginPhone").blur(function(){
        $.ajax({
            type : "POST", //提交方式
            url : address+"/api/user/verify_user/",//路径
            dataType:"json",
            data : {
                "phone" : phoneValue
            },//数据，这里使用的是Json格式进行传输
            success : function(result) {//返回数据根据结果进行相应的处理
                if(result.res==1){
                    $("#prompt").html("");
                    return false;
                }else {
                    $("#prompt").html("用户名不存在！");
                     $("#loginPhone").focus();
                }
            }
        });
    })
    //验证手机号
    $("#btn_longin").click(function(){
        var re = /^1[3|4|5|8][0-9]\d{4,8}$/;
        if(phoneValue==""){
            $("#prompt").html("手机号不能为空！");
        }else if(!re.test(phoneValue)){
            $("#prompt").html("手机号格式不对！");
        }else if(passValue==""){
            $("#prompt").html("密码不能为空！");
        }else {
            $("#prompt").html("");
            /*登录请求*/
            $.ajax({
                type : "POST", //提交方式
                url : address+"/api/user/login_check/",//路径
                dataType:"json",
                data : {
                    "phone" : phoneValue,
                    "password":passValue,
                },//数据，这里使用的是Json格式进行传输
                success : function(result) {//返回数据根据结果进行相应的处理
                    if(result.res==0){
                        alert("登录成功！")
                        $(".longin").html(rePhone);
                        $(".register").remove();
                    }
                    console.log(result.res+"33333")

                }
            });
            window.location.href="tender.html";
        }

    })

    // 记住我取值
   /* if ($.cookie("rmbMe") == "true") {
        $("#rmbMe").attr("checked", true);
        $("#loginPhone").val($.cookie("userpPhone"));
        $("#loginPass").val($.cookie("password"));
    }

    // 记住用户名，默认不记住
    var checkFlg = false;
// 记住用户名
    $("#rmbMe").click(function(){
        if (!checkFlg) {
            $("#rmbMe").attr("checked", true);
        } else {
            $("#rmbMe").attr("checked", false);
        }
        checkFlg = !checkFlg;
    });

// 保存用户名
    function setCookie() {
        if (checkFlg) {
            var userName = $("#userName").val();
            $.cookie("rmbMe", "true", { expires: 7 }); // 记住我勾选
            $.cookie("userName", userName, { expires: 7 }); // 存储一个带7天期限的 cookie
        } else {
            $.cookie("rmbMe", "false", { expires: -1 }); // 删除 cookie
            $.cookie("userName", '', { expires: -1 });
        }
    }

// 登录
    function login() {
        setCookie();
    }*/
});

