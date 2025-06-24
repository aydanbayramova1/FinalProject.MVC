
"use strict";

function handlePreloader() {
    var $preloader = $(".preloader");

    $(window).on("load", function () {
        setTimeout(function () {
            $preloader.fadeOut(600);
        }, 3000);
    });

    setTimeout(function () {
        if ($preloader.is(":visible")) {
            $preloader.fadeOut(600);
        }
    }, 1000);
}

$(document).ready(function () {
    handlePreloader();
});


function initStickyHeader() {
    if ($('.active-sticky-header').length) {
        const $window = $(window);

        function setHeaderHeight() {
            $("header.main-header").css("height", $('header .header-sticky').outerHeight());
        }

        $window.on('resize', function () {
            setHeaderHeight();
        });


        $window.on("scroll", function () {
            var fromTop = $window.scrollTop();
            setHeaderHeight();
            var headerHeight = $('header .header-sticky').outerHeight();

            $("header .header-sticky").toggleClass("hide", (fromTop > headerHeight + 100));
            $("header .header-sticky").toggleClass("active", (fromTop > 600));
        });
    }
}

$(document).ready(function () {
    initStickyHeader();
});


function initMobileMenu(menuSelector, targetSelector, labelText = '') {
    $(menuSelector).slicknav({
        label: labelText,
        prependTo: targetSelector
    });
}



function toggleMobileMenu() {
    const menu = document.getElementById('mobileMenu');
    menu.classList.toggle('show');
}

document.addEventListener('click', function (e) {
    const menu = document.getElementById('mobileMenu');
    const menuIcon = document.querySelector('.slicknav-menu');
    if (!menuIcon.contains(e.target)) {
        menu.classList.remove('show');
    }
});



