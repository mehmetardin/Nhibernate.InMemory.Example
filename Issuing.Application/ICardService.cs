using Issuing.Domain;
using System.Collections.Generic;

namespace Issuing.Application
{
    public interface ICardService
    {
        void Create(Card card);
        List<Card> GetAll();
    }
}
