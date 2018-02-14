function CompensationGetCreate() {
    $.ajax({
        url: '/Compensation/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PVCompensationIndex').html(result);
    });
    $('#DivHide').hide();
    if ($("#Bonus").is(":checked") === true) {
        $('#DivHide').show();
    }
    else {
        $('#DivHide').hide();
    }
    // Has Version Changed
    $('#Bonus').change(function () {
        if ($(this).is(':checked') === true) {
            $('#DivHide').show();
        }
        else {
            $('#DivHide').hide();
        }
    });
}
function LoadPVCompensationIndex(id) {
    $.ajax({
        url: '/Compensation/Create',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {
        $('#PVCompensationIndex').html(result);
    });
};
function SaveCompensationFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Compensation/Create",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data === "OK") {
                    location.reload();
                }
                $('#PVCompensationIndex').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}