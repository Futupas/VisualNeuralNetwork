var DATA = undefined;
var test_nn = undefined;


function httpGetAsync(theUrl, callback)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() { 
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open('GET', theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}
httpGetAsync('/get_data', function(responseText){
    DATA = JSON.parse(responseText);
    httpGetAsync('/get_nn', function(responseText){
        test_nn = JSON.parse(responseText);
        GenerateNN();
    });
});
// httpGetAsync('/get_nn', function(responseText){
//     test_nn = JSON.parse(responseText);
//     GenerateNN();
// });

var MAIN_DIV = document.getElementById('main');


function GenerateNN() {
    // while (DATA === undefined || test_nn === undefined) {};
    var layers_count = test_nn.neurons.length;
    var biggest_layer_neurons_count = test_nn.neurons[0].length;
    for (var i = 1; i < layers_count; i++) {
        if (test_nn.neurons[i].length > biggest_layer_neurons_count) biggest_layer_neurons_count = test_nn.neurons[i].length;
    }
    MAIN_DIV.style.height = biggest_layer_neurons_count * (DATA.neuron_side + DATA.neurons_distance) + 'px';
    MAIN_DIV.style.width = layers_count * (DATA.neuron_side + DATA.layers_distance) + 'px';
    for (var i = 0; i < layers_count; i++) {
        var layer = document.createElement('div');
        layer.classList.add('layer');
        layer.setAttribute('data-layer_id', i);
        for (var j = 0; j < test_nn.neurons[i].length; j++) {
            var neuron = document.createElement('div');
            neuron.classList.add('neuron');
            neuron.style.margin = (DATA.neurons_distance)+'px '+(DATA.layers_distance/2)+'px';
            neuron.style.borderRadius = DATA.neuron_side/10+'px';
            neuron.style.width = DATA.neuron_side+'px';
            neuron.style.height = DATA.neuron_side+'px';
            neuron.style.border = DATA.neuron_border+'px solid black';
            neuron.innerText = Math.round(test_nn.neurons[i][j].value * 100) / 100;
            neuron.style.lineHeight = DATA.neuron_side+'px';
            neuron.style.fontSize = DATA.neuron_side/3+'px';
            neuron.style.textAlign = 'center';
            neuron.ChangeValue = function(new_value) {
                this.innerText = Math.round(new_value * 100) / 100;
                return this;
            }
            if (j == 0) neuron.style.marginTop = (DATA.neurons_distance/2)+'px';
            if (j == test_nn.neurons[i].length-1) neuron.style.marginBottom = (DATA.neurons_distance/2)+'px';
            test_nn.neurons[i][j].element = neuron;

            neuron.data = {
                layer: i,
                neuron: j
            };
            neuron.opened = false;
            neuron.onclick = function (e) {
                this.opened = !this.opened;
                if (this.opened) {
                    var all_main_weights = MAIN_DIV.querySelectorAll('.main_weight');
                    var all_links = MAIN_DIV.querySelectorAll('.link');
                    var all_neurons = MAIN_DIV.querySelectorAll('.neuron');
                    for (var i = 0; i < all_main_weights.length; i++) {
                        all_main_weights[i].classList.remove('visible');
                    }
                    for (var i = 0; i < all_links.length; i++) {
                        all_links[i].classList.remove('invisible');
                    }
                    for (var i = 0; i < all_neurons.length; i++) {
                        all_neurons[i].opened = false;
                    }
                    this.opened = true;

                    var current_layer = this.data.layer;
                    var current_neuron = this.data.neuron;
                    //all neurons of current layer
                    for (var _i = 0; _i < test_nn.neurons[current_layer].length; _i++) {
                        if (_i == current_neuron) {
                            for(var _j = 0; _j < test_nn.neurons[current_layer][_i].weights.length; _j++) {
                                test_nn.neurons[current_layer][_i].weights[_j].element.querySelector('.main_weight').classList.add('visible');
                            }
                            continue;
                        };
                        for(var _j = 0; _j < test_nn.neurons[current_layer][_i].weights.length; _j++) {
                            test_nn.neurons[current_layer][_i].weights[_j].element.classList.add('invisible');
                        }
                    }
                    //all neurons of next layer
                    for (var _i = 0; _i < test_nn.neurons[current_layer+1].length; _i++) {
                        for(var _j = 0; _j < test_nn.neurons[current_layer+1][_i].weights.length; _j++) {
                            if (_j == current_neuron) {
                                test_nn.neurons[current_layer+1][_i].weights[_j].element.querySelector('.main_weight').classList.add('visible');
                                continue;
                            }
                            test_nn.neurons[current_layer+1][_i].weights[_j].element.classList.add('invisible');
                        }
                    }
                } else {
                    var current_layer = this.data.layer;
                    var current_neuron = this.data.neuron;
                    //all neurons of current layer
                    for (var _i = 0; _i < test_nn.neurons[current_layer].length; _i++) {
                        if (_i == current_neuron) {
                            for(var _j = 0; _j < test_nn.neurons[current_layer][_i].weights.length; _j++) {
                                test_nn.neurons[current_layer][_i].weights[_j].element.querySelector('.main_weight').classList.remove('visible');
                            }
                            continue;
                        };
                        for(var _j = 0; _j < test_nn.neurons[current_layer][_i].weights.length; _j++) {
                            test_nn.neurons[current_layer][_i].weights[_j].element.classList.remove('invisible');
                        }
                    }
                    //all neurons of next layer
                    for (var _i = 0; _i < test_nn.neurons[current_layer+1].length; _i++) {
                        for(var _j = 0; _j < test_nn.neurons[current_layer+1][_i].weights.length; _j++) {
                            if (_j == current_neuron) {
                                test_nn.neurons[current_layer+1][_i].weights[_j].element.querySelector('.main_weight').classList.remove('visible');
                                continue;
                            }
                            test_nn.neurons[current_layer+1][_i].weights[_j].element.classList.remove('invisible');
                        }
                    }
                }
            }
            
            layer.appendChild(neuron);
        }
        MAIN_DIV.appendChild(layer);
    }

    for (var i_l = 0; i_l < layers_count; i_l++) {
        for (var i_n = 0; i_n < test_nn.neurons[i_l].length; i_n++) {
            for (var i_w = 0; i_w < test_nn.neurons[i_l][i_n].weights.length; i_w++) {
                test_nn.neurons[i_l][i_n].weights[i_w].element = GenerateLink(i_l, i_w, i_n, test_nn.neurons[i_l][i_n].weights[i_w].value)
            }
        }
    }
    
}
function GenerateLink(this_layer, from_neuron, to_neuron, value) {
    var link = document.createElement('div');
    link.classList.add('link');
    var main_line = document.createElement('div');
    main_line.classList.add('main_line');
    main_line.style.height = DATA.weight_line_width + 'px';

    link.style.width = DATA.layers_distance + 'px';
    var from_top = test_nn.neurons[this_layer-1][from_neuron].element.offsetTop;
    from_top += DATA.neuron_side / (test_nn.neurons[this_layer].length <= 1 ? 2 : test_nn.neurons[this_layer].length) * (test_nn.neurons[this_layer].length <= 1 ? 1 : to_neuron);

    var to_top = test_nn.neurons[this_layer][to_neuron].element.offsetTop;
    to_top += DATA.neuron_side / (test_nn.neurons[this_layer-1].length + 1) * (from_neuron + 1);

    link.style.top = (from_top < to_top ? from_top : to_top) + 'px';
    link.style.height = Math.abs(from_top - to_top) + 'px';
    link.style.left = (this_layer-1) * (DATA.neuron_side + DATA.layers_distance) + DATA.neuron_side + (DATA.layers_distance/2) + 'px';

    var start_weight = document.createElement('div');
    start_weight.classList.add('circle_weight');
    start_weight.innerHTML = Math.round(value * 100) / 100;
    var end_weight = document.createElement('div');
    end_weight.classList.add('circle_weight');
    end_weight.innerHTML = Math.round(value * 100) / 100;;

    var start_weight_size = DATA.neuron_side / (test_nn.neurons[this_layer].length <= 1 ? 2 : test_nn.neurons[this_layer].length);
    start_weight.style.width = start_weight_size + 'px';
    start_weight.style.height = start_weight_size + 'px';
    start_weight.style.lineHeight = start_weight_size - (2 * (DATA.neuron_border / 1.5)) + 'px';
    start_weight.style.fontSize = start_weight_size / 3 + 'px';
    start_weight.style.borderRadius = start_weight_size / 2 + 'px';
    start_weight.style.left = start_weight_size / -2 + 'px';
    start_weight.style.border = DATA.neuron_border / 1.5 + 'px solid black';
    var end_weight_size = DATA.neuron_side / (test_nn.neurons[this_layer-1].length + 1);
    end_weight.style.width = end_weight_size + 'px';
    end_weight.style.height = end_weight_size + 'px';
    end_weight.style.lineHeight = end_weight_size - (2 * (DATA.neuron_border / 1.5)) + 'px';
    end_weight.style.fontSize = end_weight_size / 3 + 'px';
    end_weight.style.borderRadius = end_weight_size / 2 + 'px';
    end_weight.style.right = end_weight_size / -2 + 'px';
    end_weight.style.border = DATA.neuron_border / 1.5 + 'px solid black';

    var arrow = document.createElement('div');
    arrow.classList.add('arrow');
    arrow.style.top = DATA.weight_line_width * -1.5 + 'px';
    arrow.style.right = end_weight_size/2.5 + 'px';
    arrow.style.borderTop = DATA.weight_line_width*2 + 'px solid transparent';
    arrow.style.borderLeft = DATA.weight_line_width*5 + 'px solid black';
    arrow.style.borderBottom = DATA.weight_line_width*2 + 'px solid transparent';

    var length = Math.sqrt(Math.pow(from_top - to_top, 2) + Math.pow(DATA.layers_distance, 2));
    main_line.style.width = length + 'px';
    main_line.style.left = '0px';
    if (from_top >= to_top) {
        main_line.style.bottom = '0px';
        start_weight.style.bottom = start_weight_size / -2 + 'px';
        end_weight.style.top = end_weight_size / -2 + 'px';
    }
    else {
        main_line.style.top = '0px';
        start_weight.style.top = start_weight_size / -2 + 'px';
        end_weight.style.bottom = end_weight_size / -2 + 'px';
    }
    main_line.style.transform = 'Rotate('+Math.asin((to_top - from_top)/length)+'rad)';

    var main_weight = document.createElement('div');
    main_weight.classList.add('main_weight');
    main_weight.innerHTML = Math.round(value * 10000000) / 10000000;
    main_weight.style.height = DATA.weight_number_size + 'px';
    main_weight.style.lineHeight = (DATA.weight_number_size - DATA.weight_line_width) + 'px';
    main_weight.style.fontSize = (DATA.weight_number_size - DATA.weight_line_width) / 1.7 + 'px';
    main_weight.style.top = (DATA.weight_number_size / -2) + (DATA.weight_line_width * .25) + 'px';
    main_weight.style.borderRadius = DATA.weight_number_size * .5 + 'px';
    main_weight.style.border = DATA.weight_line_width * .5 + 'px solid black';
    main_weight.style.padding = '0px '+DATA.weight_number_size*.4+'px';

    main_line.appendChild(arrow);
    main_line.appendChild(main_weight);
    link.appendChild(main_line);
    link.appendChild(start_weight);
    link.appendChild(end_weight);

    link.ChangeWeight = function (new_weight) {
        var circle_weights = this.querySelectorAll('.circle_weight');
        var main_weight = this.querySelector('.main_weight');
        circle_weights[0].innerText = Math.round(new_weight * 100) / 100;
        circle_weights[1].innerText = Math.round(new_weight * 100) / 100;
        main_weight.innerText = Math.round(new_weight * 10000000) / 10000000;
        return this;
    }

    MAIN_DIV.appendChild(link);

    return link;
}

// GenerateNN();

document.getElementById('main_container').onmousemove = function(e) {
    if (e.buttons == 1) {
        document.getElementById('main_container').scrollTo(
            document.getElementById('main_container').scrollLeft - e.movementX, 
            document.getElementById('main_container').scrollTop - e.movementY);
    }
}



socket = new WebSocket('ws://'+window.location.host+'/');
socket.onopen = function(evt) { 
        console.log('websocket open'); 
        console.log(evt); 
    };
socket.onclose = function(evt) { 
        console.log('websocket close'); 
        console.log(evt); 
    };
socket.onmessage = function(evt) { 
        // console.log('websocket message'); 
        // console.log(evt);
        var new_data = JSON.parse(evt.data);
        UpdateNN(new_data);
    };
socket.onerror = function(evt) { 
        console.log('websocket error'); 
        console.log(evt); 
    };



    
function UpdateNN(new_data) {
    for (var i_l = 0; i_l < test_nn.neurons.length; i_l++) {
        for (var i_n = 0; i_n < test_nn.neurons[i_l].length; i_n++) {
            test_nn.neurons[i_l][i_n].element.ChangeValue(new_data.neurons[i_l][i_n].value);
            for (var i_w = 0; i_w < test_nn.neurons[i_l][i_n].weights.length; i_w++) {
                test_nn.neurons[i_l][i_n].weights[i_w].element.ChangeWeight(new_data.neurons[i_l][i_n].weights[i_w].value);
            }
        }
    }

    if (new_data.is_learning === true) {
        document.getElementById('btn_start_learning').style.cursor = 'not-allowed';
        document.getElementById('btn_stop_learning').style.cursor = 'auto';
    } else if (new_data.is_learning === false) {
        document.getElementById('btn_start_learning').style.cursor = 'auto';
        document.getElementById('btn_stop_learning').style.cursor = 'not-allowed';
    } else {
        document.getElementById('btn_start_learning').style.cursor = 'auto';
        document.getElementById('btn_stop_learning').style.cursor = 'auto';
    }
}


document.getElementById('btn_start_learning').onclick = function (e) {
    httpGetAsync('/nn_start_learning', function(responseText){
        var new_data = JSON.parse(responseText);
        UpdateNN(new_data);
    });
}
document.getElementById('btn_stop_learning').onclick = function (e) {
    httpGetAsync('/nn_stop_learning', function(responseText){
        var new_data = JSON.parse(responseText);
        UpdateNN(new_data);
    });
}


var nn_zoom = 100;
SetNNZoom(nn_zoom, false);
document.getElementById('zoom_input').mult_step = 1.5;

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
