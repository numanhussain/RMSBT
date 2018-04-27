function CompensationGetCreate(id, item) {
    clearClasses();
    $.ajax({
        url: '/Compensation/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 5;
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
    $("#buybackdiv").hide();
    if (document.getElementById('BuyBackOption').checked) {
        $("#buybackdiv").show();
    }
    else {
        $("#buybackdiv").hide();
    }
    //LFA Selected
    $('#BuyBackOption').click(function () {
        if ($(this).is(":checked")) {
            $("#buybackdiv").show();
        }
        else {
            $("#buybackdiv").hide();
        }
    }); $("#fooddiv").hide();
    if (document.getElementById('Food').checked) {
        $("#fooddiv").show();
    }
    else {
        $("#fooddiv").hide();
    }
    //LFA Selected
    $('#Food').click(function () {
        if ($(this).is(":checked")) {
            $("#fooddiv").show();
        }
        else {
            $("#fooddiv").hide();
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
                if (data == "OK") {
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'bg-success-400',
                        life: 6000
                    });
                    CompensationGetCreate(id)
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
function FormControlsScriptEditCompensation(model) {
    if (model.BBonus == true) {
        $('input:radio[id=BBonus]').prop('checked', true);
    }
    if (model.GBonus == true) {
        $('input:radio[id=GBonus]').prop('checked', true);
    }
    if (model.BLFA == true) {
        $('input:radio[id=BLFA]').prop('checked', true);
    }
    if (model.GLFA == true) {
        $('input:radio[id=GLFA]').prop('checked', true);
    }
    if (model.BOT == true) {
        $('input:radio[id=BOT]').prop('checked', true);
    }
    if (model.GOT == true) {
        $('input:radio[id=GOT]').prop('checked', true);
    }
    if (model.BProvidentFund == true) {
        $('input:radio[id=BProvidentFund]').prop('checked', true);
    }
    if (model.GProvidentFund == true) {
        $('input:radio[id=GProvidentFund]').prop('checked', true);
    }
    if (model.BGratuity == true) {
        $('input:radio[id=BGratuity]').prop('checked', true);
    }
    if (model.GGratuity == true) {
        $('input:radio[id=GGratuity]').prop('checked', true);
    }
    if (model.Free == true) {
        $('input:radio[id=Free]').prop('checked', true);
    }
    if (model.Subsidized == true) {
        $('input:radio[id=Subsidized]').prop('checked', true);
    }
}