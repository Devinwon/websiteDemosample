var JobDatatalbe;
//绑定Datatable
function BindJobDatatable(ajaxUrl, dataJson) {
    //清空Datatable
    $('#QA_SelectJobList').empty();
    if (JobDatatalbe == null) {
        JobDatatalbe = $(".jobdatatable_question").dataTable({
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
	            { "data": "Name" },
                { "data": "City" },
                { "data": "Salaryrange" },
                { "data": "Worktype" },
                { "data": "Degreerequired" },
                { "data": "Experiencerequired" }
            ],
            "columnDefs": [{
                "targets": [6], "render": function (data, type, row) {
                    return "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['JId'] + "'  onclick='EidtJobPage(" + row['JId'] + "," + row['SId'] + ")'><span class='fa fa-pencil'></span>编辑</button> </td>";
                }
            }]
        });
    } else {
        var oSettings = JobDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": dataJson
        };
        JobDatatalbe.fnClearTable(0);
        JobDatatalbe.fnDraw();
    }
}
//打开职位编辑界面
function EidtJobPage(JID, SID) {
    var ajaxUrl = getRootPath() + "/Enterprise/Pop_JobEdit?JID=" + JID;
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
            $("#hiddenSId").val(SID);
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
//关闭模态窗销毁DT
function closejobPop() {
    JobDatatalbe = null;
}