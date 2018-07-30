$(function () {

    //页面打开时直接显示所有项
    SelectMajor(0,1);

    //2015年12月30日 涂建 增加记录历史的功能（用于浏览器返回按钮）
    if (history.pushState) {
        window.addEventListener("popstate", function () {
            initPage();
        });
    }


    //ajax加载专业列表
    $.ajax({
        type: "post",
        url: "/Projects/GetAllMajorList",
        dataType: "json",
        data: "",

        success: function (data) {
            $('#selMajor').empty();
            $('#selMajor').append("<option value='0'>== 选择专业 ==</option>");
            $.each(data, function (i, item) {
                $('#selMajor').append("<option value='" + item.MID + "'>" + item.MajorName + "</option>");
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

  

    //选择按钮被点击时触发
    $('#btnSelectMajor').on('click', function () {
        //选择课程
        var CID = $('#selMajor').val();
        SelectMajor(CID,1);
        //2015年12月30日 涂建 增加记录历史的功能（用于浏览器返回按钮）
        history.pushState({ cid: CID }, CID, location.href.split("?")[0] + "?" + CID);


    });

    $("#gridview_page").delegate("li", "click", function () {
        if ($(this).attr("class") != "active" && $(this).attr("class") != "disabled") {
            pageindex = $(this).find("a").attr("data-dt-idx");
            var query = location.href.split("?")[1];
            var url = "";
            if (typeof query == "undefined") {
                var url = location.href.split("?")[0] + "?0-" + pageindex;
            } else {
                if (query.indexOf("-") > 0) {
                    query = query.split("-")[0];
                }
                var url = location.href.split("?")[0] + "?" + query + "-" + pageindex;
            }
            //2015年12月30日 涂建 增加记录历史的功能（用于浏览器返回按钮）
            history.pushState({ cid: query }, query, url);
        }
    });

});

function initPage(target) {
    var query = location.href.split("?")[1], eleTarget = target || null;
    var pageindex = 1;
    if (typeof query == "undefined") {

    } else {
        if (query.indexOf("-") > 0)
        {
            pageindex = query.split("-")[1];
            query = query.split("-")[0];
        }
        SelectMajor(query, pageindex);
        //同步更新下拉列表项
        $("#selMajor option").removeAttr("selected");
        $("#selMajor option[value=" + query + "]").attr("selected", true);

    }
};

function SelectMajor(CID,INDEX) {

    //if (CID == "0") {
    //    noty({
    //        text: "请先选择专业！",
    //        layout: 'topRight',
    //        type: 'warning',
    //    });
    //    return false;
    //}

    //3. AJAX动态调用后台接口生成练习题
    var myGridview = $('#gridview').GridView({
        ajaxUrl: '/Projects/GetPorjectList',
        ajaxData: { CID: CID },
        pageSize: 8,
        pageIndex: INDEX,
        pageDiv: 'pageDiv',
        titleLength: 20
    });
    onresize();
}