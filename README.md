# ImportadorMBox
Importa emails no formato MBox do Google para um servidor IMAP

Va em sua cona do google, e solicite para baixar seus dados (Google Takeout).
Pode marcar apenas a opção de E-mail, você receberá o link para download do arquivo em seu e-mail.
Extraia para uma pasta, um dos arquivos extraidos tem a extensão mbox, precisaremos apenas dele.

Então configure a variaveis de acordo com o seu cenário, no program.cs.
No visual studio é necessário instalar o pacote MailKit, use o comando na janela de Package Manager Console: dotnet add package MailKit

Só executar e aguardar.
Aplicativo em .net 8
