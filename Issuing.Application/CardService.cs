using Issuing.Domain;
using Issuing.Persistence;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Issuing.Application
{
    public class CardService : ICardService
    {

        private readonly ISession _session;
        private readonly RepositoryBase<Card> _repository;

        public CardService(ISession session)
        {
            _session = session;
            _repository = new RepositoryBase<Card>(_session);
        }

        public void Create(Card card)
        {
            _repository.Insert(card);
        }

        public List<Card> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
