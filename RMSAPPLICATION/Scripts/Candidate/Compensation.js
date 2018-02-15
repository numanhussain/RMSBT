function CompensationGetCreate(id) {
    $.ajax({
        url: '/Compensation/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
    });
}
function CompensationDetailHide() {
    //Worked Before Change
    $("#Bonusdiv").hide();
    if (document.getElementById('Bonus').checked) {
        $("#Bonusdiv").show();
    }
    else {
        $("#Bonusdiv").hide();
    }
    //Worked Before Selected
    $('#Bonus').click(function () {
        if ($(this).is(":checked")) {
            $("#Bonusdiv").show();
        }
        else {
            $("#Bonusdiv").hide();
        }
    });
 //LFA  Change
    $("#LFAdiv").hide();
    if (document.getElementById('LFA').checked) {
        $("#LFAdiv").show();
    }
    else {
        $("#LFAdiv").hide();
    }
    //LFA Selected
    $('#LFA').click(function () {
        if ($(this).is(":checked")) {
            $("#LFAdiv").show();
        }
        else {
            $("#LFAdiv").hide();
        }
    });
//OT  Change
    $("#OTdiv").hide();
    if (document.getElementById('OT').checked) {
        $("#OTdiv").show();
    }
    else {
        $("#OTdiv").hide();
    }
    //OT Selected
    $('#OT').click(function () {
        if ($(this).is(":checked")) {
            $("#OTdiv").show();
        }
        else {
            $("#OTdiv").hide();
        }
    });
//Provident  Change
    $("#Providentdiv").hide();
    if (document.getElementById('ProvidentFund').checked) {
        $("#Providentdiv").show();
    }
    else {
        $("#Providentdiv").hide();
    }
    //Provident Selected
    $('#ProvidentFund').click(function () {
        if ($(this).is(":checked")) {
            $("#Providentdiv").show();
        }
        else {
            $("#Providentdiv").hide();
        }
    });
//Gratuity Change
    $("#Gratuitydiv").hide();
    if (document.getElementById('Gratuity').checked) {
        $("#Gratuitydiv").show();
    }
    else {
        $("#Gratuitydiv").hide();
    }
    //Provident Selected
    $('#Gratuity').click(function () {
        if ($(this).is(":checked")) {
            $("#Gratuitydiv").show();
        }
        else {
            $("#Gratuitydiv").hide();
        }
    });
};
function SaveCompensationFunction() {
    $('#btnPostCreate').click(function () {
        $.ajax({
            url: '/Compensation/Create',
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
    });
}