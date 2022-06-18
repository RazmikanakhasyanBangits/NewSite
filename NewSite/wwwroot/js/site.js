

    const body = document.querySelector('body'),
        sidebar = body.querySelector('nav'),
        toggle = body.querySelector(".toggle"),
        searchBtn = body.querySelector(".search-box"),
        modeSwitch = body.querySelector(".toggle-switch"),
        modeText = body.querySelector(".mode-text");

    console.log("Hello");


    toggle.addEventListener("click", () => {
        sidebar.classList.toggle("close");
        console.log("sdds");
    })

    searchBtn.addEventListener("click", () => {
        sidebar.classList.remove("close");
        console.log("sdds");
    })

    modeSwitch.addEventListener("click", () => {
        body.classList.toggle("dark");

        if (body.classList.contains("dark")) {
            modeText.innerText = "Light mode";
        } else {
            modeText.innerText = "Dark mode";

        }
    });


    // -----------------------------------------------------------------

    // -> Var
    /*let counter = document.querySelector('.counter');
    let friendCard = document.querySelector('.friends')
    let startCount = counter && parseInt(counter.getAttribute('data-count'));
    let data = null;
    var jsontext = null;

    async function searchUser(data) {
        const res = await fetch("/Search", {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: 'follow',
            referrerPolicy: 'no-referrer',
            body: JSON.stringify(data)
        }).then(res => res.json());

        friendCard.innerHTML = `
            ${res.reduce((prevHTML, item) => `
                ${prevHTML}
                
                  <form class="asd">
                    <div class="Searched_Users_Block">
                        <input type="hidden" name="UserId" value="${item.userId}" />
                        <img class="User_Image" src="./Images/${item.path}"/>
                        <span class="User_Name" name="Name">${item.name}</span>
                        <span class="User_Surname" name="Surname">${item.surname}</span>
                    </div>
                     <button type="submit" class="Request">Request</button>
                    </form>
            `, '')}
        `

        document.querySelectorAll(".asd").forEach(function (requsetCard) {
            requsetCard.addEventListener('submit', function (event) {
                event.preventDefault();
                var formData = new FormData(this);
                console.log(formData)
                data = {
                    UserId: parseInt(formData.get('UserId')),
                };
                sendRequest(data, this);
            });
        });
    }

    function deleteFriend(data, form) {
        fetch('account/Friends/Remove', {
            method: "DELETE",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                Pragma: 'no-cache',
            },
            body: JSON.stringify(data)
        })
            .then(response => {
                console.log(form);
                form.remove();
                counter.innerHTML = (startCount) <= 1 ? '' : `Search Result(${--startCount} Users)`;
            })
            .catch(error => {
                console.log(`We have error from server ${error.message}`)
            });
    }

    function sendRequest(data, form) {
        fetch(`account/SendFriendRequest`, {
            method: "Post",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                Pragma: 'no-cache',
            },
            body: JSON.stringify(data)
        })
            .then(response => {
                console.log(form);
                form.remove();
                counter.innerHTML = (startCount) <= 1 ? '' : `Search Result(${--startCount} Users)`;
            })
            .catch(error => {
                console.log(`We have error from server ${error.message}`)
            });

    }


    document.querySelectorAll(".search").forEach(function (search) {
        search.addEventListener('submit', function (event) {
            event.preventDefault();
            var formData = new FormData(this);
            data = {
                UserName: formData.get('UserName'),
            };
            searchUser(data);

        });
    });





    document.querySelectorAll('.friendCard').forEach(function (friendCard) {
        friendCard.addEventListener('submit', function (event) {
            event.preventDefault();
            var formData = new FormData(this);
            data = {
                CurrentUserId: parseInt(formData.get('CurrentUserId')),
                DeleteUserId: parseInt(formData.get('DeleteUserId')),
            };
            console.log(this);
            deleteFriend(data, this);
        });
    })

})()*/