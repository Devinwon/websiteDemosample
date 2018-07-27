
    $(function(){
        var address = "";//http://192.168.10.182:8000
        var seId = "";
        //订阅管理页面*!/ //行业初始化数据*!/
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
                    var createdConLi_1 = "<li class='liCla_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                    $(".tradeId_1").append(createdConLi_1)
                }
                //添加点击事件*!/
                $(".liCla_1").on("click", function(){
                    $(".tradeChilren_1").css({display:"block"});
                    $(".liCla_1").css({background:"#ffffff",color:"black"})
                    $(this).css({background:"#02AFF3",color:"#ffffff"})
                    var tradeZiId_1 = $(this).attr("id");
                    $(".tradeChilren_1").empty();
                    //获得行业子集*!/
                    $.ajax
                    ({
                        url:address+"/api/purcate/"+tradeZiId_1+"/",
                        type:"get",//提交方式
                        data: {},
                        dataType: "json",
                        success:function(resp)
                        {
                            var a=[]
                            a=resp
                            for(var i=0;i< a.length;i++){
                                var traLi_1 = "<li class='tradeCla_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                                $(".tradeChilren_1").append(traLi_1)
                            }
                            $(".tradeCla_1").on("click",function(){
                                $(".tradeCla_1").removeClass("tradeActive");
                                $(this).addClass("tradeActive")
                                /*武汉*/
                                $(".li_1").html( $(this).html())
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
                        var createdConLi_1 = "<li class='liCla1_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                        $("#departmenId_1").append(createdConLi_1)
                }
                //添加点击事件*!/
                $(".liCla1_1").on("click", function(){
                    $(".deparChilren_1").css({display:"block"});
                    $(".liCla1_1").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var deparZiId_1= $(this).attr("id");
                    $(".deparChilren_1").empty();
                    //获得行业子集*!/
                    $.ajax
                    ({
                        url:address+"/api/orgcate/"+deparZiId_1+"/",
                        type:"get",//提交方式
                        data: {},
                        dataType: "json",
                        success:function(resp)
                        {
                            var a=[]
                            a=resp
                            for(var i=0;i< a.length;i++){
                                var traLi = "<li class='deparCla_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                                $(".deparChilren_1").append(traLi)
                            }
                            $(".deparCla_1").on("click",function(){
                                $(".deparCla_1").removeClass("deparActive");
                                $(this).addClass("deparActive")
                                /*武汉*/
                                $(".li_2").html( $(this).html())
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
                        var createdConLi = "<li class='liCla2_1 liCla2-last'>"+ a[i].Name +"</li>";
                    }else {
                        var createdConLi = "<li class='liCla2_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                    }
                    $("#voiceId_1").append(createdConLi)
                }
               //添加点击事件*!/
                $(".liCla2_1").on("click", function(){
                    $(".areachil_1").css({display:"none"});
                    $(".city_1").css({display:"block"});
                    $(".liCla2_1").css({background:"#ffffff",color:"black"});
                    $(this).css({background:"#02AFF3",color:"#ffffff"});
                    var cityId = $(this).attr("id");
                    $(".city_1").empty()
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
                                var createdCityLi = "<li class='cityCla_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                                $(".city_1").append(createdCityLi)
                            }
                            /*市下划线*/
                            $(".cityCla_1").on("click",function(){
                                $(".areachil_1").css({display:"block"});
                                $(".cityCla_1").removeClass("cityActive");
                                $(this).addClass("cityActive");
                               // /!*请求区*!/
                                var areaId = $(this).attr("id");
                                $(".areachil_1").empty();
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
                                        for (var i=0;i< a.length;i++){
                                           var areaD = "<li class='areaCla_1' id="+a[i].id+">"+ a[i].Name +"</li>";
                                            $(".areachil_1").append(areaD)
                                        }
                                        /*区下划线*/
                                        $(".areaCla_1").on("click",function(){
                                            $(".areaCla_1").removeClass("areaActive");
                                            $(this).addClass("areaActive");
                                            /*武汉*/
                                            $(".li_3").html( $(this).html())
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


        /*订阅页面js*/
        $.ajax({
            url : address+'/api/user_scribes/',
            type : 'get',
            dataType : 'json',
            data : {},
            success : function(resp) {
                var a = {}
                a=resp
                var vince = document.getElementById("select");
                for(var i = 0;i<a.length;i++){
                    var opt = document.createElement("option");
                    opt.innerHTML = a[i].ScribeName;
                    opt.setAttribute('id', a[i].id);
                    vince.appendChild(opt);
                }
            }
        })
        /*订阅头部*/
      $.ajax({
            url : address+'/api/user_scribes/',
            type : 'get',
            dataType : 'json',
            data : {},
            success : function(resp) {
                var a = {}
                a=resp
                var htl = "";
                for (var i=0;i< a.length;i++){
                    htl += "<li class='g' id="+a[i].id+">"+a[i].ScribeName+"</li>"
                }
                $(".groupNum").html(htl)
                /* if($(".g").index($(".g")) == 1){
                    $(".g").addClass("groupActive")
                }*/
                $(".g").click(function(){
                    $(".g").removeClass("groupActive");
                    $(this).addClass("groupActive");
                    var groupId = $(this).attr("id")
                   $.ajax({
                        url : address+'/api/user_scribes/'+groupId+"/",
                        type : 'get',
                        dataType : 'json',
                        data : {},
                        success : function(resp) {
                            var a = {}
                            a=resp
                            var htl_1 = "";
                            //没有取到？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？
                            for (var i=0;i< a.length;i++){
                                htl_1 += "<li class='g_1' id="+a[i].id+">"+a[i].Name+"</li>"
                            }
                            $(".groupCon").html(htl_1)
                        }
                    })
                    /*获得下面相应的招标信息*/
                    var groupLi = $(".groupCon li");
                    for (var i=0;i<groupLi.length;i++){
                        groupLi[i].index = i;
                        if(i==0){
                            var tradeId_1 = $(".groupCon li").attr("id");
                        }else if(i==1){
                            var deparId_1 = $(".groupCon li").attr("id");
                        }else if(i==1){
                            var areaId_1 = $(".groupCon li").attr("id");
                        }
                    }
                    if($(this).html()=="招标"){
                        $.ajax
                        ({
                            type: "GET",//提交方式
                            url:address+"/api/bidinfo/?orgcate="+tradeId_1+"&area="+areaId_1+
                            "&purcate="+deparId_1+"&status=1&pageSize=10&pageNo=1/",//路径
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
                            url:address+"/api/bidinfo/?orgcate="+tradeId_1+"&area="+areaId_1+"&purcate="+deparId_1+"&status=3&pageSize=10&pageNo=1/",//路径
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
                            url:address+"/api/bidinfo/?orgcate="+tradeId_1+"&area="+areaId_1+"&purcate="+deparId_1+"&status=2&pageSize=10&pageNo=1/",//路径
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
            }
        })

       //$(".btn_keep").on("click",function(){
       //     alert($("ul.tradeChilren li.tradeActive").attr("id"));
       // })
        /*得到select里面的每个id*/
        var ose = document.getElementById("select");
        ose.onchange =function (){
            var vinId =  ose.options[ ose.selectedIndex].id;
                seId = vinId
            console.log(vinId+"6666")
        }


        /*项目页面招标中标切换卡点击相应的选项渲染相应的数据*/
        $("#choiceTab_1 li").click(function(){
            $("#choiceTab_1 li").removeClass("active_1");
            $(this).addClass("active_1");
            var tradeId = $("li.tradeActive").attr("id")
            var deparId = $("li.deparActive").attr("id")
            var areaId = $("li.areaActive").attr("id")
            if($(this).html()=="招标"){
                $.ajax
                ({
                    type: "GET",//提交方式
                    url:address+"/api/bidinfo/?orgcate="+tradeId+"&area="+areaId+"&purcate="+deparId+"&status=1&pageSize=10&pageNo=1/",//路径
                    data: {},
                    dataType: "json",
                    //data
                    success:function(resp)
                    {
                        var a = {}
                        a=resp
                        var htl="";
                        for (var i=0;i<a.length;i++){
                            htl+="<div class='titleCla_1'><ul><li><p><img src='../images/pic1.png' alt=''>"
                                +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                                "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                                "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                                "</li><li>今天&nbsp;更新</li></ul></div>";
                        }
                        $(".tenderBottom_1").html(htl)
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
                            htl+="<div class='titleCla_1'><ul><li><p><img src='../images/pic1.png' alt=''>"
                                +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                                "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                                "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                                "</li><li>今天&nbsp;更新</li></ul></div>";
                        }
                        $(".tenderBottom_1").html(htl)
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
                            htl+="<div class='titleCla_1'><ul><li><p><img src='../images/pic1.png' alt=''>"
                                +a[i].TagName+"</p></li><li><a href="+a[i].Url+">"+a[i].Title+"</a></li><li>"+a[i].PurchaseDept+
                                "</li></ul><ul><li ><i class='icon iconfont icon-weizhi'></i>河南省松山是少林去</li>" +
                                "<li><i class='icon iconfont icon-ren3'></i>查看人数<span>"+a[i].GetViews+"</span>" +
                                "</li><li>今天&nbsp;更新</li></ul></div>";
                        }
                        $(".tenderBottom_1").html(htl)
                    }
                })
            }else if($(this).html()=="全部"){
                console.log("444444")
            }
        })
        /*保存组合*/
        //获得自定义名称
        var newGroupValue = $(".newGroup").val();
        var tradeId = $("li.tradeActive").attr("id")
        var deparId = $("li.deparActive").attr("id")
        var areaId = $("li.areaActive").attr("id")
        $(".btn_keep").click(function(){
            $.ajax
            ({
                type: "put",//提交方式
                url: address+"/api/user_scribes/"+seId+"/",//路径
                data: {
                    id:seId,
                    User:1,
                    OrgCategory:tradeId,
                    PurchseArea:areaId,
                    PurchaseCategory:deparId,
                    ScribeName:newGroupValue
                },
                dataType: "json",
                success:function(resp)
                {
                     if(resp.status === 200){
                          alert("更新成功")
                    }else if(resp.status === 304){
                            alert("更新失败")
                    }
                }
            })
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

        /*新建跳跃按钮*/
        $(".button_1").click(function(){
            $(".program_1").css({display:"none"})
            $(".readGrey").css({display:"none"})
            $(".readGrey_2").css({display:"block"})
        })

     /*订阅修改的select部分*/
        $.ajax({
            url : address+'/api/user_scribes/',
            type : 'get',
            dataType : 'json',
            data : {},
            success : function(resp) {
                var a = {}
                a=resp
                for(var i = 0;i<a.length;i++){
                    if(a[i].ScribeName=="新建组合1"){
                        $(".newGroup_2").val("新建组合2");
                    }else if(a[i].ScribeName=="新建组合2"){
                        $(".newGroup_2").val("新建组合3");
                    }else if(a[i].ScribeName=="新建组合3"){
                        $(".newGroup_2").val("新建组合4");
                    }else if(a[i].ScribeName=="新建组合4"){
                        $(".newGroup_2").val("新建组合5");
                    }else if(a[i].ScribeName=="新建组合5"){
                        $(".newGroup_2").val("新建组合1");
                    }
                }
            }
        })



     /*新建行业部门地区数据初始化*/
    //订阅管理页面*!/ //行业初始化数据*!/
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
                var createdConLi_2 = "<li class='liCla_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                $(".tradeId_2").append(createdConLi_2)
            }
            console.log(createdConLi_2+"21352345")
            //添加点击事件*!/
            $(".liCla_2").on("click", function(){
                $(".tradeChilren_2").css({display:"block"});
                $(".liCla_2").css({background:"#ffffff",color:"black"})
                $(this).css({background:"#02AFF3",color:"#ffffff"})
                var tradeZiId_2 = $(this).attr("id");
                $(".tradeChilren_2").empty();
                //获得行业子集*!/
                $.ajax
                ({
                    url:address+"/api/purcate/"+tradeZiId_2+"/",
                    type:"get",//提交方式
                    data: {},
                    dataType: "json",
                    success:function(resp)
                    {
                        var a=[]
                        a=resp
                        for(var i=0;i< a.length;i++){
                            var traLi_2 = "<li class='tradeCla_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                            $(".tradeChilren_2").append(traLi_2)
                        }
                        $(".tradeCla_2").on("click",function(){
                            $(".tradeCla_2").removeClass("tradeActive");
                            $(this).addClass("tradeActive")
                            /*武汉*/
                            $(".li_1_2").html( $(this).html())
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
                var createdConLi_2 = "<li class='liCla1_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                $("#departmenId_2").append(createdConLi_2)
            }
            //添加点击事件*!/
            $(".liCla1_2").on("click", function(){
                $(".deparChilren_2").css({display:"block"});
                $(".liCla1_2").css({background:"#ffffff",color:"black"});
                $(this).css({background:"#02AFF3",color:"#ffffff"});
                var deparZiId_2= $(this).attr("id");
                $(".deparChilren_2").empty();
                //获得行业子集*!/
                $.ajax
                ({
                    url:address+"/api/orgcate/"+deparZiId_2+"/",
                    type:"get",//提交方式
                    data: {},
                    dataType: "json",
                    success:function(resp)
                    {
                        var a=[]
                        a=resp
                        for(var i=0;i< a.length;i++){
                            var traLi = "<li class='deparCla_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                            $(".deparChilren_2").append(traLi)
                        }
                        $(".deparCla_2").on("click",function(){
                            $(".deparCla_2").removeClass("deparActive");
                            $(this).addClass("deparActive")
                            /*武汉*/
                            $(".li_2_2").html( $(this).html())
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
                    var createdConLi = "<li class='liCla2_2 liCla2-last'>"+ a[i].Name +"</li>";
                }else {
                    var createdConLi = "<li class='liCla2_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                }
                $("#voiceId_2").append(createdConLi)
            }
            //添加点击事件*!/
            $(".liCla2_2").on("click", function(){
                $(".areachil_2").css({display:"none"});
                $(".city_2").css({display:"block"});
                $(".liCla2_2").css({background:"#ffffff",color:"black"});
                $(this).css({background:"#02AFF3",color:"#ffffff"});
                var cityId = $(this).attr("id");
                $(".city_2").empty()
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
                            var createdCityLi = "<li class='cityCla_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                            $(".city_2").append(createdCityLi)
                        }
                        /*市下划线*/
                        $(".cityCla_2").on("click",function(){
                            $(".areachil_2").css({display:"block"});
                            $(".cityCla_2").removeClass("cityActive");
                            $(this).addClass("cityActive");
                            // /!*请求区*!/
                            var areaId = $(this).attr("id");
                            $(".areachil_2").empty();
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
                                    for (var i=0;i< a.length;i++){
                                        var areaD = "<li class='areaCla_2' id="+a[i].id+">"+ a[i].Name +"</li>";
                                        $(".areachil_2").append(areaD)
                                    }
                                    /*区下划线*/
                                    $(".areaCla_2").on("click",function(){
                                        $(".areaCla_2").removeClass("areaActive");
                                        $(this).addClass("areaActive");
                                        /*武汉*/
                                        $(".li_3_2").html( $(this).html())
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


  })