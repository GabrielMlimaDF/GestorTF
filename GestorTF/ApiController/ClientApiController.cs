using GestorTF.Models;
using GestorTF.Models.ViewModels;
using GestorTF.Models.ViewModels.ClientViewModel;
using GestorTF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterClientAsync(ClientRegisterDto clientRegisterDto)
        {
            Guid usuarioId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var client = new Cliente
                {
                    Nome = clientRegisterDto.Nome,
                    Cnpj = clientRegisterDto.Cnpj,
                    Email = clientRegisterDto.Email,
                    Telefone = clientRegisterDto.Telefone,
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


    }
}
