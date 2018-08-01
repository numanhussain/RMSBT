function OpenNotification() {
    $.ajax({
        type: "GET",
        url: '/Notification/GetSystemNotification',
        success: function (response) {
            $("#DivNotifications").append(response.Notification);
            document.getElementById("divNotificationCount").innerHTML = response.NotificationCount;
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function CheckSession() {
    $.ajax({
        type: "GET",
        url: '/Home/SessionInfo',
    }).done(function (data) {
        if (data === true) {
            window.location.href = '/Home/Login';
        }
    }).fail(function (e) {
        alert('Error');
    });
}