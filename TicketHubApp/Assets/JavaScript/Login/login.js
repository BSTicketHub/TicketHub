import { setLoginBarcode } from "./_barcode.js";
import { registerTicketLoginTransition, registerLoginContainers, registerSignUpLogInToggle, registerForgotPasswordToggle, getParameterByName } from "./_loginEvents.js";

window.onload = function () {
    setLoginBarcode();

    registerTicketLoginTransition();

    registerLoginContainers();

    registerSignUpLogInToggle();

    registerForgotPasswordToggle();

    let param = getParameterByName("type");
    if (param == "shop") {
        $("#shop").click();
    }
};
