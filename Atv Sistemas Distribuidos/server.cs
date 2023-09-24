using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class server
{
    static void Main()
    {
        // Defina o endereço IP e a porta em que o servidor escutará as conexões.
        int port = 12345;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

        // Crie um socket TCP para o servidor.
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Faça o bind do socket ao ponto de extremidade.
        serverSocket.Bind(endPoint);

        // Comece a escutar por conexões.
        serverSocket.Listen(10);
        Console.WriteLine("Servidor iniciado...");

        // Aguarde por uma conexão do cliente.
        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("Cliente conectado...");

        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = clientSocket.Receive(buffer);

            // Decodifique os dados recebidos.
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Recebido: " + data);

            // Realize a operação e envie a resposta de volta para o cliente.
            double resultado = Calcular(data);
            byte[] response = Encoding.ASCII.GetBytes(resultado.ToString());
            clientSocket.Send(response);
            Console.WriteLine("Enviado: " + resultado);
        }

        // Encerre a conexão com o cliente e pare o servidor.
        clientSocket.Close();
        serverSocket.Close();
    }

    static double Calcular(string expressao)
    {
        string[] partes = expressao.Split(' ');
        double numero1 = Convert.ToDouble(partes[0]);
        double numero2 = Convert.ToDouble(partes[2]);
        string operacao = partes[1];

        double resultado = 0;
        switch (operacao)
        {
            case "+":
                resultado = numero1 + numero2;
                break;
            case "-":
                resultado = numero1 - numero2;
                break;
            case "*":
                resultado = numero1 * numero2;
                break;
            case "/":
                resultado = numero1 / numero2;
                break;
        }

        return resultado;
    }
}
