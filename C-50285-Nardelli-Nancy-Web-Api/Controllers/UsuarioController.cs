using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Infrastructure;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace C_50285_Nardelli_Nancy_Web_Api.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;

        /// <summary>
        /// Constructor del controlador de usuario.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
            
        }

        [HttpPost("api/usuario/login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _unitOfWork.UsuarioRepositorio.Login(model);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucces = false;
                _response.ErrorMessages.Add("El Usuario o Contraseña son incorrectos");
                return BadRequest(_response);
            }
            _response.IsSucces = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        //Para el front de coderhouse, yo no usaría un get para login
        [HttpGet("/api/Usuario/{nombreUsuario}/{contraseña}")]
        public async Task<ActionResult> Login(string nombreUsuario, string contraseña)
        {
            var loginResponse = await _unitOfWork.UsuarioRepositorio.Login(new LoginRequestDTO { UserName = nombreUsuario, Password = contraseña });

            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucces = false;
                _response.ErrorMessages.Add("El Usuario o Contraseña son incorrectos");
                return BadRequest(_response);
            }

            _response.IsSucces = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("/api/Usuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (await _unitOfWork.UsuarioRepositorio.Get(v => v.NombreUsuario.ToLower() == usuarioCreateDTO.NombreUsuario.ToLower()) != null)
                {
                    ModelState.AddModelError("UsuarioExiste", "El usuario con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (usuarioCreateDTO == null)
                {
                    return BadRequest(usuarioCreateDTO);
                }
                Usuario model = _mapper.Map<Usuario>(usuarioCreateDTO);
               
                await _unitOfWork.UsuarioRepositorio.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("getUser", new { id = usuarioCreateDTO.Id }, _response);

            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;

        }
        
        [HttpGet("id:int", Name = "GetUser")]
        [ProducesResponseType(typeof(ApiSuccessResponse<Usuario>), 200)]
        public async Task<IActionResult> GetById(int id)
        {

            var user = await _unitOfWork.UsuarioRepositorio.GetById(id);
            if (user == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el usuario
            }
            var userDto = _mapper.Map<UsuarioCreateDTO>(user);
            return ResponseFactory.CreateSuccessResponse(200, userDto);
        }

        [HttpPut("/api/Usuario/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsSucces = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Usuario modelo = _mapper.Map<Usuario>(updateDto);

            await _unitOfWork.UsuarioRepositorio.Update(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }

        [HttpGet("/api/Usuario/{nombreUsuario}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorNombre(string nombreUsuario)
        {
            var usuario = await _unitOfWork.UsuarioRepositorio.Get(u => u.NombreUsuario == nombreUsuario);
            if (usuario == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el usuario
            }
            return usuario;
        }

        [HttpDelete("/api/Usuario/{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UsuarioRepositorio.GetById(id);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSucces = false;
                _response.ErrorMessages.Add($"Usuario con ID {id} no encontrado.");
                return NotFound(_response);
                //return NotFound($"Usuario con ID {id} no encontrado."); // Devolver 404 si no se encuentra el usuario
               
            }

            await _unitOfWork.UsuarioRepositorio.Delete(user);
            _response.IsSucces = true;
            return NoContent();
        }
    }
}
