# Чиста конфігурація для розробки
 <img src="https://accounting.org.ua/images/preferences.png?v=3" /> <b>Базові класи і функції для розробки нової програми або навчання </b> | .net 7, Linux, Windows <br/>

 <hr />

 <b>Встановлення dotnet-sdk для Ubuntu 22.10</b>
 
 Детальніше - [Install the .NET SDK or the .NET Runtime on Ubuntu](https://learn.microsoft.com/uk-ua/dotnet/core/install/linux-ubuntu)<br/>
 
    wget https://packages.microsoft.com/config/ubuntu/22.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    
    sudo apt-get update && sudo apt-get install -y dotnet-sdk-7.0
    
    # Переглянути детальну інформацію про встановлені версії sdk і runtimes
    dotnet --list-sdks && dotnet --list-runtimes

<br/>

 <b>Встановлення PostgreSQL для Ubuntu</b>
 
 Детальніше - [PostgreSQL](https://www.postgresql.org/download/linux/ubuntu/)<br/>
 
    sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'
    wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
    
    sudo apt-get update
    
    sudo apt-get -y install postgresql

    # Встановлення пароля для postgres
    sudo -u postgres psql
    \password postgres
    
    # Переглянути детальну інформацію про встановлену програму postgresql
    dpkg -l | grep postgresql

<br/>

 <b>Встановлення Git</b>
    
    sudo apt install git

<br/>

 <b>Клонування репозиторіїв</b>
    
    git clone https://github.com/tarachom/Empty.git
    git clone https://github.com/tarachom/Configurator3.git
    git clone https://github.com/tarachom/AccountingSoftwareLib.git
    
<hr />
 
  Детальніше [accounting.org.ua](https://accounting.org.ua)<br/>
  Середовище розробки [Visual Studio Code](https://code.visualstudio.com)<br/>
  База даних [PostgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)<br/>