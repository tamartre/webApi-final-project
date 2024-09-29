async function addUserToUsers() {
    const userName = document.getElementById("txtUserName").value;
    const lastName = document.getElementById("txtLastName").value;
    const password = document.getElementById("txtPassword").value;
    const firstName = document.getElementById("txtFirstName").value;
    const email = document.getElementById("txtEmail").value;

    if (!userName || !password) {
        alert("Username and password are required.");
        return;
    }
    if (email && !validateEmail(email)) { alert("Invalid email"); return; }

    const user = { userName, lastName, password, firstName, email };
    const passwordStrength = await checkPasswordStrength(password);

    if (passwordStrength >= 2) {
        const res = await fetchApprove("api/User/register", "POST", user);
        handleUserRegistrationResponse(res);
    } else {
        alert("Weak password.");
    }
}

async function checkExistingUser() {
    const userName = document.getElementById("txtUserNamelogin").value;
    const password = document.getElementById("txtPasswordlogin").value;
    if (!userName || !password) {
        alert("userName and password are required .")
        return;
    }
    const user = { userName, password };

    const res = await fetchApprove("api/User/login", "POST", user);
    handleUserLoginResponse(res);
}

async function checkPasswordStrength(password) {
    const res = await fetchApprove("api/User/password", "POST", password);
    return handlePasswordCheckResponse(res);
}

async function updateUser() {
    const userId = localStorage.getItem("userID");
    const updatedUserDetails = await fetchUserDetails(userId);
    if (!updatedUserDetails) return;
    const { firstName, lastName, password, email } = getUserInputDetails(updatedUserDetails);
    const user = { userName: updatedUserDetails.userName, userId, firstName, lastName, password, email: email.trim() };
    if (user.email) {
        if (!validateEmail(user.email)) { alert("Invalid email"); return; }
    }

    const passwordStrength = await checkPasswordStrength(password);
    if (passwordStrength >= 2) {
        const res = await fetchApprove(`api/User/${userId}`, "PUT", user);
        handleUserUpdateResponse(res);
    }
    else {
        alert("Weak password.");
    }
}

async function fetchApprove(url, method, bodyData) {
    const response = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(bodyData)
    });
    return response;
}

async function fetchUserDetails(userId) {
    const response = await fetch(`api/User/${userId}`);
    if (!response.ok) {
        console.error("Failed to fetch user details.");
        return null;
    }
    return response.json();
}

function handleUserRegistrationResponse(res) {
    if (res.status === 400) {
        alert("Email is invalid.");
    } else if (res.status === 204) {
        alert("Username must be unique.");
    } else if (res.status !== 200) {
        alert("Registration failed.");
    } else {
        res.json().then(data => alert(`Registered: ${data.userName}`));
    }
}

function handleUserLoginResponse(res) {
    if (res.status === 401) {
        alert("Unauthorized.");
    } else if (res.status === 200) {
        res.json().then(data => {
            localStorage.setItem("userID", data.userId);
            window.location.replace("Products.html");
        });
    }
}
function handlePasswordCheckResponse(res) {
    return res.json().then(strength => {
        const colorIndicator = document.getElementById("passwordCheck");
        if (strength === 0) {
            colorIndicator.style.setProperty("background-color", "red");
        } else if (strength === 1) {
            colorIndicator.style.setProperty("background-color", "orange");
        } else if (strength >= 2) {
            colorIndicator.style.setProperty("background-color", "green");
        }
        return strength;
    });
}

function handleUserUpdateResponse(res) {
    if (res.status === 400) {
        alert("Email is invalid.");
    } else if (res.status === 204) {
        alert("User could not be found.");
    } else if (res.status !== 200) {
        alert("Update failed.");
    } else {
        alert("Update succeeded.");
    }
}

function getUserInputDetails(defaultUserDetails) {
    const firstName = document.getElementById("txtFirstName").value || defaultUserDetails.firstName;
    const lastName = document.getElementById("txtLastName").value || defaultUserDetails.lastName;
    const password = document.getElementById("txtPassword").value || defaultUserDetails.password;
    const email = document.getElementById("txtEmail").value || defaultUserDetails.email;
    return { firstName, lastName, password, email };
}


function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}