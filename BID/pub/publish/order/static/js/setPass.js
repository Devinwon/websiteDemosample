/**
 * Created by lenovo on 2018/4/11.
 */
$(function(){
    var address = ""//http://192.168.10.151:8000
    //登录验证手机号
    $("#btn1").click(function(){
        /*电话*/
        var isPhone=localStorage.getItem("isPhone")||'';
        if(isPhone ==1) {
            var rePhone = localStorage.getItem("registerPhone");
        }
        console.log(rePhone+"111111")
        /*密码*/
        var isPass=localStorage.getItem("isPass")||'';
        if(isPass ==2) {
            var YPass = localStorage.getItem("YPass");
        }
        var setPass = $("#setPass").val();
        var setPass1 = $("#setPass1").val();
        if(setPass==""||setPass1==""){
            $("#prompt").html("密码不能为空");
        }else if(setPass!=setPass1){
            $("#prompt").html("密码不一致");
        }else {
            $("#prompt").html("");
            /*注册请求*/
            $.ajax({
                type : "POST", //提交方式
                url : address+"/api/user/register_check/",//路径
                dataType:"json",
                data : {
                    "phone" : rePhone,
                    "YZM":YPass,
                    "password":setPass,
                    "password_confirm":setPass1
                },//数据，这里使用的是Json格式进行传输
                success : function(result) {//返回数据根据结果进行相应的处理
                    if(result.res==0){
                        alert("注册成功！请登录！")
                    }
                }
            });
        }
    })
    /*同意*/
    /* var cb=document.getElementById("rmbMe");
     cb.onclick=function(){
     if(cb.checked==true){
     document.getElementById("btn1").disabled=false;
     document.getElementById("btn1").style.background="blue"
     }
     else{
     document.getElementById("btn1").disabled=true;
     document.getElementById("btn1").style.background="darkgrey"
     }
     }*/

    /*仿刷新：检测是否存在cookie*/  /*注册验证码获取*///倒计时
   /* if($.cookie("captcha")){
        var count = $.cookie("captcha");
        var btn = $('#getting');
        btn.val(count+'秒后重新获取').attr('disabled',true).css('cursor','not-allowed');
        var resend = setInterval(function(){
            count--;
            if (count > 0){
                btn.val(count+'秒后重新获取').attr('disabled',true).css('cursor','not-allowed');
                $.cookie("captcha", count, {path: '/', expires: (1/86400)*count});
            }else {
                clearInterval(resend);
                btn.val("获取验证码").removeClass('disabled').removeAttr('disabled style');
            }
        }, 1000);
    }
    /!*点击改变按钮状态，已经简略掉ajax发送短信验证的代码*!/  /!*注册验证码获取*!///倒计时
    $('#getting').click(function(){
        var btn = $(this);
        var count = 60;
        var resend = setInterval(function(){
            count--;
            if (count > 0){
                btn.val(count+"秒后重新获取");
                $.cookie("captcha", count, {path: '/', expires: (1/86400)*count});
            }else {
                clearInterval(resend);
                btn.val("获取验证码").removeAttr('disabled style');
            }
        }, 1000);
        btn.attr('disabled',true).css('cursor','not-allowed');
        /!*ajax*!/
        var registerPhone = $("#registerPhone").val();
        console.log(registerPhone)
        $.ajax({
            type : "POST", //提交方式
            url : "http://192.168.10.182:8000/api/user/send_code/",//路径
            dataType:"json",
            data : {
                "phone" : registerPhone
            },//数据，这里使用的是Json格式进行传输
            success : function(result) {//返回数据根据结果进行相应的处理
                console.log(result.res)

            }
        });
    });*/

});