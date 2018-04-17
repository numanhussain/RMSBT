function MiscellaneousGetCreate(id, item) {
    clearClasses();
    $("#WorkedBeforeDiv").hide();
    $.ajax({
        url: '/Miscellaneous/Create',
        type: "GET",
        cache: false,
    }).done(function (result) {
        $('#PartialViewContainer').html(result);
        $("#hv1").addClass("liInActive");
        $("#hv2").addClass("liInActive");
        $("#hv3").addClass("liInActive");
        $("#hv4").addClass("liInActive");
        $("#hv5").addClass("liInActive");
        $("#hv6").addClass("liActive");
        $("#hv33").addClass("liInActive");
        $("#hv7").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 7;
    });
}
function MiscellaneousDetailHide() {
    $('#CriminalDetailtb').hide();
    $('#CrimanalRecord').change(function () {
        if ($("#CrimanalRecord").val() == "Yes") {
            $("#CriminalDetailtb").show();
        } else {
            $("#CriminalDetailtb").hide();
        }
    });
    $('#WorkingRelativeDetailtb').hide();
    $('#WorkingRelative').change(function () {
        if ($("#WorkingRelative").val() == "Yes") {
            $("#WorkingRelativeDetailtb").show();
        } else {
            $("#WorkingRelativeDetailtb").hide();
        }
    });
    $('#InterviewedBeforeDetailtb').hide();
    $('#InterviewedBefore').change(function () {
        if ($("#InterviewedBefore").val() == "Yes") {
            $("#InterviewedBeforeDetailtb").show();
        } else {
            $("#InterviewedBeforeDetailtb").hide();
        }
    });
    $('#WorkedBeforeDetailtb').hide();
    $('#WorkedBefore').change(function () {
        if ($("#WorkedBefore").val() == "Yes") {
            $("#WorkedBeforeDetailtb").show();
        } else {
            $("#WorkedBeforeDetailtb").hide();
        }
    });
    $('#DisabilityDetailtb').hide();
    $('#Disability').change(function () {
        if ($("#Disability").val() == "Yes") {
            $("#DisabilityDetailtb").show();
        } else {
            $("#DisabilityDetailtb").hide();
        }
    });
    $('#HearAboutDetailtb').hide();
    $('#HearAboutJobID').change(function () {
        if ($("#HearAboutJobID").val() == "8") {
            $("#HearAboutDetailtb").show();
        } else {
            $("#HearAboutDetailtb").hide();
        }
    });


    //get script of above when value is saved
    if (document.getElementById('WorkingRelative').value == "Yes") {
        $("#WorkingRelativeDetailtb ").show();
    }
    else {
        $("#WorkingRelativeDetailtb").hide();
    }
    if (document.getElementById('CrimanalRecord').value == "Yes") {
        $("#CriminalDetailtb ").show();
    }
    else {
        $("#CriminalDetailtb").hide();
    }
    if (document.getElementById('InterviewedBefore').value == "Yes") {
        $("#InterviewedBeforeDetailtb ").show();
    }
    else {
        $("#InterviewedBeforeDetailtb").hide();
    }
    if (document.getElementById('Disability').value == "Yes") {
        $("#DisabilityDetailtb ").show();
    }
    else {
        $("#DisabilityDetailtb").hide();
    }
    if (document.getElementById('WorkedBefore').value == "Yes") {
        $("#WorkedBeforeDetailtb ").show();
    }
    else {
        $("#WorkedBeforeDetailtb").hide();
    }
    if (document.getElementById('HearAboutJobID').value == "8") {
        $("#HearAboutDetailtb ").show();
    }
    else {
        $("#HearAboutDetailtb").hide();
    }
}
function SaveMiscellaneousInfoFunction(id) {
    $('#btnPostCreate').click(function () {
        SaveCV();
        $.ajax({
            type: "POST",
            url: "/Miscellaneous/Create",
            data: $("#formEditID").serialize(),
            success: function (data) {
                if (data == "OK") {
                    $.jGrowl('You have successfully saved your details.', {
                        header: '',
                        position: 'center',
                        theme: 'bg-success-400',
                    });
                    MiscellaneousGetCreate(id)
                }
                else {
                    $('#PartialViewContainer').html(data);
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
}
function SaveCV() {
    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        var fileUpload = $("#CVUpload").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        var empid = document.getElementById("CandidateID").value;
        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object  
        fileData.append('CandidateID', empid);
        $.ajax({
            url: '/Miscellaneous/UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                alert(result);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}



