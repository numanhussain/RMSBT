function OpenPartialView(myUrl) {
    $.ajax({
        url: myUrl,
        type: 'GET',
        cache: false,
        success: function (data) {
            location.reload();
        },
        error: function () {
            $("#result").text('an error occured')
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
    var checkboxes = $(':checkbox');

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
            $("#titlehide").hide();
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
            $.jGrowl('<div>Welcome to Bestway!</div><div>You have successfully applied for this job. You can check the status of your application later by logging into your account at Bestway Career Portal.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div>', {
                header: '',
                position: 'center',
                theme: 'bg-blue',
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
//function DeclarationStatement(id) {
//    $.ajax({
//        type: "GET",
//        url: "/Job/DeclarationStatement",
//        contentType: "application/json; charset=utf-8",
//        data: { JobID: id },
//        datatype: "json",
//        success: function (data) {
//            $('#modelBody1').html(data);
//            $('#myModal1').modal('show');
//        },
//        error: function () {
//            alert("Dynamic content load failed.");
//        }
//    });
//}
function ViewProfileIndex(id, item) {
    if (item<8) {
        $.jGrowl('<div>You have to complete your profile first.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div></strong>', {
            header: '',
            position: 'center',
            theme: 'bg-blue',
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
        cache: false,
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
};