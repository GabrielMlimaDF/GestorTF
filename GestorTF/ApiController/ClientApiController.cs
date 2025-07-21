using GestorTF.Models;
using GestorTF.Models.ViewModels;
using GestorTF.Models.ViewModels.ClientViewModel;
using GestorTF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorTF.ApiController
{
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientApiController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("/v1/client/registers")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RegisterClientAsync(ClientRegisterDto dto)
        {
            Guid usuarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existEmail = await _clientRepository.GetClientByCnpjAsync(dto.Cnpj);
            if (existEmail)
                return StatusCode(400, new ResultViewModel<string>("05x02 - Verifique se o cadastro com o CNPJ informado já não existe."));
            try
            {
                var client = new Cliente
                {
                    Nome = dto.Nome,
                    Cnpj = dto.Cnpj,
                    Email = dto.Email,
                    Telefone = dto.Telefone,
                    UsuarioId = usuarioId
                };
                await _clientRepository.CreateClientAsync(client);
                return Created($"/v1/client/register/{client.Id}", new ResultViewModel<Cliente>(client, "Cliente criado com sucesso!"));

            }
            catch (Exception)
            {
                return StatusCode(400, new ResultViewModel<string>("05x01 - Houve uma falha ao processar requisição."));
            }
        }

        [HttpPut("/v1/client/edit")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateClientAsync(Guid id, ClientUpdateDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var client = await _clientRepository.GetByIdAsync(id, userId);

                if (client is null)
                    return StatusCode(400, new ResultViewModel<string>("05x02 - Cliente não encontrado ou você não tem acesso."));

                client.Nome = dto.Nome;
                client.Email = dto.Email;
                client.Telefone = dto.Telefone;
                client.Ativo = dto.Ativo;

                await _clientRepository.UpdateAsync(client);
                return Ok(new ResultViewModel<Cliente>(client, "Cliente alterado com sucesso!"));

            }
            catch (Exception)
            {

                return StatusCode(400, new ResultViewModel<string>("05x01 - Houve uma falha ao processar requisição."));
            }

        }
        //[HttpDelete("/v1/client/delete")]
        //[Authorize(Roles = "Admin,User")]
        //public async IActionResult DeleteClientAsync(Guid id)
        //{
        //}
        [HttpGet("/v1/client/list")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ListClientAsync()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var clientes = await _clientRepository.GetClientAllAsync(userId);

            if (clientes == null || !clientes.Any())
                return NoContent(); // 204

            return Ok(clientes); // 200 + JSON

        }
    }
}
