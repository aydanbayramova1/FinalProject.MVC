
document.addEventListener('DOMContentLoaded', function () {
    const paginationLinks = document.querySelectorAll('.pagination .page-link');

    paginationLinks.forEach(link => {
        link.addEventListener('click', function (e) {