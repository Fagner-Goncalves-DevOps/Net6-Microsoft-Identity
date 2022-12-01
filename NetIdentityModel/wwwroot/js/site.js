// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function confirmaDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmaDeleteSpan = 'confirmaDeleteSpan_' + uniqueId;
    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmaDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmaDeleteSpan).hide();
    }
}