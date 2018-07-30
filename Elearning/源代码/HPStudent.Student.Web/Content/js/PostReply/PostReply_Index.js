var rowLength = 15;//一页显示行数
var rowStart = 0;//从多少页面开始
var replyRowLength = 5;//楼层回复分页一页显示的行数
var replyRowStart = 0;//楼层回复分页从第几页开始
var replyPageIndex = 1;
var PTID = getQueryStringByName("PTID");
var IsLookMaster = 0;
var pageType = 0;
$(function () {
    BindPostReplyTable(1, IsLookMaster);
    $('#summernote').summernote({
        lang: 'zh-CN', // default: 'en-US'
        placeholder: "placeholder",
        height: 300,
        dialogsFade: true,// Add fade effect on dialogs
        dialogsInBody: true,
        disableDragAndDrop: false,
        onImageUpload: function (files, editor, $editable) {
            sendFile(files, editor, $editable);
        }
    });

});


function bindReplyHtml(data, judge,isReply,StudentID,pageIndex)
{

    if (isReply=="")
    {
        isReply = "";
    }

    
    var isHide = "hidden";
    var replyHtml = ''
       + '<div class="col-md-2"></div>'
       + '<div class="col-md-10">'
    if (judge > 0)
    {
        
        isHide = "";
        

        var jsonObj = JSON.parse(data.FRContent);
        for (var i = 0; i < jsonObj.length; i++) {
            var byReply = "";
            var byReplyMan = "";
            if(jsonObj[i].ReplyManID==StudentID)
            {
                byReply = '<a onclick="DeleteFloorReply(' + jsonObj[i].FRID + ',' + jsonObj[i].PRID + ')">删除</a>';
            }else
            {
                byReply = '<a onclick="ByReplyBind(' + jsonObj[i].PRID + ',' + jsonObj[i].ReplyManID + ',\'' + jsonObj[i].ReplyManName + '\')">回复</a>';
            }

            if (jsonObj[i].ByReplyManID != 0) {
                byReplyMan = "回复 " + jsonObj[i].ByReplyManName + ""
            }
            replyHtml += '<div class="list-group-item  FloorReply_' + data.PRID + ' " id="Floor_' + jsonObj[i].FRID + '"><span>' + jsonObj[i].ReplyManName + ' ' + byReplyMan + '：</span>' + jsonObj[i].FRContent + '<sapn class="pull-right"><span>' + getDateDiff(getDateTimeStamp(jsonObj[i].CreateDate)) + '</span>&nbsp;&nbsp;&nbsp;&nbsp;' + byReply + '</sapn></div>'
        }

    }

    replyHtml += '<div class="list-group-item  hidden" id="FloorReply_' + data.PRID + '"></div>'
         + '<div id="MyFloorReply_'+data.PRID+'" class="list-group-item ' + isHide + '"><span>' + BindReplyPager(replyRowLength, data.FRCount, pageIndex, data.PRID) + '</span><input type="hidden"  id="txt_PageIndex_' + data.PRID + '" value="' + pageIndex + '" /><input type="hidden"  id="txt_PageCount_' + data.PRID + '" value="' + data.FRCount + '" />&nbsp;<button onclick="ByReplyBind(' + data.PRID + ',0,\'' + data.RealName + '\')" class="pull-right ">我也说一句</button></div>'
         + '<div class="list-group-item  showFloorReply ' + isReply + '" data-type="0" id="showFloorReply_' + data.PRID + '"><div class="input-group" ><input type="text" id="txt_FloorReplyContent_' + data.PRID + '" class="form-control" placeholder="你 回复 ' + data.RealName + '："/><span class="input-group-btn"><button class="btn btn-primary" type="button" onclick="FloorReplyAdd(' + data.PRID + ',0,\'' + data.RealName + '\',0)">发表</button> </span> </div></div>'
         + '</div>';

    return replyHtml;

}






