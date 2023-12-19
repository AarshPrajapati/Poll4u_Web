//Js for AdminPoll.cshtml Page
function Get_View_id(id) {
    var x = id;

    $("div ." + x).fadeIn();

    $("input#" + x).hide();
    $("input." + x).show();

}
function Get_Hide_id(id) {
    var x = id;
    $("div ." + x).fadeOut();
    $("input." + x).hide();
    $("input#" + x).show();

}
//Poll Details
function Show(Que,id) {
    var Question = Que;
    var show_id = id;
    document.getElementById("cmtq").innerHTML = "Question :- " + Question;
    $("#" + show_id).show();
}

function Close(id) {
    var show_id = id;
    $("#" + show_id).hide();
}
//Admin Poll Details

function Show_comment(id, comment) {
    var x = id;
    var y = comment
    $("#" + y).fadeIn();
    document.getElementById("Adminpolls").style.opacity = 0.5;
}

function Hide_Comment(id, comment) {
    var x = id;
    var y = comment;
    $("#" + y).fadeOut();
    document.getElementById("Adminpolls").style.opacity = 1;
}

function Show_Like(id, Like) {
    var x = id;
    var y = Like;
    $("#" + y).fadeIn();
    document.getElementById("Adminpolls").style.opacity = 0.5;
}

function Hide_Like(id, Like) {
    var x = id;
    var y = Like;
    $("#" + y).fadeOut();
    document.getElementById("Adminpolls").style.opacity = 1;
}
//Js For Create_Poll.cshtml Page
let i = 3;
$(function () {
    $('#EndDate').click(function () {
        if ($(this).is(':checked'))
            $(".End_date").show();
        else
            $(".End_date").hide();
    });
});

$(document).ready(function () {

    $("#Addoption").click(function () {
        if (i == 5) {
            alert('You can only use 4 Option For More Use Comment Section');
        }
        else {
            $("#addafter").append("<br> <div class ='form-floating active'><input type='text' class='form-control'  name= 'option" + i + "' placeholder='put a option here.' id='floatingTextarea' required /><label for='floatingTextarea'><p class='fw-lighter'>Eg. Option " + i + " </p></label></div >");
            i = i + 1;
        }
    });

});

$('input[type="checkbox"]').on('change', function () {
    this.value = this.checked ? 1 : 0;
}).change();

