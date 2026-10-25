namespace Rpg.Domain.Models.Dados
{
    public struct Dado
    {
        private const int VALOR_MINIMO_DADO = 1;
        public int ValorMinimoDado => VALOR_MINIMO_DADO;
        private readonly int Lados;
        private readonly Random NumeroAleatorio = new();

        public Dado(int lados)
        {
            Lados = lados;
        }

        public int LancarDado()
        {
            return NumeroAleatorio.Next(VALOR_MINIMO_DADO,Lados);
        }
    }
}
