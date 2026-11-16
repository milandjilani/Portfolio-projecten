const hamburgerBtn = document.querySelector('.hamburgerbtn');
const menu = document.getElementById('hamburgerMenu');

hamburgerBtn.addEventListener('click', () => {
    menu.style.display = menu.style.display === 'flex' ? 'none' : 'flex';
});