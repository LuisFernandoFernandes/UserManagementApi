# UserManagementApi

API para gerenciamento de usuários e grupos de usuários.

## Funcionalidades

- Cadastro, consulta, atualização e exclusão de usuários
- Cadastro, consulta, atualização e exclusão de grupos de usuários

## Tecnologias Utilizadas

- .NET Core 8.0
- Entity Framework Core com InMemoryDatabase para persistência de dados

## Configuração do Ambiente de Desenvolvimento

1. Clone o repositório:

   ```
   git clone https://github.com/seu-usuario/seu-repositorio.git
   ```

2. Instale as dependências:

   ```
   cd UserManagementApi
   dotnet restore
   ```

3. Inicie a API:

   ```
   dotnet run
   ```

4. A API estará acessível em `http://localhost:5000`

## Endpoints

- `POST /api/Usuarios/login`: Loga com um usuário
- `GET /api/Usuarios`: Retorna a lista de usuários
- `GET /api/Usuarios/{id}`: Retorna um usuário específico
- `POST /api/Usuarios`: Cadastra um novo usuário
- `POST /api/Usuarios/creatwithvalidation`: Cadastra um novo usuário
- `PUT /api/Usuarios/{id}`: Atualiza um usuário existente
- `PUT /api/Usuarios/updatewithvalidation/{id}`: Atualiza um usuário existente validando nomes repetidos
- `DELETE /api/Usuarios/{id}`: Exclui um usuário
- `GET /api/Grupos`: Retorna a lista de grupos de usuários
- `GET /api/Grupos/{id}`: Retorna um grupo de usuário específico
- `POST /api/Grupos`: Cadastra um novo grupo de usuário
- `PUT /api/Grupos/{id}`: Atualiza um grupo de usuário existente
- `DELETE /api/Grupos/{id}`: Exclui um grupo de usuário

## Acesso Inicial

O sistema é inicializado com um usuário administrador com as seguintes credenciais:

- Nome de usuário: ADMIN
- Senha: ADMIN
- Grupo: ADMINISTRADOR
