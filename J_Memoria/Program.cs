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

            Player1 p1 = new Player1("Marques");
            Player1 p2 = new Player1("Alfredo");

            Console.WriteLine("Nome: {0}", p1.Name);
            Console.WriteLine("Pontuação: {0}", p1.Score);

            Environment.Exit(0);

            //Para criar números aleatórios
            Random gerador = new Random();

            do
            {
                for (int i = 1; i <= 8; i++) //Atribui os pares de números às posições
                {
                    //Escolhe a posição do primeiro número do par
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
                        //Pedir as posições do primeiro número      
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
                        Console.WriteLine("ERRO: VOCÊ JÁ ESCOLHEU ESSA POSIÇÃO") ;
                    }
                }while (tela[lin1, col1] !=0);

                tela[lin1, col1] = jogo[lin1, col1];

                Console.WriteLine("Digite 0 para sair ");
                int valor = int.Parse(Console.ReadLine());
                if (valor == 0)
                    break;            

                do {
                    Program.PrintMatrix(tela);
                    do
                    {
                        //Pedir as posições do segundo número
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
                if (jogo[lin1, col1] != jogo[lin2, col2])
                {
                    tela[lin1, col1] = 0;
                    tela[lin2, col2] = 0;
                }
                else
                {
                    acertos++;
                } 

            }while (acertos < 8 );

            tentativas = acertos + erros;

            Console.WriteLine("Quantidade de tentativas erradas: {0}", erros);
            Console.WriteLine("Quantidade de tentativas: {0}", tentativas);

        }
    }
}
