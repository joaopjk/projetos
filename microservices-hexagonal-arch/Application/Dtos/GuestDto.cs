using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Dtos
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public int DocumentType { get; set; }

        public static Guest MapToEntity(GuestDto dto)
        {
            if (dto == null) return null;

            return new Guest
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                DocumentId = new PersonId
                {
                    IdNumber = dto.DocumentNumber,
                    DocumentType = (Domain.Enums.DocumentType)dto.DocumentType
                }
            };
        }
    }
}
