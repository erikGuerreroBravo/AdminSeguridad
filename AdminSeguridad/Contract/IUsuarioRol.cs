using AdminSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSeguridad.Contract
{
    public interface IUsuarioRol
    {
        bool Insert(Usuario_Rol usuario_Rol);
    }
}
