# 🕒 Meu Ponto Online

**Meu Ponto Online** é um sistema de registro de ponto eletrônico criado com **ASP.NET Core Razor Pages**, utilizando o **Supabase** como backend e banco de dados PostgreSQL. Ele permite que funcionários realizem marcações de ponto com autenticação segura, geolocalização e controle de registros por tipo e horário.

---

## 🚀 Tecnologias Utilizadas

- **.NET 7 / .NET 8**
- **Razor Pages (ASP.NET Core)**
- **C#**
- **Supabase (PostgreSQL + Auth)**
- **Supabase.Client (via REST API)**
- **Bootstrap 5 + custom CSS**
- **Geolocalização via JavaScript + API de geocodificação reversa**
- **Claims Authentication com Cookie**

---

## 🧰 Funcionalidades

- 🔐 **Login com matrícula e senha (hash SHA-256)**
- 📍 **Registro de ponto com geolocalização (latitude, longitude, endereço completo)**
- 📌 **Evita registros duplicados por tipo e data**
- 👨‍💼 Cadastro de funcionários com:
  - Nome, email, matrícula, setor e função
  - Horário de entrada e saída
- 👁️ Visualização de registros pessoais
- 📑 Relatórios por funcionário, tipo de ponto e período (em desenvolvimento)
- 🌐 Interface responsiva e moderna

---

## 📦 Estrutura do Projeto

```
MeuPontoOnline/
├── Pages/
│   ├── Login/           # Página e lógica de login
│   ├── CadastroUsuarios # Cadastro de funcionário
│   ├── Registro/        # Página de registro de ponto
├── Models/              # Modelos de dados (Funcionario, RegistroPonto, Setor, Funcao)
├── Services/            # Serviços auxiliares (ex: geolocalização)
├── wwwroot/
│   ├── css/             # Estilos personalizados (Login.css, Registro.css)
├── _Layout.cshtml       # Layout base da aplicação
├── appsettings.json     # Configurações locais
└── README.md
```

---

## ⚙️ Como rodar o projeto

### ✅ Pré-requisitos

- [.NET SDK 7.0 ou superior](https://dotnet.microsoft.com/download)
- Conta no [Supabase](https://supabase.com/)
- Editor como [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### 🛠️ Passos:

1. **Clone o repositório**

```bash
git clone https://github.com/seu-usuario/meupontoonline.git
cd meupontoonline
```

2. **Configure o Supabase**

Crie as tabelas no seu projeto Supabase:

- `funcionarios`
- `registro_ponto`
- `setores`
- `funcao`

⚠️ Configure as RLS policies e índices conforme seu modelo.

3. **Configure a conexão Supabase no `Program.cs` ou via `appsettings.json`**:

```json
"Supabase": {
  "Url": "https://xxxx.supabase.co",
  "Key": "sua-chave-secreta"
}
```

4. **Rode o projeto**

```bash
dotnet run
```

Acesse via `https://localhost:7052`.

---

## 🛡️ Segurança

- Senhas dos usuários são armazenadas via hash SHA-256.
- Login utiliza `Cookie Authentication` e `Claims` para proteger páginas autenticadas.
- O ID do funcionário é extraído da `claim`, evitando manipulação de matrícula.

---

## 📍 Geolocalização

Ao registrar o ponto, o sistema obtém as coordenadas do navegador e converte para endereço completo via serviço externo (pode ser configurado com Google Maps API, OpenStreetMap, etc.).

---

## 💡 Melhorias Futuras

- 📊 Painel administrativo com relatórios
- 📈 Exportação de registros para Excel/PDF
- 🧠 Reconhecimento facial para autenticação
- 📱 Aplicativo mobile com .NET MAUI ou Blazor Hybrid
- 🔁 Integração com APIs de RH/ERP

---

## 👨‍💻 Autor

Desenvolvido por **[Marcos Vinicius]**  
📧 marcosviniciuss.dev@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/marcos-vinicius-742192245/)