function BindPostReplyTable(pageIndex, showType)
{
    IsLookMaster = showType;
    if (pageIndex == 1) {
        rowStart = 0;
    } else {
        rowStart = 0 + (rowLength * pageIndex - rowLength);//从多少页开始显示
    }


                                                 
 
    var ajaxUrl = getRootPath() + "/PostReply/BindPostReplyTable";
    var bindHtml = '';//绑定的整个HTML
    var IsCollect = '';//是否收藏
    var IsLook = '';//是否只看楼主
    var replyHtml = '';//查看回复
    var IsReply = '';//是否回复
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: { length: rowLength, start: rowStart, PTID: PTID, showType: showType },
        success: function (data) {
            if (data.data.length != null) {
                if (data.IsCollect > 0) {
                    IsCollect = '已收藏';

                } else {
                    IsCollect = '收藏';
                }
                if (showType == 1) {
                    IsLook = '取消只看楼主';
                    pageType = 1;
                    IsLookMaster = 0;
                } else
                {
                    IsLook = '只看楼主';
                    pageType = 0;
                    IsLookMaster = 1;
                }
                bindHtml += '<div class="panel panel-primary">'
                                                 + ' <div class="panel-heading">'
                                                  + ' <span class="pull-right">  <a onclick="MessageReport(' + data.PostManID + ',\'' + data.PostManName + '\',' + PTID + ',2)">举报</a></span> '
                                                    + ' <h3 class="panel-title text-danger">讨论主题：' + data.PostThemeContent + '</h3>'
                                                  + ' </div>'
                                                  + ' <div class="panel-body">'
                                                    + '  ' + data.PostContent + ''
                                                  + ' </div>'
                                                 + '  <div class="panel-footer">'
                                                      + ' <span class="pull-right">1楼&nbsp;&nbsp;' + getDateDiff(data.PostDate.replace("/Date(", "").replace(")/", "")) + '&nbsp;&nbsp;<button data-type="IsLookMaster"   data-id=' + IsLookMaster + ' class="btn btn-primary btn-sm">' + IsLook + '</button>  <button data-type="collect" data-id=' + PTID + ' class="btn btn-primary btn-sm">' + IsCollect + '</button>  <a class="btn btn-sm btn-primary"   data-type="landlordReply" href="#summer_note_top">回复</a></span>'
                                                 + '  </div>'
                                              + ' </div>';
                for (var i = 0; i < data.data.length; i++) {
                    if (data.data[i].FRCount > 0)//判断楼层中是否有回复
                    {
                        IsReply = '<button data-id=' + data.data[i].PRID + ' data-type="packupreply" class="btn btn-primary btn-sm">收起回复</button>';
                        replyHtml = bindReplyHtml(data.data[i],1,"hidden",data.LoginStudentID,1);
                    } else
                    {
                        replyHtml = bindReplyHtml(data.data[i], 0, "hidden",0,0);
                        IsReply = '<button data-id=' + data.data[i].PRID + ' data-type="reply" class="btn btn-primary btn-sm">回复</button>';
                    }

                    bindHtml += '<div class="panel panel-primary">'
                                                 + ' <div class="panel-heading">'
                                                    + '<h3 class="panel-title"> 回帖人： ' + data.data[i].RealName + '</h3><span class="pull-right"><a onclick="MessageReport('+data.data[i].StudentID+',\''+data.data[i].RealName+'\','+data.data[i].PRID+',3)">举报</a></span>'

                                                  + ' </div>'
                                                  + ' <div class="panel-body">'
                                                    + '  ' + data.data[i].PRContent + ''
                                                    + '<div id="showReply_' + data.data[i].PRID + '" class=""><hr></hr><div>'
                                                      + replyHtml
                                                      + '</div></div>'
                                                  + ' </div>'
                                                 + '  <div class="panel-footer">'
                                                      + ' <span class="pull-right">' + data.data[i].FloorNumber + '楼&nbsp;&nbsp;' + getDateDiff(data.data[i].CreateDate.replace("/Date(", "").replace(")/", "")) + '&nbsp;&nbsp;' + IsReply + ' </span>'
                                                 + '  </div>'
                                                 + '  </div>'
                                              + ' </div>';
                }
                $("#txt_PageCount").val(data.recordsTotal);
                $("#btn_Submit").val(data.PRID);
                if (data.recordsTotal==0)
                {
                    data.recordsTotal = 1;
                }
                BindPager(rowLength, data.recordsTotal, Number(pageIndex));
            }
            
            $("#datatable_postReply").html(bindHtml);
            
        } 

    });
    onresize();
}



