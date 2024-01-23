using CRUDAPI.Models;
using ECore.WebAPI.Models;

namespace CRUDAPI.Service.DepartamentoS
{
    public interface IDepartamentoInterface
    {
        // lista de todos os departamentos.
        Task<ServiceResponse<List<Departamento>>> GetDepartamentos();

        // Criar um novo departamento com base nos dados fornecidos.
        Task<ServiceResponse<List<Departamento>>> CreateDepartamento(Departamento novoDepartamento);

        // Obter as informações de um departamento com base no ID.
        Task<ServiceResponse<Departamento>> GetDepartamentoById(int id);

        // Atualiza as informações de um departamento com base nos dados editados.
        Task<ServiceResponse<List<Departamento>>> UpdateDepartamento(Departamento editadoDepartamento);

        // Exclui um departamento com base no ID.
        Task<ServiceResponse<List<Departamento>>> DeleteDepartamentoById(int id);
    }
}
