function MiscellaneousGetCreate() {
    $('#WorkedBeforeDiv').hide();
    if ($("#WorkedBefore").is(":checked") === true) {
        $('#WorkedBeforeDiv').show();
    }
    else {
        $('#WorkedBeforeDiv').hide();
    }
    $('#WorkedBefore').change(function () {
        if ($(this).is(':checked') === true) {
            $('#WorkedBeforeDiv').show();
        }
        else {
            $('#WorkedBeforeDiv').hide();
        }
    });
    $.ajax({
        url: '/Miscellaneous/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PVMiscellaneousIndex').html(result);
    });
}
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