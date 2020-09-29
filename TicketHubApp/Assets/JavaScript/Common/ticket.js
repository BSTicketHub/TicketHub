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


        //if (tag == '#') {
        //    span.classList.add("tag", "tag-red");
        //} else if (tag == '$') {
        //    span.classList.add("tag", "tag-green");
        //} else if (tag == '%') {
        //    span.classList.add("tag", "tag-blue");
        //} else if (tag == '^') {
        //    span.classList.add("tag", "tag-orange");
        //} else {
        //    span.classList.add("tag", "border", "border-secondary");
        //    span.innerText = value;
        //    return true;
        //}
        //span.innerText = value.slice(1);


    })
}