//翻页
$("#pager").delegate("li", "click", function () {//
    var pageIndex = $(this).attr("page_number");
    var countPage = Math.floor(Number($("#txt_PageCount").val()) / rowLength);
    if (Number($("#txt_PageCount").val()) % rowLength != 0) {
        countPage += 1;
    }
    if (pageIndex > 0 && pageIndex <= countPage) {
        BindPostReplyTable(pageIndex, pageType);
    }
});






//收藏，回复，只看楼主

$("#datatable_postReply").delegate(".btn-primary", "click", function () {
    var ID = Number($(this).attr("data-id"));
    var type = $(this).attr("data-type");
    if (type == "IsLookMaster")//只看楼主
    {
        BindPostReplyTable(1, ID);
    }
    else if (type == "collect")//收藏
    {
        var result =$(this);
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostReply/Collect",
            data: { PTID: ID },
            success: function (data)
            {
                if (data == 1)
                {
                    noty({
                        text: "收藏成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    result.html("已收藏");
                } else if (data == 2)
                {
                    noty({
                        text: "取消收藏成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    result.html("收藏");

                }
            }
            
        });
        
    }
    else if (type == "landlordReply")//回复楼主
    {
        $('#summernote').onfocus();
    }
    else if (type == "reply")//回复
    {
        var result = $(this);
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostReply/GetFloorReplyTable",
            data: { PRID: ID },
            success: function (data)
            {
                var sfrID = '';
                $(".showFloorReply").each(function () {
                    if ($(this).hasClass("hidden")==false) {
                        sfrID =$(this).attr('id');
                    }
                });
                var strs = new Array();
                strs = sfrID.split("_");
                $("#showFloorReply_" + strs[1] + "").addClass("hidden");
                if ($("#showFloorReply_" + strs[1] + "").attr("data-type") == 0)
                {
                    $("[data-id='" + strs[1] + "']").html("回复");
                    $("[data-id='" + strs[1] + "']").attr("data-type", "reply");
                }
                var replyHtml = '';
                $("#showReply_" + ID + "").html("");
                if (data.FRCount > 0)//判断是否有回复
                {
                    var replyPageIndex = Number($("txt_PageIndex_" + ID + "").val());
                    replyHtml = bindReplyHtml(data, 1, "", "", replyPageIndex);
                    $("#showReply_" + ID + "").html(replyHtml);
                    $("#showReply_" + ID + "").removeClass("hidden");
                  
                } else
                {
                    replyHtml = bindReplyHtml(data, 0, "","",0);
                    $("#showReply_" + ID + "").html(replyHtml);
                    $("#showReply_" + ID + "").removeClass("hidden");
                    
                }
                
                result.html("收起回复");
                result.attr("data-type", "packupreply");
            }

        });

        
    }
    else if (type == "packupreply")//收起回复
    {
        $("#showReply_" + ID + "").addClass("hidden");
        $(this).html("回复");
        $(this).attr("data-type", "reply");
        $("#showReply_" + ID + "").slideDown("slow");
    }

    

});


