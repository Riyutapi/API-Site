using CRUDAPI.Models;
using ECore.WebAPI.Models;

namespace CRUDAPI.Service.FuncionarioS
{
    public interface IFuncionarioInterface
    {
        // lista de todos os funcionários.
        Task<ServiceResponse<List<Funcionario>>> GetFuncionarios();

        // Criar um novo funcionário com base nos dados fornecidos.
        Task<ServiceResponse<List<Funcionario>>> CreateFuncionario(Funcionario novoFuncionario);

        // Obter as informações de um funcionário com base no ID.
        Task<ServiceResponse<Funcionario>> GetFuncionarioById(int id);

        // Atualiza as informações de um funcionário com base nos dados editados.
        Task<ServiceResponse<List<Funcionario>>> UpdateFuncionario(Funcionario editadoFuncionario);

        // Exclui um funcionário com base no ID.
        Task<ServiceResponse<List<Funcionario>>> DeleteFuncionarioById(int id);
    }
}
