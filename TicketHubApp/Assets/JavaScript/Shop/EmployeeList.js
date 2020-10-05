window.onload = function () {
    getEmployList();
}

function getEmployList() {
    var url = 'getEmployeeListApi';
    fetch(url, {
        method: 'GET',
    }).then(function (response) {
        return response.json();
    }).then(function (myJson) {
        genTable(myJson, 'templateTbody', 'employTemplate');
        setRemoveEmploy();
    });
}

function genTable(list, parentId, template) {
    var tbody = document.getElementById(parentId);
    var templateEle = document.getElementById(template);
    tbody.innerHTML = "";
    var td = templateEle.content.querySelectorAll("td");
    for (let item of list) {
        let tr = document.createElement("tr");
        td[0].innerHTML = item.UserName;
        td[1].innerHTML = item.Email;
        td[2].innerHTML = item.Phone;
        td[3].innerHTML = item.Gender;
        td[4].innerHTML = item.RoleName;
        td[5].children[0].setAttribute("id", item.UserId);
        var clone = document.importNode(templateEle.content, true);
        tr.appendChild(clone);
        tbody.appendChild(tr);
    }
}

function setRemoveEmploy() {
    document.querySelectorAll(".removeEmploy").forEach(function (node) {
        var url = 'deleteEmployeeApi';
        var data = { id: `${node.id}` };

        node.addEventListener('click', function () {
            let employee = node.parentNode.parentNode.children[0].innerHTML;
            var check = confirm(`是否刪除 ${employee} 員工 ? `);
            if (check) {
                fetch(url, {
                    method: 'DELETE', // or 'PUT'
                    body: JSON.stringify(data), // data can be `string` or {object}!
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(res => res.json())
                    .catch(error => console.error('Error:', error))
                    .then(function (response) {
                        if (response.Success) {
                            alert("已刪除員工 !")
                            getEmployList();
                        } else {
                            alert(`${response.Message}`)
                        }
                    });
            }
        })
    })
}