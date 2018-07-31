//$(function () {
//    alert($("#example_video_1 source").attr("src"));
//});
videojs.options.flash.swf = "/Content/js/plugins/video-js/video-js.swf";
var videoPath = "/library/video/"
$(function () {
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
