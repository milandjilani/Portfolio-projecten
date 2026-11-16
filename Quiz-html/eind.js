let gekozenVragen = [];
let huidigeVraagIndex = 0;
let score = 0;

let tijdOver = 10;
let timerInterval = null;

const alleVragen = [
    { vraag: "Welke kleur heeft een rijpe banaan meestal?", opties: ["Blauw", "Geel", "Paars", "Rood"], antwoord: "Geel" },
    { vraag: "Aan welke plant groeit een banaan?", opties: ["Bananenplant", "Eikenboom", "Cactus", "Rozenstruik"], antwoord: "Bananenplant" },
    { vraag: "Welke kleur heeft een onrijpe banaan vaak?", opties: ["Groen", "Zwart", "Oranje", "Wit"], antwoord: "Groen" },
    { vraag: "Tot welke fruitsoort behoort de banaan?", opties: ["Fruit", "Groente", "Graan", "Notensoort"], antwoord: "Fruit" },
    { vraag: "Wat zit er om een banaan heen?", opties: ["Schil", "Pantser", "Bladeren", "Steen"], antwoord: "Schil" },
    { vraag: "Welke kleur krijgt een overrijpe banaan vaak?", opties: ["Bruin", "Blauw", "Roze", "Wit"], antwoord: "Bruin" },
    { vraag: "Waar groeien bananen meestal?", opties: ["Tropische gebieden", "Woestijnen", "Poolgebieden", "Bergen"], antwoord: "Tropische gebieden" },
    { vraag: "Wat kun je maken van geprakte bananen?", opties: ["Bananenbrood", "Beton", "Papier", "Glas"], antwoord: "Bananenbrood" },
    { vraag: "Welke vorm heeft een banaan meestal?", opties: ["Gebogen", "Vierkant", "Driehoekig", "Rond"], antwoord: "Gebogen" },
    { vraag: "Welke vitamine zit onder andere in bananen?", opties: ["Vitamine B6", "Vitamine K", "Vitamine D", "Vitamine A"], antwoord: "Vitamine B6" },
    { vraag: "Hoe eet je een banaan meestal?", opties: ["Geschild", "Met schil", "Bevroren steenhard", "In blik"], antwoord: "Geschild" },
    { vraag: "Welke smoothie-ingrediënt wordt vaak gecombineerd met banaan?", opties: ["Aardbei", "Klei", "Zand", "Azijn"], antwoord: "Aardbei" },
    { vraag: "Welke kleur heeft de binnenkant van een banaan?", opties: ["Lichtgeel", "Zwart", "Groen", "Blauw"], antwoord: "Lichtgeel" },
    { vraag: "Bananen bevatten veel van welk mineraal?", opties: ["Kalium", "IJzer", "Goud", "Zilver"], antwoord: "Kalium" },
    { vraag: "Welke snack kun je maken door banaan te drogen?", opties: ["Bananenchips", "Friet", "Crackers", "Koekjes"], antwoord: "Bananenchips" },
    { vraag: "Wat gebeurt er meestal als een banaan rijper wordt?", opties: ["Hij wordt zoeter", "Hij wordt zouter", "Hij wordt kouder", "Hij wordt groter"], antwoord: "Hij wordt zoeter" },
    { vraag: "Welke kleur hebben veel bananen in de supermarkt?", opties: ["Geel", "Paars", "Turquoise", "Grijs"], antwoord: "Geel" },
    { vraag: "Welk fruit wordt vaak op een bananensplit gedaan?", opties: ["Banaan", "Ui", "Aardappel", "Broccoli"], antwoord: "Banaan" },
    { vraag: "Wat verwijder je voordat je een banaan eet?", opties: ["De schil", "De pit", "De steel", "De bladeren"], antwoord: "De schil" },
    { vraag: "Welke vrucht staat centraal in deze quiz?", opties: ["Banaan", "Appel", "Peer", "Sinaasappel"], antwoord: "Banaan" }
];


// START QUIZ
function quiz() {
    const aantal = Number(document.getElementById("invoerbox").value);
    quizKlaar = false;

    if (aantal < 1 || aantal > 20 || isNaN(aantal)) {
        alert("Vul een getal in tussen 1 en 20");
        return;
    }

    score = 0;
    huidigeVraagIndex = 0;

    document.getElementById("hier").innerHTML = "";

    gekozenVragen = [...alleVragen]
        .sort(() => Math.random() - 0.5)
        .slice(0, aantal);

    toonVraag();
    startTimer();
}


function startTimer() {
    clearInterval(timerInterval);

    tijdOver = 10;
    document.getElementById("timerbox").textContent = tijdOver;

    timerInterval = setInterval(() => {

        if (quizKlaar) {
            clearInterval(timerInterval);
            tijdOver = 10;
            document.getElementById("timerbox").textContent = tijdOver;
            return;
        }

        tijdOver--;
        document.getElementById("timerbox").textContent = tijdOver;

        if (tijdOver < 0) {
            volgendeVraag();
        }

    }, 1000);
}

// VOLGENDE VRAAG
function volgendeVraag() {
    clearInterval(timerInterval);
    huidigeVraagIndex++;

    toonVraag();
    startTimer();
}


// VRAGEN TONEN
function toonVraag() {

    if (huidigeVraagIndex >= gekozenVragen.length) {
        toonEindscherm();
        return;
    }

    const vraagObj = gekozenVragen[huidigeVraagIndex];

    document.getElementById("vraag").value = vraagObj.vraag;

    let html = "";
    vraagObj.opties.forEach(optie => {
        html += `<button onclick="kiesAntwoord('${optie}')">${optie}</button><br>`;
    });

    document.getElementById("opties").innerHTML = html;
}


// ANTWOORD
function kiesAntwoord(gekozen) {

    const juist = gekozenVragen[huidigeVraagIndex].antwoord;

    if (gekozen === juist) {
        score++;
    }

    volgendeVraag();
}



function toonEindscherm() {

    quizKlaar = true;
    clearInterval(timerInterval);

    document.getElementById("vraag").value = "Klaar!";
    document.getElementById("opties").innerHTML = "";

    const box = document.getElementById("hier");
    box.innerHTML = "";

    const totaal = gekozenVragen.length;
    const percentage = totaal > 0 ? (score / totaal) * 100 : 0;

    let boodschap = "";

    if (percentage === 100) {
        boodschap = "Perfect!";
    } else if (percentage >= 70) {
        boodschap = "Goed gedaan!";
    } else if (percentage >= 40) {
        boodschap = "Kan beter!";
    } else {
        boodschap = "Oefen nog even!";
    }

    box.innerHTML = `
        <h2>Eindscore: ${score} / ${totaal}</h2>
        <p>${boodschap}</p>
    `;
}