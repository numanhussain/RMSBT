﻿function LoadPVReferenceDetailIndex(id, item) {
    clearClasses();
    $.ajax({
        url: '/Reference/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        $('#PartialViewContainer').html(result);
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
function ReferenceDetailPostCreate(id) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Reference/Create",
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") { location.reload(); }
                else {
                    $('#myModal').modal('hide');
                    LoadPVReferenceDetailIndex(id)
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
                $('#myModal').modal('hide');
                LoadPVReferenceDetailIndex(id)
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
function ReferenceDetailPostDelete(id) {
    $('#btnPostDelete').click(function () {
        $.ajax({
            type: "POST",
            url: "/Skill/Delete",
            data: $("#formDeleteID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVSkillDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}