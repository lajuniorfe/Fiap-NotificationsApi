# 🔔 Notifications API

## 📖 Visão Geral

A **Notifications API** é o microsserviço responsável pelo processamento e envio de notificações da plataforma de jogos.

Sua principal função é consumir eventos publicados pelos demais microsserviços, processar as informações recebidas e realizar o envio de notificações aos usuários.

Neste projeto, o envio de notificações é simulado através de logs da aplicação, demonstrando o fluxo de comunicação assíncrona entre microsserviços.

A comunicação é realizada utilizando **RabbitMQ**, permitindo que os serviços sejam independentes, reduzindo o acoplamento e aumentando a escalabilidade da arquitetura.

---

# 🚀 Responsabilidades

* Consumir eventos de outros microsserviços.
* Processar notificações relacionadas às ações do sistema.
* Simular o envio de notificações aos usuários.
* Registrar logs dos eventos processados.
* Manter a comunicação assíncrona através de mensageria.

---

# 🛠️ Tecnologias Utilizadas

* .NET 10
* ASP.NET Core
* RabbitMQ
* Docker
* Kubernetes

---

# 🏗️ Arquitetura

O fluxo de notificações ocorre conforme o diagrama abaixo:

```text
RabbitMQ (user-created)
          │
          ▼
Notifications API
          │
          │ Processa notificação
          │
          ▼
     Log da aplicação


RabbitMQ (payment-processed)
          │
          ▼
Notifications API
          │
          │ Processa notificação
          │
          ▼
     Log da aplicação
```

---

# 🔄 Fluxo de Funcionamento

1. O **Users API** publica o evento `UserCreatedEvent` após a criação de um usuário.
2. O **Notifications API** consome o evento da fila `user-created`.
3. A notificação de criação de usuário é processada.

---

4. O **Payments API** publica o evento `PaymentProcessedEvent` após o processamento do pagamento.
5. O **Notifications API** consome o evento da fila `payment-processed`.
6. A notificação referente ao pagamento é processada.

---

# 📨 Filas Utilizadas

## Consome

| Fila | Evento |
|------|--------|
| `user-created` | `UserCreatedEvent` |
| `payment-processed` | `PaymentProcessedEvent` |

---

# ▶️ Executando Localmente

### Restaurar dependências

```bash
dotnet restore
```

### Executar a aplicação

```bash
dotnet run
```

---

# 🐳 Executando com Docker

A **Notifications API** pode ser executada de forma independente para fins de desenvolvimento e testes.

### Build da imagem

```bash
docker build -t notifications-api .
```

### Executar o container

```bash
docker run notifications-api
```

> **Observação:** Ao executar apenas este microsserviço, ele ficará aguardando mensagens nas filas configuradas do RabbitMQ. Para que os eventos sejam gerados e processados, é necessário que os microsserviços responsáveis pela publicação dos eventos também estejam em execução.

## 🚀 Executando a solução completa

Para simular o ambiente da aplicação de forma semelhante à produção, o recomendado é utilizar o repositório **Orchestrator**, responsável por orquestrar todos os microsserviços da plataforma.

O repositório do **Orchestrator** possui um **README** com todas as instruções necessárias para configurar e executar a solução completa. Após seguir as etapas descritas nesse repositório, basta executar:

```bash
docker compose up --build
```

Esse comando iniciará todos os componentes necessários da solução, incluindo:

- Users API
- Catalog API
- Payments API
- Notifications API
- RabbitMQ

Dessa forma, será possível testar toda a comunicação entre os microsserviços por meio de eventos, reproduzindo o funcionamento completo da arquitetura.

---

# ☸️ Executando no Kubernetes

### Aplicar os manifests

```bash
kubectl apply -f k8s/
```

### Verificar os recursos

```bash
kubectl get deployments
kubectl get pods
kubectl get services
```

### Consultar os logs

```bash
kubectl logs -f deployment/notifications-api
```

---

# 📁 Estrutura do Projeto

```text
Notifications.Api
├── Consumers
├── Events
├── Messaging
├── Services
├── Program.cs
├── appsettings.json
└── Dockerfile
```

---

# 🔗 Microsserviços Relacionados

| Microsserviço | Responsabilidade |
|---------------|------------------|
| **Users API** | Gerenciamento de usuários, autenticação e publicação de eventos. |
| **Catalog API** | Gerenciamento do catálogo de jogos e criação dos pedidos. |
| **Payments API** | Processamento dos pagamentos. |
| **Notifications API** | Consumo de eventos e envio de notificações. |

---

# 🎯 Objetivo

Este microsserviço foi desenvolvido como parte de uma arquitetura baseada em **microsserviços**, utilizando comunicação orientada a eventos com **RabbitMQ** e conteinerização com **Docker** e **Kubernetes**.

O projeto tem como objetivo demonstrar a aplicação de boas práticas de arquitetura, como:

* Separação de responsabilidades.
* Comunicação assíncrona entre serviços.
* Baixo acoplamento.
* Escalabilidade.
* Processamento orientado a eventos.
* Orquestração de containers com Kubernetes.
