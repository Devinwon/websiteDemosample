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
                        var iStatus = row['Status'] ;
                        var outHtml = "";
                        var outIcon = "";
                        if (row['LastReply'] == 1) {
                            outIcon = '<i class="fa fa-comment"></i>';
                        }
                        if(iStatus=="1")
                        {
                            outHtml = '<span class="label label-info">处理中 ' + outIcon + '</span> ';
                        } else if (iStatus == "2") {
                            outHtml = '<span class="label label-success">已处理 ' + outIcon + '</span> ';
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

