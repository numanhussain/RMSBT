//function LoadPVReferenceDetailIndex(id, item) {
//    clearClasses();
//    $.ajax({
//        url: '/Reference/Index',
//        type: "GET",
//        cache: false,
//    }).done(function (result) {
//        $("#hv1").addClass("liInActive");
//        $("#hv2").addClass("liInActive");
//        $("#hv3").addClass("liInActive");
//        $("#hv4").addClass("liInActive");
//        $("#hv5").addClass("liActive");
//        $("#hv6").addClass("liInActive");
//        $("#hv33").addClass("liInActive");
//        $("#hv7").addClass("liInActive");
//        $('#PartialViewContainer').html(result);
//    });
//};
//function ReferenceDetailGetCreate(id) {
//    $('#ReferenceGetCreate').click(function () {
//        $.ajax({
//            type: "GET",
//            url: "/Reference/Create",
//            contentType: "application/json; charset=utf-8",
//            data: { "id": id },
//            datatype: "json",
//            success: function (data) {
//                $('#modelBody').html(data);
//                $('#myModal').modal('show');
//            },
//            error: function () {
//                alert("Dynamic content load failed.");
//            }
//        });
//    });
//}
function ReferenceDetailPostCreate(id) {
   $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Reference/Create",
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $.jGrowl('You have successfully saved your details.', {
                    header: '',
                    position: 'center',
                    theme: 'bg-blue',
                    life:6000
                    });
                    ReferenceGetCreate(id)
                }
                else {
                    $('#PartialViewContainer').html(data);
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
//function ReferenceDetailPostCreate2(id) {
//    $('#btnPostCreate2').click(function () {
//        $.ajax({
//            type: "POST",
//            url: "/Reference/Create",
//            data: $("#formCreateID").serialize(),
//            success: function (data) {
//                if (data == "OK") {
//                    $('#myModal').modal('hide');
//                    $.jGrowl('<div>Welcome to Bestway!</div><div>You have successfully created your profile. This is your first step towards prospective job opportunities. We appreciate your interest in Bestway.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div>', {
//                        header: '',
//                        position: 'center',
//                        theme: 'bg-success-400',
//                    });
//                    LoadPVReferenceDetailIndex(id)
//                }
//                else {
//                    $('#modelBody').html(data);
//                }
//            },
//            error: function () {
//                alert("Dynamic content load failed.");
//            }
//        });
//    });
//}
//function ReferenceDetailGetEdit(id) {
//    $.ajax({
//        type: "GET",
//        url: "/Reference/Edit",
//        contentType: "application/json; charset=utf-8",
//        data: { "id": id },
//        datatype: "json",
//        success: function (data) {
//            $('#modelBody').html(data);
//            $('#myModal').modal('show');
//        },
//        error: function () {
//            alert("Dynamic content load failed.");
//        }
//    });
//}
//function ReferenceDetailPostEdit(id) {
//    $('#btnPostEdit').click(function () {
//        $.ajax({
//            type: "POST",
//            url: "/Reference/Edit",
//            data: $("#formEditID").serialize(),
//            success: function (data) {
//                if (data == "OK") {
//                    $('#myModal').modal('hide');
//                    LoadPVReferenceDetailIndex(id)
//                }
//                else {
//                    $('#modelBody').html(data);
//                }
//            },
//            error: function () {
//                alert("Dynamic content load failed.");
//            }
//        });
//    });
//}
//function ReferenceDetailGetDelete(id) {
//    $.ajax({
//        type: "GET",
//        url: "/Reference/Delete",
//        contentType: "application/json; charset=utf-8",
//        data: { "id": id },
//        datatype: "json",
//        success: function (data) {
//            $('#modelBody').html(data);
//            $('#myModal').modal('show');

//        },
//        error: function () {
//            alert("Dynamic content load failed.");
//        }
//    });
//}
//function ReferenceDetailPostDelete(id) {
//    $('#btnPostDelete').click(function () {
//        $.ajax({
//            type: "POST",
//            url: "/Reference/Delete",
//            data: $("#formDeleteID").serialize(),
//            success: function (data) {
//                $('#myModal').modal('hide');
//                LoadPVReferenceDetailIndex(id)
//            },
//            error: function () {
//                alert("Dynamic content load failed.");
//            }
//        });
//    });
//}
function ReferenceGetCreate(id, item) {
    clearClasses();
    $.ajax({
        url: '/Reference/Create',
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
        $("#hv7").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 7;
    });
}