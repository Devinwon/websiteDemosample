/**
 * Created by lenovo on 2018/4/11.
 */
$(function(){
    var address = ""//http://192.168.10.151:8000
    /*注册验证码获取*///倒计时
   /* /!*仿刷新：检测是否存在cookie*!/
    if($.cookie("captcha")){
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
    /!*点击改变按钮状态，已经简略掉ajax发送短信验证的代码*!/
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

        $.ajax({
            type : "POST", //提交方式
            url : "http://192.168.10.151:8000/api/user/send_code/",//路径
            dataType:"json",
            data : {
                "phone" : registerPhone
            },//数据，这里使用的是Json格式进行传输
            success : function(result) {//返回数据根据结果进行相应的处理
                console.log(result.res)
            }
        });
    });*/
    var countdown=60;
    $("#btn").click(function(){
        /*调接口*/
        var registerPhone = $("#registerPhone").val();
        if(registerPhone==""||!/^1[3456789][0-9]{9}$/.test(registerPhone)){
            $("#prompt").html("手机号为空或格式错误！");
            return false;
        }else {
            var obj = $("#btn");
            settime(obj);
            $.ajax({
                type : "POST", //提交方式
                url : address+"/api/user/send_code/",//路径
                dataType:"json",
                data : {
                    "phone" : registerPhone
                },//数据，这里使用的是Json格式进行传输
                success : function(result) {//返回数据根据结果进行相应的处理
                    console.log(result.res+"1111")
                }
            });
        }
    })
    function settime(obj) { //发送验证码倒计时
        if (countdown == 0) {
            obj.attr('disabled',false);
            //obj.removeattr("disabled");
            obj.val("免费获取验证码");
            countdown = 60;
            return;
        } else {
            obj.attr('disabled',true);
            obj.val("重新发送(" + countdown + ")");
            countdown--;
        }
        setTimeout(function() {
                settime(obj) }
            ,1000)
    }
    //注册验证手机号
    $("#btn1").click(function(){
        var registerPhone = $("#registerPhone").val();
        var YPass = $("#YPass").val();

        localStorage.setItem("isPhone",1);
        localStorage.setItem("registerPhone",registerPhone);
        localStorage.setItem("isPass",2);
        localStorage.setItem("YPass",YPass);

        var re = /^1[3|4|5|8][0-9]\d{4,8}$/;
        if(registerPhone==""){
            $("#prompt").html("手机号不能为空！");
        }else if(!re.test(registerPhone)){
            $("#prompt").html("手机号格式不对！");
        }else if(YPass==""){
            $("#prompt").html("验证码不能为空！");
        }else {
            $("#prompt").html("");
            $.ajax({
                type : "POST", //提交方式
                url : address+"/api/user/verify_user/",//路径
                dataType:"json",
                data : {
                    "phone" : registerPhone
                },//数据，这里使用的是Json格式进行传输
                success : function(result) {//返回数据根据结果进行相应的处理
                    console.log(result.res)
                    if(result.res==0){
                        window.location.href="setPass.html";
                    }else {
                        $("#prompt").html("用户名已存在！");
                    }
                }
            });

        }

    })
    /*同意*/
    var cb=document.getElementById("rmbMe");
    cb.onclick=function(){
        if(cb.checked==true){
            document.getElementById("btn1").disabled=false;
            document.getElementById("btn1").style.background="blue"
        }
        else{
            document.getElementById("btn1").disabled=true;
            document.getElementById("btn1").style.background="darkgrey"
        }
    }


});