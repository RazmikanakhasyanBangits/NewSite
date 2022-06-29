const body = document.querySelector('body'),
    sidebar = body.querySelector('nav'),
    toggle = body.querySelector(".toggle"),
    searchBtn = body.querySelector(".search-box"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");
searchResClass = body.querySelector(".friends");

toggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
    searchResClass.classList.toggle("close")
})

searchBtn.addEventListener("click", () => {
    sidebar.classList.remove("close");
    searchResClass.classList.remove("close")
})

modeSwitch.addEventListener("click", () => {
    body.classList.toggle("dark");

    if (body.classList.contains("dark")) {
        modeText.innerText = "Light mode";
    } else {
        modeText.innerText = "Dark mode";

    }
});



const search = document.getElementById("Search");
const searchResult = document.getElementById("friends");
let request = null
//Events For Requests
document.querySelectorAll(".search").forEach(function (search) {
    search.addEventListener('submit', function (event) {
        event.preventDefault();
        formData = new FormData(this)
        request = {
            UserName: formData.get('UserName')
        }
        searchUser(request);
    })
});

document.querySelectorAll(".getfriends").forEach(function (get) {
    get.addEventListener("click", function (event) {
        event.preventDefault();
        getFriends();
    })
})
//End Events


//Requests
async function getFriends() {

    const res = await fetch("/GetFriends", {
        method: 'GET',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
    }).then(res => res.json());
    searchResult.innerHTML = `
            ${res.reduce((prevHTML, item) => `
                ${prevHTML}
                
                  <form class="searchForm">
                    <div class="Searched_Users_Block">
                        <input type="hidden" name="UserId" value="${item.id}" />
                        <img class="User_Image" src="./Files/${item.photo}"/>
                        <span class="User_Name" name="Name">${item.name}</span>
                        <span class="User_Surname" name="Surname">${item.surname}</span>
                    </div>
                     <input type="submit" id="sendRequest" class="Request" value="Send Request" >
                    </form>
            `, '')}
        `
}

async function sendRequest1(data,form) {
    await fetch("/SendFriendRequest", {
        method: 'POST',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: +data?.UserId
    }).then(
        console.log(form),
        form.remove()
    )
};
async function searchUser(data) {
    const res = await fetch("/SeachUsers", {
        method: 'POST',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    }).then(res => res.json());

  
searchResult.innerHTML = `
            ${res.reduce((prevHTML, item) => `
                ${prevHTML}
                
                  <form class="searchForm">
                    <div class="Searched_Users_Block">
                        <input type="hidden" name="UserId" value="${item.id}" />
                        <img class="User_Image" src="./Files/${item.photo}"/>
                        <span class="User_Name" name="Name">${item.name}</span>
                        <span class="User_Surname" name="Surname">${item.surname}</span>
                    </div>
                     <input type="submit" id="sendRequest" class="Request" value="Send Request" >
                    </form>
            `, '')}
        `
    document.querySelectorAll(".searchForm").forEach(function (sendRequest) {
        sendRequest.addEventListener('submit', function (event) {
            event.preventDefault();
            formData = new FormData(this)
            request = {
                UserId: formData.get('UserId')
            }
            sendRequest1(request,this);
        })
    });
};
//end Requests