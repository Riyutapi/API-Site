using CRUDAPI.Models;
using CRUDAPI.Service.DepartamentoS;
using CRUDAPI.Service.FuncionarioS;
using ECore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// Controlador para manipulação de dados relacionados a Funcionários.
[Route("api/[controller]")]
[ApiController]

public class FuncionarioController : ControllerBase
{
    // Interface para operações relacionadas a Funcionários.
    private readonly IFuncionarioInterface _funcionarioInterface;

    // Construtor que recebe uma instância da interface de Funcionário ao ser instanciado.
    public FuncionarioController(IFuncionarioInterface funcionarioInterface)
    {
        _funcionarioInterface = funcionarioInterface;
    }

    // Endpoint HTTP GET para obter a lista de todos os funcionários.
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Funcionario>>>> GetFuncionarios()
    {
        return Ok(await _funcionarioInterface.GetFuncionarios());
    }

    // Endpoint HTTP GET para obter as informações de um funcionário com base no ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Funcionario>>> GetFuncionarioById(int id)
    {
        ServiceResponse<Funcionario> serviceResponse = await _funcionarioInterface.GetFuncionarioById(id);

        return Ok(serviceResponse);
    }

    // Endpoint HTTP PUT para atualizar as informações de um funcionário.
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<Funcionario>>>> UpdateFuncionario(Funcionario editadoFuncionario)
    {
        /* Infelizmente não encontrei uma maneira para apagar a imagem quando alterasse ela,
           sem dar erro de por causa de rastrear duas instâncias

        if (!string.IsNullOrWhiteSpace(editadoFuncionario.Foto))
        {
            // Pegar o caminho da foto antiga.
            ServiceResponse<Funcionario> apagarFoto = await _funcionarioInterface.GetFuncionarioById(editadoFuncionario.Id);

            if (editadoFuncionario.Foto != apagarFoto.Dados.Foto)
            {
                // Excluir foto antiga.
                ExcluirFoto(apagarFoto.Dados.Foto);
            }
        }*/

        ServiceResponse<List<Funcionario>> serviceResponse = await _funcionarioInterface.UpdateFuncionario(editadoFuncionario);

        return Ok(serviceResponse);
    }

    // Endpoint HTTP POST para criar um novo funcionário.
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Funcionario>>>> CreateFuncionario(Funcionario novoFuncionario)
    {
        // Criar um novo funcionário no banco de dados.
        ServiceResponse<List<Funcionario>> serviceResponse = await _funcionarioInterface.CreateFuncionario(novoFuncionario);
        return Ok(serviceResponse);
    }

    // Endpoint HTTP DELETE para excluir um funcionário com base no ID.
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Funcionario>>>> DeleteFuncionarioById(int id)
    {
        // Obter as informações do funcionário antes de excluí-lo.
        ServiceResponse<Funcionario> serviceResponse = await _funcionarioInterface.GetFuncionarioById(id);

        // Se o funcionário existe, excluir sua foto.
        if (serviceResponse.Sucesso)
        {
            ExcluirFoto(serviceResponse.Dados?.Foto);
        }

        // Excluir o funcionário do banco de dados.
        return Ok(await _funcionarioInterface.DeleteFuncionarioById(id));
    }

    // Método privado para excluir uma foto do diretório.
    private void ExcluirFoto(string nomeArquivo)
    {
        string pastaDestino = "ImagensFuncionarios";
        string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);
        if (!string.IsNullOrWhiteSpace(nomeArquivo))
        {
            // Verificar se o arquivo da foto existe e excluí-lo.
            if (System.IO.File.Exists(caminhoCompleto))
            {
                System.IO.File.Delete(caminhoCompleto);
                Console.WriteLine("Foto antiga excluída com sucesso.");
            }
            else
            {
                Console.WriteLine("O arquivo da foto antiga não foi encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Não tem foto");
        }
    }
}
