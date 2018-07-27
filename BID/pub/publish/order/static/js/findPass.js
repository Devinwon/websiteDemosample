/**
 * Created by lenovo on 2018/4/11.
 */
$(function(){
    var address = "http://192.168.10.182:8000"
    /*注册验证码获取*///倒计时
    var countdown=60;
    $("#btn_find").click(function(){
        /*调接口*/
        var findPhone = $("#findPhone").val();
        if(findPhone==""||!/^1[3456789][0-9]{9}$/.test(findPhone)){
            $("#prompt").html("手机号为空或格式错误！");
            return false;
        }else {
            var obj = $("#btn_find");
            settime(obj);
            $.ajax({
                type : "POST", //提交方式
                url : address+"/api/user/send_code/",//路径
                dataType:"json",
                data : {
                    "phone" : findPhone
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
    //找回密码验证手机号
    $("#btn_f").click(function(){
        window.location.href="../html/setPass_1.html";
        /* var registerPhone = $("#registerPhone").val();
         var YPass = $("#YPass").val();
         var re = /^1[3|4|5|8][0-9]\d{4,8}$/;
         if(registerPhone==""){
         $("#prompt").html("手机号不能为空！");
         }else if(!re.test(registerPhone)){
         $("#prompt").html("手机号格式不对！");
         }else if(YPass==""){
         $("#prompt").html("验证码不能为空！");
         }else {
         $("#prompt").html("");
         /!*注册请求*!/
         $.ajax({
         type : "POST", //提交方式
         url : "http://192.168.10.151:8000/user/get_phone/",//路径
         dataType:"json",
         data : {
         "phone" : registerPhone,
         "YZM":YPass
         },//数据，这里使用的是Json格式进行传输
         success : function(result) {//返回数据根据结果进行相应的处理
         console.log(result.res+"sdfgf")
         if(result.res == 1){
         window.location.href="setPass.html";
         }
         }
         });
         }*/

    })
    /*同意*/
    var cb=document.getElementById("rmbMe");
    cb.onclick=function(){
        if(cb.checked==true){
            document.getElementById("btn_f").disabled=false;
            document.getElementById("btn_f").style.background="blue"
        }
        else{
            document.getElementById("btn_f").disabled=true;
            document.getElementById("btn_f").style.background="darkgrey"
        }
    }


});