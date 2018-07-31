var myDatatalbe;
var messagesObj = {
};
$(function () {
    //设定活动背景标签
    $("#SendMessageIndexA").addClass("active");
    var MessagesObj = Object.create(messagesObj);
    BindGetMessageDatatable('/Message/SendMessageData', MessagesObj);
});
//绑定收件箱Datatable
function BindGetMessageDatatable(ajaxUrl, dataJson) {
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
	            { "data": "Receiver" },
	            { "data": "Title" },
                { "data": "DateCreated" }
            ]
            ,
            "columnDefs": [
                 {
                     "targets": 2, "render": function (data, type, row) {

                         var javascriptDate = new Date(new Date(parseInt(data.substr(6))));
                         javascriptDate = javascriptDate.getFullYear() + "-" + (javascriptDate.getMonth() + 1) + "-" + javascriptDate.getDate();
                         return javascriptDate;
                     }
                 }]
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