function CompensationGetCreate() {
    $.ajax({
        url: '/Compensation/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PVCompensationIndex').html(result);
    });
    $('#Bonus').change(function () {
        alert(OK);
        if ($(this).is(":checked")) {
            $("#DivBonus").show();
        }
        else {
            $("#DivBonus").hide();
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
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}