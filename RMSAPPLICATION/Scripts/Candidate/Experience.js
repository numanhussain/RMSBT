function LoadPVExperienceDetailIndex(id) {
    $.ajax({
        url: '/Experience/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
    });
};
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
                if (data === "OK") {
                    ('#myModal').modal('hide');
                    LoadPVExperienceDetailIndex(id)
                }
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