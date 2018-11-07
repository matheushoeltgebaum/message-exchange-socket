using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoRedes
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string NumeroVitorias { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}
