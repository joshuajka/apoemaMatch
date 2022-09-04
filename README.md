<h1 align="center">
  <img height=200px src="https://user-images.githubusercontent.com/16190760/188249013-66ef949a-d7ab-4c43-a903-61161c4fecd5.png" alt="Apoema">
</h1>

![Em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)
![](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

<h1 align="center"> :books: Glossário </h1>

Termo| Descrição
:---------: | :------:
Demandante | Um indivíduo que tenha uma encomenda/demanda/necessidade/problema tecnológico a ser resolvido.
Solucionador | Um indivíduo que está apto a solucionar as encomendas/demandas cadastradas na plataforma
Encomenda | Uma necessidade, um problema ou uma demanda tecnológica por parte do demandante
Agenciador | O Administrador da plataforma, que gerencia todo o processo de matchmaking.

<h1 align="center"> :page_facing_up: Descrição do projeto </h1>
Esse é um projeto em desenvolvimento para a disciplina RESIDÊNCIA TÉCNICA EM SISTEMAS DE INFORMAÇÃO do curso de Sistemas de Informação da Universidade Federal de Goiás (UFG). 
Nós somos duas squads, uma focada na Encomenda e suas funcionalidades, e a outra abrangendo o resto da plataforma, especialmente no matching entre a encomenda e o solucionador mais apto para solucioná-la. 

Nos dedicamos no projeto por cerca de 3 meses e meio, com instruções dos professores orientadores e com reuniões semanais com Product Owner, passando pelas etapas de entender os problemas da organização, analisar se a solução proposta conseguiria trazer valor para o Apoema conforme esperado, para só então pensar na solução tecnológica e colocar a mão na massa.

<h1 align="center"> :hammer: Funcionalidades </h1>

Funcionalidade| Descrição
:---------: | :------:
01| Cadastro do Demandante e Solucionador
02| Listagem de todos os usuários da plataforma para gerência do agenciador
03| Visualizar e alterar meus dados para o Demandante, o Solucionador e o Agenciador
04| Login e Recuperação de senha do Agenciador, Demandante e Solucionador
05| Alteração de senha e e-mail do Agenciador, Demandante e Solucionador
06| Ação de Vinculação do solucionador à encomenda por parte do Agenciador
07| Ação de Alteração do solucionador de uma encomenda por parte do Agenciador
08|
09|
10|

<h1 align="center"> :file_folder: Acesso ao projeto </h1>

Você pode [acessar o código fonte do projeto inicial](https://github.com/joshuajka/apoemaMatch), clonar o repositório digitando `git clone https://github.com/joshuajka/apoemaMatch.git` no seu terminal ou [baixá-lo](https://github.com/joshuajka/apoemaMatch/archive/refs/heads/master.zip).

<h1 align="center"> :heavy_check_mark: Técnicas e Tecnologias utilizadas </h1>

Como tecnologias principais a serem utilizadas no projeto, o time de desenvolvimento optou por trabalhar com ferramentas atuais e que tragam agilidade no processo de desenvolvimento da aplicação. Para o front-end, foi escolhido o Bootstrap, um framework front-end que fornece estruturas de CSS para a criação de sites e aplicações responsivas de forma rápida e simples. Além disso, pode lidar com sites de desktop e páginas de dispositivos móveis da mesma forma. No back-end, utilizamos .NET Core MVC, um ambiente de execução criado pela Microsoft e multiplataforma que oferece uma série de serviços voltados ao desenvolvimento web, reutilizando e reaproveitando códigos, entre suas principais funções. No que tange o banco de dados o time elegeu a tecnologia PostgreSQL (Banco de dados Relacional) para o uso. O padrão do projeto será o MVC (Model-View-Controller).

[Visual Studio Community 2019 (version 16.11)](https://my.visualstudio.com/Downloads?q=visual%20studio%202019&wt.mc_id=o~msft~vscom~older-downloads)

[.NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

[POSTGRES 14.5](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

senha: root 
usuario: postgres

Tutorial do youtube que usamos como base: [ASP.NET MVC | BUILD A COMPLETE eCOMMERCE APPLICATION](https://www.youtube.com/playlist?list=PL2Q8rFbm-4ruTcZY39MNOsEu4p76HQ5VX)


<h1 align="center"> :pencil: Tutorial: Disponibilizando a plataforma por uma VM na GCP</h1>

<h5>Criando a VM no Google Cloud</h5>
 Nesse tópico, vamos criar uma [VM no GCP](https://console.cloud.google.com/compute/instancesAdd), dê o nome da sua preferência, e modifique as configurações como litado abaixo.


Coloque a região e zona de sua preferência. Pessoalmente, eu prefiro a região us-west1 (Oregon) e a Zona us-west1-c.

Nas configurações de máquina, marque a Série como *N1* e o Tipo de máquina como *f1-micro (1 vCPU, 614 MB de memória)*. Para fins de teste e apresentação para os professores, colegas e Product Owner, não é necessário uma máquina robusta.

No Disco de inicialização, clique em Mudar, escolha 
Ubuntu como Sistema operacional e clique em Selecionar

E no tópico Firewall, marque as opções *Permitir tráfego HTTP* e *Permitir tráfegos HTTPS*

Por fim, clique em *Criar* e aguarde a sua instância ser criada.

Na coluna IP Externo, você verá o IP da sua máquina virtual, que as outras pessoas terão acesso. Já na coluna Conectar, você verá o botão SSH. Clique nele para abrir o terminal da sua VM.

<h5>Fazendo o Update e Upgrade</h5>

```bash
sudo apt update
sudo apt upgrade -y
sudo apt install wget
```

<h5>Instalando o SDK do .NET</h5>

Baixando pacotes necessários

```bash
wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```

Instalando
```bash
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-5.0
```

<h5>Instalando e configurando o Postgresql</h5>

```bash
sudo apt -y install postgresql
```

Definindo a senha
```bash
sudo -u postgres psql
\password
```

Digite “root” quando aparecer a mensagem “Enter new password for user "postgres":” E por fim, digite \q para sair do postgres

```postgresql
\q
```

<h5>Instalando e configurando o NGINX</h5>

```bash
sudo apt-get install nginx -y
```

Nesse tutorial, estou usando o editor de texto [nano](www.nano-editor.org). Sinta-se a vontade para utilizar o editor de texto de sua preferência.

Digite o comando abaixo para abrir o arquivo de configuração.
```bash
sudo nano /etc/nginx/sites-enabled/default 
```

Apague tudo o que estiver nele e cole dentro do arquivo o bloco abaixo.

```bash
server {
    listen        80;
    server_name _;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```

Utilize Ctrl+O para salvar, e Ctrl+X para sair do editor.

Caso seja necessário, para manter controle do processo nginx, usamos os comandos abaixo, mas por ora, use o comando sudo `systemctl status nginx`  para verificar se o processo está ativo
```bash
sudo systemctl start nginx
sudo systemctl restart nginx
sudo systemctl stop nginx
sudo systemctl status nginx 
```

<h5>Clonando o repositório</h5>


Para deixar o tutorial mais automático, usamos ```$USER``` para recuperar o seu nome de usuário no comandos abaixo. Você pode visualizar seu nome de usuário usando ```echo $USER```

```bash
cd /home/$USER/
git clone https://github.com/joshuajka/apoemaMatch.git
```

Para entrar na pasta do projeto, digite:
```bash
ls
cd apoemaMatch/apoemaMatch/
```

Testando se está tudo ok
```dotnet build```

<h5>Criando um serviço para o serviço se reiniciar</h5>

Digite o comando para criar um arquivo e cole o bloco abaixo nele, trocando ‘digite-seu-usuario-aqui’pelo seu usuário. Caso o diretório seja diferente, altere conforme necessário.
```sudo nano /etc/systemd/system/apoema.service```

```
[Unit]
Description=ASP .NET Web Application
[Service]
WorkingDirectory=/home/digite-seu-usuario-aqui/apoemaMatch/apoemaMatch/
ExecStart=/usr/bin/dotnet /home/digite-seu-usuario-aqui/apoemaMatch/apoemaMatch/bin/Debug/net5.0/apoemaMatch.dll
Restart=always
RestartSec=10
SyslogIdentifier=apoema
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
[Install]
WantedBy=multi-user.target
```

Ao executar ```sudo systemctl status apoema.service``` notaremos que o serviço está inativo, então vamos ativá-lo: 

```
sudo systemctl enable apoema.service 
sudo systemctl restart apoema.service 
sudo systemctl status apoema.service 
```

Vamos testar pelo comando abaixo, ou pelo IP Externo da sua VM:

```
wget localhost
cat index.html
rm -rf index.html
```

<h5>Ainda está aparecendo a página de “Welcome to nginx!”?</h5>

Utilize o comando ```nano Startup.cs``` 
procure a linha app.UseHttpsRedirection(); e comente-a, então a linha ficará assim:
```//app.UseHttpsRedirection();```

Logo após, você precisará parar o serviço, fazer o build e restartar o serviço novamente:
```
sudo systemctl stop apoema.service 
dotnet build
sudo systemctl restart apoema.service 
sudo systemctl status apoema.service 
```

Agora deve funcionar, tanto o código abaixo, quanto o IP Externo da sua VM. Cole no seu navegador *http://IP_EXTERNO* substituindo IP_EXTERNO pelo encontrado na [página contendo as suas VMs](https://console.cloud.google.com/compute/instances) na coluna IP externo da sua VM.

```
cd /home/$USER/
wget localhost
cat index.html
rm -rf index.html
```

<h5>Para atualizar a VM com uma nova versão</h5>

Pare o servico com ```sudo systemctl stop apoema.service```, faça o clone novamente, com as alterações da nova versão: ```git clone https://github.com/joshuajka/apoemaMatch.git```

Entre no postgres e delete o banco
```sudo -u postgres psql
drop database "apoemamatch-app-db";
```
Utilize ```\c "apoemamatch-app-db"``` para verificar se o banco ainda existe, e caso exista faça o drop novamente, conferindo o nome do banco.

E utilize o comando abaixo para fazer o update do banco:
```
dotnet tool install -g dotnet-ef --version 5
dotnet ef database update
```

Então, é só fazer o build novamente e restartar o serviço
```
dotnet build
sudo systemctl restart apoema.service 
sudo systemctl status apoema.service 
```
