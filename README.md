# 💰 BancoDigitalFuncional

Sistema bancário funcional com suporte a GraphQL para operações como **depósito**, **saque** e **consulta de saldo**.

---

## 🚀 Como rodar o projeto

### ✅ Pré-requisitos

Certifique-se de ter os seguintes itens instalados na sua máquina:

- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- [MySQL](https://dev.mysql.com/downloads/)

---

### 🔧 Configuração inicial

1. **Clone o repositório**

```bash
git clone https://github.com/Gabriellabenevides/BancoDigitalFuncional
```

2. **Configure o banco de dados**

Abra o arquivo `appsettings.json` do projeto principal e verifique a `connection string`:

```json
"ConnectionStrings": {
  "BDFuncionalConnection": "server=localhost;database=bancodigital;user=root;password=root"
}
```

3. **Execute as migrações do Entity Framework**

Abra o **Console do Gerenciador de Pacotes** no Visual Studio:

- Selecione como projeto padrão o **projeto de infraestrutura MySQL**
- Rode o seguinte comando:

```powershell
Update-Database
```

> Isso criará o schema inicial do banco de dados automaticamente.

![Migração](image.png)

---

4. **Pronto para rodar e testar! 🎉**

O sistema já vem com uma conta pré-configurada no banco, criada automaticamente na inicialização, para facilitar os testes.

![Conta criada](image-1.png)

---

## 🧪 Exemplos de uso via GraphQL

### ➖ Sacar

```graphql
mutation {
  sacar(numeroConta: "123", valor: 350) {
    numeroConta
    saldo
  }
}
```

![Exemplo de saque](image-2.png)

---

### ➕ Depositar

```graphql
mutation {
  depositar(numeroConta: "123", valor: 350) {
    numeroConta
    saldo
  }
}
```

![Exemplo de depósito](image-3.png)

---

### 💼 Obter saldo

```graphql
query {
  saldo(numeroConta: "123")
}
```

![Consulta de saldo](image-4.png)

---

## 🛠️ Tecnologias usadas

- ASP.NET Core
- GraphQL (HotChocolate)
- Entity Framework Core
- Moq & xUnit (para testes unitários)
- AutoFixture (geração de dados para testes)
- Fine Code Coverage (análise de cobertura de testes)
