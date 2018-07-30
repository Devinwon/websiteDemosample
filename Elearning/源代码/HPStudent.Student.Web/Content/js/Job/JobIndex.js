var iCount; //定时器
var myDatatalbe;
var jobObj = {
    jid: 0,
    name: '',
    city: '',
    salaryrange: '',
    CompanyName: ''
};
$(function () {
    var JobObj = Object.create(jobObj);
    //获取地址栏传送公司名称
    JobObj.CompanyName = getQueryString("company");
    if (JobObj.CompanyName != null) {
        $("#TxtCompanyName").val(JobObj.CompanyName);
    }
    BindDatatable('/Job/QuerySimilarJobJson', JobObj);
});
//绑定Datatable
function BindDatatable(ajaxUrl, dataJson) {
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
	            { "data": "CompanyName" },
	            { "data": "Name" },
                { "data": "City" },
                { "data": "Salaryrange" },
                { "data": "Worktype" },
                { "data": "Degreerequired" },
                { "data": "Experiencerequired" }
            ],
            "columnDefs": [{
                "targets": [7], "render": function (data, type, row) {
                    return "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "'  onclick='ViewDetailsPage(" + row['JId'] + ")'><span class='fa fa-search'></span>查看</button> </td>";
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
//查询点击事件
function SearchEnterpriseInfo() {
    var TxtJobName = jQuery.trim($('#TxtJobName').val());
    var TxtCity = jQuery.trim($('#TxtCity').val());
    var selMoney = $('#selMoney').val();
    var TxtCompanyName = $("#TxtCompanyName").val();
    var JobObj = Object.create(jobObj);
    JobObj.name = TxtJobName;
    JobObj.city = TxtCity;
    JobObj.salaryrange = selMoney;
    JobObj.CompanyName = TxtCompanyName;
    BindDatatable('/Job/QuerySimilarJobJson', JobObj);
}
//显示职位详情页面
function ViewDetailsPage(jid) {
    var ajaxUrl = getRootPath() + "/Job/QueryJobDetails?jid=" + jid;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewJobPanel').modal('show');
        },
        complete: function (html) {
            JobSend();
        }
    });
}
//获取地址栏的值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + escape(name) + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//职位申请
function JobSend() {
    var jobSendObj = {
        SId: 0,
        JId: 0,
        SenderID: 0
    };
    $("#JobSend").click(function () {
        var strs = new Array();
        strs = $(this).attr("data-id").split(',');
        var JobSendObj = Object.create(jobSendObj);
        JobSendObj.SId = strs[0];
        JobSendObj.JId = strs[1];
        //执行异步添加
        $.ajax({
            type: "post",
            url: "/Job/SendResume",
            dataType: "json",
            data: JobSendObj,
            success: function (data) {
                if (data.ResultState == 0) {
                    $('#previewJobPanel').modal('hide');
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
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    })
}


