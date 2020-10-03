// data table ajax json api
$(document).ready(function () {
    console.log(`${issueid}`)
    $('#datatable').DataTable({
        "ajax": {
            "url": "../getIssueDetailApi",
            "type": "POST",
            "data": { "Id": `${issueid}` }
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
