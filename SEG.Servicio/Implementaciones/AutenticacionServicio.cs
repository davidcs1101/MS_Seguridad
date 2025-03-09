using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using SEG.Servicio.Interfaces;
using SEG.Servicio.Utilidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
{
    public class AutenticacionServicio: IAutenticacionServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUsuarioSedeGrupoRepositorio _usuarioSedeRepositorio;
        private readonly IGrupoProgramaRepositorio _grupoRepositorio;
        private readonly IConfiguration _configuracion;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        public AutenticacionServicio(IUsuarioRepositorio usuarioRepositorio, IUsuarioSedeGrupoRepositorio usuarioSedeRepositorio, IGrupoProgramaRepositorio grupoRepositorio, IConfiguration configuracion,
            IUsuarioContextoServicio usuarioContextoServicio) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioSedeRepositorio = usuarioSedeRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _configuracion = configuracion;
            _usuarioContextoServicio = usuarioContextoServicio;
        }

        public async Task<ApiResponse<string>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionRequest)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorUsuarioAsync(autenticacionRequest.NombreUsuario);
            if (usuario == null || usuario.Clave != ProcesadorClaves.EncriptarClave(autenticacionRequest.Clave))
                return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_LOGIN_INCORRECTO };

            var token = await GenerarTokenAsync(usuario, null, null);
            return new ApiResponse<string> { Correcto = true, Mensaje = "", Data = token };
        }

        public async Task<ApiResponse<string>> AutenticarSedeAsync(int sedeId)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioSede = await _usuarioSedeRepositorio.ObtenerUsuarioSedeAsync(usuarioId, sedeId);
            if (usuarioSede == null)
                return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_LOGIN_SEDE_INCORRECTO };

            var token = await GenerarTokenAsync(usuarioSede.Usuario, usuarioSede.GrupoId, usuarioSede.SedeId);
            return new ApiResponse<string> { Correcto = true, Mensaje = "", Data = token };
        }

        private  async Task<string> GenerarTokenAsync(SEG_Usuario usuario, int? grupoId, int? sedeId)
        {
            //Datos de configuracon para el Token
            var configuracionJWT = _configuracion.GetSection("JWT");
            var issuer = configuracionJWT["Issuer"];
            var audiences = configuracionJWT.GetSection("Audience").GetChildren().Select(a => a.Value).ToList();
            int tiempoExpiracion = Convert.ToInt32(configuracionJWT["MinutosDuracionTokenAutenticacionUsuario"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracionJWT["Key"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            #region REG_Adicionamos Claims específicos del usuario
            var claims = new List<Claim>
            {
                new Claim("UsuarioId", usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };
            if (grupoId.HasValue)
            {
                claims.Add(new Claim("GrupoId", grupoId.ToString()));
                var programas = _grupoRepositorio.ListarProgramasPorGrupo(grupoId.Value)
                    .Where(gp => gp.EstadoActivo);
                foreach (var programa in programas)
                {
                    claims.Add(new Claim("Programa", programa.Codigo.ToUpper()));
                }
            }
            if (sedeId.HasValue)
            {
                claims.Add(new Claim("SedeId", sedeId.ToString()));
            }
            #endregion


            #region REG_Adicionamos Claims de opciones para seleccion de sedes y cambio de email
            /*
             Los siguientes Claims se adicionan solo si el usuario ya ha realizado el cambio de clave ya que es un requisisto 
            obligatorio para poder navegar en las opciones controladas por los permisos que aquí se adicionan.
             */
            if (!usuario.CambiarClave)
            {
                tiempoExpiracion = Convert.ToInt32(configuracionJWT["MinutosDuracionTokenAutenticacionSede"]);
                claims.Add(new Claim("Accion", "CAMBIOCLAVEOK"));
            }
            #endregion


            var token = new JwtSecurityToken(
                issuer : issuer,
                audience: _configuracion["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tiempoExpiracion),
                signingCredentials: credenciales);
            token.Payload["aud"] = audiences;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}