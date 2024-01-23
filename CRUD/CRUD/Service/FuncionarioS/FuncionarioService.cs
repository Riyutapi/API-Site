using CRUDAPI.Models;
using ECore.WebAPI.Data;
using ECore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Service.FuncionarioS
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly Contexto _context;
        // O Contexto representa o DbContext para interação com o banco de dados.
        public FuncionarioService(Contexto context)
        {
            _context = context;
        }

        // Método para criar um novo funcionário.
        public async Task<ServiceResponse<List<Funcionario>>> CreateFuncionario(Funcionario novoFuncionario)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Funcionario>> serviceResponse = new ServiceResponse<List<Funcionario>>();
            try
            {
                // Verifica se os dados do novo funcionário são válidos.
                if (novoFuncionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar Dados";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Adiciona o novo funcionário ao contexto e salva as alterações.
                _context.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                // Atualiza a lista de funcionários no serviço de resposta.
                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        // Método para excluir um funcionário com base no ID.
        public async Task<ServiceResponse<List<Funcionario>>> DeleteFuncionarioById(int id)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Funcionario>> serviceResponse = new ServiceResponse<List<Funcionario>>();
            try
            {
                // Procura o funcionário pelo ID.
                Funcionario funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                // Verifica se o funcionário foi encontrado.
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário Não Encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Remove o funcionário do contexto e salva as alterações.
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                // Atualiza a lista de funcionários no serviço de resposta.
                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Método para obter a lista de todos os funcionários.
        public async Task<ServiceResponse<List<Funcionario>>> GetFuncionarios()
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Funcionario>> serviceResponse = new ServiceResponse<List<Funcionario>>();
            try
            {
                // Obtém a lista de funcionários do contexto.
                serviceResponse.Dados = _context.Funcionarios.ToList();

                // Verifica se há funcionários na lista.
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum Funcionário Encontrado";
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Método para obter as informações de um funcionário com base no ID.
        public async Task<ServiceResponse<Funcionario>> GetFuncionarioById(int id)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<Funcionario> serviceResponse = new ServiceResponse<Funcionario>();
            try
            {
                // Procura o funcionário pelo ID.
                Funcionario funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                // Verifica se o funcionário foi encontrado.
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário Não Encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    // Define as informações do funcionário no serviço de resposta.
                    serviceResponse.Dados = funcionario;
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Método para atualizar as informações de um funcionário.
        public async Task<ServiceResponse<List<Funcionario>>> UpdateFuncionario(Funcionario editadoFuncionario)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Funcionario>> serviceResponse = new ServiceResponse<List<Funcionario>>();
            try
            {
                // Procura o funcionário pelo ID no estado não rastreado.
                Funcionario funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == editadoFuncionario.Id);

                // Verifica se o funcionário foi encontrado.
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário Não Encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    // Atualiza as informações do funcionário no contexto e salva as alterações.
                    _context.Funcionarios.Update(editadoFuncionario);
                    await _context.SaveChangesAsync();

                    // Atualiza a lista de funcionários no serviço de resposta.
                    serviceResponse.Dados = _context.Funcionarios.ToList();
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

    }
}
