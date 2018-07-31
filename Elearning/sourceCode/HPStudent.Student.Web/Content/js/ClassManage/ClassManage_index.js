var myDatatalbe = null;
$(function () {
    
});

$("#btnSearchClass").click(function () {
    var Year = $("#sel_Year").val();
    if (Year == 0) {
        noty({
            text: "请选择年级！",
            layout: 'topRight',
            type: 'error',
        });
    } else
    {
        GetPTClassTableBind(getRootPath() + "/ClassManage/GetPTClassTable?Year=" + Year);
    }
   
});

function GetPTClassTableBind(ajaxUrl) {
    var BindHtml = '';
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        success: function (data) {
            if (data.length == 0) {
                BindHtml = '<p>暂无数据！</p>';
            }
            else {
                for (var i = 0; i < data.length; i++) {

                    if (i % 4 == 0) {
                        if (i != 0) {
                            BindHtml += '</div>';
                        }
                        BindHtml += '<div class="row">';
                    }
                    BindHtml += ' <div class="col-md-3">'
                                                 + ' <div id="question-list" class="table-responsive">'
                                                        + '<div class="panel panel-default">'
                                                          + '<div class="panel-heading ui-draggable-handle">'
                                                              + '<h3 class="panel-title">' + data[i].PTCName + '</h3>'
                                                              + '<ul class="panel-controls">'
                                                               + ' <li><a  onclick="GetSearchPTClassList(\'' + data[i].PTCID + '\')"  class="panel-remove"><span class="fa fa-search"></span></a></li>'
                                                                  + '<li class="dropdown">'
                                                                      + '<a onclick="EditPTClass(\'' + data[i].PTCID + '\')" class="dropdown-toggle" data-toggle="dropdown"><span class="fa fa-pencil"></span></a>'

                                                                  + '</li>'
                                                                  + ' <li><a  onclick="DeletePTClass(\'' + data[i].PTCID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                                                                  
                                                              + '</ul>'
                                                          + '</div>'
                                                          + '<div class="panel-body">'
                                                             + ' ' + data[i].StudentName + ''
                                                          + '</div>'
                                                      + '</div>'
                                                     + '</div>'
                                                 + '</div>';
                    if ((i + 1) == data.length) {
                        BindHtml += '</div>';

                    }
                }
            }

            $("#class-list").html(BindHtml);
        }
    });
}


//删除班级
function DeletePTClass(PTCID) {

    myConfirm("确定要删除当前班级吗？", "班级删除后将无法恢复！", "Delete(" + PTCID + ")", "#pop_modaldialog");
}

function Delete(PTCID) {
    ajaxUrl = getRootPath() + "/ClassManage/DeleteClass";
    if (PTCID != "") {
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: { PTCID: PTCID },
            success: function (data) {
                if (data == 1) {
                    noty({
                        text: "删除成功！",
                        layout: 'topRight',
                        type: 'success',
                    });
                    $("#pop_modaldialog").empty();
                    var Year = $("#sel_Year").val();
                    GetPTClassTableBind(getRootPath() + "/ClassManage/GetPTClassTable?Year=" + Year);
                } else {
                    myAlert("提示", "班级删除失败，数据库可能出错了！", "#pop_modaldialog");
                }
            }
        });
    }
}

//修改班级
function EditPTClass(PTCID)
{
    var ajaxUrl = getRootPath() + "/ClassManage/ClassManageEdit";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');

        }, complete: function () {
            
            GetClass(PTCID);

        } 
    });

}


//获得要修改的班级信息
function GetClass(PTCID)
{
    var ajaxUrl = getRootPath() + "/ClassManage/GetClass";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: { PTCID: PTCID },
        success: function (data) {
            $("#txt_PTCID").val(data.PTCID);
            $("#hdnSetClassID").val(data.StudentID);
            $("#txt_PTCName").val(data.PTCName);
            $("#txt_StudyYear").val($("#sel_Year").val());
            $("#txt_StudyYear").attr("disabled", "true");
            for (var i = 0; i < data.StudentInfoList.length; i++) {
                $('#divCname').append("<a href=\"#\"><span  onclick=\"DeleteClass(" + data.StudentInfoList[i].StudentID + ")\" id=\"spanCid" + data.StudentInfoList[i].StudentID + "\" class=\"label label-info label-form fa fa-times\">&nbsp;&nbsp;" + data.StudentInfoList[i].RealName + " </span></a> ");
            }
            GetSchoolSelBind();
        }
    });


}

