using Rpg.Domain.Models.Heroi.Enums;
using Rpg.Domain.Models.Heroi.VOs;

namespace Rpg.Domain.Models.Heroi
{
    public class Heroi
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Historia { get; private set; }
        public SexoEnum Sexo{ get; private set; }
        //public Double PontosDeVida { get; set; }
        public Raca Raca { get; private set; }
        public Forca Forca { get; private set; }
        public Destreza Destreza { get; private set; }
        public Constituicao Constituicao { get; private set; }
        public Inteligencia Inteligencia { get; private set; }
        public Sabedoria Sabedoria { get; private set; }
        public Carisma Carisma { get; private set; }
    }
}
