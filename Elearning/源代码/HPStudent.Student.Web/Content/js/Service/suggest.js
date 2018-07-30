var myDatatable;
var GetSuggestListURL = getRootPath() + "/Service/GetSuggestList";
var SuggestItem = {
    IsSuggest: 0,
    Title: "",
    Content: "",
};

$(function () {
    //绑定Datatable
    BindDatatable(GetSuggestListURL);

    $("#btnAddSuggest").click(function () {
        //window.location.href = "/Service/AddSuggest";

        //3. AJAX动态调用后台接口生成练习题
        var ajaxUrl = "/Service/Pop_AddSuggest?" + Date.now();

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html, data) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#Pop_AddSuggest').modal('show');
            },
            complete: function (html, data) {
                //加载完成
                //修复icheck无法显示样式的bug
                $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
            }
        });
    });
});

//绑定Datatable
function BindDatatable(ajaxUrl) {
    if (myDatatable == null) {
        myDatatable = $(".datatable_suggest").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "IsSuggest" },
	            { "data": "Title" },
	            { "data": "CurrentStatus" },
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml = "";
                        if (row['IsSuggest'] == 0) {
                            outHtml = "建议";
                        } else {
                            outHtml = "投诉";
                        }
                        return outHtml;
                    }
                },
                {
                    "targets": 1,
                    "render": function (data, type, row) {
                        var outHtml = '<a href="/Service/SuggestDetail?SID=' + row['SID'] + '">' + row['Title'] + '</a> '
                        return outHtml;
                    }
                },
                {
                    "targets": 2,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var iStatus = row['Status'];
                        var outHtml = "";
                        var outIcon = "";
                        if (row['LastReply'] == 1) {
                            outIcon = '<i class="fa fa-comment"></i>';
                        }
                        if (iStatus == "1") {
                            outHtml = '<span class="label label-info">处理中 ' + outIcon + '</span> ';
                        } else if (iStatus == "2") {
                            outHtml += '<span class="label label-success">已处理</span> ';
                            outHtml += "<a href='javascript:void(0)' onclick='openStarScore(" + row["SID"] + ")'><span class='label label-success'>投票</span></a>";
                        } else {
                            outHtml = '<span class="label label-default">未处理 ' + outIcon + '</span> ';
                        }
                        return outHtml;
                    }
                }

            ]
        });
    } else {
        var oSettings = myDatatable.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatable.fnClearTable(0);
        myDatatable.fnDraw();
    }
}

//添加建议按钮被点击时触发
//function AddSuggest() {
//    //var suggestitem = Object.create(SuggestItem);
//    //suggestitem.Title = '';
//    //suggestitem.Conent = '';
//    //suggestitem.IsSuggest = 0;

//    alert(1);
//    //alert($("input[name='isSuggest']").val());
//    //alert($('input[name=Title]').val());
//    //alert($('input[name=Conent]').val());
//}


function AddSuggest() {
    var isSuggest = 0;
    var suggestitem = Object.create(SuggestItem);
    if ($("input[name='isSuggest']:checked").val() == "suggest") {
        isSuggest = 0;
    } else if ($("input[name='isSuggest']:checked").val() == "complaint") {
        isSuggest = 1;
    }
    suggestitem.Title = $('input[name=Title]').val();
    suggestitem.Content = $('textarea[name=Content]').val();
    suggestitem.IsSuggest = isSuggest;

    $.ajax({
        type: "post",
        url: "/Service/AddSuggest",
        dataType: "json",
        data: suggestitem,
        success: function (data) {
            if (data.ResultState == 0) {
                $('#Pop_AddSuggest').modal('hide');
                $("#pop_modaldialog").empty();

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

            BindDatatable(GetSuggestListURL);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}
//打开评分页面
function openStarScore(sid) {
    var ajaxUrl = getRootPath() + "/Service/SuggestScore?SID=" + sid;
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html, data) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#ScoreStarModal').modal('show');
        },
        complete: function (html, data) {
            $('#divStar').raty({
                cancel: true,
                score: function () {
                    return $(this).attr('data-score');
                }
            });
        }
    });
}
//评分保存
function SaveScoreStar(SID)
{
    var Suggest = {
        ScoreDetail: "",
        ScoreStar: 0,
        SID:0,
    };
    var Suggestobj = Object.create(Suggest);
    Suggestobj.ScoreDetail = $("#txtScoreDetail").val();
    Suggestobj.ScoreStar = $('#divStar').raty('score');
    Suggestobj.SID = SID;
    ajaxUrl = getRootPath() + "/Service/SuggestScoreSave";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: Suggestobj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#ScoreStarModal').modal('hide');
                    $("#pop_modaldialog").empty();
                    location.reload();
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

