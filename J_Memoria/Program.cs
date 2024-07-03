using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace J_Memoria
{
    internal class Program
    {
        static void PrintMatrix(int[,] screen)
        {
            Console.Write("   ");
            for (int i = 1; i <= 4; i++)
            {
                Console.Write(" {0}  ", i);
            }
            Console.WriteLine("\n ----------------");

            for (int j = 0; j < 4; j++)
            {
                Console.Write("{0} |", j + 1);
                for (int k = 0; k < 4; k++)
                {
                    Console.Write(" {0} |", screen[j, k]);
                }
                Console.WriteLine("\n ----------------");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int[,] jogo = new int[4, 4];
            int[,] tela = new int[4, 4];
            int lin2, lin1, col2, col1;
            int erros = 0;
            int acertos = 0;
            int tentativas = 0;
            Player1 p1;
            Player1 p2;


            Console.WriteLine("Entre com o nome do P1: ");
            string nameP1 = Console.ReadLine();
            Console.WriteLine("Entre com o nome do P2: ");
            string nameP2 = Console.ReadLine();

            p1 = new Player1(nameP1);
            p2 = new Player1(nameP2);




            //Para criar números aleatórios.
            Random gerador = new Random();

            //Sortear jogador que começa.
            int jogador = gerador.Next(1, 2);



            do
            {
                //Imprimir o nome do jogador da vez.

                Console.WriteLine("{0} é a sua vez!", 
                    jogador == 1 ? p1.Name : p2.Name);

                //Começa a contar o tempo.
                DateTime begin= DateTime.Now;

               

                    for (int i = 1; i <= 8; i++) //Atribui os pares de números às posições.
                    {
                        //Escolhe a posição do primeiro número do par.
                        int lin, col;
                        for (int j = 0; j < 2; j++)
                        {
                            do
                            {
                                lin = gerador.Next(0, 4);
                                col = gerador.Next(0, 4);

                            } while (jogo[lin, col] != 0);
                            jogo[lin, col] = i;
                        }
                    }
                    do {
                        //Impressão da tela sem precisar repetir tudo de novo.
                        Program.PrintMatrix(tela);
                        do {
                            //Pedir as posições do primeiro número.     
                            Console.WriteLine("Escolha uma linha para jogar [1, 4]: ");
                            lin1 = int.Parse(Console.ReadLine());
                        } while (lin1 > 4 || lin1 < 1);

                        do {
                            Console.WriteLine("Escolha uma coluna para jogar [1, 4]: ");
                            col1 = int.Parse(Console.ReadLine());
                        } while (col1 > 4 || col1 < 1);

                        lin1--;
                        col1--;

                        if (tela[lin1, col1] != 0)
                        {
                            Console.WriteLine("ERRO: VOCÊ JÁ ESCOLHEU ESSA POSIÇÃO");
                        }
                    } while (tela[lin1, col1] != 0);

                    tela[lin1, col1] = jogo[lin1, col1];

                   

                    do {
                        Program.PrintMatrix(tela);
                        do
                        {
                            //Pedir as posições do segundo número.
                            Console.WriteLine("Escolha uma linha para jogar [1, 4]: ");
                            lin2 = int.Parse(Console.ReadLine());
                        } while (lin2 > 4 || lin2 < 1);
                        do {
                            Console.WriteLine("Escolha uma coluna para jogar [1, 4]: ");
                            col2 = int.Parse(Console.ReadLine());
                        } while (col2 > 4 || col2 < 1);

                        lin2--;
                        col2--;

                    } while (tela[lin2, col2] != 0);

                    tela[lin2, col2] = jogo[lin2, col2];

                    Program.PrintMatrix(tela);

                    if (jogo[lin1, col1] != jogo[lin2, col2])
                    {
                        erros++;
                    }
                    Console.WriteLine();
                    //Em caso de acerto, a matriz tela permanece como está.
                    //Em caso de erro, precisamos voltar as posições para zero.
                    if (jogo[lin1, col1] == jogo[lin2, col2])
                    {
                        if (jogador == 1)
                            p1.Score = 1;
                        else p2.Score = 1;

                       acertos++;
                    }
                    else //Caso não acertem o par.
                    {
                        TimeSpan timeSpan = DateTime.Now - begin;


                        //Soma o tempo de partida.
                        if (jogador == 1)
                            p1.GameTime = timeSpan;
                           
                        else p2.GameTime = timeSpan;

                        //Inversão de jogadores.
                        jogador = jogador % 2 + 1;
                   


                    tela[lin1, col1] = 0;
                        tela[lin2, col2] = 0;

                    Console.WriteLine($"Tempo total de jogo: {timeSpan.ToString(@"hh\:mm\:ss")}");
                }

                Console.WriteLine("Digite 0 para sair ou outro número para continuar:");
                int valor = int.Parse(Console.ReadLine());

                if (valor == 0)
                    break;

                Console.WriteLine(p1.ToString());
                Console.WriteLine(p2.ToString());

                tentativas = acertos + erros;

                Console.WriteLine("Quantidade de tentativas erradas: {0}", erros);
                Console.WriteLine("Quantidade de tentativas: {0}", tentativas);
                Console.WriteLine("Quandidade de acertos: {0}", acertos);


            } while (acertos > 8);

               
        }
    }
}

