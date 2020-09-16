import { setLoginBarcode } from "./_barcode.js";
import { registerTicketLoginTransition, registerLoginContainers, registerSignUpLogInToggle, registerForgotPasswordToggle } from "./_loginEvents.js";

window.onload = function () {
    setLoginBarcode();

    registerTicketLoginTransition();

    registerLoginContainers();

    registerSignUpLogInToggle();

    registerForgotPasswordToggle();
};
