function LoadPVSkillDetailIndex(id) {
    $.ajax({
        url: '/Skill/Index',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {

        $('#PVSkillDetailIndex').html(result);
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
function SkillDetailPostCreate() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Skill/Create",
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
function SkillDetailPostEdit() {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Skill/Edit",
            contentType: "application/json; charset=utf-8",
            data: $("#formEditID").serialize(),
            datatype: "json",
            success: function (data) {
                $('#myModal').modal('hide');
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