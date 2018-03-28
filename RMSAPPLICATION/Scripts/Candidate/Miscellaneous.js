 function MiscellaneousGetCreate(id, item) {
        if (item > 5) {
        clearClasses();
            $("#WorkedBeforeDiv").hide();
            $.ajax({
                url: '/Miscellaneous/Create',
                type: "GET",
                cache: false,
            }).done(function (result) {
                $('#PartialViewContainer').html(result);
                $("#hv1").addClass("liInActive");
                $("#hv2").addClass("liInActive");
                $("#hv3").addClass("liInActive");
                $("#hv4").addClass("liInActive");
                $("#hv5").addClass("liInActive");
                $("#hv6").addClass("liActive");
                $("#hv33").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 7;
            });
        }
        else {
            alert("You have to save compensation details first.");
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
 if (data == "OK") { location.reload(); }
                else {
                $('#myModal').modal('hide');
                $('#PartialViewContainer').html(data);
}
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}