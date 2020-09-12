//import { Collapse } from "../../../Scripts/bootstrap.bundle";
let CustomerSidebarItems = Array.from(document.querySelectorAll('.sidebar-nav > div'));
let sideBarUserName = document.querySelector('.sidebar .username');
// let editBtn = document.querySelector('.edit-btn');
// editBtn.setAttribute('isedit', false);

function clearFocused() {
    for (let i of CustomerSidebarItems) {
        if (i.classList.contains('focus')) {
            i.classList.remove('focus')
        }
    }
}

function clearInfoArea() {
    let infoArea = Array.from(document.querySelectorAll('.info *'))
    console.log(infoArea)
    for (let i of infoArea) {
        i.remove();
    }
}

$.ajax({
        url: "Customer/GetCustomerInfo",
        type: "GET",
        dataType: "json",
        success: function (response) {
            console.log(response)
            var firstResponse = response;
            sideBarUserName.innerText = response.UserName;

            for (let i of CustomerSidebarItems) {
                i.addEventListener('click', function (e) {
                    clearFocused();
                    i.classList.add('focus')
                    console.dir(e.target.closest('div'))
                    clearInfoArea();
                    createInfoArea(e.target.closest('div'));
                })
            }

            createInfoArea();



            function createInfoArea(target = document.querySelectorAll(".sidebar-nav > div")[0]) {
                let infoArea = document.querySelector('.col-9')
                let content = document.createElement('div');
                content.classList.add('info-area', 'p-4', 'rounded');
                infoArea.append(content);

                let title = document.createElement('h3');
                title.classList.add('my-3', 'pb-3');
                title.innerText = target.innerText;
                content.append(title);

                let container = document.createElement('div');
                container.classList.add('container');
                content.append(container);

                let row = document.createElement('div');
                row.classList.add('row')
                container.append(row);

                switch (target.innerText) {
                    case "會員資訊":
                        createMemberDetail();
                        break;
                    case "收藏票券":
                        createWishList();
                        break;
                    case "我的票券":
                        createMyTicket();
                        break;
                    case "收藏餐廳":
                        createFavoriteStore()
                        break;
                    default:
                }
            }


            function createMemberDetail() {
                //Account
                let accountDiv = document.createElement('div');
                accountDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-12');
                let accountPhrase = document.createElement('p');
                accountPhrase.innerText = "帳號 : 123456";
                accountDiv.append(accountPhrase);

                //Password
                let passwordDiv = document.createElement('div');
                passwordDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-12');
                let passwordPhrase = document.createElement('p');
                passwordPhrase.innerText = "密碼 : 654321";
                passwordDiv.append(passwordPhrase);
                let editIcon = document.createElement('span');
                editIcon.classList.add('edit-icon', 'iconify', 'ml-2');
                editIcon.setAttribute('data-icon', 'bx:bxs-edit');
                editIcon.setAttribute('data-inline', 'false');
                editIcon.setAttribute('data-toggle', 'modal');
                editIcon.setAttribute('data-target', '#editPassword')
                passwordDiv.append(editIcon);

                //Name
                let nameDiv = document.createElement('div');
                nameDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-6');
                let namePhrase = document.createElement('p');
                namePhrase.innerText = `姓名 : ${firstResponse.UserName}`;
                nameDiv.append(namePhrase);

                //Gender
                let genderDiv = document.createElement('div');
                genderDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-6');
                let genderPhrase = document.createElement('p');
                genderPhrase.innerText = `性別 : ${firstResponse.Sex}`;
                genderDiv.append(genderPhrase);

                //Phone
                let phoneDiv = document.createElement('div');
                phoneDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-6');
                let phonePhrase = document.createElement('p');
                phonePhrase.innerText = `電話 : ${firstResponse.PhoneNumber}`;
                phoneDiv.append(phonePhrase);

                //Email
                let emailDiv = document.createElement('div');
                emailDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-6');
                let emailPhrase = document.createElement('p');
                emailPhrase.innerText = `電子郵件 : ${firstResponse.Email}`;
                emailDiv.append(emailPhrase);

                ////Birthday
                //let birthdayDiv = document.createElement('div');
                //birthdayDiv.classList.add('my-3', 'd-flex', 'align-items-center', 'col-6');
                //let birthdayPhrase = document.createElement('p');
                //birthdayPhrase.innerText = "生日 : 1999/3/12";
                //birthdayDiv.append(birthdayPhrase);

                let memberDetail = document.querySelector('.info-area .container .row')

                memberDetail.append(accountDiv, passwordDiv, nameDiv, genderDiv, phoneDiv, emailDiv);
                memberDetail.after(createEditButton());
            }

            function editMemberDetail() {
                let editedItem = Array.from(document.querySelectorAll('.info-area .col-6'))
                for (let i of editedItem) {
                    i.remove();
                }
                // Name
                let nameDiv = document.createElement("div");
                nameDiv.classList.add("my-3", "align-items-center", "col-6")
                let nameLabel = document.createElement("label");
                nameLabel.setAttribute("for", "name");
                nameLabel.innerText = "姓名 : ";
                let nameInput = document.createElement("input");
                nameInput.setAttribute("type", "text");
                nameInput.setAttribute("id", "name");
                nameInput.value = firstResponse.UserName;
                nameInput.classList.add("form-control", "userNameInput");
                nameDiv.append(nameLabel);
                nameDiv.append(nameInput);

                // Gender
                let genderDiv = document.createElement("div");
                genderDiv.classList.add("my-3", "align-items-center", "col-6")
                let genderLabel = document.createElement("label");
                genderLabel.setAttribute("for", "gender");
                genderLabel.innerText = "性別 : ";
                let genderInput = document.createElement("select");
                genderInput.setAttribute("id", "gender");
                genderInput.value = firstResponse.Sex;
                genderInput.classList.add("form-control", "genderInput");
                genderInput.options.add(new Option("男", "男"));
                genderInput.options.add(new Option("女", "女"));
                genderDiv.append(genderLabel);
                genderDiv.append(genderInput);

                //Phone
                let phoneDiv = document.createElement("div");
                phoneDiv.classList.add("my-3", "align-items-center", "col-6")
                let phoneLabel = document.createElement("label");
                phoneLabel.setAttribute("for", "phone");
                phoneLabel.innerText = "電話 : ";
                let phoneInput = document.createElement("input");
                phoneInput.setAttribute("type", "text");
                phoneInput.setAttribute("id", "phone");
                phoneInput.value = firstResponse.PhoneNumber;
                phoneInput.classList.add("form-control", "phoneInput");
                phoneDiv.append(phoneLabel);
                phoneDiv.append(phoneInput);

                //Email
                let emailDiv = document.createElement("div");
                emailDiv.classList.add("my-3", "align-items-center", "col-6")
                let emailLabel = document.createElement("label");
                emailLabel.setAttribute("for", "email");
                emailLabel.innerText = "電子郵件 : ";
                let emailInput = document.createElement("input");
                emailInput.setAttribute("type", "email");
                emailInput.setAttribute("id", "email");
                emailInput.value = firstResponse.Email;
                emailInput.classList.add("form-control", "emailInput");
                emailDiv.append(emailLabel);
                emailDiv.append(emailInput);

                ////Birthday
                //let birthdayDiv = document.createElement("div");
                //birthdayDiv.classList.add("my-3", "align-items-center", "col-6")
                //let birthdayLabel = document.createElement("label");
                //birthdayLabel.setAttribute("for", "birthday");
                //birthdayLabel.innerText = "生日 : ";
                //let birthdayInput = document.createElement("input");
                //birthdayInput.setAttribute("type", "date");
                //birthdayInput.setAttribute("id", "birthday");
                //birthdayInput.classList.add("form-control");
                //birthdayDiv.append(birthdayLabel);
                //birthdayDiv.append(birthdayInput);

                let infoArea = document.querySelector('.info-area .container .row');
                infoArea.append(nameDiv, genderDiv, phoneDiv, emailDiv);
            }

            function createEditButton() {
                let editBtn = document.createElement('button');
                editBtn.classList.add('btn', 'btn-primary', 'edit-btn')
                editBtn.setAttribute('isedit', false);
                editBtn.innerText = "編輯個人資料";

                editBtn.addEventListener("click", function () {

                    if (editBtn.getAttribute("isedit") == "true") {

                        $.ajax({ cache: false });
                        $.ajax({
                            url: "Customer/ChangeInfoData",
                            method: "post",
                            contentType: "application/json",
                            data: JSON.stringify({
                                Id: response.Id,
                                UserName: $(".userNameInput").val(),
                                Gender: $(".genderInput").val(),
                                PhoneNumber: $(".phoneInput").val(),
                                Email: $(".emailInput").val()
                            }),
                            success: function (response) {
                                firstResponse.UserName = response.UserName;
                                firstResponse.Email = response.Email;
                                firstResponse.PhoneNumber = response.PhoneNumber;
                                firstResponse.Sex = response.Sex;
                                sideBarUserName.innerText = response.UserName;
                                clearInfoArea();
                                createInfoArea();
                            },
                            error: function () {
                                alert("WRONG")
                            },
                            complete: function () {
                                editBtn.setAttribute("isEdit", false);
                                editBtn.innerText = "編輯個人資料";
                            }
                        })

                    } else {
                        editMemberDetail()
                        editBtn.isedit = true;
                        editBtn.setAttribute("isEdit", true);
                        editBtn.innerText = "儲存";
                    }
                })

                return editBtn;
            }

            function createWishList() {
                let ticketDiv = document.createElement('div');
                ticketDiv.classList.add('wish-Ticket', 'w-100', 'rounded', 'd-flex', 'mb-3', 'shadow')

                let imgDiv = document.createElement('div');
                imgDiv.classList.add('img-area')
                let textDiv = document.createElement('div')
                textDiv.classList.add('text-area', 'border', 'border-left-0', 'rounded-right', 'position-relative')
                ticketDiv.append(imgDiv, textDiv);

                let img = document.createElement('div')
                img.style.backgroundImage = "url('https://picsum.photos/400/300/?random=1')";
                img.style.backgroundSize = "cover";
                img.classList.add('w-100', 'h-100')
                imgDiv.append(img);

                let ticketTitle = document.createElement('h6');
                ticketTitle.classList.add('my-3', 'ml-4')
                ticketTitle.innerText = "【Build School限時超激優惠】 忠孝新生一日遊"
                textDiv.append(ticketTitle);

                let ticketContent = document.createElement('p');
                ticketContent.classList.add('mx-4')
                ticketContent.innerText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Facere ut atque nobis voluptatum asperiores maxime numquam. Quos minus nobis adipisci?"
                textDiv.append(ticketContent);

                let position = document.createElement('p');
                let positionIcon = document.createElement('i');
                positionIcon.classList.add('iconify', 'mr-2');
                positionIcon.setAttribute('data-icon', 'fa-solid:map-marker-alt');
                position.classList.add('position', 'ml-4');
                position.innerText = "台北";
                textDiv.append(position);
                position.prepend(positionIcon);

                let originalPrice = document.createElement('p');
                originalPrice.classList.add('oPrice', 'position-absolute')
                originalPrice.innerText = "TWD 99999";
                textDiv.append(originalPrice);

                let newPrice = document.createElement('p');
                newPrice.classList.add('nPrice', 'position-absolute');
                newPrice.innerText = "TWD 1000";
                textDiv.append(newPrice);

                let favorite = document.createElement('i');
                favorite.classList.add('favorite', 'iconify', 'position-absolute');
                favorite.setAttribute('data-icon', 'bx:bxs-heart');
                textDiv.append(favorite);

                let wishList = document.querySelector('.info-area .container .row')
                wishList.append(ticketDiv);
            }

            function createMyTicket() {
                //Nav
                let nav = document.createElement('ul');
                nav.classList.add('nav', 'nav-tabs');
                nav.setAttribute('id', 'myTab');

                let navItemValidTicket = document.createElement('li');
                navItemValidTicket.classList.add('nav-item');
                let navItemInvalidTicket = document.createElement('li');
                navItemInvalidTicket.classList.add('nav-item');

                let validTicketTab = document.createElement('a');
                validTicketTab.classList.add('nav-link', 'active')
                validTicketTab.setAttribute('data-toggle', 'tab');
                validTicketTab.setAttribute('href', '#valid')
                validTicketTab.innerText = "未使用票券";
                let invalidTicketTab = document.createElement('a');
                invalidTicketTab.classList.add('nav-link')
                invalidTicketTab.setAttribute('data-toggle', 'tab');
                invalidTicketTab.setAttribute('href', '#inValid')
                invalidTicketTab.innerText = "已使用票券";

                nav.append(navItemValidTicket, navItemInvalidTicket)
                navItemValidTicket.append(validTicketTab);
                navItemInvalidTicket.append(invalidTicketTab);

                //Content
                let contentArea = document.createElement('div')
                contentArea.classList.add('tab-content')
                contentArea.setAttribute('id', 'myTabContent')

                let validTicketContent = document.createElement('div');
                validTicketContent.classList.add('tab-pane', 'fade', 'show', 'active');
                validTicketContent.setAttribute('id', 'valid');

                //Valid Ticket
                let validTicket = document.createElement('div');
                validTicket.classList.add('valid-ticket', 'w-100', 'd-flex', 'position-relative', 'my-3', 'px-2', 'py-3', 'border-bottom')
                validTicketContent.append(validTicket);

                let imgArea = document.createElement('div');
                imgArea.classList.add('img-area', 'h-100');
                imgArea.style.backgroundImage = "url('https://picsum.photos/400/300/?random=1')"
                imgArea.style.backgroundSize = "cover";

                let textArea = document.createElement('div');
                textArea.classList.add('text-area');
                validTicket.append(imgArea, textArea);

                let title = document.createElement('h6');
                title.classList.add('my-3', 'ml-4')
                title.innerText = "【Build School限時超激優惠】 忠孝新生一日遊";
                let validTime = document.createElement('p')
                validTime.classList.add('valid-time', 'ml-4')
                validTime.innerHTML = "有效期限 : <span>2021/01/01</span> 前"
                let goDetail = document.createElement('i');
                goDetail.classList.add('go-detail', 'fas', 'fa-angle-right', 'position-absolute');
                textArea.append(title, validTime, goDetail);

                //Invalid Ticket
                let invalidTicketContent = document.createElement('div');
                invalidTicketContent.classList.add('tab-pane', 'fade');
                invalidTicketContent.setAttribute('id', 'inValid');

                let invalidTicket = document.createElement('div');
                invalidTicket.classList.add('invalid-ticket', 'w-100', 'd-flex', 'position-relative', 'my-3', 'px-2', 'py-3', 'border-bottom')
                invalidTicketContent.append(invalidTicket);

                let invalidImgArea = document.createElement('div');
                invalidImgArea.classList.add('img-area', 'h-100');
                invalidImgArea.style.backgroundImage = "url('https://picsum.photos/400/300/?random=1')"
                invalidImgArea.style.backgroundSize = "cover";

                let invalidTextArea = document.createElement('div');
                invalidTextArea.classList.add('text-area');
                invalidTicket.append(invalidImgArea, invalidTextArea);

                let invalidTitle = document.createElement('h6');
                invalidTitle.classList.add('my-3', 'ml-4')
                invalidTitle.innerText = "【Build School限時超激優惠】 忠孝新生一日遊";
                let useTime = document.createElement('p')
                useTime.classList.add('use-time', 'ml-4')
                useTime.innerHTML = "使用日期 : <span>2024/12/11</span>"
                invalidTextArea.append(invalidTitle, useTime);


                let myTicket = document.querySelector('.info-area .container .row')
                let wrapper = document.createElement('div')
                wrapper.classList.add('my-ticket', 'w-100')
                myTicket.append(wrapper);
                wrapper.append(nav, contentArea);
                contentArea.append(validTicketContent, invalidTicketContent)
            }

            function createFavoriteStore() {
                let storeDiv = document.createElement('div');
                storeDiv.classList.add('favorite-store', 'w-100', 'rounded', 'd-flex', 'mb-3', 'shadow')

                let imgDiv = document.createElement('div');
                imgDiv.classList.add('img-area')
                let textDiv = document.createElement('div')
                textDiv.classList.add('text-area', 'border', 'border-left-0', 'rounded-right', 'position-relative')
                storeDiv.append(imgDiv, textDiv);

                let img = document.createElement('div')
                img.style.backgroundImage = "url('https://picsum.photos/400/300/?random=1')";
                img.style.backgroundSize = "cover";
                img.classList.add('w-100', 'h-100')
                imgDiv.append(img);

                let ticketTitle = document.createElement('h6');
                ticketTitle.classList.add('my-3', 'ml-4')
                ticketTitle.innerText = "【東區最夯牛排館】 就是間牛排館"
                textDiv.append(ticketTitle);

                let ticketContent = document.createElement('p');
                ticketContent.classList.add('mx-4')
                ticketContent.innerText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Facere ut atque nobis voluptatum asperiores maxime numquam. Quos minus nobis adipisci?"
                textDiv.append(ticketContent);

                let position = document.createElement('p');
                let positionIcon = document.createElement('i');
                positionIcon.classList.add('iconify', 'mr-2');
                positionIcon.setAttribute('data-icon', 'fa-solid:map-marker-alt');
                position.classList.add('position', 'ml-4', 'mb-2');
                position.innerText = "台北 / 大安區";
                textDiv.append(position);
                position.prepend(positionIcon);

                let address = document.createElement('p');
                let addressIcon = document.createElement('i');
                address.classList.add('address', 'ml-4', 'mb-2');
                addressIcon.classList.add('iconify', 'mr-2');
                addressIcon.setAttribute('data-icon', 'whh:house');
                address.innerText = "復興南路一段279巷4號"
                textDiv.append(address);
                address.prepend(addressIcon);

                let phone = document.createElement('p');
                let phoneIcon = document.createElement('i');
                phone.classList.add('phone', 'ml-4', 'mb-2');
                phoneIcon.classList.add('iconify', 'mr-2');
                phoneIcon.setAttribute('data-icon', 'bx:bxs-phone');
                phone.innerText = "(02)2706-1068"
                textDiv.append(phone);
                phone.prepend(phoneIcon);

                let openTime = document.createElement('p');
                let openTimeIcon = document.createElement('i');
                openTime.classList.add('openTime', 'ml-4', 'mb-2');
                openTimeIcon.classList.add('iconify', 'mr-2');
                openTimeIcon.setAttribute('data-icon', 'fa-solid:clock');
                openTime.innerText = "17:00 - 02:00"
                textDiv.append(openTime);
                openTime.prepend(openTimeIcon);


                let favorite = document.createElement('i');
                favorite.classList.add('favorite', 'iconify', 'position-absolute');
                favorite.setAttribute('data-icon', 'bx:bxs-heart');
                textDiv.append(favorite);

                let wishList = document.querySelector('.info-area .container .row')
                wishList.append(storeDiv);
            }
        },
        error: function () {
            alert("WRONG")
        }
})

