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
        document.getElementById("UserstageAfterFirst").value = 2;
        });
};
function SavePersonalInfoFunction() {
    $('#btnPostCreate').click(function () {
        
        $.ajax({
            url: '/Candidate/Create',
            type: 'POST',
            data: $("#formEditID").serialize(),
            success: function (data, stage) {
                if (data == "OK") {location.reload(); }
                else {
                    $('#PartialViewContainer').html(data);
                }
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