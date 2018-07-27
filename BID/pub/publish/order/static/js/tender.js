/**
 * Created by lenovo on 2018/4/2.
 */
$(function(){
    $("#ulNav li").click(function(){
        $("#ulNav li").removeClass("navActive")
        $(this).addClass("navActive");
            var divShow = $("#content").children("div");
            /*li标签的顺序和div的顺序是对应的，获取索引*/
            var index = $(this).index();
            /*展示当前li对应的div内容,利用方法显示和隐藏*/
            $(divShow[index]).show();
            /*隐藏同级元素*/
            $(divShow[index]).siblings("div").hide();

    })
    /*行业初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/purcateall/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(a[i].ParentId==null){
                    var createdConLi = "<li class='liCla'>"+ a[i].Name +"</li>";
                    $("#tradeId").append(createdConLi)
                }
                /*添加点击事件*/
                $(".liCla").on("click", function(){
                 $(".liCla").css({background:"#ffffff",color:"black"});
                 $(this).css({background:"#02AFF3",color:"#ffffff"});
                 var htmlText = $(this).html();
                 var allTradeText =htmlText;
                 console.log(htmlText)
                 });
            }
        }
    })
    /*部门初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/orgcateall/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(a[i].ParentId==null){
                    var createdConLi = "<li class='liCla1'>"+ a[i].Name +"</li>";
                    $("#departmenId").append(createdConLi)
                }
                /*添加点击事件*/
                $(".liCla1").on("click", function(){
                    $(".liCla1").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var htmlText = $(this).html();
                    var allTradeText =htmlText;
                    console.log(htmlText)
                });
            }
        }
    })
    /*省市区初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/pvnshow/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(i == a.length){
                    var createdConLi = "<li class='liCla2 liCla2-last'>"+ a[i].Name +"</li>";
                }else {
                    var createdConLi = "<li class='liCla2' id="+a[i].id+">"+ a[i].Name +"</li>";
                }
                $("#voiceId").append(createdConLi)
                /**/
                /*添加点击事件*/
                $(".liCla2").on("click", function(){
                    $(".liCla2").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var cityId = $(this).attr("id");
                    $.ajax
                    ({
                        //where to get data
                        url:"/api/pvnshow/"+cityId+"/",//路径
                        // what type to exchange
                        type:"get",//提交方式
                        data: {},
                        dataType: "json",
                        //data
                        success:function(resp)
                        {
                            var a=[]
                            a=resp
                            console.log(a+"111111")
                            for (var i=0;i< a.length;i++){

                            }
                        }
                    })
                });
            }
        }
    })
    /*订阅页面*/
    /*行业初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/purcateall/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(a[i].ParentId==null){
                    var createdConLi = "<li class='liCl'>"+ a[i].Name +"</li>";
                    $("#tradeId1").append(createdConLi)
                }
                /*添加点击事件*/
                $(".liCl").on("click", function(){
                    $(".liCl").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var htmlText = $(this).html();
                    var allTradeText =htmlText;
                    console.log(htmlText)
                });
            }
        }
    })
    /*部门初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/orgcateall/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(a[i].ParentId==null){
                    var createdConLi = "<li class='liCl1'>"+ a[i].Name +"</li>";
                    $("#departmenId1").append(createdConLi)
                }
                /*添加点击事件*/
                $(".liCl1").on("click", function(){
                    $(".liCl1").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var htmlText = $(this).html();
                    var allTradeText =htmlText;
                    console.log(htmlText)
                });
            }
        }
    })
    /*省市区初始化数据*/
    $.ajax
    ({
        //where to get data
        url:"/api/pvnshow/",
        // what type to exchange
        type:"get",//提交方式
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                if(i == a.length){
                    var createdConLi = "<li class='liCl2 liCla2-last1'>"+ a[i].Name +"</li>";
                }else {
                    var createdConLi = "<li class='liCl2' id="+a[i].id+">"+ a[i].Name +"</li>";
                }
                $("#voiceId1").append(createdConLi)
                /**/
                /*添加点击事件*/
                $(".liCl2").on("click", function(){
                    $(".liCl2").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var cityId = $(this).attr("id");
                    $.ajax
                    ({
                        //where to get data
                        url:"/api/pvnshow/"+cityId+"/",//路径
                        // what type to exchange
                        type:"get",//提交方式
                        data: {},
                        dataType: "json",
                        //data
                        success:function(resp)
                        {
                            var a=[]
                            a=resp
                            console.log(a+"111111")
                            for (var i=0;i< a.length;i++){

                            }
                        }
                    })
                });
            }
        }
    })
    /*招标详情*/
    $(".titleCla").click(function(){
        $(".program_1").css({display:"none"})
        $(".tederInstail").css({display:"block"})
    })
})
/*显示隐藏*/
$("#vinMore").toggle(function(){
    $("#voiceId").css({height:"auto"})
    $("#vinMore").val("收起")
},function(){
   $("#voiceId").css({height:"24px"})
    $("#vinMore").val("更多")
})
/*显示隐藏*/
$("#vinMore1").toggle(function(){
    $("#voiceId1").css({height:"auto"})
    $("#vinMore1").val("收起")
},function(){
    $("#voiceId1").css({height:"24px"})
    $("#vinMore1").val("更多")
})