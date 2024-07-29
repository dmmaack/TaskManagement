# TaskManagement
Você foi contratado para desenvolver um sistema de gerenciamento de tarefas para uma 
pequena empresa. A empresa precisa de um sistema onde os funcionários possam criar, 
editar, deletar e visualizar tarefas. As tarefas devem ser atribuídas a usuários específicos 
e ter um status (Pendente, Em Progresso, Concluído). 

<details>
<summary>Requisitos Funcionais</summary>

### 1. Autenticação e Autorização: 
- Os usuários devem se autenticar para acessar o sistema. 
- Apenas usuários autenticados podem criar, editar, deletar e visualizar tarefas. 
- Admins podem gerenciar usuários e visualizar todas as tarefas. 
### 2. Gerenciamento de Tarefas: 
- Usuários podem criar novas tarefas, especificando título, descrição, data de vencimento e responsável. 
- Usuários podem editar e deletar suas próprias tarefas. 
- Admins podem editar e deletar qualquer tarefa. 
- Tarefas devem ter um status (Pendente, Em Progresso, Concluído). 

## Requisitos Não Funcionais 

### 1. Tecnologia: 
- Utilize ASP.NET Core para a API e Entity Framework Core para acesso ao banco de dados. 
- Utilize um banco de dados relacional (preferencialmente SQL Server). 
### 2. Padrões de Código: 
- O código deve seguir os padrões de design do .NET e boas práticas de programação. 
- A solução deve ser modular e escalável. 
### 3. Testes: 
- Inclua testes unitários para a lógica de negócio. 
- Inclua testes de integração para os endpoints da API. 
### 4. Docker: 
- Configure a aplicação para rodar em contêineres Docker. 
- Forneça um Dockerfile para a aplicação ASP.NET Core. 
- Utilize o docker-compose para orquestrar a aplicação e o banco de dados. 

## Estrutura do Projeto 
### 1. API ASP.NET Core: 
- Crie uma API com ASP.NET Core. 
- Configure o Entity Framework Core para acessar o banco de dados SQL Server. 
- Implemente autenticação e autorização usando JWT. 
### 2. Banco de Dados: 
- Utilize SQL Server como banco de dados. 
- Configure o banco de dados para rodar em um contêiner Docker. 
### 3. Docker: 
- Dockerfile: Crie um Dockerfile para a aplicação ASP.NET Core. 
- docker-compose.yml: Configure o docker-compose para orquestrar a aplicação e o banco de dados.
</details>

<details>
<summary>Configurações</summary>

**1. Baixandso código fonte:**
---
> Estou tomendo como princípio que já esteja familiarizado como Git e GitHub (conceitos e comandos).

Baixe o código fonte usando uma das opções:
- Fazendo um Fork do Repositório;
- Baixando um Clone;
- Baixando um arquivo zip do repositório;

**2. Compilando a aplicação:**
---
Foi utilizado o VSCode como IDE de codificação, então será mostrado os comandos CLI e os comandos usados na propria IDE do VSCode.

Após baixar a solution usando o Git Clone ou o Zip (e descompactando) na sua maquina local, abra a solution no VSCode.

> Pode ser usado tanto o Terminal do VSCode quanto um Terminal do próprio Windows, no meu caso eu uso o Windows Terminal Powershell

Após abrir o terminal da sua escolha, aplique os comandos para fazer o build e rodar a aplicação.

```
dotnet build
dotnet run --project TaskManagement.Api
```

Após compilar e rodar o projeto deverá aparecer essa tela:

```
Compilando...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5238
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: [caminho local]\TaskManagement\TaskManagement.Api
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
```

Para acessar a aplicação no navegador use a URL: ` http://localhost:5238/swagger/index.html `

Ok agora sabemos que está tudo certo. Vamos prosseguir.

