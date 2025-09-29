using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepositorio)
        {
            _usuarioRepository = usuarioRepositorio;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Usuario>>> GetAll()
        {
            List<Usuario> usuarios = await _usuarioRepository.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            Usuario usuario = await _usuarioRepository.GetById(id);
            return Ok(usuario);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario Usuario)
        {
            Usuario usuario = await _usuarioRepository.Post(Usuario);

            return Ok(usuario);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Usuario>> Put([FromBody] Usuario Usuario, int id)
        {
            Usuario.Id = id;
            Usuario usuario = await _usuarioRepository.Put(Usuario, id);
            return Ok(usuario);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            bool apagado = await _usuarioRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
