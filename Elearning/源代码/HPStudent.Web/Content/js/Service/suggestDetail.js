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
                var htmlTemp = '<div class="item item-visible in">'
                       + '                          <div class="image">'
                       + '                               <img src="/Content/img/users/support_female.png" alt="工作人员">'
                       + '                           </div>'
                       + '                           <div class="text ">'
                       + '                               <div class="heading">'
                       + '                                   <a href="javascript:void()">工作人员</a>'
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
//事件完结事件按钮
function EndEvent(PID) {
    noty({
        text: '确定完结吗?',
        layout: 'topRight',
        buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: '确定', onClick: function ($noty) {
                        $noty.close();
                        var suggestitem = Object.create(SuggestItem);
                        suggestitem.SID = PID;
                        var ajaxUrl = getRootPath() + "/Service/SuggestEndEvent";
                        $.ajax({
                            type: "post",
                            url: ajaxUrl,
                            dataType: "json",
                            data: suggestitem,
                            success: function (data) {
                                if (data.ResultState == 0) {
                                    noty({
                                        text: data.ResultMsg,
                                        layout: 'topRight',
                                        type: 'success',
                                    });
                                    var actioinFun = window.setTimeout(function () {
                                        //刷新页面
                                        window.location.reload();
                                    }, 500);
                                } else {
                                    noty({
                                        text: data.ResultMsg,
                                        layout: 'topRight',
                                        type: 'fail',
                                    });
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(errorThrown);
                            }
                        });
                    }
                },
                {
                    addClass: 'btn btn-danger btn-clean', text: '取消', onClick: function ($noty) {
                        $noty.close();
                    }
                }
        ]
    })
}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}