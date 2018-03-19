function OpenPartialView(myUrl) {
    $.ajax({
        url: myUrl,
        type: 'GET',
        cache: false,
        success: function (data) {
            $('#DivContainer').html(data);
        },
        error: function () {
            $("#result").text('an error occured')
        }
    });
}
function SelectAllCheckBox() {
    var checkboxes = $(':checkbox');

    //checkboxes.prop('checked', true);
    // Select/Deselect for Employee
    $("#chkSelectAll").bind("change", function () {
        $(".chkSelect").prop("checked", $(this).prop("checked"));
    });
    $(".chkSelect").bind("change", function () {
        if (!$(this).prop("checked"))
            $("#chkSelectAll").prop("checked", false);
    });
    var checkboxes = $(':checkbox');

    //checkboxes.prop('checked', true);
    // Select/Deselect for Employee
    $("#chkSelectAllCatagory").bind("change", function () {
        $(".chkCatagorySelect").prop("checked", $(this).prop("checked"));
    });
    $(".chkCatagorySelect").bind("change", function () {
        if (!$(this).prop("checked"))
            $("#chkSelectAllCatagory").prop("checked", false);
    });
}
function ApplyJob(id) {
    $.ajax({
        url: '/Job/JobApply',
        type: "POST",
        cache: false,
        data: { JobID: id }
    }).done(function (data) {
        if (data === "OK") {
            $.jGrowl('You have successfully  Applied for this job.', {
                header: 'Well done!',
                theme: 'bg-success-400'
            });
        }
    });
};
//function FilterSearch() {
//    SelectAllCheckBox();
//    $('#btnSave').click(function () {
//        $.ajax({
//            url: '/Job/Index',
//            type: 'POST',
//            cache: false,
//            data: $("#frmJobIndex").serialize(),
//            success: function (data) {
//                $('#DivContainer').html(data);
//            },
//            error: function () {
//                $("#result").text('an error occured')
//            }
//        });
//    });
//}