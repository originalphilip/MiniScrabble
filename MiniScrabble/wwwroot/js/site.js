﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//handles form when submitted
document.getElementById('wordForm').addEventListener('submit', function (event){
    event.preventDefault();
    let word = document.getElementById('word').value;
    let score = calculateScore(word);
    fetch('/Home/GetSpecialWords').then(response => response.json()).then(data => {
        console.log('Special Words:', data);
        let specialWords = data;
        if (specialWords.includes(word.toUpperCase())) {
            score *= 2;
        }
        document.getElementById('displayScore').innerText = `Score: ${score}`;

    })
        .catch(error => console.error('Error fetching special words:', error));
    fetchApiData();
});

// calculates the score for each word entered
function calculateScore(word) {
    let score = 0;
    for (let char of word.toUpperCase()) {
        score += char.charCodeAt(0) - 64; 
    }
    return score;
}


function fetchApiData() {
    fetch('https://testapi.sail-dev.com/api/data/getworddata').then(response => response.json()).then(data => {
        let displayWords = '<h3> Previous Words: </h3><ul>';
        data.forEach(item => {
            let date = new Date(item.scoreDate);
            let formattedDate = date.toLocaleDateString();
            displayWords += `<li>${item.word} = ${item.score} (${formattedDate})</li>`;
        });
        displayWords += '</ul>';
        document.getElementById('displayApiData').innerHTML = displayWords;
    });
}
