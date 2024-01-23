using Microsoft.AspNetCore.Mvc;

// Controlador para manipulação de imagens.
[ApiController]
[Route("api/[controller]")]
public class ImagemController : ControllerBase
{

    // Endpoint HTTP GET para obter uma imagem pelo nome.
    [HttpGet("{nomeDaImagem}")]
    public IActionResult ObterImagem(string nomeDaImagem)
    {
        string pastaDestino = "ImagensFuncionarios";
        string caminhoCompleto = Path.Combine(pastaDestino, nomeDaImagem);

        // Verifica se o arquivo de imagem existe no caminho especificado.
        if (!System.IO.File.Exists(caminhoCompleto))
        {
            // Retorna um resultado HTTP 404 Not Found se a imagem não for encontrada.
            return NotFound();
        }

        // Abre um fluxo de leitura para o arquivo de imagem.
        var stream = System.IO.File.OpenRead(caminhoCompleto);

        // Retorna o conteúdo do fluxo de leitura e o tipo de mídia como png.
        return File(stream, "image/png");
    }

    [HttpPost]
    public async Task<IActionResult> PostImagem(IFormFile file)
    {
        try
        {
            // Defina o caminho onde deseja salvar a imagem no servidor.
            string pastaDestino = "ImagensFuncionarios";

            // Certifique-se de que a pasta de destino existe. Se não, crie-a.
            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            // Crie um nome de arquivo único para evitar colisões.
            string nomeArquivo = Path.GetFileName(file.FileName);
            string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            // Salve o arquivo no servidor.
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorne o caminho relativo do arquivo salvo para que possa ser acessado posteriormente.
            return Ok(new { caminho = nomeArquivo });
        }
        catch (Exception ex)
        {
            // Trate qualquer exceção que possa ocorrer durante o processo.
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
