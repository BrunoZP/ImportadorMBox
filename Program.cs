using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // Configurações do servidor IMAP
        string imapHost = "imap.servidor.com"; // Exemplo: Gmail IMAP
        int imapPort = 993; // Porta padrão para IMAP com SSL
        string username = "usuario@servidor.com";
        string password = "senhadoemail";
        string mboxFilePath = "C:\\Temp\\All mail Including Spam and Trash.mbox"; // Caminho do arquivo MBOX
        string imapFolder = "Inbox"; // Pasta IMAP de destino

        try
        {
            // Conectar ao servidor IMAP
            using (var client = new ImapClient())
            {
                client.Connect(imapHost, imapPort, true); // true para usar SSL
                client.Authenticate(username, password);

                // Abrir a pasta de destino
                var folder = client.GetFolder(imapFolder);
                folder.Open(FolderAccess.ReadWrite);

                // Ler o arquivo MBOX
                using (var stream = File.OpenRead(mboxFilePath))
                {
                    var parser = new MimeParser(stream, MimeFormat.Mbox);
                    int messageCount = 0;

                    // Iterar sobre as mensagens no arquivo MBOX
                    while (!parser.IsEndOfStream)
                    {
                        try
                        {
                            var message = parser.ParseMessage();

                            // Adicionar a mensagem à pasta IMAP
                            folder.Append(message);
                            messageCount++;

                            Console.WriteLine($"Mensagem {messageCount} importada com sucesso.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao importar mensagem: {ex.Message}");
                        }
                    }

                    Console.WriteLine($"Total de {messageCount} mensagens importadas para {imapFolder}.");
                }

                // Desconectar do servidor IMAP
                client.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro geral: {ex.Message}");
        }
    }
}