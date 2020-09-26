compileTag();
function compileTag() {
    var showtag = document.querySelectorAll(".showTag span");
    showtag.forEach(function (span) {
        span.classList.add("badge", "badge-pill", "mr-2", "my-1", "p-2");
        var value = span.innerText;
        var tag = value.charAt(0);
        if (tag == '#') {
            span.classList.add("bg-danger", "text-white");
        } else if (tag == '$') {
            span.classList.add("bg-success", "text-white");
        } else if (tag == '%') {
            span.classList.add("bg-primary", "text-white");
        } else if (tag == '^') {
            span.classList.add("bg-info", "text-white");
        } else {
            span.classList.add("bg-secondary", "text-white", "border");
            span.innerText = value;
            return true;
        }
        span.innerText = value.slice(1);
    })
}
