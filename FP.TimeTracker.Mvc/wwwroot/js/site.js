// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var webApi = "https://localhost:44355/api/";
function saveEmployeeTimeSheet(parent) {
    var empID = parent.find('.EmployeeID').val();
    var period = parent.find('#Period' + empID).val();
    var timeIn = parent.find('#TimeIn' + empID).val();
    var timeOut = parent.find('#TimeOut' + empID).val();
    var data = {
        EmployeeID: empID,
        DatePeriod: period,
        TimeIn: timeIn,
        TimeOut: timeOut
    };
    var url =  "EmployeeTimeTracker/SaveTimeSheetJson"
   post(data, url)
}

function post(data, url, callback,) {

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function () { alertSuccess("Employee's time sheet has been successfully save."); },
        error: function (xhr, textStatus, errorThrown) { alertErrors(xhr, textStatus, errorThrown, "Error"); },
    });
}

function alertErrors(xhr, textStatus, errorThrown, message) {
    $("#liveAlertPlaceholder").find(".message").html(message);
    $("#liveAlertPlaceholder").removeClass('d-none alert-success').addClass('alert-danger');
}
function alertSuccess(message) {
    $("#liveAlertPlaceholder").find(".message").html(message);
    $("#liveAlertPlaceholder").removeClass(' d-none alert-danger').addClass('alert-success');
}