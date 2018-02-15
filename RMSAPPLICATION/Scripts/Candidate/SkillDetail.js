﻿function LoadPVSkillDetailIndex(id) {
    $.ajax({
        url: '/Skill/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {

        $('#PartialViewContainer').html(result);
    });
};
function SkillDetailGetCreate(id) {
    $('#SkillGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/Skill/Create",
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
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
}
function SkillDetailPostCreate(id) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/Skill/Create',
            type: 'POST',
            cache: false,
            data: $("#formCreateID").serialize(),
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
function SkillDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/Skill/Edit",
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
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
}
function SkillDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Skill/Edit",
            data: $("#formEditID").serialize(),
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
function SkillDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/Skill/Delete",
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
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
}

function SkillDetailPostDelete(id) {
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