﻿function MiscellaneousGetCreate(id, item) {
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
        $("#hv6").addClass("liInActive");
        $("#hv7").addClass("liActive");
        $("#hv8").addClass("liInActive");
        document.getElementById("UserstageAfterFirst").value = 7;
    });
}
function LoadWorkingBeforeControlShow(AppliedAs) {
    if (AppliedAs != 1) {
        $("#hidedatediv").show();
        if (document.getElementById('WorkedBeforeCurrentlyWorking').checked) {
            $("#hidedatediv").hide();
        }
        //Currently working  Selected
        $('#WorkedBeforeCurrentlyWorking').click(function () {
            if ($(this).is(":checked")) {
                $("#hidedatediv").hide();
            }
            else {
                $("#hidedatediv").show();
            }

        });
    }
}
function MiscellaneousDetailHide(vmf) {
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
    if (vmf != 1) {
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
    }
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
    $('#NoticeTimetb').hide();
    $('#NoticeTimeID').change(function () {
        if ($("#HearAboutJobID").val() != "0") {
            $("#NoticeTimetb").show();
        } else {
            $("#NoticeTimetb").hide();
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
    if (vmf != 1) {
        if (document.getElementById('InterviewedBefore').value == "Yes") {
            $("#InterviewedBeforeDetailtb ").show();
        }
        else {
            $("#InterviewedBeforeDetailtb").hide();
        }
        if (document.getElementById('WorkedBefore').value == "Yes") {
            $("#WorkedBeforeDetailtb ").show();
        }
        else {
            $("#WorkedBeforeDetailtb").hide();
        }
    }
    if (document.getElementById('Disability').value == "Yes") {
        $("#DisabilityDetailtb ").show();
    }
    else {
        $("#DisabilityDetailtb").hide();
    }
    if (document.getElementById('HearAboutJobID').value == "8") {
        $("#HearAboutDetailtb ").show();
    }
    else {
        $("#HearAboutDetailtb").hide();
    }
}
function SaveMiscellaneousInfoFunction() {
    $.ajax({
        type: "POST",
        url: "/Miscellaneous/Create",
        data: $("#formEditID").serialize(),
        success: function (data) {
            if (data === "OK") {
                SaveCV();
                MiscellaneousGetCreate();
            }
            else {
                $('#PartialViewContainer').html(data);
            }
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}
function SaveCV() {
    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#CVUpload").get(0);

        //Get name of CV
        var files = fileUpload.files;
        //Check extension of CV
    }
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
            $.jGrowl('<div>Welcome to Bestway Cement!</div><div>You have successfully created your profile. This is your first step towards prospective job opportunities. We appreciate your interest in Bestway Cement.</div><div>Regards:</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited </div>', {
                header: '',
                position: 'center',
                theme: 'alert-styled-right bg-info',
                life: 7000
            });
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}
function checkextension() {
    var a = 0;
    //binds to onchange event of your input field
    var ext = $('#CVUpload').val().split('.').pop().toLowerCase();
    if (ext == "docx" || ext == "doc" || ext == "pdf" || ext == "jpg" || ext == "") {
        SaveCV();
    }
    else {
        $.jGrowl('<div>Invalid CV formate.</div>', {
            header: '',
            position: 'center',
            theme: 'alert-styled-right bg-info',
            life: 4000
        });
    }
}

function HasCV(HasCV) {
    //Hide cv div
    if (HasCV == null) {
        $("#resumehide").show();
    }
    if (HasCV == "True") {
        $("#resumehide").hide();
    }
    $('#UploadCV').click(function () {
        if ($(this).is(":checked")) {
            $("#resumehide").show();
        }
        else {
            $("#resumehide").hide();
        }
    });
}

function MiscellaneousDateLoad() {
    var date_inputMiscellaneousInterviewDate = $('input[name="InterviewedDate"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_inputMiscellaneousInterviewDate.datepicker({
        format: 'dd-MM-yyyy',
        orientation: 'bottom',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
    var date_inputMiscellaneousJoiningDate = $('input[name="DateJoining"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_inputMiscellaneousJoiningDate.datepicker({
        format: 'dd-MM-yyyy',
        orientation: 'bottom',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
    var date_inputMiscellaneousLeavingDate = $('input[name="DateLeavig"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_inputMiscellaneousLeavingDate.datepicker({
        format: 'dd-MM-yyyy',
        orientation: 'bottom',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
}

function CheckCVAttached(HasCV) {
    $('#btnPostCreate').click(function () {
        if (HasCV !== "True") {
            var fileUpload = $("#CVUpload").get(0);
            if (fileUpload.files.length === 0) {
                // Display Message
                alert("Please attach CV.");
            }
            else {
                var ext = $('#CVUpload').val().split('.').pop().toLowerCase();
                if (ext === "docx" || ext === "doc" || ext === "pdf" || ext === "jpg" || ext === "") {
                    SaveMiscellaneousInfoFunction();
                }
                else {
                    alert("Please Select valid file extension.");
                }
            }
        }
        else {
            SaveMiscellaneousInfoFunction();
        }
    });
}
function ValidateSize(file) {
    var FileSize = file.files[0].size / 1024 / 1024; // in MB
    if (FileSize > 1) {
        alert('File size exceeds 1 MB');
        // $(file).val(''); //for clearing with Jquery
    }
    var ext = $('#CVUpload').val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['pdf', 'doc', 'docs', 'jpg']) === -1) {
        alert('Invalid  file extension');
    }
}


