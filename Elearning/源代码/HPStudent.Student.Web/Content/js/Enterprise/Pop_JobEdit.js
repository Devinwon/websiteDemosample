var JobModel = {
    JID: 0,
    Name: '',
    City: '',
    SalaryRange: '',
    WorkType: '',
    DegreeRequired: '',
    ExperienceRequired: '',
    JobDescription: '',
    SID: 0
};
//职位编辑保存事件
function EditOnclick() {
    //数据验证(未完成)
    var checkMenu = $("#MenuFrom").validationEngine("validate");
    if (!checkMenu) {
        return;
    }
    var City = $("#selCityEdit").val();
    if (City == 0) {
        noty({
            text: "请选择城市!",
            layout: 'topRight',
            type: 'error',
        });
        return;
    }
    var JID = $("#hiddenJId").val();
    var Name = $("#TxtNameEidt").val();
    var SalaryRange = $("#selMoneyEdit").val();
    var WorkType = $("#selWorkTypeEdit").val();
    var DegreeRequired = $("#selDegreeRequiredEdit").val();
    var ExperienceRequired = $("#selExperienceRequiredEdit").val();
    var JobDescription = $("#jobContent").val();
    var SID = $("#hiddenSId").val();
    //封装数据
    var JobModelObj = Object.create(JobModel);
    JobModelObj.City = City;
    JobModelObj.DegreeRequired = DegreeRequired;
    JobModelObj.ExperienceRequired = ExperienceRequired;
    JobModelObj.JID = JID;
    JobModelObj.JobDescription = JobDescription;
    JobModelObj.Name = Name;
    JobModelObj.SalaryRange = SalaryRange;
    JobModelObj.WorkType = WorkType;
    JobModelObj.SID = SID;
    var ajaxUrl = "";
    if (JID != null && JID != 0) {
        ajaxUrl = getRootPath() + "/Enterprise/pop_JobUpdate";
    }
    else {
        ajaxUrl = getRootPath() + "/Enterprise/pop_JobAdd";
    }
    var jobSearchObj = {
        Sid: 0
    };
    var JobSearchModel = Object.create(jobSearchObj);
    JobSearchModel.Sid = SID;
    $.ajax({
        type: "post",
        url: ajaxUrl,
        dataType: "json",
        data: JobModelObj,
        success: function (data) {
            if (data.ResultState == 0) {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'success',
                });
                var actioinFun = window.setTimeout(function () {
                    $('#EditJobModal').modal('hide');
                    $("#pop_modaldialog").empty();
                    BindJobDatatable('/Job/QuerySimilarJobJson', JobSearchModel)
                }, 500);
            } else {
                noty({
                    text: data.ResultMsg,
                    layout: 'topRight',
                    type: 'fail',
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}