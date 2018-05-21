/*-------------------------------------
Scroll disablen
-------------------------------------*/

var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

function disableScroll() {
    if (window.addEventListener) // older FF
        window.addEventListener('DOMMouseScroll', preventDefault, false);
    window.onwheel = preventDefault; // modern standard
    window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
    window.ontouchmove = preventDefault; // mobile
    document.onkeydown = preventDefaultForScrollKeys;
}

function enableScroll() {
    if (window.removeEventListener)
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.onmousewheel = document.onmousewheel = null;
    window.onwheel = null;
    window.ontouchmove = null;
    document.onkeydown = null;
}

/*-------------------------------------
Winkelmand index acties
-------------------------------------*/

function winkelmandAantalVerhogen(id, prijs) {
    var plusId = id;
    var plusPrijs = prijs;

    $.ajax({
        type: "GET",
        url: '/Winkelwagen/Plus',
        data: { Id: plusId, Prijs: plusPrijs }
    }).done(function (result) {
        $("#winkelwagen-list-partial").html(result);
        updateWinkelMandCount();
    });


};

function winkelmandAantalVerlagen(id, prijs) {
    var plusId = id;
    var plusPrijs = prijs;

    $.ajax({
        type: "GET",
        url: '/Winkelwagen/Min',
        data: { Id: plusId, Prijs: plusPrijs }
    }).done(function (result) {
        $("#winkelwagen-list-partial").html(result);
        updateWinkelMandCount();
    });
};

function winkelmandItemVerwijderen(id, prijs) {
    if (confirm("Bent u zeker dat u deze cadeaubon(nen) wilt verwijderen uit uw winkelwagen?") == true) {
        var plusId = id;
        var plusPrijs = prijs;

        $.ajax({
            type: "GET",
            url: '/Winkelwagen/Remove',
            data: { Id: plusId, Prijs: plusPrijs }
        }).done(function (result) {
            $("#winkelwagen-list-partial").html(result);
            updateWinkelMandCount();
        });
    }
};

/*-------------------------------------
Winkelmand count span acties acties
-------------------------------------*/

function updateWinkelMandCount() {
    $.ajax({
        type: "GET",
        url: '/Home/UpdateWinkelwagenCount'
    }).done(function (result) {
        $(".layout-winkelwagen-count").html(result);
    });
}

/*-------------------------------------
acties voor het toevoegen aan winkelmand
-------------------------------------*/

function DynamicAddCartRegion(id) {
    $('#' + id + "default").slideToggle('slow', function () {
        $('#' + id + "default").toggleClass('selecionado', $(this).is(':visible'));
    });
    $('#' + id + "card").slideToggle('slow', function () {
        $('#' + id + "card").toggleClass('selecionado', $(this).is(':visible'));
    });
}

function VoegToeAanWinkelmand(id, afbeelding, naam) {
    try {
        var id = id;
        var afbeelding = afbeelding;
        var naam = naam;
        var prijs = parseFloat(document.getElementById(id + "PrijsField").value.replace(',', '.')).toFixed(2);
        var aantal = document.getElementById(id + "AantalField").value;

        toonAddedToWinkelmandPopup(afbeelding, naam, prijs, aantal);

        $.ajax({
            type: "GET",
            url: '/Winkelwagen/Add',
            data: { Id: id, Prijs: prijs, Aantal: aantal }
        }).done(function () {
            updateWinkelMandCount();
            document.getElementById((id + "TerugKnop")).click();
        });
        return false;
    } catch (e) {
        return false;
    }
}

function verbergAddedToWinkelmandPopup() {
    document.getElementById('addedToWinkelmandPopup').style.display = 'none';
    document.getElementById("paginaWrapperVoorBlur").classList.remove('blurOverlay');
    enableScroll();
}

function toonAddedToWinkelmandPopup(afbeelding, naam, waarde, aantal) {
    document.getElementById("winkelMandPopupBonNaam").innerHTML = '<i class="far fa-check-circle font-26"></i> ' + naam + ' werd toegevoegd aan uw winkelwagen!';
    document.getElementById("winkelMandPopupBonAfbeelding").src = "/" + afbeelding;
    document.getElementById("winkelMandPopupBonWaarde").innerHTML = "- Bedrag: &euro; " + waarde;
    document.getElementById("winkelMandPopupBonAantal").innerHTML = "- Aantal: " + aantal;

    document.getElementById("paginaWrapperVoorBlur").classList.add("blurOverlay");

    document.getElementById('addedToWinkelmandPopup').style.display = 'block';

    document.getElementById("closeWinkelMandPopup").onclick = function () {
        verbergAddedToWinkelmandPopup()
    }

    window.onclick = function (event) {
        if (event.target == document.getElementById('addedToWinkelmandPopup')) {
            verbergAddedToWinkelmandPopup()
        }
    }

    disableScroll();
}

$(".winkelmand-register-enter").keyup(function (event) {
    if (event.keyCode === 13) {
        var idVanWinkelMandTrigger = event.target.id;
        var idVanKnopWinkelMandVoorTrigger = "." + idVanWinkelMandTrigger.toString().replace("PrijsField", "").replace("AantalField", "") + "BevestigKnop"
        document.getElementById(idVanKnopWinkelMandVoorTrigger).click();
    }
});

/*-------------------------------------
Animaties callen maar niet op mobile
-------------------------------------*/

$(function () {
    wow = new WOW(
        {
            mobile: false
        }
    )
    wow.init();
});