**3. Docker compose e Dockerfile**
---
Os arquivos de criação dos containers estão localizados na pasta da Solution da aplicação, são eles:
- docker-compose.yml
- Dockerfile

  ![image](https://github.com/user-attachments/assets/3e468440-0ad6-4962-a6c6-9b5db4899b5e)

### - docker-compose.yml
Cria os containers de sqlserver e container-taskmanagement e usa um Network para fazer o Link entre os containers. O container de `container-taskmanagement` é criado a partir do arquivo `Dockerfile` que também está na pasta da Solution.

Configurações:
- `services: sqlserver: container_name:` Nome do container que vai ser criado a base de dados SQL.
- `services: sqlserver: image:` Imagem a ser usada para criar o container, essa é uma imagem do SQL Server para Developer.
- `services: sqlserver: environment: SA_PASSWORD:` Utilizar uma senha forte senão o Container do SQL Server vai ser criado com erro.
- `services: container-taskmanagement: container_name:` Nome do container que vai ser hospedada a aplicação.
- `services: container-taskmanagement: ports:` A porta usada para acesso ao container via HTTP. Não está configurado HTTPS.
---
> [OBS:]
> Somante a senha do SQL Server deve ser alterada.
---
### - Dockerfile
Está configurado para realizar a puvlicação da TaskManagement.Api.csproj e: 
- Liberar a porta HTTP 5050 `ENV ASPNETCORE_URLS=http://+:5050`
- Configurar o ambiente de Homolog `ENV ASPNETCORE_ENVIRONMENT=Homolog` para usar o arquivo de `appsettings.Homolog.json`

### appsettings.Homolog.json

Na Connection String usar:

- `Server` Nome do container que vai ser criado a base de dados SQL.
- `Password:` Usar a mesma senha configurada para criar o container do SQL Server configurado no Docker Compose: `services: sqlserver: environment: SA_PASSWORD:`.

  Exemplo:
![image](https://github.com/user-attachments/assets/86020da2-8311-47c7-9ec4-c2af964ac37f)


**3. Subindo os containers:**
---
Agora deve-se subir os ambientes de aplicação e base de dados usando os containers.

> Estou partindo do princípio que já esteja familiarizado como Containers e que sua maquina local já esteja configurada para roda-los.

No pormpt de comando, no diretório da Solution, digite a seguinte linha de comando:

[caminho local]\TaskManagement> `docker compose up`

Ese comando deve baixar as imagens e subir os containers de `sqlserver-homolog` e `container-taskmanagement`

### - Container container-taskmanagement
É o container da aplicação que conforme as configurações do Dockerfile e docker-compose deve disponibilizar a seguinte URL para acesso:

![image](https://github.com/user-attachments/assets/0967a21a-86e8-4207-939d-6ce36260083c)

### - Container sqlserver-homolog
É o container da base de dados que conforme as configurações do docker-compose deve disponibilizar acesso pelo usando as seguintes configurações:

- `Server name` localhost.
- `Authentication:` SQL Servcer Authentication
- `Login:` sa
- `Senha:` Usar a mesma senha configurada para criar o container do SQL Server configurado no Docker Compose: `services: sqlserver: environment: SA_PASSWORD:`.

> No meu caso, para abrir a base de dados eu utilizo o `Microsoft Management Studio`.

**4. Iniciando a Base de Dados:**
---
O servidor da Base de Dados ainda não conté a tabela TaskManagement, entao devemos rodar os scripts de inicialização da base que deve:

- Criar a base de dados `CreateDatabase.sql`;
- Criar a tabela de Usuarios `CreateTable_01_Users.sql`;
- Criar a tabela de Tarefas `CreateTable_02_Tasks.sql`;
- Criar a carga inicial de registros `DataLoad.sql`;

Esses scripts estão no Projeto `TaskManagement.Database` dentro da pasta `Scripts`

![image](https://github.com/user-attachments/assets/ecd448d2-ad7a-4288-a8bd-12fbfd1c70c5)

Com isso podemos começar a brincadeira.

**5 TaskManagement.Api - Swagger:**
---
A API está protegida com esquema de autenticação JWT:Bearer. Com isso é necessário gerar um Token de Autenticação para acessar os endpoints. Somente dois endpoints não necessitam de autenticação: `api/Auth/Authenticate e api/Auth/GetServerTime`. Existem diferenças entre Usuários Administradores ou Usuários normais ao executar as chamadas da API.

-- Tipos de Usuários
```
[Description("Sem Permissão")]
NoPermission = 0,

[Description("Administrador")]
Administrator = 1,

[Description("Usuario")]
User = 2,
```

### - Autenticando
- acessar o endpoint `api/Auth/Authenticate` usando um usuário que pode ser buscado na base de edados na tabela `[Users]`.
- O retorno deve trazer o Token de Autenticação.

![image](https://github.com/user-attachments/assets/294ecfa8-d409-43da-b1c4-8adef9fbf9c5)

- Ir no topo da pagina do Swagger e acessar o botão de `Authorize` onde deve abrir a tela para inserir o Token gerado anteriormente.

![image](https://github.com/user-attachments/assets/ae2b50a5-cac5-41e7-aed2-46a31ebe6852)

> Colocar a palavra Bearer antes do token ao inserir no campo Value. Exemplo:  `Bearer [token]`.

- Para validar o usuário logado, utilize o endpoint `Users > /api/v1/Users/GetLoggedUser`. A Api pega o usuário logado usando os Claims armazenados ao gerar o Token para poder `realizar as validações de tipo de Usuário e consultas com o ID do usuário` necessário para algumas regras.
- Todos os EndPoints utilizam as `informações do usuário Logado` para realizar as ações.

</details>

---

Atenciosamente[^1].

[^1]: Obrigado por acompanhar até aqui esse desenvolvimento, em caso de opiniões ou ajuda para melhorias fique a vontade para entrar em contato ou baixar o código fonte. :wink:
