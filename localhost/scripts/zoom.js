var nn_zoom = 100;
SetNNZoom(nn_zoom, false);

document.getElementById('zoom_input').oninput = function(e) {
    var is_from_buttons = (e.data === undefined)
    var value = parseInt(this.value);

    if (isNaN(value)) {
        console.error('Zoom value is not a number!');
    } else {
        SetNNZoom(value, is_from_buttons);
    }
}


function SetNNZoom(new_value, is_from_buttons) {
    var zoom_input = document.getElementById('zoom_input');
    if (!is_from_buttons) {
        zoom_input.old_value = new_value;
        zoom_input.value = new_value;
        console.log(new_value);
        MAIN_DIV.style.transform = 'scale('+(new_value / 100)+')';
    } else {
        var old_value = zoom_input.old_value;
        var value = (new_value > old_value) ? (old_value * zoom_input.mult_step) : (old_value / zoom_input.mult_step);
        zoom_input.old_value = value;
        value = Math.round(value * 10) / 10;
        zoom_input.value = value;
        console.log(value);
        MAIN_DIV.style.transform = 'scale('+(value / 100)+')';
    }
}