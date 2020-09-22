window.onload = function () {
    let menus = document.querySelectorAll(".side-menu a");
    menus.forEach((menu) => {
        menu.addEventListener("click", function () {
            removeActives();
            menu.parentNode.classList.add("active");
        });
    });

    function removeActives() {
        menus.forEach((menu) => {
            menu.parentNode.classList.remove("active");
        });
    }
}