/**
 * Created by lenovo on 2018/4/2.
 */
$(function(){
    /*时时查询传给后台的参数*/
    var address = ""//http://193.112.160.28";
    /*页面切换卡*/
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
            /*显示隐藏*/
            $(".program_1").css({display:"block"})
            $(".tederInstail").css({display:"none"})

    })
    /*招标详情 点击每个div*/
    $(".titleCla").click(function(){
        $(".program_1").css({display:"none"})
        $(".tederInstail").css({display:"block"})
    })
    /*点击订阅管理跳转到管理页面*/
    $("#readId").click(function(){
        $("#dis").css({display:"none"})
        $("#readGrey").css({display:"block"})
    })
   /* $.ajax({
        type: "get", //提交方式
        url: address+"/api/bidinfo/" +
        "?title="+inputValue+"&orgcate="+allText1+"&purcate="+allText+"&v="+valueA+"/",//路径
        dataType: "json",
        data: {
        },//数据，这里使用的是Json格式进行传输
        success: function (resp) {//返回数据根据结果进行相应的处理
            var a = {}
            a=resp

            for (var i=0;i<a.length;i++){
                var dataTitle = a[i].Title;
                var urlTitle = a[i].Url;
                var oLis = document.createElement("li");
                var oAs = document.createElement("a");
                oLis.appendChild(oAs);
                oAs.innerHTML = dataTitle;
                newTender.appendChild(oLis);
                oAs.setAttribute('href',urlTitle);
            }
        }
    });*/
    /*行业初始化数据*/
    $.ajax
    ({
        url:address+"/api/purcate/",
        type:"get",//提交方式
        data: {},
        dataType: "json",
        success:function(resp)
        {
            var a=[]
            a=resp
            for(var i=0;i<a.length;i++){
                    var createdConLi = "<li class='liCla' id="+a[i].id+">"+ a[i].Name +"</li>";
                    $(".tradeId").append(createdConLi)
            }
            /*添加点击事件*/
            $(".liCla").on("click", function(){
                $(".tradeChilren").css({display:"block"});
                $(".liCla").css({background:"#ffffff",color:"black"})
                $(this).css({background:"#02AFF3",color:"#ffffff"})
                var tradeZiId = $(this).attr("id");
                $(".tradeChilren").empty();
                /*获得行业子集*/
                $.ajax
                ({
                    url:address+"/api/purcate/"+tradeZiId+"/",
                    type:"get",//提交方式
                    data: {},
                    dataType: "json",
                    success:function(resp)
                    {
                        var a=[]
                        a=resp
                        for(var i=0;i< a.length;i++){
                            var traLi = "<li class='tradeCla' id="+a[i].id+">"+ a[i].Name +"</li>";
                            $(".tradeChilren").append(traLi)
                        }
                        $(".tradeCla").on("click",function(){
                            $(".tradeCla").removeClass("tradeActive");
                            $(this).addClass("tradeActive")
                        })
                    }
                })
            });
        }
    })
    /*部门初始化数据*/
    $.ajax
    ({
        //where to get data
        url:address+"/api/orgcate/",
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
                    var createdConLi = "<li class='liCla1' id="+a[i].id+">"+ a[i].Name +"</li>";
                    $("#departmenId").append(createdConLi)
            }
            /*添加点击事件*/
            $(".liCla1").on("click", function(){
                $(".deparChilren").css({display:"block"});
                $(".liCla1").css({background:"#ffffff",color:"black"});
                $(this).css({background:"#02AFF3",color:"#ffffff"});
                var deparZiId = $(this).attr("id");
                console.log(deparZiId+"111111")
                $(".deparChilren").empty();
                /*获得部门子集*/
                $.ajax
                ({
                    url:address+"/api/orgcate/"+deparZiId+"/",
                    type:"get",//提交方式
                    data: {},
                    dataType: "json",
                    success:function(resp)
                    {
                        var a=[]
                        a=resp
                        for(var i=0;i< a.length;i++){
                            var traLi = "<li class='deparCla' id="+a[i].id+">"+ a[i].Name +"</li>";
                            $(".deparChilren").append(traLi)
                        }
                        $(".deparCla").on("click",function(){
                            $(".deparCla").removeClass("deparActive");
                            $(this).addClass("deparActive")
                        })
                    }
                })
            });
        }
    })
    /*省市区初始化数据*/
   $.ajax
    ({
        //where to get data
        url:address+"/api/pvnshow/",
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
            }
            /*添加点击事件*/
            $(".liCla2").on("click", function(){
                $(".areachil").css({display:"none"});
                $(".city").css({display:"block"});
                $(".liCla2").css({background:"#ffffff",color:"black"});
                $(this).css({background:"#02AFF3",color:"#ffffff"});
                var cityId = $(this).attr("id");
                $(".city").empty()
                $.ajax
                ({
                    url:address+"/api/pvnshow/"+cityId+"/",//路径
                    type:"get",//提交方式
                    data: {},
                    dataType: "json",
                    success:function(resp)
                    {
                        var a=[]
                        a=resp
                         for (var i=0;i< a.length;i++){
                             var createdCityLi = "<li class='cityCla' id="+a[i].id+">"+ a[i].Name +"</li>";
                             $(".city").append(createdCityLi)
                         }

                        /*市下划线*/
                        $(".cityCla").on("click",function(){
                            $(".areachil").css({display:"block"});
                            $(".cityCla").removeClass("cityActive");
                            $(this).addClass("cityActive");
                            /*请求区*/
                            var areaId = $(this).attr("id");
                            $(".areachil").empty();
                            $.ajax
                            ({
                                url:address+"/api/pvnshow/"+areaId+"/",//路径
                                type:"get",//提交方式
                                data: {},
                                dataType: "json",
                                success:function(resp)
                                {
                                    var a=[]
                                    a=resp
                                    var htm = "";
                                    for (var i=0;i< a.length;i++){
                                        console.log(a[i].Name)
                                         htm += "<li class='areaCla' id="+a[i].id+">"+ a[i].Name +"</li>";
                                    }
                                    $(".areachil").html(htm)
                                    /*区下划线*/
                                   $(".areaCla").on("click",function(){
                                        $(".areaCla").removeClass("areaActive");
                                        $(this).addClass("areaActive");
                                    })
                                }
                            })
                        })
                    }
                })
            });
        }
    })

    $(".area li").click(function(){
        $(".area li").removeClass("areaActive");
        $(this).addClass("areaActive");
    })

  /*项目页面招标中标切换卡招标部分初始化数据*/
    $.ajax
    ({
        type: "GET",//提交方式
        url:address+"/api/bidinfo/",//路径
        data: {},
        dataType: "json",
        //data
        success:function(resp)
        {
            var a = {}
            a=resp
            var htl="";
            for (var i=0;i<a.length;i++){
                 htl+="<div class='titleCla'><ul><li><p><img src='../images/pic1.png' alt=''>"
                     +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                     "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                     "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                     "</li><li>今天&nbsp;更新</li></ul></div>";
            }
            $(".tenderBottom").html(htl)
        }
    })
    /*项目页面招标中标切换卡点击相应的选项渲染相应的数据*/
    $("#choiceTab li").click(function(){
        $("#choiceTab li").removeClass("active");
        $(this).addClass("active");
        var tradeId = $("li.tradeActive").attr("id")
        var deparId = $("li.deparActive").attr("id")
        var areaId = $("li.areaActive").attr("id")
       if($(this).html()=="招标"){
            $.ajax
            ({
                type: "GET",//提交方式
                url:address+"/api/bidinfo/?orgcate="+tradeId+"&area="+areaId+
                "&purcate="+deparId+"&status=1&pageSize=10&pageNo=1/",//路径
                data: {},
                dataType: "json",
                //data
                success:function(resp)
                {
                    var a = {}
                    a=resp
                        var htl="";
                        for (var i=0;i<a.length;i++){
                            htl+="<div class='titleCla'><ul><li><p><img src='../images/pic1.png' alt=''>"
                                +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                                "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                                "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                                "</li><li>今天&nbsp;更新</li></ul></div>";
                        }
                        $(".tenderBottom").html(htl)
                }
            })
        }else if($(this).html()=="中标"){
            $.ajax
            ({
                type: "GET",//提交方式
                url:address+"/api/bidinfo/?orgcate="+tradeId+"&area="+areaId+"&purcate="+deparId+"&status=3&pageSize=10&pageNo=1/",//路径
                data: {},
                dataType: "json",
                //data
                success:function(resp)
                {
                    var a = {}
                    a=resp
                    var htl="";
                    for (var i=0;i<a.length;i++){
                        htl+="<div class='titleCla'><ul><li><p><img src='../images/pic1.png' alt=''>"
                            +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                            "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                            "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                            "</li><li>今天&nbsp;更新</li></ul></div>";
                    }
                    $(".tenderBottom").html(htl)
                }
            })
        }else if($(this).html()=="废标"){
            $.ajax
            ({
                type: "GET",//提交方式
                url:address+"/api/bidinfo/?orgcate="+tradeId+"&area="+areaId+"&purcate="+deparId+"&status=2&pageSize=10&pageNo=1/",//路径
                data: {},
                dataType: "json",
                //data
                success:function(resp)
                {
                    var a = {}
                    a=resp
                    var htl="";
                    for (var i=0;i<a.length;i++){
                        htl+="<div class='titleCla'><ul><li><p><img src='../images/pic1.png' alt=''>"
                            +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                            "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                            "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                            "</li><li>今天&nbsp;更新</li></ul></div>";
                    }
                    $(".tenderBottom").html(htl)
                }
            })
        }else if($(this).html()=="全部"){
            console.log("444444")
        }
    })

    /*分页*/
    /*项目页面分页*/
    var pageNavObj = null;//用于储存分页对象
    pageNavObj = new PageNavCreate("PageNavId",{
        pageCount:20,
        currentPage:1,
        perPageNum:5,
    });
    pageNavObj.afterClick(pageNavCallBack);
    function pageNavCallBack(clickPage){
        pageNavObj = new PageNavCreate("PageNavId",{
            pageCount:20,
            currentPage:clickPage,
            perPageNum:5,
        });
        listPage(pageNavObj.currentPage,pageNavObj.perPageNum);
        pageNavObj.afterClick(pageNavCallBack);
    }
    var tradeId1 = $("li.tradeActive").attr("id")
    var deparId1 = $("li.deparActive").attr("id")
    var areaId1= $("li.areaActive").attr("id")
    function  listPage(page,num){
        console.log(page+"==================listPage");
        $.ajax
        ({
            type: "GET",//提交方式
            url:address+"/api/bidinfo/?orgcate="+tradeId1+"&area="+deparId1+"&purcate="+areaId1+"&status=1&pageSize=10&pageNo="+page+"/",//路径
            data: {},
            dataType: "json",
            //data
            success:function(resp)
            {
                var a = {}
                a=resp
                console.log(a+"11111")
                for (var i=0;i<a.length;i++){
                    var htl="";
                    for (var i=0;i<a.length;i++){
                        htl+="<div class='titleCla'><ul><li><p><img src='../images/pic1.png' alt=''>"
                            +a[i].TagName+"</p></li><li><a href="+ a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                            "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                            "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                            "</li><li>今天&nbsp;更新</li></ul></div>";
                    }
                    $(".tenderBottom").html(htl)
                }
            }
        })
    }

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
