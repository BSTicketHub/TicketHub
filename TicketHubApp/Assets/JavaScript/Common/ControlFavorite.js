function controlFromFavorite(Icon) {
    console.dir(event.target.nearestViewportElement)
    console.dir(Icon)
    //Icon.setAttribute('data-icon', 'bi:heart')

    $.ajax({
        cache: false,
        url: "../Shop/ToggleFavoriteList",
        method: "post",
        data: {
            ShopId: Icon.getAttributeNode('shopid').value,
            UserId: "@ViewBag.UserId"
        },
        success: function () {
        },
        error: function () {
            alert("failed");
        },
        complete: function () {
            if (Icon.getAttribute('data-icon') == "bi:heart-fill") {
                Icon.setAttribute('data-icon', 'bi:heart')
            } else {
                Icon.setAttribute('data-icon', 'bi:heart-fill')
            }
        }
    });
}