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
        $("#hv5").addClass("liActive");
        $("#hv6").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        $("#hv8").addClass("liInActive");
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
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'alert-styled-right bg-info',
                        life: 6000
                    });
                    SelfAssessmentGetCreate()
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