function GetSearchPTClassList(PTCID)
{

    var ajaxUrl = getRootPath() + "/ClassManage/PTClassStudentInfo";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');

        }, complete: function () {

            SearchPTClassList(PTCID);
            

        }
    });

}


//绑定表格
function SearchPTClassList(PTCID) {
    var ajaxUrl = getRootPath() + "/ClassManage/GetClassStudentInfo";
    var dataJson = { PTCID: PTCID }
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_studentinfo").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl,
                "data": dataJson
            },
            "serverMethod": 'POST',
            "columns": [
	            { "data": "RealName" },
                { "data": "Sex" },
                { "data": "LastLoginTime" },
                { "data": "ResumeStatus" },
	            { "data": "Operation" }
            ],
            "columnDefs": [
                {
                    "targets": 0,
                    "class": "text-center"
                },
                 {
                     "targets": 1,
                     "class": "text-center",
                     "targets": [1], "render": function (data, type, row) {
                         var sex = "女";
                         if (data == "0") {
                             sex = "男"
                         }
                         return sex;
                     }

                 },
                  {
                      "targets": 2,
                      "class": "text-center"
                  },
                   {
                       "targets": 3,
                       "class": "text-center",
                       "render": function (data, type, row) {
                           var status = "<span class=\"label label-info\"><span class=\"fa fa-check\"></span></span>";
                           if (data == "0") {
                               status = "<span class=\"label label-danger\"><span class=\"fa fa-times\"></span></span>";
                           } 
                           return status;
                       }
                   }
                   ,
                   {
                       "targets": 4,
                       "class": "text-center"
                   }
            ]
        });
        onresize();
       
       
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = { "Ajax": ajaxUrl, "data": dataJson };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();
        

    }


}


//测试列表的测试和查看项添加事件
$("#pop_modaldialog").delegate(".btn-primary", "click", function () {
    var StudentID = $(this).attr("data-id");
    var type = $(this).attr("data-action");
    if (type == "seeResume")
    {
        var ajaxUrl = getRootPath() + "/Resume/PreviewResume?SID=" + StudentID;

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog-Resume").empty();
                $('#pop_modaldialog-Resume').append(html);
                $('#previewResumepanel').modal('show');
            },
            complete: function (html) {
            }
        });
    } else if (type == "editStuInfo")
    {
        var ajaxUrl = getRootPath() + "/ClassManage/EditStuInfo?SID=" + StudentID;
       
        $.ajax({
            type: "Post",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog-StuInfo").empty();
                $('#pop_modaldialog-StuInfo').append(html);
                $('#editStuInfo').modal('show');
            }
        });
    }

    

});

function BindEditStuInfo(StudentID)
{
    var ajaxUrl = getRootPath() + "/ClassManage/BindEditStuInfo";
    $.ajax({
        type: "Post",
        url: ajaxUrl,
        success: function (date) {
            
        }
    });

}


$("#pop_modaldialog-StuInfo").delegate("#txt_Email", "change", function () {
    var Email = $("#txt_Email").val();
    if (Email.substring(Email.length - 7, Email.length) == "@qq.com") {
        var result = /\d+(?:\.\d+)?/.exec(Email);
        $("#txt_QQ").val(result[0]);
    }
})



