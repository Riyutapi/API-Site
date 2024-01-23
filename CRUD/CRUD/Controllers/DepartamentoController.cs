using CRUDAPI.Models;
using CRUDAPI.Service.DepartamentoS;
using ECore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// Controlador para manipulação de dados relacionados a Departamentos.
[Route("api/[controller]")]
[ApiController]
public class DepartamentoController : ControllerBase
{
    // Interface para operações relacionadas a Departamentos.
    private readonly IDepartamentoInterface _departamentoInterface;

    // Construtor que recebe uma instância da interface de Departamento ao ser instanciado.
    public DepartamentoController(IDepartamentoInterface departamentoInterface)
    {
        _departamentoInterface = departamentoInterface;
    }

    // Endpoint HTTP GET para obter a lista de todos os departamentos.
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Departamento>>>> GetDepartamentos()
    {
        return Ok(await _departamentoInterface.GetDepartamentos());
    }

    // Endpoint HTTP GET para obter as informações de um departamento com base no ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Departamento>>> GetDepartamentoById(int id)
    {
        ServiceResponse<Departamento> serviceResponse = await _departamentoInterface.GetDepartamentoById(id);

        return Ok(serviceResponse);
    }

    // Endpoint HTTP PUT para atualizar as informações de um departamento.
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<Departamento>>>> UpdateDepartamento(Departamento editadoDepartamento)
    {
        ServiceResponse<List<Departamento>> serviceResponse = await _departamentoInterface.UpdateDepartamento(editadoDepartamento);

        return Ok(serviceResponse);
    }

    // Endpoint HTTP POST para criar um novo departamento.
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Departamento>>>> CreateDepartamento(Departamento novoDepartamento)
    {
        return Ok(await _departamentoInterface.CreateDepartamento(novoDepartamento));
    }

    // Endpoint HTTP DELETE para excluir um departamento com base no ID.
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Departamento>>>> DeleteDepartamentoById(int id)
    {
        ServiceResponse<List<Departamento>> serviceResponse = await _departamentoInterface.DeleteDepartamentoById(id);

        return Ok(serviceResponse);
    }
}
