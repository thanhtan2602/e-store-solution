$(document).ready(function () {
    $('.slider-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        fade: true,
        asNavFor: '.slider-nav'
    });

    $('.slider-nav').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.slider-for',
        centerMode: true,
        focusOnSelect: true
    });

    $('.filtering').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: true,
    });
});

const $ = document.querySelector.bind(document)
const $$ = document.querySelectorAll.bind(document)
const tabs = $$('.tab-link') // Add dot prefix for class
const contents = $$('.tab-content') // Add dot prefix for class
console.log(tabs, contents)