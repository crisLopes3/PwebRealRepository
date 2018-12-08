using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    public enum Ramo
    {
        [Description("Sistemas de Informação")]
        SistemasDeInformacao,
        [Description("Desenvolvimento de Aplicações")]
        DesenvolvimentoAplicacoes,
        [Description("Redes e Administração de Sistemas")]
        RedesEAdministracaoDeSistemas
    }
}