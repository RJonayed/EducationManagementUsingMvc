$(document).ready(function () {
    $("#btnLoadReport").click(function () {
        ReportManager.LoadReport();
    });
});
var ReportManager = {
    LoadReport: function ()
    {
        var jsonParam = "";
        var serviceUrl = "Admissions/GenerateEmployeeReport";
        ReportManager.GetReport(serviceUrl, jsonParam, onFailed);
        function onFailed(error) { alert(error); }
    },
    GetReport: function (serviceUrl, jsonParam, errorCallback) {
        $.ajax({
            url: serviceUrl,
            async: false,
            type: "POST",
            data: "{" + jsonParam + "}",
            contentType: "application/json; charset=utf-8",
            success: function () {
                window.open('../Report/ReportViewer.aspx', '_newtab')
            },
            error: errorCallback
        })
    }
}
