function MiscellaneousGetCreate() {
    $.ajax({
        url: '/Miscellaneous/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PVMiscellaneousIndex').html(result);
    });
}
    //if (document.getElementById('WorkedBefore').checked) {
    //    $("#DivWorkedBefore").show();
    //}
    //else {
    //    $("#DivWorkedBefore").hide();
    ////}
    //$('#WorkedBefore').change(function () {
    //    alert(OK);
    //    if ($(this).is(":checked")) {
    //        $("#DivWorkedBefore").show();
    //    }
    //    else {
    //        $("#DivWorkedBefore").hide();
    //    }
    //});

function LoadPVMiscellaneousIndex(id) {
    $.ajax({
        url: '/Miscellaneous/Create',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {
        $('#PVMiscellaneousIndex').html(result);
    });
};
function SaveMiscellaneousInfoFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Candidate/Create",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data === "OK") {
                    location.reload();
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}