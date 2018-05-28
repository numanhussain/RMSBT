function LoadPVExperienceDetailIndex(id, item) {
    clearClasses();
    $.ajax({
        url: '/Experience/Index',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 4;
    });
};
function ExperienceDetailGetCreate(id) {
    $('#ExperienceGetCreate').click(function () {
        $.ajax({
            type: "GET",
            url: "/Experience/Create",
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
function ExperienceDetailPostCreate(id) {
    $('#btnPostCreate2').click(function () {
        $.ajax({
            url: '/Experience/Create',
            type: 'POST',
            cache: false,
            data: $("#formCreateID2").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    LoadPVExperienceDetailIndex(id)
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'alert-styled-right bg-info',
                        life: 6000
                    });
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
function ExperienceDetailGetEdit(id) {
    $.ajax({
        type: "GET",
        url: "/Experience/Edit",
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
function ExperienceDetailPostEdit(id) {
    $('#btnPostEdit').click(function () {
        $.ajax({
            type: "POST",
            url: "/Experience/Edit",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $('#myModal').modal('hide');
                    LoadPVExperienceDetailIndex(id)
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
function ExperienceDetailGetDelete(id) {
    $.ajax({
        type: "GET",
        url: "/Experience/Delete",
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
function ExperienceDetailPostDelete(id) {
    $('#btnDeletePost').click(function () {
        $.ajax({
            type: "POST",
            url: "/Experience/Delete",
            data: $("#formDeleteID").serialize(),
            success: function (data) {
                $('#myModal').modal('hide');
                LoadPVExperienceDetailIndex(id)
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function LoadExperienceRadioButtons(val, id) {
    if (val == "True") {
        $('input:radio[id=HaveExperienceYes]').prop('checked', true);
    } else {
        $('input:radio[id=HaveExperienceNo]').prop('checked', true);
    }
    $('#HaveExperienceYes').click(function () {

        var val = "";
        var radio = document.getElementById('HaveExperienceYes');
        val = true;
        $.ajax({
            type: "POST",
            url: "/Experience/SaveExperienceDetail",
            cache: false,
            data: { HaveExperience: val },
            datatype: "json",
            success: function (data) {
                if (data == "OK") {
                    LoadPVExperienceDetailIndex(id)
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
    $('#HaveExperienceNo').click(function () {

        var val = "";
        var radio = document.getElementById('HaveExperienceNo');
        val = false;
        $.ajax({
            type: "POST",
            url: "/Experience/SaveExperienceDetail",
            cache: false,
            data: { HaveExperience: val },
            datatype: "json",
            success: function (data) {
                if (data == "OK") {
                    LoadPVExperienceDetailIndex(id)
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
function HaveExperience() {
    $('#HaveExperience').click(function () {
        var id = false;
        if ($(this).is(":checked")) {
            id = true;
            alert("Ok")
        }
        else {
            id = false;
        }
        $.ajax({
            type: "POST",
            url: "/Experience/SaveExperienceDetail",
            cache: false,
            data: { HaveExperience: id },
            datatype: "json",
            success: function (data) {
                if (data == "OK") {
                    LoadPVExperienceDetailIndex(id)

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
function FormControlsScriptEditExperience(ContactEmployerYes, ContactEmployerNo) {
    if (ContactEmployerYes == "YES") {
        $('input:radio[id=ContactEmployerYes]').prop('checked', true);
    } if (ContactEmployerNo == "NO") {
        $('input:radio[id=ContactEmployerNo]').prop('checked', true);
    }
}
function LoadDD2() {

    //Load City
    $('#CityID').empty();
    var convalue = $('#selectedCountryIDHidden').val();
    var URL = '/Candidate/CityList';
    $.getJSON(URL + '/' + convalue, function (data) {
        var items;
        if (document.getElementById("selectedCityIdHidden").value != null)
            var selectedItemID = document.getElementById("selectedCityIdHidden").value;

        $.each(data, function (i, state) {
            if (state.Value == selectedItemID)
                items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#CityID').html(items);
    });
    //Selection City:
    $("#CountryID").on('change', function () {
        $('#CityID').empty();
        var convalue = $('#CountryID').val();
        var URL = '/Candidate/CityList';
        //var URL = '/Emp/LocationList';
        $.getJSON(URL + '/' + convalue, function (data) {
            var items;
            if (document.getElementById("selectedCityIdHidden").value != null)
                var selectedItemID = document.getElementById("selectedCityIdHidden").value;

            $.each(data, function (i, state) {
                if (state.Value == selectedItemID)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#CityID').html(items);
        });


    });
    ShowCityHide2();
    $("#CountryID").on("change", function () {
        ShowCityHide2();
    });
}
function PostIndex(id) {
    $('#btnPostCreate').click(function () {

        $.ajax({
            url: '/Experience/IndexSubmitt',
            type: 'POST',
            data: $("#formCreateID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'bg-blue',
                        life: 6000
                    });
                    LoadPVExperienceDetailIndex(id)
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
function PostIndex2() {
    $("#Experience").change(function () {
        var Experience = $('#Experience').val();
        alert(Experience);
        $.ajax({
            url: "/Experience/SaveOverallExperience",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: { Experience: Experience },
            success: function (data) {
                LoadPVExperienceDetailIndex(id)
            },
            error: function () {
                $("#result").text('an error occured')
            }
        });
    });
}
function ShowCityHide2() {
    $("#OtherCityDivHide2").hide();
    if ($("#CountryID").val() == 74) {
        $("#CityDivHide2").show();
        $("#OtherCityDivHide2").hide();
    }
    else {
        $("#CityDivHide2").hide();
        $("#OtherCityDivHide2").show();
    }
}