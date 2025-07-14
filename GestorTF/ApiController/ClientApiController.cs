using GestorTF.Models;
using GestorTF.Models.ViewModels;
using GestorTF.Models.ViewModels.ClientViewModel;
using GestorTF.Repository;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
            Guid usuarioId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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


    }
}
