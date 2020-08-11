

$(document).ready(function () {

  $('.pdfviewer').each(function () {
    var pdf = $(this).data('pdf');
    var div = '#' + $(this).attr('id');
    PDFObject.embed(pdf, div);

  });

  $(".screens-sliders").owlCarousel({
    loop: true,
    margin: 0,
    nav: false,
    items: 1,
    lazyLoad: true,
    center: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    navText: [
      "<div class='nav-btn prev-slide'></div>",
      "<div class='nav-btn next-slide'></div>"
    ]
  });

  $(".featured-posts-slides").owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    items: 1,
    dots: false,
    lazyLoad: true,
    center: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    navText: [
      "<i class='fas fa-chevron-left'></i>",
      "<i class='fas fa-chevron-right'></i>"
    ]
  });

  var featuresCarousel = $(".system-features-slides").owlCarousel({
    loop: true,
    margin: 0,
    dots: false,
    nav: false,
    mouseDrag: false,
    items: 1,
    lazyLoad: true,
    center: true,
    autoplay: true,
    autoplayTimeout: 4000,
    autoplayHoverPause: true
  });

  $('.next-feature').click(function () {
    featuresCarousel.trigger('next.owl.carousel');
  });
  $('.prev-feature').click(function () {
    featuresCarousel.trigger('prev.owl.carousel');
  });


  $("#arrow").click(function () {
    $.fn.pagepiling.moveSectionDown();
  });


  $(".search-icon").click(function (e) {
    var p = $(this).parent();

    p.toggleClass('opensearch');
    e.stopPropagation();
  });


  $(".has-sub-menu").mouseover(function () {
    $(this).toggleClass('active');
  });

  $(document).click(function (e) {
    if (($(e.target).parents(".blog-search").length === 0) && ($('.blog-search').hasClass('opensearch'))) {
      $('.blog-search').removeClass('opensearch');
    }
    if (($(e.target).parents(".has-sub-menu").length === 0) && ($('.has-sub-menu').hasClass('active'))) {
      $('.has-sub-menu').removeClass('active');
    }

  });
});
