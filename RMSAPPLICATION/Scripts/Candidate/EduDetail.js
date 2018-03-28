function LoadPVEduDetailIndex(id, item) {
    if (item > 2) {
        clearClasses();
        $.ajax({
            url: '/EduDetail/Index',
            type: "GET",
            cache: false,
            data: { cid: id }
        }).done(function (result) {
            $('#PartialViewContainer').html(result);
            $("#hv1").addClass("liInActive");
            $("#hv2").addClass("liActive");
            $("#hv3").addClass("liInActive");
            $("#hv4").addClass("liInActive");
            $("#hv5").addClass("liInActive");
            $("#hv6").addClass("liInActive");
            $("#hv33").addClass("liInActive");
            document.getElementById("UserstageAfterFirst").value = 3;
        });
    }
    else {
        alert("You have to Save Personal Details first.");
    }
};
function EduDetailGetCreate(id) {
    $('#EduGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/EduDetail/Create",
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
function EduDetailPostCreate(id) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/EduDetail/Create',
            type: 'POST',
            cache: false,
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK")
                { location.reload(); }
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
function EduDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/EduDetail/Edit",
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
function EduDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/EduDetail/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data == "OK") { location.reload(); }
                else {
                    $('#myModal').modal('hide');
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function EduDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/EduDetail/Delete",
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
function EduDetailPostDelete(id) {
    $('#btnDeletePost').click(function () {
        $.ajax({
            type: "POST",
            url: "/EduDetail/Delete",
            data: $("#formDeleteID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVEduDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
//function DegreeChange() {
//    $("#InstitutionDD").hide();
//    $("#CGPATB").hide();
//    $("#DegreeID").on("change", function () {
//        if ($("#DegreeID option:selected").val() == 3) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 4) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 5) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        if ($("#DegreeID option:selected").val() == 6) {
//            $("#InstitutionDD").show();
//            $("#CGPATB").show();
//        }
//        else {
//            $("#InstitutionDD").hide();
//            $("#CGPATB").hide();
//        }
//  });

//}
function DegreeChange() {
    ShowHide();
    $("#DegreeID").on("change", function () {
        ShowHide();
    });
}
function ShowHide() {
    $("#InstitutionDD").hide();
    $("#CGPATB").hide();
    $("#BoardTB").show();
    switch ($("#DegreeID").val()) {
        case "3":
            $("#InstitutionDD").show();
            $("#CGPATB").show();
            $("#BoardTB").hide();
            break;
        case "4":
            $("#InstitutionDD").show();
            $("#CGPATB").show();
            $("#BoardTB").hide();
            break;
        case "5":
            $("#InstitutionDD").show();
            $("#CGPATB").show();
            $("#BoardTB").hide();
            break;
        case "6":
            $("#InstitutionDD").show();
            $("#CGPATB").show();
            $("#BoardTB").hide();
            break;
    }
}