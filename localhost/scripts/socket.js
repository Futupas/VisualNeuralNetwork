
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