$(document).ready(function () {
    startgallery();
});

function startgallery() {
    $('#gallery').imagegallery({
        // selector given to jQuery's delegate method:
        selector: 'a[data-gallery="gallery"]',
        // event handler namespace:
        namespace: 'imagegallery',
        // Shows the next image after the given time in ms (0 = disabled):
        slideshow: 0,
        // Offset of image width to viewport width:
        offsetWidth: 100,
        // Offset of image height to viewport height:
        offsetHeight: 100,
        // Display images fullscreen (overrides offsets):
        fullscreen: false,
        // Display images as canvas elements:
        canvas: false,
        // body class added on dialog display:
        bodyClass: 'gallery-body',
        // element id of the loading animation:
        loaderId: 'gallery-loader',
        // list of available dialog effects,
        // used when show/hide is set to "random":
        effects: [
            'blind',
            'clip',
            'drop',
            'explode',
            'fade',
            'fold',
            'puff',
            'slide',
            'scale'
        ],
        // The following are jQuery UI dialog options, see
        // http://jqueryui.com/demos/dialog/#options
        // for additional options and documentation:
        modal: true,
        resizable: false,
        width: 'auto',
        height: 'auto',
        show: 'fade',
        hide: 'fade',
        dialogClass: 'gallery-dialog'
    });
}
