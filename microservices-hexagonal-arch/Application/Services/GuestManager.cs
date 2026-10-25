using Application.Dtos;
using Application.Ports;
using Application.Request;
using Application.Responses;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Services
{
    public class GuestManager(IGuestRepository guestRepository) : IGuestManager
    {
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDto.MapToEntity(request.Data);
                guest.ValidateState();

                request.Data.Id = await guestRepository.Create(guest);

                return new GuestResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (InvalidDocumentException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.InvalidDocumentId,
                    Message = "The document id passed is not validS"
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.MissingRequiredInformation,
                    Message = "Missing required information"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.CouldNotStoreData,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