function EditStuInfo()
{
   var StudentID = $("#txt_StudentID").val();
   var Name = $("#txt_Name").val();
   var StartYear = $("#selStartYear option:selected").val();
   var Sex = $('input[name="optionsRadios"]:checked ').val();
   var Email = $("#txt_Email").val();                
   var Brithday = $("#txt_Brithday").val();
   var Nation = $("#txt_Nation").val();
   var HomeAddress = $("#txt_HomeAddress").val();
   var HomeMobile = $("#txt_HomeMobile").val();
   var QQ = $("#txt_QQ").val();
   var IDCard = $("#txt_IDCard").val();
   var Password = IDCard.substring(IDCard.length - 6, IDCard.length);
   if (Name == "" || Name==null||Name==undefined)
   {
       noty({
           text: "请填写班级！",
           layout: 'topRight',
           type: 'error',
       });
   } else if (Email == "" || Email == null||Email == undefined)
   {
       noty({
           text: "请填写邮箱！",
           layout: 'topRight',
           type: 'error',
       });
   } else if (/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(Email) == false)
   {
       noty({
           text: "邮箱格式不正确！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (Nation == "" || Nation == null || Nation == undefined) {
       noty({
           text: "请填写民族！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (HomeAddress == "" || HomeAddress == null || HomeAddress == undefined) {
       noty({
           text: "请填写家庭住址！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (HomeMobile == "" || HomeMobile == null || HomeMobile == undefined) {
       noty({
           text: "请填写联系电话！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (/^1[34578]\d{9}$/.test(HomeMobile) == false) {
       noty({
           text: "联系电话格式不正确！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (QQ == "" || QQ == null || QQ == undefined) {
       noty({
           text: "请填写QQ号码！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else if (IDCard == "" || IDCard == null || IDCard == undefined) {

       noty({
           text: "请填写身份证号！",
           layout: 'topRight',
           type: 'error',
       });


   }
   else if (/(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/.test(IDCard) == false) {
       noty({
           text: "身份证号格式不正确！",
           layout: 'topRight',
           type: 'error',
       });
   }
   else {
       $.ajax({
           type: "POST",
           url: getRootPath() + "/ClassManage/UpdateStuInfo",
           data: { StudentID: StudentID, Name: Name, StartYear: StartYear, Sex: Sex, Email: Email, Nation: Nation, HomeAddress: HomeAddress, HomeMobile: HomeMobile, QQ: QQ, IDCard: IDCard, Password: Password },
           success: function (data) {
               if (data == 1) {
                   noty({
                       text: "修改成功！",
                       layout: 'topRight',
                       type: 'success',
                   });
                   $('#editStuInfo').modal("hide");
                   $("#pop_modaldialog-StuInfo").empty();
               }
           }, error: function (data) {
               noty({
                   text: "出错了，请联系管理员！",
                   layout: 'topRight',
                   type: 'success',
               });
           }


       })

   }


}

    //添加实训班级
    $("#btnAddClass").click(function () {
        var Year = $("#sel_Year").val();
        if (Year == 0) {
            noty({
                text: "请选择年级！",
                layout: 'topRight',
                type: 'error',
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: getRootPath() + "/ClassManage/ClassManageEdit",
                success: function (html) {
                    $("#pop_modaldialog").empty();
                    $('#pop_modaldialog').append(html);
                    $('#edit-manager').modal('show');

                }, complete: function (html)
                {
                    if (html != null)
                    {
                        feiCheckbox();
                    }

                    GetSchoolSelBind();
                    $("#txt_StudyYear").val($("#sel_Year").val());
                    $("#txt_StudyYear").attr("disabled", "true");
                    $("#defModalHead").html("<span class=\"fa fa-pencil\"></span> 添加实训班级");
                }
                //,complete: function () {
                //    ClassGroupEditBind(CID, '', 0);
                //    $('#AllChecked').on('ifChecked', function () {
                //        $('#StudentList input').each(function () {
                //            $(this).iCheck('check');
                //        });
                //    });
                //    $('#AllChecked').on('ifUnchecked', function () {
                //        $('#StudentList input').each(function () {
                //            $(this).iCheck('uncheck');
                //        });
                //    });
                //}
            });
        }
    });

    //加载校区
    function GetSchoolSelBind()
    {
        var ajaxUrl = getRootPath() + "/ClassManage/GetSchoolSelBind";
        var classOption = '';
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    classOption += '<option value=' + data[i].SchoolID + '>' + data[i].SchoolName + '</option>';
                }
                $("#sel_School").append(classOption);
            }
        });
    }
    //加载学校班级
    $(document).delegate("#sel_School", "change", function (event) {
        var Year = $("#sel_Year").val();
        var School = $("#sel_School").val();
            $("#sel_Class").empty();
            $("#sel_Class").append("<option value='0'>== 请选择 ==</option>");
            var ajaxUrl = getRootPath() + "/ClassManage/GetClassSelBind";
            var classOption = '';
            $.ajax({
                type: "POST",
                url: ajaxUrl,
                data: { Year: Year, School: School },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        classOption += '<option value=' + data[i].CID + '>' + data[i].CName + '</option>';
                    }
                    $("#sel_Class").append(classOption);
                }
            });

        
    });


    //加载学校班级
    $(document).delegate("#sel_Class", "change", function (event) {
        $('#StudentList').empty();
        var Class = $("#sel_Class").val();
        var School = $("#sel_School").val();
        var PTCID = $("#txt_PTCID").val();
        var Year = $("#sel_Year").val();
        var ajaxUrl = getRootPath() + "/ClassManage/GetClassStudentBind";
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: { PTCID: PTCID, School: School, Class: Class, Year: Year },
            success: function (data) {
               
                for (var i = 0; i < data.length; i++) {
                    var strCheck = "";
                    $('#StudentList').append("<label class=\"check col-md-3\"><input type=\"checkbox\" name=\"chkItem\" id=\"chkItem_" + data[i].StudentID + "\"  data-id =\"" + data[i].StudentID + "\"  data-name =\"" + data[i].RealName + "\"  class=\"icheckbox\" " + strCheck + "/> " + data[i].RealName + "</label>");
                    var StudentList = $("#hdnSetClassID").val();
                    var arrStuID = StudentList.split(",");
                    for (var j = 0; j < arrStuID.length; j++) {
                        if (arrStuID[j] == data[i].StudentID)
                        {
                            $("#chkItem_" + data[i].StudentID + "").iCheck('check');
                        }
                    }
                    //if (arrStuID.toString().indexOf(data[i].StudentID) > -1)
                    //{
                    //    $("#chkItem_" + data[i].StudentID + "").iCheck('check');
                    //}
                }


                $(".icheckbox").on("ifChecked", function (event) {
                    var dataID = $(this).attr("data-id");
                    var dataName = $(this).attr("data-name");
                    iCheckBoxClick(dataID, dataName, true);
                });
                //取消
                $(".icheckbox").on("ifUnchecked", function (event) {
                    var dataID = $(this).attr("data-id");
                    var dataName = $(this).attr("data-name");
                    iCheckBoxClick(dataID, dataName, false);
                });
            }, complete: function (data) {
                //修复icheck无法显示样式的bug
                feiCheckbox();
            }
        });

    });

    //ICheckBox选中事件
    function iCheckBoxClick(dataID, dataName, ischeck) {
        var classid = $("#hdnSetClassID").val();



        if (ischeck == true) {
            $('#divCname').append("<a href=\"#\"><span  onclick=\"DeleteClass(" + dataID + ")\" id=\"spanCid" + dataID + "\" class=\"label label-info label-form fa fa-times\">&nbsp;&nbsp;" + dataName + " </span> </a>");
            classid +=  dataID+"," ;
        } else if (ischeck==false)
        {
            $("#spanCid" + dataID).remove();
            
            var arrClassID = classid.split(',');
            if (arrClassID.indexOf(dataID.toString()) == -1)
            {
                return;
            }
            arrClassID.splice(arrClassID.indexOf(dataID.toString()), 1);
            classid = arrClassID;
        }
        $("#hdnSetClassID").val(classid);

    }


    function DeleteClass(cid) {
        noty({
            text: '是否确定删除?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: '确定', onClick: function ($noty) {
                            //删除标签
                            iCheckBoxClick(cid ,"", false)
                            //关闭当前页选中的CheckBox
                            var arrstr;
                            $(".icheckbox").each(function () {
                                if ($(this).is(':checked') == true) {
                                    stuid = $(this).attr("data-id");
                                    if (stuid == cid) {
                                        $(this).iCheck('uncheck');
                                    }
                                }
                            });
                            $noty.close();
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

    function EditClassManage() {
        var PTCID = $("#txt_PTCID").val();
        var PTCName = $("#txt_PTCName").val();
        var StudentID = $("#hdnSetClassID").val();
        var SchoolID = $("#sel_School  option:selected").val();
        var ClassID = $("#sel_Class  option:selected").val();
        var Year = $("#sel_Year").val();

       
         
         if (PTCName == "" || PTCName == undefined) {
             noty({
                 text: '未填写班级名称',
                 layout: 'topRight',
                 type: 'error',
             });
         } else {
             
           
             if (PTCID != "") {
                 if (StudentID == "") {
                     if (SchoolID == "0") {
                         noty({
                             text: '未选择校区',
                             layout: 'topRight',
                             type: 'error',
                         });
                     } else if (ClassID == "0") {
                         noty({
                             text: '未选择班级',
                             layout: 'topRight',
                             type: 'error',
                         });
                     } else if (StudentID == "" || StudentID == undefined) {
                         noty({
                             text: '未勾选学员',
                             layout: 'topRight',
                             type: 'error',
                         });
                     }

                 } else
                 {
               

                ajaxUrl = getRootPath() + "/ClassManage/UpdatePTClass";
                $.ajax({
                    type: "POST",
                    url: ajaxUrl,
                    data: { PTCID: PTCID, PTCName: PTCName, StudentID: StudentID, Year: Year },
                    success: function (data) {
                        if (data == 1) {
                            noty({
                                text: "修改成功！",
                                layout: 'topRight',
                                type: 'success',
                            });
                            $('#edit-manager').modal('hide');
                            $("#pop_modaldialog").empty();
                            GetPTClassTableBind(getRootPath() + "/ClassManage/GetPTClassTable?Year=" + Year);
                        } else {
                            noty({
                                text: "修改失败！",
                                layout: 'topRight',
                                type: 'error',
                            });
                        }
                    }
                });
                 }
            } else {
                if (SchoolID == "0") {
                    noty({
                        text: '未选择校区',
                        layout: 'topRight',
                        type: 'error',
                    });
                } else if (ClassID == "0") {
                    noty({
                        text: '未选择班级',
                        layout: 'topRight',
                        type: 'error',
                    });
                } else if (StudentID == "" || StudentID == undefined) {
                    noty({
                        text: '未勾选学员',
                        layout: 'topRight',
                        type: 'error',
                    });
                }
                else
                {
                ajaxUrl = getRootPath() + "/ClassManage/AddPTClass";
                $.ajax({
                    type: "POST",
                    url: ajaxUrl,
                    data: {  PTCName: PTCName, StudentID: StudentID, Year: Year},
                    success: function (data) {
                        if (data == 1) {
                            noty({
                                text: "添加成功！",
                                layout: 'topRight',
                                type: 'success',
                            });
                            $('#edit-manager').modal('hide');
                            $("#pop_modaldialog").empty();
                            GetPTClassTableBind(getRootPath() + "/ClassManage/GetPTClassTable?Year=" + Year);
                        } else {
                            noty({
                                text: "添加失败！",
                                layout: 'topRight',
                                type: 'error',
                            });
                        }
                    }
                });

                }

            }
        }
    }




    //关闭子类编辑
    function CloseModal() {
        $('#pop_modaldialog').modal('hide');
        $("#pop_modaldialog").empty();
        $("#pop_modaldialog-Resume").empty();
        myDatatalbe = null;
    }


    $('#pop_modaldialog').on('hide.bs.modal', function () {
        myDatatalbe = null;
    })







    //修复动态加载的Checkbox无法显示样式的BUG
    var feiCheckbox = function () {
        if ($(".icheckbox").length > 0) {
            $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
        }
    }