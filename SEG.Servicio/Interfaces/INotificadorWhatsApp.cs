﻿using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface INotificadorWhatsApp
    {
        Task<bool> EnviarAsync(DatoWhatsAppRequest datoWhatsAppRequest);
    }
}
