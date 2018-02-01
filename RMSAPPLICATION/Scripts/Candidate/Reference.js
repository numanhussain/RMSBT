function LoadPVReferenceDetailIndex(id) {
        $.ajax({
            url: '/Reference/Index',
            type: "GET",
            cache: false,
            data: { cid: id }
        }).done(function (result) {
            $('#PVReferenceDetailIndex').html(result);
        });
};
function ReferenceDetailGetCreate(id) {
    $('#ReferenceGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/Reference/Create",
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
function ReferenceDetailPostCreate() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Reference/Create",
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data === "OK") {
                    $('#myModal').modal('hide');
                    location.reload();
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
function ReferenceDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/Reference/Edit",
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
function ReferenceDetailPostEdit() {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Reference/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data === "OK") {

                    $('#myModal').modal('hide');
                    location.reload();
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
function ReferenceDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/Reference/Delete",
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