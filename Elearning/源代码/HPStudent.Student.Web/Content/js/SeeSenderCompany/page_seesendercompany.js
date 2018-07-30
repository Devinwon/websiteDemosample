var myDatatalbe = null;
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
    if (JobObj.CompanyName != null) {
        $("#TxtCompanyName").val(JobObj.CompanyName);
    }
    BindDatatable('/SeeSenderCompany/GetSeeSenderCompanyTable', JobObj);

})

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
                { "data": "IsRead" }
                

            ]//,
            //"columnDefs": [{
            //    "targets": [8], "render": function (data, type, row) {
            //        return "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "'  onclick='ViewDetailsPage(" + row['JId'] + ")'><span class='fa fa-search'></span>查看</button> </td>";
            //    }
            //}]
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
    //if ((typeof (TxtJobName) != "undefined" && TxtJobName != null && TxtJobName != "") ||
    //    (typeof (TxtCity) != "undefined" && TxtCity != null && TxtCity != "") ||
    //    selMoney != "== 薪资范围 ==") {
    var JobObj = Object.create(jobObj);
    JobObj.name = TxtJobName;
    JobObj.city = TxtCity;
    JobObj.salaryrange = selMoney;
    JobObj.CompanyName = TxtCompanyName;
    BindDatatable('/SeeSenderCompany/GetSeeSenderCompanyTable', JobObj);
    //} else {
    //    noty({
    //        text: "搜索关键词为空!",
    //        layout: 'topRight',
    //        type: 'error',
    //    });
    //}
}


function ViewDetailsPage(val)
{
    function ViewDetailsPage(jid) {
        var ajaxUrl = getRootPath() + "/Job/QueryJobDetails?jid=" + jid;
        $.ajax({
            type: "POST",
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

}


