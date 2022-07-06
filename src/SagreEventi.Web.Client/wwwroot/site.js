let handler;

window.Connection = {
    Initialize: function (interop) {

        const updateOnlineStatus = function () {
            interop.invokeMethodAsync("Connection.StatusChanged", navigator.onLine);
        }

        window.addEventListener("online", updateOnlineStatus);
        window.addEventListener("offline", updateOnlineStatus);
        updateOnlineStatus();
    },
    Dispose: function () {

        if (updateOnlineStatus != null) {

            window.removeEventListener("online", updateOnlineStatus);
            window.removeEventListener("offline", updateOnlineStatus);
        }
    }
};