var rowLength = 15;//一页显示行数
var rowStart = 0;//从多少页面开始

$(function () {
    BindPostTheme(1);
});


//绑定帖子
function BindPostTheme(pageIndex)
{
    
    if (pageIndex == 1) {
        rowStart = 0;
    } else
    {
        rowStart = 0 + (rowLength * pageIndex - rowLength);//从多少页开始显示
    }
    
    var PBID = getQueryStringByName("PBID");
    var ajaxUrl = getRootPath() + "/PostTheme/BindPostTheme";
    var bindHtml = "";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: { length: rowLength, start: rowStart, PBID: PBID },
        success: function (data) {
            if (data.data.length !=null) {
                for (var i = 0; i < data.data.length; i++) {
                    bindHtml += '<div class="panel panel-primary">'
                                                 + ' <div class="panel-heading">'
                                                    + ' <a href="/PostReply/Index?PTID=' + data.data[i].PTID + '"><h3 class="panel-title"><span class="glyphicon glyphicon-star">  ' + data.data[i].PostThemeContent + '</span></h3></a> '

                                                  + ' </div>'
                                                  //+ ' <div class="panel-body">'
                                                  //  + '  ' + data.data[i].PostContent + ''
                                                  //+ ' </div>'
                                                 + '  <div class="panel-footer">'
                                                      + ' <span class="pull-left">回帖数量：' + data.data[i].PRCount + '</span>'
                                                      + ' <span class="pull-right">发帖人：' + data.data[i].PostManName + '&nbsp;&nbsp;&nbsp;&nbsp;发帖时间：' + getDateDiff(getDateTimeStamp(data.data[i].PostDate)) + '</span>'
                                                 + '  </div>'
                                              + ' </div>';
                }
                $("#txt_PageCount").val(data.recordsTotal);
                BindPager(rowLength, data.recordsTotal, Number(pageIndex));
               
            }

            $("#datatable_postTheme").html(bindHtml);
        }
          
    });
    onresize();
}

//翻页
$("#pager").delegate("li", "click", function () {
    var pageIndex = $(this).attr("page_number");
    var countPage = Math.floor(Number($("#txt_PageCount").val()) / rowLength);
    if (Number($("#txt_PageCount").val()) % rowLength != 0) {
        countPage += 1;
    }
    if (pageIndex > 0 && pageIndex <= countPage)
    {
        BindPostTheme(pageIndex);
    }
});


//贴吧是否关注方法
function IsAttention(type)
{
    var PBID = getQueryStringByName("PBID");
    var PostBarName = $("#PostBarName").html();
    if(type=="Yes")//关注贴吧
    {
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostTheme/Attention?PBID=" + PBID + "",
            success: function (data) {
                if (data == 1)
                {
                    noty({
                        text: "" + PostBarName + "关注成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    $("#IsAttention").html("<button class=\"btn btn-primary btn-sm\" onclick=\"IsAttention('No')\"><span class=\"fa fa-check-square\"></span>取消关注</button>");

                }
            }
        })
    
    } else if (type == "No")//取消关注贴吧
    {
        $.ajax({
            type: "POST",
            url: getRootPath() + "/PostTheme/CancelAttention?PBID=" + PBID + "",
            success: function (data) {
                if (data == 1) {
                    noty({
                        text: "" + PostBarName + "取消关注成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    $("#IsAttention").html("<button class=\"btn btn-primary btn-sm\" onclick=\"IsAttention('Yes')\"><span class=\"fa fa-check-square-o\"></span>关注</button>"); 
                }
            }
        })

    }

}

//发表帖子
function PublishPost()
{
    $.ajax({
        type: "POST",
        url: getRootPath() + "/PostTheme/PostThemeEdit",
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        }, complete: function ()
        {
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

        }
    });

}

//添加主题
function PostThemeAdd() {
    var PBID = getQueryStringByName("PBID");
    var PostThemeContent = $("#txt_PostThemeContent").val();
    var PostContent = encodeURI($('#summernote').code().toString());
    if (PostThemeContent == "" )
    {
        noty({
            text: "主题不能为空！",
            layout: 'topRight',
            type: 'error',
        });
    } else if (PostContent == "") {
        noty({
            text: "主题不能为空！",
            layout: 'topRight',
            type: 'error',
        });
    } else
    {
        $.ajax({
            type: "POST",
            data: { PBID: PBID, PostThemeContent: PostThemeContent, PostContent: PostContent },
            url: getRootPath() + "/PostTheme/PostThemeAdd",
            success: function (data) {
                if (data > 0) {
                    noty({
                        text: "添加成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                }
            },
            complete: function () {
                $('#summernote').code("");
                $('#edit-manager').modal('hide');
                $("#pop_modaldialog").empty();
                BindPostTheme(1);
            }
        });

    }

}
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


