function SelfAssessmentGetCreate() {
    clearClasses();
    $.ajax({
        url: '/SelfAssessment/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liActive");
    });
};
function SelfAssessmentPostCreate(item) {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/SelfAssessment/Index',
            type: 'POST',
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") {
                        $.jGrowl('<div>Welcome to Bestway!</div><div>You have successfully created your profile. This is your first step towards prospective job opportunities. We appreciate your interest in Bestway.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div>', {
                            header: 'Well done!',
                            theme: 'bg-success-400',
                        });
                    SelfAssessmentGetCreate(id)
                }
                else {
                    $('#PartialViewContainer').html(data);
                }
            },
            error: function () {
                $("#result").text('an error occured')
            }
        });
    });
}