
export function setLoginBarcode() {
    let docStyle = getComputedStyle(document.documentElement);
    let backPaper = docStyle.getPropertyValue("--back-paper");
    //not response only set once
    let screenWidth = screen.width;
    let bcWidth = 1.5;
    let bcHeight = 50;
    if (screenWidth < 768) {
        bcWidth = 1;
        bcHeight = 40;
    } else if (screenWidth >= 1200) {
        bcWidth = 1.5;
        bcHeight = 60;
    }
    JsBarcode(".barcode", moment().format("yyyyMMDD"), {
        width: bcWidth,
        height: bcHeight,
        fontSize: 12,
        margin: 5,
        background: "transparent",
        lineColor: backPaper,
    });
}
