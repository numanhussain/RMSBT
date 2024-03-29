﻿function OpenPartialView(myUrl) {
    $.ajax({
        url: myUrl,
        type: 'GET',
        cache: false,
        success: function (data) {
            location.reload();
        },
        error: function () {
            $("#result").text('an error occured');
        }
    });
}
function SelectAllCheckBox() {
    var checkboxes = $(':checkbox');

    //checkboxes.prop('checked', true);
    // Select/Deselect for Employee
    $("#chkSelectAll").bind("change", function () {
        $(".chkSelect").prop("checked", $(this).prop("checked"));
    });
    $(".chkSelect").bind("change", function () {
        if (!$(this).prop("checked"))
            $("#chkSelectAll").prop("checked", false);
    });

    //checkboxes.prop('checked', true);
    // Select/Deselect for Employee
    $("#chkSelectAllCatagory").bind("change", function () {
        $(".chkCatagorySelect").prop("checked", $(this).prop("checked"));
    });
    $(".chkCatagorySelect").bind("change", function () {
        if (!$(this).prop("checked"))
            $("#chkSelectAllCatagory").prop("checked", false);
    });
    $("#declarationdivhide").hide();
    $("#ApproveDecline").hide();
    $('#ask2').change(function () {
        if ($(this).is(":checked")) {
            $("#hideshow2").hide();
            $("#titlehide").show();
            $("#hrhide").hide();
            $("#profiledivhide").hide();
            $("#declarationdivhide").show();
        }
        else {
            $("#titlehide").show();
            $("#hrhide").show();
            $("#profiledivhide").show();
            $("#declarationdivhide").hide();
        }
    });
    $('#ask').change(function () {
        if ($(this).is(":checked")) {
            $("#ApproveDecline").show();
        }
        else {
            $("#ApproveDecline").hide();
        }
    });

}
function ApplyJob(id, item) {
    $.ajax({
        url: '/Job/JobApply',
        type: "POST",
        cache: false,
        data: { JobID: id }
    }).done(function (data) {
        if (data === "OK") {
            $('#myModal1').modal('hide');
            $.jGrowl('<div>Welcome to Bestway Cement!</div><div>You have successfully applied for this job. You can check the status of your application later by logging into your account at Bestway Career Portal.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div>', {
                header: '',
                position: 'center',
                theme: 'alert-styled-right bg-info',
                life: 7000
            });
            $("#DivJobApplied").show();
            $("#DivJobApply").hide();
            $("#DivJobApplied2").show();
            $("#DivJobApply2").hide();
        }
        else { alert(data); }
    });
}
function ViewProfileIndex(id, item, CateID) {
    if (item === 0) {
        $.jGrowl('<div>You have to complete your profile first.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div></strong>', {
            header: '',
            position: 'center',
            theme: 'alert-styled-left bg-danger',
            life: 9000
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: "/Job/ViewProfileIndex",
            contentType: "application/json; charset=utf-8",
            data: { JobID: id },
            datatype: "json",
            success: function (data) {
                $('#modelBody1').html(data);
                $('#myModal1').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        $("#closbtn").click(function () {
            $('#myModal1').modal('hide');
        });
    }
}
function CandidateGetCreate() {
    $.ajax({
        url: '/Candidate/Create',
        type: "GET",
        cache: false
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
    });
}
function GetFilteredListHome() {
    var CatID = $("#CatagoryID").val();
    var LocID = $('#LocationID').val();
    $.ajax({
        type: "POST",
        url: "/Home/IndexSubmit",
        data: { LocationID: LocID, CatagoryID: CatID },
        datatype: "json",
        success: function (data) {
            $('#PVDTBody').html(data);
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
    $("#CatagoryID").on('change', function () {
        var CatID = $(this).val();
        var LocID = $('#LocationID').val();
        $.ajax({
            type: "POST",
            url: "/Home/IndexSubmit",
            data: { LocationID: LocID, CatagoryID: CatID },
            datatype: "json",
            success: function (data) {
                $('#PVDTBody').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#LocationID").on('change', function () {
        var LocID = $(this).val();
        var CatID = $('#CatagoryID').val();
        $.ajax({
            type: "POST",
            url: "/Home/IndexSubmit",
            data: { LocationID: LocID, CatagoryID: CatID },
            datatype: "json",
            success: function (data) {
                $('#PVDTBody').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function GetFilteredList() {
    var CatID = $("#CatagoryID").val();
    var LocID = $('#LocationID').val();
    $.ajax({
        type: "POST",
        url: "/Job/IndexSubmit",
        data: { LocationID: LocID, CatagoryID: CatID },
        datatype: "json",
        success: function (data) {
            $('#PartialContainer').html(data);
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
    $("#CatagoryID").on('change', function () {
        var CatID = $(this).val();
        var LocID = $('#LocationID').val();
        $.ajax({
            type: "POST",
            url: "/Job/IndexSubmit",
            data: { LocationID: LocID, CatagoryID: CatID },
            datatype: "json",
            success: function (data) {
                $('#PartialContainer').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#LocationID").on('change', function () {
        var LocID = $(this).val();
        var CatID = $('#CatagoryID').val();
        $.ajax({
            type: "POST",
            url: "/Job/IndexSubmit",
            data: { LocationID: LocID, CatagoryID: CatID },
            datatype: "json",
            success: function (data) {
                $('#PartialContainer').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function CheckCVAttachedJobUpper(HasCV, id, item, CateID) {
    $('#DivJobApply').click(function () {
        if (HasCV === "True") {
            //var fileUpload = $("#CVUpload").get(0);
            //if (fileUpload.files.length === 0) {
            //    // Display Message
            //    alert("Please attach CV.");
            //}
            //else {
            ViewProfileIndex(id, item, CateID);
        }
        else {
            alert("Please attach CV or update your CV again.");
        }
    });
}
function CheckCVAttachedJobLower(HasCV, id, item, CateID) {
    $('#DivJobApply2').click(function () {
        if (HasCV === "True") {
            //var fileUpload = $("#CVUpload").get(0);
            //if (fileUpload.files.length === 0) {
            //    // Display Message
            //    alert("Please attach CV.");
            //}
            //else {
            ViewProfileIndex(id, item, CateID);
        }
        else {
            alert("Please attach CV or update your CV again.");
        }
    });
}