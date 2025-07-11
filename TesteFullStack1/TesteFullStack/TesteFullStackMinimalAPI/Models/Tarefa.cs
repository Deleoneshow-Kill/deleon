namespace TesteFullStackMinimalAPI.Models
{

        public class Tarefa(string nome, string detalhes)
        {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Nome { get; private set; } = nome;
        public string Detalhes { get; private set; } = detalhes;
        public bool Concluida { get; private set; } = false;
        public DateTime DataCadastro { get; private set; } = DateTime.Now; 
        public DateTime? DataConclusao { get; private set; } = null;
        public void AtualizarTarefa(string nome, string detalhe, bool? concluido = false)
        {
            Nome = nome;
            Detalhes = detalhe;
            Concluida = concluido ?? false;
            DataConclusao = Concluida ? DateTime.Now : null;
        }
    }
}
