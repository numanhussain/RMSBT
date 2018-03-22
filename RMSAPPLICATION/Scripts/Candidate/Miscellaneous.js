function MiscellaneousGetCreate(id, item) {
    if (item > "6") {
        $("#WorkedBeforeDiv").hide();
        $.ajax({
            url: '/Miscellaneous/Create',
            type: "GET",
            cache: false,
        }).done(function (result) {
            $('#PartialViewContainer').html(result);
        });
    }
    else {
        alert("You have to save reference details first.");
    }
}
function MiscellaneousDetailHide() {
    //Worked Before Change
    $("#WorkedBeforeDiv").hide();
    if (document.getElementById('WorkedBefore').checked) {
        $("#WorkedBeforeDiv").show();
    }
    else {
        $("#WorkedBeforeDiv").hide();
    }
    //Worked Before Selected
    $('#WorkedBefore').click(function () {
        if ($(this).is(":checked")) {
            $("#WorkedBeforeDiv").show();
        }
        else {
            $("#WorkedBeforeDiv").hide();
        }
    });

};
function SaveMiscellaneousInfoFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Miscellaneous/Create",
            data: $("#formEditID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                $('#PartialViewContainer').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}