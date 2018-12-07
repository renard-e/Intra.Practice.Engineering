// Write your JavaScript code.

function requiredSignUp() {
    var tab = new Array(document.forms["formSignUp"]["email"].value, document.forms["formSignUp"]["passwd"].value, document.forms["formSignUp"]["confirmpasswd"].value);
    if (tab[0] == "" || tab[1] == "" || tab[2] == "")
    {
        alert("Please fill all the blanks");
        return false;
    }
}

function requiredSignIn() {
    var tab = new Array(document.forms["formSignIn"]["email"].value, document.forms["formSignIn"]["passwd"].value);
    if (tab[0] == "" || tab[1] == "")
    {
        alert("Please fill all the blanks");
        return false;
    }
}

function removeItem(Id)
{
    window.location.href = "RemoveItem?Id=" + Id;
}

function changeState(user, id, state)
{
    window.location.href = "Manager/changeStateRequest?user=" + user + "&id=" + id + "&newState=" + state;
}