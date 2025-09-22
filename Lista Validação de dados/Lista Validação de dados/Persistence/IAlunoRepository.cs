namespace Lista_Validação_de_dados.Persistence
{
    using System.Collections.Generic;
    using EscolaApi.Models;
    using Lista_Validação_de_dados.Models;

    public interface IAlunoRepository
    {
        IEnumerable<Aluno> GetAll();
        Aluno GetByRa(string ra);
        void Add(Aluno aluno);
        bool Update(String Ra, Aluno aluno);
        bool Remove(string ra);
    }
}
