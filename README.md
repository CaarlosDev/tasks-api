Esta é a API para o aplicativo de lista de tarefas. Ela foi desenvolvida com ASP.NET Core e usa PostgreSQL como banco de dados, totalmente configurada para rodar via Docker.

---

## 🚀 Tecnologias Utilizadas
- ASP.NET Core (.NET 9)
- PostgreSQL
- Entity Framework Core
- JWT (Autenticação)
- Docker e Docker Compose

---

## 🛠 Pré-requisitos

Antes de começar, você precisa ter instalado:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

---

## ▶️ Como rodar o projeto

1. **Clone este repositório:**

    git clone https://github.com/seu-usuario/tasks-api.git
    cd tasks-api

3. **Suba os containers com Docker Compose:**

    docker-compose up --build -d

   Isso irá:
    Criar e iniciar o container da API em http://localhost:5039
    Criar o container do banco PostgreSQL
    Aplicar as migrations automaticamente na inicialização

4. **A API estará disponível em:**

    http://localhost:5039

    POST /api/auth/register → Realiza registro de usuário
    POST /api/auth/login → Realiza login do usuário
    GET /api/tarefas → Lista tarefas
    POST /api/tarefas → Cria nova tarefa
