var SuggestItem = {
    SID: 0,
    Content: "",

};

$(function () {
    //通过状态控制按钮显示
    var state = $("#EventState").val();
    if (state == 2) {
        $("#btnEndEvent").hide();
        $("#ReplySuggestDIV").hide();
    }
})

function ReplySuggest() {
    var suggestitem = Object.create(SuggestItem);
    suggestitem.Content = $("input[name=tbSay]").val();
    suggestitem.SID = GetQueryString("SID");
    $.ajax({
        type: "post",
        url: "/Service/ReplySuggest",
        dataType: "json",
        data: suggestitem,
        success: function (data) {
            if (data.ResultState == 0) {
                //添加回复的内容
                var htmlTemp = '<div class="item item-visible">'
                       + '                          <div class="image">'
                       + '                               <img src="/Content/img/users/child_male.png" alt="我">'
                       + '                           </div>'
                       + '                           <div class="text ">'
                       + '                               <div class="heading">'
                       + '                                   <a href="javascript:void()">我</a>'
                       + '                                    <span class="date">刚刚</span>'
                       + '                                </div>'
                       + suggestitem.Content
                       + '                           </div>'
                       + '                        </div>';
                $(".messages").append(htmlTemp);
                //弹出成功回复 的提示
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });

            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });

            }
        },
        complete: function (data) {


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}