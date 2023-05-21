# Projeto de Fechadura Elétrica com NFC

Este repositório contém o código e os recursos necessários para o projeto da disciplina UPX da Faculdade de Engenharia de Sorocaba. O projeto consiste em uma fechadura elétrica controlada por Arduino, que utiliza comunicação NFC para permitir o acesso de dispositivos autorizados. Quando um celular ou uma tag NFC autorizada é aproximada da fechadura, esta será aberta e um registro será enviado para uma API, informando o ID do Arduino, o código NFC utilizado e a localização com base no endereço IP da rede em que o Arduino estava conectado. Essas informações serão armazenadas em um banco de dados SQL Server e exibidas em uma página HTML, utilizando JavaScript, jQuery e o plugin DataTables, além do framework Bootstrap.

## Funcionalidades

- Abertura da fechadura elétrica quando um celular ou tag NFC autorizada é aproximada.
- Envio de informações para uma API após a abertura da fechadura.
- Consulta do endereço IP da rede do Arduino utilizando a API [ipinfo.io](https://ipinfo.io/).
- Armazenamento das informações em um banco de dados SQL Server.
- Exibição das informações em uma página HTML utilizando JavaScript, jQuery, DataTables e Bootstrap.

## Pré-requisitos

Antes de executar o projeto, verifique se você possui os seguintes requisitos instalados em seu ambiente de desenvolvimento:

- Arduino IDE: [https://www.arduino.cc/en/software](https://www.arduino.cc/en/software)
- Biblioteca NFC para Arduino: [Adafruit PN532](https://github.com/adafruit/Adafruit-PN532)
- Biblioteca Ethernet para Arduino: [Ethernet](https://www.arduino.cc/en/Reference/Ethernet)
- SQL Server: Certifique-se de ter um servidor SQL Server instalado e pronto para ser usado. Você pode restaurar o banco de dados utilizando o arquivo `banco.bak` fornecido neste repositório.

## Instalação

1. Clone este repositório em seu ambiente de desenvolvimento:

```shell
git clone https://github.com/seu-usuario/nome-do-repositorio.git
```

## Uso

1. Alimente o Arduino com energia.

2. Aproxime um celular ou uma tag NFC autorizada da fechadura.

3. A fechadura elétrica será aberta e as informações serão enviadas para a API, consultando o IP da rede e armazenando os dados no banco de dados.

4. Acesse a página HTML para visualizar as informações registradas na tabela, utilizando um navegador web.
