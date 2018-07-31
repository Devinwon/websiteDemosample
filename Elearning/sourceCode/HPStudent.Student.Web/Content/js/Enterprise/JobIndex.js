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
    BindDatatable('/Enterprise/QuerySimilarJobJson', JobObj);
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
                    var button = "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "'  onclick='ViewDetailsPage(" + row['JId'] + ")'><span class='fa fa-search'></span>查看</button> </td> ";
                    button += "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "'  onclick='EidtPage(" + row['JId'] + ")'><span class='fa fa-pencil'></span>编辑</button> </td>";
                    return button;
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
    var JobObj = Object.create(jobObj);
    JobObj.name = TxtJobName;
    JobObj.city = TxtCity;
    JobObj.salaryrange = selMoney;
    BindDatatable('/Enterprise/QuerySimilarJobJson', JobObj);
}
//显示职位详情页面
function ViewDetailsPage(jid) {
    var ajaxUrl = getRootPath() + "/Enterprise/QueryJobDetails?jid=" + jid;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewJobPanel').modal('show');
        },
        complete: function (html) {
        }
    });
}
//获取地址栏的值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + escape(name) + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//打开编辑界面
function EidtPage(ID) {
    var ajaxUrl = getRootPath() + "/Enterprise/JobEdit?JID=" + ID;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#EditJobModal').modal('show');
        },
        complete: function (html) {
            //加载页面已存在数据并绑定
            var SalaryRange = $("#hiddenSalaryRange").val();
            var WorkType = $("#hiddenWorkType").val();
            var DegreeRequired = $("#hiddenDegreeRequired").val();
            var ExperienceRequired = $("#hiddenExperienceRequired").val();
            if (SalaryRange != null && SalaryRange != '') {
                $("#selMoneyEdit").val(SalaryRange);
            }
            if (WorkType != null && WorkType != '') {
                $("#selWorkTypeEdit").val(WorkType);
            }
            if (DegreeRequired != null && DegreeRequired != '') {
                $("#selDegreeRequiredEdit").val(DegreeRequired);
            }
            if (ExperienceRequired != null && ExperienceRequired != '') {
                $("#selExperienceRequiredEdit").val(ExperienceRequired);
            }
            //城市选择
            $('#selProvinceEdit').change(function () {
                $('#selCityEdit').empty();
                var ajaxUrl = "/Enterprise/GetCityListByParentAID";
                var ParentAID = $('#selProvinceEdit').val();
                $.ajax({
                    type: "post",
                    url: ajaxUrl,
                    dataType: "json",
                    data: "ParentAID=" + ParentAID,
                    success: function (data) {
                        //填充城市下拉框
                        $.each(data, function (i, item) {
                            $('#selCityEdit').append("<option value='" + item.AreaName + "'>" + item.AreaName + "</option>");
                        });
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        noty({
                            text: errorThrown,
                            layout: 'topRight',
                            type: 'error',
                        });
                    }
                });
            });
            //如果已经绑定城市就直接选中
            var hiddenIsCheckProvince = $("#hiddenIsCheckProvince").val();
            if (hiddenIsCheckProvince != "" && hiddenIsCheckProvince != 0) {
                $('#selProvinceEdit').val(hiddenIsCheckProvince);
                var ajaxUrl = "/Enterprise/GetCityListByParentAID";
                $.ajax({
                    type: "post",
                    url: ajaxUrl,
                    dataType: "json",
                    data: "ParentAID=" + hiddenIsCheckProvince,
                    success: function (data) {
                        //填充城市下拉框
                        $.each(data, function (i, item) {
                            $('#selCityEdit').append("<option value='" + item.AreaName + "'>" + item.AreaName + "</option>");
                        });
                        $('#selCityEdit').val($("#hiddenCity").val());
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        noty({
                            text: errorThrown,
                            layout: 'topRight',
                            type: 'error',
                        });
                    }
                });
            }
        }
    });
}


