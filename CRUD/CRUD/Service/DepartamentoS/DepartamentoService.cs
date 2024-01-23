using CRUDAPI.Models;
using ECore.WebAPI.Data;
using ECore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Service.DepartamentoS
{
    public class DepartamentoService : IDepartamentoInterface
    {
        private readonly Contexto _context;
        // O Contexto representa o DbContext para interação com o banco de dados.
        public DepartamentoService(Contexto context)
        {
            _context = context;
        }
        // Método para criar um novo departamento.
        public async Task<ServiceResponse<List<Departamento>>> CreateDepartamento(Departamento novoDepartamento)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Departamento>> serviceResponse = new ServiceResponse<List<Departamento>>();
            try
            {
                // Verifica se os dados do novo departamento são válidos.
                if (novoDepartamento == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar Dados";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Adiciona o novo departamento ao contexto e salva as alterações.
                _context.Add(novoDepartamento);
                await _context.SaveChangesAsync();

                // Atualiza a lista de departamentos no serviço de resposta.
                serviceResponse.Dados = _context.Departamentos.ToList();
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Método para excluir um departamento com base no ID.
        public async Task<ServiceResponse<List<Departamento>>> DeleteDepartamentoById(int id)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Departamento>> serviceResponse = new ServiceResponse<List<Departamento>>();
            try
            {
                // Procura o departamento pelo ID.
                Departamento departamento = _context.Departamentos.FirstOrDefault(x => x.Id == id);

                // Verifica se o departamento foi encontrado.
                if (departamento == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Departamento Não Encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Remove o departamento do contexto e salva as alterações.
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();

                // Atualiza a lista de departamentos no serviço de resposta.
                serviceResponse.Dados = _context.Departamentos.ToList();
            }
            catch (Exception ex)
            {
                // Em caso de erro, define as informações de erro no serviço de resposta.
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Método para obter a lista de todos os departamentos.
        public async Task<ServiceResponse<List<Departamento>>> GetDepartamentos()
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Departamento>> serviceResponse = new ServiceResponse<List<Departamento>>();
            try
            {
                // Obtém a lista de departamentos do contexto.
                serviceResponse.Dados = _context.Departamentos.ToList();

                // Verifica se há departamentos na lista.
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum Departamento Encontrado";
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

        // Método para obter as informações de um departamento com base no ID.
        public async Task<ServiceResponse<Departamento>> GetDepartamentoById(int id)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<Departamento> serviceResponse = new ServiceResponse<Departamento>();
            try
            {
                // Procura o departamento pelo ID.
                Departamento departamento = _context.Departamentos.FirstOrDefault(x => x.Id == id);

                // Verifica se o departamento foi encontrado.
                if (departamento == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Departamento Não Encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    // Define as informações do departamento no serviço de resposta.
                    serviceResponse.Dados = departamento;
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

        // Método para atualizar as informações de um departamento.
        public async Task<ServiceResponse<List<Departamento>>> UpdateDepartamento(Departamento editadoDepartamento)
        {
            // Inicializa um objeto de resposta do serviço.
            ServiceResponse<List<Departamento>> serviceResponse = new ServiceResponse<List<Departamento>>();
            try
            {
                // Procura o departamento pelo ID no estado não rastreado.
                Departamento departamento = _context.Departamentos.AsNoTracking().FirstOrDefault(x => x.Id == editadoDepartamento.Id);

                // Verifica se o departamento foi encontrado.
                if (departamento == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Departamento Não Encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    // Atualiza as informações do departamento no contexto e salva as alterações.
                    _context.Departamentos.Update(editadoDepartamento);
                    await _context.SaveChangesAsync();

                    // Atualiza a lista de departamentos no serviço de resposta.
                    serviceResponse.Dados = _context.Departamentos.ToList();
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
