/**
 * Created by lenovo on 2018/4/12.
 */
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
            $(".tradeId1_1").append(createdConLi_1)
            console.log( createdConLi_1)
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
             var tradeId_1 =  $(this).attr("id");

             console.log(tradeId_1+"1234")
             })
             }
             })
             });
            //$("#wuhan li")[0].append(tradeHtml)*!/
        }
    }
})