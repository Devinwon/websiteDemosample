
$(function () {
    BindPostBar();
});

//绑定热门贴吧
function BindPostBar() {
    var PBCID = getQueryStringByName("PBCID");
    var ajaxUrl = getRootPath() + "/PostBarInfo/BindPostBar?PBCID=" + PBCID + "";
    var bindHtml = "";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        success: function (data) {
            if (data.length != 0) {
                for (var i = 0; i < data.length; i++) {
                    bindHtml += '<a class="gallery-item" href="/PostTheme/Index?PBID=' + data[i].PBID + '" title="" data-gallery>'
                                                       + '<div class="image">'
                                                            + '<img src="' + data[i].PBHeadPortrait + '" alt="发帖人数" />'
                                                            + '<ul class="gallery-item-controls">'
                                                                + '<li><label><span class="fa fa-male"></span>' + data[i].Attention + '</label></li>'
                                                               + ' <li><label><span class="fa fa-pencil"></span>' + data[i].PostNumber + '</label></li>'
                                                           + ' </ul>'
                                                       + ' </div>'
                                                        + '<div class="meta">'
                                                           + ' <strong>' + data[i].PBName + '</strong>'
                                                            + '<span>大家都讨论：' + data[i].DiscussTopic + '</span>'
                                                        + '</div>'
                                                    + '</a>'

                }
            } else {

                bindHtml = '<div class="gridview_empty text-center">没有找到符合条件的数据</div>';
            }
            $("#links").html(bindHtml);
        }

    });


    onresize();
}