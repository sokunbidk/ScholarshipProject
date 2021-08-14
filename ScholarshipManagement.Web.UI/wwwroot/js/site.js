////// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
////// for details on configuring this project to bundle and minify static web assets.


////function congrats() {
////    $(document).ready(function () {
////        let action = document.getElementById('successUser');
////        if (action.innerHTML === "User successfully created") {
////            Swal.fire({
////                icon: 'success',
////                title: 'Congratulations! You have successfully registered.',
////                showCancelButton: false,
////                focusConfirm: false,
////                html:
////                    'Please click on, ' +
////                    '<b><a asp-controller="User" asp-action="Login">Sign In</a></b> ' +
////                    'to log into your page',
////                confirmButtonColor: '#b1ef95',
////                confirmButtonText:
////                    '<a asp-controller="User" asp-action="Login">Sign In</a>',
////                confirmButtonAriaLabel: 'Thumbs up, OK!'
////            })
////        }
////    })

let fullname = document.getElementById('fullname').value;
let phoneNumber = document.getElementById('phoneNo').value;
console.log(password);

function confirmPassword() {

    let password = document.getElementById('password').value;
    let cpassword = document.getElementById('confirmpassword').value;
    if (password != cpassword) {
        document.getElementById('passworderr').innerHTML = " password doesn't match !"
        return false;
    }
    else {
        document.getElementById('passworderr').innerHTML = "";
        return true;
    }
}

function validateEmail() {
  

    let email = document.getElementById('email').value;
    var mailformat = /^[^@@\s]+@@[^@@\.\s]+(\.[^@@\.\s]+)+$/;
    if (!email.match(mailformat) && email != "") {
        document.getElementById('emailerr').innerHTML = " Email is invalid Try Lower Case !"
        return false;
    }
    else {
        document.getElementById('emailerr').innerHTML = "";
        return true;
    }
}

function validateMemberCode() {

    let MemberCode = document.getElementById('membercode').value;
    if (isNaN(MemberCode)) {
        document.getElementById('codeerr').innerHTML = " MemberCode is invalid !"
        return false
    }
    else {
        document.getElementById('codeerr').innerHTML = "";
        return true;
    }
}

/*function validateSignUpForm() {
    let is_Email_Valid = validateEmail();
    let is_Password_Valid = confirmPassword();
    let is_MemberCode_Valid = validateMemberCode();



    if ((email === null || is_Email_Valid === false) || (password === null || is_Password_Valid === false) || (MemberCode === null || is_MemberCode_Valid === false) || (phoneNumber === null || fullname)) {
        return false;
    }
    return true;
  
}
*/
