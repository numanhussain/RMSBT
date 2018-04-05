function CandidateProfile(id) {
    $.ajax({
        type: "GET",
        url: "/Candidate/CandidateProfile",
        contentType: "application/json; charset=utf-8",
        data: { CID: id },
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