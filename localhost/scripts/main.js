httpGetAsync('/get_data', function(responseText){
    DATA = JSON.parse(responseText);
    httpGetAsync('/get_nn', function(responseText){
        test_nn = JSON.parse(responseText);
        GenerateNN();
    });
});

document.getElementById('main_container').onmousemove = function(e) {
    if (e.buttons == 1) {
        document.getElementById('main_container').scrollTo(
            document.getElementById('main_container').scrollLeft - e.movementX, 
            document.getElementById('main_container').scrollTop - e.movementY);
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