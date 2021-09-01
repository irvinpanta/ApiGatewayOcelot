using System;
using System.Collections.Generic;

namespace ApiGatewayOcelot.Dto
{
    public class Mesa
    {
        
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Salon { get; set; }

        public List<Salones> Salonesss { get; set; } = new List<Salones>();


    }
}