//被回复人
function ByReplyBind(PRID, ByReplyManID, byReplyManName,isByReply)
{
    var sfrID = '';
    $(".showFloorReply").each(function () {
        if ($(this).hasClass("hidden") == false) {
            sfrID = $(this).attr('id');
        }
    });
    var strs = new Array();
    strs = sfrID.split("_");
    $("#showFloorReply_" + strs[1] + "").addClass("hidden");
    if ($("#showFloorReply_" + strs[1] + "").attr("data-type") == 0) {
        $("[data-id='" + strs[1] + "']").html("回复");
        $("[data-id='" + strs[1] + "']").attr("data-type", "reply");
    } else
    {
        $("[data-id='" + strs[1] + "']").html("收起回复");
        $("[data-id='" + strs[1] + "']").attr("data-type", "packupreply");
    }
    $(".showFloorReply").addClass("hidden");
    $("#showFloorReply_" + PRID + "").removeClass("hidden");
    $("#showFloorReply_" + PRID + "").attr("data-type", "1");

    if (ByReplyManID > 0)
    {
       $("#showFloorReply_" + PRID + "").find("button").attr("onclick", "FloorReplyAdd(" + PRID + "," + ByReplyManID + ",\"" + byReplyManName + "\",1)");
       $("#showFloorReply_" + PRID + "").find("input").attr("placeholder", "你 回复 : " + byReplyManName + "");
    }   
}




//楼层回复
function FloorReplyAdd(PRID, ByReplyManID, ByReplyManName,type)
{
    var FloorReplyContent = $("#txt_FloorReplyContent_" + PRID + "").val();
    if (FloorReplyContent != "") {
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostReply/FloorReplyAdd",
            data: { PRID: PRID, FloorReplyContent: FloorReplyContent, ByReplyManID: ByReplyManID },
            success: function (data) {
                if (data > 0) {
                    noty({
                        text: "回复成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    var myDate = new Date();
                    var bindHtml = '<span>你 回复 ' + ByReplyManName + '：</span>' + FloorReplyContent + '<sapn class="pull-right"><span>' + getDateDiff(getNowFormatDate()) + '</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</sapn>';
                    $("#MyFloorReply_" + PRID + "").removeClass("hidden");
                    $("#FloorReply_" + PRID + "").html(bindHtml);
                    $("#FloorReply_" + PRID + "").removeClass("hidden");
                    $("#showFloorReply_" + PRID + "").addClass("hidden");
                    $("#txt_FloorReplyContent_" + PRID + "").val("");    
                } else
                {
                    noty({
                        text: "回复失败！",
                        layout: 'topRight',
                        type: 'error',
                    });
                }
                
                

            }
        });
    }
    else
    {
        noty({
            text: "回复内容不能为空！",
            layout: 'topRight',
            type: 'error',
        });

    }
    
}


function DeleteFloorReply(FRID,PRID)
{
    $.ajax({
        type: "POST",
        url: getRootPath() + "/PostReply/FloorReplyDel",
        data: { FRID: FRID, PRID: PRID },
        success: function (data)
        {
            if (data.IsSuccess > 0) {
                noty({
                    text: "删除成功！",
                    layout: 'topRight',
                    type: 'success',
                });
                if (data.FRCount > 0)//判断是否有回复
                {                    
                    var replyPageIndex = Math.floor(data.FRCount / replyRowLength);
                    if (data.FRCount % replyRowLength != 0) {
                        replyPageIndex += 1;
                    }
                    if (replyPageIndex < Number($("#txt_PageIndex_" + PRID + "").val())) {
                        replyHtml = bindReplyHtml(data, 1, "hidden", data.LoginStudentID, replyPageIndex);
                        $("#showReply_" + PRID + "").html(replyHtml);
                    } else
                    {
                        replyHtml = bindReplyHtml(data, 1, "hidden", data.LoginStudentID, Number($("#txt_PageIndex_" + PRID + "").val()));
                        $("#showReply_" + PRID + "").html(replyHtml);
                    }
                   

                } else {
                   
                    replyHtml = bindReplyHtml(data, 0, "hidden", 0,0);
                    $("#showReply_" + PRID + "").html(replyHtml);
                    $("#showReply_" + PRID + "").addClass("hidden");
                    $("[data-id='" + PRID + "']").html("回复");
                    $("[data-id='" + PRID + "']").attr("data-type", "reply");
                    $("#showReply_" + PRID + "").slideDown("slow");
                }
            } else
            {
                noty({
                    text: "删除失败！",
                    layout: 'topRight',
                    type: 'success',
                });
            }
        }
    })


}





