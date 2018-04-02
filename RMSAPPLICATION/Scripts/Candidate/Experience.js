function LoadPVExperienceDetailIndex(id, item) {
    if (item > 3) {
        clearClasses();
        $.ajax({
            url: '/Experience/Index',
            type: "GET",
            cache: false,
        }).done(function (result) {
            $('#PartialViewContainer').html(result);
            $("#hv1").addClass("liInActive");
            $("#hv2").addClass("liInActive");
            $("#hv3").addClass("liActive");
            $("#hv4").addClass("liInActive");
            $("#hv5").addClass("liInActive");
            $("#hv6").addClass("liInActive");
            $("#hv33").addClass("liInActive");
            document.getElementById("UserstageAfterFirst").value = 4;
        });
    }
    else {
        alert("You have to save one education detail first.");
    }
};
function clearClasses() {
    $("#hv1").removeClass("liInActive");
    $("#hv2").removeClass("liInActive");
    $("#hv33").removeClass("liInActive");
    $("#hv3").removeClass("liInActive");
    $("#hv4").removeClass("liInActive");
    $("#hv5").removeClass("liInActive");
    $("#hv6").removeClass("liInActive");
    $("#hv1").removeClass("liActive");
    $("#hv2").removeClass("liActive");
    $("#hv33").removeClass("liActive");
    $("#hv3").removeClass("liActive");
    $("#hv4").removeClass("liActive");
    $("#hv5").removeClass("liActive");
    $("#hv6").removeClass("liActive");
}
function ExperienceDetailGetCreate(id) {
    $('#ExperienceGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/Experience/Create",
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
function ExperienceDetailPostCreate(id) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Experience/Create",
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") { location.reload(); }
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
function ExperienceDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/Experience/Edit",
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
function ExperienceDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Experience/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVExperienceDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function ExperienceDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/Experience/Delete",
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
function ExperienceDetailPostDelete(id) {
    $('#btnDeletePost').click(function () {
        $.ajax({
            type: "POST",
            url: "/Experience/Delete",
            data: $("#formDeleteID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVExperienceDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function HaveExperience() {
    $('#HaveExperience').click(function () {
        var id = ($(this).is(":checked"));
        alert(id);
        if ($(this).is(":checked")) {

            $.ajax({
                type: "POST",
                url: "/Experience/SaveExperienceDetail",
                cache: false,
                data: { id: id },
                datatype: "json",
                success: function (data) {
                    $('#myModal').modal('show');
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });
        }
    });
}