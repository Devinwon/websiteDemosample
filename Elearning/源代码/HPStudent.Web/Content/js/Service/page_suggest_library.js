var myDatatalbe;
$(function () {
    //1. AJAX动态调用后台接口生成练习题
   var ajaxUrl = "/Service/GetSuggestList";

    //2.Datatable绑定数据
    BindDatatable(ajaxUrl);

    $("#Suggest-list").delegate(".btn-primary", "click", function () {
        if ($(this).attr("data-action") == "do") {
            var actionID = $(this).attr("data-id");
            window.open('/Service/SuggestDetail?SID='+actionID);
        }
    });
});

//绑定Datatable
function BindDatatable(ajaxUrl) {

    //清空Datatable
    $('#Data_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_suggest").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "IsSuggest" },
	            { "data": "SchoolName" },
	            { "data": "ClassName" },
	            { "data": "StudentName" },
	            { "data": "PostDate" },
	            { "data": "Status" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml;
                        if (row['IsSuggest'] == "0") {
                            outHtml = "建议";
                        } else {
                            outHtml = "投诉";
                        }                       
                        return outHtml;
                    }
                },
                  {
                      "targets": 1,
                      "class": "text-center",
                  },
                {
                    "targets": 2,
                    "class": "text-center",
                },
                {
                    "targets": 3,
                    "class": "text-center",
                },
                {
                    "targets": 4,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['PostDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
                {
                    "targets": 5,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var outHtml;
                        var outIcon = "";
                        if (row['LastReply'] == 0)
                        {
                            outIcon = '<i class="fa fa-comment"></i>';
                        }
                        if (row['Status'] == 0) {
                            //未处理
                            outHtml = '<span class="label label-warning">未处理 ' + outIcon + '</span>';
                        } else if (row['Status'] == 1) {
                            //处理中
                            outHtml = '<span class="label label-info">处理中 ' + outIcon + '</span>';
                        } else if (row['Status'] == 2) {
                            //已关闭
                            outHtml = '<span class="label label-default">已关闭 ' + outIcon + '</span>';
                        }
                        return outHtml;
                    }
                },
                {
                    "targets": 6,
                    "render": function (data, type, row) {
                        var outHtml = '<button class="btn btn-primary btn-sm" data-id="' + row['SID'] + '" data-action="do"><span class="fa fa-pencil"></span>处理</button> ';
                        return outHtml;
                    }
                }
            ]

        });

        onresize();
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();
    }
}