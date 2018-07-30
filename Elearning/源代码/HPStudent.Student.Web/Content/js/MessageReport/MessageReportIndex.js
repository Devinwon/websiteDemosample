var myDatatalbe;
var messageReportObj = {
};
$(function () {
    var MessageReportObj = Object.create(messageReportObj);
    BindGetMessageReportDatatable('/MessageReport/GetMessageReportData', MessageReportObj);
});
//绑定收件箱Datatable
function BindGetMessageReportDatatable(ajaxUrl, dataJson) {
    //清空Datatable
    $('#QA_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_question").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl,
                "data": dataJson
            },
            "serverMethod": 'POST',
            "columns": [
	            { "data": "Reporter" },
                { "data": "BeReporter" },
                { "data": "IsDo" },
                { "data": "ReportDate" },
                { "data": "Category" }
            ]
            ,
            "columnDefs": [
                {
                    "targets": 2, "render": function (data, type, row) {
                        var retsult = "";
                        if (data == 0) {
                            retsult = "未处理";
                        }
                        else if (data == 1) {
                            retsult = "处理中";
                        }
                        else if (data == 2) {
                            retsult = "已完结";
                        }
                        return retsult
                    }
                },
                {
                    "targets": 3, "render": function (data, type, row) {
                        var javascriptDate = new Date(new Date(parseInt(data.substr(6))));
                        javascriptDate = javascriptDate.getFullYear() + "-" + (javascriptDate.getMonth() + 1) + "-" + javascriptDate.getDate();
                        return javascriptDate;
                    }
                },
                {
                    "targets": 4, "render": function (data, type, row) {
                        var retsult = "";
                        if (data == 0) {
                            retsult = "私信";
                        }
                        else if (data == 1) {
                            retsult = "论坛帖子";
                        }
                        else if (data == 2) {
                            retsult = "题库报错";
                        }
                        return retsult
                    }
                }
                ,
                {
                    "targets": [5], "render": function (data, type, row) {
                        return "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "' ><span class='fa fa-search'></span>查看</button> </td>";
                    }
                }
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": dataJson
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}