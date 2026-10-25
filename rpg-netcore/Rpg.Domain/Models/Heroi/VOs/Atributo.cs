namespace Rpg.Domain.Models.Heroi.VOs
{
    public abstract class Atributo
    {
        private const int VALOR_MINIMO = 1;
        private const int VALOR_MAXIMO = 100;
        public int Valor { get; }

        public Atributo(int valor)
        {
            Valor = valor < VALOR_MINIMO ? throw new ArgumentOutOfRangeException(nameof(valor), $"Valor mínimo: {VALOR_MINIMO}") :
                    valor > VALOR_MAXIMO ? throw new ArgumentOutOfRangeException(nameof(valor), $"Valor máximo: {VALOR_MINIMO}") : valor;
        }
    }
}
