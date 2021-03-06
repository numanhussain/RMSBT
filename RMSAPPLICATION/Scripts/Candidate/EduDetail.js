﻿function LoadPVEduDetailIndex(id) {
    clearClasses();
    $.ajax({
        url: '/EduDetail/Index',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        $("#hv8").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 3;
    });
};
function EduDetailGetCreate(id) {
    $('#EduGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/EduDetail/Create",
            contentType: "application/json; charset=utf-8",
            data: { "id": id },
            datatype: "json",
            success: function (data) {
                $('#modelBody').html(data);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function EduDetailPostCreate(id) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/EduDetail/Create',
            type: 'POST',
            cache: false,
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    LoadPVEduDetailIndex(id)
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'alert-styled-right bg-info',
                        life: 6000
                    });
                }
                else {
                    $('#modelBody').html(data);
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function EduDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/EduDetail/Edit",
        contentType: "application/json; charset=utf-8",
        data: { "id": id },
        datatype: "json",
        success: function (data) {
            $('#modelBody').html(data);
            $('#myModal').modal('show');

        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}
function EduDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/EduDetail/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    LoadPVEduDetailIndex(id)
                }
                else {
                    $('#modelBody').html(data);
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function EduDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/EduDetail/Delete",
        contentType: "application/json; charset=utf-8",
        data: { "id": id },
        datatype: "json",
        success: function (data) {
            $('#modelBody').html(data);
            $('#myModal').modal('show');
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}
function EduDetailPostDelete(id) {
    $('#btnDeletePost').click(function () {
        $.ajax({
            type: "POST",
            url: "/EduDetail/Delete",
            data: $("#formDeleteID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVEduDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
//function DegreeChange() {
//    $("#InstitutionDD").hide();
//    $("#CGPATB").hide();
//    $("#DegreeID").on("change", function () {
//        if ($("#DegreeID option:selected").val() == 3) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 4) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 5) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 6) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        else {
//            $("#InstitutionDD").hide();
//            $("#CGPATB").hide();
//        }
//  });

//}
function DegreeChange() {
    ShowHide();
    $("#DegreeLevelID").on("change", function () {
        ShowHide();
    });
}
function ShowHide() {
    $("#InstitutionDD").hide();
    $("#DegreeTypeDD").hide();
    $("#DegreeTitleTB").hide();
    $("#OtherInstitute").hide();
    $("#MajorSubjectDD").hide();
    $("#marksdiv").hide();
    $("#OtherDegree").hide();
    $("#BoardDD").hide();
    $("#OtherBoard").hide();
    $("#marksdiv").hide();
    $("#GradeDiv").hide();
    $("#CGPADiv").hide();
    switch ($("#DegreeLevelID").val()) {
        case "1":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").hide();
            $("#BoardDD").show();
            break;
        case "2":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").show();
            break;
        case "3":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").hide();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").show();
            break;
        case "4":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").hide();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").show();
            break;
        case "5":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").show();
            break;
        case "6":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").hide();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").show();
            break;
        case "7":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").show();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").hide();
            break;
        case "8":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").show();
            $("#DegreeTypeDD").show();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").hide();
            break;
        case "9":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").show();
            $("#DegreeTypeDD").hide();
            $("#MajorSubjectDD").show();
            $("#marksdiv").show();
            $("#BoardDD").hide();
            break;
        case "10":
            $("#DegreeTitleTB").show();
            $("#InstitutionDD").show();
            $("#DegreeTypeDD").hide();
            $("#MajorSubjectDD").show();
            $("#marksdiv").hide();
            $("#BoardDD").hide();
            break;
    }
}
function InstituteChange() {
    $("#hidedatediv").show();
    if (document.getElementById('InProgress').checked) {
        $("#hidedatediv").hide();
    }
    //LFA Selected
    $('#InProgress').click(function () {
        if ($(this).is(":checked")) {
            $("#hidedatediv").hide();
        }
        else {
            $("#hidedatediv").show();
        }

    });

    // Other Institute Show
    ShowInstituteHide();
    $("#InstitutionID").on("change", function () {
        ShowInstituteHide();
    });
    // Degree level Other
    ShowOtherDegreeLevel();
    $("#DegreeLevelID").on("change", function () {
        ShowOtherDegreeLevel();
    });

    // Other Board Name
    ShowOtherBoard();
    $("#BoardID").on("change", function () {
        ShowOtherBoard();
    });
    // Other Board Name
    ShowEduCriteria();
    $("#EduCriteriaID").on("change", function () {
        ShowEduCriteria();
    });
}
function ShowInstituteHide() {
    $("#OtherInstitute").hide();
    if ($("#InstitutionID").val() == 150) {
        $("#OtherInstitute").show();
    }
}
function ShowOtherDegreeLevel() {
    // Degree Level Other
    $("#OtherDegreelevel").hide();
    if ($("#DegreeLevelID").val() == 11) {
        $("#OtherDegreelevel").show();
    }
}

function ShowOtherBoard() {
    // Degree Level Other
    $("#OtherBoard").hide();
    if ($("#BoardID").val() == 48) {
        $("#OtherBoard").show();
    }
}

function ShowEduCriteria() {
    $("#TotalMarksDiv").hide();
    $("#GradeDiv").hide();
    $("#CGPADiv").hide();
    if ($("#EduCriteriaID").val() == 1) {
        $("#TotalMarksDiv").show();
        $("#GradeDiv").hide();
        $("#CGPADiv").hide();
    }
    else if ($("#EduCriteriaID").val() == 2) {
        $("#TotalMarksDiv").hide();
        $("#GradeDiv").show();
        $("#CGPADiv").hide();
    }
    else if ($("#EduCriteriaID").val() == 3) {
        $("#TotalMarksDiv").hide();
        $("#GradeDiv").hide();
        $("#CGPADiv").show();
    }
}
function DegreeTypeChange() {
    $("#DegreeTypeID").on("change", function () {
        ShowOtherDegreeHide();
    });
}
function ShowOtherDegreeHide() {
    $("#OtherDegree").hide();
    if ($("#DegreeTypeID").val() == 70 || $("#DegreeTypeID").val() == 71 || $("#DegreeTypeID").val() == 72 || $("#DegreeTypeID").val() == 73 || $("#DegreeTypeID").val() == 74 || $("#DegreeTypeID").val() == 75 || $("#DegreeTypeID").val() == 68 || $("#DegreeTypeID").val() == 76) {
        $("#OtherDegree").show();
    }
}
function LoadDegreeTypeDD() {
    //Load DegreeType
    $('#DegreeTypeID').empty();
    var convalue = $('#selectedDegreeLevelIDHidden').val();
    var URL = '/EduDetail/DegreeTypeList';
    $.getJSON(URL + '/' + convalue, function (data) {
        var items;
        if (document.getElementById("selectedCityIdHidden").value != null)
            var selectedItemID = document.getElementById("selectedCityIdHidden").value;
        $.each(data, function (i, state) {
            if (state.Value == selectedItemID)
                items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#DegreeTypeID').html(items);
    });
    //Selection Degre Level:
    $("#DegreeLevelID").on('change', function () {
        $('#DegreeTypeID').empty();
        var convalue = $('#DegreeLevelID').val();
        var URL = '/EduDetail/DegreeTypeList';
        //var URL = '/Emp/LocationList';
        $.getJSON(URL + '/' + convalue, function (data) {
            var items;
            if (document.getElementById("selectedCityIdHidden").value != null)
                var selectedItemID = document.getElementById("selectedCityIdHidden").value;

            $.each(data, function (i, state) {
                if (state.Value == selectedItemID)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#DegreeTypeID').html(items);
        });


    });
}
function CalculatePercentage() {
    $("#ObtainedMark").change(function () {
        var TotalMark = $('#TotalMark').val();
        var ObtainedMark = $('#ObtainedMark').val();
        $.ajax({
            url: "/EduDetail/CalculatePercentage",
            contentType: "application/json; charset=utf-8",
            data: { TotalMark: TotalMark, ObtainedMark: ObtainedMark },
            success: function (data) {
                document.getElementById("Percentage").value = data;
            },
            error: function () {
                $("#result").text('an error occured')
            }
        });
    });
}

function EduDateLoad() {
    var date_inputEduStartDate = $('input[name="StartDate"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_inputEduStartDate.datepicker({
        format: 'dd-MM-yyyy',
        orientation: 'bottom',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
    var date_inputEduEndDate = $('input[name="EndDate"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_inputEduEndDate.datepicker({
        format: 'dd-MM-yyyy',
        orientation: 'bottom',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
}