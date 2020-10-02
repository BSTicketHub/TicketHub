compileTag();
function compileTag() {
    var showtag = document.querySelectorAll(".showTag span");
    showtag.forEach(function (span) {
        var value = span.innerText;
        var tag = value.charAt(0);
        switch (tag) {
            case '#':
                span.classList.add("tag", "tag-red");
                span.innerText = value.slice(1);
                break;
            case '$':
                span.classList.add("tag", "tag-green");
                span.innerText = value.slice(1);
                break;
            case '%':
                span.classList.add("tag", "tag-blue");
                span.innerText = value.slice(1);
                break;
            case '^':
                span.classList.add("tag", "tag-orange");
                span.innerText = value.slice(1);
                break;
            default:
                span.classList.add("tag", "border", "border-secondary");
                span.innerText = value.slice(0);
                break;
        }
    })
}