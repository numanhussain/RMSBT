function CandidateGetCreate() {
    $.ajax({
        url: '/Candidate/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PVCandidateIndex').html(result);
    });
};

function LoadPVCandidateIndex(id) {
    $.ajax({
        url: '/Candidate/Create',
        type: "GET",
        cache: false,
        data: { cid: id }
    }).done(function (result) {
        $('#PVCandidateIndex').html(result);
    });
};
function SavePersonalInfoFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            type: "POST",
            url: "/Candidate/Create",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data === "OK") {
                    location.reload();
                }
                else {

                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        var formData = new FormData();
        var empid = document.getElementById("CandidateID").value;
        alert(empid);
        var totalFiles = document.getElementById("imgupload").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var _file = document.getElementById("imgupload").files[i];
            formData.append("imgupload", _file);
        }
        formData.append("CID", empid);
        // for Image
        $.ajax({
            url: '/Candidate/CandidateImage',
            type: 'POST',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function () {
            },
            error: function () {
                $("#result").text('an error occured')
            }
        });
    });
}