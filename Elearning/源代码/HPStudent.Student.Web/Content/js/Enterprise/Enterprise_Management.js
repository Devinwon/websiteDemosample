var iCount; //定时器
var myDatatalbe;
var CompanyInfoModelObj = {
    CompanyName: '',
    SearchType:0
};
$(function () {
    var Obj = Object.create(CompanyInfoModelObj);
    BindDatatable('/Enterprise/QueryInitialData', Obj);
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
	            { "data": "EID" },
	            { "data": "EnterpriseName" },
                { "data": "Agreement" }
            ],
            "columnDefs": [{
                "targets": [2], "render": function (data, type, row) {
                    if (data != "" && data != null) {
                        return "<span class='label label-info'><span class='fa fa-check'></span>";
                    }
                    else {
                        return "<span class='label label-danger'><span class='fa fa-times'></span>";
                    }
                }
            },
                {
                    "targets": [3], "render": function (data, type, row) {
                        var btnStr = "<button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['EID'] + "'  onclick='ViewSearchPage(" + row['EID'] + ")'><span class='fa fa-search'></span>查看</button> ";
                        btnStr += "<button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['EID'] + "'  onclick='pop_EditCompany(" + row['EID'] + ")'>编辑企业</button> ";
                        btnStr += "<button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['EID'] + "'  onclick='Pop_JobShow(" + row['EID'] + ")'>编辑职位</button> ";
                        btnStr += "<button data-action='show' class='btn btn-primary btn-sm' data-id='" + row['EID'] + "'  onclick='AgreementUpload(" + row['EID'] + ")'>上传就业协议</button>";
                        return "<td>" + btnStr + " </td>";
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
//企业导入按钮
function PreviewEnterpriseUpload(ID) {
    var ajaxUrl = "/Enterprise/UpLoadEnterpriseInfo?SID=" + ID;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewUploadpanel').modal('show');
        },
        complete: function (html) {
            //0.初始化fileinput
            var oFileInput = new FileInput();
            oFileInput.Init("txt_file", "/Enterprise/UpLoadInfo");
        }
    });
}
//就业协议导入按钮
function AgreementUpload(ID) {
    var ajaxUrl = "/Enterprise/UploadEnterpriseAgreement";
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewAgreementUploadpanel').modal('show');
        },
        complete: function (html) {
            //0.初始化FileInputAgreement
            var oFileInput = new FileInputAgreement();
            oFileInput.Init("txt_file", "/Enterprise/UploadAgreement?SID=" + ID);
        }
    });
}
//企业查询按钮
function SearchEnterpriseInfo() {
    var Obj = Object.create(CompanyInfoModelObj);
    Obj.CompanyName = jQuery.trim($('#keyWord').val());
    Obj.SearchType = $("#selSearchType").val();
    BindDatatable('/Enterprise/QuerySimilarEnterpriseJson', Obj);
}
function ViewSearchPage(eid) {
    var ajaxUrl = getRootPath() + "/Enterprise/QueryEnterprisePage?EID=" + eid;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewEnterprisePanel').modal('show');
        },
        complete: function (html) {
            //显示职位详情
            $("#AJobInfo").click(function () {
                var cominfo = $(this).attr("title");
                window.location.href = ' ../Job/Index?company=' + escape(cominfo);
            })
        }
    });
}
//弹出新增企业窗口
function pop_EditCompany(SID) {
    var ajaxUrl = getRootPath() + "/Enterprise/Pop_EditCompany?SID=" + SID;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-Company').modal('show');
        },
        complete: function (html) {
            feSelect();
        }
    });
}
//修复下拉框
var feSelect = function () {
    if ($(".select").length > 0) {
        $(".select").selectpicker();
        $(".select").on("change", function () {
            if ($(this).val() == "" || null === $(this).val()) {
                if (!$(this).attr("multiple"))
                    $(this).val("").find("option").removeAttr("selected").prop("selected", false);
            } else {
                $(this).find("option[value=" + $(this).val() + "]").attr("selected", true);
            }
        });
    }
}
//弹出职位编辑页
function Pop_JobShow(SID) {
    var ajaxUrl = getRootPath() + "/Enterprise/Pop_JobShow";
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_Searchmodaldialog").empty();
            $('#pop_Searchmodaldialog').append(html);
            $('#JobListModal').modal('show');
        },
        complete: function () {
            var jobSearchObj = {
                Sid: 0
            };
            var JobSearchModel = Object.create(jobSearchObj);
            JobSearchModel.Sid = SID;
            BindJobDatatable('/Job/QuerySimilarJobJson', JobSearchModel)
            $("#btnAddMenu").attr("onclick", "EidtJobPage(0," + SID + ")");
        }
    });
}

