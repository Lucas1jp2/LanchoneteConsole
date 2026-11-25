using LanchoneteConsole.DAO;
using LanchoneteConsole.Database;
using LanchoneteConsole.Mapeamento;
using System;
using System.Collections.Generic;

namespace LanchoneteConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("=== LANCHONETE CONSOLE - MENU PRINCIPAL ===");
                    Console.WriteLine("1 - Pais");
                    Console.WriteLine("2 - Estado");
                    Console.WriteLine("3 - Cidade");
                    Console.WriteLine("4 - Bairro");
                    Console.WriteLine("5 - Endereco");
                    Console.WriteLine("6 - Cliente");
                    Console.WriteLine("7 - Funcionario");
                    Console.WriteLine("8 - Produto");
                    Console.WriteLine("9 - Combo");
                    Console.WriteLine("10 - Mesa");
                    Console.WriteLine("11 - Pedido");
                    Console.WriteLine("12 - ItemPedido");
                    Console.WriteLine("13 - Pagamento");
                    Console.WriteLine("14 - Entrega");
                    Console.WriteLine("0 - Sair");
                    Console.Write("Escolha uma opção: ");

                    int opcaoEntidade = int.Parse(Console.ReadLine());

                    switch (opcaoEntidade)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 1 - PAIS ===");
                            MenuPais();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 2 - ESTADO ===");
                            MenuEstado();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 3 - CIDADE ===");
                            MenuCidade();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 4 - BAIRRO ===");
                            MenuBairro();
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 5 - ENDERECO ===");
                            MenuEndereco();
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 6 - CLIENTE ===");
                            MenuCliente();
                            break;
                        case 7:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 7 - FUNCIONARIO ===");
                            MenuFuncionario();
                            break;
                        case 8:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 8 - PRODUTO ===");
                            MenuProduto();
                            break;
                        case 9:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 9 - COMBO ===");
                            MenuCombo();
                            break;
                        case 10:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 10 - MESA ===");
                            MenuMesa();
                            break;
                        case 11:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 11 - PEDIDO ===");
                            MenuPedido();
                            break;
                        case 12:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 12 - ITEMPEDIDO ===");
                            MenuItemPedido();
                            break;
                        case 13:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 13 - PAGAMENTO ===");
                            MenuPagamento();
                            break;
                        case 14:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 14 - ENTREGA ===");
                            MenuEntrega();
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO ESCOLHIDA: 0 - SAIR ===");
                            Console.WriteLine("Encerrando o sistema...");
                            executando = false;
                            PausarTela();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("=== OPÇÃO INVÁLIDA ===");
                            Console.WriteLine("Escolha um número válido do menu.");
                            PausarTela();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("=== ERRO ===");
                    Console.WriteLine(ex.Message);
                    PausarTela();
                }
            }
        }

        static void PausarTela()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }

        static int LerOperacao(string entidade)
        {
            Console.WriteLine($"\n--- OPERAÇÕES DE {entidade.ToUpper()} ---");
            Console.WriteLine("1 - Cadastrar (INSERT)");
            Console.WriteLine("2 - Atualizar (UPDATE)");
            Console.WriteLine("3 - Deletar (DELETE)");
            Console.WriteLine("4 - Listar todos (GetAll)");
            Console.WriteLine("5 - Buscar por ID");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha a operação: ");
            int op = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"=== {entidade.ToUpper()} - OPÇÃO ESCOLHIDA: {op} ===");
            return op;
        }

        static void MenuPais()
        {
            PaisDAO dao = new PaisDAO();
            switch (LerOperacao("Pais"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) PAIS");
                    Pais pNovo = new Pais();
                    Console.Write("Nome do país: ");
                    pNovo.Nome = Console.ReadLine();
                    dao.Insert(pNovo);
                    Console.WriteLine("Resultado: País cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) PAIS");
                    Pais pAlt = new Pais();
                    Console.Write("ID do país para atualizar: ");
                    pAlt.PaisID = int.Parse(Console.ReadLine());
                    Console.Write("Novo nome do país: ");
                    pAlt.Nome = Console.ReadLine();
                    dao.Update(pAlt);
                    Console.WriteLine("Resultado: País atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) PAIS");
                    Console.Write("ID do país para deletar: ");
                    int idDel = int.Parse(Console.ReadLine());
                    dao.Delete(idDel);
                    Console.WriteLine("Resultado: País deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) PAIS");
                    foreach (Pais item in dao.GetAll())
                        Console.WriteLine($"{item.PaisID} - {item.Nome}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID PAIS");
                    Console.Write("ID do país: ");
                    Pais pBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {pBusca.PaisID} - {pBusca.Nome}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuEstado()
        {
            EstadoDAO dao = new EstadoDAO();
            switch (LerOperacao("Estado"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) ESTADO");
                    Estado eNovo = new Estado();
                    Console.Write("Nome do estado: ");
                    eNovo.Nome = Console.ReadLine();
                    Console.Write("ID do país (fk_paisID): ");
                    eNovo.fk_paisID = int.Parse(Console.ReadLine());
                    dao.Insert(eNovo);
                    Console.WriteLine("Resultado: Estado cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) ESTADO");
                    Estado eAlt = new Estado();
                    Console.Write("ID do estado para atualizar: ");
                    eAlt.EstadoID = int.Parse(Console.ReadLine());
                    Console.Write("Novo nome do estado: ");
                    eAlt.Nome = Console.ReadLine();
                    Console.Write("ID do país (fk_paisID): ");
                    eAlt.fk_paisID = int.Parse(Console.ReadLine());
                    dao.Update(eAlt);
                    Console.WriteLine("Resultado: Estado atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) ESTADO");
                    Console.Write("ID do estado para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Estado deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) ESTADO");
                    foreach (Estado item in dao.GetAll())
                        Console.WriteLine($"{item.EstadoID} - {item.Nome} (País: {item.fk_paisID})");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID ESTADO");
                    Console.Write("ID do estado: ");
                    Estado eBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {eBusca.EstadoID} - {eBusca.Nome} (País: {eBusca.fk_paisID})");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuCidade()
        {
            CidadeDAO dao = new CidadeDAO();
            switch (LerOperacao("Cidade"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) CIDADE");
                    Cidade cNovo = new Cidade();
                    Console.Write("Nome da cidade: ");
                    cNovo.Nome = Console.ReadLine();
                    Console.Write("ID do estado (fk_estadoID): ");
                    cNovo.fk_estadoID = int.Parse(Console.ReadLine());
                    dao.Insert(cNovo);
                    Console.WriteLine("Resultado: Cidade cadastrada com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) CIDADE");
                    Cidade cAlt = new Cidade();
                    Console.Write("ID da cidade para atualizar: ");
                    cAlt.CidadeID = int.Parse(Console.ReadLine());
                    Console.Write("Novo nome da cidade: ");
                    cAlt.Nome = Console.ReadLine();
                    Console.Write("ID do estado (fk_estadoID): ");
                    cAlt.fk_estadoID = int.Parse(Console.ReadLine());
                    dao.Update(cAlt);
                    Console.WriteLine("Resultado: Cidade atualizada com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) CIDADE");
                    Console.Write("ID da cidade para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Cidade deletada com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) CIDADE");
                    foreach (Cidade item in dao.GetAll())
                        Console.WriteLine($"{item.CidadeID} - {item.Nome} (Estado: {item.fk_estadoID})");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID CIDADE");
                    Console.Write("ID da cidade: ");
                    Cidade cBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {cBusca.CidadeID} - {cBusca.Nome} (Estado: {cBusca.fk_estadoID})");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuBairro()
        {
            BairroDAO dao = new BairroDAO();
            switch (LerOperacao("Bairro"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) BAIRRO");
                    Bairro bNovo = new Bairro();
                    Console.Write("Nome do bairro: ");
                    bNovo.Nome = Console.ReadLine();
                    Console.Write("Taxa de entrega: ");
                    bNovo.Taxa = decimal.Parse(Console.ReadLine());
                    Console.Write("ID da cidade (fk_cidadeID): ");
                    bNovo.fk_cidadeID = int.Parse(Console.ReadLine());
                    dao.Insert(bNovo);
                    Console.WriteLine("Resultado: Bairro cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) BAIRRO");
                    Bairro bAlt = new Bairro();
                    Console.Write("ID do bairro para atualizar: ");
                    bAlt.BairroID = int.Parse(Console.ReadLine());
                    Console.Write("Novo nome do bairro: ");
                    bAlt.Nome = Console.ReadLine();
                    Console.Write("Nova taxa de entrega: ");
                    bAlt.Taxa = decimal.Parse(Console.ReadLine());
                    Console.Write("ID da cidade (fk_cidadeID): ");
                    bAlt.fk_cidadeID = int.Parse(Console.ReadLine());
                    dao.Update(bAlt);
                    Console.WriteLine("Resultado: Bairro atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) BAIRRO");
                    Console.Write("ID do bairro para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Bairro deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) BAIRRO");
                    foreach (Bairro item in dao.GetAll())
                        Console.WriteLine($"{item.BairroID} - {item.Nome} | Taxa: {item.Taxa} (Cidade: {item.fk_cidadeID})");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID BAIRRO");
                    Console.Write("ID do bairro: ");
                    Bairro bBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {bBusca.BairroID} - {bBusca.Nome} | Taxa: {bBusca.Taxa}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuEndereco()
        {
            EnderecoDAO dao = new EnderecoDAO();
            switch (LerOperacao("Endereco"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) ENDERECO");
                    Endereco endNovo = new Endereco();
                    Console.Write("Número (ou deixe vazio): ");
                    string numStr = Console.ReadLine();
                    endNovo.Numero = string.IsNullOrEmpty(numStr) ? (int?)null : int.Parse(numStr);
                    Console.Write("Observação: ");
                    endNovo.Observacao = Console.ReadLine();
                    Console.Write("ID do bairro (fk_bairroID): ");
                    endNovo.fk_bairroID = int.Parse(Console.ReadLine());
                    dao.Insert(endNovo);
                    Console.WriteLine("Resultado: Endereço cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) ENDERECO");
                    Endereco endAlt = new Endereco();
                    Console.Write("ID do endereço para atualizar: ");
                    endAlt.EnderecoID = int.Parse(Console.ReadLine());
                    Console.Write("Número (ou deixe vazio): ");
                    string numAlt = Console.ReadLine();
                    endAlt.Numero = string.IsNullOrEmpty(numAlt) ? (int?)null : int.Parse(numAlt);
                    Console.Write("Observação: ");
                    endAlt.Observacao = Console.ReadLine();
                    Console.Write("ID do bairro (fk_bairroID): ");
                    endAlt.fk_bairroID = int.Parse(Console.ReadLine());
                    dao.Update(endAlt);
                    Console.WriteLine("Resultado: Endereço atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) ENDERECO");
                    Console.Write("ID do endereço para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Endereço deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) ENDERECO");
                    foreach (Endereco item in dao.GetAll())
                        Console.WriteLine($"{item.EnderecoID} - Nº {item.Numero} | {item.Observacao} (Bairro: {item.fk_bairroID})");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID ENDERECO");
                    Console.Write("ID do endereço: ");
                    Endereco endBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {endBusca.EnderecoID} - Nº {endBusca.Numero} | {endBusca.Observacao}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuCliente()
        {
            ClienteDAO dao = new ClienteDAO();
            switch (LerOperacao("Cliente"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) CLIENTE");
                    Cliente cNovo = new Cliente();
                    Console.Write("Nome: ");
                    cNovo.Nome = Console.ReadLine();
                    Console.Write("Celular: ");
                    cNovo.Celular = Console.ReadLine();
                    Console.Write("Data nascimento (aaaa-mm-dd ou vazio): ");
                    string dn = Console.ReadLine();
                    cNovo.DataNasc = string.IsNullOrEmpty(dn) ? (DateOnly?)null : DateOnly.Parse(dn);
                    Console.Write("ID endereço (ou vazio): ");
                    string fkEnd = Console.ReadLine();
                    cNovo.fk_enderecoID = string.IsNullOrEmpty(fkEnd) ? (int?)null : int.Parse(fkEnd);
                    dao.Insert(cNovo);
                    Console.WriteLine("Resultado: Cliente cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) CLIENTE");
                    Cliente cAlt = new Cliente();
                    Console.Write("ID do cliente para atualizar: ");
                    cAlt.ClienteID = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    cAlt.Nome = Console.ReadLine();
                    Console.Write("Celular: ");
                    cAlt.Celular = Console.ReadLine();
                    Console.Write("Data nascimento (aaaa-mm-dd ou vazio): ");
                    string dnAlt = Console.ReadLine();
                    cAlt.DataNasc = string.IsNullOrEmpty(dnAlt) ? (DateOnly?)null : DateOnly.Parse(dnAlt);
                    Console.Write("ID endereço (ou vazio): ");
                    string fkEndAlt = Console.ReadLine();
                    cAlt.fk_enderecoID = string.IsNullOrEmpty(fkEndAlt) ? (int?)null : int.Parse(fkEndAlt);
                    dao.Update(cAlt);
                    Console.WriteLine("Resultado: Cliente atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) CLIENTE");
                    Console.Write("ID do cliente para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Cliente deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) CLIENTE");
                    foreach (Cliente item in dao.GetAll())
                        Console.WriteLine($"{item.ClienteID} - {item.Nome} | {item.Celular}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID CLIENTE");
                    Console.Write("ID do cliente: ");
                    Cliente cBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {cBusca.ClienteID} - {cBusca.Nome} | {cBusca.Celular}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuFuncionario()
        {
            FuncionarioDAO dao = new FuncionarioDAO();
            switch (LerOperacao("Funcionario"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) FUNCIONARIO");
                    Funcionario fNovo = new Funcionario();
                    Console.Write("Nome: ");
                    fNovo.Nome = Console.ReadLine();
                    Console.Write("Cargo: ");
                    fNovo.Cargo = Console.ReadLine();
                    Console.Write("Email: ");
                    fNovo.Email = Console.ReadLine();
                    Console.Write("Celular: ");
                    fNovo.Celular = Console.ReadLine();
                    Console.Write("CPF: ");
                    fNovo.Cpf = Console.ReadLine();
                    Console.Write("Salário: ");
                    fNovo.Salario = decimal.Parse(Console.ReadLine());
                    Console.Write("Data nascimento (aaaa-mm-dd ou vazio): ");
                    string dn = Console.ReadLine();
                    fNovo.DataNasc = string.IsNullOrEmpty(dn) ? (DateOnly?)null : DateOnly.Parse(dn);
                    Console.Write("ID endereço (ou vazio): ");
                    string fkEnd = Console.ReadLine();
                    fNovo.fk_enderecoID = string.IsNullOrEmpty(fkEnd) ? (int?)null : int.Parse(fkEnd);
                    dao.Insert(fNovo);
                    Console.WriteLine("Resultado: Funcionário cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) FUNCIONARIO");
                    Funcionario fAlt = new Funcionario();
                    Console.Write("ID do funcionário para atualizar: ");
                    fAlt.FuncionarioID = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    fAlt.Nome = Console.ReadLine();
                    Console.Write("Cargo: ");
                    fAlt.Cargo = Console.ReadLine();
                    Console.Write("Email: ");
                    fAlt.Email = Console.ReadLine();
                    Console.Write("Celular: ");
                    fAlt.Celular = Console.ReadLine();
                    Console.Write("CPF: ");
                    fAlt.Cpf = Console.ReadLine();
                    Console.Write("Salário: ");
                    fAlt.Salario = decimal.Parse(Console.ReadLine());
                    Console.Write("Data nascimento (aaaa-mm-dd ou vazio): ");
                    string dnAlt = Console.ReadLine();
                    fAlt.DataNasc = string.IsNullOrEmpty(dnAlt) ? (DateOnly?)null : DateOnly.Parse(dnAlt);
                    Console.Write("ID endereço (ou vazio): ");
                    string fkEndAlt = Console.ReadLine();
                    fAlt.fk_enderecoID = string.IsNullOrEmpty(fkEndAlt) ? (int?)null : int.Parse(fkEndAlt);
                    dao.Update(fAlt);
                    Console.WriteLine("Resultado: Funcionário atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) FUNCIONARIO");
                    Console.Write("ID do funcionário para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Funcionário deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) FUNCIONARIO");
                    foreach (Funcionario item in dao.GetAll())
                        Console.WriteLine($"{item.FuncionarioID} - {item.Nome} | {item.Cargo} | {item.Salario}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID FUNCIONARIO");
                    Console.Write("ID do funcionário: ");
                    Funcionario fBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {fBusca.FuncionarioID} - {fBusca.Nome} | {fBusca.Cargo}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuProduto()
        {
            ProdutoDAO dao = new ProdutoDAO();
            switch (LerOperacao("Produto"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) PRODUTO");
                    Produto prNovo = new Produto();
                    Console.Write("Nome: ");
                    prNovo.Nome = Console.ReadLine();
                    Console.Write("Tipo (LANCHE, BEBIDA, OUTROS): ");
                    prNovo.Tipo = Console.ReadLine();
                    Console.Write("Valor: ");
                    prNovo.Valor = decimal.Parse(Console.ReadLine());
                    dao.Insert(prNovo);
                    Console.WriteLine("Resultado: Produto cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) PRODUTO");
                    Produto prAlt = new Produto();
                    Console.Write("ID do produto para atualizar: ");
                    prAlt.ProdutoID = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    prAlt.Nome = Console.ReadLine();
                    Console.Write("Tipo (LANCHE, BEBIDA, OUTROS): ");
                    prAlt.Tipo = Console.ReadLine();
                    Console.Write("Valor: ");
                    prAlt.Valor = decimal.Parse(Console.ReadLine());
                    dao.Update(prAlt);
                    Console.WriteLine("Resultado: Produto atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) PRODUTO");
                    Console.Write("ID do produto para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Produto deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) PRODUTO");
                    foreach (Produto item in dao.GetAll())
                        Console.WriteLine($"{item.ProdutoID} - {item.Nome} ({item.Tipo}) - R$ {item.Valor}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID PRODUTO");
                    Console.Write("ID do produto: ");
                    Produto prBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {prBusca.ProdutoID} - {prBusca.Nome} - R$ {prBusca.Valor}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuCombo()
        {
            ComboDAO dao = new ComboDAO();
            switch (LerOperacao("Combo"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) COMBO");
                    Combo cbNovo = new Combo();
                    Console.Write("Nome: ");
                    cbNovo.Nome = Console.ReadLine();
                    Console.Write("Valor: ");
                    cbNovo.Valor = decimal.Parse(Console.ReadLine());
                    Console.Write("Data início (aaaa-mm-dd): ");
                    cbNovo.DataInicio = DateOnly.Parse(Console.ReadLine());
                    Console.Write("Data fim (aaaa-mm-dd): ");
                    cbNovo.DataFim = DateOnly.Parse(Console.ReadLine());
                    dao.Insert(cbNovo);
                    Console.WriteLine("Resultado: Combo cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) COMBO");
                    Combo cbAlt = new Combo();
                    Console.Write("ID do combo para atualizar: ");
                    cbAlt.ComboID = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    cbAlt.Nome = Console.ReadLine();
                    Console.Write("Valor: ");
                    cbAlt.Valor = decimal.Parse(Console.ReadLine());
                    Console.Write("Data início (aaaa-mm-dd): ");
                    cbAlt.DataInicio = DateOnly.Parse(Console.ReadLine());
                    Console.Write("Data fim (aaaa-mm-dd): ");
                    cbAlt.DataFim = DateOnly.Parse(Console.ReadLine());
                    dao.Update(cbAlt);
                    Console.WriteLine("Resultado: Combo atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) COMBO");
                    Console.Write("ID do combo para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Combo deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) COMBO");
                    foreach (Combo item in dao.GetAll())
                        Console.WriteLine($"{item.ComboID} - {item.Nome} - R$ {item.Valor}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID COMBO");
                    Console.Write("ID do combo: ");
                    Combo cbBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {cbBusca.ComboID} - {cbBusca.Nome} - R$ {cbBusca.Valor}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuMesa()
        {
            MesaDAO dao = new MesaDAO();
            switch (LerOperacao("Mesa"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) MESA");
                    Mesa mNovo = new Mesa();
                    Console.Write("Localização: ");
                    mNovo.Localizacao = Console.ReadLine();
                    dao.Insert(mNovo);
                    Console.WriteLine("Resultado: Mesa cadastrada com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) MESA");
                    Mesa mAlt = new Mesa();
                    Console.Write("ID da mesa para atualizar: ");
                    mAlt.MesaID = int.Parse(Console.ReadLine());
                    Console.Write("Localização: ");
                    mAlt.Localizacao = Console.ReadLine();
                    dao.Update(mAlt);
                    Console.WriteLine("Resultado: Mesa atualizada com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) MESA");
                    Console.Write("ID da mesa para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Mesa deletada com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) MESA");
                    foreach (Mesa item in dao.GetAll())
                        Console.WriteLine($"{item.MesaID} - {item.Localizacao}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID MESA");
                    Console.Write("ID da mesa: ");
                    Mesa mBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {mBusca.MesaID} - {mBusca.Localizacao}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuPedido()
        {
            PedidoDAO dao = new PedidoDAO();
            switch (LerOperacao("Pedido"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) PEDIDO");
                    Pedido pdNovo = new Pedido();
                    pdNovo.DataPedido = DateOnly.FromDateTime(DateTime.Now);
                    Console.Write("Preferencial (true/false): ");
                    pdNovo.Preferencial = bool.Parse(Console.ReadLine());
                    Console.Write("Status: ");
                    pdNovo.Status = Console.ReadLine();
                    Console.Write("Observação: ");
                    pdNovo.Observacao = Console.ReadLine();
                    Console.Write("Tipo (ENTREGA, RETIRAR, MESA): ");
                    pdNovo.Tipo = Console.ReadLine();
                    Console.Write("Total: ");
                    pdNovo.Total = decimal.Parse(Console.ReadLine());
                    Console.Write("ID funcionário (fk_funcionarioID): ");
                    pdNovo.fk_funcionarioID = int.Parse(Console.ReadLine());
                    Console.Write("ID cliente (ou vazio): ");
                    string cli = Console.ReadLine();
                    pdNovo.fk_clienteID = string.IsNullOrEmpty(cli) ? (int?)null : int.Parse(cli);
                    dao.Insert(pdNovo);
                    Console.WriteLine("Resultado: Pedido cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) PEDIDO");
                    Pedido pdAlt = new Pedido();
                    Console.Write("ID do pedido para atualizar: ");
                    pdAlt.PedidoID = int.Parse(Console.ReadLine());
                    pdAlt.DataPedido = DateOnly.FromDateTime(DateTime.Now);
                    Console.Write("Preferencial (true/false): ");
                    pdAlt.Preferencial = bool.Parse(Console.ReadLine());
                    Console.Write("Status: ");
                    pdAlt.Status = Console.ReadLine();
                    Console.Write("Observação: ");
                    pdAlt.Observacao = Console.ReadLine();
                    Console.Write("Tipo: ");
                    pdAlt.Tipo = Console.ReadLine();
                    Console.Write("Total: ");
                    pdAlt.Total = decimal.Parse(Console.ReadLine());
                    Console.Write("ID funcionário (fk_funcionarioID): ");
                    pdAlt.fk_funcionarioID = int.Parse(Console.ReadLine());
                    Console.Write("ID cliente (ou vazio): ");
                    string cliAlt = Console.ReadLine();
                    pdAlt.fk_clienteID = string.IsNullOrEmpty(cliAlt) ? (int?)null : int.Parse(cliAlt);
                    dao.Update(pdAlt);
                    Console.WriteLine("Resultado: Pedido atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) PEDIDO");
                    Console.Write("ID do pedido para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Pedido deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) PEDIDO");
                    foreach (Pedido item in dao.GetAll())
                        Console.WriteLine($"{item.PedidoID} | {item.DataPedido} | {item.Status} | R$ {item.Total}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID PEDIDO");
                    Console.Write("ID do pedido: ");
                    Pedido pdBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {pdBusca.PedidoID} | {pdBusca.Status} | R$ {pdBusca.Total}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuItemPedido()
        {
            ItemPedidoDAO dao = new ItemPedidoDAO();
            switch (LerOperacao("ItemPedido"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) ITEMPEDIDO");
                    ItemPedido ipNovo = new ItemPedido();
                    Console.Write("Quantidade: ");
                    ipNovo.Quantidade = int.Parse(Console.ReadLine());
                    Console.Write("Valor unitário: ");
                    ipNovo.ValorUnitario = decimal.Parse(Console.ReadLine());
                    Console.Write("Total item: ");
                    ipNovo.TotalItem = decimal.Parse(Console.ReadLine());
                    Console.Write("Observação: ");
                    ipNovo.Observacao = Console.ReadLine();
                    Console.Write("ID produto (fk_produtoID): ");
                    ipNovo.fk_produtoID = int.Parse(Console.ReadLine());
                    Console.Write("ID pedido (fk_pedidoID): ");
                    ipNovo.fk_pedidoID = int.Parse(Console.ReadLine());
                    dao.Insert(ipNovo);
                    Console.WriteLine("Resultado: ItemPedido cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) ITEMPEDIDO");
                    ItemPedido ipAlt = new ItemPedido();
                    Console.Write("ID do itemPedido para atualizar: ");
                    ipAlt.ItemPedidoID = int.Parse(Console.ReadLine());
                    Console.Write("Quantidade: ");
                    ipAlt.Quantidade = int.Parse(Console.ReadLine());
                    Console.Write("Valor unitário: ");
                    ipAlt.ValorUnitario = decimal.Parse(Console.ReadLine());
                    Console.Write("Total item: ");
                    ipAlt.TotalItem = decimal.Parse(Console.ReadLine());
                    Console.Write("Observação: ");
                    ipAlt.Observacao = Console.ReadLine();
                    Console.Write("ID produto (fk_produtoID): ");
                    ipAlt.fk_produtoID = int.Parse(Console.ReadLine());
                    Console.Write("ID pedido (fk_pedidoID): ");
                    ipAlt.fk_pedidoID = int.Parse(Console.ReadLine());
                    dao.Update(ipAlt);
                    Console.WriteLine("Resultado: ItemPedido atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) ITEMPEDIDO");
                    Console.Write("ID do itemPedido para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: ItemPedido deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) ITEMPEDIDO");
                    foreach (ItemPedido item in dao.GetAll())
                        Console.WriteLine($"{item.ItemPedidoID} | Qtd: {item.Quantidade} | Total: {item.TotalItem}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID ITEMPEDIDO");
                    Console.Write("ID do itemPedido: ");
                    ItemPedido ipBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {ipBusca.ItemPedidoID} | Qtd: {ipBusca.Quantidade} | Total: {ipBusca.TotalItem}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuPagamento()
        {
            PagamentoDAO dao = new PagamentoDAO();
            switch (LerOperacao("Pagamento"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) PAGAMENTO");
                    Pagamento pgNovo = new Pagamento();
                    Console.Write("Data pagamento (aaaa-mm-dd ou vazio): ");
                    string dp = Console.ReadLine();
                    pgNovo.DataPagamento = string.IsNullOrEmpty(dp) ? (DateOnly?)null : DateOnly.Parse(dp);
                    Console.Write("Data vencimento (aaaa-mm-dd): ");
                    pgNovo.DataVencimento = DateOnly.Parse(Console.ReadLine());
                    Console.Write("Forma pagamento: ");
                    pgNovo.FormaPagamento = Console.ReadLine();
                    Console.Write("Qtd parcelas: ");
                    pgNovo.QtdParcelas = int.Parse(Console.ReadLine());
                    Console.Write("Total: ");
                    pgNovo.Total = decimal.Parse(Console.ReadLine());
                    Console.Write("Status: ");
                    pgNovo.Status = Console.ReadLine();
                    dao.Insert(pgNovo);
                    Console.WriteLine("Resultado: Pagamento cadastrado com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) PAGAMENTO");
                    Pagamento pgAlt = new Pagamento();
                    Console.Write("ID do pagamento para atualizar: ");
                    pgAlt.PagamentoID = int.Parse(Console.ReadLine());
                    Console.Write("Data pagamento (aaaa-mm-dd ou vazio): ");
                    string dpAlt = Console.ReadLine();
                    pgAlt.DataPagamento = string.IsNullOrEmpty(dpAlt) ? (DateOnly?)null : DateOnly.Parse(dpAlt);
                    Console.Write("Data vencimento (aaaa-mm-dd): ");
                    pgAlt.DataVencimento = DateOnly.Parse(Console.ReadLine());
                    Console.Write("Forma pagamento: ");
                    pgAlt.FormaPagamento = Console.ReadLine();
                    Console.Write("Qtd parcelas: ");
                    pgAlt.QtdParcelas = int.Parse(Console.ReadLine());
                    Console.Write("Total: ");
                    pgAlt.Total = decimal.Parse(Console.ReadLine());
                    Console.Write("Status: ");
                    pgAlt.Status = Console.ReadLine();
                    dao.Update(pgAlt);
                    Console.WriteLine("Resultado: Pagamento atualizado com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) PAGAMENTO");
                    Console.Write("ID do pagamento para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Pagamento deletado com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) PAGAMENTO");
                    foreach (Pagamento item in dao.GetAll())
                        Console.WriteLine($"{item.PagamentoID} | {item.FormaPagamento} | R$ {item.Total} | {item.Status}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID PAGAMENTO");
                    Console.Write("ID do pagamento: ");
                    Pagamento pgBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {pgBusca.PagamentoID} | {pgBusca.FormaPagamento} | R$ {pgBusca.Total}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }

        static void MenuEntrega()
        {
            EntregaDAO dao = new EntregaDAO();
            switch (LerOperacao("Entrega"))
            {
                case 1:
                    Console.WriteLine("Ação: CADASTRAR (INSERT) ENTREGA");
                    Entrega entNovo = new Entrega();
                    Console.Write("Taxa entrega: ");
                    entNovo.TaxaEntrega = decimal.Parse(Console.ReadLine());
                    Console.Write("Entregador: ");
                    entNovo.Entregador = Console.ReadLine();
                    Console.Write("Observação: ");
                    entNovo.Observacao = Console.ReadLine();
                    Console.Write("ID endereço (fk_enderecoID): ");
                    entNovo.fk_enderecoID = int.Parse(Console.ReadLine());
                    Console.Write("ID pedido (fk_pedidoID): ");
                    entNovo.fk_pedidoID = int.Parse(Console.ReadLine());
                    dao.Insert(entNovo);
                    Console.WriteLine("Resultado: Entrega cadastrada com sucesso!");
                    PausarTela();
                    break;
                case 2:
                    Console.WriteLine("Ação: ATUALIZAR (UPDATE) ENTREGA");
                    Entrega entAlt = new Entrega();
                    Console.Write("ID da entrega para atualizar: ");
                    entAlt.EntregaID = int.Parse(Console.ReadLine());
                    Console.Write("Taxa entrega: ");
                    entAlt.TaxaEntrega = decimal.Parse(Console.ReadLine());
                    Console.Write("Entregador: ");
                    entAlt.Entregador = Console.ReadLine();
                    Console.Write("Observação: ");
                    entAlt.Observacao = Console.ReadLine();
                    Console.Write("ID endereço (fk_enderecoID): ");
                    entAlt.fk_enderecoID = int.Parse(Console.ReadLine());
                    Console.Write("ID pedido (fk_pedidoID): ");
                    entAlt.fk_pedidoID = int.Parse(Console.ReadLine());
                    dao.Update(entAlt);
                    Console.WriteLine("Resultado: Entrega atualizada com sucesso!");
                    PausarTela();
                    break;
                case 3:
                    Console.WriteLine("Ação: DELETAR (DELETE) ENTREGA");
                    Console.Write("ID da entrega para deletar: ");
                    dao.Delete(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Resultado: Entrega deletada com sucesso!");
                    PausarTela();
                    break;
                case 4:
                    Console.WriteLine("Ação: LISTAR TODOS (GetAll) ENTREGA");
                    foreach (Entrega item in dao.GetAll())
                        Console.WriteLine($"{item.EntregaID} | Taxa: {item.TaxaEntrega} | {item.Entregador}");
                    PausarTela();
                    break;
                case 5:
                    Console.WriteLine("Ação: BUSCAR POR ID ENTREGA");
                    Console.Write("ID da entrega: ");
                    Entrega entBusca = dao.BuscarID(int.Parse(Console.ReadLine()));
                    Console.WriteLine($"Resultado: {entBusca.EntregaID} | Taxa: {entBusca.TaxaEntrega} | {entBusca.Entregador}");
                    PausarTela();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    PausarTela();
                    break;
                default:
                    Console.WriteLine("Operação inválida!");
                    PausarTela();
                    break;
            }
        }
    }
}