//楼层回复分页绑定
function BindReplyPager(rowNumber, countRowNumber, pageIndex, PRID) {
    if (countRowNumber == 0 && countRowNumber=='')
    {
        countRowNumber = 1;
        pageIndex = 1;
    }

    var countPage = Math.floor(countRowNumber / rowNumber);
    if (countRowNumber % rowNumber != 0) {
        countPage += 1;
    }
    var bindHtml = "";
    if (pageIndex != 1) {
        bindHtml += ' <a  class="btn btn-primary btn-xs"   onclick="ReplyPagerChangeGetTable(' + PRID + ',' + (pageIndex - 1) + ')" page_number="' + (pageIndex - 1) + '"><i class="fa fa-chevron-left"></i></a>&nbsp;';
    } else {
        bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled"><i class="fa fa-chevron-left "></i></a>&nbsp;';
    }

    if (pageIndex - 2 <= 1) {
        if (countPage < 5) {
            for (var i = 1; i <= countPage; i++) {
                if (i == pageIndex) {
                    bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled">' + i + '</a>&nbsp;';
                } else {
                    bindHtml += '<a  class="btn btn-primary btn-xs"   onclick="ReplyPagerChangeGetTable(' + PRID + ',' + i + ')" page_number="' + i + '">' + i + '</a>&nbsp;';
                }

            }
        } else {
            for (var i = 1; i <= 5; i++) {
                if (i == pageIndex) {
                    bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled">' + i + '</a>&nbsp;';
                } else {
                    bindHtml += '<a  class="btn btn-primary btn-xs"    onclick="ReplyPagerChangeGetTable(' + PRID + ',' + i + ')" page_number="' + i + '">' + i + '</a>&nbsp;';
                }

            }
        }

    }
    else if (pageIndex - 2 > 1 && pageIndex + 2 <= countPage) {
        for (var i = pageIndex - 2; i < pageIndex + 3; i++) {
            if (i == pageIndex) {
                bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled">' + i + '</a>&nbsp;';
            } else {
                bindHtml += '<a  class="btn btn-primary btn-xs"   onclick="ReplyPagerChangeGetTable(' + PRID + ',' + i + ')" page_number="' + i + '">' + i + '</a>&nbsp;';
            }

        }

    }
    else if (pageIndex + 2 > countPage) {
        if (countPage < 5) {
            for (var i = countPage - 3; i <= countPage; i++) {
                if (i == pageIndex) {
                    bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled">' + i + '</a>&nbsp;';
                } else {
                    bindHtml += '<a  class="btn btn-primary btn-xs"   onclick="ReplyPagerChangeGetTable(' + PRID + ',' + i + ')" page_number="' + i + '">' + i + '</a>&nbsp;';
                }

            }
        }
        else {
            for (var i = countPage - 4; i <= countPage; i++) {
                if (i == pageIndex) {
                    bindHtml += ' <a class="btn btn-primary btn-xs"   disabled ="disabled">' + i + '</a>&nbsp;';
                } else {
                    bindHtml += '<a  class="btn btn-primary btn-xs"   onclick="ReplyPagerChangeGetTable(' + PRID + ',' + i + ')" page_number="' + i + '">' + i + '</a>&nbsp;';
                }

            }

        }

    }


    if (pageIndex < countPage) {
        bindHtml += '  <a    onclick="ReplyPagerChangeGetTable(' + PRID + ',' + (pageIndex + 1) + ')"   class="btn btn-primary btn-xs"><i class="fa fa-chevron-right"></i></a>&nbsp;';
    }
    else {
        bindHtml += '  <a   class="btn btn-primary  btn-xs" disabled ="disabled" ><i class="fa fa-chevron-right"></i></a>&nbsp;';
    }
    return bindHtml;

}





