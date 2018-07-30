﻿var iCount; //定时器
var myDatatalbe;
var ResgisterObj = {
    jid: 0,
    name: '',
    city: '',
    salaryrange: '',
    CompanyName: '',
    IsActivated: 0
};
$(function () {
    BindDatatable('/RegisterManager/QuerySimilarEnterpriseReviewJson', Object.create(ResgisterObj));
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
                "url": ajaxUrl
            },
            "serverMethod": 'POST',
            "columns": [
	            { "data": "CompanyName" },
	            { "data": "Email" },
                { "data": "RealName" },
                { "data": "Phone" }
            ],
            "columnDefs": [{
                "targets": [4], "render": function (data, type, row) {
                    if ($('#Check_IsActivated').is(':checked')) {

                        return "<td><button class='btn btn-success btn-sm disabled' onclick='DataPass(" + row['SID'] + ")' data-id='" + row['SID'] + "' data-action='Pass'>通过</button></td>";
                    } else
                    {
                        return "<td><button class='btn btn-success btn-sm' onclick='DataPass(" + row['SID'] + ")' data-id='" + row['SID'] + "' data-action='Pass'>通过</button></td>";
                    }
                   
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
//审核按钮事件
function DataPass(sid) {
    noty({
        text: '是否确认通过?',
        layout: 'topRight',
        buttons: [{
            addClass: 'btn btn-success btn-clean', text: '确认', onClick: function ($noty) {
                $noty.close();
                $.ajax({
                    url: '/RegisterManager/CheckPass',
                    data: { SID: sid },
                    type: 'POST',
                    success: function (data) {
                        if (data.ResultState == 0) {
                            noty({
                                text: data.ResultMsg,
                                layout: 'topRight',
                                type: 'success',
                            });
                            var actioinFun = window.setTimeout(function () {
                                window.location.reload();
                            }, 500);
                        } else {
                            noty({
                                text: data.ResultMsg,
                                layout: 'topRight',
                                type: 'error',
                            });
                        }
                    },
                    error: function (xhr, responseText) {
                        console.log(xhr);
                    }
                });
            }
        },
                {
                    addClass: 'btn btn-danger btn-clean', text: '取消', onClick: function ($noty) {
                        $noty.close();
                    }
                }]
    })
}
//条件查询按钮
function SearchEnterpriseInfo() {
    var TxtCompanyName = jQuery.trim($('#TxtCompanyName').val());
    var ResgisterInfo = Object.create(ResgisterObj);
    if ($('#Check_IsActivated').is(':checked')) {
        ResgisterInfo.IsActivated = 2;
    }   
    ResgisterInfo.CompanyName = TxtCompanyName;
    BindDatatable('/RegisterManager/QuerySimilarEnterpriseReviewJson', ResgisterInfo);
}