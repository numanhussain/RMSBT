function LoadPVSkillDetailIndex(id, item) {
    clearClasses();
    $.ajax({
        url: '/Skill/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liActive");
        $("#hv7").addClass("liInActive");
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
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'bg-success-400',
                    });
                    LoadPVSkillDetailIndex(id)
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
function SkillDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Skill/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    LoadPVSkillDetailIndex(id)
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