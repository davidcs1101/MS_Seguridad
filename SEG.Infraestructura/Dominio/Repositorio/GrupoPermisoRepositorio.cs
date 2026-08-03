using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ModelosVistas;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class GrupoPermisoRepositorio : IGrupoPermisoRepositorio
    {
        private readonly AppDbContext _context;
        public GrupoPermisoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public void MarcarCrear(SEG_GrupoPermiso grupoPermiso) {
            _context.SEG_GruposPermisos.Add(grupoPermiso);
        }

        public void MarcarModificar(SEG_GrupoPermiso grupoPermiso) {
            _context.SEG_GruposPermisos.Update(grupoPermiso);
        }

        public void MarcarEliminar(SEG_GrupoPermiso grupoPermiso)
        {
            _context.SEG_GruposPermisos.Remove(grupoPermiso);
        }

        public async Task<SEG_GrupoPermiso?> ObtenerGrupoPermisoAsync(int grupoId, int permisoId)
        {
            return await _context.SEG_GruposPermisos
                .FirstOrDefaultAsync(x => x.GrupoId == grupoId && x.PermisoId == permisoId);
        }

        public async Task<SEG_GrupoPermiso?> ObtenerPorIdAsync(int id) {
            return await _context.SEG_GruposPermisos.FindAsync(id);
        }

        public IQueryable<SEG_GrupoPermiso> ListarPermisosPorGrupo(int grupoId)
        {
            return _context.SEG_GruposPermisos
                .Include(gp => gp.Grupo)
                .Include(gp => gp.Permiso)
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador)
                .Where(gp => gp.GrupoId == grupoId);
        }

        public IQueryable<SEG_GrupoPermiso> Listar()
        {
            return _context.SEG_GruposPermisos
                .Include(gp => gp.Grupo)
                .Include(gp => gp.Permiso)
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }

        public IQueryable<AutorizacionMV> ListarPermisosCache()
        {
            return _context.SEG_GruposPermisos
                .AsNoTracking()
                .Select(x => new AutorizacionMV
                {
                    Id = x.Id,
                    CodigoPrograma = x.Permiso.Programa.Codigo,
                    EstadoPrograma = x.Permiso.Programa.EstadoActivo,

                    CodigoGrupo = x.Grupo.Codigo,
                    EstadoGrupo = x.Grupo.EstadoActivo,

                    CodigoPermiso = x.Permiso.Codigo,
                    EstadoPermiso = x.Permiso.EstadoActivo,

                    EstadoGrupoPermiso = x.EstadoActivo
                });
        }
    }
}
