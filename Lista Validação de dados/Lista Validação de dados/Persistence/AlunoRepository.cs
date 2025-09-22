namespace Lista_Validação_de_dados.Persistence
{
    using EscolaApi.Models;
    using Lista_Validação_de_dados.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class AlunoRepository : IAlunoRepository
    {
        private readonly List<Aluno> _alunos = new();
        private readonly object _lock = new();

        private static string NormalizeRa(string ra) => ra?.Trim().ToUpperInvariant();

        private static string NormalizeCpf(string cpf) => new string((cpf ?? "").Where(char.IsDigit).ToArray());

        public void Add(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            aluno.Ra = NormalizeRa(aluno.Ra);
            aluno.Cpf = NormalizeCpf(aluno.Cpf);

            lock (_lock)
            {
                if (_alunos.Any(a => a.Ra == aluno.Ra)) throw new InvalidOperationException("Já existe um aluno com esse RA.");
                _alunos.Add(aluno);
            }
        }

        public IEnumerable<Aluno> GetAll()
        {
            lock (_lock) { return _alunos.Select(a => a).ToList(); }
        }

        public Aluno GetByRa(string ra)
        {
            var r = NormalizeRa(ra);
            lock (_lock) { return _alunos.FirstOrDefault(a => a.Ra == r); }
        }

        public bool Remove(string ra)
        {
            var r = NormalizeRa(ra);
            lock (_lock)
            {
                var found = _alunos.FirstOrDefault(a => a.Ra == r);
                if (found == null) return false;
                _alunos.Remove(found);
                return true;
            }
        }

        public bool Update(string ra, Aluno aluno)
        {
            var r = NormalizeRa(ra);
            lock (_lock)
            {
                var existing = _alunos.FirstOrDefault(a => a.Ra == r);
                if (existing == null) return false;
                existing.Nome = aluno.Nome;
                existing.Email = aluno.Email;
                existing.Cpf = NormalizeCpf(aluno.Cpf);
                existing.Ativo = aluno.Ativo;
                return true;
            }
        }
    }
}
