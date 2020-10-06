document.getElementById("search").addEventListener("click", fetchData);
function fetchData() {
    let value = document.getElementById("inputAccount").value;
    $.ajax({
        type: 'POST',
        url: 'searchAccountApi',
        data: { account: value },
        success: function (json) {
            if (json.Email != null) {
                setUserInfo(json.Email, json.UserName, json.PhoneNumber, json.Sex);
                document.getElementById("submit").classList.remove('grayBtn');
                document.getElementById("submit").classList.add('blueBtn');
            } else {
                document.getElementById("message").innerText = "Error : " + json;
            }
        }
    });
}

function setUserInfo(account, name, phone, gender) {
    document.getElementById("account").innerText = account;
    document.getElementById("name").innerText = name;
    document.getElementById("phone").innerText = phone;
    document.getElementById("gender").innerText = gender;
    document.getElementById("submit").setAttribute("type", "submit");
    document.getElementById("inputAccount").setAttribute("readonly", true);
}