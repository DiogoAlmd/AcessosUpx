function cadastrar() {
    // Obter referências aos elementos do formulário e ao botão de envio
    let inputNome = document.getElementById('nomeInput');
    let inputNFC = document.getElementById('inputNFC');
    let submit = document.getElementById('botaoSubmit');
  
    // Adicionar um ouvinte de evento de clique ao botão de envio
    submit.addEventListener('click', function() {
      // Criar uma nova solicitação XMLHttpRequest
      let request = new XMLHttpRequest();
      let url = "http://localhost:61142/api/User/AdicionaUser";
      
      // Preparar os parâmetros da solicitação no formato JSON
      let params = JSON.stringify({
        "nome": inputNome.value,
        "nfc": inputNFC.value,
      });
      
      // Configurar a solicitação HTTP POST
      request.open("POST", url, true);
      request.setRequestHeader("Content-type", "application/json");
      
      // Definir a função de retorno de chamada para lidar com a resposta da solicitação
      request.onreadystatechange = function() {
        if (request.readyState === 4 && request.status === 200) {
          // Recarregar a página quando a solicitação for bem-sucedida
          location.reload();
        }
      };
      
      // Enviar a solicitação com os parâmetros
      request.send(params);
    });
  }
  