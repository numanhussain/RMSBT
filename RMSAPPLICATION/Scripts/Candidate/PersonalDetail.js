function CandidateGetCreate() {
    $.ajax({
        url: '/Candidate/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
    });
};
function SavePersonalInfoFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/Candidate/Create',
            type: 'POST',
            data: $("#formEditID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                $('#PartialViewContainer').html(data);
            },
            error: function () {
                $("#result").text('an error occured')
            }
        });
        var formData = new FormData();
        var empid = document.getElementById("CandidateID").value;
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