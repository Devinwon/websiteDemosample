//数据初始化
var myDatatalbe; //初始化一个Datatable容器
var starPage; //起始页

$(function () {

    //省份下拉框变化
    $('#selProvince').change(function () {

        var ajaxUrl = "/Student/GetComSchoolByParentAID";
        var ParentAID = $('#selProvince').val();
        
        if (ParentAID == "== 选择省 ==") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
      
        $('#selCity').empty();
        //   BindDatatable(ajaxUrl);

        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            data: "ParentAID=" + ParentAID,

            success: function (data) {
                //填充Datatable
                $.each(data, function (i, item) {
                    $('#CommonSchoolList').append("<tr><td>" + item.SchoolID + "</td><td>"
                        + item.AreaName + "</td><td>" + item.CityName + "</td><td>"
                        + item.SchoolName + "</td><td><button class='btn btn-primary btn-sm' data-id='"
                        + item.SchoolID + "' data-action='edit'><span class='fa fa-pencil'></span>编辑</button><button class='btn btn-primary btn-sm' type='button' data-id='"
                        + item.SchoolID + "'><span class='fa fa-times'></span> 删除</button></td></tr>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }

        });
        if ($(this).val() == "湖北省") {
            $("#selCity").empty();
            $('#selCity').append("<option>== 选择城市 ==</option>");
            $('#selCity').append("<option>武汉市</option>");
            $('#selCity').append("<option>宜昌市</option>");
            $('#selCity').append("<option>黄冈市</option>");
            $('#selCity').append("<option>荆州市</option>");
        }
    });

    $("#btnSelect").on('click', function (e) {
        
        var ajaxUrl = "/Student/GetSchoolByAreaID";
        var ParentAID = $('#selProvince').val();
        var AreaID = $('#selCity').val();
        if (ParentAID == "== 选择省 ==") {
            myAlert("选择的省不正确！", "请重新选择", "#pop_modaldialog");
            return false;
        }
        if (AreaID == "== 选择城市 ==") {
            AreaID = "";
        }
        $('#CommonSchoolList').empty();
        //   BindDatatable(ajaxUrl);

        $.ajax({
            type: "post",
            url: ajaxUrl,
            dataType: "json",
            data: "ParentAID=" + CID + "&AreaID=" + AreaID,

            success: function (data) {
                //填充Datatable
                $.each(data, function (i, item) {
                    $('#CommonSchoolList').append("<tr><td>" + item.SchoolID + "</td><td>"
                        + item.AreaName + "</td><td>" + item.CityName + "</td><td>"
                        + item.SchoolName + "</td><td><button class='btn btn-primary btn-sm' data-id='"
                        + item.SchoolID + "' data-action='edit'><span class='fa fa-pencil'></span>编辑</button><button class='btn btn-primary btn-sm' type='button' data-id='"
                        + item.SchoolID + "'><span class='fa fa-times'></span> 删除</button></td></tr>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }

        });
        return false;
    });

    //添加校区
    $('#btnAddSchool').on('click', function (e) {
        //如果没有选择省份或城市，弹出提示
        if ($('#selProvince').val() == "== 选择省 ==" || $("#selCity").val() == "== 选择城市 ==") {
            alert("请先选择省份和城市后再添加校区");
            return false;
        }

        //弹出添加窗口
        var ajaxUrl = "Pop_StudentSchool_Add?" + Date.now();

        //$.get(ajaxUrl,function(html,status){
        //	$("#pop_modaldialog").empty();
        //    $('#pop_modaldialog').append(html);
        //    $('#add-student-school').modal('show'); 
        //});	
        alert($('#selProvince').find("option:selected").text());
        alert($('#selCity').find("option:selected").text());
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#add-student-school').modal('show');
            },
            complete: function (html) {
                $('#pop_SelProvince').val($('#selProvince').find("option:selected").text());
                $('#pop_SelCity').val($('#selCity').find("option:selected").text());
            }
        });

    });

    //成绩列表的编辑和删除项添加事件
    $('#class-list').find('.btn-primary').on('click', function (e) {
        if ($(this).attr("data-action") == "edit") {
            //编辑按钮
            var ajaxUrl = "Pop_StudentSchool_Add?id=" + $(this).attr("data-id") + "&" + Date.now();
            $.get(ajaxUrl, function (html, status) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-student-school').modal('show');
            });

        } else {
            //删除按钮
            myConfirm("确定要删除此考试成绩吗？", "数据删除后将无法恢复！", "DelSchool()", "#pop_modaldialog");

            return false;
        }


    });


    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {
        $(this).parents(".message-box").removeClass("open");
    });

    //弹出确认窗口关闭  -- 确定
    $("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {
        $(this).parents(".message-box").removeClass("open");
        // alert('2');
    });

});

//删除班级信息
function DelSchool() {
    alert("数据删除成功");
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}

//添加班级信息
function AddSchool() {
    alert("数据添加成功");
    $('#add-student-school').modal('hide');
}

//添加班级信息
function EditSchool() {
    alert("数据修改成功");
    $('#edit-student-school').modal('hide');
}

function BindDatatable(ajaxUrl, datas) {
    if (myDatatalbe == undefined) {
        myDatatalbe = $(".datatable_question").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: "./content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "type": 'POST',
            "data": datas,
            "columns": [
	            { "data": "编号" },
	            { "data": "省份" },
	            { "data": "城市" },
                { "data": "校区" },
	            { "data": "操作" },
            ]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        oSettings.data = datas;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}