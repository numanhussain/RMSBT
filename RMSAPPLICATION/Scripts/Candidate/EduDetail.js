function LoadPVEduDetailIndex(id) {
    $.ajax({
        url: '/EduDetail/Index',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {
        $('#PVEduDetailIndex').html(result);
    });
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
function EduDetailPostCreate(){
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/EduDetail/Create",
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
            error: function (){
                alert("Dynamic content load failed.");
            }
        });
    });
}
function EduDetailGetEdit(id){
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
function EduDetailPostEdit(){
    $('#btnPostEdit').click(function (){
        $.ajax({
            type: "POST",
            url: "/EduDetail/Edit",
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
function EduDetailGetDelete(id){
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