var myDatatalbe;
$(function () {

    //单据上传按钮被点击时提交
    $('#btnAddFee').on('click', function (e) {
        var ajaxUrl = "Pop_AddFee";
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-fee').modal('show');
            }
        });
    });

    ajaxUrl = "/Fee/GetFeeListBySID?Year=" + '';
    //Datatable绑定数据
    BindDatatable(ajaxUrl);

    //费用列表的查看项添加事件
    $("#fee-list").delegate(".btn-primary", "click", function () {

        if ($(this).attr("data-action") == "show") {
            //查询按钮
            var ajaxUrl = "Pop_ShowFee?FeeID=" + $(this).attr("data-id");
            $.ajax({
                type: "GET",
                url: ajaxUrl,
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#show-fee').modal('show');
                }
            });

        }
    });

    //选择了学年后触发 
    $('#btnSelYear').on('click', function () {
        var Year = $('#selYear').val();
        if (Year == "0") {
            Year = "";
        }


        ajaxUrl = "/Fee/GetFeeListBySID?Year=" + Year;

        //Datatable绑定数据
        BindDatatable(ajaxUrl);

        return false;
    });

});

//关闭弹出窗口
function CloseFee() {
    $("#pop_modaldialog").empty();
}

function AddFee() {
    //1.Form校验
    var Year = $("#pop_selYear").val();

    if (Year == "0") {
        noty({
            text: "请选择上传缴费学年！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }



    if ($("#pop_selTitle").val() == "0") {
        noty({
            text: "请选择缴费科目！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }
    var FeeTitle = $('#pop_selTitle').find("option:selected").text()
    var Fee = $("input[name='tbFee']").val();
    if (isNaN(Fee)) {
        noty({
            text: "费用金额请填写数字！",
            layout: 'topRight',
            type: 'warning',
        });
        return false;
    }

    var file = $('#uploadFile').val();
    //检查是否已选择上传文件
    if (file != '') {
        var filename = file.replace(/.*(\/|\\)/, '');
        var fileext = (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename.toLowerCase()) : '';
        //检查文件格式
        if (fileext == 'jpg' || fileext == 'jpeg') {
            //展示等待信息
            $('#loading').ajaxStart(function () {
                $(this).show();
            }).ajaxComplete(function () {
                $(this).hide();
            });

            var flag = false;
            //2.ajax调用上传接口
            var ajaxUploadFile = "/Fee/Upload?FeeTitle=" + FeeTitle + "&Year=" + Year + "&Fee=" + Fee;
            $.ajaxFileUpload({
                url: ajaxUploadFile,
                secureuri: false,
                dataType: "json",
                fileElementId: 'uploadFile',
                success: function (data, status)  //服务器成功响应处理函数
                {
                    flag = true;
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert(e);
                }
            });
        }
    }



    //禁用上传按钮
    $('#btnSubmit').attr("disabled", "disabled");
    //修改删除按钮的描述为上传中
    $('#btnSubmit').text("上传中...");
    //2.1成功弹出提示
    if (flag == true) {
        noty({
            text: "费用凭据上传成功！",
            layout: 'topRight',
            type: 'success',
        });
    }

    BindDatatable(ajaxUrl)
    //2.2关闭弹出窗口
    $("#pop_modaldialog").empty();
}


//绑定Datatable
function BindDatatable(ajaxUrl) {
    //  清空Datatable
    $('#QA_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_fee").dataTable({
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
                { "data": "Year" },
                { "data": "Dateline" },
                { "data": "FeeTitle" },
                { "data": "Fee" },
                { "data": "IsCheck" },
                { "data": "FeeID" },
            ],
            "columnDefs": [
                  {
                      "targets": 0, "render": function (data, type, row) {
                          return data == "2" ? "第二学年" : data == "3" ? "第三学年" : "第一学年";
                      }
                  },
                   {
                       "targets": 1, "render": function (data, type, row) {
                           return data;
                       }
                   },
                   {
                       "targets": 4, "render": function (data, type, row) {
                           return data == "1" ? " <td class='text-center'><span class='label label-success'>已审核</span></td>" : data == "2" ? "<td class='text-center'><span class='label label-danger'>已退回</span></td>" : " <td class='text-center'><span class='label label-warning'>待审核</span></td>";
                       }
                   },
                   {
                       "targets": 5, "render": function (data, type, row) {
                           return "<td><button data-action='show' class='btn btn-primary btn-sm' data-id='" + data + "'><span class='fa fa-search'></span>查看</button> </td>";
                       }

                   },
            ]
        });

    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }

}

function getRootPath() {
    //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
    var curWwwPath = window.document.location.href;
    //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
    var pathName = window.document.location.pathname;
    var pos = curWwwPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPaht = curWwwPath.substring(0, pos);
    return (localhostPaht);
}