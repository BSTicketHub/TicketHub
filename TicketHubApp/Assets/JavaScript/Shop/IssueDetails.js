// data table ajax json api
$(document).ready(function () {
    $('#datatable').DataTable({
        "ajax": {
            "url": "getIssueDetailApi",
            "data": { "Id": issueid }
        },
        "scrollX": true,
        "stateSave": true,
        scrollY: 250
    });
});

// modify button click event
document.getElementById("modify").addEventListener("click", function () {
    window.location.assign("EditIssue" + "?id=" + issueid);
});
