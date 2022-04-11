using HolaMundoSocket.Comunicaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HolaMundoSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            
            if (servidor.Iniciar())
            {
                //ok puede conectar
                Console.WriteLine("Servidor Iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCliente();
                    //Construir el mecanismo para escribir  y leer 
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    //protocolo de comuniaccion de ambos, debe ser igual
                    cliente.Escribir("Hola mundo cliente, dime tu nombre?");
                    string respuesta = cliente.Leer();
                    Console.WriteLine("El cliente mando: {0}", respuesta);
                    cliente.Escribir("Hasta la vista beibi " + respuesta);
                    cliente.Desconectar();


                }




            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso", puerto);
            }


        }
    }
}
