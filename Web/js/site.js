﻿$(document).ready(function () {
    // All input type file, bootstrap stilized
    $("input[type=file]").bootstrapFileInput();

    // Al bootstrap modal options
    $('.modal').modal({
        show: false,
        keyboard: false
    })
});