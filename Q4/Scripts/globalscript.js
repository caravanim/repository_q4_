$(document).ready(function () {
    $(document).on("click", "#login", loginf);
    $("#lform").submit(checkUser);
    $(document).on("click", "#register", Registerf);
    $("#Rform").submit(CreateNewUser);
    $(document).on("click", ".close", closeWindow);
});

function closeWindow() {
    $("#logformat").hide();
    $("#Registerform").hide();
    $("#FirstName").val("");
    $("#LastName").val("");
    $("#Birthday").val("");
    $("#Mail").val("");
    $("#psw").val("");
    $("#lPassword").val("");
    $("#lMail").val("");
    $("#faildlogin").html("");
}

function Registerf() {
    $("#Registerform").show();

    var modal = $("#Registerform")[0];
    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

}

function CreateNewUser() {
    let firstName = $("#FirstName").val();
    let lastName = $("#LastName").val();
    let birthday = $("#Birthday").val();
    let mail = $("#Mail").val();
    let psw = $("#psw").val();

    R = {
        Firstname: firstName,
        Lastname: lastName,
        Birthday: birthday,
        Mail: mail,
        Password: psw
    }

    $("#FirstName").val("");
    $("#LastName").val("");
    $("#Birthday").val("");
    $("#Mail").val("");
    $("#psw").val("");

    ajaxCall("POST", "../api/Users", JSON.stringify(R), postUFSuccess, postUFError)
    $("#Registerform").hide();
    return false;
}

function postUFSuccess(status) {
    console.log(status);
}

function postUFError(err) {
    console.log("The user was not uploaded to the server");
}

function loginf() {
    if ($("#login").val() == "sign out") {
        $("#register").show();
        $("#login").prop("value", "Login")
        $("#username").html("");
    }
    else {
        $("#logformat").show();
        var modal = $("#logformat")[0];

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    }

}

function checkUser() {
    let mail = $("#lMail").val();
    let password = $("#lPassword").val();
    let apiCall = "../api/Users?mail=" + mail + "&password=" + password;
    ajaxCall("GET", apiCall, "", getUsrSuccess, getUsrError);
    $("#lPassword").val("");
    $("#lMail").val("");
    return false;
}

function getUsrSuccess(username) {
    console.log(username);
    if (username.Firstname == null) {
        str = "The user name or password are not exist";
        $("#faildlogin").html(str);

    }
    else {

        localStorage[0] = JSON.stringify(username);
        console.log(localStorage[0]);
        $("#register").hide();
        $("#login").prop("value", "sign out")
        $("#username").show();
        str = username.Firstname;
        $("#username").html(str);
        $("#logformat").hide();
    }


}

function getUsrError(err) {
    alert("The user name or password are not exist");
    console.log(err);
}