function CandidateGetCreate() {
    clearClasses();
    $.ajax({
        url: '/Candidate/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liInActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 2;
    });
};
function clearClasses() {
    $("#hv1").removeClass("liInActive");
    $("#hv2").removeClass("liInActive");
    $("#hv33").removeClass("liInActive");
    $("#hv3").removeClass("liInActive");
    $("#hv4").removeClass("liInActive");
    $("#hv5").removeClass("liInActive");
    $("#hv6").removeClass("liInActive");
    $("#hv7").removeClass("liInActive");
    $("#hv1").removeClass("liActive");
    $("#hv2").removeClass("liActive");
    $("#hv33").removeClass("liActive");
    $("#hv3").removeClass("liActive");
    $("#hv4").removeClass("liActive");
    $("#hv5").removeClass("liActive");
    $("#hv6").removeClass("liActive");
    $("#hv7").addClass("liActive");
}
function SavePersonalInfoFunction() {
    $('#btnPostCreate').click(function () {

        $.ajax({
            url: '/Candidate/Create',
            type: 'POST',
            data: $("#formEditID").serialize(),
            success: function (data) {
                $.jGrowl('You have successfully saved your personal details.Kindly go to to education details.', {
                    header: 'Well done!',
                    theme: 'bg-success-400',
                });
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
function LoadDD() {
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
}
function RedirectToIndex() {
    $.ajax({
        url: '/Candidate/Index',
        type: 'POST',
        success: function (data) {
            $('#myModal').modal('hide');
            $('#PartialViewContainer').html(data);
        },
        error: function () {
            $("#result").text('an error occured')
        }
    });
}
function myFunction() {
    var txt;
    if (confirm("Press a button!")) {
        txt = "You pressed OK!";
    }
    else {
        txt = "You pressed Cancel!";
    }
}
function UpdateAppliedAs() {
    $("#AppliedAs").on('change', function () {
        var id = $(this).val();
        $.ajax({
            url: '/Home/UpdateAppliedAs',
            type: "POST",
            cache: false,
            data: { id: id }
        }).done(function (result) {
        });
    });
}

function ViewProfileIndex(id) {
    $.ajax({
        type: "GET",
        url: "/Candidate/ViewProfileIndex",
        contentType: "application/json; charset=utf-8",
        data: { JobID: id },
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
