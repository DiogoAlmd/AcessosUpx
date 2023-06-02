// Função para fazer uma solicitação HTTP GET usando XMLHttpRequest
function fetchData(url) {
  return new Promise((resolve, reject) => {
    const xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.onload = () => {
      if (xhr.status === 200) {
        // Resolve a promessa com os dados obtidos, convertendo a resposta JSON em um objeto JavaScript
        resolve(JSON.parse(xhr.responseText));
      } else {
        // Rejeita a promessa com um erro em caso de falha na solicitação
        reject(new Error(`Failed to fetch data: ${xhr.statusText}`));
      }
    };
    xhr.onerror = () => {
      // Rejeita a promessa com um erro caso ocorra um erro na solicitação
      reject(new Error('Failed to fetch data'));
    };
    xhr.send();
  });
}

async function main() {
  try {
    // Chama a função fetchData para obter os dados de acessos da API
    const acessos = await fetchData('http://localhost:61142/api/Acessos');

    // Inicializa a tabela DataTable com os dados de acessos obtidos
    $('#tabela').DataTable({
      data: acessos,
      columns: [
        { data: 'id' },
        { data: 'nfc' },
        { data: 'arduino' },
        { data: 'localizacao' },
        { data: 'data' },
      ]
    });
  } 
  catch (error) {
    // Exibe o erro no console em caso de falha na obtenção dos dados
    console.error(error);
  }
}

$(document).ready(function() {
  // Chama a função main ao carregar o documento
  main();
});
