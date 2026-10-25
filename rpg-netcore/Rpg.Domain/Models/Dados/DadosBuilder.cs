namespace Rpg.Domain.Models.Dados
{
    public static class DadosBuilder
    {
        public static Dado CriarDado(TipoDeDado tipoDeDado)
        {
            return new Dado((int)tipoDeDado);
        }
    }
}
