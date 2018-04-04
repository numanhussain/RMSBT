function OpenPartialView(myUrl) {
    $.ajax({
        url: myUrl,
        type: 'GET',
        cache: false,
        success: function (data) {
            $('#DivContainer').html(data);
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
            $.jGrowl('You have successfully  Applied for this job.', {
                header: 'Well done!',
                theme: 'bg-success-400',
            });
        }
        else { alert(data); }
    });
}
function DeclarationStatement(id) {
    $.ajax({
        type: "GET",
        url: "/Job/DeclarationStatement",
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
}
