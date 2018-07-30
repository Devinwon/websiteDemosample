//初始化fileinput
var FileInput = function () {
    var oFile = new Object();
    //初始化fileinput控件（第一次初始化）
    oFile.Init = function (ctrlName, uploadUrl) {
        var control = $('#' + ctrlName);
        //初始化上传控件的样式
        control.fileinput({
            language: 'zh', //设置语言
            uploadUrl: uploadUrl, //上传的地址
            allowedFileExtensions: ['xlsx', 'xls'],//接收的文件后缀
            showUpload: true, //是否显示上传按钮
            showCaption: false,//是否显示标题
            browseClass: "btn btn-primary", //按钮样式	 
            //dropZoneEnabled: false,//是否显示拖拽区域
            //maxFileSize: 0,//单位为kb，如果为0表示不限制文件大小
            //minFileCount: 0,
            maxFileCount: 1, //表示允许同时上传的最大文件个数
            enctype: 'multipart/form-data',
            validateInitialCount: true,
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
        });
        //导入文件上传完成之后的事件
        $("#txt_file").on("fileuploaded", function (event, data, previewId, index) {
            $("#myModal").modal("hide");
            var dataInfo = data.response.lstOrderImport;
            if (dataInfo == "undefined") {
                toastr.error('文件格式类型不正确');
                return;
            }
            else {
                var contentInfo = "";
                if (data.response.names.length>0)
                {
                    var names = "";
                    for (var tt = 0; tt < data.response.names.length; tt++)
                    {
                        names += '\r' + data.response.names[tt] ;
                    }
                    names = names.substring(1, names.length - 1);
                    contentInfo = "，其中" + '\r' + names + '\r' + "等企业信息导入失败！";
                }
                alert("总共导入：" + data.response.total + "条企业信息" + contentInfo);
            }
        });
    }
    return oFile;
};