var iCount; //定时器
var myDatatalbe;
var ResgisterObj = {
    RoleId: 0,
    Email: '',
    Password: '',
    CompanyName: '',
    Phone: '',
    RealName: '',
    RegisterType: '',
    IsActivated :0
};
$(function () {
    BindDatatable('/RegisterManager/QuerySimilarStudentReviewJson', Object.create(ResgisterObj));
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
                { "data": "RealName" },
	            { "data": "Email" },
                { "data": "Phone" }
            ],
            "columnDefs": [{
                "targets": [3], "render": function (data, type, row) {
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
        buttons: [
                {
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
                                        //重新载入
                                        SearchStudentInfo();
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
                }
        ]
    })
}
//条件查询按钮
function SearchStudentInfo() {
    var TxtRealnameName = jQuery.trim($('#TxtRealnameName').val());
    var ResgisterInfo = Object.create(ResgisterObj);
    ResgisterInfo.RealName = TxtRealnameName;
    if ($('#Check_IsActivated').is(':checked')) {
        ResgisterInfo.IsActivated = 2;
    }
  
    BindDatatable('/RegisterManager/QuerySimilarStudentReviewJson', ResgisterInfo);
}



$("#selectAll").on('ifUnchecked', function (event) {        //如果不选中，点击后则为选中
    
});