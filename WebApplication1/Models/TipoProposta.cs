using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    public enum TipoProposta
    {
        [XmlEnum("Estágio")]
        Estagio,
        [XmlEnum("Projeto")]
        Projeto
    }
}