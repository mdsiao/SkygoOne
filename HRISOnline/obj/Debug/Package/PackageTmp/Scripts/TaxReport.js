$("document").ready(function () {
    $('#tblReports').dataTable({
        "columnDefs": [{
            "targets": [2],
            "visible": true,
            "searchable": false,
            "orderable": false
        }],
        aaSorting: [[0, 'desc']]
    });
});

function GenerateReport(strParam, strReportName) {
    //alert(strParam + "|" + strReportName);

    var win = window.open("TaxReport/ViewReport/" + "" + strParam + "", "_blank");
    win.document.title = strReportName;
}