function ReplyPagerChangeGetTable(PRID, pageIndex)
{
    var countPage = Math.floor(Number($("#txt_PageCount_" + PRID + "").val()) / replyRowLength);
    if (Number($("#txt_PageCount_" + PRID + "").val()) % replyRowLength != 0) {
        countPage += 1;
    }
    if (pageIndex > 0 && pageIndex <= countPage) {
        if (pageIndex == 1) {
            replyRowStart = 0;
        } else {
            replyRowStart = 0 + (replyRowLength * pageIndex - replyRowLength);//从多少页开始显示
        }
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostReply/ReplyPagerChangeGetTable",
            data: { length: replyRowLength, start: replyRowStart, PRID: PRID, },
            success: function (data)
            {
                $("txt_PageIndex_" + PRID + "").val(pageIndex);
                replyHtml = bindReplyHtml(data, 1, "hidden", data.LoginStudentID, pageIndex);
                $("#showReply_" + PRID + "").html(replyHtml);
                $("#showReply_" + PRID + "").removeClass("hidden");
            }
        });

    }
}


$("#btn_Submit").on("click", function () {
    //var PRContent = encodeURI($('#summernote').code().toString());
    //if ($('#summernote').code().toString() == "<p><br></p>")
    //{
    //    noty({
    //        text: "回复内容不能为空！",
    //        layout: 'topRight',
    //        type: 'error',
    //    });

    //} else
    //{
    //    $.ajax({
    //        type: "POST",
    //        data: { PTID: PTID, PRContent: PRContent },
    //        url: getRootPath() + "/PostReply/PostReplyAdd",
    //        success: function (data) {
    //            if (data > 0) {
    //                var countPage = Math.floor(Number(data) / rowLength);
    //                if (Number(data) % rowLength != 0) {
    //                    countPage += 1;
    //                }
    //                BindPostReplyTable(countPage, pageType);

    //            }
    //        },
    //        complete: function () {
    //            $('#summernote').code("");
    //        }
    //    });
    //}

});

function sendFile(file, editor, $editable) {
    var formData = new FormData();
    formData.append("file", file[0]);

    $.ajax({
        data: formData,
        type: "POST",
        url: getRootPath() + "/PostReply/UploadProductDescriptionImage",
        cache: false,
        contentType: false,
        processData: false,
        cache: false,
        success: function (imageUrl) {
            editor.insertImage($editable, imageUrl);
            //$('.summernote').summernote('editor.insertImage',imageUrl);  
        },
        error: function () {

        }
    })
}






//新建题目页面自动下拉
function postion() {
    var h = 1000;
    var speed = 1000;
    var windowHeight = parseInt($("body").css("height"));
    $('html > body').animate({
        scrollTop: h + 'px'
    },
            speed);
}

function MessageReport(BeReportUID,BeReporter,Tid,Type)
{
    $.ajax({ 
        type: "POST",
        url: getRootPath() + "/PostReply/MessageReport",
        success: function (html)
        {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        }, complete: function ()
        {
            $("#txt_BeReportUID").val(BeReportUID);
            $("#txt_BeReporter").val(BeReporter);
            $("#txt_Tid").val(Tid);
            $("#txt_Type").val(Type);
        }
    });

}

function MessageReportAdd()
{
    var BeReportUID =  $("#txt_BeReportUID").val();
    var BeReporter =  $("#txt_BeReporter").val();
    var Tid = $("#txt_Tid").val();
    var Type = $("#txt_Type").val();
    var Body = $("#txt_Body").val();
    if (Body == "") {
        noty({
            text: "举报内容不能为空！",
            layout: 'topRight',
            type: 'error',
        });
    } else
    {
        $.ajax({
            type: "POST",
            data: { Tid: Tid, BeReportUID: BeReportUID, BeReporter: BeReporter, Body: Body, Type: Type },
            url: getRootPath() + "/PostReply/MessageReportAdd",
            success: function (data) {
                if (data > 0) {
                    noty({
                        text: "感谢你的举报，让论坛成为更加文明的净土！",
                        layout: 'topRight',
                        type: 'success',
                    });
                } else
                {
                    noty({
                        text: "出错拉，请联系管理员！",
                        layout: 'topRight',
                        type: 'success',
                    });
                }
            },
            complete: function () {
                $('#summernote').code("");
                $('#edit-manager').modal('hide');
                $("#pop_modaldialog").empty();
            }
        });

    }



}
