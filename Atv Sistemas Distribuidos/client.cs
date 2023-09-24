using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class client
{
    static void Main()
    {
        try
        {
            // Conecte-se ao servidor.
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 12345);
            Console.WriteLine("Conectado ao servidor...");

            while (true)
            {
                // Solicite a operação ao usuário.
                Console.Write("Digite a operação (ex. 5 + 3): ");
                string operacao = Console.ReadLine();

                // Envie a operação para o servidor.
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.ASCII.GetBytes(operacao);
                stream.Write(data, 0, data.Length);

                // Receba a resposta do servidor e imprima na tela.
                byte[] response = new byte[1024];
                int bytesRead = stream.Read(response, 0, response.Length);
                string resultado = Encoding.ASCII.GetString(response, 0, bytesRead);
                Console.WriteLine("Resultado: " + resultado);
            }

            // Encerre a conexão com o servidor.
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }
}
