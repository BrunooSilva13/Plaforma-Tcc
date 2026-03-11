# Plataforma TCC - Microservices Architecture

Projeto desenvolvido para colocar em prática conceitos de arquitetura de software e desenvolvimento backend utilizando C#.

O sistema é composto por múltiplos serviços independentes que se comunicam entre si, simulando uma arquitetura de microserviços semelhante à utilizada em empresas.

## Serviços

O sistema é dividido em quatro APIs principais:

- Customer Service — Responsável pelo cadastro e gerenciamento de clientes
- Product Service — Responsável pelo cadastro e gerenciamento de produtos
- Currency Service — Responsável por fornecer cotações de moedas estrangeiras
- Sales Service — Responsável pelo controle e registro de vendas

## Tecnologias utilizadas

- ASP.NET Core
- PostgreSQL
- Docker
- Docker Compose
- Arquitetura de Microserviços

## Estrutura do Projeto

Plataforma-Tcc/
│
├ docker-compose.yml
├ database/
│ └ init.sql
└ Services/
└ CustomerService/


