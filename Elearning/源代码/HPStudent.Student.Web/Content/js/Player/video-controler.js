//$(function () {
//    alert($("#example_video_1 source").attr("src"));
//});
videojs.options.flash.swf = "/Content/js/plugins/video-js/video-js.swf";
var videoPath = "/library/video/"
$(function () {
    //校验Cookie是否设置
    if (getCookie("isRegister") == null || getCookie("isRegister") == "0") {
        $('#RegisterModal').modal('show');
    }
    //屏蔽右键
    $('#example_video_1').bind('contextmenu', function () { return false; });
    //获取URL中的数据
    var Request = new Object();
    Request = GetRequest();
    var videoUrl = videoPath + Request["src"];
    var posterUrl = videoPath + Request["src"] + ".jpg";
    videojs("example_video_1").ready(function () {
        var myPlayer = this;
        myPlayer.poster(posterUrl);
        if (videoUrl.slice(-3) == "mp4") {
            myPlayer.src([
                { type: "video/mp4", src: videoUrl }
            ]);
        } else {
            
            myPlayer.src([
                { type: "video/avi", src: videoUrl }
        ])
        }
       
        // EXAMPLE: Start playing the video.
        //myPlayer.play();

    });
});

//用户提交个人注册信息
function fnRegister() {
    var regMobile = /^0?1[3|4|5|8][0-9]\d{8}$/;
    var regEmail = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
    var realname = $('#realname').val();
    var mobile = $('#mobile').val();
    var email = $('#email').val();

    $('#alertarea').html("");

    if (realname.length <= 0)
    {        
        myTips("姓名别忘记填");
    } else if (!regMobile.test(mobile))
    {
        myTips("填写的手机号不正确");
    } else if (!regEmail.test(email))
    {
        myTips("填写的邮箱不正确");
    } else {
        //验证成功，记录学生信息到数据库
        
        //ajax加载专业列表
        $.ajax({
            type: "post",
            url: "/Webpage/Register",
            dataType: "json",
            data: "realname=" + realname + "&mobile=" + mobile + "&email=" + email,
            success: function (data) {
                //记录成功，记录Cookieb并隐藏注册窗
                setCookie("isRegister", "1");
                $('#RegisterModal').modal('hide');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
        
    }
}

function myTips(msg){
    var outHtml = "<span class=\"label label-warning\"><i class=\"fa fa-exclamation-circle\"></i> "+ msg +"</span>";
    $('#alertarea').html(outHtml);
}
function GetRequest() { 
    var url = location.search; //获取url中"?"符后的字串 
    var theRequest = new Object(); 
    if (url.indexOf("?") != -1) { 
        var str = url.substr(1); 
        strs = str.split("&"); 
        for(var i = 0; i < strs.length; i ++) { 
            theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]); 
        } 
    } 
    return theRequest; 
} 
function setCookie(name, value) {
    var Days = 365;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}