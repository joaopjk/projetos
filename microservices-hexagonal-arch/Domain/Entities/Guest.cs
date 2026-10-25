using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        public bool ValidateState()
        {
            if (DocumentId == null ||
                string.IsNullOrWhiteSpace(DocumentId.IdNumber))
                throw new InvalidDocumentException();

            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Email))
                throw new MissingRequiredInformationException();

            return true;
        }
    }
}
