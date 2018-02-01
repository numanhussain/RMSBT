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