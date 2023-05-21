function cadastrar(){
    inputNome = document.getElementById('nomeInput')
    inputNFC = document.getElementById('inputNFC')
    submit = document.getElementById('botaoSubmit');

    submit.addEventListener('click', function(){
        let request = new XMLHttpRequest();
        let url = "http://localhost:61142/api/User/AdicionaUser";
        let params = JSON.stringify({
            "nome": inputNome.value,
            "nfc": inputNFC.value,
        });
        request.open("POST", url, true);
        request.setRequestHeader("Content-type", "application/json");
        request.onreadystatechange = function() {
            if (request.readyState === 4 && request.status === 200) {
                location.reload();
            }
        };
        request.send(params);
    });
}